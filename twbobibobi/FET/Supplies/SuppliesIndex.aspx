<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuppliesIndex.aspx.cs" Inherits="Temple.FET.Supplies.SuppliesIndex" %>

<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="shortcut icon" href="favicon.ico" />
<meta content="minimum-scale=1,initial-scale=1,width=device-width,shrink-to-fit=no" name="viewport" />
<link href="https://fonts.googleapis.com/css?family=Noto+Sans+TC:wght@400;500;600;700&family=Roboto:300,400,500,600,700&amp;display=swap" rel="stylesheet" />
<title>企業補財庫服務</title>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.css" />
<link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css?t=15963258" />
<script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
     <script type="text/javascript" src="js/bootstrap.min.js"></script>
<meta name="keywords" content="" />
<meta name="description" content="" />
    <style type="text/css">
        .track-temple {
            display: block;
            position: relative;
            width: 100%;
            height: auto;
            line-height: normal;
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
            top: 40px;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 90%;
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
            margin: 0;
            padding: 0;
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
                top: 25px;
                height: 77%;
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
        }

        .track-temple .gap-x-2 {
            gap: .5rem;
        }
    </style>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'G-4YWFRTFCTT');
    </script>
</head>
<body class="">
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
<!--Simple Header-->
<div class="simple-header"> <a href="index.html" class="simple-header-logo"><img src="images/fetnet-logo.png" alt="FetnetLogo" class="simple-logo-img"></a> </div>
<!--Simple Header End-->
<div class="main">
  <section class="kv">
    <div class="inside-container">
      <h1>企業補財庫服務
                                <div class="track-temple EventServiceContent">
                                    <button type="button" class="d-lg-inline-flex btn bg-custom-primary d-flex justify-content-center align-items-center font-weight-bold text-white rounded-12px waves-effect waves-light" data-toggle="modal" data-target="#ModalContentShow" style="font-size: 1.25rem; letter-spacing: 1px;">
                                        <span class="mr-2">企業補財庫說明</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24">
                                            <path fill="currentColor" d="M11.7 18q-2.4-.125-4.05-1.85T6 12q0-2.5 1.75-4.25T12 6q2.425 0 4.15 1.65T18 11.7l-2.1-.625q-.325-1.35-1.4-2.212T12 8q-1.65 0-2.825 1.175T8 12q0 1.425.863 2.5t2.212 1.4zm1.2 3.95q-.225.05-.45.05H12q-2.075 0-3.9-.788t-3.175-2.137T2.788 15.9T2 12t.788-3.9t2.137-3.175T8.1 2.788T12 2t3.9.788t3.175 2.137T21.213 8.1T22 12v.45q0 .225-.05.45L20 12.3V12q0-3.35-2.325-5.675T12 4T6.325 6.325T4 12t2.325 5.675T12 20h.3zm7.625.55l-4.275-4.275L15 22l-3-10l10 3l-3.775 1.25l4.275 4.275z"></path>
                                        </svg></button>

                                    <div class="modal fade" id="ModalContentShow" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="z-index: 9999; display: none;" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document" style="max-width: 45rem; margin: 1.75rem auto;">
                                            <div class="modal-content rounded-12px">
                                                <div class="modal-header">
                                                    <h5 class="h5 modal-title title-font">企業補財庫說明
                                                    </h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" aria-hidden="true">
                                                            <path fill="#161616" d="M6.4 19L5 17.6l5.6-5.6L5 6.4L6.4 5l5.6 5.6L17.6 5L19 6.4L13.4 12l5.6 5.6l-1.4 1.4l-5.6-5.6z"></path>
                                                        </svg></button>
                                                </div>
                                                <div class="modal-body w-100">
                                                    <div class="gridLayout bg-transparent" style="grid-template-columns: 100%;">
                                                        <div class="listGroupItem py-4 px-3">
                                                            <img decoding="async" loading="lazy" width="800" height="600" src="https://bobibobi.tw/Temples/images/temple/supplies3_wu.jpg" alt="Image 1" class="img-fluid rounded-12px ignore" />
                                                            <h5 class="h5 title-font mb-2">企業補財庫 X 北港武德宮</h5>
                                                            <h5 class="h5 title-font mb-2">活動內容：</h5>
                                                            <p class="">
                                                                企業補庫免煩惱,<span style="color: red;">遠傳電信</span>獨家與全臺最大財神廟<span style="color: red;">『北港武德宮』</span>合作，推出企業補財庫，提供   月月補庫2次(初一、十五各一次)，讓您在打拼事業的同時，有如神助~業績節節高升!!
                                                            </p>
                                                            <h5 class="h5 title-font mb-2">參加辦法：</h5>
                                                            <p>
                                                                只要在下方填好公司資料 or 負責人資料，完成線上報名，我方會將報名資料傳給北港武德宮，廟方將會在每月初一與十五幫貴公司補財庫，並將寄出感謝狀給予貴公司。(補財庫費用每月1300元）
                                                            </p>
                                                            <h5 class="h5 title-font mb-2">注意事項：</h5>
                                                            <p>1.	付費只能使用遠傳電信帳單，費用為每月1300元</p>
                                                            <p>2.	此服務為訂閱式服務，訂閱後將每個月從遠傳電信帳單中收取費用</p>
                                                            <p>3.	如欲取消請提早一個月來電取消，客服電話：04-36092299</p>
                                                            <p>4.	每月26號(農曆)前報名，補財庫於次月農曆初一開始第一次，十五第二次</p>
                                                            <p>5.	訂閱後每月26號(農曆)收費，收費成功才會將資料給予北港武德宮進行次月的補財庫服務</p>
                                                            <p>6.	收費失敗將有專人撥打聯繫電話聯絡，如聯繫不上將視為取消服務</p>
                                                            <p>7.	通訊地址請留可收信地址，每月完成補財庫將會寄送感謝狀</p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn bg-custom-primary font-weight-bold text-white w-100 rounded-12px waves-effect waves-light" data-dismiss="modal" style="font-size: 20px;">關閉</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div></h1>
      
      <div class="kv-script">
        <p>購買說明</p>
        <ul>
          <li>1.請於下方填寫購買者與被祈福者完整資料</li>
          <li>2.企業補財庫資訊將於付款後提供給廟方進行祈福作業</li>
          <li>3.企業補財庫時間及物品寄送:將依廟方作業進行,完成祈福後相關通知將寄到收件地址</li>
          <li>4.客服聯絡: tp@fareastone.com.tw 將盡快於一週內回覆</li>
          <li>5.付款前請確認網路訊號是否良好，以及交易流程中請勿關閉網頁，以免交易失敗</li>
          <li>6.此服務為訂閱式服務，訂閱後將每個月從遠傳電信帳單中收取費用</li>
          <li>7.每月26號(農曆)前報名，補財庫於次月農曆初一開始第一次，十五第二次</li>
          <li>8.訂閱後每月26號(農曆)收費，收費成功才會將資料給予北港武德宮進行次月的補財庫服務</li>
          <li>9.宗教所進行事宜沒有鑑賞期及退換貨服務，若下單有任何疑問請致電進行確認。</li>
        </ul>
      </div>
    </div>
  </section>
  <section class="form-area ribbon-bg">
    <div class="container-inside">
      <div class="area-title">
        <h2>購買者資料</h2>
      </div>
      <div class="form-list-group">
        <div class="">
          <div class="grid-area form-group-gap">
            <div class="long-grid">
              <div class="form-group">
                <label for="input-buyer" class="">購買者</label>
                <div class="text-input">
                  <input id="member_name" name="member_name" type="text" placeholder="請輸入姓名" class="required" aria-label="請輸入姓名" maxlength="200" value="" />
                </div>
              </div>
            </div>
            <div class="long-grid">
              <div class="form-group">
                <label for="input-buyer-mobile" class="">購買者手機門號</label>
                <div class="text-input">
                  <input id="member_tel" name="member_tel" type="text" placeholder="請輸入手機號碼" class="required" aria-label="請輸入手機號碼" maxlength="200" value="" />
                </div>
              </div>
            </div>
            <div class="long-grid">
              <div class="form-group">
                <label for="input-buyer-mobile" class="">購買者Email</label>
                <div class="text-input">
                  <input id="member_email" name="member_email" type="text" placeholder="請輸入購買者Email(必填)" class="required" aria-label="請輸入購買者Email(必填)" maxlength="200" value="" />
                        <span style="color: red;">北港武德宮 推行"無紙功德"環保理念" 原紙本感謝狀之提供改為 Email提供電子感謝狀。</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>          
    <div class="container-inside">
      <div class="area-title">
        <h2>被祈福者資料填寫</h2>
      </div>
      <div class="form-list-group">
        <div class="people">
          <div class="people-area">
            <p class="area-title-script mb10">選擇服務項目</p>
            <div class="checkbox-group slider-area">
              <label>
              <input type="checkbox" name="Group1" value="企業補財庫 1300元/月" id="Group1_0" onclick="return  false;" checked="checked" />
              <div class="radio-content">
                <div class="radio-content-inner">
                  <div class="radio-content-txt">   企業補財庫 1300元/月</div>
                </div>
              </div>
              </label>
            </div>
            <div class="page2">
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者姓名</label>
                    <div class="text-input">
                      <input id="bless_name_1" name="bless_name_1" type="text" placeholder="請輸入負責人姓名or公司名"class="required" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
                <div class="long-grid">
                  <div class="form-group">
                    <label for="sex" class="">性別</label>
                    <div class="text-input">
                      <div class="radio-group2">
                        <label>
                        <input name="sex" type="radio" id="Group2_0" value="善男" checked>
                        <div class="radio-content">
                          <div class="radio-content-inner">
                            <div class="radio-content-txt">善男</div>
                          </div>
                        </div>
                        </label>
                        <label>
                        <input type="radio" name="sex" value="信女" id="Group2_1">
                        <div class="radio-content">
                          <div class="radio-content-inner">
                            <div class="radio-content-txt">信女</div>
                          </div>
                        </div>
                        </label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者電話</label>
                    <div class="text-input">
                      <input id="bless_tel_1" name="bless_tel_1" type="text" placeholder="請輸入祈福人聯絡電話"class="required" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group-gap mgap30">
                <div class="long-grid gap10">
                  <div class="form-group">
                    <label for="birthday" class="mb10">被祈福者農曆生日 (民國年)</label>
                  </div>
                </div>
                <div class="form-group calendar">
                  <div class="text-input">
                    <div class="grid-area">
                      <div class="short-grid">
                        <input id="input-birthday-year" type="text" placeholder="民國年" class="required" aria-label="民國年" maxlength="200" value="" />
                      </div>
                      <div class="short-grid">
                        <select id="input-birthday-month">
                          <option>1</option>
                          <option>2</option>
                          <option>3</option>
                          <option>4</option>
                          <option>5</option>
                          <option>6</option>
                          <option>7</option>
                          <option>8</option>
                          <option>9</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                        </select>
                      </div>
                      <div class="short-grid">
                        <select id="input-birthday-day">
                          <option>1</option>
                          <option>2</option>
                          <option>3</option>
                          <option>4</option>
                          <option>5</option>
                          <option>6</option>
                          <option>7</option>
                          <option>8</option>
                          <option>9</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                          <option>24</option>
                          <option>25</option>
                          <option>26</option>
                          <option>27</option>
                          <option>28</option>
                          <option>29</option>
                          <option>30</option>
                          <option>31</option>
                        </select>
                      </div>
                    </div>
                  </div>
                </div>
                <p class="sp-txt">※ 民國年 = 西元年 - 1911 </p>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">閏月</label>
                      <div class="long-grid">
                          <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                              <option value="N">非閏月</option>

                              <option value="Y">閏月</option>
                          </select>
                      </div>
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">農曆時辰</label>
                      <div class="long-grid">
                        <select id="input-birthday-time">
                            <option>吉</option>

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
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者市話</label>
                    <div class="text-input">
                      <input id="bless_homenum_1" name="bless_homenum_1" type="text" placeholder="請輸入祈福人市話(可不填)" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者Email</label>
                    <div class="text-input">
                      <input id="bless_email_1" name="bless_email_1" type="text" placeholder="請輸入祈福人Email(可不填)" class="" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group-gap mgap20">
                <div class="long-grid gap10">
                  <div class="form-group">
                    <label for="abroad" class="mb10">被祈福者居住地址</label>
                  </div>
                </div>
                <div class="custom-area" style="display:block;">
                  <div class="form-group">
                    <div class="text-input">
                        <div class="grid-area CusAddress">
                            <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                            <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                            <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                            <div class="long-grid">
                                <input id="bless_address_1" name="bless_address_1" type="text" placeholder="請輸入地址" aria-label="請輸入地址" maxlength="200" value="" class="mw">
                            </div>
                        </div>
                    </div>
                  </div>
                </div>
              </div>
                
                        <div class="Notice">
                            <!--警告說明-->
                        </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</div>
<div class="steps-bar">
  <div class="steps-bar-container">
    <div class="steps-bar-preview">
      <div class="fui-container">
        <div class="fui-cart-container">
          <div class="column">
            <div class="steps-area">
              <div class="step-item step-active"><span class="text">輸入資料</span></div>
              <div class="step-item"><span class="text">付款</span></div>
            </div>
          </div>
          <div class="column">
            <div class="button-area">
              <button class="button-submit subBtn" id="subBtn"><span class="text">下一步</span></button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<!--Simple Footer-->
<div class="simple-footer">
  <div class="simple-footer-container">
    <div class="simple-footer-logo"> <a href="index.html"><img src="images/fetnet-footer-logo.png" alt="FetnetLogo" class="simple-logo-img"></a></div>
    <div class="simple-footer-info">遠傳電信版權所有 © Far EasTone<span class="simple-footer-txt">Telecommunications Co., Ltd</span></div>
  </div>
</div>
<!--Simple Footer End--> 

    
<script type="text/javascript" src="js/jquery-ui.js"></script>
<link href="css/jquery-ui.css" rel="stylesheet" type="text/css"/>
<link href="css/jquery-ui.theme.css" rel="stylesheet" type="text/css"/>
<!-----顯示選單----->
<script>
    var aid = '<%=aid %>';
    var a = '<%=a %>';
    $(function () {
        if (aid != 0) {
            ac_loadServerMethod("editinfo", null, editinfo);
        }
    })
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
</script>
    
<!-----必填欄位檢查----->
<script>
    var regex = "^民國\\d{2,3}年(0?[1-9]|1[012])月(0?[1-9]|[12][0-9]|3[01])日$";  // 民國日期格式
    $("#subBtn").on("click", function () {
        var isValid = true;
        var isBirth = true;

        var value = $("#member_tel").val().trim();
        var value_email = $("#member_email").val().trim();

        if (value == "") {
            $(".Notice").text("購買人電話不能為空。");
            $(".Notice").addClass("active");
            $("#member_tel").addClass('unfilled');
            //$("#member_tel").focus();
        }
        else if (!Isphone(value)) {
            $(".Notice").text("購買人電話格式錯誤。");
            $(".Notice").addClass("active");
            $("#member_tel").addClass('unfilled');
            //$("#member_tel").focus();
        }
        else if (value_email == "") {
            $(".Notice").text("購買人Email不能為空。");
            $(".Notice").addClass("active");
            $("#member_email").addClass('unfilled');
            //$("#member_email").focus();
        }
        else if (!IsEmail(value_email)) {
            $(".Notice").text("購買人Email格式錯誤。");
            $(".Notice").addClass("active");
            $("#member_email").addClass('unfilled');
            //$("#member_email").focus();
        }
        else {
            var today = new Date();
            var ss = $("#input-birthday-year").val().trim();
            var lyear = (today.getFullYear() - 1911)
            if ($("#input-birthday-year").val() == "" || ss > lyear) {
                isValid = false;
                isBirth = false;
                $(this).addClass('unfilled');
            }
            else {
                $(this).removeClass('unfilled');
            }

            if (isBirth) {
                // 遍歷每個必填欄位
                $('.required').each(function () {
                    var value = $.trim($(this).val());
                    var text = this;
                    if (value === '' || value === null) {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            if (isValid) {
                // 所有欄位都已填寫
                console.log('所有欄位都已填寫');
                $(".Notice").removeClass("active");

                gotoChecked_wu();
            } else {
                // 在這裡可以進行表單提交或其他相關處理
                // 有欄位未填寫
                if (isBirth) {
                    $(".Notice").text("請檢查上方欄位是否都已填寫。");
                    $(".Notice").addClass("active");
                }
                else {
                    $(".Notice").text("請檢查上方生日欄位格式是否正確。正確格式：民國xxx年");
                    $(".Notice").addClass("active");
                    $("#input-birthday-year").focus();
                }
            }
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
            if (res.OldUser == 1) {
                alert("此購買人電話已註冊過，將會每個月進行扣款。如果有疑問，請洽客服。");
            }
            else {
                alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
            }
        }
    }



    //更新之前輸入的資料
    function editinfo(res) {
        var index = 1;
        if (res.StatusCode == 1) {
            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);
            $("#member_email").val(res.AppEmail);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);

                    if (item.Sex == "善男") {
                        $("#Group2_0").attr("checked", true);
                    }
                    else {
                        $("#Group2_1").attr("checked", true);
                    }

                    $("#input-birthday-year").val(res.year);
                    $("#input-birthday-month").val(res.month);
                    $("#input-birthday-day").val(res.day);


                    $("#input-birthday-time").val(item.BirthTime);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_homenum_" + index).val(item.Homenum);
                    $("#bless_email_" + index).val(item.Email);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_Remark_" + index).val(item.Remark);

                    $("#bless_service_" + index).val(item.SuppliesType).trigger("change");

                    index++;
                });
            }

        }
    }

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val().trim();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

    function gotoChecked_wu() {
        var listcount = 1;

        Appname = $("#member_name").val().trim();                                               //購買人姓名
        Appmobile = $("#member_tel").val()                                                      //購買人電話
        Appemail = $("#member_email").val()                                                     //購買人Email

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        birthTime_Tag = [];
        leapMonth_Tag = [];
        homenum_Tag = [];
        email_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        suppliestype_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($('input:radio[name=sex]:checked').val());                             //祈福人性別

            var birth = "民國" + $("#input-birthday-year").val() + "年" + $("#input-birthday-month").val() + "月" + $("#input-birthday-day").val() + "日";
            birth_Tag.push(birth);                                                              //祈福人農歷生日
            birthTime_Tag.push($("#input-birthday-time").val());                                //祈福人農曆時辰
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-閏月 N-非閏月
            homenum_Tag.push($("#bless_homenum_" + i).val());                                   //市話
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人email
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址

            var suppliestype = 3;                                                               //服務項目
            suppliestype_Tag.push(suppliestype);
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appemail: Appemail,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            birthTime_Tag: JSON.stringify(birthTime_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            homenum_Tag: JSON.stringify(homenum_Tag),
            email_Tag: JSON.stringify(email_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            suppliestype_Tag: JSON.stringify(suppliestype_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
            listcount: listcount
        };

        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkEndTime() {
        //var startTime = $("#startTime").val().trim();
        var startTime = new Date();
        var endTime = $("#endTime").text();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
    }

    function checkedStartTime() {
        //var startTime = $("#startTime").val().trim();
        var endTime = new Date();
        var startTime = $("#startTime").text();
        if (Date.parse(endTime).valueOf() >= Date.parse(startTime).valueOf()) {
            return true;
        }
        return false;
    }
</script>
</body>
</html>
