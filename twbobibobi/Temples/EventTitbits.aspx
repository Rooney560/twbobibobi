<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventTitbits.aspx.cs" Inherits="twbobibobi.Temples.EventTitbits" %>

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
    <link rel="stylesheet" href="css/swiper-bundle.min.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <!-- Demo styles -->
    <style>
/*        html,
        body {
            position: relative;
            height: 100%;
        }

        body {
            background: #eee;
            font-family: Helvetica Neue, Helvetica, Arial, sans-serif;
            font-size: 14px;
            color: #000;
            margin: 0;
            padding: 0;
        }
*/
        .swiper {
            width: 100%;
            height: 100%;
        }

        .swiper-slide {
            text-align: center;
            font-size: 18px;
            background: #fff;
            display: flex;
            justify-content: center;
            align-items: center;
        }

            .swiper-slide img {
                display: block;
                width: 100%;
                height: 100%;
                object-fit: cover;
            }

        /*        body {
            background: #000;
            color: #000;
        }
*/
        .swiper {
            width: 100%;
            height: 300px;
            margin-left: auto;
            margin-right: auto;
        }

        .swiper-slide {
            background-size: cover;
            background-position: center;
        }

        .mySwiper2 {
            height: 80%;
            width: 100%;
        }

        .mySwiper {
            height: 20%;
            box-sizing: border-box;
            padding: 10px 0;
        }

            .mySwiper .swiper-slide {
                width: 25%;
                height: 100%;
                opacity: 0.4;
            }

            .mySwiper .swiper-slide-thumb-active {
                opacity: 1;
            }

        .swiper-slide img {
            display: block;
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        .swiper2-custom-height {
            height: 640px;
        }

        .swiper-custom-height {
            /*height: 100px;*/
        }

        .swiper-slide-container {
            background-color: transparent;
        }

            .swiper-slide-container img {
                width: auto;
                height: 100%;
                object-fit: contain; /* 图片按比例缩放，保持图片完整显示在容器内 */
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
                    <li><a href="EventTitbitsCategory.aspx" title="活動花絮列表">活動花絮列表</a></li>
                    <li>點燈活動花絮</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="">
                    <div style="--swiper-navigation-color: #fff; --swiper-pagination-color: #fff" class="swiper mySwiper2">
                        <div class="swiper-wrapper swiper2-custom-height">
<%=mainList %>
<%--                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-1.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-2.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <iframe width="1102" height="624" src="https://www.youtube.com/embed/2E79wMxeWWs" title="#大樹朝天宮 #開廟門 #新廟慶成 #入火安座  #壬寅年" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-4.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-5.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-6.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-7.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-8.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-9.jpg" />
                            </div>
                            <div class="swiper-slide swiper-slide-container">
                                <img src="SiteFile/EventTitbits/nature-10.jpg" />
                            </div>--%>
                        </div>
                        <div class="swiper-button-next"></div>
                        <div class="swiper-button-prev"></div>
                    </div>
                    <div thumbsslider="" class="swiper mySwiper">
                        <div class="swiper-wrapper swiper-custom-height">
<%=navList %>
<%--                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-1.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-2.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-3.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-4.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-5.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-6.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-7.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-8.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-9.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="SiteFile/EventTitbits/nature-10.jpg" />
                            </div>--%>
                        </div>
                        <div class="swiper-button-next"></div>
                        <div class="swiper-button-prev"></div>
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
<!-- Swiper JS -->
<script src="js/swiper-bundle.min.js"></script>

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



    var swiper = new Swiper(".mySwiper", {
        loop: true,
        spaceBetween: 10,
        slidesPerView: 7,
        freeMode: true,
        watchSlidesProgress: true,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        //autoplay: {
        //    delay: 3000,
        //    stopOnLastSlide: false,
        //    disableOnInteraction: true,
        //},
    });
    var swiper2 = new Swiper(".mySwiper2", {
        loop: true,
        spaceBetween: 10,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        thumbs: {
            swiper: swiper,
        },
        autoplay: {
            delay: 5000,
            stopOnLastSlide: false,
            disableOnInteraction: false,
            pauseOnMouseEnter: true,
        },
    });

</script>
