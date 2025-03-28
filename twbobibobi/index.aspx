<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="twbobibobi.index" %>

<%@ Register Src="~/Temples/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Temples/header.ascx" TagPrefix="uc2" TagName="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <%
// 输出meta标签
//Response.Write("\r\n<meta property=\"og:title\" content=\"" + SEO_Title + "\">");
//Response.Write("\r\n<meta name=\"description\" content=\"" + SEO_Description + "\">");
//Response.Write("\r\n<meta property=\"og:description\" content=\"" + SEO_Description + "\">");
        foreach(string item in seo_Tags)
            Response.Write("\r\n" + item);

    %>

    <%--<meta property="og:title" content="【保必保庇】線上宮廟服務平台" />--%>
    <!--標題-->
    <%--<meta property="og:url" content="./Temples/index.aspx" />--%>
    <!--網址：請補上網址-->
    <%--<meta name="description" content="線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,補財庫,普渡,點燈服務等,忙碌之餘也能進行跨縣市宮廟點燈" />--%>
    <!--簡介-->
    <%--<meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />--%>
    <!--簡介-->
    <%--<meta property="og:site_name" content="【保必保庇】線上宮廟服務平台" />--%>
    <!--標題-->
    <%--<meta property="og:type" content="website" />--%>

    <!--抓取圖片-->
    <%--<meta property="og:image" content="./Temples/images/fb.jpg" />--%>
    <%--<meta name="twitter:image:src" content="./Temples/images/fb.jpg" />--%>
    <link rel="image_src" href="./Temples/images/fb.jpg" />


    <link rel="shortcut icon" href="./Temples/images/favicon.png" />
    <link href="./Temples/images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title><%=SEO_Title %></title>
    <!--標題-->


    <link rel="stylesheet" type="text/css" href="./Temples/slick/slick.css" />
    <link rel="stylesheet" type="text/css" href="./Temples/slick/slick-theme.css" />

    <script src="./Temples/js/jquery-3.7.1.min.js"></script>
    <link href="./Temples/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./Temples/Bootstrap/js/bootstrap.bundle.min.js"></script>
    <!--資源項目-->
    <link href="./Temples/css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="./Temples/css/style_index.css" />

    <%-- <style>
        .SiteName {
    position: relative;
    display: block;
    margin: 0 auto;
    background: url(./images/logo_big.png);
    width: 100%;
    text-align: center;
    padding-top: 42%;
    background-size: cover;
}
    </style>--%>
    <style type="text/css">
        .fs-3 {
            color: #242424;
            font-weight: bold;
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
    <%--<div id="wrap">--%>
        <!--#warp //start-->

        <!--頁首選單-->
        <uc2:header runat="server" id="header" />
        <!-----本頁內容開始----->
        <article id="Index">
            <!--Section 1:主視覺-->
            <section id="KV">
                <div class="KVBgItem">
                    <div class="KVTreeR">
                        <img src="./Temples/images/kv_bg_r.png" width="183" height="136" alt="" />
                    </div>
                    <div class="KVTreeL">
                        <img src="./Temples/images/kv_bg_l.png" width="323" height="160" alt="" />
                    </div>
                </div>
                <div class="KVBk">
                    <div class="KVMenu">
                        <div class="MenuBk">
                            <ul class="MenuList">
                                <li>
                                    <a href="./Temples/temple.aspx" title="合作宮廟">
                                        <div>
                                            <img src="./Temples/images/menu_01.png" width="75" height="75" alt="" />
                                        </div>
                                        <div>合作宮廟</div>
                                    </a>
                                </li>
                                <li>
                                    <a href="./Temples/service.aspx" title="信眾服務">
                                        <div>
                                            <img src="./Temples/images/menu_02.png" width="75" height="75" alt="" />
                                        </div>
                                        <div>信眾服務</div>
                                    </a>
                                </li>
                                <li>
                                    <a href="./Temples/news.aspx" title="最新消息">
                                        <div>
                                            <img src="./Temples/images/menu_03.png" width="75" height="75" alt="" />
                                        </div>
                                        <div>最新消息</div>
                                    </a>
                                </li>
                                <li>
                                    <a href="./SearchLog.aspx" target="_blank" title="訂單查詢">
                                        <div>
                                            <img src="./Temples/images/menu_04.png" width="75" height="75" alt="" />
                                        </div>
                                        <div>訂單查詢</div>
                                    </a>
                                </li>
                                <li id="shop2">
                                    <a href="https://shop.bobibobi.tw/" target="_blank" title="開運商品">
                                        <div>
                                            <img src="https://bobibobi.tw/Temples/images/menu_05.png" width="75" height="75" alt="" />
                                        </div>
                                        <div>開運商品</div>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="KVLogo">
                        <div class="KVLine top"></div>
                        <div class="KVContent">
                            <div class="SiteName">
                                <h1 class="SiteNameTxt"><%=FindSysSetting("home_site_name") %></h1>
                            </div>
                            <div class="KVText">
                                <p>
                                    <%--線上宮廟服務平台--%><h1 class="SiteNameTxt2"><%=FindSysSetting("home_title") %></h1>
                                    <br />
                                    <%=FindSysSetting("home_subtitle") %>
                                </p>
                            </div>
                        </div>
                        <div class="KVLine btm"></div>
                    </div>
                </div>
            </section>

            <!-- 轮播图片 -->
            <%--<section>--%>
            <div id="carouselExampleCaptions" class="carousel slide carousel-fade" data-bs-ride="carousel" data-bs-pause="false">
                <div class="carousel-indicators">
<%=carousel_indicators %>
                    <!--
                    <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="0"
                        class="active"
                        aria-current="true"
                        aria-label="Slide 1">
                    </button>
                    <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="1"
                        aria-label="Slide 2">
                    </button>
                    <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="2"
                        aria-label="Slide 3">
                    </button>
                    <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="3"
                        aria-label="Slide 4">
                    </button>
                    -->
                </div>
                <div class="carousel-inner">
<%=carousel_inners %>
                    <!--
                    <div class="carousel-item active">
                        <img src="./Temples/images/carousel1t.jpg" class="d-none d-sm-block w-100" alt="..." />
                        <img src="./Temples/images/carousel1m.jpg" class="d-block d-sm-none w-100" alt="..." />
                        <div class="carousel-caption">
                            <h1>世代信仰-數位傳承</h1>
                            <h5>提供祈福點燈、線上普渡、補財庫等各種信眾服務</h5>
                            <a href="https://bobibobi.tw/Temples/temple.aspx">
                                <div class="btn btn-warning carousel-button">合作宮廟</div>
                            </a>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="./Temples/images/carousel2t.jpg" class="d-none d-sm-block w-100" alt="..." />
                        <img src="./Temples/images/carousel2m.jpg" class="d-block d-sm-none w-100" alt="..." />
                        <div class="carousel-caption">
                            <h1>世代信仰-數位傳承</h1>
                            <h5>提供線上各大宮廟祈福點燈，免出門、免排隊，在家輕鬆得神助</h5>
                            <a href="https://bobibobi.tw/Temples/service.aspx">
                                <div class="btn btn-warning carousel-button">立即點燈</div>
                            </a>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="./Temples/images/carousel3t.jpg" class="d-none d-sm-block w-100" alt="..." />
                        <img src="./Temples/images/carousel3m.jpg" class="d-block d-sm-none w-100" alt="..." />
                        <div class="carousel-caption">
                            <h1>世代信仰-數位傳承</h1>
                            <h5>Some representative placeholder content for the third slide.</h5>
                            <a href="https://bobibobi.tw/Temples/service.aspx">
                                <div class="btn btn-warning carousel-button">中元普渡</div>
                            </a>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="./Temples/images/carousel4t.jpg" class="d-none d-sm-block w-100" alt="..." />
                        <img src="./Temples/images/carousel4m.jpg" class="d-block d-sm-none w-100" alt="..." />
                        <div class="carousel-caption">
                            <h1>世代信仰-數位傳承</h1>
                            <h5>Some representative placeholder content for the third slide.</h5>
                            <a href="https://bobibobi.tw/Temples/service.aspx">
                                <div class="btn btn-warning carousel-button">補財庫</div>
                            </a>
                        </div>
                    </div>
                    -->
                </div>
                <button
                    class="carousel-control-prev"
                    type="button"
                    data-bs-target="#carouselExampleCaptions"
                    data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button
                    class="carousel-control-next"
                    type="button"
                    data-bs-target="#carouselExampleCaptions"
                    data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
            <%--</section>--%>





            <!--Section 5:最新消息-->
            <%--<sec<%-- id="IndexNews">--%>
            <div class="IndexNews">
                <div class="IndexNewsList">
                    <%--  <ul>
                        <li>
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights_pet.aspx">
                                <div class="Newsimg">
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240520_NewsImg.jpg" width="640" height="400" alt="" />
                                </div>
                                <div class="NewsTtile">２０２４毛小孩專屬平安燈線上報名開始~</div>
                                <div class="NewsInfo">🐱🐶🐭🐹🐰🐦🐴🐡🐍🦎🦂軟萌限定~毛起來愛牠！🏮毛小孩專屬平安燈🏮</div>
                                <div class="ReadMoreBtn"><span>閱讀更多</span></div>
                            </a>
                        </li>
                        <li>
                            <a href="https://bobibobi.tw/Temples/newsContent_2024supplies_ty.aspx">
                                <div class="Newsimg">
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240518_NewsImg.png" width="640" height="400" alt="" />
                                </div>
                                <div class="NewsTtile">２０２４桃園威天宮天赦日補運活動線上報名開始~</div>
                                <div class="NewsInfo">迎芒種接端午 5/30開運天赦日</div>
                                <div class="ReadMoreBtn"><span>閱讀更多</span></div>
                            </a>
                        </li>
                        <li>
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights_ty.aspx">
                                <div class="Newsimg">
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240426_NewsImg.jpg" width="640" height="400" alt="" />
                                </div>
                                <div class="NewsTtile">２０２４桃園威天宮孝恩祈福活動線上報名開始~</div>
                                <div class="NewsInfo">在 帝君座前點一盞【孝親祈福燈】，為您的父母、祖父母、養育恩親祈求 關聖帝君降臨護佑，為親人添福、添平安。</div>
                                <div class="ReadMoreBtn"><span>閱讀更多</span></div>
                            </a>
                        </li>
                        <!--↓↓範例↓↓-->
                        <!--↑↑範例↑↑-->
                    </ul>--%>


                    <div class="container px-4 ">
                        <div class="row justify-content-center py-5">
                            <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                            <div class="CategoryTitle">-&nbsp;當前活動&nbsp;-</div>
                            <hr class="Category" />
                        </div>
                        <div class="row gx-5">
                            <div class="col-lg-4 col-sm-12 py-3">
                                <a href="https://bobibobi.tw/Temples/newsContent_2025lights.aspx">
                                    <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                        <img src="https://bobibobi.tw/Temples/SiteFile/News/20241029_NewsImg_s.jpg" class="card-img-top p-3" alt="保必保庇２０２５乙巳蛇年新春線上點燈" title="保必保庇２０２５乙巳蛇年新春線上點燈" />
                                    <%--</div>--%>
                                        <div class="card-body">
                                    <div class="fs-3">２０２５乙巳蛇年新春線上點燈報名開始囉~</div>
                                    <div class="fs-5">『保必保庇』今年與11家全台知名宮廟配合線上點燈服務，提供信眾更多元化的選擇，讓您在忙碌之餘也可以輕鬆完成點燈保平安，為自己與家人祈福。</div>
                               
                                        </div>
                                        <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                            <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="col-lg-4 col-sm-12 py-3">
                                <a href="https://bobibobi.tw/Temples/templeService_supplies_Fw.aspx">
                                    <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                        <%--<div class="Newsimg">--%>
                                        <img src="https://bobibobi.tw/Temples/SiteFile/News/20250306_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="斗六五路財神宮補財庫" title="斗六五路財神宮補財庫" />
                                        <%--</div>--%>
                                        <div class="card-body">
                                            <div class="fs-3">斗六五路財神宮２０２５補財庫</div>
                                            <div class="fs-5">全台少數24小時營業的財神廟開放線上報名補財庫了！使用催財符打開你的財庫，並且先赦罪後再補財庫！改善正財、偏財運勢，助事業順遂，累積福報，讓財富源源不絕。</div>

                                        </div>
                                        <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                            <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="col-lg-4 col-sm-12 py-3">
                                <a href="https://bobibobi.tw/Temples/templeService_supplies_Lk.aspx">
                                    <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                        <%--<div class="Newsimg">--%>
                                        <img src="https://bobibobi.tw/Temples/SiteFile/News/20241228_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="鹿港城隍廟２０２５補財庫" title="鹿港城隍廟２０２５補財庫" />
                                        <%--</div>--%>
                                        <div class="card-body">
                                            <div class="fs-3">鹿港城隍廟２０２５補財庫</div>
                                            <div class="fs-5">✨ 鹿港城隍廟，幫你補足財富能量，讓好運不斷、財源滾滾！ ✨🌟 每個人的「財庫」如同存錢罐，偶爾也需要補充和修繕。當你覺得財運卡關、收入停滯···</div>

                                        </div>
                                        <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                            <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <%--</section>--%>

            <hr />

            <!--Section: 燈種介紹-->
            <div class="IndexLightList">
                <div class="row justify-content-center pt-5 pb-4">
                    <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                    <div class="CategoryTitle">-&nbsp;資訊專區&nbsp;-</div>
                    <hr class="Category" />
                </div>
                <div class="row justify-content-center LightList-Content">
                    <div class="LightList">
                        <%=lightList %>
                        <!--
                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_gm.png" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">光明燈</div>
                                        <div class="fs-5">祈求元神光彩、補運助氣、增福益壽，適用欲求平安順遂、前途光明之民眾。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_yy.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">姻緣燈/桃花燈</div>
                                        <div class="fs-5">祈求天賜佳緣、感情順利、婚姻美滿，適用欲求姻緣順利之單身、已婚男女。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_cs.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">安拜斗/財神燈</div>
                                        <div class="fs-5">祈求延命保安、趨吉避凶、投資倍利，適用欲消災解厄之民眾、創業投資者。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_fs.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">藥師佛燈/福壽燈</div>
                                        <div class="fs-5">點藥師燈能護佑身體健康、疾病消除、、延年益壽，也適合保佑家庭人丁興旺，懷孕安胎、生產順利。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_cw.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">寵物平安燈</div>
                                        <div class="fs-5">「寵物平安燈」為毛小孩添福、添運、消災、祈福。為寵物燃點平安燈，祈求眾神護佑平安健康。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_wc.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">文昌燈</div>
                                        <div class="fs-5">祈求工作升遷、金榜題名、學業精進，適用莘莘學子、領公司薪水之上班族。</div>
                                    </div>
                                    <div class="card-footer pt-0 pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="LightList-Item">
                            <a href="./Temples/lightInfo.aspx?a=3">
                                <div class="card shadow h-100 LightCard">
                                    <img src="./Temples/SiteFile/light/light_ts.jpg" style="height: 150px;" class="card-img-to" alt="" />
                                    <div class="card-body pb-0">
                                        <div class="fs-3">安太歲</div>
                                        <div class="fs-5">祈求行年平安、諸事順利、四時無災，適用肖龍、狗、牛、羊之犯太歲民眾。</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        -->

                    </div>
                </div>
                <br />
            </div>


            <!--Section: 法會介紹-->
<%=ceremonyList %>
            <!--
            <div class="IndexCeremony">
                <div class="row justify-content-center py-5">
                    <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                    <div class="CategoryTitle">-&nbsp;法會介紹&nbsp;-</div>
                    <hr class="Category" />
                </div>
                <div class="row justify-content-center">
                    <div class="accordion CeremonyList" id="accordionExample">
                        <div class="accordion-item" style="background-color: papayawhip;">
                            <div class="accordion-button" style="background-color: wheat;" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                <h2 class="accordion-header">開光紀念
                                </h2>
                            </div>
                            <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row justify-content-center pt-3 pb-5">
                                        <img src="./Temples/images/ceremony/01.jpg" class="w-50" alt="" />
                                    </div>
                                    <div class="fs-5 fw-lighter lh-lg">
                                        農曆臘月初九，是開基天官武財神渡台紀念日，也是本宮宮慶。本宮按例於十二月初七至初九啟建護國息災祈安植福萬緣法會，為讓一路扶持武德宮的善信大德可於歲末年終之際，祈得個人來年的平安，家族的興旺，亦為眾生祝福累積殊勝功德。
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" style="background-color: papayawhip;">
                            <div class="accordion-button collapsed" style="background-color: wheat;" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <h2 class="accordion-header">朝禮星斗
                                </h2>
                            </div>
                            <div id="collapseTwo" class="accordion-collapse collapse" style="background-color: transparent;" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row justify-content-center pt-3 pb-5">
                                        <img src="./Temples/images/ceremony/02.jpg" class="w-50" alt="" />
                                    </div>
                                    <div class="fs-5 fw-lighter lh-lg">
                                        斗者，星斗也，在道教中人的死生與魂魄依歸均由各星斗府所管轄，尤其人之本命元辰、福禍功過更由北斗主判，故一般常言「南斗註生、北斗註死」。因此，禮斗即朝禮本命星君，如能求得庇佑則可逢凶化吉、元辰光彩！又每位星斗職掌與訴求不同，因此在禮斗法會中不僅祭祀本命星君，亦請求可補足運勢較弱之處的星君保佑，即可得安康吉祥！ 
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" style="background-color: papayawhip;">
                            <div class="accordion-button collapsed" style="background-color: wheat;" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                <h2 class="accordion-header">萬緣法會
                                </h2>
                            </div>
                            <div id="collapseThree" class="accordion-collapse collapse" style="background-color: transparent;" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row justify-content-center pt-3 pb-5">
                                        <img src="./Temples/images/ceremony/03.jpg" class="w-50" alt="" />
                                    </div>
                                    <div class="fs-5 fw-lighter lh-lg">
                                        本宮的萬緣法會由武財神作主，除了讓每個人祈得本命星君照護外，亦指示禮齋三日，為群生祈福。法會結合了道教的禮斗與佛教的經懺、施食，禮斗裨益元神光采，經懺功德更是消業植福田廣結善緣；初九放焰口普施，使幽眾聞法受食、除苦得樂而得到救脫。因此，萬緣法會不僅是個人祈得平安順事、增添福壽，同時是將慈悲功德迴向予全世界的方便法門，甚為殊勝！
                                <br />
                                        而萬緣法會中的每個斗，都會有一個主斗首或稱斗主，是為該場法會主要供奉該神明的主要功德主。至於該扶奉什麼斗、什麼神明？有一些企業或資深信眾，經年都扶奉固定的斗，但更多信眾會在濟世日時前來請示武財公，請武財公根據信眾的流年、健康、運勢等指引該年度適合扶奉的斗，以期來年整年能夠逢凶化吉、消災解厄、元辰光采、福壽綿延！ 
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" style="background-color: papayawhip;">
                            <div class="accordion-button collapsed" style="background-color: wheat;" type="button" data-bs-toggle="collapse" data-bs-target="#collapse4" aria-expanded="false" aria-controls="collapse4">
                                <h2 class="accordion-header">祈安植福
                                </h2>
                            </div>
                            <div id="collapse4" class="accordion-collapse collapse" style="background-color: transparent;" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row justify-content-center pt-3 pb-5">
                                        <img src="./Temples/images/ceremony/04.jpg" class="w-50" alt="" />
                                    </div>
                                    <div class="fs-5 fw-lighter lh-lg">
                                        以下簡單列舉禮斗聖班中若干斗之功用：
                                <br />
                                        斗父、斗母｜分別為混沌未開時之陽體與陰體，為紫微及眾星之父、母，可求眾星所轄之事。
                                <br />
                                        白玉佛菩薩｜主重大病苦急難
                                <br />
                                        地藏王菩薩斗｜超拔累世、冤親債主、父母眷屬增長功德、往生淨土消災免難、得福得惠、順心如意
                                <br />
                                        五路財神斗(共五斗)｜分別為東路、西路、南路、北路、中路。為開運招財、利市納富。
                                <br />
                                        紫微玉斗｜祈求延壽錫福、消災懺罪。
                                <br />
                                        孔明玉斗｜祈求貨財恆足、綿延不絕。
                                <br />
                                        關聖帝君斗｜忠義正義、文衡仲權，司職本、司其益。
                                <br />
                                        太歲玉斗｜祈求消災消禍、化解沖犯、免去刑剋。
                                <br />
                                        南斗｜主延壽、亦主爵祿，求益壽延年、加官晉爵。
                                <br />
                                        東斗｜主護命、亦利財祿，祈求去病延年、掃除災愆。
                                <br />
                                        中斗｜主護命，能消除殃業、延生保命。
                                <br />
                                        西斗｜主護身，祈求身體康泰、瓜迭綿長。
                                <br />
                                        北斗｜主解厄，主求消災厄、除疾疫、添福慧。
                                <br />
                                        北斗九皇星(共九斗)｜分別為北斗七星之天樞宮、天璇宮、天璣宮、天權宮、天衡宮、闓陽宮、瑤光宮、洞明宮、隱光宮。
                                <br />
                                        玉皇斗｜延壽、賜福、平安。
                                <br />
                                        三仙姑斗｜主生育、姻緣，求好姻緣或好人緣、生產順利、無子嗣者求子、有子女者求平安長大。  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            -->


            <!--Section 4:信眾服務-->
            <%--<section id="IndexService">--%>
            <div class="IndexService">
                <div class="row justify-content-center py-5">
                    <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                    <div class="CategoryTitle">-&nbsp;信眾服務&nbsp;-</div>
                    <hr class="Category" />
                </div>
                <div class="IndexServiceList">
                    <ul>
                        <li><a href="./Temples/service.aspx" title="祈福點燈">
                            <div>祈福點燈</div>
                        </a></li>
                        <li><a href="./Temples/service.aspx" title="中元普渡">
                            <div>中元普渡</div>
                        </a></li>
                        <li><a href="./Temples/service.aspx" title="補財庫">
                            <div>補財庫</div>
                        </a></li>
                    </ul>
                    <br />
                </div>
            </div>
            <%--</section>--%>


            <!--Section 2:合作宮廟-->
            <div class="IndexTemple">
                <div class="row justify-content-center py-5">
                    <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                    <div class="CategoryTitle">-&nbsp;合作宮廟&nbsp;-</div>
                    <hr class="Category" />
                </div>

                <div class="TempleList">

                    <% =Templelist %>


                    <%--<div>
                        <a href="./Temples/templeInfo.aspx?a=3">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/Temple/sample.png" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">大甲鎮瀾宮</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_01">祈福點燈</li>
                                    <li class="Tag_03">中元普渡</li>
                                    <li class="Tag_04">備用01</li>
                                    <li class="Tag_05">備用02</li>
                                    <li class="Tag_06">備用03</li>
                                </ul>
                            </div>
                        </a>
                    </div>

                    <div>
                        <a href="./Temples/empleInfo.aspx?a=4">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/temple/sample_h.jpg" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">新港奉天宮</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_01">祈福點燈</li>
                                    <li class="Tag_03">中元普渡</li>
                                </ul>
                            </div>
                        </a>
                    </div>

                    <div>
                        <a href="./Temples/templeInfo.aspx?a=6">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/temple/sample_wu.jpg" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">北港武德宮</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_01">祈福點燈</li>
                                    <li class="Tag_02">補財庫</li>
                                    <li class="Tag_03">中元普渡</li>
                                </ul>
                            </div>
                        </a>
                    </div>

                    <div>
                        <a href="./Temples/templeInfo.aspx?a=8">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/temple/sample_Fu.jpg" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">西螺福興宮</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_03">中元普渡</li>
                                </ul>
                            </div>
                        </a>
                    </div>

                    <div>
                        <a href="./Temples/templeInfo.aspx?a=9">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/temple/sample_Jing.jpg" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">桃園大廟景福宮</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_03">中元普渡</li>
                                </ul>
                            </div>
                        </a>
                    </div>

                    <div>
                        <a href="./Temples/templeInfo.aspx?a=10">
                            <div class="IndexTempleImg">
                                <img src="./Temples/images/temple/sample_Luer.jpg" width="600" height="400" alt="" />
                            </div>
                            <div class="IndexTempleName">台南正統鹿耳門聖母廟</div>
                            <div class="IndexTempleTag">
                                <ul>
                                    <li class="Tag_03">中元普渡</li>
                                </ul>
                            </div>
                        </a>
                    </div>--%>
                </div>
                <br />
            </div>
            <%=eventTitbitsList %>
            <%--         <div class="IndexEventTitbits">
             <div class="row justify-content-center py-5">
                 <img src="./Temples/images/roof.png" class="pb-1" style="width: 200px;" alt="" />
                 <div class="CategoryTitle">-&nbsp;活動花絮&nbsp;-</div>
                 <hr class="Category" />
             </div>
             <div class="EventTitbitsList">

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=3">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-1.jpg" alt="" />
                         </div>
                     </a>
                 </div>

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=4">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-2.jpg" alt="" />
                         </div>
                     </a>
                 </div>

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=6">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-3.jpg" alt="" />
                         </div>
                     </a>
                 </div>

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=8">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-4.jpg" alt="" />
                         </div>
                     </a>
                 </div>

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=9">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-5.jpg" alt="" />
                         </div>
                     </a>
                 </div>

                 <div>
                     <a href="./Temples/EventTitbits.aspx?a=10">
                         <div class="IndexEventTitbitsImg">
                             <img src="./Temples/SiteFile/EventTitbits/nature-6.jpg" alt="" />
                         </div>
                     </a>
                 </div>
             </div>
             <br />
         </div>
    
            --%>


            <!--Section 3:照片區域-->
            <%--     <section id="IndexPhoto">
                <div class="IndexPhoto"></div>
            </section>--%>



            <!--Section 6:歷年活動-->
            <%-- <section id="IndexHistoryEvent">
                <div class="IndexHistoryEventBk">
                    <ul class="HistoryEventList">--%>

            <%--<!--↓↓範例 (至少要放5筆資料)↓↓-->
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url('./Temples/SiteFile/Event/sample1.jpg');"></a></li>
                        <!--↑↑範例↑↑-->

                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20230709_NewsImg_s.png');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20230406_Supplies2Img.jpg');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20230318_PilgrimageImg.png');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20221125_LightsImg.png');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20220914_SuppliesImg.jpg');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20220709_PurdueImg.png');"></a></li>
                        <li class="IndexEventItem"><a href="eventContent.aspx" style="background: url(' SiteFile/Event/20211125_LightsImg.jpeg');"></a></li>--%>
            <%--    </ul>
                </div>
            </section>--%>
        </article>

        <!-----本頁內容結束----->
        <uc1:footer runat="server" ID="footer" />
    <%--</div>--%>
</body>
</html>
<!----------本頁js---------->
<!-----顯示選單----->
<script>
    $(window).on("load", function () {
        menuShow();
    })
    $(window).scroll(function () {
        menuShow();
    });
    function menuShow() {
        var $menuShowPlace = $("#carouselExampleCaptions").offset().top;
        var $scrollVal = $(this).scrollTop();
        //console.log($menuShowPlace);
        if ($scrollVal >= $menuShowPlace) {
            $("header").addClass("active");
        } else {
            $("header").removeClass("active");
        }
    }
</script>

<!-----宮廟輪播----->
<script type="text/javascript" src="./Temples/slick/slick.min.js"></script>
<script>
    $(document).ready(function () { 
        if (location.search.indexOf('fbda') >= 0) {
            $("#shop2").hide();
        }
        $('.TempleList').slick({
            dots: false,
            arrows: true,
            //infinite: true,
            //centerMode: true,
            centerPadding: '10vw',
            slidesToShow: 5,
            autoplay: true,
            autoplaySpeed: 3000,
            responsive: [
                {
                    breakpoint: 1190,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 3
                    }
                },
                {
                    breakpoint: 920,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 2
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        arrows: true,
                        centerMode: true,
                        centerPadding: '40px',
                        slidesToShow: 1
                    }
                }
            ]
        });


        $('.LightList').slick({
            dots: false,
            arrows: true,
            //infinite: true,
            //centerMode: true,
            centerPadding: '10vw',
            slidesToShow: 5,
            autoplay: true,
            autoplaySpeed: 3000,
            responsive: [
                {
                    breakpoint: 1440,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 3
                    }
                },
                {
                    breakpoint: 1024,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 2
                    }
                },
                {
                    breakpoint: 720,
                    settings: {
                        arrows: true,
                        centerMode: false,
                        centerPadding: '40px',
                        slidesToShow: 1
                    }
                }
            ]
        });

        $('.EventTitbitsList').slick({
            dots: false,
            arrows: true,
            //infinite: true,
            //centerMode: true,
            centerPadding: '10vw',
            slidesToShow: 5,
            autoplay: true,
            autoplaySpeed: 3000,
            responsive: [
                {
                    breakpoint: 1190,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 3
                    }
                },
                {
                    breakpoint: 920,
                    settings: {
                        centerPadding: '20vw',
                        slidesToShow: 2
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        arrows: true,
                        centerMode: true,
                        centerPadding: '40px',
                        slidesToShow: 1
                    }
                }
            ]
        });

    });
</script>

<!-----歷年活動跑馬燈----->
<script type="text/javascript" src="./Temples/js/grouploop-1.0.3.min.js"></script>
<script>
    $(".IndexHistoryEventBk").grouploop({
        velocity: 0.5,
        forward: false,
        pauseOnHover: true,
        childNode: ".IndexEventItem",
        childWrapper: ".HistoryEventList"
    })
</script>
