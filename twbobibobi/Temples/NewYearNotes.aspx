<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewYearNotes.aspx.cs" Inherits="twbobibobi.Temples.NewYearNotes" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="過年期間，拜拜與出遊要特別留意以下幾點｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/NewYearNotes.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜首先，拜拜時穿著得體，避免過於隨意或顏色太過鮮豔。進廟要保持肅靜，先拜主神，再依序拜其他神明，香燭不可熄滅也不宜亂丟。
其次，出遊前需規劃行程與確認交通、住宿，避開人潮高峰，注意車輛或公共運輸的安全。自駕者切勿疲勞駕駛，乘客則保管好個人物品，提防扒手。
最後，尊重當地風俗與環保，避免亂扔垃圾或冒犯禁忌。若前往宗教景點，應遵守參訪規範。過年期間保持和氣，少爭執，方能在拜拜與出遊中收穫更多福氣與歡樂。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜首先，拜拜時穿著得體，避免過於隨意或顏色太過鮮豔。進廟要保持肅靜，先拜主神，再依序拜其他神明，香燭不可熄滅也不宜亂丟。
其次，出遊前需規劃行程與確認交通、住宿，避開人潮高峰，注意車輛或公共運輸的安全。自駕者切勿疲勞駕駛，乘客則保管好個人物品，提防扒手。
最後，尊重當地風俗與環保，避免亂扔垃圾或冒犯禁忌。若前往宗教景點，應遵守參訪規範。過年期間保持和氣，少爭執，方能在拜拜與出遊中收穫更多福氣與歡樂。" />
    <!--簡介-->
    <meta property="og:site_name" content="過年期間，拜拜與出遊要特別留意以下幾點｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/NewYearNotes.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>過年期間，拜拜與出遊要特別留意以下幾點｜保必保庇</title>
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

        /*.EventServiceContent .second:before { content: counters(item, ".") " "; counter-increment: item }*/

        #toc-container {
            background: #f9f9f9;
            border: 1px solid #aaa;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            display: table;
            margin-bottom: 1em;
            padding: 10px 20px 10px 10px;
            position: relative;
            width: auto;
        }
        .toc-container-direction {
            direction: ltr;
        }
        .toc-title-container {
            display: table;
            width: 100%;
        }
        div#toc-container .toc-title {
            font-weight: 500;
        }

        div#toc-container .toc-title {
            font-size: 120%;
        }

        div#toc-container .toc-title {
            display: initial;
        }

        #toc-container .toc-title {
            text-align: left;
            line-height: 1.45;
            margin: 0;
            padding: 0;
        }

        .toc-counter ul {
            counter-reset: item;
        }

        #toc-container ul, #toc-container li {
            background: 0 0;
            list-style: none;
            list-style-position: initial;
            list-style-image: initial;
            list-style-type: none;
            line-height: 1.6;
            margin: 0;
        }
        
        #toc-container ul, #toc-container li {
            padding: 0;
        }

        div#toc-container ul li {
            font-weight: 500;
        }

        div#toc-container ul li {
            font-size: 95%;
        }

        #toc-container a {
            color: #444;
            box-shadow: none;
            text-decoration: none;
            text-shadow: none;
            display: inline-flex;
            align-items: stretch;
            flex-wrap: nowrap;
        }
        
        #toc-container ul {
            list-style-type: disc;
            list-style-position: inside;
        }
        
        #toc-container ol, #toc-container ul {
            padding-left: 1rem;
            margin-left: 1rem;
        }

        .toc-counter nav ul li a:before {
            content: counters(item, '.', decimal) '. ';
            display: inline-block;
            counter-increment: item;
            flex-grow: 0;
            flex-shrink: 0;
            margin-right: .2em;
            float: left;
        }

        #toc-container ol ul, #toc-container ul ul {
            list-style-type: circle;
            list-style-position: inside;
            margin-left: 15px;
        }

        #toc-container a:visited {
            color: #9f9f9f;
        }

        #toc-container a:hover {
    text-decoration: underline;
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

            $(".content_titleL").height($("header").height());
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
                    <li><a href="NewYearNotes.aspx" title="過年期間注意事項">過年期間注意事項</a></li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <div id="toc-container" class="toc-container-direction toc-counter">
                            <div class="toc-title-container">
                                <p class="toc-title" style="cursor:inherit">目錄</p>
                            </div>
                            <nav>
                                <ul>
                                    <li><a href="NewYearNotes_01.aspx">蛇年犯太歲生肖過年出遊注意事項</a></li>
                                    <li><a href="NewYearNotes_02.aspx">過年祭祖注意事項</a></li>
                                    <li><a href="NewYearNotes_03.aspx">過年送禮禁忌</a></li>
                                    <li><a href="NewYearNotes_04.aspx">過年廟裡拜拜注意什麼</a></li>
                                    <li><a href="NewYearNotes_05.aspx">除夕到初四要拜什麼</a></li>
                                    <li><a href="NewYearNotes_06.aspx">初五開工怎麼拜</a></li>
                                    <li><a href="NewYearNotes_07.aspx">過年迎財神指南</a></li>
                                </ul>
                            </nav>
                        </div>
                        <br />
                        <h1 class="TempleName">過年期間，拜拜與出遊要特別留意以下幾點｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜首先，拜拜時穿著得體，避免過於隨意或顏色太過鮮豔。進廟要保持肅靜，先拜主神，再依序拜其他神明，香燭不可熄滅也不宜亂丟。
其次，出遊前需規劃行程與確認交通、住宿，避開人潮高峰，注意車輛或公共運輸的安全。自駕者切勿疲勞駕駛，乘客則保管好個人物品，提防扒手。
最後，尊重當地風俗與環保，避免亂扔垃圾或冒犯禁忌。若前往宗教景點，應遵守參訪規範。過年期間保持和氣，少爭執，方能在拜拜與出遊中收穫更多福氣與歡樂。
                            </p>
                        <br />
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
    })
</script>
