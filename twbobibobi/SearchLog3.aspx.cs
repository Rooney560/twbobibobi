using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using twbobibobi.Model;
using twbobibobi.Repository;
using twbobibobi.Services;

namespace twbobibobi
{
    /// <summary>
    /// 用戶查詢紀錄（新版） - 改用重構後的 Service/Repository 架構
    /// </summary>
    public partial class SearchLog3 : AjaxBasePage
    {
        /// <summary>
        /// 購買人編號
        /// </summary>
        public int applicantID = 0;

        /// <summary>
        /// 宮廟編號
        /// </summary>
        public int adminID = 0;

        /// <summary>
        /// 服務項目
        /// 1-點燈 
        /// 2-普度 
        /// 4-下元補庫 
        /// 5-呈疏補庫(天官武財神聖誕補財庫) 
        /// 6-企業補財庫 
        /// 7-天赦日補運 
        /// 8-天赦日祭改 
        /// 9-關聖帝君聖誕 
        /// 10-代燒金紙 
        /// 11-天貺納福添運法會 
        /// 12-靈寶禮斗 
        /// 13-七朝清醮 
        /// 14-九九重陽天赦日補運 
        /// 15-護國息災梁皇大法會 
        /// 16-補財庫 
        /// 17-赦罪補庫 
        /// 18-天公生招財補運 
        /// 19-供香轉運 
        /// 20-安斗 
        /// 21-供花供果 
        /// 22-孝親祈福燈 
        /// 23-祈安植福
        /// 24-祈安禮斗
        /// 25-千手觀音千燈迎佛法會
        /// </summary>
        public int kind = 0;

        /// <summary>
        /// 購買人姓名
        /// </summary>
        public string AppName;

        /// <summary>
        /// 購買人姓名
        /// </summary>
        public string AppMobile;

        /// <summary>
        /// 查詢資料列表
        /// </summary>
        public string Datalist;

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark;

        /// <summary>
        /// 總金額
        /// </summary>
        public int Total;

        /// <summary>
        /// 初始化 Ajax Handler（保留原本 ac_loadServerMethod 機制）
        /// </summary>
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "checkedapplicant");
        }

        /// <summary>
        /// 頁面載入事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// AJAX 處理類別，與前端 ac_loadServerMethod 對應
        /// </summary>
        public class AjaxPageHandler
        {
            /// <summary>
            /// 查詢使用者紀錄（新版）
            /// </summary>
            /// <param name="basePage">BasePage 物件</param>
            public void checkedapplicant(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);
                basePage.SaveRequestLog(basePage.Request.Url.ToString());
                try
                {
                    string name = basePage.Request["m_Name"]?.Trim() ?? string.Empty;
                    string mobile = basePage.Request["m_Mobile"]?.Trim() ?? string.Empty;
                    string[] years = { "2025", "2024" };

                    // 初始化服務層
                    ILogQueryRepository repo = new LogQueryRepository(basePage);
                    SearchLogService service = new SearchLogService(repo);

                    // 查詢結果
                    List<ApplicantResult> results = service.SearchApplicant(name, mobile, years);

                    if (results.Count > 0)
                    {
                        var sb = new StringBuilder();
                        int total = 0;

                        foreach (var r in results)
                        {
                            total++;
                        }

                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("Datalist", sb.ToString());
                        basePage.mJSonHelper.AddContent("Total", total);
                    }
                    else
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 0);
                        basePage.mJSonHelper.AddContent("Datalist", string.Empty);
                        basePage.mJSonHelper.AddContent("Total", 0);
                    }
                }
                catch (Exception ex)
                {
                    basePage.SaveErrorLog($"【checkedapplicant】查詢發生錯誤：{ex.Message}");
                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                    basePage.mJSonHelper.AddContent("ErrorMessage", ex.Message);
                }
            }
        }
    }
}