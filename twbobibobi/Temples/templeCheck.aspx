<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeCheck.aspx.cs" Inherits="Temple.Temples.templeCheck" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="訂單確認|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="<%=ogurl %>" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="訂單確認|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>訂單確認|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css?t=15963288" />
    <link href="css/css-loader.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.cookie.min.js"></script>
    <script type="text/javascript">

        var countdown = 90;
        var sending = false;

        $(function () {

            countdown = $.cookie('validateCodeCountdown');

            if (isNaN(countdown)) {
                $.cookie('validateCodeCountdown', 0);
                countdown = 0;
            }

            if (countdown > 0) {
                sending = true;
                var obj = $("#btn");
                obj.attr("disabled", true);
                obj.val("若您沒有收到簡訊，請按此重新發送(" + countdown + "s)");
                settime(obj);
            }

            $(window).on('beforeunload unload', function () {
                if (sending) {
                    console.log(countdown);
                    $.cookie('validateCodeCountdown', countdown);
                }
                if (!sending) {
                    console.log(countdown);
                    $.cookie('validateCodeCountdown', 0);
                }
            });

            $('ul.myclass li span').click(function () {
                var $cb = $(this).parent().find(":radio");
                if (!$cb.prop("checked")) {
                    $cb.prop("checked", true);
                } else {
                    $cb.prop("checked", false);
                }
            });

            $('ul.myclass li img').click(function () {
                var $cb = $(this).parent().find(":radio");
                if (!$cb.prop("checked")) {
                    $cb.prop("checked", true);
                } else {
                    $cb.prop("checked", false);
                }
            });

            //if (isAppleDevice()) {
            //    $("#applePay").show();
            //}
            //else {
            //    $("#applePay").hide();
            //}
        });

        function sendsms(res) {
            // 重導到相關頁面
            if (res.StatusCode == 1) {
                countdown = 90;
                sending = true;
                var obj = $("#btn");
                settime(obj);
            } else {
                if (res.CodeError != 0) {
                    switch (res.CodeError) {
                        case "-2":
                            alert("驗證碼超時，請重新發送新的驗證碼！如一直錯誤，請聯繫客服！");
                            $("#Code").focus();
                            $("#Code").addClass('unfilled');
                            break;
                        case "-3":
                            alert("驗證碼輸入錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                            $("#Code").focus();
                            $("#Code").addClass('unfilled');
                            break;
                        case "-4":
                            alert("更新驗證碼狀態錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                            $("#Code").focus();
                            $("#Code").addClass('unfilled');
                            break;
                        case "-5":
                            alert("此購買人電話當日超過三次寄發驗證碼，請返回修改購買人電話！");
                            $("#Code").focus();
                            $("#Code").addClass('unfilled');
                            break;
                        default:
                            alert("驗證碼傳送失敗！請再試一次，如再錯誤，請洽客服！");
                            break;
                    }
                }
                else {
                    alert("驗證碼傳送失敗！請再試一次，如再錯誤，請洽客服！");
                }
            }
        }

        function send() {
            data = {
                AppMobile: $("#AppMobile").text(),
                AppEmail: $("#AppEmail").text()
            };
            ac_loadServerMethod("sendsms", data, sendsms);
            //sending = true;
            //var obj = $("#btn");
            //settime(obj);
        }

        $("input[name='verifyType']").on("change", function () {
            var type = $(this).val();
            if (type === "sms") {
                $(".AppMobile").show();
                $(".AppEmail").hide();
            } else {
                $(".AppMobile").hide();
                $(".AppEmail").show();
            }
        });

        function settime(obj) { //發送驗證碼倒數計時
            if (countdown == 0) {
                obj.attr('disabled', false);
                obj.val("發送驗證碼");
                countdown = 90;
                sending = false;
                return;
            } else {
                obj.attr('disabled', true);
                obj.val("若您沒有收到簡訊，請按此重新發送(" + countdown + "s)");
                countdown--;
            }
            setTimeout(function () {
                settime(obj);
            }, 1000)
        }

        function isAppleDevice() {
            const ua = navigator.userAgent;
            const platform = navigator.platform;

            // iPhone / iPad / iPod
            if (/iPhone|iPad|iPod/.test(ua)) return true;

            // Mac
            if (platform === "MacIntel" && "ontouchend" in document) {
                // iPad 偽裝成 Mac
                return true;
            }
            if (/Macintosh/.test(ua)) return true;

            return false;
        }

    </script>
    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>
    <style>
        .sendsms {
            width: 90%;
            margin: 0 auto 2vw;
            border-bottom: 5px solid #5e0b15;
        }

        .sendsms .cehckBtn {
            background: #587291;
            color: #fff;
        }
        
        .payType {
            width: 90%;
            margin: 0 auto 2vw;
            border-bottom: 5px solid #5e0b15;
        }

        .checkedbox {
            vertical-align: middle;
            -webkit-transform: scale(1.2);
            -moz-transform: scale(1.2);
            -ms-transform: scale(1.2);
            transform: scale(1.2);
            -webkit-transform-origin: right;
            -moz-transform-origin: right;
            -ms-tranform-origin: right;
            transform-origin: right;
            height: 40px;
            width: 1.5vw;
            margin-bottom: 4px;
            position: relative;
            border-radius: 2px;
        }
        
        .payType input.checkedbox,
        .label input  {
            width: 1vw;
            margin-left: 5px;
        }

        .payType img {
            width: auto;
            display: inline;
            top: 0px;
            left: 0px;
        }

        .payType ul li {
            height: 40px;
        }

        .alertmsg {
            font-size: 0.8vw;
        }

        .aDate {
            margin-left: 30px;
        }

        /*.TotalCost {
            width: 90%;
            margin: 0 auto 2vw;
            border-bottom: 5px solid #5e0b15;
        }*/
        
        /* 容器：預設為橫向排列 */
        .invoice-types {
          display: flex;
          flex-wrap: wrap;
          gap: 3vw;                /* 選項之間的水平/垂直間距 */
          margin: 1vw 0;             /* 上下 margin */
        }

        /* 單一選項 */
        .invoice-types label {
            width: 10vw;
            font-weight: bold;
          display: flex;
          align-items: center;
          font-size: 1.1rem;
          color: #800000;            /* 用你頁面標題色，例如深紅 */
        }

        /* radio 與文字的間距 */
        .invoice-types input[type="radio"] {
          margin-right: 0.5rem;
        }

        /* 優惠訊息 */
        .pay-msg {
            list-style: none;   /* 不要顯示圓點 */
            margin-left: 30px;  /* 對齊支付圖示 */
            margin-top: -5px;   /* 視覺調整，讓它緊貼 */
            color: red;
            font-weight: bold;
        }

        /*手機板*/
        @media only screen and (max-width: 720px) {
            .PayButton input {
                margin: 5px auto;
                padding: 1.5vw 0;
                width: 88% !important;
            }

            .payType input.checkedbox,
            .label input{
                width: 5vw !important;
                margin-left: 5px;
            }

            .PayButton ul li {
                display: block;
            }

            /*.TotalCost {
                width: 100%;
            }*/

            .payType {
                width: 100%;
            }

            .aDate {
                margin-left: 30px;
            }

            .alertmsg {
                font-size: 2.8vw;
            }

            .sendsms {
                padding: 1vw;
                width: 100%;
            }

            .aDate {
                margin-left: 30px;
            }

            .alertmsg {
                font-size: 2.8vw;
            }

            .invoice-types {
                flex-direction: column;
                gap: 1vw;
            }
            
            .invoice-types label {
                width: 100%;
            }
        }
    </style>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async="" src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'G-4YWFRTFCTT');
    </script>
    <!-- Google Tag Manager -->
    <script>(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-5L2H7Z3N');</script>
    <!-- End Google Tag Manager -->
    <!-- Google Tag Manager -->
    <script>(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-NGRZRR4V');</script>
    <!-- End Google Tag Manager -->
</head>
<body>
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-NGRZRR4V"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-5L2H7Z3N"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
    <div class="loader loader-default" data-text="跳轉付款頁面，請勿關閉此畫面。"></div>
    <div id="wrap">
        <!--#warp //start-->

        <!--頁首選單-->
        <uc2:header runat="server" id="header" />
        <!-----本頁內容開始----->
        <article id="Temple" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li>訂單確認</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="OrderForm">
                    <div class="OrderStateBk">
                        <div class="OrderStateTitle">訂單確認</div>
                        <div class="OrderPurchaser">
                            <!--↓↓訂購代表人↓↓-->
                            <%=OrderPurchaser %>
                            <%--<div class="OrderData">
                                <div class="label">申請人姓名：</div>
                                <div class="txt">王小明</div>
                            </div>
                            <div class="OrderData">
                                <div class="label">聯絡電話：</div>
                                <div class="txt">0912345678</div>
                            </div>
                            <div class="OrderData">
                                <div class="label">電子信箱：</div>
                                <div class="txt">email@mail.com</div>
                            </div>--%>
                            <!--↑↑訂購代表人↑↑-->
                        </div>

                        <div class="OrderInfo">
                            <ul>
                                <li class="OrderInfoTitle">
                                    <div>項目</div>
                                    <div>金額</div>
                                </li>
                                <!--↓↓一個<li>為一個項目↓↓-->
                                <%=OrderInfo %>
                                <%--<li>
                                    <div>
                                        <div class="ProductsName">光明燈</div>
                                        <div class="ProductsInfo">
                                            <div class="OrderData">
                                                <div class="label">祈福人：</div>
                                                <div class="txt">王小明</div>
                                            </div>
                                            <div class="OrderData">
                                                <div class="label">聯絡電話：</div>
                                                <div class="txt">0912345678</div>
                                            </div>
                                            <div class="OrderData">
                                                <div class="label">農歷生日：</div>
                                                <div class="txt">民國90年1月1日</div>
                                            </div>
                                            <div class="OrderData">
                                                <div class="label">地址：</div>
                                                <div class="txt"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>150元</div>
                                </li>--%>
                                <!--↑↑一個<li>為一個項目↑↑-->
                            </ul>
                        </div>

                        <!--↓↓OTP認證↓↓-->
                        <div class="FormInput sendsms">
                            <div class="verify-type">
                                <label><input type="radio" name="verifyType" value="sms" checked /> 手機驗證</label>
                                <label><input type="radio" name="verifyType" value="email" /> Email 驗證</label>
                            </div>

                            <div id="verifyTarget">
                                <div class="AppMobile">
                                    <h1 class="TempleName AppMobile" style="display:inline;"><%=AppMobile %></h1>
                                    <span class="AppMobile" style="color: red;">我們將會寄發簡訊至您的購買人電話，已驗證您的電話是否正確。(每筆每日以3次為限)<br />
                                        請輸入驗證碼後，再點選您所要支付的按鈕。
                                    </span>
                                </div>
                                <div class="AppEmail">
                                    <h1 class="TempleName AppEmail" id="AppEmail" style="display:none;"><%=AppEmail %></h1>
                                    <span class="AppEmail" style="color: red; display:none;">我們將會寄發驗證碼至您的購買人信箱，已驗證您的信箱是否正確。<br />
                                        請輸入驗證碼後，再點選您所要支付的按鈕。
                                    </span>
                                </div>
                            </div>

                            <label>簡訊驗證</label><input id="Code" type="tel" placeholder="請輸入簡訊驗證碼" /><br />
                            <div style="margin-bottom: 2vw">
                                <input type="button" class="cehckBtn" id="btn" value="發送驗證碼" onclick="send()" />
                            </div>
                        </div>
                        <!--↑↑OTP認證↑↑-->


                        <!--↓↓折扣碼未輸入↓↓-->
                       <%-- <div class="FormInput discount">
                            <label>折扣序號</label><input type="text" placeholder="請輸入折扣序號" /><div>
                                <input type="button" class="cehckBtn" value="檢查折扣碼" /></div>
                        </div>--%>
                        <!--↑↑折扣碼未輸入↑↑-->


                        <!--↓↓折扣碼套用成功↓↓-->
                        <!--<div class="FormInput discount"><label>折扣序號</label><div class="discountOk">已成功折扣150元</div><div><input type="button" class="cehckBtn" value="移除折扣碼"></div></div>-->
                        <!--↑↑折扣碼套用成功↑↑-->


                        <!--要啟用警告，在class加入"active"即可-->
                        <div class="Notice">
                            <!--警告說明-->
                            折扣碼錯誤！請檢查後再重新輸入。                    
                        </div>

                        <div class="payType">
                            <h1 class="TempleName">付款方式</h1>
                            <ul class="myclass">             
                                <li id="fetPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="mobile_fet_pay" runat="server" value="FetCSP" />
                                    <img src="./images/FETCsp.png" alt="遠傳門號付款" title="遠傳門號付款" /><span>遠傳門號付款</span>
                                    </li>           
                                <li id="JkosPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="Jkos_pay" runat="server" value="JkosPay" />                                    
                                    <img src="./images/JKOSPay.png" alt="JKOSPAY" title="JKOSPAY" />
                                </li>                       
                                <li id="LinePay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="Line_pay" runat="server" value="LinePay" />                                    
                                    <img src="./images/LINEPay2.png" alt="LINEPAY" title="LINEPAY" />
                                </li>              
                                <li id="PXPayPlusPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="PXPayPlus" runat="server" value="PXPayPlus" />                                    
                                    <img src="./images/PXPayPlus3.png" alt="PXPayPlus" title="PXPayPlus" />
                                </li>  
                                <li id="pxpayMsg" class="pay-msg">
                                    連結帳戶付款：筆筆3%回饋無上限 <br/>
                                    2025/02/01-2025/09/30
                                </li>    
                                <li id="cardPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="card_pay" runat="server" value="CreditCard" />
                                    <img src="./images/CreditCard.png?t=12588" alt="信用卡付款" title="信用卡付款" /><span>信用卡付款</span>
                                </li>
                                <li id="twmPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="mobile_twm_pay" runat="server" value="TwmCSP" />
                                    <img src="./images/TwmCSP.png" alt="台哥大門號付款" title="台哥大門號付款" /><span>台哥大門號付款</span>
                                    </li>
                                <li id="chtPay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="mobile_cht_pay" runat="server" value="ChtCSP" />
                                    <img src="./images/ChtCSP.png" alt="中華門號付款" title="中華門號付款" /><span>中華門號付款</span>
                                    </li>
                                <li id="ApplePay" runat="server">
                                    <input name="pay" type="radio" class="checkedbox" id="Apple_pay" runat="server" value="ApplePay" />
                                    <img src="./images/ApplePay2.png" alt="APPLEPAY" title="APPLEPAY" /><span></span>
                                    </li>
                            </ul>
                        <br />
                        <br />
                        </div>
                        
                        <!--↓↓發票資料↓↓-->
                        <div class="FormInput sendsms">
                            <h1 class="TempleName" style="display: inline;">發票資料</h1>
                            <span style="color: red;">（使用電信帳單支付，將由電信業者開予發票。）</span>

                            <!-- 發票類型選擇 -->
                            <p class="invoice-types invoice">
                                <label class="label">
                                    <input type="radio" name="InvoiceType" class="checkedbox" value="1" checked="checked" />
                                    一般電子發票
                                </label>
                                <label class="label">
                                    <input type="radio" name="InvoiceType" class="checkedbox" value="2" />
                                    手機載具發票
                                </label>
                                <%--<label>
                                <input type="radio" name="InvoiceType" class="checkedbox" value="3" />
                                發票捐贈
                              </label>--%>
                                <label class="label">
                                    <input type="radio" name="InvoiceType" class="checkedbox" value="4" />
                                    公司發票
                                </label>

                                <!-- 載具代碼輸入 + 挑選常用按鈕 -->
                                <p id="InvoiceNum">
                                    <label>統一編號 </label>
                                    <input id="InvoiceCode" type="text"
                                        placeholder="請輸入統一編號"
                                        style="width: 80%" /><br />
                                    <label>公司名稱 </label>
                                    <input id="InvoiceName" type="text"
                                        placeholder="請輸入公司名稱"
                                        style="width: 80%" /><br />
                                </p>
                                <p id="MobileCarrier">
                                    <label>手機載具</label>
                                    <input id="CarrierCode" type="text"
                                        placeholder="開頭為 &quot;/&quot;之條碼，共8碼"
                                        style="width: 80%" /><br />
                                </p>
                            </p>

                            <!-- 載具選擇（僅手機條碼／會員／自然人） -->
                            <%--<div class="carrier-types" style="margin: 1vw 0;">
                                <label>
                                    <input type="radio" name="CarrierOption" value="member" />
                                    會員載具</label>
                                <label>
                                    <input type="radio" name="CarrierOption" value="mobile" />
                                    手機條碼載具</label>
                                <label>
                                    <input type="radio" name="CarrierOption" value="citizen" />
                                    自然人憑證載具</label>
                            </div>--%>
                            <br />
                        </div>
                        <!--↑↑發票資料↑↑-->

                        <div class="TotalCost">總計金額： $<span id="Cost" class="Cost"><%=Total %></span>元</div>

                        <div class="PayButton">
                            <ul>
                                <li>
                                    <input name="EditOrder" type="button" id="EditOrder" value="修改" /></li>
                                <li>
                                    <input name="SubmitOrder" type="button" id="SubmitOrder" value="確認" /></li>
                                <li>
                                    <input name="Google_pay" type="button" id="Google_pay3" value="" style="display: none" /></li>
                            </ul>
                        </div>

                    </div>
                </div>

            </section>

        </article>
        <!-----本頁內容結束----->
        <uc1:footer runat="server" id="footer" />
    </div>
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-ABCDEFGH"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
</body>
</html>
<!----------本頁js---------->
<!-----顯示選單----->
<script>
    $(function () {
        $("header").addClass("active");

        $('input[name="pay"]').eq(0).prop('checked', true);

        if ($("#PXPayPlusPay").length > 0) {
            // 存在
            checkPromoExpired("pxpayMsg");
        }
        else {
            $("#pxpayMsg").hide();
        }

        $('input[name="pay"]').click(function () {
            var $radio = $(this);
            //alert($radio.val());
            if ($radio.data('checked')) {
                $radio.prop('checked', false);
                $radio.data('checked', false);
            } else {
                $radio.prop('checked', true);
                $radio.data('checked', true);
            }
        })

        // 快取區塊
        var $numSection = $('#InvoiceNum'),
            $mobileSection = $('#MobileCarrier'),
            $invoiceInfo = $('.invoice'),
            $radios_invoice = $('input[name="InvoiceType"]'),
            $radios_pay = $('input[name="pay"]');

        // 根據選擇顯示／隱藏
        function updateInvoiceFields() {
            var sel = $radios_invoice.filter(':checked').val();
            if (sel === '1') {
                $numSection.hide();
                $mobileSection.hide();
            }
            else if (sel === '2') {
                $numSection.hide();
                $mobileSection.show();
            }
            else if (sel === '4') {
                $numSection.show();
                $mobileSection.hide();
            }
        }
        function updatepayFields() {
            var sel = $radios_pay.filter(':checked').val();

            if (sel.indexOf('CSP') >= 0) {
                $invoiceInfo.hide();
            }
            else {
                $invoiceInfo.show();
            }
        }

        // 綁定 change 事件
        $radios_invoice.on('change', updateInvoiceFields);
        $radios_pay.on('change', updatepayFields);

        // 初始執行一次
        updateInvoiceFields();
        updatepayFields();
    })

    /**
    * @summary 判斷指定活動訊息 <li> 是否已過期
    * @param {string} elementId - 要檢查的 <li> 元素 ID
    * @description
    * 1. 從指定元素文字中自動解析日期範圍（格式：yyyy/MM/dd-yyyy/MM/dd）
    * 2. 若今日超過活動結束日則自動隱藏
    * 3. 若日期解析失敗，可設定 fallback 結束日期
    * @example
    * checkPromoExpired("pxpayMsg");
    */
    function checkPromoExpired(elementId) {
        var $li = $("#" + elementId);
        if ($li.length === 0) return; // 沒找到元素就跳出

        // 取得 li 的文字內容
        var text = $li.text();

        // 以正則取出 yyyy/MM/dd 日期格式
        var matches = text.match(/\d{4}\/\d{2}\/\d{2}/g);

        // 檢查是否有日期範圍
        if (matches && matches.length >= 2) {
            var endDateStr = matches[1]; // 第二個日期為結束日
            var endDate = new Date(endDateStr.replace(/\//g, "-"));
            var today = new Date();

            // 將結束時間設為當天 23:59:59
            endDate.setHours(23, 59, 59, 999);

            if (today > endDate) {
                $li.hide();
            }
        } else {
            // 若沒有找到日期，可設定預設結束日期（可依需求調整）
            $li.show();
        }
    }
</script>


<!-----付款按鈕----->
<script>
    //修改資料
    $("#EditOrder").on("click", function () {
        var getUrlString = location.href;
        var url = new URL(getUrlString);
        $.cookie('validateCodeCountdown', 0);
        countdown = 0;

        backURL(url);
    })

    ////遠傳手機門號付款
    //$("#mobile_fet_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('FetCSP');
    //})

    ////信用卡付款
    //$("#card_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('CreditCard');
    //})

    ////Google付款
    //$("#Google_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('GOOGLEPAY');
    //})

    ////line付款
    //$("#Line_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('LinePay');
    //})

    ////街口支付付款
    //$("#Jkos_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('JkosPay');
    //})

    ////全支付付款
    //$("#PXPayPlus").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('PXPayPlus');
    //})

    ////中華手機門號付款
    //$("#mobile_cht_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('ChtCSP');
    //})

    ////台哥大手機門號付款
    //$("#mobile_twm_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('TwmCSP');
    //})

    ////銀聯卡付款
    //$("#union_pay").on("click", function () {
    //    //付款串接放這裡
    //    nextStep('UnionPay');
    //})

    //確認付款
    $("#SubmitOrder").on("click", function () {
        //付款串接放這裡
        var payType = $('input[name="pay"]:checked').val();

        if (typeof (payType) == "undefined") {
            alert("請選擇付款方式！");
        }
        else {
            nextStep(payType);
        }
    })

    //前往付款
    function nextStep(ChargeType) {
        var isValid = true;

        // 1. 先驗證簡訊 OTP
        var otp = $('#Code').val() ? $('#Code').val().trim() : '';
        if (!otp) {
            isValid = false;
            $('#Code').addClass('unfilled');
            $('.Notice').text('請輸入驗證碼').addClass('active');
            $('#Code').focus();
        } else {
            $('#Code').removeClass('unfilled');
            $('.Notice').removeClass('active');
        }

        // 2. 如果 OTP 合格，再驗證發票欄位
        if (isValid) {
            var invoiceType = $('input[name="InvoiceType"]:checked').val();

            if (invoiceType === '2') {
                // 手機載具發票 → 檢查手機載具欄位
                var carrier = $('#CarrierCode').val() ? $('#CarrierCode').val().trim() : '';
                if (!carrier) {
                    isValid = false;
                    $('#CarrierCode').addClass('unfilled');
                    $('.Notice').text('請輸入手機載具條碼').addClass('active');
                    $('#CarrierCode').focus();
                } else {
                    $('#CarrierCode').removeClass('unfilled');
                }
            }
            else if (invoiceType === '4') {
                // 公司發票 → 檢查統編 + 公司名稱
                var invCode = $('#InvoiceCode').val() ? $('#InvoiceCode').val().trim() : '';
                var invName = $('#InvoiceName').val() ? $('#InvoiceName').val().trim() : '';
                if (!invCode || !invName) {
                    isValid = false;
                    if (!invCode) $('#InvoiceCode').addClass('unfilled'); else $('#InvoiceCode').removeClass('unfilled');
                    if (!invName) $('#InvoiceName').addClass('unfilled'); else $('#InvoiceName').removeClass('unfilled');
                    $('.Notice').text('請填寫統一編號與公司名稱').addClass('active');
                    (invCode ? $('#InvoiceName') : $('#InvoiceCode')).focus();
                } else {
                    $('#InvoiceCode, #InvoiceName').removeClass('unfilled');
                }
            }
        }

        // 若都通過，就繼續送出
        if (isValid) {
            var getUrlString = location.href;
            var url = new URL(getUrlString);
            var kind = url.searchParams.get('kind');
            var a = url.searchParams.get('a');

            $radios = $('input[name="InvoiceType"]');

            data = {
                AppName: $("#AppName").text(),
                AppMobile: $("#AppMobile").text(),
                AppEmail: $("#AppEmail").text(),
                Total: $("#Cost").text(),
                Code: otp,
                InvoiceType: invoiceType,
                CarrierCode: $("#CarrierCode").val(),
                InvoiceCode: $("#InvoiceCode").val(),
                InvoiceName: $("#InvoiceName").val(),
                ChargeType: ChargeType
            };

            var checkedPay = true;

            //if (ChargeType == "ApplePay") {
            //    if (!isAppleDevice()) {
            //        alert("僅限APPLE裝置使用！請選擇其他支付方式！")
            //        checkedPay = false;
            //    }
            //}

            if (checkedPay) {
                $(".loader").addClass("is-active");
                ac_loadServerMethod("gotopay", data, gotopay);
            }
        }
    }

    //導向付款頁面
    function gotopay(res) {
        console.log(">> gotopay callback, res =", res);
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            $("#Code").removeClass('unfilled');
            if (res.redirect) {
                $.cookie('validateCodeCountdown', 0);
                countdown = 0;
                window.location = res.redirect;
                //$(".loader").removeClass("is-active");
            }
        }
        else if (res.StatusCode == -1)
        {
            $(".loader").removeClass("is-active");
            alert("訪問失敗。\n錯誤訊息：「" + res.ErrorMessage + "」\n請聯繫客服或在官方LINE留下錯誤訊息。");
        }
        else {
            $(".loader").removeClass("is-active");
            if (res.Timeover == 1 && res.Timeover !== undefined) {
                alert("此申請人付款時間已超時20分鐘，請重新申請。");
                location = res.backURL;
            }
            else if (res.Getlisterr == 1 && res.Getlisterr !== undefined) {
                alert(msg + " 請洽客服並告知此問題，我們將盡快為您處理！");
                var getUrlString = location.href;
                var url = new URL(getUrlString);

                backURL(url);
            }
            else if (res.CodeError != 0 && res.CodeError !== undefined) {
                switch (res.CodeError) {
                    case "-2":
                        alert("驗證碼超時，請重新發送新的驗證碼！如一直錯誤，請聯繫客服！");
                        $("#Code").focus();
                        $("#Code").addClass('unfilled');
                        break;
                    case "-3":
                        alert("驗證碼輸入錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                        $("#Code").focus();
                        $("#Code").addClass('unfilled');
                        break;
                    case "-4":
                        alert("更新驗證碼狀態錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                        $("#Code").focus();
                        $("#Code").addClass('unfilled');
                        break;
                    case "-5":
                        alert("此購買人電話當日超過三次寄發驗證碼，請返回修改購買人電話！");
                        $("#Code").focus();
                        $("#Code").addClass('unfilled');
                        break;
                    case "-6":
                        alert("手機載具格式錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                        $("#CarrierCode").focus();
                        $("#CarrierCode").addClass('unfilled');
                        break;
                    case "-7":
                        alert("電子發票綁定資料錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
                        $("#CarrierCode").focus();
                        $("#CarrierCode").addClass('unfilled');
                        break;
                    case "-8":
                        alert("此訂單已開立發票，請重新購買！如一直錯誤，請聯繫客服！");

                        var getUrlString = location.href;
                        var url = new URL(getUrlString);

                        backURL(url);
                        break;
                }
            }
            else {
                if (res.overnumType == 3) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else if (res.overnumType == 4) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else if (res.overnumType == 5) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else if (res.overnumType == 6) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else if (res.overnumType == 8) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else if (res.overnumType == 10) {
                    alert(res.LightsString + "燈種已額滿，請重新填單。或至其他宮廟點燈。");
                }
                else {
                    alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
                }
            }
        }
    }

    function backURL(url) {
        switch (url.searchParams.get('kind')) {
            case "1":
                switch (url.searchParams.get('a')) {
                    case "3":
                        window.location = 'templeService_lights_da.aspx' + window.location.search;
                        break;
                    case "4":
                        window.location = 'templeService_lights_h.aspx' + window.location.search;
                        break;
                    case "6":
                        window.location = 'templeService_lights_wu.aspx' + window.location.search;
                        break;
                    case "8":
                        window.location = 'templeService_lights_Fu.aspx' + window.location.search;
                        break;
                    case "10":
                        if (url.searchParams.get('type') == "2") {
                            window.location = 'templeService_marriagelights_Luer.aspx' + window.location.search;
                        }
                        else {
                            window.location = 'templeService_lights_Luer.aspx' + window.location.search;
                        }
                        break;
                    case "14":
                        if (url.searchParams.get('type') == "2") {
                            window.location = 'templeService_lights_ty_mom.aspx' + window.location.search;
                        }
                        else {
                            window.location = 'templeService_lights_ty.aspx' + window.location.search;
                        }
                        break;
                    case "15":
                        window.location = 'templeService_lights_Fw.aspx' + window.location.search;
                        break;
                    case "16":
                        window.location = 'templeService_lights_dh.aspx' + window.location.search;
                        break;
                    case "17":
                        window.location = 'templeService_lights_Hs.aspx' + window.location.search;
                        break;
                    case "21":
                        window.location = 'templeService_lights_Lk.aspx' + window.location.search;
                        break;
                    case "23":
                        window.location = 'templeService_lights_ma.aspx' + window.location.search;
                        break;
                    case "31":
                        window.location = 'templeService_lights_wjsan.aspx' + window.location.search;
                        break;
                    case "32":
                        window.location = 'templeService_lights_ld.aspx' + window.location.search;
                        break;
                    case "35":
                        window.location = 'templeService_lights_st.aspx' + window.location.search;
                        break;
                    case "38":
                        window.location = 'templeService_lights_bj.aspx' + window.location.search;
                        break;
                    case "39":
                        window.location = 'templeService_lights_sbbt.aspx' + window.location.search;
                        break;
                    case "40":
                        window.location = 'templeService_lights_bpy.aspx' + window.location.search;
                        break;
                    case "41":
                        window.location = 'templeService_lights_ssy.aspx' + window.location.search;
                        break;
                }
                break;
            case "2":
                switch (url.searchParams.get('a')) {
                    case "3":
                        window.location = 'templeService_purdue_da.aspx' + window.location.search;
                        break;
                    case "4":
                        window.location = 'templeService_purdue_h.aspx' + window.location.search;
                        break;
                    case "6":
                        window.location = 'templeService_purdue_wu.aspx' + window.location.search;
                        break;
                    case "8":
                        window.location = 'templeService_purdue_Fu.aspx' + window.location.search;
                        break;
                    case "10":
                        window.location = 'templeService_purdue_Luer.aspx' + window.location.search;
                        break;
                    case "14":
                        window.location = 'templeService_purdue_ty.aspx' + window.location.search;
                        break;
                    case "15":
                        window.location = 'templeService_purdue_Fw.aspx' + window.location.search;
                        break;
                    case "16":
                        window.location = 'templeService_purdue_dh.aspx' + window.location.search;
                        break;
                    case "21":
                        window.location = 'templeService_purdue_Lk.aspx' + window.location.search;
                        break;
                    case "23":
                        window.location = 'templeService_purdue_ma.aspx' + window.location.search;
                        break;
                    case "31":
                        window.location = 'templeService_purdue_wjsan.aspx' + window.location.search;
                        break;
                }
                break;
            case "3":
                break;
            case "4":
                switch (url.searchParams.get('a')) {
                    case "6":
                        window.location = 'templeService_supplies.aspx' + window.location.search;
                        break;
                }
                break;
            case "5":
                switch (url.searchParams.get('a')) {
                    case "6":
                        window.location = 'templeService_supplies2.aspx' + window.location.search;
                        break;
                }
                break;
            case "6":
                switch (url.searchParams.get('a')) {
                    case "6":
                        window.location = 'templeService_supplies3.aspx' + window.location.search;
                        $("#card_pay").hide();
                        $("#Line_pay").hide();
                        break;
                }
                break;
            case "7":
                switch (url.searchParams.get('a')) {
                    case "14":
                        window.location = 'templeService_supplies_ty.aspx' + window.location.search;
                        break;
                    case "23":
                        window.location = 'templeService_supplies_ma.aspx' + window.location.search;
                        break;
                }
                break;
            case "9":
                switch (url.searchParams.get('a')) {
                    case "14":
                        if (url.searchParams.get('bobi') == 1) {
                            window.location = 'templeService_EmperorGuansheng_bobi_ty.aspx' + window.location.search;
                        }
                        else {
                            window.location = 'templeService_EmperorGuansheng_ty.aspx' + window.location.search;
                        }
                        break;
                }
                break;
            case "11":
                switch (url.searchParams.get('a')) {
                    case "16":
                        window.location = 'templeService_supplies_dh.aspx' + window.location.search;
                        break;
                }
                break;
            case "12":
                switch (url.searchParams.get('a')) {
                    case "23":
                        if (url.searchParams.get('fet') == 1) {
                            window.location = 'templeService_Lingbaolidou_ma_fet.aspx' + window.location.search;
                        }
                        else {
                            window.location = 'templeService_Lingbaolidou_ma.aspx' + window.location.search;
                        }
                        break;
                }
                break;
            case "13":
                switch (url.searchParams.get('a')) {
                    case "3":
                        window.location = 'templeService_TaoistJiaoCeremony_da.aspx' + window.location.search;
                        break;
                }
                break;
            case "14":
                switch (url.searchParams.get('a')) {
                    case "14":
                        window.location = 'templeService_supplies2_ty.aspx' + window.location.search;
                        break;
                }
                break;
            case "15":
                switch (url.searchParams.get('a')) {
                    case "16":
                        window.location = 'templeService_lybc_dh.aspx' + window.location.search;
                        break;
                }
                break;
            case "16":
                switch (url.searchParams.get('a')) {
                    case "15":
                        window.location = 'templeService_supplies_Fw.aspx' + window.location.search;
                        break;
                    case "21":
                        window.location = 'templeService_supplies_Lk.aspx' + window.location.search;
                        break;
                }
                break;
            case "17":
                switch (url.searchParams.get('a')) {
                    case "33":
                        window.location = 'templeService_supplies_sx.aspx' + window.location.search;
                        break;
                }
                break;
            case "18":
                switch (url.searchParams.get('a')) {
                    case "14":
                        window.location = 'templeService_supplies3_ty.aspx' + window.location.search;
                        break;
                }
                break;
            case "19":
                switch (url.searchParams.get('a')) {
                    case "33":
                        window.location = 'templeService_supplies2_sx.aspx' + window.location.search;
                        break;
                }
                break;
            case "20":
                switch (url.searchParams.get('a')) {
                    case "15":
                        window.location = 'templeService_andou_Fw.aspx' + window.location.search;
                        break;
                    case "31":
                        window.location = 'templeService_andou_wjsan.aspx' + window.location.search;
                        break;
                }
                break;
            case "21":
                switch (url.searchParams.get('a')) {
                    case "31":
                        window.location = 'templeService_huaguo_wjsan.aspx' + window.location.search;
                        break;
                }
                break;
            case "23":
                switch (url.searchParams.get('a')) {
                    case "35":
                        window.location = 'templeService_blessing_st.aspx' + window.location.search;
                        break;
                }
                break;
            case "25":
                switch (url.searchParams.get('a')) {
                    case "14":
                        window.location = 'templeService_qnlight_ty.aspx' + window.location.search;
                        break;
                }
                break;
        }
    }
</script>