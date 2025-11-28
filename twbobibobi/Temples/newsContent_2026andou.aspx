<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2026andou.aspx.cs" Inherits="twbobibobi.Temples.newsContent_2026andou" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２６丙午馬年安奉斗燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2026andou.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上安斗,線上宮廟服務,線上即可安斗,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,馬年安斗,115年安斗,2026安斗,新春安斗,錢母,發財金,安奉斗燈,安奉禮斗" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上安斗,線上宮廟服務,線上即可安斗,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,馬年安斗,115年安斗,2026安斗,新春安斗,錢母,發財金,安奉斗燈,安奉禮斗" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２６丙午馬年安奉斗燈|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/SiteFile/News/20250422_NewsImg_s.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/SiteFile/News/20250422_NewsImg_s.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/SiteFile/News/20250422_NewsImg_s.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>２０２６丙午馬年安奉斗燈|最新消息|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <style type="text/css">
        .NewsBk h1 {
            font-size: 2vw;
        }

        .NewsDate {
            font-size: 1.3vw;
        }

        .Content h2 {
            font-size: 1.9vw;
        }

        .Content p, .Content b, .Content a, .Content i {
            font-size: 1.3vw;
        }

        .Content li {
            font-size: 1.3vw;
        }

        .Content span {
            font-size: 1.9vw;
            font-weight: bold;
        }

        .Content p, .Content h2 {
            margin-bottom: 1vw;
        }
        
        .ytvideo {
            max-width: 100%;
            width: 100%;
            height: 550px;
        }

        @media only screen and (max-width: 720px) {
            .NewsBk h1 {
                font-size: 6.2vw;
            }

            .NewsDate {
                font-size: 5vw;
            }

            .Content h2 {
                font-size: 5.2vw;
            }

            .Content p, .Content b, .Content a, .Content i {
                font-size: 5vw;
            }

            .Content li {
                font-size: 5vw;
            }

            .Content span {
                font-size: 6vw;
            }

            .Content p, .Content h2 {
                margin-bottom: 2vw;
            }
        }
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
        <article id="News" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li><a href="news.aspx" title="最新消息">最新消息</a></li>
                    <li>２０２６丙午馬年安奉斗燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20250422_NewsImg.jpg?t=98089899" width="1160" height="550" alt="保必保庇２０２６丙午馬年新春線上安奉斗燈" title="保必保庇２０２６丙午馬年新春線上安奉斗燈" /></div>
                <div class="NewsBk">
                    <h1>歡迎使用2026安奉斗燈服務，線上報名皆會於廟裡實際安奉斗燈</h1>
                    <%--<div class="NewsDate">發布日期：2023-10-12</div>--%>
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <br />
                        <p><span style="font-weight; color: black;">斗燈</span>是一種在道教及民間信仰中，用於供奉或禮拜星辰（尤其是北斗、南斗星君）時所點燃的特殊燈火，常見於「禮斗」「安斗」或「點斗燈」等儀式。以下為斗燈的主要意涵與用途：</p>
                        <h2>1. 象徵星辰護佑</h2>
                            <p>　•	斗燈代表著北斗、南斗或其他星辰神君的能量，被視為溝通天地、引領星辰之力的象徵。透過點燃斗燈，信眾祈求星君庇佑、護宅平安、驅散不祥。</p>
                        <h2>2. 常用於禮斗、安斗法會</h2>
                            <p>　•	在道觀或宮廟舉辦的「禮斗」「安斗」法會中，法師會焚香念誦經文、拜祭星君，並點燃斗燈或上燈。藉由燈火的明亮，象徵照亮運勢、消除業障。</p>
                        <h2>3. 驅煞與祈福</h2>
                            <p>　•	不少人相信，透過在廟宇或家中點斗燈，可除去晦氣、化解災厄，同時招來福氣與吉祥。若逢犯太歲、時運不濟，尤其適合點斗燈以尋求星君的協助。</p>
                        <p>斗燈在道教與民間星辰信仰裡扮演著「傳遞光亮、召喚星辰之力」的角色，能讓禮斗或安斗儀式更加莊嚴與有效。點燃斗燈時，配合誠心祈禱與修善積德，能期盼除厄運、添福運，為個人或家庭帶來更光明順遂的前路。</p>
                    
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <h2>報名安奉斗燈</h2>
                                <div class="ServiceTempleList">
                                    <ul>

                                        <li><%=Status_AnDou_Fw_2026 %></li>
                                        <li><%=Status_AnDou_wjsan_2026 %></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                        <uc3:SocialMedia runat="server" id="SocialMedia" />
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

        // 取得目前網址中的 purl
        var urlParams = new URLSearchParams(window.location.search);
        var purl = urlParams.get("purl");

        if (!purl) return; // 沒有 purl 就直接離開

        // 對 .ServiceTempleList 中的所有 <a> 加上 purl
        $(".ServiceTempleList a").each(function () {
            var href = $(this).attr("href");

            if (!href) return;

            // 判斷 href 是否已有 query string
            if (href.indexOf("?") > -1) {
                href += "&purl=" + encodeURIComponent(purl);
            } else {
                href += "?purl=" + encodeURIComponent(purl);
            }

            $(this).attr("href", href);
        });
    })
</script>
