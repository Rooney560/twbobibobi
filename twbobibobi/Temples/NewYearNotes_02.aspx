<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewYearNotes_02.aspx.cs" Inherits="twbobibobi.Temples.NewYearNotes_02" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="過年祭祖注意事項｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/NewYearNotes_02.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜過年祭祖是重要的傳統儀式，代表對祖先的尊敬與感恩，同時祈求新年家宅平安、運勢昌隆。在進行祭祖儀式時，有以下注意事項，幫助您莊重且順利地完成儀式。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜過年祭祖是重要的傳統儀式，代表對祖先的尊敬與感恩，同時祈求新年家宅平安、運勢昌隆。在進行祭祖儀式時，有以下注意事項，幫助您莊重且順利地完成儀式。" />
    <!--簡介-->
    <meta property="og:site_name" content="過年祭祖注意事項｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/NewYearNotes_02.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>過年祭祖注意事項｜保必保庇</title>
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
        }
        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
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
                    <li><a href="NewYearNotes.aspx" title="過年期間注意事項">過年期間注意事項</a></li>
                    <li><a href="NewYearNotes_02.aspx" title="過年祭祖注意事項">過年祭祖注意事項</a></li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">過年祭祖注意事項｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜過年祭祖是重要的傳統儀式，代表對祖先的尊敬與感恩，同時祈求新年家宅平安、運勢昌隆。在進行祭祖儀式時，有以下注意事項，幫助您莊重且順利地完成儀式：
                            </p>
                        <br />
                        <h1 class="TempleName">時間選擇</h1>
                        <p>　。	<span class="title">適合時段：</span>過年祭祖通常選在除夕或正月初一的早上進行，以示對祖先的敬重。</p>
                        <p>　。	<span class="title">避免晦氣時辰：</span>避免在午夜或過晚的時間祭祖，選擇白天進行更為吉祥。</p>
                        <br />
                        <h1 class="TempleName">準備供品</h1>
                        <p>　。	<span class="title">新鮮供品：</span>供品如水果、糕點、熟食應選擇新鮮的，避免使用隔夜菜或不新鮮的食品。</p>
                        <p>　。	<span class="title">供品種類：</span>水果如橙子（寓意吉利）、蘋果（寓意平安），糕點如發糕（象徵發財），另可準備酒、茶和熟食（如雞、魚）。</p>
                        <p>　。	<span class="title">數量講究：</span>供品數量以雙數為佳，如六、八，象徵吉利與圓滿。</p>
                        <p>　。	<span class="title">禁忌供品：</span>避免供梨（諧音「離」）或李子（象徵分離），也不要使用破損或腐壞的供品。</p>
                        <br />
                        <h1 class="TempleName">香燭與紙錢</h1>
                        <p>　。	<span class="title">足夠的香燭：</span>點香時應準備足量的香燭，通常以三支為宜，象徵天、地、人和諧。</p>
                        <p>　。	<span class="title">紙錢焚燒順序：</span>紙錢應按規定順序焚燒，避免隨意丟棄。</p>
                        <p>　。	<span class="title">確保香燭不熄滅：</span>祭拜過程中，香燭若熄滅被視為不吉利，需重新點燃。</p>
                        <br />
                        <h1 class="TempleName">儀式環境</h1>
                        <p>　。	<span class="title">清潔祭祀場所：</span>祭祖前需先清理祭桌和祖先牌位，保持整潔，避免污垢或雜物。</p>
                        <p>　。	<span class="title">擺放牌位：</span>祖先牌位應擺放在高於人頭的位置，避免低於視線，這被視為不敬。</p>
                        <br />
                        <h1 class="TempleName">儀式後注意</h1>
                        <p>　。	<span class="title">香燭燃盡後再處理：</span>祭祖結束後，需等待香燭完全燃燒後再清理。</p>
                        <p>　。	<span class="title">分食供品：</span>供品不可直接丟棄，應分給家人共享，象徵祖先賜福。</p>
                        <p>　。	<span class="title">保留清潔：</span>祭拜後應保持祭桌的整潔，避免馬上進行打掃。</p>
                        <br />
                        <h1 class="TempleName">特別禮儀</h1>
                        <p>　。	<span class="title">燃放鞭炮：</span>祭祖後可適時燃放鞭炮，以增添喜氣並象徵驅邪迎福。</p>
                        <p>　。	<span class="title">祈求祝福：</span>祭拜時應真誠向祖先祈求新年的祝福，表達感恩與敬意。</p>
                        <br />
                        <h1 class="TempleName">其他補充</h1>
                        <p>　。	<span class="title">避免過於簡化：</span>祭祖儀式不宜過於簡化或草率，應依循傳統規矩進行。</p>
                        <p>　。	<span class="title">特別家庭習俗：</span>若家族有特定的祭祖習俗，應尊重並遵循。</p>
                        <br />
                        <h1 class="TempleName">總結</h1>
                        <p>過年祭祖是對祖先表達感恩與祈福的重要儀式，應以莊重、虔誠的態度進行，並遵守相關禮儀與禁忌。
                            通過恰當的祭祖方式，既能延續傳統文化，也能祈求新年家庭平安、興旺發達！</p>
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
