<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_h.aspx.cs" Inherits="Temple.Temples.templeService_purdue_h" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="新港奉天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_h.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="新港奉天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/purdue_h_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/purdue_h_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/purdue_h_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>新港奉天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <style type="text/css">
        textarea {
            font-size: 15px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            width: 100%;
            height: 120px;
            padding: 5px;
            font-family: Helvetica, Arial, "微軟正黑體", "微軟雅黑體",Heiti TC, sans-serif;
            border: 1px solid #e5e5e5;
        }
        .FormInput_h {
            color: #707070;
            text-align: left;
            margin-right: 1vw;
            display: inline-block;
            font-size: 1vw;
            vertical-align: middle;
        }
        .red {
            color: #F00;
        }         
        .FormInput_h2 label {
            color: #707070;
            margin-right: 1vw;
            display: inline-block;
            font-size: 1vw;
            vertical-align: middle;
            width: 15vw;
            text-align: left;
        } 
        .FormInput_h3 label {
            width: 15vw;
            text-align: left;
        }
        .FormInput_h2 select {
            border: 1px solid #D3D4D9;
            padding: 5px;
            color: #707070;
        }
        .content_a {
            font-size: 1.2vw;
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
            .AppAddress > div:first-child {
                width: 20%;
            }
            .DeathAddress > div:first-child {
                width: 20%;
            }
            .FormInput_h3 label, .FormInput_h2 label, .FormInput_h {
                width: 100%;
                font-size: 3.8vw;
            }
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }

            .EventServiceContent img {
                width: 100%;
            }
        }

        /*電腦版*/
        @media only screen and (min-width: 576px) {
            .text_s input, .tel input, .mail input, .date input {
                width: 20vw;
            }
        }
    </style>
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=4" title="新港奉天宮">新港奉天宮</a></li>
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_h_2025.jpg" width="1160" height="550" alt="《新港奉天宮》中元普渡線上報名" title="《新港奉天宮》中元普渡線上報名" />
                </div>
                <h1 class="TempleName">歡迎使用《新港奉天宮》中元普渡線上報名</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2025/07/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/08/15 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>【乙巳年中元普渡。靈寶啟燈消灾拔薦齋壇超度法會】</h2>
                            <p>即日起開始受理贊普。</p>
                            <p>法會日期：農曆七月初六日至七月初八日（國曆8/28~8/30）</p>
                            <p>法會科儀內容：</p>
                            <p>祝燈祈福、赦罪法會、運香叩禱、三官妙經、獻供七獻、普度植福等</p>
                        </div>
                        <div>
                            <p>新港奉天宮乙巳年中元普度靈寶啟燈消灾拔薦齋壇超度法會</p>
                            <p>為期三天法會來款待好兄弟及祖先。信眾如不能到場，本宮亦會準備文稿及普度符令</p>
                            <p>法會開始當天可向祖先牌位或方位稟告祖先</p>
                            <p>鼓勵將普度品捐出，救助弱勢團體</p>
                        </div>

                    </div>
                </div>


                <!--訂購表單-->
                <!--說明：
            1.必填欄位請於input或select增加class="required"。
            2.需動態產生表單，請使用<ul class="InputGroup">包覆，搭配<li bless-id="{編號}">使用。
            3.每個欄位呈現為<div class="FormInput {項目}">，項目請由下方自行挑選複製使用，若有缺的話，亦可通知補上。
            4.因欄位搭配很多JS的生成及檢核，若有使用到"地址"及"生日(或日期)"的部份，需特別注意JS的部份。
        -->
                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" maxlength="5" type="text" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入購買人電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>購買人信箱</label><input name="member_mail" type="email" class="required" id="member_mail" placeholder="請輸入電子信箱" />
                        </div>
                        <div class="FormInput date birth">
                            <label>農曆生日</label><input name="member_birthday" type="text" class="datapicker required3" id="member_birthday" placeholder="請選擇農曆生日或國曆生日二擇一" />
                        </div>
                        <div class="FormInput select count">
                            <label>閏月</label>
                            <select name="member_leapMonth" class="required" id="member_leapMonth">
                                <option value="N">非閏月</option>

                                <option value="Y">閏月</option>
                            </select>
                        </div>
                        <div class="FormInput select count">
                            <label>農曆時辰</label>
                            <select name="member_birthtime" class="required" id="member_birthtime">
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
                        <div class="FormInput date birth">
                            <label>國曆生日</label><input name="member_sbirth" type="text" class="datapicker required3" id="member_sbirth" placeholder="請選擇農曆生日或國曆生日二擇一" />
                        </div>
                        <div class="FormInput address">
                            <label>購買人地址</label>
                            <div class="AppAddress">
                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_app_zipcode_1" data-id="bless_app_zipcode_1"></div>
                                <div data-role="county" data-style="addr-county required" data-name="bless_app_county_1" data-id="bless_app_county_1"></div>
                                <div data-role="district" data-style="addr-district required" data-name="bless_app_district_1" data-id="bless_app_district_1"></div>
                            </div>
                            <input name="bless_app_address_1" type="text" class="required" id="bless_app_address_1" placeholder="請輸入地址" />
                        </div>
                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">
                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="FormTitle_B">辦理項目<span>（勾選拔度法會 (個人)，請填寫祈福人資料。）</span></div>
                                <label class="FormInput_h">
                                    <input name="item1" type="checkbox" id="item1" value="功德主一位-3000" onchange="CountPrice();" />
                                    功德主：一戶<span class="red">3,000元</span>贊助一天法會（可全家，限同一地址)【法會現場為其功德主誦經祈福】</label>
                                <textarea name="tp_a" rows="2" cols="20" id="tp_a" placeholder="可填寫同戶姓名及農曆生日；可不填。"></textarea>
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <label class="FormInput_h">
                                    <input name="item2" type="checkbox" id="item2" value="祝燈延壽消災-600" onchange="CountPrice();" />
                                    祝燈延壽消災<span class="red"> 600元</span> (個人) 【主要陽世人賜福科儀】
                                            法會現場點燃油燈由高功道長主持法會，於現場即張貼參與者姓名等資料，為參與者辦理科儀。
                                </label>
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <label class="FormInput_h">
                                    <input name="item3" type="checkbox" id="item3" value="四十九愿解冤釋結-600" onchange="CountPrice();" /> 四十九愿解冤釋結<span class="red"> 600元 </span>(個人) 【主要解開冤結以赦免罪過、消除怨恨】</label>
                                <br /><br />
                            </li>
                            <li bless-id="2">
                                <div class="FormTitle_B">拔度法會 (個人)<span>（勾選拔度法會 (個人)，請填寫祈福人資料。）</span></div>
                                <div class="FormInput_h2">
                                    <label class="">
                                        <input name="item4" type="checkbox" id="item4" value="冤親債主-500" onchange="CountPrice();" />
                                        (1) 冤親債主 <span class="red">500元</span>
                                    </label>
                                </div>
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput_h2">
                                    <label class="">
                                    <input name="item5" type="checkbox" id="item5" value="九玄七祖-500" onchange="CountPrice();" /> (2) 九玄七祖 <span class="red">500元</span><br />(歷代祖先，迴向功德)</label>
                                </div>
                                <div id="bless_firstname_1" name="bless_firstname_1">
                                    <div class="FormInput text_s">
                                        <label>超度亡者姓氏</label><input name="bless_first_name_1" type="text" class="" id="bless_first_name_1" placeholder="請輸入超度亡者姓氏" />
                                    </div>
                                </div>
                                <div id="bless_deathaddress_1" name="bless_deathaddress_1">
                                    <div class="FormInput address">
                                        <label>祖先牌位地址</label>
                                        <div class="DeathAddress">
                                            <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_death_zipcode_1" data-id="bless_death_zipcode_1"></div>
                                            <div data-role="county" data-style="addr-county " data-name="bless_death_county_1" data-id="bless_death_county_1"></div>
                                            <div data-role="district" data-style="addr-district " data-name="bless_death_district_1" data-id="bless_death_district_1"></div>
                                        </div>
                                        <input name="bless_death_address_1" type="text" class="" id="bless_death_address_1" placeholder="請輸入地址" />
                                    </div>
                                </div>
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput_h2">
                                    <label class="">
                                    <input name="item6" type="checkbox" id="item6" value="功德迴向往生者-500" onchange="CountPrice();" /> (3) 功德迴向往生者<span class="red">500元</span></label>
                                </div>
                                <div id="bless_deathname_1" name="bless_deathname_1">
                                    <div class="FormInput text_s">
                                            <label>超度亡者姓名</label><input name="bless_death_name_1" type="text" class="" id="bless_death_name_1" placeholder="請輸入超度亡者姓名" />
                                    </div>
                                </div>
                                <div id="bless_deathaddress_2" name="bless_deathaddress_2">
                                    <div class="FormInput address">
                                        <label>祖先牌位地址</label>
                                        <div class="DeathAddress">
                                            <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_death_zipcode_2" data-id="bless_death_zipcode_2"></div>
                                            <div data-role="county" data-style="addr-county " data-name="bless_death_county_2" data-id="bless_death_county_2"></div>
                                            <div data-role="district" data-style="addr-district " data-name="bless_death_district_2" data-id="bless_death_district_2"></div>
                                        </div>
                                        <input name="bless_death_address_2" type="text" class="" id="bless_death_address_2" placeholder="請輸入地址" />
                                    </div>
                                </div>
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput_h2 select">
                                    <label class="">
                                        <input name="item7" type="checkbox" id="item7" value="地祇主-500" onchange="CountPrice();" /> (4) 地祇主<span class="red">500元</span>
                                    </label>
                                    <select name="bless_LandlordNum_1" class="" id="bless_LandlordNum_1">
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
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput_h2 select">
                                    <label class="">
                                        <input name="item8" type="checkbox" id="item8" value="嬰靈-500" onchange="CountPrice();" />
                                        (5) 嬰靈<span class="red">500元</span>
                                    </label>
                                    <select name="bless_BabyNum_1" class="" id="bless_BabyNum_1">
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
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput_h2 select">
                                    <label class="">
                                        <input name="item9" type="checkbox" id="item9" value="動物靈-500" onchange="CountPrice();" />
                                        (6) 動物靈<span class="red">500元</span>
                                    </label>
                                    <select name="bless_AnimalNum_1" class="" id="bless_AnimalNum_1">
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
                                <hr style="clear: both; border: none; height: 1px; background: #dcdcdc;" />
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required4" maxlength="5" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                    <input type="checkbox" class="checkedbox" id="bless_copy_name_1" />
                                    <label for="bless_copy_name_1" id="bless_checkednamelabel_1" style="width: auto;">同購買人姓名</label>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required4 " id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
                                    <input type="checkbox" class="checkedbox" id="bless_copy_tel_1" />
                                    <label for="bless_copy_tel_1" id="bless_checkedtellabel_1" style="width: auto;">同購買人聯絡電話</label>
                                </div>
                                <div class="FormInput select">
                                    <label>性別</label>
                                    <select name="bless_sex_1" class="" id="bless_sex_1">
                                        <option selected="selected" value="善男">善男</option>
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
                                    <input name="bless_address_1" type="text" class="" id="bless_address_1" placeholder="請輸入地址"/>
                                </div>
                            </li>
                            <li bless-id="3">
                                <div class="FormTitle_B">認捐項目<span></span></div>
                                <div class="FormInput select FormInput_h3">
                                    <label>普 度 品 一份<span class="red">1,000元</span></label>
                                    <select name="bless_PurdueNum_1" class="" id="bless_PurdueNum_1">
                                        <option selected="selected" value="0">0</option>
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
                                <div class="FormInput select FormInput_h3">
                                    <label>白米認捐 一袋<span class="red">200元</span><br />2公斤裝（不領回）</label>
                                    <select name="bless_RiceNum_1" class="" id="bless_RiceNum_1">
                                        <option selected="selected" value="0">0</option>
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
                                <div class="FormInput select FormInput_h3">
                                    <label>金紙部分 一份<span class="red">200元</span><br />
                                        附:壽金、金衣、白錢、二五、古板、金銀，份數
                                    </label>
                                    <select name="bless_mMoneyNum_1" class="" id="bless_mMoneyNum_1">
                                        <option selected="selected" value="0">0</option>
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
                                
                                <label class="FormInput_h">
                                    備註說明
                                </label>
                                <textarea name="bless_Remark_1" type="text" class="" id="bless_Remark_1" placeholder="請輸入問題內容"></textarea>
                            </li>
                        </ul>
                        <!--可複製的區塊 //end-->

                        <%--<div class="FormAddList"><a href="javascript:addList();" title="增加祈福人">✚ 增加祈福人</a></div>--%>

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
            alert('親愛的大德您好\n新港奉天宮 2025普度活動已截止！！\n感謝您的支持, 謝謝!');
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

    $('.AppAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });

    $('.DeathAddress').twzipcode({
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

    // 工具：由 input 元件往上找 label
    function getLabelText($input) {
        const id = $input.attr('id') || '';
        if (id.includes('_county_')) return '縣市';
        if (id.includes('_district_')) return '鄉鎮市區';

        let txt = $input.closest('.FormInput')
            .find('label').first().text().trim();
        if (!txt) {
            $input.closest('.FormInput')
                .prevAll('.FormInput')
                .each(function () {
                    const t = $(this).find('label').first().text().trim();
                    if (t) { txt = t; return false; }
                });
        }
        return txt.replace(/：|:$/g, '');
    }
    // 針對祈福人做驗證
    function validateBless() {
        const $li = $('.InputGroup > li[bless-id=2]');

        // 1. 收集所有被勾選的 item1~item9
        const selected = [];
        for (let j = 1; j <= 9; j++) {
            if ($(`#item${j}`).is(':checked')) {
                selected.push(j);
            }
        }
        if (selected.length === 0) {
            showToastAndFocus($('#item1'), '請先選擇您想辦理的普度項目');
            return false;
        }

        // 2. 如果沒有選到個人法會 (4~9)，就不需要填祈福人資料
        const personalSvc = selected.filter(svc => svc >= 4 && svc <= 9);
        if (personalSvc.length === 0) {
            return true;
        }

        // 3. 電話：非空 + 格式
        const $tel = $li.find('#bless_tel_1');
        const tel = $tel.val()?.trim();
        if (!tel) {
            showToastAndFocus($tel, '祈福人電話不能為空。');
            $tel.addClass('unfilled');
            return false;
        }
        if (!Isphone(tel)) {
            showToastAndFocus($tel, '祈福人電話格式錯誤。');
            $tel.addClass('unfilled');
            return false;
        }
        $tel.removeClass('unfilled');

        // 4. 國內才檢查 縣市 & 區域
        if ($li.find('#bless_oversea_1').val() === '1') {
            const $county = $li.find('#bless_county_1');
            const $district = $li.find('#bless_district_1');
            if (!$county.val()) {
                showToastAndFocus($county, '祈福人地址 縣市為空，請重新選擇。');
                $county.addClass('unfilled');
                return false;
            }
            if (!$district.val()) {
                showToastAndFocus($district, '祈福人地址 區域為空，請重新選擇。');
                $district.addClass('unfilled');
                return false;
            }
            $county.removeClass('unfilled');
            $district.removeClass('unfilled');
        }

        // 5. 檢查部分地址
        const $addr = $li.find('#bless_address_1');
        const addr = $addr.val()?.trim();
        if (!addr) {
            showToastAndFocus($addr, '祈福人地址不能為空。');
            $addr.addClass('unfilled');
            return false;
        }
        $addr.removeClass('unfilled');

        // 6. 農曆/國曆生日二擇一 (假設只有 svc=4,8 需要)
        const needBirthday = [4, 8];
        if (personalSvc.some(svc => needBirthday.includes(svc))) {
            const $b1 = $li.find(`#bless_birthday_1`);
            const $b2 = $li.find(`#bless_sbirth_1`);
            if (!($b1.val()?.trim()) && !($b2.val()?.trim())) {
                showToastAndFocus($b1, '請選擇農曆或國曆生日其中一項。');
                $b1.addClass('unfilled');
                $b2.addClass('unfilled');
                return false;
            }
            $b1.removeClass('unfilled');
            $b2.removeClass('unfilled');
        }

        // 7. 根據 svc (4~9) 決定要檢查的欄位
        const fieldMap = {
            5: ['bless_first_name_1', 'bless_death_county_1', 'bless_death_district_1', 'bless_death_address_1'],
            6: ['bless_death_name_1', 'bless_death_county_2', 'bless_death_district_2', 'bless_death_address_2'],
        };

        let fields = [];
        personalSvc.forEach(svc => {
            const list = fieldMap[svc] || [];
            list.forEach(key => fields.push(`${key}`));
        });
        // 去重
        fields = Array.from(new Set(fields));

        // 8. 批次檢查這些欄位
        const missing = [];
        let $firstErr = null;
        fields.forEach(fid => {
            const $el = $li.find(`#${fid}`);
            const v = $el.val()?.trim();
            if (!v) {
                $el.addClass('unfilled');
                const lbl = getLabelText($el);
                if (!missing.includes(lbl)) missing.push(lbl);
                if (!$firstErr) $firstErr = $el;
            } else {
                $el.removeClass('unfilled');
            }
        });

        if (missing.length) {
            showToastAndFocus(
                $firstErr,
                missing.join('、') + ' 為必填欄位，請重新填寫。'
            );
            return false;
        }

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
        if (!validateBless()) {
            return;
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
                gotoChecked_h();
            } else {
                alert('新港奉天宮 2025普度活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('新港奉天宮 2025普度活動尚未開始！');
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
        }
        else {
            if (res.elementId) {
                if (res.elementId == 'item1') {
                    $(".Notice").text("您尚未選擇辦理項目。");
                    $(".Notice").addClass("active");
                }
                else if (res.elementId == 'm_phone') {
                    $(".Notice").text("手機格式錯誤。");
                    $(".Notice").addClass("active");
                }
            }
            else {
                if (res.StatusType == 1) {
                    alert("此購買人已完成付款動作，請重新購買。")
                    window.location = "https://bobibobi.tw/Temples/templeService_purdue_h.aspx";
                }
                else {
                    alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
                }
            }
        }
    }

    //更新之前輸入的資料
    function editinfo(res) {
        if (res.StatusCode == 1) {
            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);
            $("#member_mail").val(res.AppEmail);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    //$("#member_birthday").val(item.AppBirth);
                    $("#member_leapMonth").val(item.AppLeapMonth);
                    $("#member_birthtime").val(item.AppBirthTime);
                    //$("#member_sbirth").val(item.AppsBirth);
                    $("#bless_app_county_1").val(item.AppCounty).trigger("change");
                    $("#bless_app_district_1").val(item.Appdist).trigger("change");
                    $("#bless_app_address_1").val(item.AppAddr);

                    if (item.Merit == "1") {
                        $('#item1').prop("checked", true);
                        $("#tp_a").val(item.MeritText);
                    }

                    if (item.Life == "1") {
                        $('#item2').prop("checked", true);
                    }

                    if (item.Redress == "1") {
                        $('#item3').prop("checked", true);
                    }

                    if (item.Creditor == "1") {
                        $('#item4').prop("checked", true);
                    }

                    if (item.Ancestor == "1") {
                        $('#item5').prop("checked", true);
                        $("#bless_first_name_1").val(item.AncestorLastname);
                        $("#bless_death_county_1").val(item.AncestorCounty).trigger("change");
                        $("#bless_death_district_1").val(item.Ancestordist).trigger("change");
                        $("#bless_death_address_1").val(item.AncestorAddr);
                    }

                    if (item.Deceased == "1") {
                        $('#item6').prop("checked", true);
                        $("#bless_death_name_1").val(item.DeceasedName);
                        $("#bless_death_county_2").val(item.DeceasedCounty).trigger("change");
                        $("#bless_death_district_2").val(item.Deceaseddist).trigger("change");
                        $("#bless_death_address_2").val(item.DeceasedAddr);
                    }

                    if (item.Landlord == "1") {
                        $('#item7').prop("checked", true);
                        $("#bless_LandlordNum_1").val(item.LandlordNum);
                    }

                    if (item.Baby == "1") {
                        $('#item8').prop("checked", true);
                        $("#bless_LandlordNum_1").val(item.BabyNum);
                    }

                    if (item.Animal == "1") {
                        $('#item9').prop("checked", true);
                        $("#bless_LandlordNum_1").val(item.AnimalNum);
                    }

                    $("#bless_name_1").val(item.Name);
                    $("#bless_tel_1").val(item.Mobile);
                    $("#bless_sex_1").val(item.Sex);
                    //$("#bless_birthday_1").val(item.Birth);
                    $("#bless_leapMonth_1").val(item.LeapMonth);
                    $("#bless_birthtime_1").val(item.BirthTime);
                    $("#bless_sBirth_1").val(item.sBirth);
                    $("#bless_oversea_1").val(item.oversea).trigger("change");
                    if (item.oversea == 1) {
                        $("#bless_cusaddress_1").show();
                        $("#bless_county_1").val(item.County).trigger("change");
                        $("#bless_district_1").val(item.dist).trigger("change");
                    }
                    else {
                        $("#bless_cusaddress_1").hide();
                    }
                    $("#bless_address_1").val(item.Addr);
                    $("#bless_Remark_1").val(item.Remark);

                    if (item.PurdueNum != "0") {
                        $("#bless_PurdueNum_1").val(item.PurdueNum);
                    }

                    if (item.RiceNum != "0") {
                        $("#bless_RiceNum_1").val(item.RiceNum);
                    }

                    if (item.mMoneyNum != "0") {
                        $("#bless_mMoneyNum_1").val(item.mMoneyNum);
                    }

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

    function gotoChecked_h() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                                  //購買人姓名
        Appmobile = $("#member_tel").val()                                  //購買人電話
        AppEmail = $("#member_mail").val();                                 //購買人信箱
        AppBirth = $("#member_birthday").val()                              //購買人農曆生日
        AppleapMonth = $("#member_leapMonth").val();                        //閏月 Y-是 N-否
        Appbirthtime = $("#member_birthtime").val();                        //購買人農曆時辰
        AppsBirth = $("#member_sbirth").val()                               //購買人國曆生日
        Appcounty = $("select[name='bless_app_county_1']").val()            //購買人縣市
        Appdist = $("select[name='bless_app_district_1']").val()            //購買人區域
        Appaddr = $("#bless_app_address_1").val()                           //購買人地址(部分)
        AppzipCode = $("#bless_app_zipcode_1").val();                       //購買人郵遞區號

        item1 = $('#item1').is(":checked") ? 1 : 0;                         //功德主
        item2 = $('#item2').is(":checked") ? 1 : 0;                         //祝燈延壽消災
        item3 = $('#item3').is(":checked") ? 1 : 0;                         //四十九愿解冤釋結
        item4 = $('#item4').is(":checked") ? 1 : 0;                         //冤親債主
        item5 = $('#item5').is(":checked") ? 1 : 0;                         //九玄七祖
        item6 = $('#item6').is(":checked") ? 1 : 0;                         //功德迴向往生者
        item7 = $('#item7').is(":checked") ? 1 : 0;                         //地祇主
        item8 = $('#item8').is(":checked") ? 1 : 0;                         //嬰靈
        item9 = $('#item9').is(":checked") ? 1 : 0;                         //動物靈

        tp_a = $("#tp_a").val();;                                           //功德主內容

        firstName = $("#bless_first_name_1").val();                         //超度亡者姓氏
        Deathcounty1 = $("select[name='bless_death_county_1']").val()       //祖先牌位縣市
        Deathdist1 = $("select[name='bless_death_district_1']").val()       //祖先牌位區域
        Deathaddr1 = $("#bless_death_address_1").val()                      //祖先牌位地址(部分)
        DeathzipCode1 = $("#bless_death_zipcode_1").val()                   //祖先牌位郵遞區號
        DeathName = $("#bless_death_name_1").val();                         //超度亡者姓名
        Deathcounty2 = $("select[name='bless_death_county_2']").val()       //祖先牌位縣市
        Deathdist2 = $("select[name='bless_death_district_2']").val()       //祖先牌位區域
        Deathaddr2 = $("#bless_death_address_2").val()                      //祖先牌位地址(部分)
        DeathzipCode2 = $("#bless_death_zipcode_2").val()                   //祖先牌位郵遞區號
        Name = $("#bless_name_1").val();                                    //祈福人姓名
        Mobile = $("#bless_tel_1").val();                                   //祈福人電話
        Sex = $("#bless_sex_1").val();                                      //祈福人性別 善男 信女
        Birth = $("#bless_birthday_1").val()                                //農曆生日
        leapMonth = $("#bless_leapMonth_1").val();                          //閏月 Y-是 N-否
        birthtime = $("#bless_birthtime_1").val();                          //農曆時辰
        sBirth = $("#bless_sbirth_1").val()                                 //國曆生日
        oversea = $("#bless_oversea_1").val();                              //國內-1 國外-2
        county = $("select[name='bless_county_1']").val()                   //祈福人縣市
        dist = $("select[name='bless_district_1']").val()                   //祈福人區域
        addr = $("#bless_address_1").val()                                  //祈福人地址(部分)
        zipCode = $("#bless_zipcode_1").val();                              //祈福人郵遞區號
        itema7 = $("#bless_LandlordNum_1").val();                           //地祇主-數量
        itema8 = $("#bless_BabyNum_1").val();                               //嬰靈-數量                                
        itema9 = $("#bless_AnimalNum_1").val();                             //動物靈-數量

        PurdueNum = $("#bless_PurdueNum_1").val();                          //普度數量
        RiceNum = $("#bless_RiceNum_1").val();                              //白米數量
        mMoneyNum = $("#bless_mMoneyNum_1").val();                          //金紙數量
        Remark = $("#bless_Remark_1").val();                                //備註說明

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            AppEmail: AppEmail,
            AppBirth: AppBirth,
            AppleapMonth: AppleapMonth,
            Appbirthtime: Appbirthtime,
            AppsBirth: AppsBirth,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            AppzipCode: AppzipCode,
            item1: item1,
            item2: item2,
            item3: item3,
            item4: item4,
            item5: item5,
            item6: item6,
            item7: item7,
            item8: item8,
            item9: item9,
            tp_a: tp_a,
            firstName: firstName,
            Deathcounty1: Deathcounty1,
            Deathdist1: Deathdist1,
            Deathaddr1: Deathaddr1,
            DeathzipCode1: DeathzipCode1,
            DeathName: DeathName,
            Deathcounty2: Deathcounty2,
            Deathdist2: Deathdist2,
            Deathaddr2: Deathaddr2,
            DeathzipCode2: DeathzipCode2,
            Name: Name,
            Mobile: Mobile,
            Sex: Sex,
            Birth: Birth,
            leapMonth: leapMonth,
            birthtime: birthtime,
            sBirth: sBirth,
            oversea: oversea,
            county: county,
            dist: dist,
            addr: addr,
            zipCode: zipCode,
            itema7: itema7,
            itema8: itema8,
            itema9: itema9,
            PurdueNum: PurdueNum,
            RiceNum: RiceNum,
            mMoneyNum: mMoneyNum,
            Remark: Remark
        };
        hasTextArea = true;
        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkEndTime() {
        //var startTime = $("#startTime").val();
        var startTime = new Date();
        var endTime = $("#endTime").text();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
    }

    function checkedStartTime() {
        //var startTime = $("#startTime").val();
        var endTime = new Date();
        var startTime = $("#startTime").text();
        if (Date.parse(endTime).valueOf() >= Date.parse(startTime).valueOf()) {
            return true;
        }
        return false;
    }
</script>
