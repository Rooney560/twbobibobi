<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Video_contest.aspx.cs" Inherits="twbobibobi.Temples.Video_contest" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="保必保庇全民影展。拍出你的信仰日常，就有機會獲得獎金3000元！｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/Video_contest.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇｜信仰，不只存在廟裡，也在每個人生活裡。這次，我們邀請全台信眾、創作者，一起用鏡頭拍下屬於你的「宮廟故事」。不論是神明靈驗感應、遶境盛事、點燈祈福、廟口日常，只要你願意拍、敢分享，就有機會登上保必保庇官方平台，還能抱走獎金3000元！" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇｜信仰，不只存在廟裡，也在每個人生活裡。這次，我們邀請全台信眾、創作者，一起用鏡頭拍下屬於你的「宮廟故事」。不論是神明靈驗感應、遶境盛事、點燈祈福、廟口日常，只要你願意拍、敢分享，就有機會登上保必保庇官方平台，還能抱走獎金3000元！" />
    <!--簡介-->
    <meta property="og:site_name" content="保必保庇全民影展。拍出你的信仰日常，就有機會獲得獎金3000元！｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_001.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_001.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/shortvideo/video_contest_001.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>保必保庇全民影展。拍出你的信仰日常，就有機會獲得獎金3000元！｜保必保庇</title>
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
        .EventServiceContent ol {
            list-style: auto;
            padding: revert;
        }
        .title {
            font-weight: bold;
            color: black;
            font-size: 1.2vw;
        }
        .title2 {
            font-weight: bold;
            color: black;
        }
        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }
            .title {
                font-size: 4vw;
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
                    <li>保必保庇全民影展</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_001.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_002.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_003.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_004.jpg?t=123" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_005.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_006.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_007.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_008.jpg" /><br />
                        <img src="https://bobibobi.tw/Temples/images/temple/shortvideo/video_contest_009.jpg" />
                    </div>
                </div>
                <br />
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
