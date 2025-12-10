using twbobibobi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using twbobibobi.FET.Data;
using twbobibobi.FET.Dto;
using twbobibobi.FET.Processors;
using twbobibobi.FET.Services;

namespace twbobibobi.FET.API
{
    public partial class CreateOrder : AjaxBasePage
    {
        // 定义两个 Stopwatch：一个测总耗时；一个测分段耗时
        private Stopwatch swTotal = new Stopwatch();
        private Stopwatch swSegment = new Stopwatch();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            //{
            //    Response.StatusCode = 405;
            //    Response.End();
            //    return;
            //}
            Response.ContentType = "application/json";

            // --- 1. 启动总耗时计时 ---
            swTotal.Start();

            try
            {
                // ① 接到请求：读取 InputStream，拿到原始 JSON
                //swSegment.Restart();

                // 1. 讀原始流、取出加密內容
                string stream;
                using (var sr = new StreamReader(Request.InputStream))
                    stream = sr.ReadToEnd();
                //swSegment.Stop();
                //SaveTimingLog("接到请求 / 读取原始 JSON", swSegment.ElapsedMilliseconds);

                // ② 从 stream 中抽出 channel、clientOrderNumber、paramContent、FETVALUE
                //swSegment.Restart();

                // 2. 從 stream 抽出必要參數
                string channel = mJSonHelper.GetStringFromJson("channel", stream);
                string clientOrderNumber = mJSonHelper.GetStringFromJson("clientOrderNumber", stream);
                string encryptText = mJSonHelper.GetStringFromJson("paramContent", stream);
                string fetValue = mJSonHelper.GetStringFromJson("FETVALUE", stream);
                //swSegment.Stop();
                //SaveTimingLog("解析 stream 拆出 channel/clientOrderNumber/paramContent/FETVALUE", swSegment.ElapsedMilliseconds);

                // ③ 决定 Environment（Dev/UAT/Prod）并取加密 Key
                //swSegment.Restart();
                // 3. 決定 Environment（Dev / UAT / Prod）與取得對應的 EncryptionKey
                var host = Request.Url.Host;

                // 從 config 拿白名單
                var uatHosts = (ConfigurationManager.AppSettings["UatHosts"] ?? "")
                                   .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // 1) 本機測試優先視為 Dev
                string env = Request.IsLocal
                    ? "Prod"
                    // 2) Host 在 UAT 白名單裡就當 UAT
                    : uatHosts.Any(h => host.IndexOf(h, StringComparison.OrdinalIgnoreCase) >= 0)
                        ? "UAT"
                        // 3) 否則就讀設定檔裡的 Environment（預設 Prod）
                        : ConfigurationManager.AppSettings["Environment"];

                // 組對應的 Fkey 名稱 和 key 名稱 
                string FkeyName = $"EncryptionKey_F{env}";
                string Fkey = ConfigurationManager.AppSettings[FkeyName];
                string keyName = $"EncryptionKey_{env}";
                string key = ConfigurationManager.AppSettings[keyName];

                // 如果沒設定到，就記錯誤並回應
                if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(Fkey))
                {
                    var err = $"[{host}] 找不到設定: {keyName} & {FkeyName}";
                    SaveErrorLog(err);
                    JSONErrorOrder(key, err);
                    return;
                }
                //swSegment.Stop();
                //SaveTimingLog("决定 Environment 并获取对应的 EncryptionKey", swSegment.ElapsedMilliseconds);


                try
                {
                    // ④.1 验签
                    //swSegment.Restart();
                    // 4. 驗簽
                    string F = mJSonHelper.Sha256(channel + Fkey + clientOrderNumber);
                    if (!F.Equals(fetValue, StringComparison.OrdinalIgnoreCase))
                        throw new Exception("驗簽失敗");
                    //swSegment.Stop();
                    //SaveTimingLog("验签 (Sha256)", swSegment.ElapsedMilliseconds);

                    // ④.2 解密
                    //swSegment.Restart();
                    // 5. 解密
                    string decrypt = AESHelper.AesDecrypt(encryptText, key);
                    if (string.IsNullOrEmpty(decrypt))
                        throw new Exception("解密失敗");
                    //swSegment.Stop();
                    //SaveTimingLog("AES 解密（AesDecrypt）", swSegment.ElapsedMilliseconds);

                    // ④.3 反序列化 DTO + 取 itemsInfo
                    //swSegment.Restart();
                    // 6. 反序列化 DTO
                    var order = JsonConvert.DeserializeObject<OrderRequestDto>(decrypt);
                    var jobj = JObject.Parse(decrypt);
                    var itemsInfo = (JArray)jobj["items"];
                    //swSegment.Stop();
                    //SaveTimingLog("反序列化 JSON 到 OrderRequestDto + 取 itemsInfo", swSegment.ElapsedMilliseconds);

                    // ⑤ 构造各层依赖并调用服务
                    //swSegment.Restart();
                    // 7. 建構各層依賴並呼叫服務
                    // 取得台北標準時間
                    DateTime dtNow = LightDAC.GetTaipeiNow();
                    var lightDAC = new LightDAC(this);
                    var codeRepo = new TempleCodeRepository(lightDAC);
                    var comboRepo = new ComboProductRuleRepository(this);
                    //var orderRepo = new OrderRepository(lightDAC, dbHelper, dtNow.Year.ToString());
                    var Year = env == "Prod" ? dtNow.ToString() : "TEST";
                    var factory = new TempleOrderProcessorFactory(this, Year);
                    var combo = new ComboProductRuleService(comboRepo);
                    var service = new OrderService(codeRepo, factory, combo);

                    SaveRequestLog(Request.Url + stream);

                    var fetOrderNumber = order.FetOrderNumber;

                    string OrderId = dtNow.ToString("yyyyMMddHHmmssfff");
                    //swSegment.Stop();
                    //SaveTimingLog("构造 Service 依赖 / 生成 OrderId（不含数据库/网络耗时）", swSegment.ElapsedMilliseconds);

                    // ⑥ 写入数据库（Insert OrderHeader + OrderDetail + …）并获取返回结果
                    swSegment.Restart();
                    var result = service
                                .ProcessOrderAsync(clientOrderNumber, order, fetOrderNumber, order.TotalAmount.ToString(), itemsInfo.ToString(), OrderId)
                                .GetAwaiter().GetResult();
                    swSegment.Stop();
                    SaveTimingLog("调用 Service.ProcessOrderAsync(Insert OrderHeader+Detail)", swSegment.ElapsedMilliseconds);

                    // ⑦ 组装并返回最终结果给客户端
                    //swSegment.Restart();
                    // 8. 組裝回傳項目結果 (此處需自行從 OrderService 取得實際編號列表)
                    JSONStringOrder(
                        key,
                        result.ClientOrderNumber,
                        OrderId,
                        result.PartnerOrderNumbers.ToArray(),
                        itemsInfo
                    );
                    //swSegment.Stop();
                    //SaveTimingLog("组装并调用 JSONStringOrder 返回给客户端", swSegment.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    // 9. 錯誤時，先寫錯誤日誌，再回應客戶端
                    // 内层业务逻辑出错时，我们也要记录当前这段的耗时
                    if (swSegment.IsRunning)
                    {
                        swSegment.Stop();
                        SaveTimingLog("业务逻辑异常阶段耗时（含验签/解密/反序列化/Service 调用）", swSegment.ElapsedMilliseconds);
                    }
                    SaveErrorLog(ex.ToString());
                    JSONErrorOrder(key, ex.Message);
                    return;
                }

                // --- 2. 整个流程结束，停掉总耗时并记录 ---
                swTotal.Stop();
                SaveTimingLog("整个 createOrder 流程总耗时", swTotal.ElapsedMilliseconds);
            }
            catch (Exception exGlobal)
            {
                // 如果上面有漏掉的异常，这里也能捕捉到并记录总耗时
                if (swTotal.IsRunning)
                {
                    swTotal.Stop();
                    SaveTimingLog("整条流程未捕获异常，总耗时 (catch 到 exGlobal)", swTotal.ElapsedMilliseconds);
                }
                SaveErrorLog(exGlobal.ToString());
                JSONErrorOrder("", exGlobal.Message);
            }
        }

    }
}