<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewYearNotes_05.aspx.cs" Inherits="twbobibobi.Temples.NewYearNotes_05" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="除夕到初四拜什麼？｜保必保庇" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/NewYearNotes_05.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇導讀｜在農曆新年期間，從除夕到初四的每一天都有特定的習俗與祭拜對象，反映出不同的文化傳統和祈福寓意。以下是每一天的祭拜內容與注意事項。" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇導讀｜在農曆新年期間，從除夕到初四的每一天都有特定的習俗與祭拜對象，反映出不同的文化傳統和祈福寓意。以下是每一天的祭拜內容與注意事項。" />
    <!--簡介-->
    <meta property="og:site_name" content="除夕到初四拜什麼？｜保必保庇" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="canonical" href="https://bobibobi.tw/Temples/NewYearNotes_05.aspx" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>除夕到初四拜什麼？｜保必保庇</title>
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
                    <li><a href="NewYearNotes_05.aspx" title="除夕到初四拜什麼？">除夕到初四拜什麼？</a></li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <h1 class="TempleName">除夕到初四拜什麼？｜保必保庇</h1>
                            <p>
                                保必保庇導讀｜在農曆新年期間，從除夕到初四的每一天都有特定的習俗與祭拜對象，反映出不同的文化傳統和祈福寓意。以下是每一天的祭拜內容與注意事項：
                            </p>
                        <br />
                        <h1 class="TempleName">除夕（祭祖、拜天公、拜神明）</h1>
                        <h2>主題：辭舊迎新、祭祖感恩</h2>
                        <span class="title">　　 祭拜對象：</span>
                        <p>　　　 。	<span class="title">祖先：</span>表達孝敬與感恩，祈求祖先庇佑家族平安。</p>
                        <p>　　　 。	<span class="title">天公（玉皇大帝）：</span>部分地區會拜天公，祈求新的一年風調雨順。</p>
                        <p>　　　 。	<span class="title">土地公：</span>拜土地公以感謝一年的庇護。</p>
                        <span class="title">　　 祭拜內容：</span>
                        <p>　　　 。	<span class="title">祭祖：</span>準備供品（雞、魚、肉、酒、飯菜等），以新鮮食物為主。</p>
                        <p>　　　 。	<span class="title">神明：</span>點香、燭，供上水果、甜點等。</p>
                        <br />
                        <h1 class="TempleName">注意事項：</h1>
                        <p>　。	祭祖通常在傍晚進行，祭拜後不可掃地，以示留福。</p>
                        <br />
                        <h1 class="TempleName">初一（拜天公、拜家中神明）</h1>
                        <h2>主題：迎接新年、祈福納吉</h2>
                        <span class="title">　　 祭拜對象：</span>
                        <p>　　　 。	<span class="title">家中神明：如灶神、財神、觀音等。</p>
                        <p>　　　 。	<span class="title">天公（玉皇大帝）：部分地區認為初一是敬天公的重要日子。</p>
                        <span class="title">　　 祭拜內容：</span>
                        <p>　　　 。	<span class="title">早上祭拜神明：</span>準備三牲、清茶、水果、發糕等吉祥供品。</p>
                        <p>　　　 。	<span class="title">燃香祭拜：</span>向家中神明敬香，祈求新年順遂。</p>
                        <p>　　　 。	<span class="title">拜財神：</span>部分地區會拜財神，祈求財運興旺。</p>
                        <br />
                        <h1 class="TempleName">注意事項：</h1>
                        <p>　。	初一忌殺生，因此供品多以素食為主。</p>
                        <p>　。	避免說不吉利的話。</p>
                        <br />
                        <h1 class="TempleName">初二（迎婿日、拜土地公）</h1>
                        <h2>主題：嫁出女兒的家人回娘家、拜土地公</h2>
                        <span class="title">　　 祭拜對象：</span>
                        <p>　　　 。	<span class="title">土地公：</span>感謝祂守護家庭、土地平安。</p>
                        <p>　　　 。	<span class="title">家中神明：</span>與初一類似，也可再次拜家中神明。</p>
                        <span class="title">　　 祭拜內容：</span>
                        <p>　　　 。	<span class="title">準備素食供品：</span>如糕點、水果、茶。</p>
                        <p>　　　 。	<span class="title">迎婿禮俗：</span>嫁出的女兒攜丈夫回娘家拜年。</p>
                        <br />
                        <h1 class="TempleName">注意事項：</h1>
                        <p>　。	回娘家需帶禮品如糖果、糕點等，表達感謝。</p>
                        <p>　。	土地公祭拜宜選擇早晨或上午進行。</p>
                        <br />
                        <h1 class="TempleName">初三（赤狗日、祭拜神靈、驅邪祈福）</h1>
                        <h2>主題：消災解厄、敬神明</h2>
                        <span class="title">　　 祭拜對象：</span>
                        <p>　　　 。	<span class="title">門神、灶神、土地公：</span>驅除不祥、迎來新年吉祥。</p>
                        <p>　　　 。	<span class="title">驅邪祈福：</span>部分地區會祭拜家宅守護神。</p>
                        <span class="title">　　 祭拜內容：</span>
                        <p>　　　 。	<span class="title">供品：</span>清茶、水果、鮮花等，簡單而不過於隆重。</p>
                        <p>　　　 。	<span class="title">驅邪儀式：</span>點燃鞭炮，象徵趕走煞氣和邪靈。</p>
                        <br />
                        <h1 class="TempleName">注意事項：</h1>
                        <p>　。	初三被認為是「赤狗日」，忌拜年，因此大多在家安靜祭拜神靈。</p>
                        <p>　。	避免與人爭執。</p>
                        <br />
                        <h1 class="TempleName">初四（接神日、迎灶神）</h1>
                        <h2>主題：接神迎福、祈求新年順遂</h2>
                        <span class="title">　　 祭拜對象：</span>
                        <p>　　　 。	<span class="title">灶神：</span>初四是灶神返回人間的日子。</p>
                        <p>　　　 。	<span class="title">家中神靈：</span>再次向家中神明敬香祈福。</p>
                        <p>　　　 。	<span class="title">財神：</span>部分地區會在初四迎財神。</p>
                        <span class="title">　　 祭拜內容：</span>
                        <p>　　　 。	<span class="title">供品：</span>茶、糕點、三牲、酒水，特別為灶神準備甜食，象徵祂多言吉語。</p>
                        <p>　　　 。	<span class="title">接神儀式：</span>點香、燃燭，祈求神明保佑新一年順遂。</p>
                        <br />
                        <h1 class="TempleName">注意事項：</h1>
                        <p>　。	初四宜家人團聚，迎接神明返回家宅。</p>
                        <p>　。	保持整潔，準備好的供品表達誠意。</p>
                        <br />
                        <h1 class="TempleName">總結</h1>
                        <p>　　　 <span class="title">除夕：</span>以祭祖為主，感恩祈福。</p>
                        <p>　　　 <span class="title">初一：</span>敬拜天公與家中神明，祈求平安與好運。</p>
                        <p>　　　 <span class="title">初二：</span>迎婿回娘家，拜土地公。</p>
                        <p>　　　 <span class="title">初三：</span>驅邪祈福，避免拜年。</p>
                        <p>　　　 <span class="title">初四：</span>接神迎福，敬灶神與財神。</p>
                        <p>每一天的祭拜既有傳統的文化意涵，也充滿對新年的美好祝願。遵循習俗進行祭拜，能為家庭帶來更多吉祥與安康！</p>
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
