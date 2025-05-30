﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2023lights.aspx.cs" Inherits="Temple.Temples.newsContent_2023lights" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２３新春兔年點燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2023lights.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２３新春兔年點燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２３新春兔年點燈|最新消息|【保必保庇】線上宮廟服務平台</title>
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
</head>
<body>
    <div id="wrap">
        <!--#warp //start-->

        <!--頁首選單-->
        <uc2:header runat="server" id="header" />
        <!-----本頁內容開始----->
        <article id="News" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li><a href="news.aspx" title="最新消息">最新消息</a></li>
                    <li>２０２３新春兔年點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20221125_LightsImg.png" width="1160" height="550" alt="" /></div>
                <div class="NewsBk">
                    <h1>２０２３新春兔年點燈</h1>
                    <div class="NewsDate">發布日期：2022-11-25</div>
                    <div class="NewsContent Content">

                        <!--內容放這裡 //Start-->
                        <p>2023新春兔年點燈開放線上報名~數量有限,額滿即止!</p>
                        <h2>新年新氣象 | 點燈迎好運 | 線上報名最方便</h2>
                        <p>大甲鎮瀾宮：光明燈、太歲燈、文昌燈</p>
                        <p>新港奉天宮：光明燈、太歲燈</p>
                        <p>北港武德宮：光明燈、太歲燈、財神燈</p>

                        <h2>Y23癸卯年犯太歲生肖如下：</h2>
                        <p>兔 (座太歲)</p>
                        <p>雞 (衝太歲)</p>
                        <p>馬 (破太歲)</p>
                        <p>鼠 (刑太歲)</p>
                        <p>以上生肖建議要安太歲。</p>

                        <h2>保必保庇(線上報名)報名最方便，數量有限，敬請把握！</h2>
                        <!--內容放這裡 //End-->
                    </div>
                </div>

                <div class="BackBtn">
                    <a href="news.aspx">回上一層</a>
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
    })
</script>
<!-----照片----->
<script src="js/fancybox.umd.js"></script>
<link rel="stylesheet"
    href="css/fancybox.css" />
<script>
    Fancybox.bind("[data-fancybox]");
</script>
