<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingGuide.aspx.cs" Inherits="Temple.Temples.ShoppingGuide" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="購物說明|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/ShoppingGuide.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="購物說明|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>購物說明|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <style type="text/css">
        .inputBtn input {
            border: 0.2vw solid #fff;
            display: block;
            width: 100%;
            border-radius: 100px;
            height: 2.2vw;
            font-size: 1.2vw;
            color: #fff;
            background: #B91503;
        }
        .content_a {
            font-size: 1.2vw;
        }
        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }
        }
    </style>
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
</head>
<body>
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
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li><a href="ShoppingGuide.aspx" title="購物說明">購物說明</a></li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h2>購物流程：</h2>
                        <div>
                            <p>1.	選購商品：透過頁面瀏覽找到您要的商品</p>
                            <p>2.	購物結帳：在要購買的商品頁點選「立刻買」或「放入購物車」，進入結帳頁面</p>
                            <p>3.	付款選擇：請選擇付款方式，並填寫訂購資料</p>
                            <p>4.	收到訂單：會員依據本公司提供之商品數量及價格等確認機制進行下單，即表示您提出要約，並不代表雙方交易已經完成(契約尚未成立)，如果本公司確認交易條件無誤、會員所訂購之商品仍有存貨或所訂購之服務仍可提供或無其他本公司無法接受訂單之情形，此時本公司將另行寄發會員出貨通知(契約成立)，或者會員可以直接在網站上查詢出貨狀況。</p>
                        </div>
                        <br />
                        <h2>付款方式：</h2>
                        <div>
                            <p>1.	信用卡線上刷卡(線上刷卡的資料傳輸均採用SSL憑證加密，保護您的資料安全)、行動支付付款(LINE Pay、HAPPY GO Pay…等)、ATM轉帳。</p>
                            <p>2.	電信帳單代收(資料傳輸均採用SSL憑證加密，保護您的資料安全)消費明細等個人隱私不會分享給任何第三方，消費與電信帳單結合，繳費更輕鬆！。</p>
                        </div>
                        <br />
                        <h2>商品運送：</h2>
                        <div>
                            <p>1.	宅配商品將以宅配、貨運或郵局方式送達，運送區域僅限於台灣本島（外島地區的朋友請利用台灣親友地址做為收貨地址）。</p>
                            <p>2.	超商門市取貨：訂購時請選擇取貨門市，待商品送達後，系統將發送簡訊及Email提醒您取貨，請本人攜帶身分證件領取商品。</p>
                            <p>3.	一般宅配/門市取貨商品於付款完成後將於2~5個工作天內配達（不含例假日）。</p>
                        </div>
                        <br />
                        <h2>客戶服務：</h2>
                        <div>
                            <p>1.	若您有任何疑問，可加入LINE OA (ID:bobibobi.tw) 服務人員將儘速回覆予您，或可撥打客服電話04-36092299，將有專人為您服務。</p>
                        </div>
                    </div>
                </div>

            </section>

        </article>
        <!-----本頁內容結束----->
        <uc1:footer runat="server" id="footer" />
    </div>


</body>
</html>
<!----------本頁js---------->
<!-----顯示選單----->
<script>
    $(function () {
        $("header").addClass("active");

        $("#Fet").click(function () {
            window.open("https://www.fetnet.net/content/cbu/tw/digital-services/temple-service/temple02-intro.html");
        });
    })
</script>