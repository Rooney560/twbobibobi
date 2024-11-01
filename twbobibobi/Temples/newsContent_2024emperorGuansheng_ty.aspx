<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2024emperorGuansheng_ty.aspx.cs" Inherits="Temple.Temples.newsContent_2024emperorGuansheng_ty" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２４桃園威天宮關聖帝君聖誕線上報名開始~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2024emperorGuansheng_ty.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２４桃園威天宮關聖帝君聖誕線上報名開始~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２４桃園威天宮關聖帝君聖誕千秋活動報名開始 ~|最新消息|【保必保庇】線上宮廟服務平台</title>
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
                    <li>２０２４桃園威天宮關聖帝君聖誕千秋活動報名開始 ~</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20240618_NewsImg.png" width="1160" height="550" alt="" /></div>
                <div class="NewsBk">
                    <h1>２０２４桃園威天宮關聖帝君聖誕千秋活動報名開始 ~</h1>
                    <div class="NewsDate">發布日期：2024-06-18</div>
                    <br />
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <h2>關聖帝君聖誕千秋活動</h2>
                        <p>活動時間：2024/06/18~2024/07/26</p>
                        <p>桃園威天宮十周年【關聖帝君聖誕千秋 祝壽謝恩祈福儀式】</p>
                        <p>好消息！好消息！關聖帝君是青龍轉世，更是武財神，今年是十二年才一次的龍年，也是威天宮建廟10 周年紀念，機會非常難得！
                            今年的關公聖誕期間是您向 帝君謝恩、祝壽、招財、求平安最好的日子。</p>
                        <p>威天宮特別舉辦【關聖帝君聖誕千秋 祝壽謝恩祈福儀式】，敬邀大德報名設立【祝壽謝恩招財祿位】，威天宮也精心準備了獨創紀念品來回饋大德。</p>
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li>詳細詳細報名方式請點><a href="templeService_EmperorGuansheng_bobi_ty.aspx" target="_blank" title="關聖帝君聖誕千秋活動報名網頁">關聖帝君聖誕千秋活動報名網頁</a></li>
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
