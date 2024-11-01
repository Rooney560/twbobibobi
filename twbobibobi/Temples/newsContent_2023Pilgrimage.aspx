<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2023Pilgrimage.aspx.cs" Inherits="Temple.Temples.newsContent_2023Pilgrimage" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２３媽祖繞境活動|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2023Pilgrimage.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２３媽祖繞境活動|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２３媽祖繞境活動|最新消息|【保必保庇】線上宮廟服務平台</title>
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
                    <li>２０２３媽祖繞境活動線上報名開始囉~</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20230318_PilgrimageImg.png" width="1160" height="550" alt="" /></div>
                <div class="NewsBk">
                    <h1>２０２３媽祖繞境活動線上報名開始囉~</h1>
                    <div class="NewsDate">發布日期：2023-03-18</div>
                    <div class="NewsContent Content">

                        <!--內容放這裡 //Start-->
                        <h2>誠心繞境祈平安  錢母加持助開運</h2>
                        <p>2023大甲媽祖遶境即將開跑~</p>
                        <p>讓我們一起感謝媽祖，用感恩的誠心參加一年一度的媽祖繞境，錢母加持讓您好運旺旺來！虎爺香火袋把平安帶著走！</p>

                        <h2>鎮宅、鎮宅錢母擺件</h2>
                        <p>奉天宮首次推出的產品，擺放家中財位可鎮宅、招財、避邪、 保平安，可於側邊雕刻信士姓名，數量有限、 售完為止。</p>

                        <h2>開運隨身御守</h2>
                        <p>奉天宮的咬錢虎爺香火袋，是御守也是香火袋(內含古銅色復古錢母)，隨身攜帶助開運、招財、 保平安，可當鑰匙圈或掛於包包。</p>

                        <h2>六家宮廟祈福過香火</h2>
                        <p>商品會在駐駕宮廟祈福過香火，並實況錄影、拍照，於繞境結束後陸續將商品寄出。</p>
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