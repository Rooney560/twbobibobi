<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleColumn.aspx.cs" Inherits="twbobibobi.Temples.ArticleColumn" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="文章專欄|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/ArticleColumn.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="文章專欄|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>文章專欄|【保必保庇】線上宮廟服務平台</title>
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
                    <li>文章專欄</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="IndexNewsList2 PageNewsList Page1">
                    <div class="row gx-5">       
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/DuanWuWuShiShui.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250429_DuanWuWuShiShui_s.jpg?t=666168" class="card-img-top p-3" alt="端午節午時水：自製、用途、保存全指南" title="端午節午時水：自製、用途、保存全指南" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">端午節午時水：自製、用途、保存全指南</div>
                                        <div class="fs-5">午節作為中國傳統的節日，不僅有賽龍舟、吃粽子的習俗，還有一項傳承多年的習慣——製作午時水。這種水被認為擁有強大的驅邪、保平安、祈福的功效。</div>

                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/ZodiacFortune_Lixia.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250430_ZodiacFortune_Lixia_s.jpg?t=666168" class="card-img-top p-3" alt="立夏12生肖的運勢如何？" title="立夏12生肖的運勢如何？" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">立夏12生肖的運勢如何？</div>
                                        <div class="fs-5">立夏是二十四節氣中的一個重要節氣，標誌著春天的結束和夏天的開始。在中國傳統文化中，立夏不僅是農業上的節氣轉變，還象徵著一年中的氣候、運勢等方面的變化。</div>

                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/AnDouVsBaiDou.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250429_AnDouVsBaiDou_s.jpg?t=666168" class="card-img-top p-3" alt="安斗 vs 拜斗 的差異" title="安斗 vs 拜斗 的差異" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">安斗 vs 拜斗 的差異</div>
                                        <div class="fs-5">許多人拜過斗，卻不一定安過斗！「拜斗」祈願一次性加持，「安斗」則是在天上立名，長期補運護元神。</div>

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
                            <a href="https://bobibobi.tw/Temples/AvalokiteshvaraInfo.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250424_Avalokiteshvara_s.jpg?t=666168" class="card-img-top p-3" alt="觀音三會是哪幾天？誕辰／出家／得道" title="觀音三會是哪幾天？誕辰／出家／得道" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">觀音三會是哪幾天？誕辰／出家／得道</div>
                                        <div class="fs-5"> 觀世音菩薩是華人世界中最受敬仰的佛教菩薩之一，其慈悲救苦、有求必應的形象深入人心。信眾會在特定的節日祭拜觀音，祈求平安、健康、順利與感應。</div>

                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/AnDouInformation.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250422_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="安斗是什麼？" title="安斗是什麼？" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">安斗是什麼？</div>
                                        <div class="fs-5">安斗就像是加強版的光明燈，不僅點亮前路，更是在神明座前為自己「立名入冊」，將個人姓名、生辰八字正式上呈天庭。</div>

                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/Supplies02.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                    <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250424_Supplies_s.jpg?t=55282" class="card-img-top p-3" alt="為什麼要補財庫？" title="為什麼要補財庫？" />
                                    <%--</div>--%>
                                    <div class="card-body">
                                        <div class="fs-3">為什麼要補財庫？</div>
                                        <div class="fs-5">每個人出生就有一個無形的「財庫」。財庫若滿，代表財運豐厚，容易存錢、聚財。財庫若空或破損，就算努力賺錢，也容易漏財、破財。</div>

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
                            <a href="https://bobibobi.tw/Temples/Supplies01.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250221_NewsImg_s.jpg?t=666168" class="card-img-top p-3" alt="天赦日是什麼？2025有哪幾天" title="天赦日是什麼？2025有哪幾天" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">天赦日是什麼？2025有哪幾天</div>
                                <div class="fs-5">天赦日中國傳統曆法中的一個重要吉日，被視為全年最吉利的日子之一。 這一天是天道赦罪之日，有「上天開恩赦罪」之意。</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>   
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/NewYearNotes.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250424_NewYearImg_s.jpg?t=666168" class="card-img-top p-3" alt="過年期間注意事項" title="過年期間注意事項" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">過年期間注意事項</div>
                                <div class="fs-5">若前往宗教景點，應遵守參訪規範。過年期間保持和氣，少爭執，方能在拜拜與出遊中收穫更多福氣與歡樂。</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div> 
                        <div class="col-lg-4 col-sm-12 py-3">
                            <a href="https://bobibobi.tw/Temples/ZodiacFortune.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/images/temple/Zodiac/Snake_2025.jpg" class="card-img-top p-3" alt="保必保庇２０２５生肖運勢" title="保必保庇２０２５生肖運勢" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">２０２５蛇年十二生肖運勢</div>
                                <div class="fs-5">2025年是乙巳蛇年，乙木與巳火結合，木火相生，這一年不同生肖的運勢會受到「蛇」 這個生肖以及天干地支的影響。</div>
                               
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
                            <a href="https://bobibobi.tw/Temples/LightsGuide.aspx">
                                <div class="card shadow h-100" style="background-color: white; color: #707070;">
                                <%--<div class="Newsimg">--%>
                                    <img src="https://bobibobi.tw/Temples/SiteFile/News/20250424_NewsImg_s.jpg" class="card-img-top p-3" alt="保必保庇 燈種說明" title="保必保庇 燈種說明" />
                                <%--</div>--%>
                                    <div class="card-body">
                                <div class="fs-3">燈種說明</div>
                                <div class="fs-5">2025蛇年犯太歲生肖有哪些? 線上祈福點燈安太歲，點燈介紹一次看</div>
                               
                                    </div>
                                    <div class="card-footer pb-3" style="border-top: none; background-color: transparent;">
                                        <div class="ReadMoreBtn"><span>&nbsp;詳情&nbsp;</span></div>
                                    </div>
                                </div>
                            </a>
                        </div>    
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                    </div>
                </div>
                <%--<div class="IndexNewsList2 PageNewsList Page2">
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓-->   
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                    </div>
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓-->   
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                    </div>
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓-->   
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                    </div>
                    <div class="row gx-5">  
                        <!--↓↓範例 (3筆一列，建議一頁放6筆或12筆)↓↓-->   
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                        <div class="col-lg-4 col-sm-12 py-3">
                        </div>      
                    </div>
                </div>
                <div class="IndexNewsList3 PageNewsList Page3">
                </div>--%>

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
        var num = $(".PageNewsList").find(".py-3").length;  //文章專欄總數
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
