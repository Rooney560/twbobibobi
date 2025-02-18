<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="Temple.Temples.news" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/news.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="最新消息|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>最新消息|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Bootstrap/js/bootstrap.bundle.min.js"></script>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
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
                    <li>最新消息</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="IndexNewsList2 PageNewsList Page1">
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
                            <a href="https://bobibobi.tw/Temples/templeService_supplies3_ty.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250121_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="桃園威天宮正月初九天公生招財補運" title="桃園威天宮正月初九天公生招財補運" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">桃園威天宮正月初九天公生招財補運</div>
                                <div class="fs-5">正月初九是玉皇大帝的聖誕，也稱為【天公生】覺得自己運氣不順，財庫不足，在新的一年向玉皇上帝祝壽、祈福補運補財庫，讓您事業興旺、財運順遂。</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>    
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/templeService_supplies_sx.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250110_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="小龍蛇年旺財氣，赦罪補庫好運到" title="小龍蛇年旺財氣，赦罪補庫好運到" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">小龍蛇年旺財氣，赦罪補庫好運到</div>
                                <div class="fs-5">赦罪解業在於懺悔改過與累積正能量。透過宗教儀式、行善積德、真誠懺悔等方式，去消除過往的負面影響，使內心得到寧靜與解脫，也為未來的人生奠下美好的基礎。</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>    
                    </div>
                    <div class="row gx-5"> 
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2025supplies_Lk.aspx">
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
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lybc_dh.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20241127_NewsImg_s.jpg" class="card-img-top p-3" alt="台東東海龍門天聖宮２０２４護國息災梁皇大法會" title="台東東海龍門天聖宮２０２４護國息災梁皇大法會" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">台東東海龍門天聖宮２０２４護國息災梁皇大法會</div>
                                <div class="fs-5">『#梁皇寶懺』乃是大乘佛教中最具威力的懺悔法門之一，它不僅能夠消除眾生的業障、還能夠增長智慧、培養慈悲心』護國息災梁皇大法會</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024taoistJiaoCeremony_da.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20240923_NewsImg_s.jpg" class="card-img-top p-3" alt="大甲鎮瀾宮重修慶成祈安七朝清醮活動" title="大甲鎮瀾宮重修慶成祈安七朝清醮活動" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４大甲鎮瀾宮重修慶成祈安七朝清醮活動</div>
                                <div class="fs-5">甲辰龍年，大甲鎮瀾宮重修慶成，舉行祈安七朝清醮盛典酎謝神恩，期望各界共襄盛舉，踴躍協助，同心付出，再現風華。祈佑風調雨順、國泰民安,邀請各界善信大德共襄盛舉。</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024supplies.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20241001_NewsImg_s.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４北港武德宮下元補庫活動</div>
                                <div class="fs-5">財是流量，庫則是存量，有財無庫，財來則財去，累積不了多少，總是剩餘不多。也因此十方善信來到財神祖廟，最嚮往的一個法門，就是“補財庫”。</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024supplies2_ty.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240913_NewsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４桃園威天宮九九重陽天赦日補運活動</div>
                                <div class="fs-5">關聖帝君是第18代玉皇大帝，【天赦日】是 玉皇大帝赦免災厄，同時也是招財改運最好的日子，將您的運勢補強轉正，讓您運晉財入、闔家平安、好運旺旺來！</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lingbaolidou_ma.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240827_NewsImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４玉敕大樹朝天宮靈寶禮斗活動報名開始 ~</div>
                                <div class="fs-5">靈寶禮斗是一種祈求自身本命元辰光彩消災解厄、祈福延壽之科儀故能消災、祈福、祛病、延生</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024purdue_pet.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/images/temple/purdue_pet_02.jpg" class="card-img-top p-3" alt="" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">２０２４中元普渡寵物普度活動</div>
                                        <div class="fs-5">
                                            報名參加【鎮瀾宮】代辦寵物中元普渡（喵星人澎派組）（毛小孩澎派組）<br />
                                            即完成中元喵星人 毛小孩普渡，植福、轉好運！
                                        </div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024purdue_baby.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240730_NewsImg.png" class="card-img-top p-3" alt="" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">２０２４超渡嬰靈活動</div>
                                        <div class="fs-5">
                                            藉由中元超拔法會，讓無緣子女前來聞經受渡，沾領法會功德
                            早日超渡能夠歸依佛國淨土，讓陽世報恩人能夠向無緣子女表達歉意，並累積陽世報恩人的福報。
                                        </div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024purdue.aspx">
                                <%--<div class="card h-100" style="background-color: #ffffff78">--%>
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples//SiteFile/News/20240621_NewsImg.png" class="card-img-top p-3" alt="" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">２０２４中元普渡線上報名開始囉~</div>
                                        <div class="fs-5">
                                            一年一度的農曆七月十五日中元普渡，幫助好兄弟，渡化眾生，值福、做好事、得功德。贊普施食供養使鬼道眾生來受法食，仗神佛的慈悲願力，
                                    讓一切餓鬼皆能得渡，成就無上功德。
                                        </div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="IndexNewsList2 PageNewsList Page2">
                    <div class="row gx-5">
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024emperorGuansheng_ty.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240618_NewsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４桃園威天宮關聖帝君聖誕千秋活動報名開始 ~</div>
                                <div class="fs-5">威天宮特別舉辦【關聖帝君聖誕千秋 祝壽謝恩祈福儀式】，敬邀大德報名設立【祝壽謝恩招財祿位】</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights_pet.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240520_NewsImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４毛小孩專屬平安燈線上報名開始~</div>
                                <div class="fs-5">🐱🐶🐭🐹🐰🐦🐴🐡🐍🦎🦂軟萌限定~毛起來愛牠！🏮毛小孩專屬平安燈🏮</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024supplies_ty.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240518_NewsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４桃園威天宮天赦日補運活動線上報名開始~</div>
                                <div class="fs-5">迎芒種接端午 5/30開運天赦日</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <!--↑↑範例↑↑-->
                    </div>
                    <div class="row gx-5">
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights_ty.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240426_NewsImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４桃園威天宮孝恩祈福活動線上報名開始~</div>
                                <div class="fs-5">在 帝君座前點一盞【孝親祈福燈】，為您的父母、祖父母、養育恩親祈求 關聖帝君降臨護佑，為親人添福、添平安。</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights_Fu.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240409_NewsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４西螺福興宮文昌燈活動線上報名開始~</div>
                                <div class="fs-5">考試來臨龍免驚~文昌加持助考運~現在只要點一盞西螺福興宮的文昌燈，將會贈送文昌筆吊飾</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024supplies2.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20240322_NewsImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４北港武德宮武財神聖誕補庫進財線上報名開始~</div>
                                <div class="fs-5">『保必保庇』每年接近此時節，分靈出去的大小宮廟甚或神壇與一般家戶都會不約而同踴躍回娘家為老財神爺祝壽。</div>
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>  
                    </div>
                    <div class="row gx-5">   
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2024lights.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20231220_NewsImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２４甲辰龍年新春點燈線上報名開始囉~</div>
                                <div class="fs-5">『保必保庇』今年與9家全台知名宮廟配合線上報名點燈服務，提供信眾更多元化的選擇，讓您在忙碌之餘也可以輕鬆完成點燈保平安，為自己與家人祈福。</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2023supplies.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20231012_NewsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２３北港武德宮下元補庫線上報名開始~</div>
                                <div class="NewsDate">2023-10-12</div>
                                <div class="fs-5">農曆十月十五日為水官大帝聖誕，民間又稱為下元節，三官大帝負責考核眾生功過，水官主解厄，由於人們的財庫受福報災厄影響，因此這一日同時也是補財庫的大日，人們禮敬水官大帝，祈求消除一切災障困厄後，再請武財公賜財賜福。</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>  
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2023purdue.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20230709_NewsImg_s.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２３中元普渡線上報名開始囉~</div>
                                <div class="NewsDate">2023-07-09</div>
                                <div class="fs-5">一年一度的農曆七月十五日中元普渡，幫助好兄弟，渡化眾生，值福、做好事、得功德。贊普施食供養使鬼道眾生來受法食，仗神佛的慈悲願力，讓一切餓鬼皆能得渡，成就無上功德。好兄弟聞經受渡在家裡普只做到普，而無法做到渡，在大廟裡參與普渡能讓好兄弟聞經受渡。線上報名好方便與臺灣知名宮廟合作，接受線上報名，讓您在忙碌之餘也可以輕鬆完成中元普渡。善的循環選擇捐出普渡後的普品，幫助社會弱勢，做到真正的冥陽兩利。</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>    
                    </div>
                    <div class="row gx-5">
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2023supplies2.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20230406_Supplies2Img.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２３補財庫線上報名開始囉~</div>
                                <div class="NewsDate">2023-04-06</div>
                                <div class="fs-5">財庫是甚麼?就是你在世間所能承接裝盛、所能積累的財富的多寡。嚴格來說，財是流量，庫則是存量，有財無庫，財來則財去，累積不了多少，總是剩餘不多。也因此十方善信來到財神祖廟，最嚮往的一個法門，就是“補財庫”。</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2023Pilgrimage.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20230318_PilgrimageImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２３媽祖繞境活動線上報名開始囉~</div>
                                <div class="NewsDate">2023-03-18</div>
                                <div class="fs-5">2023大甲媽祖遶境即將開跑~讓我們一起感謝媽祖，用感恩的誠心參加一年一度的媽祖繞境，錢母加持讓您好運旺旺來！虎爺香火袋把平安帶著走！</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2023lights.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20221125_LightsImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２３新春兔年點燈線上報名開始囉~</div>
                                <div class="NewsDate">2022-11-25</div>
                                <div class="fs-5">新年新氣象 | 點燈迎好運 | 線上報名最方便，大甲鎮瀾宮：光明燈、太歲燈、文昌燈；新港奉天宮：光明燈、太歲燈；北港武德宮：光明燈、太歲燈、財神燈</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>  
                    </div>
                </div>
                <div class="IndexNewsList3 PageNewsList Page3">
                    <div class="row gx-5">
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓--> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2022supplies.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20220914_SuppliesImg.jpg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２２下元補庫線上報名開始囉~</div>
                                <div class="NewsDate">2022-09-14</div>
                                <div class="fs-5">農曆十月十五日為水官大帝聖誕，民間又稱為下元節，三官大帝負責考核眾生功過，水官主解厄，由於人們的財庫受福報災厄影響，因此這一日同時也是補財庫的大日，人們禮敬水官大帝，祈求消除一切災障困厄後，再請武財公賜財賜福。歡迎信眾來廟參拜祈求水官大帝保佑，無災無厄迎接新的一年！</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2022purdue.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20220709_PurdueImg.png" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２２中元普渡線上報名開始囉~</div>
                                <div class="NewsDate">2022-07-09</div>
                                <div class="fs-5">與大甲鎮瀾宮、新港奉天宮、北港武德宮合作，讓信眾以線上報名的方式參與中元普渡，省去舟車勞頓至宮廟報名的時間，普渡完成信眾可以選擇將普品捐出或是寄回(運費自負)。</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/newsContent_2022lights.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="SiteFile/News/20211125_LightsImg.jpeg" class="card-img-top p-3" alt="" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２２新春虎年點燈線上報名開始囉~</div>
                                <div class="NewsDate">2021-11-25</div>
                                <div class="fs-5">新年新氣象 | 點燈迎好運 | 線上報名最方便，大甲鎮瀾宮：光明燈、太歲燈、文昌燈；新港奉天宮：光明燈、太歲燈；北港武德宮：光明燈、太歲燈、財神燈</div>
                                
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div> 
                    </div>
                </div>

                <!--頁碼示意-->
                <div class="pageCtrl">
                    <ul class="pagelist">
                        <%--<li id="PageCtrl_F"><a href="#" title="最前頁">
                            <img src="images/pageCtrl_L1.png" width="30" height="30" alt="" /></a></li>
                        <li id="PageCtrl_P"><a href="#" title="上一頁">
                            <img src="images/pageCtrl_L2.png" width="30" height="30" alt="" /></a></li>
                        <li page-id="1"><a href="#" class="active" title="第1頁">1</a></li>
                        <li page-id="2"><a href="#"  title="第2頁">2</a></li>
                        <li id="PageCtrl_N"><a href="#" title="下一頁">
                            <img src="images/pageCtrl_R2.png" width="30" height="30" alt="" /></a></li>
                        <li id="PageCtrl_L"><a href="#" title="最後頁">
                            <img src="images/pageCtrl_R1.png" width="30" height="30" alt="" /></a></li>--%>
                    </ul>
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

        goPage(1, 12);

    })

    function goPage(pno, psize) {
        $(".pagelist").empty();
        var num = $(".PageNewsList").find(".py-3").length;  //最新消息總數
        var totalPage = 0;//總共幾頁
        var pageSize = psize;//一頁顯示幾行
        //以下是總共會有幾個分頁
        if (num / pageSize > parseInt(num / pageSize)) {  //paresInt是去小數點
            totalPage = parseInt(num / pageSize) + 1; //有剩就要多一頁
        } else {
            totalPage = parseInt(num / pageSize); //整除就不用多一頁
        }
        var currentPage = pno;//當前第幾頁

        $(".PageNewsList").hide();
        $(".Page" + pno).show();

        var tempStr = ""; //存上一頁 1 2 3 4 5 下一頁

        var innital = currentPage; //下面的頁面 [1] 2 3 4 5 
        var after = currentPage + 2; // 1 2 3 4 [5] 顯示到共五頁


        if (totalPage <= 2) {
            innital = 1 //如果頁面不到五頁，強迫從1開始數
        }

        else if (innital + 2 >= totalPage) {
            innital = totalPage - 2 // 不要讓初始頁面爆表 若只有7頁 選到[5] innital 一樣是[3] 4 5 6 7 
        }

        if (after >= totalPage) {
            after = totalPage //若 after超過總頁數一定只能讓他在總頁數 若只有7頁 選到[5] after 一樣是3 4 5 6 [7] 
        }

        if (currentPage > 1) {
            tempStr = "<li id=\"PageCtrl_F\"><a href=\"#\" title=\"最前頁\" onClick=\"goPage(1," + psize + ")\"><img src=\"images/pageCtrl_L1.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
        }
        else {
            tempStr = "<li id=\"PageCtrl_F\"><a title=\"最前頁\"><img src=\"images/pageCtrl_L1.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
        }

        if (currentPage > 1) { //不是第一頁了，上一頁有連結 ，連結#是因為不用跳轉頁面，沒有導向任何網站，只是讓他可以按觸發onClick()方法
            tempStr += "<li id=\"PageCtrl_P\"><a href=\"#\" title=\"上一頁\" onClick=\"goPage(" + (currentPage - 1) + "," + psize + ")\"><img src=\"images/pageCtrl_L2.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
            //tempStr += "<a href=\"#\" onClick=\"goPage(" + (currentPage - 1) + "," + psize + ")\"><上一頁     </a>"
            for (var j = innital; j <= after; j++) { //跑innital ~ after的頁面 j =當前頁面 psize顯示行數

                if (currentPage == j) {
                    tempStr += "<li><a class=\"active\" title=\"第" + j + "頁\">" + j + "</a></li>";
                }
                else {
                    tempStr += "<li><a href=\"#\" onClick=\"goPage(" + j + "," + psize + ")\" title=\"第" + j + "頁\">" + j + "</a></li>"
                }
            }
        } else {  //第一頁，所以上一頁沒有連結
            tempStr += "<li id=\"PageCtrl_P\"><a title=\"上一頁\"><img src=\"images/pageCtrl_L2.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
            for (var j = innital; j <= after; j++) { //跑innital ~ after的頁面 j =當前頁面 psize顯示行數
                if (currentPage == j) {
                    tempStr += "<li><a class=\"active\" title=\"第" + j + "頁\">" + j + "</a></li>";
                }
                else {
                    tempStr += "<li><a href=\"#\" onClick=\"goPage(" + j + "," + psize + ")\" title=\"第" + j + "頁\">" + j + "</a></li>"
                }
            }


        }

        if (currentPage < totalPage) { //還沒到最後一頁，所以下一頁還有效果
            tempStr += "<li id=\"PageCtrl_N\"><a href=\"#\" title=\"下一頁\" onClick=\"goPage(" + (currentPage + 1) + "," + psize + ")\"><img src=\"images/pageCtrl_R2.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";

        } else { //到最後一頁了，下一頁無效化
            tempStr += "<li id=\"PageCtrl_N\"><a title=\"下一頁\"><img src=\"images/pageCtrl_R2.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";

        }

        if (currentPage == totalPage) {
            tempStr += "<li id=\"PageCtrl_L\"><a title=\"最後頁\"><img src=\"images/pageCtrl_R1.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
        }
        else {
            tempStr += "<li id=\"PageCtrl_L\"><a href=\"#\" title=\"最後頁\" onClick=\"goPage(" + after + "," + psize + ")\"><img src=\"images/pageCtrl_R1.png\" width=\"30\" height=\"30\" alt=\"\"/></a></li>";
        }

        $(".pagelist").append(tempStr);
    }

</script>
