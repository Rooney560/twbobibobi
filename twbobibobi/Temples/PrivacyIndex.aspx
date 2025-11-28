<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivacyIndex.aspx.cs" Inherits="Temple.Temples.PrivacyIndex" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="隱私權政策|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/PrivacyIndex.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="隱私權政策|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>隱私權政策|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <style type="text/css">
        .ServiceTempleList {
            width: calc(100% - 11vw);
        }

        .ServiceType {
            width: 10vw;
            margin-right: 1vw;
            text-align: right;
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
</head>
<body>
    <div id="wrap">
        <!--#warp //start-->

        <!--頁首選單-->
        <uc2:header runat="server" id="header" />
        <!-----本頁內容開始----->
        <article id="Service" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li>隱私權政策</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="Content">
                    <div style="text-align: center;">
                        <h2>隱私權政策</h2>
                    </div>
                    <div class="">
                        <p>感謝您使用 九九商通科技股份有限公司 的網上購物服務。 九九商通科技股份有限公司 致力於保護您的隱私權，請您仔細閱讀以下政策內容：</p>
                        <h2>1. **資料收集與使用**</h2>
                        <p>-  九九商通科技股份有限公司 會收集您提供的個人資訊，如姓名、地址、電子郵件等，用於處理訂單及提供客戶服務。</p>
                        <p>- 購物網站使用cookies以提升您的使用體驗，但您可透過瀏覽器設定拒絕或管理這些cookies。</p>
                        
                        <h2>2. **資料分享**</h2>
                        <p>-  九九商通科技股份有限公司 承諾不會出售、租賃或交換您的個人資訊給第三方，除非經您同意或法律規定。</p>
                        <p>- 與合作夥伴及第三方服務供應商的資訊分享，僅用於執行 九九商通科技股份有限公司 的服務。</p>

                        <h2>3. **安全保障**</h2>
                        <p>-  九九商通科技股份有限公司 致力於保護您的資訊安全，透過適當的技術措施，防止未經授權的訪問、使用、洩漏或修改個人資訊。</p>
                        
                        <h2>4. **未成年人保護**</h2>
                        <p>-  九九商通科技股份有限公司 不主動收集未成年人的個人資訊。若您發現未成年人提供了個人資訊，請立即通知 九九商通科技股份有限公司 以刪除相關資料。</p>
                        
                        <h2>5. **隱私權政策修訂**</h2>
                        <p>-  九九商通科技股份有限公司 保留修改本政策的權利，任何修改將在網站上公布，並生效於公布後的合理期間。</p>
                        <br />
                        <p>謝謝您閱讀 九九商通科技股份有限公司 的隱私權政策，如有任何問題，請隨時聯繫 九九商通科技股份有限公司 。</p>
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
