<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purdue13.aspx.cs" Inherits="twbobibobi.Temples.Purdue13" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="中元節拜拜懶人包：超市買得到的供品推薦｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/Purdue13.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜在忙碌的生活節奏下，很多人想拜拜卻卡在「不知道要準備什麼」、「時間來不及」，結果錯過中元節好時機。
        其實中元普渡不用繁複，只要心誠＋簡單準備，就能達到祈福、轉運的效果。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜在忙碌的生活節奏下，很多人想拜拜卻卡在「不知道要準備什麼」、「時間來不及」，結果錯過中元節好時機。
        其實中元普渡不用繁複，只要心誠＋簡單準備，就能達到祈福、轉運的效果。" />
    <!--簡介-->
    <meta property="og:site_name" content="中元節拜拜懶人包：超市買得到的供品推薦｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/purdue/13.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/purdue/13.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/Purdue13.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>中元節拜拜懶人包：超市買得到的供品推薦｜保必保庇</title>
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
                    <li>中元節拜拜懶人包：超市買得到的供品推薦</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue/13.jpg?t=55688" width="1160" height="550" alt="保必保庇中元節拜拜懶人包：超市買得到的供品推薦" 
                        title="中元節拜拜懶人包：超市買得到的供品推薦" />
                </div>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">中元節拜拜懶人包：超市買得到的供品推薦｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜在忙碌的生活節奏下，很多人想拜拜卻卡在「不知道要準備什麼」、「時間來不及」，結果錯過中元節好時機。
                                其實中元普渡不用繁複，只要心誠＋簡單準備，就能達到祈福、轉運的效果。
                            </p>
                        <br />
                        <br />
                        <h1 class="TempleName">🛒 超市就買得到的普渡供品推薦</h1>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>類別</th>
                                    <th>推薦項目</th>
                                    <th>象徵意涵</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="類別">飲料類</td>
                                    <td data-label="推薦項目">鮮奶茶、罐裝綠茶、瓶裝水</td>
                                    <td data-label="象徵意涵">清涼解渴</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">餅乾糖果</td>
                                    <td data-label="推薦項目">可樂果、乖乖、綜合禮盒</td>
                                    <td data-label="象徵意涵">誘引好兄弟、安撫孤魂</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">水果</td>
                                    <td data-label="推薦項目">蘋果（平安）、香蕉（招福）、橘子（吉祥）</td>
                                    <td data-label="象徵意涵">成雙吉利</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">米麵類</td>
                                    <td data-label="推薦項目">泡麵、白米小包、米香</td>
                                    <td data-label="象徵意涵">象徵供食、充實陰間糧倉</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">素三牲</td>
                                    <td data-label="推薦項目">豆干、素火腿、海苔豆皮</td>
                                    <td data-label="象徵意涵">慈悲不殺生</td>
                                </tr>
                                <tr>
                                    <td data-label="類別">金紙</td>
                                    <td data-label="推薦項目">普渡金、紙元寶一份</td>
                                    <td data-label="象徵意涵">功德回向、轉運聚財</td>
                                </tr>
                            </tbody>
                        </table>
                        <h2>✅小提醒：供品以乾淨、整齊、未過期為原則，並用紅紙墊底更添吉祥。</h2>
                        <br />
                        <h1 class="TempleName">🕯️ 簡易拜拜流程</h1>
                        <p>　1.	擇吉日準備，選擇下午2～5點間進行</p>
                        <p>　2.	桌子朝向外門，擺放供品、插香</p>
                        <p>　3.	誠心祝禱（例如：「誠心普渡十方好兄弟，祈求闔家平安順遂」）</p>
                        <p>　4.	焚燒金紙（於空曠安全處）</p>
                        <p>　5.	結語：「感謝諸位前來享用，請平安離開，勿留本處。」</p>
                        <br />
                        <h2>這樣的懶人版拜拜方式，不但實用，也保有文化尊重與祈福精神。即使身處都市，也能簡單完成一場有感的中元普渡。</h2>
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
