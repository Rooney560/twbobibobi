<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies_dh.aspx.cs" Inherits="Temple.Temples.templeService_supplies_dh" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="天貺納福添運法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_supplies_dh.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="天貺納福添運法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/supplies_dh_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/supplies_dh_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/supplies_dh_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>天貺納福添運法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
        
        .EventServiceContent img {
            width: 75%;
            margin: 0 auto;
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

        .FormCount {
            margin: 1vw 0;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=16" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                    <li>天貺納福添運法會</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/supplies_dh_2025.jpg" width="1160" height="550" alt="天貺納福添運法會" title="天貺納福添運法會" />
                </div>
                <h1 class="TempleName">台東東海龍門天聖宮</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2025/06/09 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2025/06/30 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>【2025乙巳年．開天門『天貺納福添運法會』的獨特】</h2>
                            </div>
                            <div>
                                <p>活動日期：07月01日乙巳蛇年6月初6日(二) 時間:晚上07：30 虔敬設案焚香乞神憐 燃燈照亮指引民心安 農曆6月初6，「開天門」在民間被稱為天貺節，「貺」有賜予之意。</p>
                                <p>「天貺節」，也是民間相傳玉皇大帝‼開天門‼，相傳這一天，天門將開，民間的所有祈求、懺悔將可在這日直達上天。 且正逢 虎爺將軍聖誕，更有助於消災補運，上半年的
                                    所有不順利能夠否極泰來，下半年順利平安。可以說是「補運 補財庫 最佳時機」，而且每年只有一次。</p>
                                <p>當天將遵循往例辦理相關活動，並設置天案禮聘 道長 演法，齊心祝禱各善信能早日恢復往日的榮光及美好生活。 昊天金闕玉皇上帝賜福人間 歡迎各地善信報名參加</p>
                                <p>『天赦日』是開恩赦罪、消除罪愆的日子；『六月六天貺節』則是可以 大膽許願 向宇宙下訂單、離心想事成最近的日子，因為這天大開天門，您的 祈願玉帝聽得到 
                                    華人最古老的哲學典籍《易經》曾云：『立天之道曰 陰與陽，立地之道曰 柔與剛，立人之道曰 仁與義，兼三才而兩之。』</p>
                                <p>文中的『三才』是指 天才 .地才 .人才，道教把這三才視為一件事情發展成功的關鍵</p>
                                <p>請 玉皇大帝賜福祈願的天赦日</p>
                                <p>首先要在天時運作吉祥的日子舉行；</p>
                                <p>第二，要選擇地脈具有良好風水的廟宇；</p>
                                <p>第三，個人要具備虔誠的信心去參與。</p>
                                <br />
                                <h2>念念不忘．必有迴響</h2>
                                <h2>向宇宙下訂單</h2>
                                <br />
                                <%--<h2>『32天帝燈』</h2>
                                <p>每盞3,000元。含天帝燈+供天帝財寶箱(特大)+疏文結緣禮：五雷號令 與參加者結緣‼️<span id="supplies1" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>黑虎將軍補財庫</h2>
                                <p>元寶箱800元<span id="supplies2" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>消災解厄 科儀</h2>
                                <p>每份科儀為：600元整。媽祖元神燈+消災解厄疏文。<span id="supplies3" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>身體康健 科儀</h2>
                                <p>每份科儀為：600元整。中壇元帥主燈+身體康健疏文。<span id="supplies4" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補運 科儀</h2>
                                <p>每份科儀為：600元整。斗姥元辰燈消災解厄+補運勢疏文。<span id="supplies5" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補財庫 科儀</h2>
                                <p>每份科儀為：600元整。五路財神燈+補財庫疏文。<span id="supplies6" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補文昌 科儀</h2>
                                <p>每份科儀為：600元整。文昌智慧燈+補文昌疏文。<span id="supplies7" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>招貴人 科儀</h2>
                                <p>每份科儀為：600元整。虎爺制邪燈+招貴人疏文+蓮花+金紙。<span id="supplies8" style="color:red" class="content_a" runat="server">(已額滿)</span></p>--%>
                                
                                <h2>『水晶蓮花燈』</h2>
                                <p>每盞2,388元。<span id="supplies1" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>財神財寶箱</h2>
                                <p>每組800元。內藏五路財神秘符，猶如開通宇宙財富帳戶。<span id="supplies2" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>虎爺財寶箱</h2>
                                <p>每組800元。專剋小人暗箭，職場必備護身盾牌。<span id="supplies3" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>旺龍紫氣寶燈</h2>
                                <p>每盞700元。應合九紫離火運勢，助登2025年的大運契機。<span id="supplies4" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>玉皇宥罪錫福七星燈</h2>
                                <p>每組1000元。集合北斗延壽之力，點亮七重防護罩。<span id="supplies5" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>通天點金大龍香</h2>
                                <p>每支1000元。開通天香直達玊皇天尊聖前祈願賜福延壽、赦罪消業、解厄除災、運途順利、平安健康、財源廣進。<span id="supplies6" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>五路財神香</h2>
                                <p>每單位200元。<span id="supplies7" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>開恩赦罪 科儀</h2>
                                <p>每份科儀為：600元整。媽祖元神燈+開恩赦罪疏文。<span id="supplies8" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>消災解厄 科儀</h2>
                                <p>每份科儀為：600元整。媽祖元神燈+消災解厄疏文。<span id="supplies9" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補運 科儀</h2>
                                <p>每份科儀為：600元整。斗姥元辰燈+補運勢疏文。<span id="supplies10" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>身體康健 科儀</h2>
                                <p>每份科儀為：600元整。中壇元帥主燈+身體康健疏文。<span id="supplies11" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補財庫 科儀</h2>
                                <p>每份科儀為：600元整。五路財神燈+補財庫疏文。<span id="supplies12" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>補文昌 科儀</h2>
                                <p>每份科儀為：600元整。文昌智慧燈+補文昌疏文。<span id="supplies13" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>招貴人 科儀</h2>
                                <p>每份科儀為：600元整。虎爺制邪燈+補貴人疏文。<span id="supplies14" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                                               
                                
                            </div>
                        </div>

                        <uc3:SocialMedia runat="server" id="SocialMedia" />
                    </div>
                </div>

                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
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
                            <input name="member_address" type="text" class="required" id="member_address" placeholder="請輸入購買人地址" />
                        </div>
                        <%--<div class="FormInput">
                            <label class="emailalert"></label>
                            <span style="color: red;">我們將根據購買人地址寄送發票，請填寫正確的購買人地址。</span>
                        </div>--%>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個天貺納福添運法會對應一位祈福人。如需多位，請點選增加祈福人。）</div>
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
                                    <label>農曆生日</label><input name="bless_birthday_1" type="text" class="datapicker required2" id="bless_birthday_1" placeholder="請選擇農曆生日或國曆生日二擇一"/>
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
                                    <label>國曆生日</label><input name="bless_sbirthday_1" type="text" class="datapicker required2" id="bless_sbirthday_1" placeholder="請選擇國曆生日或農曆生日二擇一"/>
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
                                <div name="bless_person_1" id="bless_person_1">
                                    <div class="FormInput select FormCount">
                                        <label>活動項目</label>
                                        <span>【開天門】 - 水晶蓮花燈 $2388</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count1_1" class="count" id="bless_count1_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 財神財寶箱 $800</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count2_1" class="count" id="bless_count2_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 虎爺財寶箱 $800</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count3_1" class="count" id="bless_count3_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 旺龍紫氣寶燈 $700</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count4_1" class="count" id="bless_count4_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 玉皇宥罪錫福七星燈 $1000</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count5_1" class="count" id="bless_count5_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 通天點金大龍香 $1000</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count6_1" class="count" id="bless_count6_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 五路財神香 $200</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count7_1" class="count" id="bless_count7_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 開恩赦罪科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count8_1" class="count" id="bless_count8_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 消災解厄科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count9_1" class="count" id="bless_count9_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 補運科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count10_1" class="count" id="bless_count10_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 身體康健科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count11_1" class="count" id="bless_count11_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 補財庫科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count12_1" class="count" id="bless_count12_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 補文昌科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count13_1" class="count" id="bless_count13_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="FormInput select FormCount">
                                        <label></label>
                                        <span>【開天門】 - 招貴人科儀 $600</span>
                                        <div class="countalign">
                                            <label>數量</label>
                                            <select name="bless_count14_1" class="count" id="bless_count14_1">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select>
                                        </div>
                                    </div>
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
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-ABCDEFGH"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
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
            alert('親愛的大德您好\n台東東海龍門天聖宮 2025天貺納福添運法會已截止！！\n感謝您的支持, 謝謝!');
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

                if (newId.indexOf('leapMonth') >= 0) {
                    $("#" + newId).val('N');
                }

                if (newId.indexOf('birthtime') >= 0) {
                    $("#" + newId).val('吉');
                }

                if (newId.indexOf('count') >= 0) {
                    $("#" + newId).val('0');
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

        // 4. 檢查「每位祈福人都至少選一個數量」
        let allBlessersOk = true;

        $(".InputGroup > li[bless-id]").each(function () {
            const $li = $(this);
            const $counts = $li.find(".count");    // 取得這位祈福人的所有數量 select
            let thisOk = false;                 // 區域變數：預設這位還沒選過

            // 先清掉上一輪標記
            $counts.removeClass("unfilled");

            // 只要這組任何一個 >0 就 pass
            $counts.each(function () {
                if (parseInt($(this).val(), 10) > 0) {
                    thisOk = true;
                    return false;  // 跳出這位 count 的 each
                }
            });

            if (!thisOk) {
                // 這位沒選任何數量 → 標紅、Toast 並聚焦
                allBlessersOk = false;
                $counts.addClass("unfilled");
                showToastAndFocus(
                    $counts.first(),
                    "請為每位祈福人至少選擇一個活動項目的數量"
                );
                return false;  // 跳出整個 li 的 each，不再檢查後面的人
            }
        });

        // 如果有任何一位沒通過，就不要繼續往下
        if (!allBlessersOk) {
            return;
        }

        // 5. 隱私權同意
        if (!$("#checkedprivate").is(":checked")) {
            showToastAndFocus($("#checkedprivate"), "請勾選同意隱私權政策。");
            return;
        }

        // 6. 全部通過，送出
        console.log("所有欄位都已填寫正確，準備送出");
        // 如果活動時間判斷...
        if (checkedStartTime()) {
            if (checkEndTime()) {
                gotoChecked_dh();
            } else {
                alert('台東東海龍門天聖宮 2025天貺納福添運法會已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('台東東海龍門天聖宮 2025天貺納福添運法會尚未開始！');
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
                    $("#bless_Remark_" + index).val(item.Remark);
                    $("#bless_count1_" + index).val(item.Count1);
                    $("#bless_count2_" + index).val(item.Count2);
                    $("#bless_count3_" + index).val(item.Count3);
                    $("#bless_count4_" + index).val(item.Count4);
                    $("#bless_count5_" + index).val(item.Count5);
                    $("#bless_count6_" + index).val(item.Count6);
                    $("#bless_count7_" + index).val(item.Count7);
                    $("#bless_count8_" + index).val(item.Count8);
                    $("#bless_count9_" + index).val(item.Count9);
                    $("#bless_count10_" + index).val(item.Count10);
                    $("#bless_count11_" + index).val(item.Count11);
                    $("#bless_count12_" + index).val(item.Count12);
                    $("#bless_count13_" + index).val(item.Count13);
                    $("#bless_count14_" + index).val(item.Count14);

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

    function gotoChecked_dh() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                      //購買人姓名
        Appmobile = $("#member_tel").val();                     //購買人電話
        AppEmail = $("#member_mail").val();                     //購買人信箱
        AppzipCode = $("#member_zipcode").val();                //購買人郵遞區號
        Appcounty = $("select[name='member_county']").val();    //購買人縣市
        Appdist = $("select[name='member_district']").val();    //購買人區域
        Appaddr = $("#member_address").val();                   //購買人部分地址

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
        remark_Tag = [];

        count1_Tag = [];
        count2_Tag = [];
        count3_Tag = [];
        count4_Tag = [];
        count5_Tag = [];
        count6_Tag = [];
        count7_Tag = [];
        count8_Tag = [];
        count9_Tag = [];
        count10_Tag = [];
        count11_Tag = [];
        count12_Tag = [];
        count13_Tag = [];
        count14_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val().trim());                                          //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                         //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                            //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                     //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                                //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                                //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirthday_" + i).val().trim());                                   //祈福人國曆生日
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

            count1_Tag.push($("select[name='bless_count1_" + i + "']").val());                          //水晶蓮花燈
            count2_Tag.push($("select[name='bless_count2_" + i + "']").val());                          //財神財寶箱
            count3_Tag.push($("select[name='bless_count3_" + i + "']").val());                          //虎爺財寶箱
            count4_Tag.push($("select[name='bless_count4_" + i + "']").val());                          //旺龍紫氣寶燈
            count5_Tag.push($("select[name='bless_count5_" + i + "']").val());                          //玉皇宥罪錫福七星燈
            count6_Tag.push($("select[name='bless_count6_" + i + "']").val());                          //通天點金大龍香
            count7_Tag.push($("select[name='bless_count7_" + i + "']").val());                          //五路財神香
            count8_Tag.push($("select[name='bless_count8_" + i + "']").val());                          //開恩赦罪科儀
            count9_Tag.push($("select[name='bless_count9_" + i + "']").val());                          //消災解厄科儀
            count10_Tag.push($("select[name='bless_count10_" + i + "']").val());                        //補運科儀
            count11_Tag.push($("select[name='bless_count11_" + i + "']").val());                        //身體康健科儀
            count12_Tag.push($("select[name='bless_count12_" + i + "']").val());                        //補財庫科儀
            count13_Tag.push($("select[name='bless_count13_" + i + "']").val());                        //補文昌科儀
            count14_Tag.push($("select[name='bless_count14_" + i + "']").val());                        //招貴人科儀

            //count1_Tag.push($("select[name='bless_count1_" + i + "']").val());                          //三十二天帝燈
            //count2_Tag.push($("select[name='bless_count2_" + i + "']").val());                          //黑虎將軍補財庫
            //count3_Tag.push($("select[name='bless_count3_" + i + "']").val());                          //消災解厄科儀
            //count4_Tag.push($("select[name='bless_count4_" + i + "']").val());                          //身體康健科儀
            //count5_Tag.push($("select[name='bless_count5_" + i + "']").val());                          //補運科儀
            //count6_Tag.push($("select[name='bless_count6_" + i + "']").val());                          //補財庫科儀
            //count7_Tag.push($("select[name='bless_count7_" + i + "']").val());                          //補文昌科儀
            //count8_Tag.push($("select[name='bless_count8_" + i + "']").val());                          //招貴人科儀
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
            count1_Tag: JSON.stringify(count1_Tag),
            count2_Tag: JSON.stringify(count2_Tag),
            count3_Tag: JSON.stringify(count3_Tag),
            count4_Tag: JSON.stringify(count4_Tag),
            count5_Tag: JSON.stringify(count5_Tag),
            count6_Tag: JSON.stringify(count6_Tag),
            count7_Tag: JSON.stringify(count7_Tag),
            count8_Tag: JSON.stringify(count8_Tag),
            count9_Tag: JSON.stringify(count9_Tag),
            count10_Tag: JSON.stringify(count10_Tag),
            count11_Tag: JSON.stringify(count11_Tag),
            count12_Tag: JSON.stringify(count12_Tag),
            count13_Tag: JSON.stringify(count13_Tag),
            count14_Tag: JSON.stringify(count14_Tag),
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
