<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsContent_2026lights.aspx.cs" Inherits="twbobibobi.Temples.newsContent_2026lights" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="２０２６丙午馬年新春線上點燈報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/newsContent_2026lights.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,馬年點燈,115年點燈,2026點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,馬年點燈,115年點燈,2026點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:site_name" content="２０２６丙午馬年新春點燈線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/SiteFile/News/20251030_NewsImg.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/SiteFile/News/20251030_NewsImg.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/SiteFile/News/20251030_NewsImg.jpg" />
    
    <!--預覽影片-->
    <meta property="og:video" content="https://www.youtube.com/embed/8tdGf8JjhgY" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="新港奉天宮｜2026 開運錢母擺件發售中" />
    <meta name="twitter:description" content="開臺媽祖X金虎爺聯名！2026最強開運錢母擺件限量開賣，附介紹影片說明與購買連結。" />
    <meta name="twitter:image" content="https://bobibobi.tw/Temples/SiteFile/News/20251030_NewsImg.jpg" />

    <title>２０２６丙午馬年新春點燈線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台</title>
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

        .tablecontent {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            margin-top: 16px;
            font-family: "Noto Sans TC", sans-serif;
            background-color: #fff;
            box-shadow: 0 0 0 1px #d1d1d1;
        }

        .tablecontent th,
        .tablecontent td {
            border: 1px solid #d0d0d0;
            padding: 12px 16px;
            text-align: center;
            vertical-align: middle;
            font-size: 16px;
            background-color: #fefefe;
            color: #333;
            box-shadow: inset 1px 1px 0 #ffffff, inset -1px -1px 0 #cccccc;
        }

        .tablecontent th {
            background-color: #e8edf5;
            font-weight: bold;
            box-shadow: inset 1px 1px 0 #ffffff, inset -1px -1px 0 #b0b0b0;
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
            .tablecontent {
                border: none;
            }

            .tablecontent thead {
                display: none;
            }

            .tablecontent tr {
                display: block;
                margin: 16px auto;
                width: 95%;
                border: 1px solid #ccc;
                border-radius: 8px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.08);
                padding: 10px;
                background-color: #fff;
                transition: box-shadow 0.3s ease;
            }

            .tablecontent tr:hover {
                box-shadow: 0 6px 12px rgba(0, 0, 0, 0.12);
            }

            .tablecontent td {
                display: flex;
                justify-content: space-around;
                padding: 10px 12px;
                border: none;
                border-bottom: 1px solid #eee;
                font-size: 16px;
                background-color: transparent;
            }

            .tablecontent td:last-child {
                border-bottom: none;
            }

            .tablecontent td::before {
                content: attr(data-label);
                font-weight: bold;
                color: #800000;
                flex-shrink: 0;
                margin-right: 12px;
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
      "description": "2026最強開運錢母擺件\n新港奉天宮開臺媽祖X金虎爺\n讓您新的一年財運大爆發\nhttps://st.bobibobi.tw/6sh4w6",
      "thumbnailUrl": "https://img.youtube.com/vi/8tdGf8JjhgY/maxresdefault.jpg",
      "uploadDate": "2026-01-03T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/8tdGf8JjhgY",
      "contentUrl": "https://bobibobi.tw/Temples/newsContent_2026lights.aspx"
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
                    <li>２０２６丙午馬年新春線上點燈報名開始囉~</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="NewsImg">
                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20251030_NewsImg.jpg?t=98089899" width="1160" height="550" alt="保必保庇２０２６丙午馬年新春線上點燈" title="保必保庇２０２６丙午馬年新春線上點燈" /></div>
                <div class="NewsBk">
                    <h1>歡迎使用2026丙午馬年新春點燈~所有線上點燈皆會於廟裡實際安奉</h1>
                    <%--<div class="NewsDate">發布日期：2023-10-12</div>--%>
                    <div class="NewsContent Content">
                        <!--內容放這裡 //Start-->
                        <br />
                        <p>『保必保庇』與全台知名宮廟配合線上點燈服務，提供信眾更多元化的選擇，讓您在忙碌之餘也可以輕鬆完成點燈保平安，為自己與家人祈福。</p>
                        <br />
                        <h2>2026丙午馬年犯太歲生肖如下：</h2>
                        <img src="https://bobibobi.tw/Temples/images/temple/lights_anti_2026.jpg" width="1160" height="550" alt="保必保庇提供太歲生肖 馬 鼠 牛 兔" 
                            title="保必保庇提供太歲生肖 馬 鼠 牛 兔" />
                        <br />
                        <img src="https://bobibobi.tw/Temples/SiteFile/News/TempleInfo.jpg?t=98089899" width="1160" height="550" alt="保必保庇合作宮廟" title="保必保庇合作宮廟" />
                        <br />
                        <h2>點燈早鳥優惠中~滿2000即贈7-11禮卷</h2>
                        <img src="https://bobibobi.tw/Temples/SiteFile/News/20251103_NewsImg.jpg?t=98089897" width="1160" height="550" alt="保必保庇早鳥優惠" title="保必保庇早鳥優惠" />
                        <br />
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <h2>選擇宮廟</h2>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><%=Status_Lights_da_2026 %></li>
                                        <li><%=Status_Lights_h_2026 %></li>
                                        <li><%=Status_Lights_wu_2026 %></li>
                                        <li><%=Status_Lights_Fu_2026 %></li>
                                        <li><%=Status_Lights_Luer_2026 %></li>
                                        <li><%=Status_Lights_ty_2026 %></li>
                                        <li><%=Status_Lights_Fw_2026 %></li>
                                        <li><%=Status_Lights_dh_2026 %></li>
                                        <li><%=Status_Lights_Lk_2026 %></li>
                                        <li><%=Status_Lights_ma_2026 %></li>
                                        <li><%=Status_Lights_wjsan_2026 %></li>
                                        <li><%=Status_Lights_ld_2026 %></li>
                                        <li><%=Status_Lights_st_2026 %></li>
                                        <li><%=Status_Lights_bj_2026 %></li>
                                        <li><%=Status_Lights_sbbt_2026 %></li>
                                        <li><%=Status_Lights_bpy_2026 %></li>
                                        <li><%=Status_Lights_ssy_2026 %></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                        <br />
                        <h2>限時限量開運商品販售中</h2>
                        <h2>1. 鎮宅、開運錢母擺件</h2>
                            <p style="color:darkslategrey">擺放家中財位可鎮宅、招財、避邪、 保平安。</p>
                        <%--<h2>2. 開運隨身御守(虎爺香火袋)</h2>
                            <p style="color:darkslategrey">隨身攜帶助開運、招財、 保平安。</p>
                        <h2>黃金符令手鍊</h2>
                            <p style="color:darkslategrey">傳統符令時尚穿戴，平安跟著走。</p>--%>
                        
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

        // 取得目前網址中的 purl
        var urlParams = new URLSearchParams(window.location.search);
        var purl = urlParams.get("purl");

        if (!purl) return; // 沒有 purl 就直接離開

        // 對 .ServiceTempleList 中的所有 <a> 加上 purl
        $(".ServiceTempleList a").each(function () {
            var $link = $(this); // ← 先抓住 this，後面 forEach 就可以用
            var href = $link.attr("href");

            if (!href) return;

            if (purl) {
                // 判斷 href 是否已有 query string
                if (href.indexOf("?") > -1) {
                    href += "&purl=" + encodeURIComponent(purl);
                } else {
                    href += "?purl=" + encodeURIComponent(purl);
                }
            }

            $link.attr("href", href);
        });
    })
</script>
