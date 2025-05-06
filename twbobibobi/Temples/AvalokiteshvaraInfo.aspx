<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvalokiteshvaraInfo.aspx.cs" Inherits="twbobibobi.Temples.AvalokiteshvaraInfo" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="觀音三會是哪幾天？誕辰／出家／得道，如何祭拜？｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/AvalokiteshvaraInfo.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜觀世音菩薩是華人世界中最受敬仰的佛教菩薩之一，其慈悲救苦、有求必應的形象深入人心。信眾會在特定的節日祭拜觀音，祈求平安、健康、順利與感應。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜觀世音菩薩是華人世界中最受敬仰的佛教菩薩之一，其慈悲救苦、有求必應的形象深入人心。信眾會在特定的節日祭拜觀音，祈求平安、健康、順利與感應。" />
    <!--簡介-->
    <meta property="og:site_name" content="觀音三會是哪幾天？誕辰／出家／得道，如何祭拜？｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/AvalokiteshvaraInfo.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>觀音三會是哪幾天？誕辰／出家／得道，如何祭拜？｜保必保庇</title>
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
                    <li>觀世音菩薩介紹</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">觀音三會是哪幾天？誕辰／出家／得道，如何祭拜？｜保必保庇</h1>
                        <div>
                            <p>
                                觀世音菩薩是華人世界中最受敬仰的佛教菩薩之一，其慈悲救苦、有求必應的形象深入人心。信眾會在特定的節日祭拜觀音，祈求平安、健康、順利與感應。以下為觀音菩薩的重要紀念日、祭拜方式與供品準備建議：
                            </p>
                        </div>
                        <br />
                        <h1 class="TempleName">觀世音菩薩三大紀念日</h1>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>節日</th>
                                    <th>農曆日期</th>
                                    <th>意義</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="節日">聖誕日</td>
                                    <td data-label="農曆日期">農曆 2 月 19 日</td>
                                    <td data-label="意義">菩薩誕生之日</td>
                                </tr>
                                <tr>
                                    <td data-label="節日">出家日</td>
                                    <td data-label="農曆日期">農曆 9 月 19 日</td>
                                    <td data-label="意義">菩薩剃度出家紀念日</td>
                                </tr>
                                <tr>
                                    <td data-label="節日">成道日</td>
                                    <td data-label="農曆日期">農曆 6 月 19 日</td>
                                    <td data-label="意義">菩薩成道、證悟日</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <p>👉 這三天合稱為「觀音三會」，皆為極吉祥之日，適合祭拜、點燈、行善、放生、誦經、求籤祈福等。</p>
                        <br />
                        <h1 class="TempleName">如何祭拜觀音菩薩？</h1>
                        <h2>祭拜地點</h2>
                        <div>
                            <p>　■	佛寺／觀音殿／家中佛堂皆可。</p>
                            <p>　■	若無佛堂，也可於乾淨明亮處設香案暫拜。</p>
                        </div>
                        <h2>祭拜流程建議</h2>
                        <div>
                            <p>　 1.  潔淨身心（洗手、換乾淨衣物）</p>
                            <p>　 2.  擺放香案供品</p>
                            <p>　 3.  上香（三炷香為主）</p>
                            <p>　 4.  誠心禮拜</p>
                            <p>　 5.  誦唸觀音聖號或《普門品》、《大悲咒》</p>
                            <p>　 6.  默念願望、祈求平安</p>
                            <p>　 7.  燒香禱告後燒金紙（視寺廟規定而定）</p>
                        </div>
                        <br />
                        <h1 class="TempleName">觀音菩薩供品準備建議</h1>
                        <h2>適合供品</h2>
                        <p>觀音為清淨慈悲之相，供品應以「素食」、「潔淨」為原則。</p>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>類別</th>
                                    <th>供品建議</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="類別">水果</td>
                                    <td data-label="供品建議">蘋果（平安）、香蕉（團圓）、葡萄（吉祥）、橘子（大吉）、梨子（離苦得樂）等皆可</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">甜品</td>
                                    <td data-label="供品建議">發糕、壽桃、紅龜粿、糖果等（象徵喜氣與祝壽）</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">鮮花</td>
                                    <td data-label="供品建議">百合（純潔）、蓮花（智慧）、菊花（莊嚴）、康乃馨（慈悲）等</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">清水</td>
                                    <td data-label="供品建議">一杯清水代表清淨心</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">香／燭</td>
                                    <td data-label="供品建議">三炷香、一對紅燭（或電子燭）</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <h2>供品水果數量與禁忌</h2>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>項目</th>
                                    <th>說明</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="項目">☑ 奇數</td>
                                    <td data-label="說明">最佳為 3、5、7 種水果或數量，代表吉祥、圓滿</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">✖ 忌雙數</td>
                                    <td data-label="說明">雙數常用於祭祖或喪事場合，避免 2、4、6 類型的搭配</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">✖ 忌供葷食</td>
                                    <td data-label="說明">包含肉類、酒類、葱蒜韭薤等五辛皆不宜上桌</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">✖ 忌腐壞水果</td>
                                    <td data-label="說明">供品須新鮮完整，不可有爛點或損傷</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">✖ 忌供釋迦果</td>
                                    <td data-label="說明">雖名「釋迦」，但此為佛名，避免供奉以示敬意</td>
                                </tr>
                            </tbody>
                        </table>

                        <br />
                        <h1 class="TempleName">建議搭配功德行為</h1>
                        <div>
                            <p>　•	誦經、持咒（如誦《大悲咒》、《普門品》、《心經》）</p>
                            <p>　• 點燈祈福（光明燈、姻緣燈、健康燈）</p>
                            <p>　• 放生、佈施</p>
                            <p>　• 發願吃素一日，淨心禮佛</p>
                        </div>
                        <br />
                        <h1 class="TempleName">小提醒</h1>
                        <div>
                            <p>　•	若無法到寺廟祭拜，也可在家以誠心禮拜、唸佛、供花水方式代替。</p>
                            <p>　• 觀音菩薩以「慈悲為本」，重心誠不在形式，心誠則靈。</p>
                        </div>
                        <br />
                        <h1 class="TempleName">觀音燈介紹</h1>
                        <h2>點一盞觀音燈，讓慈悲與光明照亮人生</h2>
                        <div>
                            <p>觀音燈，是供奉給<span class="title">觀世音菩薩</span>的祈福明燈，象徵慈悲、智慧與守護。
                                透過點燈，信眾向菩薩祈求平安健康、消災解厄、家庭和樂、願望成就，心誠則靈。</p>
                        </div>
                        <br />
                        <h2>選擇宮廟：</h2>
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="templeService_lights_Fu.aspx" target="_blank" title="西螺福興宮">西螺福興宮</a></li>
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
