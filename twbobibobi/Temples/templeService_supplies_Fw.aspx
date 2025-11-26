<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies_Fw.aspx.cs" Inherits="twbobibobi.Temples.templeService_supplies_Fw" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="斗六五路財神宮|補財庫|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_supplies_Fw.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="斗六五路財神宮|補財庫|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!-- 預覽影片 -->
    <meta property="og:video" content="https://www.youtube.com/embed/GveFGmkpGG8" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/supplies_Fw.jpg?t=666168" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/supplies_Fw.jpg?t=666168" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/supplies_Fw.jpg?t=666168" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="補財庫大揭秘" />
    <meta name="twitter:description" content="樂透得主這樣拜來這裡補財庫有機會成為下一個樂透得主!" />
    <meta name="twitter:image" content="https://bobibobi.tw/Temples/images/temple/supplies_Fw.jpg?t=666168" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>斗六五路財神宮|補財庫|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
        .countalign {
            position: absolute;
            right: 0;
            margin-right: 140px;
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

        .important {
            color: red;
            font-weight: bold;
        }

       .EventServiceContent .grouplist {
            display: flex; align-items: center; gap: 8px;
        }
       
        .ytvideo {
            max-width: 100%;
            width: 100%;
            height: 550px;
            margin-bottom: -5px;
        }

        .FormInput p {
            font-size: 1vw;
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
            .RecAddress > div:first-child {
                width: 20%;
            }

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

            .countalign {
                position: initial;
                right: 0;
                margin-right: 0;
                width: 100%
            }

            .countalign label {
                width: 100%;
                text-align: left;
                margin-bottom: 1%;
            }

            .FormInput p {
                font-size: 3.8vw;
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
    <script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "VideoObject",
      "name": "補財庫大揭秘",
      "description": "樂透得主這樣拜來這裡補財庫有機會成為下一個樂透得主!",
      "thumbnailUrl": "https://img.youtube.com/vi/GveFGmkpGG8/maxresdefault.jpg",
      "uploadDate": "2025-02-27T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/GveFGmkpGG8",
      "contentUrl": "https://bobibobi.tw/Temples/templeService_supplies_Fw.aspx"
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
                    <li><a href="#" title="斗六五路財神宮">斗六五路財神宮</a></li>
                    <li>補財庫</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="https://bobibobi.tw/Temples/images/temple/supplies_Fw.jpg?t=666168" width="1160" height="550" alt="斗六五路財神宮補財庫" title="斗六五路財神宮補財庫" />
                </div>
                <h1 class="TempleName">歡迎使用《斗六五路財神宮》補財庫線上報名</h1>
                <div class="TempleServiceInfo">
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                            <div id="startTime">2025/06/09 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2025/12/31 23:59</div>
                        </div>
                        <div>
                            <p>斗六五路財神宮補財庫比較特別的是會先使用<span class="important content_a">催財符</span>，催財符就如同一把鑰匙，燒化後打開你的財庫，讓金銀財寶能順利進入您的財庫。</p>
                        </div>
                        <div>
                            <h2>先赦罪再補庫</h2>
                        </div>
                        <div>
                            <p>在傳統信仰中，財庫影響著我們的運勢與福祿。然而，若因前世業障或今生疏忽，導致財庫虧損，便可能出現財務困頓、事業受阻的狀況。因此，補財庫成為一種祈求財運順遂的重要法門。</p>
                        </div>
                        <div>
                            <p>但補庫之前，需先行赦罪。因為財庫的虧損，往往與無形的業障、冤親債主相關。先透過赦罪科儀，向神明懺悔過往罪業，化解業障，才能讓補庫的功效真正發揮，讓財運、福報源源不絕。</p>
                        </div>
                        <div>
                            <h2>📿 先赦罪，再補庫，開啟順遂人生！</h2>
                        </div>
                        <div class="grouplist">
                            <h2>🔹 赦罪：</h2>
                            <p>清理業障，化解阻礙</p>
                        </div>
                        <div class="grouplist">
                            <h2>🔹 補庫：</h2>
                            <p>充盈財庫，廣納福報</p>
                        </div>
                        <div class="grouplist">
                            <h2>🔹 圓滿：</h2>
                            <p>運勢提升，事事順遂</p>
                        </div>
                        <div>
                            <h2>補財庫有分正財與偏財</h2>
                        </div>
                        <div class="">
                            <p style="color: forestgreen; font-weight: bold;">補（正）財庫：</p>
                            <p>正財是指：需要用時間勞力工作換取之財。例如每月固定薪水收入，或是認真努力經營事業，都需要補正財庫。使用正財符令來開啟你的正財運，想要事業鴻圖大展，生意興隆，財源廣進，每月都一定要補正財庫，讓你正財運亨通，財源滾滾而來。</p>
                        </div>
                        <div class="">
                            <p style="color: forestgreen; font-weight: bold;">補（偏）財庫：</p>
                            <p>偏財是指：不用靠勞力工作而得之財。例如：一夕暴富，意外之財，彩券，股市，娛樂業，八大行業，都需要補偏財庫。使用偏財符令來開啟你的偏財運，想要輕鬆進財，每月都一定要補偏財庫，讓你偏財運亨通，八方進財，財富滾滾而來。</p>
                        </div>
                        <br />

                        <div class="">
                            <iframe class="ytvideo" src="https://www.youtube.com/embed/GveFGmkpGG8?si=IIrTlhH-xeTN6MTt" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                        </div>

                        <br />
                        <uc3:SocialMedia runat="server" ID="SocialMedia" />
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
                            <label>購買人信箱</label><input name="member_mail" type="text" class="required" id="member_mail" placeholder="請輸入購買人信箱"/>
                        </div>
                        <div class="FormInput address">
                            <label>購買人地址</label>
                            <div class="MemAddress">
                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="member_zipcode" data-id="member_zipcode"></div>
                                <div data-role="county" data-style="addr-county required" data-name="member_county" data-id="member_county"></div>
                                <div data-role="district" data-style="addr-district required" data-name="member_district" data-id="member_district"></div>
                            </div>
                            <input name="member_address" type="text" class="required" id="member_address" placeholder="請輸入購買人地址。將用於寄送感謝狀。" />
                        </div>
                        <div class="FormInput">
                            <label class="emailalert"></label>
                            <span style="color: red;">我們將根據購買人地址寄送感謝狀，請填寫正確的購買人地址。</span>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div class="FormInput">
                                    <p>（祈福人限填一位，每個補財庫對應一位祈福人。如需多位，請點選增加祈福人。）</p>
                                </div>
                                <div class="FormInput select">
                                    <label>活動項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="21">補財庫(正財) $500</option>
                                        <option value="22">補財庫(偏財) $500</option>
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
                                    <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker required2" id="bless_birthday_1" placeholder="請選擇農歷生日或國曆生日二擇一"/>
                                </div>
                                <div class="FormInput select">
                                    <label>閏月</label>
                                    <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                                        <option value="N">非閏月</option>

                                        <option value="Y">閏月</option>
                                    </select>
                                </div>
                                <div class="FormInput select">
                                    <label>農歷時辰</label>
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
                                    <label>國歷生日</label><input name="bless_sbirthday_1" type="text" class="datapicker required2" id="bless_sbirthday_1" placeholder="請選擇國歷生日或農曆生日二擇一"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人市話</label><input name="bless_homenum_1" type="tel" class="" id="bless_homenum_1" placeholder="請輸入祈福人市話(選填)"/>
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
            alert('親愛的大德您好\n斗六五路財神宮 2025補財庫已截止！！\n感謝您的支持, 謝謝!');
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
                    $("#" + newId).val('21');
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
            $('.MemAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
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
        const sbirth = $li.find(`#bless_sbirthday_${i}`).val();
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
                gotoChecked_Fw();
            } else {
                alert('斗六五路財神宮 2025補財庫已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('斗六五路財神宮 2025補財庫尚未開始！');
            location = 'https://bobibobi.tw/Temples/temple.aspx';
        }
    })

    //導向確認資料頁面
    function gotochecked(res) {
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            if (res.redirect) {
                window.location = res.redirect;
            } else {
                alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
            }
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
                    //$("#bless_sbirthday_" + index).val(item.sBirth);
                    $("#bless_homenum_" + index).val(item.Homenum);
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
                    $("#bless_service_" + index).val(item.SuppliesType);
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

    function gotoChecked_Fw() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                                                          //購買人姓名
        Appmobile = $("#member_tel").val();                                                         //購買人電話
        AppEmail = $("#member_mail").val();                                                         //購買人信箱
        AppzipCode = $("#member_zipcode").val();                                                    //購買人郵政區號
        Appcounty = $("select[name='member_county']").val();                                        //購買人縣市
        Appdist = $("select[name='member_district']").val();                                        //購買人區域
        Appaddr = $("#member_address").val().trim();

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        homenum_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        SuppliesType_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                                 //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                                //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                                   //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val());                                            //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                                       //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                                       //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirthday_" + i).val());                                          //祈福人國曆生日
            homenum_Tag.push($("#bless_homenum_" + i).val().trim());                                    //祈福人市話
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
            SuppliesType_Tag.push($("#bless_service_" + i).val().trim());                               //服務項目
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
            homenum_Tag: JSON.stringify(homenum_Tag),
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
            SuppliesType_Tag: JSON.stringify(SuppliesType_Tag),
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
