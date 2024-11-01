<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2022lights.aspx.cs" Inherits="Temple.Temples.newsContent_2022lights" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２２新春虎年點燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2022lights.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２２新春虎年點燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２２新春虎年點燈|最新消息|【保必保庇】線上宮廟服務平台</title>
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
                    <li>２０２２新春虎年點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20211125_LightsImg.jpeg" width="1160" height="550" alt="" /></div>
                <div class="NewsBk">
                    <h1>２０２２新春虎年點燈</h1>
                    <div class="NewsDate">發布日期：2021-11-25</div>
                    <div class="NewsContent Content">

                        <!--內容放這裡 //Start-->
                        <p></p>
                        <p>2022 虎年新春點燈代辦服務開始報名~</p>
                        <p>免出門，在家就可以點好燈，祈求媽祖保佑，照亮未來的光明，趨吉避凶～</p>

                        <h2>Y22壬演年犯太歲生肖如下：</h2>
                        <p>虎｜值太歲：容易破財、運勢多阻礙、做事一波三折。</p>
                        <p>猴｜值、刑太歲：容易和親近之人吵架反目，犯小人。</p>
                        <p>蛇｜刑、害太歲：容易遭陷害、健康損害，破財可能。</p>
                        <p>豬｜破、合太歲：因小人而破財、感情上容易不和諧。</p>
                        
                        <h2>可選擇點燈宮廟如下：</h2>
                        <p>大甲鎮瀾宮｜新港奉天宮</p>

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