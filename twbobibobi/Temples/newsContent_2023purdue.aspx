<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2023purdue.aspx.cs" Inherits="Temple.Temples.newsContent_2023purdue" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２３中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2023purdue.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２３中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２３中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台</title>
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
                    <li>２０２３中元普渡線上報名開始囉~</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20230709_NewsImg.png" width="1160" height="550" alt="" /></div>
                <div class="NewsBk">
                    <h1>２０２３中元普渡線上報名開始囉~</h1>
                    <div class="NewsDate">發布日期：2023-10-12</div>
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <p>一年一度的農曆七月十五日中元普渡，幫助好兄弟，渡化眾生，值福、做好事、得功德。</p>
                        <h2>贊普施食供養</h2>
                        <p>使鬼道眾生來受法食，仗神佛的慈悲願力，讓一切餓鬼皆能得渡，成就無上功德。</p>
                        <h2>好兄弟聞經受渡</h2>
                        <p>在家裡普只做到普，而無法做到渡，在大廟裡參與普渡能讓好兄弟聞經受渡。</p>
                        <h2>線上報名好方便</h2>
                        <p>與臺灣知名宮廟合作，接受線上報名，讓您在忙碌之餘也可以輕鬆完成中元普渡。</p>
                        <h2>善的循環</h2>
                        <p>選擇捐出普渡後的普品，幫助社會弱勢，做到真正的冥陽兩利。</p>
                        <br />
                        <ul class="ServiceList">

                            <!--細項服務項目-->
                            <li>
                                <h2>選擇宮廟</h2>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="templeService_purdue_da.aspx" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                        <li><a href="templeService_purdue_wu.aspx" title="北港武德宮">北港武德宮</a></li>
                                        <li><a href="templeService_purdue_Fu.aspx" title="西螺福興宮">西螺福興宮</a></li>
                                        <li><a href="templeService_purdue_Jing.aspx" title="桃園大廟景福宮">桃園大廟景福宮</a></li>
                                        <li><a href="templeService_purdue_Luer.aspx" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                        <%--<p>
                            <a href="templeService_purdue.aspx?a=3">大甲鎮瀾宮</a>、
                            <a href="templeService_purdue.aspx?a=4">新港奉天宮</a>、
                            <a href="templeService_purdue.aspx?a=6">北港武德宮</a>、
                            <a href="templeService_purdue.aspx?a=8">西螺福興宮</a>、
                            <a href="templeService_purdue.aspx?a=9">桃園大廟景福宮</a>、
                            <a href="templeService_purdue.aspx?a=10">台南正統鹿耳門聖母廟</a>。</p>--%>
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

