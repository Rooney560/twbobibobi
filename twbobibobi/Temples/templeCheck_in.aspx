<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeCheck_in.aspx.cs" Inherits="twbobibobi.Temples.templeCheck_in" %>

<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="現場訂單確認|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="<%=ogurl %>" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="現場訂單確認|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>現場訂單確認|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css?t=15963288" />
    <link href="css/css-loader.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>
    <style>        
        /*手機板*/
        @media (max-width: 1140px) {
            .PayButton input {
                margin: 5px auto;
                padding: 1.5vw 0;
                width: 88% !important;
            }

            .PayButton ul li {
                display: block;
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
        })(window, document, 'script', 'dataLayer', 'GTM-NGRZRR4V');</script>
    <!-- End Google Tag Manager -->
</head>
<body>
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
    <div class="loader loader-default" data-text="跳轉付款頁面，請勿關閉此畫面。"></div>
    <div id="wrap">
        <!--#warp //start-->

        <!--頁首選單-->
        <!-----本頁內容開始----->
        <article id="Temple" class="">
            <!--本頁路徑-->
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


                        <!--↓↓折扣碼未輸入↓↓-->
                        <%--<div class="FormInput discount">
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




                        <div class="TotalCost">總計金額： $<span id="Cost" class="Cost"><%=Total %></span>元</div>

                        <div class="PayButton">
                            <ul>
                                <li>
                                    <input name="EditOrder" type="button" id="EditOrder" value="修改" /></li>
                                <li id="fetPay" runat="server">
                                    <input name="mobile_fet_pay" type="button" id="mobile_fet_pay" runat="server" value="遠傳門號付款" /></li>
                                <li id="cardPay" runat="server">
                                    <input name="card_pay" type="button" id="card_pay" runat="server" value="信用卡付款" /></li>
                                <%--<li id="fetPay" runat="server">
                                    <input name="Google_pay" type="button" id="Google_pay" value="GOOGLE PAY" /></li>
                                    <input name="Google_pay" type="button" id="Google_pay" value="GOOGLE PAY" /></li>--%>
                                <li id="LinePay" runat="server">
                                    <input name="Line_pay" type="button" id="Line_pay" runat="server" value="LINE PAY" /></li>
                                <li id="JkosPay" runat="server">
                                    <input name="Jkos_pay" type="button" id="Jkos_pay" runat="server" value="街口支付" /></li>
                                <li id="chtPay" runat="server">
                                    <input name="mobile_cht_pay" type="button" id="mobile_cht_pay" runat="server" value="中華門號付款" /></li>
                                <li id="twmPay" runat="server">
                                    <input name="mobile_twm_pay" type="button" id="mobile_twm_pay" runat="server" value="台哥大門號付款" /></li>
                                <li>
                                    <input name="Google_pay" type="button" id="Google_pay2" value="" style="display: none" /></li>
                            </ul>
                        </div>

                    </div>
                </div>

            </section>

        </article>
        <!-----本頁內容結束----->
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
    })
</script>


<!-----付款按鈕----->
<script>
    //修改資料
    $("#EditOrder").on("click", function () {
        var getUrlString = location.href;
        var url = new URL(getUrlString);

        //switch (url.searchParams.get('kind')) {
        //    case "1":
        //        switch (url.searchParams.get('a')) {
        //            case "3":
        //                window.location = 'templeService_lights_da.aspx' + window.location.search;
        //                break;
        //            case "4":
        //                window.location = 'templeService_lights_h.aspx' + window.location.search;
        //                break;
        //            case "6":
        //                window.location = 'templeService_lights_wu.aspx' + window.location.search;
        //                break;
        //            case "8":
        //                window.location = 'templeService_lights_Fu.aspx' + window.location.search;
        //                break;
        //            case "10":
        //                window.location = 'templeService_lights_Luer.aspx' + window.location.search;
        //                if (url.searchParams.get('type') == "2") {
        //                    window.location = 'templeService_marriagelights_Luer.aspx' + window.location.search;
        //                }
        //                break;
        //            case "14":
        //                window.location = 'templeService_lights_ty.aspx' + window.location.search;
        //                break;
        //            case "15":
        //                window.location = 'templeService_lights_Fw.aspx' + window.location.search;
        //                break;
        //            case "16":
        //                window.location = 'templeService_lights_dh.aspx' + window.location.search;
        //                break;
        //            case "21":
        //                window.location = 'templeService_lights_Lk.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "2":
        //        switch (url.searchParams.get('a')) {
        //            case "3":
        //                window.location = 'templeService_purdue_da.aspx' + window.location.search;
        //                break;
        //            case "4":
        //                window.location = 'templeService_purdue_h.aspx' + window.location.search;
        //                break;
        //            case "6":
        //                window.location = 'templeService_purdue_wu.aspx' + window.location.search;
        //                break;
        //            case "8":
        //                window.location = 'templeService_purdue_Fu.aspx' + window.location.search;
        //                break;
        //            case "9":
        //                window.location = 'templeService_purdue_Jing.aspx' + window.location.search;
        //                break;
        //            case "10":
        //                window.location = 'templeService_purdue_Luer.aspx' + window.location.search;
        //                break;
        //            case "14":
        //                window.location = 'templeService_purdue_ty.aspx' + window.location.search;
        //                break;
        //            case "15":
        //                window.location = 'templeService_purdue_Fw.aspx' + window.location.search;
        //                break;
        //            case "16":
        //                window.location = 'templeService_purdue_dh.aspx' + window.location.search;
        //                break;
        //            case "21":
        //                window.location = 'templeService_purdue_Lk.aspx' + window.location.search;
        //                break;
        //            case "23":
        //                window.location = 'templeService_purdue_ma.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "3":
        //        break;
        //    case "4":
        //        switch (url.searchParams.get('a')) {
        //            case "6":
        //                window.location = 'templeService_supplies.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "5":
        //        switch (url.searchParams.get('a')) {
        //            case "6":
        //                window.location = 'templeService_supplies2.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "6":
        //        switch (url.searchParams.get('a')) {
        //            case "6":
        //                window.location = 'templeService_supplies3.aspx' + window.location.search;
        //                $("#card_pay").hide();
        //                $("#Line_pay").hide();
        //                break;
        //        }
        //        break;
        //    case "7":
        //        switch (url.searchParams.get('a')) {
        //            case "14":
        //                window.location = 'templeService_supplies_ty.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "9":
        //        switch (url.searchParams.get('a')) {
        //            case "14":
        //                if (url.searchParams.get('bobi') == 1) {
        //                    window.location = 'templeService_EmperorGuansheng_bobi_ty.aspx' + window.location.search;
        //                }
        //                else {
        //                    window.location = 'templeService_EmperorGuansheng_ty.aspx' + window.location.search;
        //                }
        //                break;
        //        }
        //        break;
        //    case "11":
        //        switch (url.searchParams.get('a')) {
        //            case "16":
        //                window.location = 'templeService_supplies_dh.aspx' + window.location.search;
        //                break;
        //        }
        //        break;
        //    case "12":
        //        switch (url.searchParams.get('a')) {
        //            case "23":
        //                if (url.searchParams.get('fet') == 1) {
        //                    window.location = 'templeService_Lingbaolidou_ma_fet.aspx' + window.location.search;
        //                }
        //                else {
        //                    window.location = 'templeService_Lingbaolidou_ma.aspx' + window.location.search;
        //                }
        //                break;
        //        }
        //        break;
        //}
    })

    //遠傳手機門號付款
    $("#mobile_fet_pay").on("click", function () {
        //付款串接放這裡
        nextStep('FetCSP');
    })

    //信用卡付款
    $("#card_pay").on("click", function () {
        //付款串接放這裡
        nextStep('CreditCard');
    })

    //Google付款
    $("#Google_pay").on("click", function () {
        //付款串接放這裡
        nextStep('GOOGLEPAY');
    })

    //line付款
    $("#Line_pay").on("click", function () {
        //付款串接放這裡
        nextStep('LinePay');
    })

    //街口支付付款
    $("#Jkos_pay").on("click", function () {
        //付款串接放這裡
        nextStep('JkosPay');
    })

    //中華手機門號付款
    $("#mobile_cht_pay").on("click", function () {
        //付款串接放這裡
        nextStep('ChtCSP');
    })

    //台哥大手機門號付款
    $("#mobile_twm_pay").on("click", function () {
        //付款串接放這裡
        nextStep('TwmCSP');
    })

    //前往付款
    function nextStep(ChargeType) {
        $(".loader").addClass("is-active");

        var getUrlString = location.href;
        var url = new URL(getUrlString);
        var kind = url.searchParams.get('kind');
        var a = url.searchParams.get('a');

        data = {
            AppMobile: $("#AppMobile").text(),
            Total: $("#Cost").text(),
            ChargeType: ChargeType
        };

        ac_loadServerMethod("gotopay", data, gotopay);
    }

    //導向付款頁面
    function gotopay(res) {

        // 重導到相關頁面
        if (res.StatusCode == 1) {
            if (res.redirect) {
                window.location = res.redirect;
                //$(".loader").removeClass("is-active");
            }
        } else {
            if (res.Timeover == 1) {
                alert("此申請人付款時間已超時20分鐘，請重新申請。");
                location = res.backURL;
            }
            else if (res.Getlisterr == 1) {
                alert(msg + " 請洽客服並告知此問題，我們將盡快為您處理！");
                var getUrlString = location.href;
                var url = new URL(getUrlString);

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
                                window.location = 'templeService_lights_Luer.aspx' + window.location.search;
                                if (url.searchParams.get('type') == "2") {
                                    window.location = 'templeService_marriagelights_Luer.aspx' + window.location.search;
                                }
                                break;
                            case "14":
                                window.location = 'templeService_lights_ty.aspx' + window.location.search;
                                break;
                            case "15":
                                window.location = 'templeService_lights_Fw.aspx' + window.location.search;
                                break;
                            case "16":
                                window.location = 'templeService_lights_dh.aspx' + window.location.search;
                                break;
                            case "21":
                                window.location = 'templeService_lights_Lk.aspx' + window.location.search;
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
                            case "9":
                                window.location = 'templeService_purdue_Jing.aspx' + window.location.search;
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

</script>
