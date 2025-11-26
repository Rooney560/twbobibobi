<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmperorGuansheng07.aspx.cs" Inherits="twbobibobi.Temples.EmperorGuansheng07" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/EmperorGuansheng07.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜走進廟裡拜關聖帝君，你可能會注意到：有的關公坐著拿書、有的站著握刀、有的則是威風凜凜地騎著赤兔馬。其實，關聖帝君在民間信仰中形象多變，每一種「神像姿態」與「聖號封號」都有獨特意義與護佑功能。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜走進廟裡拜關聖帝君，你可能會注意到：有的關公坐著拿書、有的站著握刀、有的則是威風凜凜地騎著赤兔馬。其實，關聖帝君在民間信仰中形象多變，每一種「神像姿態」與「聖號封號」都有獨特意義與護佑功能。" />
    <!--簡介-->
    <meta property="og:site_name" content="廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/emperorGuansheng/7.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/emperorGuansheng/7.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/EmperorGuansheng7.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？｜保必保庇</title>
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
                    <li><a href="EmperorGuanshengGuide.aspx" title="關聖帝君聖誕說明">關聖帝君聖誕說明</a></li>
                    <li>廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/emperorGuansheng/7.jpg?t=55688" width="1160" height="550" alt="保必保庇廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？" 
                        title="廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？" />
                </div>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">廟裡供的是哪尊關公？三大關聖帝君神像分別代表什麼？｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜走進廟裡拜關聖帝君，你可能會注意到：有的關公坐著拿書、有的站著握刀、有的則是威風凜凜地騎著赤兔馬。其實，關聖帝君在民間信仰中形象多變，每一種「神像姿態」與「聖號封號」都有獨特意義與護佑功能。
                            </p>
                            <p>
                                走進廟裡拜關聖帝君，你可能會注意到：有的關公坐著拿書、有的站著握刀、有的則是威風凜凜地騎著赤兔馬。其實，關聖帝君在民間信仰中形象多變，每一種「神像姿態」與「聖號封號」都有獨特意義與護佑功能。
                            </p>
                        <br />
                        <br />
                        <h1 class="TempleName">【一、為什麼關聖帝君有這麼多形象？】</h1>
                        <p>
                            關聖帝君原是歷史人物關羽，後來被歷代皇帝加封成神，再加上民間信仰的融合，逐漸衍生出不同的神格與樣貌。根據道教、儒教與佛教的融合演變，關公的神像在廟中通常會出現以下三種主要形象，各自象徵不同的神聖功能。
                        </p>
                        <h2>【第一尊：文衡聖帝——坐像閱書型】</h2>
                        <h2>🔹 形象特徵：</h2>
                        <p>　•	端坐在椅上</p>
                        <p>　•	手持《春秋》或經書</p>
                        <p>　•	神情沉穩內斂</p>
                        <h2>🔹 象徵意義：</h2>
                        <p>這是最文靜、內斂的關公形象，象徵他博學多才、精通義理。這一尊關公又被稱為「文衡聖帝」，主掌智慧、道德、誠信，尤其適合祈求：</p>
                        <p>　•	考試順利、學業進步</p>
                        <p>　•	決策清明、心性穩定</p>
                        <p>　•	經商誠信、人際圓融</p>
                        <h2>🔹 適合對象：</h2>
                        <p>　•	學子考生</p>
                        <p>　•	公司高階主管、創業者</p>
                        <p>　•	希望強化思考與智慧的人</p>
                        <p>📌特別說明：有些信眾會在這尊神像前點「文昌燈」，加強靈感與邏輯力。</p>
                        <br />
                        <h2>【第二尊：武財神關公——立像持刀型】</h2>
                        <h2>🔹 形象特徵：</h2>
                        <p>　•	站立姿態，英氣勃發</p>
                        <p>　•	右手握青龍偃月刀</p>
                        <p>　•	目光炯炯、怒目圓睜</p>
                        <h2>🔹 象徵意義：</h2>
                        <p>這是最常見的關公形象，被尊為「武財神」或「威靈顯赫關聖帝君」。他代表關公忠勇剛正的一面，能鎮煞、避邪、保事業、助正財，是許多商家與企業愛拜的形象。</p>
                        <p>適合祈求：</p>
                        <p>　•	生意興隆、正財進帳</p>
                        <p>　•	避小人、破官司</p>
                        <p>　•	鎮煞除邪、家宅平安</p>
                        <h2>🔹 適合對象：</h2>
                        <p>　•	生意人、公司行號</p>
                        <p>　•	律師、警察、軍人</p>
                        <p>　•	想求事業穩定或避厄者</p>
                        <p>📌補充小知識：此形象常放於店面、櫃台、公司收銀處，以鎮財位、護營業。</p>
                        <br />
                        <h2>【第三尊：騎馬關公——伏魔勇將型】</h2>
                        <h2>🔹 形象特徵：</h2>
                        <p>　•	英姿煥發地騎在赤兔馬上</p>
                        <p>　•	披掛鎧甲、揮刀前衝</p>
                        <p>　•	通常神態較剛烈、有戰將氣息</p>
                        <h2>🔹 象徵意義：</h2>
                        <p>這尊關公象徵「行動力」、「突破困境」、「化煞避災」。民間稱其為「伏魔關聖帝君」或「關將軍」。常見於驅邪法會或鎮煞場所。</p>
                        <p>適合祈求：</p>
                        <p>　•	驅邪除煞、破惡運</p>
                        <p>　•	面對大變動、困難決策</p>
                        <p>　•	出征、遠行、重要挑戰</p>
                        <h2>🔹 適合對象：</h2>
                        <p>　•	即將創業、轉職、遷居者</p>
                        <p>　•	出國工作、遠行在外的人</p>
                        <p>　•	面臨壓力與挑戰的人</p>
                        <p>📌很多武廟或道壇會以「三尊並列」形式供奉，表示文武兼備、動靜皆宜。</p>
                        <br />
                        <h1 class="TempleName">【其他常見關公造像延伸補充】</h1>
                        <p>除了上述三種主流形象，還有一些特殊造像也值得一提：</p>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>造像名稱</th>
                                    <th>特徵與意涵</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="造像名稱">關公與周倉、關平</td>
                                    <td data-label="特徵與意涵">三尊並列，象徵忠孝節義、團隊合作</td>
                                </tr>
                                <tr>
                                    <td data-label="造像名稱">閉目關公像</td>
                                    <td data-label="特徵與意涵">少見造像，象徵「不怒自威」，強化內在修養</td>
                                </tr>
                                <tr>
                                    <td data-label="造像名稱">彩繪神像</td>
                                    <td data-label="特徵與意涵">多見於戲劇化神轎與陣頭活動中，強化神威顯赫感</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <p>📌特別提醒：拜關公不只是外在形式，更重要是對「義氣、誠信、正直」的內化認同。</p>
                        <br />
                        <h1 class="TempleName">【如何挑選適合自己的關公神像來拜？】</h1>
                        <p>你可以根據自己的需求與處境選擇拜哪一尊關公：</p>
                        <table class="tablecontent">
                            <thead>
                                <tr>
                                    <th>你的狀況</th>
                                    <th>適合拜的形象</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td data-label="你的狀況">考試、提案、思緒卡住</td>
                                    <td data-label="適合拜的形象">文衡聖帝（坐像）</td>
                                </tr>
                                <tr>
                                    <td data-label="你的狀況">想穩定財運、擋小人</td>
                                    <td data-label="適合拜的形象">武財神（立像）</td>
                                </tr>
                                <tr>
                                    <td data-label="你的狀況">創業初期、轉職挑戰</td>
                                    <td data-label="適合拜的形象">騎馬關公（突破型）</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <p>如果你不確定怎麼選，建議你誠心走進廟中，依照感覺與直覺「緣到即拜」，關公不分形象，皆可感應誠意之人。</p>
                        <br />
                        <h1 class="TempleName">【結語：不同神像，同一信仰核心】</h1>
                        <p>不論你拜的是哪一尊關聖帝君，核心信仰都是「忠、義、誠、信、勇」。形象只是神明顯化的象徵，而真正的神力來自於你的「正念」與「正行」。</p>
                        <p>下次走進廟中，不妨花幾分鐘看看供奉的關公是哪一尊，再對照自己的需求，心中更有方向、祈願更聚焦。記得，不是姿態決定靈驗，而是你拜神的誠意與行動力，讓你與神明之間的力量真正產生連結。</p>
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
