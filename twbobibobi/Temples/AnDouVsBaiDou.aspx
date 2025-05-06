<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnDouVsBaiDou.aspx.cs" Inherits="twbobibobi.Temples.AnDouVsBaiDou" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="安斗 vs 拜斗 的差異｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/AnDouVsBaiDou.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜許多人拜過斗，卻不一定安過斗！「拜斗」祈願一次性加持，「安斗」則是在天上立名，長期補運護元神。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜許多人拜過斗，卻不一定安過斗！「拜斗」祈願一次性加持，「安斗」則是在天上立名，長期補運護元神。" />
    <!--簡介-->
    <meta property="og:site_name" content="安斗 vs 拜斗 的差異｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/AnDouVsBaiDou.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>安斗 vs 拜斗 的差異｜保必保庇</title>
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
                    <li>安斗 vs 拜斗 的差異</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">安斗 vs 拜斗 的差異｜保必保庇</h1>
                        <h2>【安斗 vs 拜斗，你分得清嗎？】</h2>
                        <div>
                            <p>許多人拜過斗，卻不一定安過斗！</p>
                            <p>「拜斗」祈願一次性加持，「安斗」則是在天上立名，長期補運護元神。</p>
                            <p>點一盞斗燈，神明全年守護不間斷；</p>
                            <p>參加一次拜斗，誠心請願化災解厄。</p>
                            <p>想轉運、補氣、解煞，先搞懂怎麼選！</p>
                        </div>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>項目</th>
                                    <th>安斗</th>
                                    <th>拜斗</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="項目">意義</td>
                                    <td data-label="安斗">在天界「立名上奏」，安置斗燈供奉神明</td>
                                    <td data-label="拜斗">透過誦經、上表祈願，向斗府神明稟報請願</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">方式</td>
                                    <td data-label="安斗">由宮廟登記姓名、生辰八字後，供奉斗燈於神前全年</td>
                                    <td data-label="拜斗">多數為一日或特定時辰舉辦的科儀，現場誦經、疏文</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">時效</td>
                                    <td data-label="安斗">多為「全年」或「數月」安奉</td>
                                    <td data-label="拜斗">通常是「一次性」的法會或節日特定日子舉辦</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">主要功用</td>
                                    <td data-label="安斗">提升整體運勢、補元神、祈福消災、持續護佑</td>
                                    <td data-label="拜斗">祈求特定願望如解厄、轉運、病癒、補財庫等</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">神明對象</td>
                                    <td data-label="安斗">主祀北斗星君、南斗星君，或天公</td>
                                    <td data-label="拜斗">同樣以斗府諸星君為主，但可搭配不同神明</td>
                                </tr>
                                <tr>
                                    <td data-label="項目">是否點燈</td>
                                    <td data-label="安斗">☑ 通常會供奉「斗燈」</td>
                                    <td data-label="拜斗">✖ 不一定點燈，重在誦經與科儀</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <h1 class="TempleName">簡單比喻</h1>
                        <div>
                            <p>　•	<span class="title">安斗</span>像是「在天上登記戶口、點燈開通神明加持」</p>
                            <p>　• <span class="title">拜斗</span>則像是「參加一場正式的祈福法會，集中請願一次」</p>
                        </div>
                        <br />
                        <h1 class="TempleName">適合時機</h1>
                        <div>
                            <p>　•	如果你希望全年受到神明庇佑，補運補元神，建議「安斗」。</p>
                            <p>　• 如果你目前有特定困難、急需轉運或逢節日想祈福，「拜斗」是很好的選擇。</p>
                        </div>
                        <br />
                        <h1 class="TempleName">【安奉斗燈服務開放報名中】</h1>
                        <div>
                            <p>想補運、轉運、求平安，卻不知道從哪裡開始？</p>
                            <p>【安斗】是上奏天庭、補足元神的祈福首選！</p>
                            <p>由廟方為您登記姓名、生辰八字，全年供奉斗燈於神明座前，</p>
                            <p>祈求神明加持——消災解厄、補運補元神、運勢提昇、諸事順利！</p>
                            <p>提供多種專屬斗燈選擇：</p>
                            <span class="title">平安斗｜事業斗｜財神斗｜文昌斗｜藥師斗｜元神斗｜福祿壽斗</span>
                            <p>誠心點燈，神明護佑全年不間斷。</p>
                            <p>現在就為自己或親人點一盞斗燈，讓神光長伴！</p>
                        </div>
                        <br />
                        <h2>選擇宮廟：</h2>
                        <ul class="ServiceList">
                            <!--細項服務項目-->
                            <li>
                                <div class="ServiceTempleList">
                                    <ul>
                                        <li><a href="templeService_andou_Fw.aspx" target="_blank" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                        <li><a href="templeService_andou_wjsan.aspx" target="_blank" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
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

