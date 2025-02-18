<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventTitbitsCategory.aspx.cs" Inherits="twbobibobi.Temples.EventTitbitsCategory" %>

<%@ Register Src="~/Temples/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Temples/header.ascx" TagPrefix="uc2" TagName="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="活動花絮|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/EventTitbits.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="活動花絮|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>活動花絮|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Bootstrap/js/bootstrap.bundle.min.js"></script>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style_index.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <style>
        .btn-category {
            width: 100%;
        }
    </style>
</head>
<body>
    <div id="wrap">
        <!--#warp //start-->

        <!--頁首選單-->
        <uc2:header runat="server" ID="header" />
        <!-----本頁內容開始----->
        <article id="EventTitbits" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li>活動花絮列表</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="container text-center">

                    <div class="row row-cols-2">
<%=eventList %>
<%--                        <div class="col p-4">
                            <h1 class="display-6 text-info">點燈 - 活動花絮 (1092筆)</h1>
                            <a href="EventTitbits.aspx" title="點燈活動花絮"><img src="SiteFile/EventTitbits/nature-1.jpg" class="img-thumbnail" alt="點燈活動花絮"></a>
                        </div>
                        <div class="col p-4">
                            <h1 class="display-6 text-info">補財庫 - 活動花絮 (657筆)</h1>
                            <a href="EventTitbits.aspx" title="補財庫活動花絮">
                                <img src="SiteFile/EventTitbits/nature-2.jpg" class="img-thumbnail" alt="補財庫活動花絮">
                            </a>
                        </div>
                        <div class="col p-4">
                            <h1 class="display-6 text-info">中元普渡 - 活動花絮 (23筆)</h1>
                            <a href="EventTitbits.aspx" title="中元普渡活動花絮">
                                <img src="SiteFile/EventTitbits/nature-3.jpg" class="img-thumbnail" alt="中元普渡活動花絮">
                            </a>
                        </div>--%>
                    </div>
                </div>
                <br />
            </section>

        </article>
        <!-----本頁內容結束----->
        <uc1:footer runat="server" ID="footer" />
    </div>
</body>
</html>
<!----------本頁js---------->

<!-----顯示選單----->
<script>
    $(function () {
        $("header").addClass("active");
    })
    $(window).scroll(function () {
        menuShow();
    });
    function menuShow() {
        var $menuShowPlace = 80; // $("#EventTitbits").offset().top;
        var $scrollVal = $(this).scrollTop();
        console.log($scrollVal + " - " + $menuShowPlace);
        if ($scrollVal > $menuShowPlace) {
            $("header").removeClass("active");
        } else {
            $("header").addClass("active");
        }
    }
</script>
