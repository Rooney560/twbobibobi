<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2025lights.aspx.cs" Inherits="twbobibobi.Temples.newsContent_2025lights" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２５乙巳蛇年新春線上點燈報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2025lights.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２５乙巳蛇年新春點燈線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/SiteFile/News/20241029_NewsImg_s.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/SiteFile/News/20241029_NewsImg_s.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/SiteFile/News/20241029_NewsImg_s.jpg" />
    
    <!--預覽影片-->
    <meta property="og:video" content="https://www.youtube.com/embed/8tdGf8JjhgY" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="新港奉天宮｜2025 開運錢母擺件發售中" />
    <meta name="twitter:description" content="開臺媽祖X金虎爺聯名！2025最強開運錢母擺件限量開賣，附介紹影片說明與購買連結。" />
    <meta name="twitter:image" content="https://bobibobi.tw/Temples/SiteFile/News/20241029_NewsImg_s.jpg" />

    <title>２０２５乙巳蛇年新春點燈線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台</title>
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
    <script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "VideoObject",
      "name": "新港奉天宮開運錢母擺件",
      "description": "2025最強開運錢母擺件\n新港奉天宮開臺媽祖X金虎爺\n讓您新的一年財運大爆發\nhttps://st.bobibobi.tw/6sh4w6",
      "thumbnailUrl": "https://img.youtube.com/vi/8tdGf8JjhgY/maxresdefault.jpg",
      "uploadDate": "2025-01-03T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/8tdGf8JjhgY",
      "contentUrl": "https://bobibobi.tw/Temples/newsContent_2025lights.aspx"
    }
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
                    <li>２０２５乙巳蛇年新春線上點燈報名開始囉~</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="SiteFile/News/20241029_NewsImg.jpg?t=98089899" width="1160" height="550" alt="保必保庇２０２５乙巳蛇年新春線上點燈" title="保必保庇２０２５乙巳蛇年新春線上點燈" /></div>
                <div class="NewsBk">
                    <h1>歡迎使用2025乙巳蛇年新春點燈~所有線上點燈皆會於廟裡實際安奉</h1>
                    <%--<div class="NewsDate">發布日期：2023-10-12</div>--%>
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <p>『保必保庇』今年與11家全台知名宮廟配合線上點燈服務，提供信眾更多元化的選擇，讓您在忙碌之餘也可以輕鬆完成點燈保平安，為自己與家人祈福。</p>
                        <h2 style="color:peru; padding-bottom:5px;">2025乙巳蛇年犯太歲生肖如下：</h2>
                        <h2 style="color:peru; padding-bottom:5px;">蛇 | 值太歲：運程阻礙、人際孤立，易有病痛、不利遠行、防爛桃花。</h2>
                        <h2 style="color:peru; padding-bottom:5px;">豬 | 沖太歲：情緒抑鬱，工作吃力、少貴人、易破財，需主動求變。</h2>
                        <h2 style="color:peru; padding-bottom:5px;">虎 | 刑、害太歲：易遭小人暗算，努力付諸東流，易過度花費、精神恍惚。</h2>
                        <h2 style="color:peru; padding-bottom:5px;">猴 | 刑、破太歲：是非較多，易有官非破財、疾病纏身、感情破裂。</h2>
                        <p>可選擇宮廟與燈種如下，點擊宮廟名稱即可前往報名，數量有限，敬請把握！</p>
                        <h2>大甲鎮瀾宮</h2>
                        <p style="color:darkslategrey">光明燈、安太歲、文昌燈</p>
                        <h2>台南正統鹿耳門聖母廟</h2>
                        <p style="color:darkslategrey">光明燈、安太歲、文昌燈、財利燈、福壽燈、姻緣燈</p>
                        <h2>新港奉天宮</h2>
                        <p style="color:darkslategrey">光明燈、安太歲</p>
                        <h2>北港武德宮</h2>
                        <p style="color:darkslategrey">財神燈、光明燈、安太歲</p>
                        <h2>西螺福興宮</h2>
                        <p style="color:darkslategrey">光明燈、安太歲、財神燈、藥師佛燈、文昌燈、觀音佛祖燈</p>
                        <h2>桃園威天宮</h2>
                        <p style="color:darkslategrey">光明燈、安太歲、財神燈、藥師燈、貴人燈</p>
                        <h2>斗六五路財神宮</h2>
                        <p style="color:darkslategrey">財庫燈、發財燈、月老桃花燈、貴人燈(光明燈)、安太歲、寵物平安燈</p>
                        <h2>台東東海龍門天聖宮</h2>
                        <p style="color:darkslategrey">光明燈、安太歲、文昌燈、財利燈、龍王燈、虎爺燈</p>
                        <h2>鹿港城隍廟</h2>
                        <p style="color:darkslategrey">轉運納福燈、光明燈上層、文魁智慧燈、元神光明燈、太歲平安符、正財福報燈、偏財旺旺燈、廣進安財庫</p>
                        <h2>玉敕大樹朝天宮</h2>
                        <p style="color:darkslategrey">安太歲、光明燈、五文昌燈、福財燈</p>
                        <h2>台灣道教總廟無極三清總道院</h2>
                        <p style="color:darkslategrey">光明燈、太歲燈、事業燈、財神燈、文昌燈、藥師燈、全家光明燈<br />
                                                       財神斗、事業斗、平安斗、文昌斗、藥師斗、元神斗、福祿壽斗、觀音斗</p>
                        <h2>松柏嶺受天宮</h2>
                        <p style="color:darkslategrey">安奉太歲、光明燈、文昌燈、財神燈</p>

                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <h2>選擇宮廟</h2>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><%=Status_Lights_da_2025 %></li>
                                        <li><%=Status_Lights_Luer_2025 %></li>
                                        <li><%=Status_Lights_h_2025 %></li>
                                        <li><%=Status_Lights_wu_2025 %></li>
                                        <li><%=Status_Lights_Fu_2025 %></li>
                                        <li><%=Status_Lights_ty_2025 %></li>
                                        <li><%=Status_Lights_Fw_2025 %></li>
                                        <li><%=Status_Lights_dh_2025 %></li>
                                        <li><%=Status_Lights_Lk_2025 %></li>
                                        <li><%=Status_Lights_ma_2025 %></li>
                                        <li><%=Status_Lights_wjsan_2025 %></li>
                                        <li><%=Status_Lights_st_2025 %></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                        <%--<p>
                            <a href="templeService_lights.aspx?a=3">大甲鎮瀾宮</a>、
                            <a href="templeService_lights.aspx?a=4">新港奉天宮</a>、
                            <a href="templeService_lights.aspx?a=6">北港武德宮</a>、
                            <a href="templeService_lights.aspx?a=8">西螺福興宮</a>、
                            <a href="templeService_lights.aspx?a=9">桃園大廟景福宮</a>、
                            <a href="templeService_lights.aspx?a=10">台南正統鹿耳門聖母廟</a>。</p>--%>
                        <!--內容放這裡 //End-->
                        <br />
                        <h2>限時限量開運商品販售中</h2>
                        <h2>1. 鎮宅、開運錢母擺件</h2>
                            <p style="color:darkslategrey">擺放家中財位可鎮宅、招財、避邪、 保平安。</p>
                        <%--<h2>2. 開運隨身御守(虎爺香火袋)</h2>
                            <p style="color:darkslategrey">隨身攜帶助開運、招財、 保平安。</p>
                        <h2>黃金符令手鍊</h2>
                            <p style="color:darkslategrey">傳統符令時尚穿戴，平安跟著走。</p>--%>
                        <h2>2. 招財大嘴貓(白色)、招財大嘴貓(藍色)、招財大嘴貓(粉色)、招財大嘴貓(橘色)</h2>
                            <p style="color:darkslategrey">大嘴招財貓, 能吃就是福. 笑口常開好運來。</p>
                        
                        <ul class="ServiceList">
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="https://bobibobi.tw/Product/MoneymotherIndex.aspx" target="_blank" title="商品詳細規格與資訊請點這">商品詳細規格與資訊請點這</a></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                        <br />
                        <div class="IndexVideo">
                            <h1 class="TempleName">商品介紹</h1>
                            <iframe class="ytvideo" src="https://www.youtube.com/embed/8tdGf8JjhgY?si=IIrTlhH-xeTN6MTt" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                        </div>
                        <br />

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
    })
</script>