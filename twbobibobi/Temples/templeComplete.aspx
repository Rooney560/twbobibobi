<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeComplete.aspx.cs" Inherits="Temple.Temples.templeComplete" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="交易結果|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content=<%= ogurl %> />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="交易結果|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>交易結果|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
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
                    <li><a href="<%=index %>" title="首頁">首頁</a></li>
                    <li>交易結果</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="OrderForm">
                    <div class="OrderStateBk">
                        <div class="OrderStateTitle">結帳結果</div>
                        <div class="OrderState">付款成功</div>
                        <div class="OrderStateContent">
                            <%=OrderStateContent %>
                            <%--<div>
                                <div class="OrderLabel">訂單編號：</div>
                                <div class="OrderTxt">123456789</div>
                            </div>
                            <div>
                                <div class="OrderLabel">訂單時間：</div>
                                <div class="OrderTxt">2023/07/07 12:00:00</div>
                            </div>--%>
                        </div>                        
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
                        <div class="TotalCost">總計金額： $<span id="Cost" class="Cost"><%=Total %></span>元</div>
                        <div class="BackLink"><a href="../index.aspx" title="回首頁">回首頁</a></div><br />
                        <div class="QRLink">
                            <br />
                            <uc3:SocialMedia runat="server" id="SocialMedia" />
                            <%--<a href="https://page.line.me/bobibobi.tw" runat="server" id="lineurl" target="_blank">LINE好友募集中！<br />
現在只要加入保必保庇LINE好友並填寫註冊資料，就送錢母小紅包！<br /><span id="purdue" runat="server">參加普度項目，可參加錢母小紅包抽獎。<br />一個門號僅限參加一次活動。</span><span id="purdue2" runat="server" style="color: red;">中獎率百分百！</span>
數量有限，敬請把握喔！      <asp:Image ID="Qrcodeimg" runat="server" /></a>--%>
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
    })
</script>
