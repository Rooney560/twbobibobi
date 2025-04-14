<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2025lights_ty.aspx.cs" Inherits="twbobibobi.Temples.newsContent_2025lights_ty" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２５桃園威天宮母親節孝親祈福謝恩活動" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2025lights_ty.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２５桃園威天宮母親節孝親祈福謝恩活動" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２５桃園威天宮母親節孝親祈福謝恩活動</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <style type="text/css">
        @media only screen and (max-width: 767px) {
            .ServiceTempleList li {
                display: block;
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
                    <li>２０２５桃園威天宮母親節孝親祈福謝恩活動</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20250402_NewsImg.jpg" width="1160" height="550" alt="2025桃園威天宮母親節孝親祈福謝恩活動" title="2025桃園威天宮母親節孝親祈福謝恩活動" /></div>
                <div class="NewsBk">
                    <h1>２０２５桃園威天宮母親節孝親祈福謝恩活動</h1>
                    <div class="NewsDate">發布日期：2025-04-01</div>
                    <br>
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <h2>桃園威天宮孝親謝恩母親節活動</h2>
                        <p>活動時間：2025/04/01~2024/05/09</p>
                        <p>在 帝君座前點一盞【孝親祈福燈】，為您的父母、祖父母、養育恩親祈求 關聖帝君降臨護佑，為親人添福、添平安。</p>
                        <p>並贈送點燈的大德 關公加持的【帝君祈福拖輪包】，保佑您的恩親平安、順利、發大財！</p>
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="templeService_lights_ty_mom.aspx" target="_blank" title="報名請點這裡">報名請點這裡</a></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
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
