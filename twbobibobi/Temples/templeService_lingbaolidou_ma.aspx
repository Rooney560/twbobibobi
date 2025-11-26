<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lingbaolidou_ma.aspx.cs" Inherits="twbobibobi.Temples.templeService_lingbaolidou_ma" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="玉敕大樹朝天宮|靈寶禮斗活動|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lingbaolidou_ma.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="玉敕大樹朝天宮|靈寶禮斗活動|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />
    <meta property="og:video" content="https://www.youtube.com/embed/7k0i9T1f5BY" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />


    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lingbaolidou_ma_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lingbaolidou_ma_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lingbaolidou_ma_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="玉敕大樹朝天宮｜靈寶禮斗 線上報名中" />
    <meta name="twitter:description" content="參加玉敕大樹朝天宮 銀同媽祈福法會，線上報名中，附介紹影片說明流程與時間。" />
    <meta name="twitter:image" content="https://bobibobi.tw/Temples/images/temple/lingbaolidou_ma_2025.jpg" />

    <title>玉敕大樹朝天宮|靈寶禮斗活動|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
     <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link href="css/Activity.css?t=2589963" rel="stylesheet" />
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
        .track-temple {
            display: block;
            position: relative;
            width: 100%;
            height: auto;
            --color-schema-primary: rgb(15, 102, 114);
            --color-schema-primary-light: #ed261a;
            --color-schema-secondary: #bd8520;
            --hanwanghei-heavy-font: "Times New Roman", Times, "hanWangHeiHeavy", "微軟正黑體", serif;
            --font-custom-style: "Times New Roman", Times, "Microsoft Jhenghei", "微軟正黑體", sans-serif;
        }
        .track-temple .btn {
            margin: .375rem;
            color: inherit;
            text-transform: uppercase;
            word-wrap: break-word;
            white-space: normal;
            cursor: pointer;
            border: 0;
            border-radius: .125rem;
            -webkit-box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
            box-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
            -webkit-transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, -webkit-box-shadow 0.15s ease-in-out;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, -webkit-box-shadow 0.15s ease-in-out;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out, -webkit-box-shadow 0.15s ease-in-out;
            padding: .84rem 2.14rem;
            font-size: .81rem;
        }
        .mr-2, .mx-2 {
            font-size: 1.25rem;
            margin-right: .5rem !important;
        }
        .track-temple .text-white {
            color: #fff !important;
        }
        .track-temple .font-weight-bold {
            font-weight: 500 !important;
        }
        .track-temple .bg-custom-primary {
            background-color: var(--color-schema-primary);
        }
        .fade {
            transition: opacity .15s linear;
        }
        .modal-open .modal {
            overflow-x: hidden;
            overflow-y: auto;
        }
        .modal {
            padding-right: 0 !important;
        }
        .modal {
            position: fixed;
            top: 120px;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 85%;
            overflow: hidden;
            outline: 0;
        }
        .modal-dialog {
            position: relative;
            width: auto;
            margin: .5rem;
            pointer-events: none;
        }

        .modal-header {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: start;
            align-items: flex-start;
            -ms-flex-pack: justify;
            justify-content: space-between;
            padding: 1rem 1rem;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: calc(.3rem - 1px);
            border-top-right-radius: calc(.3rem - 1px);
        }

        .modal .h5 {
            font-size: 1.25rem;
        }

        .modal-body {
            position: relative;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            padding: 1rem;
        }

        .w-100 {
            width: 100% !important;
        }

        .modal-dialog-scrollable .modal-body {
            overflow-y: auto;
        }

        .modal-footer {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            -ms-flex-align: center;
            align-items: center;
            -ms-flex-pack: end;
            justify-content: flex-end;
            padding: .75rem;
            border-top: 1px solid #dee2e6;
            border-bottom-right-radius: calc(.3rem - 1px);
            border-bottom-left-radius: calc(.3rem - 1px);
        }

        .track-temple .grid {
            display: grid;
        }

        .track-temple .grid-auto {
            grid-template-columns: 100%;
        }

        .pb-4, .py-4 {
            padding-bottom: 1.5rem !important;
        }

        .pt-4, .py-4 {
            padding-top: 1.5rem !important;
        }

        .pl-3, .px-3 {
            padding-left: 1rem !important;
        }

        .pr-3, .px-3 {
            padding-right: 1rem !important;
        }

        *, ::after, ::before {
            box-sizing: border-box;
        }

        .track-temple img {
            max-width: 100%;
            height: auto;
        }

        .modal-dialog .modal-content .modal-header {
            border-top-left-radius: .125rem;
            border-top-right-radius: .125rem;
        }

        .track-temple .gridLayout .listGroupItem {
            display: block;
            position: relative;
            width: 100%;
            height: auto;
            background: rgba(255, 255, 255, .5);
            backdrop-filter: blur(10px);
            border-radius: 12px;
            overflow: hidden;
        }

        .bg-transparent {
            background-color: transparent !important;
        }

        .track-temple .gridLayout {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(500px, 1fr));
            grid-gap: .75rem;
        }

        .modal-footer {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            -ms-flex-align: center;
            align-items: center;
            -ms-flex-pack: end;
            justify-content: flex-end;
            padding: .75rem;
            border-top: 1px solid #dee2e6;
            border-bottom-right-radius: calc(.3rem - 1px);
            border-bottom-left-radius: calc(.3rem - 1px);
        }

        .modal-dialog-scrollable .modal-footer, .modal-dialog-scrollable .modal-header {
            -ms-flex-negative: 0;
            flex-shrink: 0;
        }

        .modal-content {
            position: relative;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            pointer-events: auto;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid rgba(0, 0, 0, .2);
            border-radius: .3rem;
            outline: 0;
        }

        .modal-dialog-scrollable .modal-content {
            max-height: calc(100vh - 1rem);
            overflow: hidden;
        }

        .modal-dialog .modal-content {
            border: 0;
            border-radius: .125rem;
            -webkit-box-shadow: 0 5px 11px 0 rgba(0, 0, 0, 0.18), 0 4px 15px 0 rgba(0, 0, 0, 0.15);
            box-shadow: 0 5px 11px 0 rgba(0, 0, 0, 0.18), 0 4px 15px 0 rgba(0, 0, 0, 0.15);
        }

        .modal-dialog-centered.modal-dialog-scrollable .modal-content {
            max-height: none;
        }

        .modal-dialog-scrollable {
            display: -ms-flexbox;
            display: flex;
            max-height: calc(100% - 1rem);
        }

        .modal-dialog-centered {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: center;
            align-items: center;
            min-height: calc(100% - 1rem);
        }

            .modal-dialog-centered.modal-dialog-scrollable {
                -ms-flex-direction: column;
                flex-direction: column;
                -ms-flex-pack: center;
                justify-content: center;
                height: 100%;
            }

        .modal.fade .modal-dialog {
            transition: -webkit-transform .3s ease-out;
            transition: transform .3s ease-out;
            transition: transform .3s ease-out, -webkit-transform .3s ease-out;
            -webkit-transform: translate(0, -50px);
            transform: translate(0, -50px);
        }

        .modal.show .modal-dialog {
            -webkit-transform: none;
            transform: none;
        }

        .track-temple .rounded-12px {
            border-radius: 12px;
        }

        .close {
            float: right;
            font-size: 1.5rem;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            opacity: .5;
        }

        button.close {
            padding: 0;
            background-color: transparent;
            border: 0;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }

        .justify-content-between {
            -ms-flex-pack: justify !important;
            justify-content: space-between !important;
        }

        .flex-wrap {
            -ms-flex-wrap: wrap !important;
            flex-wrap: wrap !important;
        }

        .flex-row {
            -ms-flex-direction: row !important;
            flex-direction: row !important;
        }

        .d-flex {
            display: -ms-flexbox !important;
            display: flex !important;
        }

        #yt {
            width: 100%;
            height: 500px;
        }

        .checkedbox {
            vertical-align: middle;
            -webkit-transform: scale(1.2);
            -moz-transform: scale(1.2);
            -ms-transform: scale(1.2);
            transform: scale(1.2);
            -webkit-transform-origin: right;
            -moz-transform-origin: right;
            -ms-tranform-origin: right;
            transform-origin: right;
            height: 12px;
            width: 12px;
            margin-bottom: 4px;
            position: relative;
            border-radius: 2px;
        }
        
        .text_s input.checkedbox, .tel input.checkedbox {
            width: 12px;
            margin-left: 5px;
        }
        
        /* Toast 容器 ------------------------------------ */
        .toast {
            position: fixed;
            bottom: 20px; /* 距離底部 20px */
            left: 50%; /* 水平置中 */
            transform: translateX(-50%) translateY(100px);
            /* 初始往下隱藏 100px */
            background: rgba(0, 0, 0, 0.8); /* 半透明黑底 */
            color: #fff; /* 白字 */
            padding: 10px 20px; /* 內距 */
            border-radius: 4px; /* 圓角 */
            opacity: 0; /* 初始透明 */
            transition: transform .3s ease, opacity .3s ease; /* 進出場動畫 */
            z-index: 9999; /* 最上層 */
            box-sizing: border-box;
            max-width: calc(100% - 40px); /* 左右各留 20px 安全邊距 */
            overflow-wrap: break-word; /* 自動換行 */
        }

        /* Toast 顯示時 -------------------------------- */
        .toast.visible {
            opacity: 1;
            transform: translateX(-50%) translateY(0);
        }

        /* 大螢幕時限制最大寬度 ------------------------ */
        @media (min-width: 768px) {
            .toast {
                max-width: 300px;
            }
        }

        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }

            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }

            .content_a {
                font-size: 5vw;
            }

            .modal {
                top: 80px;
            }
            .MemAddress > div:first-child {
                width: 20%;
            }
        }

        @media only screen and (min-width: 576px) {
            .modal-dialog-centered {
                min-height: calc(100% - 3.5rem);
            }

            .modal-dialog-scrollable {
                max-height: calc(100% - 3.5rem);
            }

                .modal-dialog-scrollable .modal-content {
                    max-height: calc(100vh - 3.5rem);
                }

            .track-temple .grid-auto {
                grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
            }

            .track-temple .image-flex img:first-child {
                width: calc(35% - .5rem);
            }

            .track-temple .image-flex img:last-child {
                width: calc((100% - 35%) - .5rem);
            }

            .text_s input, .tel input, .mail input, .date input {
                width: 20vw;
            }

        }

        .track-temple .gap-x-2 {
            gap: .5rem;
        }
    </style>
    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>
    <script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "VideoObject",
      "name": "玉敕大樹朝天宮-靈寶活動",
      "description": "玉敕大樹朝天宮 銀同媽\n祈福法會，線上報名中\n法會日期: \n農曆: 113年09月18日\n國曆:113年10月20日",
      "thumbnailUrl": "https://img.youtube.com/vi/7k0i9T1f5BY/maxresdefault.jpg",
      "uploadDate": "2024-09-05T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/7k0i9T1f5BY",
      "contentUrl": "https://bobibobi.tw/Temples/templeService_lingbaolidou_ma.aspx"
    }
    </script>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
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
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
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
                    <li><a href="temple.aspx" title="合作宮廟">合作宮廟</a></li>
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=23" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                    <li>靈寶禮斗</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lingbaolidou_ma_2025.jpg?t=2589963" width="1160" height="550" alt="保必保庇提供玉敕大樹朝天宮靈寶禮斗線上報名服務" 
                        title="保必保庇提供玉敕大樹朝天宮靈寶禮斗線上報名服務" />
                </div>
                <h1 class="TempleName">玉敕大樹朝天宮</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2025/09/12 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2025/11/06 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>▌靈寶禮斗，消災解厄，植福延壽，祈安福醮</h2>
                            </div>
                            <div>
                                <div class="track-temple">
                                    <button type="button" class="d-lg-inline-flex btn bg-custom-primary d-flex justify-content-center align-items-center font-weight-bold text-white rounded-12px waves-effect waves-light" data-toggle="modal" data-target="#ModalContentShow" style="font-size: 1.25rem; letter-spacing: 1px;">
                                        <span class="mr-2">靈寶禮斗說明</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24">
                                            <path fill="currentColor" d="M11.7 18q-2.4-.125-4.05-1.85T6 12q0-2.5 1.75-4.25T12 6q2.425 0 4.15 1.65T18 11.7l-2.1-.625q-.325-1.35-1.4-2.212T12 8q-1.65 0-2.825 1.175T8 12q0 1.425.863 2.5t2.212 1.4zm1.2 3.95q-.225.05-.45.05H12q-2.075 0-3.9-.788t-3.175-2.137T2.788 15.9T2 12t.788-3.9t2.137-3.175T8.1 2.788T12 2t3.9.788t3.175 2.137T21.213 8.1T22 12v.45q0 .225-.05.45L20 12.3V12q0-3.35-2.325-5.675T12 4T6.325 6.325T4 12t2.325 5.675T12 20h.3zm7.625.55l-4.275-4.275L15 22l-3-10l10 3l-3.775 1.25l4.275 4.275z"></path>
                                        </svg></button>

                                    <div class="modal fade" id="ModalContentShow" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="z-index: 9999; display: none;" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document" style="max-width: 45rem; margin: 1.75rem auto;">
                                            <div class="modal-content rounded-12px">
                                                <div class="modal-header">
                                                    <h5 class="h5 modal-title title-font">靈寶禮斗說明
                                                    </h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" aria-hidden="true">
                                                            <path fill="#161616" d="M6.4 19L5 17.6l5.6-5.6L5 6.4L6.4 5l5.6 5.6L17.6 5L19 6.4L13.4 12l5.6 5.6l-1.4 1.4l-5.6-5.6z"></path>
                                                        </svg></button>
                                                </div>
                                                <div class="modal-body w-100">
                                                    <div class="gridLayout bg-transparent" style="grid-template-columns: 100%;">
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">誠聘台南府城延陵道壇吳政憲道長主法
                                                            </h5>
                                                            <img decoding="async" loading="lazy" width="800" height="600" src="images/temple/240813133322268732.png" alt="Image 1" class="img-fluid rounded-12px ignore">
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">點燈安斗誦經禮懺、祈安納福、元辰光彩消災解厄、賜福平安
                                                            </h5>
                                                            <p class="textContent mb-1">
                                                                靈寶禮斗是一種祈求自身本命元辰光彩消災解厄、祈福延壽之科儀故能消災、祈福、祛病、延生
                                                            </p>
                                                            <div class="grid grid-auto image-gallery gap-x-2 mb-3">
                                                                <img width="800" height="600" decoding="async" loading="lazy" src="images/temple/240813133323762291.png" class="img-fluid w-100 rounded-12px ignore" alt="Image 3"><img width="800" height="600" decoding="async" loading="lazy" src="images/temple/240813133324347697.png" class="img-fluid w-100 rounded-12px ignore" alt="Image 4">
                                                            </div>
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2" style="line-height: 32px;">燃點七盞斗燈以像斗真，修齋設醮，酌水獻花，祈禱眾真，禮拜斗真，則可以消災解厄，延生獲福。
                                                            </h5>
                                                            <div class="grid grid-auto image-gallery gap-x-2 mb-3">
                                                                <img width="600" height="800" decoding="async" loading="lazy" src="images/temple/240813174622506354.png" class="img-fluid rounded-12px ignore" alt="Image 2"><img width="600" height="800" decoding="async" loading="lazy" src="images/temple/240813174622996583.png" class="img-fluid rounded-12px ignore" alt="Image 3">
                                                            </div>
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">玉敕大樹朝天宮凌霄寶殿
                                                            </h5>
                                                            <img width="635" height="424" decoding="async" loading="lazy" src="images/temple/240813133325808022.jpg" class="d-block img-fluid rounded-12px w-100 ignore" alt="Image 5">
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">南斗六司延生星君掌【延壽賜福】
                                                            </h5>
                                                            <img width="640" height="427" decoding="async" loading="lazy" src="images/temple/240813133326248458.jpg" class="d-block img-fluid rounded-12px w-100 ignore" alt="Image 6">
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">北斗七元解厄星君掌【消災解厄】
                                                            </h5>
                                                            <p class="textContent mb-3">
                                                                故祈求南北斗星君 消災解厄、植福延壽、逢兇化吉、庇佑百泰。
                                                            </p>
                                                            <div class="d-flex flex-row flex-wrap justify-content-between image-flex gap-x-2">
                                                                <img width="600" height="800" decoding="async" loading="lazy" src="images/temple/240813133326673364.png" class="d-block relative img-fluid rounded-12px ignore" alt="Image 7"><img width="626" height="417" decoding="async" loading="lazy" src="images/temple/240813133327360580.jpg" class="d-block relative img-fluid rounded-12px ignore" alt="Image 8">
                                                            </div>
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">歡迎十方善信參加法會
                                                            </h5>
                                                            <p class="textContent mb-0">
                                                                同霑法喜、共悟聖恩、廣結福源、同享功德
                                                            </p>
                                                        </div>
                                                        <div class="listGroupItem py-4 px-3">
                                                            <h5 class="h5 title-font mb-2">法會功德主
                                                            </h5>
                                                            <p class="textContent mb-1">
                                                                <span class="block">護持法會的所有功德主均以疏文放榜稟奏上呈天曹，廻向功德，同霑法益。</span><span class="block">醮會經懺祝禱之功，超幽之德，為諸善信消災植福之法憑，其功德無限，功德主是一大福慧之良緣。</span>
                                                            </p>
                                                            <img width="800" height="600" decoding="async" loading="lazy" src="images/temple/240813133327778649.png" class="d-block img-fluid w-100 rounded-12px ignore" alt="Image 9">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn bg-custom-primary font-weight-bold text-white w-100 rounded-12px waves-effect waves-light" data-dismiss="modal" style="font-size: 20px;">關閉</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <h2>法會時間：國曆 114 年 11 月 08 日 ; 農曆 114 年 09 月 19 日 星期六</h2>
                                <br />
                                <%--<h2>靈寶禮斗-功德主($ 6,800)</h2><span id="lingbaolidou1" style="color:red" class="content_a" runat="server">(已額滿)</span>--%>
                                <h2>靈寶禮斗-隨喜功德主($ 1,000)</h2><span id="lingbaolidou2" style="color:red" class="content_a" runat="server">(已額滿)</span>
                                <h2>靈寶禮斗-消災解厄科儀($ 550)</h2><span id="lingbaolidou3" style="color:red" class="content_a" runat="server">(已額滿)</span>
                                <br />
                                <br />
                                <h2>購買說明</h2>
                                <iframe id="yt" src="https://www.youtube.com/embed/7k0i9T1f5BY?si=daU0l31f072UrgRN" title="靈寶禮斗購買說明" frameborder="0" ></iframe>
                            </div>
                        </div>

                        <uc3:SocialMedia runat="server" id="SocialMedia" />
                    </div>
                </div>

                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" maxlength="5" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>購買人信箱</label><input name="member_mail" type="text" class="" id="member_mail" placeholder="請輸入購買人信箱"/>
                        </div>
                        <div class="FormInput address">
                            <label>購買人地址</label>
                            <div class="MemAddress">
                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="member_zipcode" data-id="member_zipcode"></div>
                                <div data-role="county" data-style="addr-county required" data-name="member_county" data-id="member_county"></div>
                                <div data-role="district" data-style="addr-district required" data-name="member_district" data-id="member_district"></div>
                            </div>
                            <input name="member_address" type="text" class="required" id="member_address" placeholder="請輸入購買人地址" />
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span>（祈福人限填一位，每個靈寶禮斗對應一位祈福人。如需多位，請點選增加祈福人。）</span></div>
                                <div class="FormInput select">
                                    <label>活動項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value>請選擇</option>
                                        <%--<option value="靈寶禮斗-功德主">靈寶禮斗-功德主 $ 6,800</option>--%>
                                        <option value="靈寶禮斗-隨喜功德主">靈寶禮斗-隨喜功德主 $ 1,000</option>
                                        <option value="靈寶禮斗-消災解厄科儀">靈寶禮斗-消災解厄科儀 $ 550</option>
                                    </select>
                                </div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" maxlength="5" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                    <input type="checkbox" class="checkedbox" id="bless_copy_name_1" />
                                    <label for="bless_copy_name_1" id="bless_checkednamelabel_1" style="width: auto;">同購買人姓名</label>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
                                    <input type="checkbox" class="checkedbox" id="bless_copy_tel_1" />
                                    <label for="bless_copy_tel_1" id="bless_checkedtellabel_1" style="width: auto;">同購買人聯絡電話</label>
                                </div>
                                <div class="FormInput select">
                                    <label>性別</label>
                                    <select name="bless_sex_1" class="required" id="bless_sex_1">
                                        <option selected="selected" value="">請選擇</option>
                                        <option value="善男">善男</option>
                                        <option value="信女">信女</option>
                                    </select>
                                </div>
                                <div class="FormInput date">
                                    <label>農曆生日</label><input name="bless_birthday_1" type="text" class="datapicker required2" id="bless_birthday_1" placeholder="請選擇農曆生日或國曆生日二擇一" />
                                </div>
                                <div class="FormInput select">
                                    <label>閏月</label>
                                    <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                                        <option value="N">非閏月</option>

                                        <option value="Y">閏月</option>
                                    </select>
                                </div>
                                <div class="FormInput select">
                                    <label>農曆時辰</label>
                                    <select name="bless_birthtime_1" class="" id="bless_birthtime_1">
                                        <option value="吉">吉</option>

                                        <option value="子">子(23:00-01:00)</option>

                                        <option value="丑">丑(01:00-03:00)</option>

                                        <option value="寅">寅(03:00-05:00)</option>

                                        <option value="卯">卯(05:00-07:00)</option>

                                        <option value="辰">辰(07:00-09:00)</option>

                                        <option value="巳">巳(09:00-11:00)</option>

                                        <option value="午">午(11:00-13:00)</option>

                                        <option value="未">未(13:00-15:00)</option>

                                        <option value="申">申(15:00-17:00)</option>

                                        <option value="酉">酉(17:00-19:00)</option>

                                        <option value="戌">戌(19:00-21:00)</option>

                                        <option value="亥">亥(21:00-23:00)</option>
                                    </select>
                                </div>
                                <div class="FormInput date">
                                    <label>國曆生日</label><input name="bless_sbirth_1" type="text" class="datapicker required2" id="bless_sbirth_1" placeholder="請選擇國曆生日或農曆生日二擇一" />
                                </div>
                                <div class="FormInput select">
                                    <label>祈福人地址</label>
                                    <select name="bless_oversea_1" class="" id="bless_oversea_1">
                                        <option value="1">國內</option>

                                        <option value="2">國外</option>
                                    </select>
                                </div>
                                <div class="FormInput address">
                                    <label></label>
                                    <div class="CusAddress" id="bless_cusaddress_1">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county required4" data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district required4" data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="required" id="bless_address_1" placeholder="請輸入地址"/>
                                </div>
                                <div class="FormInput text_s">
                                    <label>備註</label><textarea name="bless_Remark_1" type="text" class="" id="bless_Remark_1" placeholder="請輸入問題內容"></textarea>
                                </div>
                            </li>

                        </ul>
                        <!--可複製的區塊 //end-->

                        <div class="FormAddList"><a href="javascript:addList();" title="增加祈福人">✚ 增加祈福人</a></div>

                        <div class="Notice">
                            <!--警告說明-->
                        </div>

                        <div class="FormButtom">
                            <div>
                                <input type="checkbox" id="checkedprivate" />
                                <label for="checkedprivate">本人同意
                                    <a href="PrivacyPolicy.aspx" target="_blank">隱私權政策</a>
                                    並已取得當事人同意，為「保必保庇線上宮廟服務平台」之所有交易行為，九九商通得基於
                                    <a href="PrivacyPolicy.aspx" target="_blank">隱私權政策</a>
                                    蒐集、處理及利用本人所提供之資料，並提供予合作廠商及服務宮廟。</label>
                            </div>
                            <input type="button" id="subBtn" class="subBtn" value="下一步"/>
                        </div>

                    </form>
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
    var aid = '<%=aid %>';
    var a = '<%=a %>';
    $(function () {
        $("header").addClass("active");

        if (!checkEndTime()) {
            alert('親愛的大德您好\n玉敕大樹朝天宮 2025靈寶禮斗活動已截止！！\n感謝您的支持, 謝謝!');
        }

        $("input[type='tel']").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        $("#bless_oversea_1").change(function () {
            if ($("#bless_oversea_1").val() == 1) {
                //alert("國內");
                $("#bless_cusaddress_1").show();
            }
            else {
                //alert("國外");
                $("#bless_cusaddress_1").hide();
            }
        });

        $("#bless_copy_name_1").change(function () {
            if ($("#bless_copy_name_1").is(':checked')) {
                //alert("選中同購買人姓名");
                var name = $("#member_name").val().trim();
                $("#bless_name_1").val(name);
            }
            else {
                //alert("取消同購買人姓名");
                $("#bless_name_1").val('');
            }
        });

        $("#bless_copy_tel_1").change(function () {
            if ($("#bless_copy_tel_1").is(':checked')) {
                //alert("選中同購買人電話");
                var name = $("#member_tel").val().trim();
                $("#bless_tel_1").val(name);
            }
            else {
                //alert("取消同購買人電話");
                $("#bless_tel_1").val('');
            }
        });

        if (aid != 0) {
            ac_loadServerMethod("editinfo", null, editinfo);
        }
    })
</script>
<!-----月曆外掛----->
<script type="text/javascript" src="js/jquery-ui.js"></script>
<link href="css/jquery-ui.css" rel="stylesheet" type="text/css"/>
<link href="css/jquery-ui.theme.css" rel="stylesheet" type="text/css"/>
<script>
    function dateSelect() {
        var dtNow = new Date();
        var maxY = (dtNow.getFullYear() - 1911);
        var minY = maxY + 50;
        $(".datapicker").datepicker({
            dayNames: ["", "", "", "",
                "", "", ""],
            dayNamesMin: ["", "", "", "",
                "", "", ""],
            dayNamesShort: ["", "", "", "",
                "", "", ""],
            changeMonth: true,
            changeYear: true,
            yearRange: "c-" + minY.toString() + ":c+" + maxY.toString(),
            minDate: "-" + minY.toString() + "y",
            maxDate: "-1d",
            showMonthAfterYear: true,
            dateFormat: "yy/mm/dd",
            beforeShow: function (input) {
                // 禁止用户手动输入
                $(input).prop("readonly", true);
                $(".ui-datepicker-calendar thead tr:eq(0)").hide();
            }
        });
    }
    $(function () {
        dateSelect();
    });
</script>

<!-----縣市外掛----->
<!--<script type="text/javascript" src="js/twzipcode.js"></script>-->
<script type="text/javascript" src="js/jquery.twzipcode.min.js"></script>
<script>
    $('.CusAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });
    $('.MemAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });
</script>

<!-----增減祈福人----->
<script>
    var originalField = $('.InputGroup > li').first().clone();

    //增加
    function addList() {
        var lastblessNum = parseInt($('.InputGroup > li').last().attr('bless-id')) + 1;
        console.log(lastblessNum);

        if (lastblessNum <= 6) {
            var newField = originalField.clone();
            newField.find('input, select').val('');

            //若有地址的話，將套件還原為預設狀態
            newField.find('.addr-zip, .addr-county, .addr-district').remove();

            $('.InputGroup > li:last').after(newField);

            //將所有的ID更新為新的值
            $('.InputGroup > li:last').attr('bless-id', lastblessNum);


            //更新所有動態產生的ID編號  
            $('.InputGroup > li:last').find('div').each(function (index) {
                var originalId = $(this).attr('id');
                if (originalId != null) {
                    var newId = originalId.slice(0, -1) + lastblessNum;
                    $(this).attr('id', newId);
                    $(this).attr('name', newId);

                }
            });

            $('.InputGroup > li:last').find('label').each(function (index) {
                var originalId = $(this).attr('id');
                if (originalId != null) {
                    var newId = originalId.slice(0, -1) + lastblessNum;

                    if (newId.indexOf('checkednamelabel') >= 0) {
                        $(this).attr('id', newId);
                        $(this).attr('name', newId);
                        $(this).attr('for', 'bless_copy_name_' + lastblessNum);
                    }

                    if (newId.indexOf('checkedtellabel') >= 0) {
                        $(this).attr('id', newId);
                        $(this).attr('name', newId);
                        $(this).attr('for', 'bless_copy_tel_' + lastblessNum);
                    }

                }
            });

            $('.InputGroup > li:last').find('input').each(function (index) {
                var originalId = $(this).attr('id');
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);

                if (newId.indexOf('copy_name') >= 0) {
                    $("#" + newId).change(function () {
                        if ($("#bless_copy_name_" + lastblessNum).is(':checked')) {
                            //alert("選中同購買人姓名");
                            var name = $("#member_name").val().trim();
                            $("#bless_name_" + lastblessNum).val(name);
                        }
                        else {
                            //alert("取消同購買人姓名");
                            $("#bless_name_" + lastblessNum).val('');
                        }
                    });
                }

                if (newId.indexOf('copy_tel') >= 0) {
                    $("#" + newId).change(function () {
                        if ($("#bless_copy_tel_" + lastblessNum).is(':checked')) {
                            //alert("選中同購買人電話");
                            var name = $("#member_tel").val().trim();
                            $("#bless_tel_" + lastblessNum).val(name);
                        }
                        else {
                            //alert("取消同購買人電話");
                            $("#bless_tel_" + lastblessNum).val('');
                        }
                    });
                }

                $("input[type='tel']").on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^\d].+/, ""));
                    if ((event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });

            });
            $('.InputGroup > li:last').find('select').each(function (index) {
                var originalId = $(this).attr('id');
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);

                if (newId.indexOf('service') >= 0) {
                    $("#" + newId).val('靈寶禮斗-隨喜功德主');
                }

                if (newId.indexOf('leapMonth') >= 0) {
                    $("#" + newId).val('N');
                }

                if (newId.indexOf('birthtime') >= 0) {
                    $("#" + newId).val('吉');
                }

                if (newId.indexOf('oversea') >= 0) {
                    $("#" + newId).val('1');

                    $("#" + newId).change(function () {
                        var oversea = $(this).val();
                        if (oversea == 1) {
                            //alert("國內");
                            $("#bless_cusaddress_" + lastblessNum).show();
                        }
                        else {
                            //alert("國外");
                            $("#bless_cusaddress_" + lastblessNum).hide();
                        }
                    });
                }
            });

            $('.InputGroup > li:last').find('textarea').each(function (index) {
                var originalId = $(this).attr('id');
                if (originalId != null) {
                    var newId = originalId.slice(0, -1) + lastblessNum;
                    $(this).attr('id', newId);
                    $(this).attr('name', newId);

                }
            });

            $('.InputGroup > li:last .CusAddress').find('div[data-role]').each(function (index) {
                var originalId = $(this).attr('data-id');
                var originalName = $(this).attr('data-name');
                var newId = originalId.slice(0, -1) + lastblessNum;
                var newName = originalName.slice(0, -1) + lastblessNum;
                $(this).attr('data-id', newId);
                $(this).attr('data-name', newId);
            });


            $('.DeletData').addClass("active");

            dateSelect();//有日期選擇時使用
            $('.CusAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
        }
        else {
            alert("祈福人資料最多六位！" + $('.InputGroup > li').last().attr('bless-id'));
        }
    }

    //刪除
    $(".InputGroup").on("click", ".deletList", function () {
        $(this).parents('li').remove();
        var liCount = $('.InputGroup li').length;
        if (liCount == 1) {
            $('.DeletData').removeClass("active");
        }
    })
</script>

<!-----必填欄位檢查----->
<script>
    // 工具：抓出所有 .required.unfilled 的 label 名稱
    function getMissingRequiredNames() {
        return $('.required.unfilled').map(function () {
            const $input = $(this);
            let $grp = $input.closest('.FormInput');
            // 嘗試讀同層 label
            let labelText = $grp.find('label').first().text().trim();
            if (!labelText) {
                // 如果是地址那種沒有 label (e.g. 祈福人地址)，就往上找前一個有 label 的群組
                $grp.prevAll('.FormInput').each(function () {
                    const txt = $(this).find('label').first().text().trim();
                    if (txt) {
                        labelText = txt;
                        return false;  // break
                    }
                });
            }
            return labelText.replace(/：|:/g, '');
        }).get();
    }

    // 顯示 Toast，3 秒後自動消失，並在關閉時執行 callback
    function showToast(msg, callback) {
        const $t = $(`<div class="toast">${msg}</div>`)
            .appendTo('body');
        // 進場
        requestAnimationFrame(() => $t.addClass('visible'));
        // 3 秒後退場並呼叫 callback
        setTimeout(() => {
            $t.removeClass('visible');
            $t.one('transitionend', () => {
                $t.remove();
                if (typeof callback === 'function') callback();
            });
        }, 1000);
    }

    // Toast 顯示完畢後再捲動＋聚焦
    function showToastAndFocus($el, msg) {
        showToast(msg, () => {
            // 等 toast 完全隱藏之後再聚焦，不搶畫面
            $(".Notice").text(msg).addClass("active");
            $el.addClass("unfilled");
            $el[0].scrollIntoView({ block: 'center' });
            $el.focus();
        });
    }

    function clearError($elem) {
        $elem.removeClass("unfilled");
    }

    function clearNotice() {
        $(".Notice").removeClass("active").text("");
    }

    // 通用驗證器清單
    const validators = [
        {
            // 購買人電話：非空 + 格式
            selector: "#member_tel",
            checks: [
                { fn: v => v !== "", msg: "購買人電話不能為空。" },
                { fn: Isphone, msg: "購買人電話格式錯誤。" }
            ]
        },
        {
            // 購買人信箱：非空 + 格式
            selector: "#member_mail",
            checks: [
                { fn: v => v !== "", msg: "購買人信箱不能為空。" },
                { fn: IsEmail, msg: "購買人信箱格式錯誤。" }
            ]
        },
        {
            // 購買人區域：有縣市就一定要有區域
            selector: "#member_district",
            checks: [
                {
                    fn: v => {
                        const county = $("#member_county").val();
                        if (!county) return false; // 縣市未選
                        return (v || "").trim() !== ""; // 區域必填
                    },
                    msg: "購買人地址未完整選擇，請重新選擇縣市與區域。"
                }
            ]
        },
        {
            // 所有通用必填欄位
            selector: ".required",
            checks: [{ fn: v => (v || "").trim() !== "", msg: "上面有欄位未填寫。" }]
        }
    ];

    // 針對每一位祈福人做驗證
    function validateBless(i) {
        const $li = $(`.InputGroup > li[bless-id=${i}]`);
        // 電話
        const tel = $li.find(`#bless_tel_${i}`).val().trim();
        if (!tel) {
            showToastAndFocus($li.find(`#bless_tel_${i}`), "祈福人電話不能為空。");
            return false;
        }
        if (!Isphone(tel)) {
            showToastAndFocus($li.find(`#bless_tel_${i}`), "祈福人電話格式錯誤。");
            return false;
        }
        clearError($li.find(`#bless_tel_${i}`));

        // 若國內才要檢查縣市 & 區域
        if ($li.find(`#bless_oversea_${i}`).val() === "1") {
            const county = $li.find(`#bless_county_${i}`).val();
            if (!county) {
                showToastAndFocus($li.find(`#bless_county_${i}`), "祈福人地址 縣市為空，請重新選擇縣市。");
                return false;
            }
            clearError($li.find(`#bless_county_${i}`));

            const district = $li.find(`#bless_district_${i}`).val();
            if (!district) {
                showToastAndFocus($li.find(`#bless_district_${i}`), "祈福人地址 區域為空，請重新選擇區域。");
                return false;
            }
            clearError($li.find(`#bless_district_${i}`));
        }

        // 農曆/國曆生日二擇一
        const birth = $li.find(`#bless_birthday_${i}`).val();
        const sbirth = $li.find(`#bless_sbirth_${i}`).val();
        if (!birth && !sbirth) {
            showToastAndFocus($li.find(".required2"), "請選擇農曆或國曆生日其中一項。");
            return false;
        }
        clearError($li.find(".required2"));

        return true;
    }

    // 回到上一頁後若選過縣市但區域為空，強制清空縣市
    $(window).on("pageshow", function (e) {
        // 1. 購買人：縣市有、區域空 → 清空縣市
        const memberCounty = $("#member_county").val();
        const memberDistrict = $("#member_district").val();
        if (memberCounty && !memberDistrict) {
            $("#member_county").val("");
        }

        // 2. 祈福人：動態 N 個
        $(".InputGroup > li[bless-id]").each(function () {
            const $li = $(this);
            const id = $li.attr("bless-id");              // e.g. "1", "2", ...
            const $county = $li.find(`#bless_county_${id}`);
            const $district = $li.find(`#bless_district_${id}`);

            // 如果選了「國內」才需檢查
            if ($li.find(`#bless_oversea_${id}`).val() === "1") {
                if ($county.val() && !$district.val()) {
                    // 清空縣市，迫使使用者重選才會帶出新的區域
                    $county.val("");
                }
            }
        });
    });

    $("#subBtn").on("click", function () {
        // 先把前一次的狀態清掉
        clearNotice();
        $('.required').each((_, el) => clearError($(el)));

        // 1. 先跑通用 validators，但對 .required rule 不馬上跳出，只標記 .unfilled
        for (const rule of validators) {
            const $eles = $(rule.selector);
            for (let i = 0; i < $eles.length; i++) {
                const $el = $eles.eq(i);
                const val = $el.val();
                clearError($el);

                for (const check of rule.checks) {
                    if (!check.fn(val)) {
                        // 標記錯誤欄位
                        $el.addClass('unfilled');
                        // 如果是「非 .required」的 rule，就立刻提示並 return
                        if (rule.selector !== '.required') {
                            showToastAndFocus($el, check.msg);
                            return;
                        }
                        // 如果是 .required 這支，就只標記，繼續跑完所有 required
                    }
                }
            }
        }

        // 2. 全部通用檢查後，看看還有哪些 .required 還是 unfilled
        const missing = getMissingRequiredNames();
        if (missing.length) {
            // 去重、組字串
            const uniq = [...new Set(missing)];
            const msg = uniq.join('、') + ' 未填寫';
            // 聚焦到第一個錯誤欄位
            const $first = $('.required.unfilled').first();
            showToastAndFocus($first, msg);
            return;
        }

        // 3. 驗證所有祈福人
        const lastId = Number($('.InputGroup > li').last().attr('bless-id') || 0);
        for (let i = 1; i <= lastId; i++) {
            if (!validateBless(i)) {
                return;
            }
        }

        // 4. 隱私權同意
        if (!$("#checkedprivate").is(":checked")) {
            showToastAndFocus($("#checkedprivate"), "請勾選同意隱私權政策。");
            return;
        }

        // 5. 全部通過，送出
        console.log("所有欄位都已填寫正確，準備送出");
        // 如果活動時間判斷...
        if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
            if (checkEndTime()) {
                gotoChecked_ma();
            } else {
                alert('玉敕大樹朝天宮 2025靈寶禮斗活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('玉敕大樹朝天宮 2025靈寶禮斗活動尚未開始！');
            location = 'https://bobibobi.tw/Temples/temple.aspx';
        }
    })

    //導向確認資料頁面
    function gotochecked(res) {
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            if (res.redirect) {
                window.location = res.redirect;
            }
        } else {
            alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
        }
    }

    //更新之前輸入的資料
    function editinfo(res) {
        var index = 1;
        if (res.StatusCode == 1) {
            for (var i = 1; i < res.listcount; i++) {
                addList();
            }

            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);
            $("#member_mail").val(res.AppEmail);
            $("#member_county").val(res.AppCounty).trigger("change");
            $("#member_district").val(res.Appdist).trigger("change");
            $("#member_address").val(res.AppAddr);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);
                    //$("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    //$("#bless_sBirth_" + index).val(item.sBirth);
                    $("#bless_oversea_" + index).val(item.oversea).trigger("change");
                    if (item.oversea == 1) {
                        $("#bless_cusaddress_" + index).show();
                        $("#bless_county_" + index).val(item.County).trigger("change");
                        $("#bless_district_" + index).val(item.dist).trigger("change");
                    }
                    else {
                        $("#bless_cusaddress_" + index).hide();
                    }
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_service_" + index).val(item.LingbaolidouString);
                    $("#bless_Remark_" + index).val(item.Remark);

                    index++;
                });
            }

        }
    }

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

    function gotoChecked_ma() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                                                      //購買人姓名
        Appmobile = $("#member_tel").val();                                                     //購買人電話
        AppEmail = $("#member_mail").val();                                                     //購買人信箱
        AppzipCode = $("#member_zipcode").val();                                                //購買人郵遞區號
        Appcounty = $("select[name='member_county']").val();                                    //購買人縣市
        Appdist = $("select[name='member_district']").val();                                    //購買人區域
        Appaddr = $("#member_address").val();                                                   //購買人部分地址

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        LingbaolidouString_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val().trim());                                          //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                         //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                            //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                     //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                                //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                                //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val().trim());                                      //祈福人國曆生日
            oversea_Tag.push($("#bless_oversea_" + i).val());                                           //國內-1 國外-2

            if ($("#bless_oversea_" + i).val() == "1") {
                zipCode_Tag.push($("#bless_zipcode_" + i).val().trim());                                //祈福人郵遞區號
                county_Tag.push($("select[name='bless_county_" + i + "']").val().trim());               //祈福人縣市
                dist_Tag.push($("select[name='bless_district_" + i + "']").val().trim());               //祈福人區域
            }
            else {
                zipCode_Tag.push("0");
                county_Tag.push("");
                dist_Tag.push("");
            }
            addr_Tag.push($("#bless_address_" + i).val().trim());                                       //祈福人部分地址
            remark_Tag.push($("#bless_Remark_" + i).val());                                             //備註
            LingbaolidouString_Tag.push($("#bless_service_" + i).val());                                //服務項目
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            AppEmail: AppEmail,
            AppzipCode: AppzipCode,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
            LingbaolidouString_Tag: JSON.stringify(LingbaolidouString_Tag),
            listcount: listcount
        };

        hasTextArea = true;
        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkEndTime() {
        var startTime = new Date();
        var endTime = $("#endTime").text();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
    }

    function checkedStartTime() {
        var endTime = new Date();
        var startTime = $("#startTime").text();
        if (Date.parse(endTime).valueOf() >= Date.parse(startTime).valueOf()) {
            return true;
        }
        return false;
    }
</script>
