<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purdue12.aspx.cs" Inherits="twbobibobi.Temples.Purdue12" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="上班族也能參拜！懶人版中元普渡清單下載｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/Purdue12.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜沒時間張羅三牲五果？中元節又剛好碰上週三？別擔心，這篇為你準備一份「懶人版中元普渡指南」
        ，不論你是租屋族、小資族、公司代表，都能輕鬆完成拜拜儀式，求平安不漏福！" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜沒時間張羅三牲五果？中元節又剛好碰上週三？別擔心，這篇為你準備一份「懶人版中元普渡指南」
        ，不論你是租屋族、小資族、公司代表，都能輕鬆完成拜拜儀式，求平安不漏福！" />
    <!--簡介-->
    <meta property="og:site_name" content="上班族也能參拜！懶人版中元普渡清單下載｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/purdue/12.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/purdue/12.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/Purdue12.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>上班族也能參拜！懶人版中元普渡清單下載｜保必保庇</title>
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
                    <li><a href="PurdueGuide.aspx" title="普渡說明">普渡說明</a></li>
                    <li>上班族也能參拜！懶人版中元普渡清單下載</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue/12.jpg?t=55688" width="1160" height="550" alt="保必保庇上班族也能參拜！懶人版中元普渡清單下載" 
                        title="上班族也能參拜！懶人版中元普渡清單下載" />
                </div>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">上班族也能參拜！懶人版中元普渡清單下載｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜沒時間張羅三牲五果？中元節又剛好碰上週三？別擔心，這篇為你準備一份「懶人版中元普渡指南」
                                ，不論你是租屋族、小資族、公司代表，都能輕鬆完成拜拜儀式，求平安不漏福！
                            </p>
                        <br />
                        <br />
                        <h1 class="TempleName">🗂️ 懶人版中元普渡清單（供品簡化版）</h1>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>供品類別</th>
                                    <th>建議項目（便利店就買得到）</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="供品類別">飲料類</td>
                                    <td data-label="建議項目（便利店就買得到）">罐裝茶飲、運動飲料、瓶裝水</td>
                                </tr>
                                <tr>
                                    <td data-label="供品類別">餅乾糖果</td>
                                    <td data-label="建議項目（便利店就買得到）">綜合禮盒、巧克力棒、鹹餅乾</td>
                                </tr>
                                <tr>
                                    <td data-label="供品類別">水果</td>
                                    <td data-label="建議項目（便利店就買得到）">蘋果、香蕉、橘子（成雙成對即可）</td>
                                </tr>
                                <tr>
                                    <td data-label="供品類別">素三牲</td>
                                    <td data-label="建議項目（便利店就買得到）">豆干、素火腿、海苔豆皮</td>
                                </tr>
                                <tr>
                                    <td data-label="供品類別">金紙</td>
                                    <td data-label="建議項目（便利店就買得到）">普渡金、紙元寶一份</td>
                                </tr>
                            </tbody>
                        </table>
                        <h2>✅ 不需三牲熟食，也不必祭拜祖先，只要簡單誠心備好一桌，就能參與。</h2>
                        <br />
                        <h1 class="TempleName">📍 公司普渡技巧</h1>
                        <p>若你代表公司拜拜，可考慮以下簡單流程：</p>
                        <p>　1.	供桌設在騎樓或門口，時間選中午至下午3點間</p>
                        <p>　2.	準備簡單食物與飲品＋香＋金紙</p>
                        <p>　3.	念出公司名稱與祈福願望：「保佑公司業績蒸蒸日上，員工平安健康」</p>
                        <br />
                        <h1 class="TempleName">🔄 拜完記得：收桌＋謝語</h1>
                        <h2>祭拜完後，可唸：「感謝各位好兄弟前來享用，請平安離開、不留本處。」象徵儀式圓滿與結束。食物可帶回與同事共享，象徵轉化功德。</h2>
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
