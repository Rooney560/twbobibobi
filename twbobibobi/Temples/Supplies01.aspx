<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Supplies01.aspx.cs" Inherits="twbobibobi.Temples.Supplies01" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="天赦日是什麼？2025有哪幾天｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/Supplies01.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜天赦日是中國傳統曆法中的一個重要吉日，被視為全年最吉利的日子之一。這一天是天道赦罪之日，有「上天開恩赦罪」之意，傳說中是上天原
        諒眾生過失、赦免罪過的日子，因此被稱為「天赦日」。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜天赦日是中國傳統曆法中的一個重要吉日，被視為全年最吉利的日子之一。這一天是天道赦罪之日，有「上天開恩赦罪」之意，傳說中是
        上天原諒眾生過失、赦免罪過的日子，因此被稱為「天赦日」。" />
    <!--簡介-->
    <meta property="og:site_name" content="天赦日是什麼？2025有哪幾天｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/Supplies01.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>天赦日是什麼？2025有哪幾天｜保必保庇</title>
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
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }
            .title {
            font-size: 3.8vw;
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
                    justify-content: space-between;
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
                    <li><a href="ArticleColumn.aspx" title="文章專欄">文章專欄</a></li>
                    <li>天赦日介紹</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">天赦日是什麼？2025有哪幾天｜保必保庇</h1>
                        <div>
                            <p>
                                <span class="title">天赦日</span>中國傳統曆法中的一個重要吉日，被視為<span class="title">全年最吉利的日子之一</span>。
                                這一天是<span class="title">天道赦罪之日</span>，有「上天開恩赦罪」之意，傳說中是上天原諒眾生過失、赦免罪過的日子，因此被稱為「天赦日」。
                            </p>
                        </div>
                        <br />
                        <h1 class="TempleName">天赦日的由來與意義</h1>
                        <p>「赦」的意思是赦免、寬恕。在古代天子有「大赦天下」的權力，象徵赦罪、重生。相傳天赦日是由上天所定的特別時日，在這一天，天門大開、萬事可赦，是一年當中最適合<span class="title">化解業障、祈福轉運、補運補財庫</span>的日子。</p>
                        <p>因此，許多信仰民眾會在天赦日進行：</p>
                        <div>
                            <p>　<span class="title">•	補財庫</span></p>
                            <p>　<span class="title">•	還陰債</span></p>
                            <p>　<span class="title">•	祈求好運</span></p>
                            <p>　<span class="title">•	化解災厄</span></p>
                            <p>　<span class="title">•	改運祈福</span></p>
                            <p>　<span class="title">•	補運點燈</span></p>
                            <p>　<span class="title">•	求職轉業</span></p>
                        </div>
                        <br />
                        <h1 class="TempleName">📌 天赦日如何判定？（農民曆的算法）</h1>
                        <p>天赦日每年出現 4～6次，但不是固定的日子，而是根據天干地支與節氣交錯而定。簡單來說：</p>
                        <div>
                            <p>　<span class="title">•	春季（立春之後）：</span>天干為「戊」日</p>
                            <p>　<span class="title">•	夏季（立夏之後）：</span>天干為「甲」日</p>
                            <p>　<span class="title">•	秋季（立秋之後）：</span>天干為「戊」日</p>
                            <p>　<span class="title">•	冬季（立冬之後）：</span>天干為「甲」日</p>
                        </div>
                        <p>搭配<span class="title">特定的地支日子與節氣組合</span>，由專業曆師或《通書》記載判定，才會標示為「天赦」。</p>
                        <br />
                        <h1 class="TempleName">天赦日可以做什麼？</h1>
                        <p><span class="title">宜做之事（大吉）</span></p>
                        <div>
                            <p>　•	補財庫、補運</p>
                            <p>　•	化解官司、訴訟</p>
                            <p>　•	改名、改運、求職</p>
                            <p>　•	安太歲、安神位</p>
                            <p>　•	婚嫁、動土（視個人八字配合）</p>
                        </div>
                        <p><span class="title">忌諱之事</span></p>
                        <div>
                            <p>　•	若與個人八字犯沖，仍需避開</p>
                            <p>　•	雖為大吉日，但若農民曆另有「破日」、「黑道日」等不吉註記，需謹慎評估</p>
                        </div>
                        <br />
                        <h1 class="TempleName">小提醒</h1>
                        <div>
                            <p>　•	天赦日是百無禁忌的大吉日，但若個人流年運勢欠佳，建議搭配<span class="title">擇日師傅、命理師</span>協助更準確。</p>
                            <p>　•	廟宇通常會在這幾天辦補運、補財庫法會，可以參加來求轉運。</p>
                        </div>
                        <br />
                        <h1 class="TempleName">限時補運儀式開放報名，誠心參與，讓財運、福氣、貴人運全面提升！ 把握天赦日，翻轉好運勢！</h1>
                        <br />
                        <h2>2025天赦日有哪幾天</h2>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>日期（國曆）</th>
                                    <th>農曆</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="日期（國曆）">2025/03/10</td>
                                    <td data-label="農曆">二月十一</td>
                                </tr>
                                <tr>
                                    <td data-label="日期（國曆）">2025/05/25</td>
                                    <td data-label="農曆">四月二八</td>
                                </tr>
                                <tr>
                                    <td data-label="日期（國曆）">2025/07/24</td>
                                    <td data-label="農曆">六月三十</td>
                                </tr>
                                <tr>
                                    <td data-label="日期（國曆）">2025/08/07</td>
                                    <td data-label="農曆">閏六月十四</td>
                                </tr>
                                <tr>
                                    <td data-label="日期（國曆）">2025/10/06</td>
                                    <td data-label="農曆">八月十五</td>
                                </tr>
                                <tr>
                                    <td data-label="日期（國曆）">2025/12/21</td>
                                    <td data-label="農曆">十一月初三</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <h2>選擇宮廟：</h2>
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="templeService_supplies_ty.aspx" target="_blank" title="桃園威天宮">桃園威天宮</a></li>
                                        <li><a href="templeService_supplies_ma.aspx" target="_blank" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                    </ul>
                                </div>
                            </li>
                        </ul>
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
