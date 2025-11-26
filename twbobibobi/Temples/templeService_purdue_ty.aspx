<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_ty.aspx.cs" Inherits="Temple.Temples.templeService_purdue_ty" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="桃園威天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_ty.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="桃園威天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/purdue_ty_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/purdue_ty_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/purdue_ty_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>桃園威天宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
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
            .DeathAddress > div:first-child {
                width: 20%;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=14" title="桃園威天宮">桃園威天宮</a></li>
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_ty_2025.jpg" width="1160" height="550" alt="《桃園威天宮》中元普渡線上報名" title="《桃園威天宮》中元普渡線上報名" />
                </div>
                <h1 class="TempleName">歡迎使用《桃園威天宮》中元普渡線上報名</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2025/07/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/09/09 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>何謂贊普</h2>
                            <p>贊普是贊助供養六道眾生齋食及法食的意思，不僅能施食供養，最主要是召請鬼道眾生來受法食，仗神佛的慈悲願力，讓一切餓鬼，皆能得度，成就無上功德。</p>
                            <p>報名參加【桃園威天宮】代辦中元普渡服務。</p>
                            <p>即完成中元普渡，植福、轉好運！</p>
                            <p>普品貼上善信大德的姓名於農曆7月15日，擺在法會現場、宴請四方兄弟。</p>
                            <p>參加線上普度，普品將捐出公益團體。</p>
                        </div>
                        <div>
                            <h2>孝道功德主 $ 3000元 (含花雕木牌一座、超薦中牌一座)<span id="purdue1" style="color: red" class="content_a" runat="server">(已額滿)</span></h2>
                            <h2>光明功德主 $ 1000元 (含超薦大牌一座)<span id="purdue2" style="color: red" class="content_a" runat="server">(已額滿)</span></h2>
                            <h2>發心功德主 $ 600元 (含超薦中牌一座)<span id="purdue3" style="color: red" class="content_a" runat="server">(已額滿)</span></h2>
                            <h2>普渡品-乙份 $ 800</h2>
                            <h2>普渡白米50台斤-乙份 $ 1800</h2>
                            <h2>普渡白米3台斤-乙份 $ 600</h2>
                            <br />
                            <br />
                            <p>超薦項目(十選一)</p>
                            <h2>顯考O公O府君</h2>
                            <p>「顯考O公O府君」-被超薦者欄，請依前述格式，自行完整填寫(姓氏)公、(名字)府君，地址欄請填寫牌位所在之地址。</p>
                            <h2>顯妣O母 氏OO夫人</h2>
                            <p>「顯妣 母 氏 夫人」-被超薦者欄，請依前述格式，自行完整填寫(夫姓)母、(超薦者姓氏)公、(名字)夫人，地址欄請填寫牌位所在之地址。</p>
                            <h2>O氏歷代祖先</h2>
                            <p>「氏歷代祖先」-被超薦者欄，請依前述格式，自行完整填寫(姓氏)氏，地址欄請填寫祖先牌位所在之地址。</p>
                            <%--<h2>空白牌</h2>
                            <p>「空白牌」-被超薦者欄及地址欄，請根據欲被超薦者之姓名及地址填寫。如任一欄，無相關資訊欲填寫，則請填寫「無」，被超薦事由，則請填寫備註欄。</p>--%>
                            <h2>車號OOO車輛誤傷之生靈</h2>
                            <p>「車號 車輛誤傷之生靈」-被超薦者欄，請依前述格式，自行完整填寫車號(號碼數字)車輛，地址欄請填寫陽上人之地址。</p>
                            <h2>無緣子女</h2>
                            <p>「無緣子女」-被超薦者欄，請依前述格式，自行完整填寫無緣子女(姓名)，如無則請填「無」，地址欄請填寫，如無則請填「無」。</p>
                            <h2>累劫冤親債主</h2>
                            <p>「累劫冤親債主」-請填寫地址欄。</p>
                            <h2>地基主</h2>
                            <p>「地基主」-請填寫地址欄。</p>
                            <h2>過去飼養一切動物之靈</h2>
                            <p>「過去飼養一切動物之靈」-被超薦者欄，請填寫寵物(姓名)，如無則請填寫「無」，地址欄請填寫陽上人之地址。</p>
                            <h2>十方法界一切有情眾生</h2>
                            <p>「十方法界一切有情眾生」-請填寫地址欄。</p>
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
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>購買人信箱</label><input name="member_mail" type="text" class="required" id="member_mail" placeholder="請輸入購買人信箱"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">
                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個普度項目對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput select">
                                    <label>普度項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option selected="selected" value>請選擇</option>
                                        <option value="1">贊普(普度品) $800</option>
                                        <option value="14">孝道功德主 $3000</option>
                                        <option value="15">光明功德主 $1000</option>
                                        <option value="16">發心功德主 $600</option>
                                        <option value="18">普度白米50台斤 $1800</option>
                                        <option value="19">普度白米3台斤  $600</option>
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
                                <div class="Zamp">
                                    <div id="count_1" name="count_1">
                                        <div class="FormInput select">
                                            <label>普品數量</label>
                                            <select name="bless_count_1" class="" id="bless_count_1">
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
                                                <option value="11">11</option>
                                                <option value="12">12</option>
                                                <option value="13">13</option>
                                                <option value="14">14</option>
                                                <option value="15">15</option>
                                                <option value="16">16</option>
                                                <option value="17">17</option>
                                                <option value="18">18</option>
                                                <option value="19">19</option>
                                                <option value="20">20</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="Salvation">
                                    <div class="FormInput select " name="ServiceA_1" id="ServiceA_1">
                                        <label>花雕木牌</label>
                                        <select name="bless_tabletA_1" class="" id="bless_tabletA_1">
                                            <option selected="selected" value>請選擇</option>
                                            <option value="顯考O公O府君">顯考O公O府君</option>
                                            <option value="顯妣O母 氏OO夫人">顯妣O母 氏OO夫人</option>
                                            <option value="O氏歷代祖先">O氏歷代祖先</option>
                                            <%--<option value="空白牌">空白牌</option>--%>
                                            <option value="車號OOO車輛誤傷之生靈">車號OOO車輛誤傷之生靈</option>
                                            <option value="無緣子女">無緣子女</option>
                                            <option value="累劫冤親債主">累劫冤親債主</option>
                                            <option value="地基主">地基主</option>
                                            <option value="過去飼養一切動物之靈">過去飼養一切動物之靈</option>
                                            <option value="十方法界一切有情眾生">十方法界一切有情眾生</option>
                                        </select>
                                    </div>
                                    <div id="bless_ServiceA_deathname_1" name="bless_ServiceA_deathname_1">
                                        <div class="FormInput text_s">
                                            <label>亡者姓名</label><input name="bless_ServiceA_death_name_1" type="text" class="required3" id="bless_ServiceA_death_name_1" placeholder="請輸入亡者姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_firstname_1" name="bless_ServiceA_firstname_1">
                                        <div class="FormInput text_s">
                                            <label>姓氏</label><input name="bless_ServiceA_first_name_1" type="text" class="required4" id="bless_ServiceA_first_name_1" placeholder="請輸入姓氏" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_momname_1" name="bless_ServiceA_momname_1">
                                        <div class="FormInput text_s">
                                            <label>(夫姓) 母</label><input name="bless_ServiceA_mom_name_1" type="text" class="required4" id="bless_ServiceA_mom_name_1" placeholder="請輸入(夫姓) 母" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_lastname_1" name="bless_ServiceA_lastname_1">
                                        <div class="FormInput text_s">
                                            <label>名字</label><input name="bless_ServiceA_last_name_1" type="text" class="required4" id="bless_ServiceA_last_name_1" placeholder="請輸入名字" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_reason_1" name="bless_ServiceA_reason_1">
                                        <div class="FormInput text_s">
                                            <label>超薦事由</label><input name="bless_ServiceA_reason_name_1" type="text" class="required4" id="bless_ServiceA_reason_name_1" placeholder="請輸入被超薦者資料" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_licenseNum_1" name="bless_ServiceA_licenseNum_1">
                                        <div class="FormInput text_s">
                                            <label>車牌(車牌數字)</label><input name="bless_ServiceA_licenseNum_name_1" type="text" class="required4" id="bless_ServiceA_licenseNum_name_1" placeholder="請輸入車牌(車牌數字)" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceA_deathaddress_1" name="bless_ServiceA_deathaddress_1">
                                        <div class="FormInput address">
                                            <label>被超薦者地址</label>
                                            <div class="DeathAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_ServiceA_death_zipcode_1" data-id="bless_ServiceA_death_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county " data-name="bless_ServiceA_death_county_1" data-id="bless_ServiceA_death_county_1"></div>
                                                <div data-role="district" data-style="addr-district " data-name="bless_ServiceA_death_district_1" data-id="bless_ServiceA_death_district_1"></div>
                                            </div>
                                            <input name="bless_ServiceA_death_address_1" type="text" class="" id="bless_ServiceA_death_address_1" placeholder="請輸入被超薦者地址" />
                                        </div>
                                    </div>
                                    <div class="FormInput select " name="ServiceB_1" id="ServiceB_1">
                                        <label>超薦大牌</label>
                                        <select name="bless_tabletB_1" class="" id="bless_tabletB_1">
                                            <option selected="selected" value>請選擇</option>
                                            <option value="顯考O公O府君">顯考O公O府君</option>
                                            <option value="顯妣O母 氏OO夫人">顯妣O母 氏OO夫人</option>
                                            <option value="O氏歷代祖先">O氏歷代祖先</option>
                                            <%--<option value="空白牌">空白牌</option>--%>
                                            <option value="車號OOO車輛誤傷之生靈">車號OOO車輛誤傷之生靈</option>
                                            <option value="無緣子女">無緣子女</option>
                                            <option value="累劫冤親債主">累劫冤親債主</option>
                                            <option value="地基主">地基主</option>
                                            <option value="過去飼養一切動物之靈">過去飼養一切動物之靈</option>
                                            <option value="十方法界一切有情眾生">十方法界一切有情眾生</option>
                                        </select>
                                    </div>
                                    <div id="bless_ServiceB_deathname_1" name="bless_ServiceB_deathname_1">
                                        <div class="FormInput text_s">
                                            <label>亡者姓名</label><input name="bless_ServiceB_death_name_1" type="text" class="required3" id="bless_ServiceB_death_name_1" placeholder="請輸入亡者姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_firstname_1" name="bless_ServiceB_firstname_1">
                                        <div class="FormInput text_s">
                                            <label>姓氏</label><input name="bless_ServiceB_first_name_1" type="text" class="required4" id="bless_ServiceB_first_name_1" placeholder="請輸入姓氏" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_momname_1" name="bless_ServiceB_momname_1">
                                        <div class="FormInput text_s">
                                            <label>(夫姓) 母</label><input name="bless_ServiceB_mom_name_1" type="text" class="required4" id="bless_ServiceB_mom_name_1" placeholder="請輸入(夫姓) 母" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_lastname_1" name="bless_ServiceB_lastname_1">
                                        <div class="FormInput text_s">
                                            <label>名字</label><input name="bless_ServiceB_first_name_1" type="text" class="required4" id="bless_ServiceB_last_name_1" placeholder="請輸入名字" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_reason_1" name="bless_ServiceB_reason_1">
                                        <div class="FormInput text_s">
                                            <label>超薦事由</label><input name="bless_ServiceB_reason_name_1" type="text" class="required4" id="bless_ServiceB_reason_name_1" placeholder="請輸入超薦事由" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_licenseNum_1" name="bless_ServiceB_licenseNum_1">
                                        <div class="FormInput text_s">
                                            <label>車牌(車牌數字)</label><input name="bless_ServiceB_licenseNum_name_1" type="text" class="required4" id="bless_ServiceB_licenseNum_name_1" placeholder="請輸入車牌(車牌數字)" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceB_deathaddress_1" name="bless_ServiceB_deathaddress_1">
                                        <div class="FormInput address">
                                            <label>被超薦者地址</label>
                                            <div class="DeathAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_ServiceB_death_zipcode_1" data-id="bless_ServiceB_death_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county " data-name="bless_ServiceB_death_county_1" data-id="bless_ServiceB_death_county_1"></div>
                                                <div data-role="district" data-style="addr-district " data-name="bless_ServiceB_death_district_1" data-id="bless_ServiceB_death_district_1"></div>
                                            </div>
                                            <input name="bless_ServiceB_death_address_1" type="text" class="" id="bless_ServiceB_death_address_1" placeholder="請輸入被超薦者地址" />
                                        </div>
                                    </div>
                                    <div class="FormInput select " name="ServiceC_1" id="ServiceC_1">
                                        <label>超薦中牌</label>
                                        <select name="bless_tabletC_1" class="" id="bless_tabletC_1">
                                            <option selected="selected" value>請選擇</option>
                                            <option value="顯考O公O府君">顯考O公O府君</option>
                                            <option value="顯妣O母 氏OO夫人">顯妣O母 氏OO夫人</option>
                                            <option value="O氏歷代祖先">O氏歷代祖先</option>
                                            <option value="空白牌">空白牌</option>
                                            <option value="車號OOO車輛誤傷之生靈">車號OOO車輛誤傷之生靈</option>
                                            <option value="無緣子女">無緣子女</option>
                                            <option value="累劫冤親債主">累劫冤親債主</option>
                                            <option value="地基主">地基主</option>
                                            <option value="過去飼養一切動物之靈">過去飼養一切動物之靈</option>
                                            <option value="十方法界一切有情眾生">十方法界一切有情眾生</option>
                                        </select>
                                    </div>
                                    <div id="bless_ServiceC_deathname_1" name="bless_ServiceC_deathname_1">
                                        <div class="FormInput text_s">
                                            <label>亡者姓名</label><input name="bless_ServiceC_death_name_1" type="text" class="required3" id="bless_ServiceC_death_name_1" placeholder="請輸入亡者姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_firstname_1" name="bless_ServiceC_firstname_1">
                                        <div class="FormInput text_s">
                                            <label>姓氏</label><input name="bless_ServiceC_first_name_1" type="text" class="required4" id="bless_ServiceC_first_name_1" placeholder="請輸入姓氏" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_momname_1" name="bless_ServiceC_momname_1">
                                        <div class="FormInput text_s">
                                            <label>(夫姓) 母</label><input name="bless_ServiceC_mom_name_1" type="text" class="required4" id="bless_ServiceC_mom_name_1" placeholder="請輸入(夫姓) 母" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_lastname_1" name="bless_ServiceC_lastname_1">
                                        <div class="FormInput text_s">
                                            <label>名字</label><input name="bless_ServiceC_first_name_1" type="text" class="required4" id="bless_ServiceC_last_name_1" placeholder="請輸入名字" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_reason_1" name="bless_ServiceC_reason_1">
                                        <div class="FormInput text_s">
                                            <label>超薦事由</label><input name="bless_ServiceC_reason_name_1" type="text" class="required4" id="bless_ServiceC_reason_name_1" placeholder="請輸入超薦事由" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_licenseNum_1" name="bless_ServiceC_licenseNum_1">
                                        <div class="FormInput text_s">
                                            <label>車牌(車牌數字)</label><input name="bless_ServiceC_licenseNum_name_1" type="text" class="required4" id="bless_ServiceC_licenseNum_name_1" placeholder="請輸入車牌(車牌數字)" />
                                        </div>
                                    </div>
                                    <div id="bless_ServiceC_deathaddress_1" name="bless_ServiceC_deathaddress_1">
                                        <div class="FormInput address">
                                            <label>被超薦者地址</label>
                                            <div class="DeathAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_ServiceC_death_zipcode_1" data-id="bless_ServiceC_death_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county " data-name="bless_ServiceC_death_county_1" data-id="bless_ServiceC_death_county_1"></div>
                                                <div data-role="district" data-style="addr-district " data-name="bless_ServiceC_death_district_1" data-id="bless_ServiceC_death_district_1"></div>
                                            </div>
                                            <input name="bless_ServiceC_death_address_1" type="text" class="" id="bless_ServiceC_death_address_1" placeholder="請輸入被超薦者地址" />
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
            alert('親愛的大德您好\n桃園威天宮 2025普度活動已截止！！\n感謝您的支持, 謝謝!');
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

    $('.DeathAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });

    $('.InputGroup > li:last .Zamp').find('div').each(function (index) {
        var originalId = $(this).attr('id');
        if (originalId) {
            $("#" + originalId).hide();
            $("#" + originalId).hide();
        }
    });

    $('.InputGroup > li:last .Salvation').find('div').each(function (index) {
        var originalId = $(this).attr('id');
        if (originalId) {
            $("#" + originalId).hide();
            $("#" + originalId).hide();
        }
    });

</script>

<!-----增減祈福人----->
<script>
    var originalField = $('.InputGroup > li').first().clone();

    //增加
    function addList() {
        var lastblessNum = parseInt($('.InputGroup > li').last().attr('bless-id')) + 1;
        console.log("bless-id-" + lastblessNum);


        var newField = originalField.clone();
        //newField.find('input, select').val('');

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

        $('.InputGroup > li:last .DeathAddress').find('div[data-role]').each(function (index) {
            var originalId = $(this).attr('data-id');
            var originalName = $(this).attr('data-name');
            var newId = originalId.slice(0, -1) + lastblessNum;
            var newName = originalName.slice(0, -1) + lastblessNum;
            $(this).attr('data-id', newId);
            $(this).attr('data-name', newId);
        });

        $('.InputGroup > li:last .Zamp').find('div').each(function (index) {
            var originalId = $(this).attr('id');
            if (originalId) {
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);
            }
        });

        $('.InputGroup > li:last .Salvation').find('div').each(function (index) {
            var originalId = $(this).attr('id');
            if (originalId) {
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);
            }
        });

        $('.DeletData').addClass("active");

        dateSelect();//有日期選擇時使用
        $('.CusAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
        $('.DeathAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行

        //選中普度項目後顯示所需輸入的欄位
        $('.InputGroup > li:last').find('select').each(function (index) {
            var originalId = $(this).attr('id');
            if (originalId) {
                if (originalId.indexOf("service") > 0) {
                    var newId = originalId.slice(0, -1) + lastblessNum;

                    $("#" + newId).on("change", function () {
                        var originalId = $(this).attr('id');
                        if (originalId) {
                            var id = originalId.slice(-1);
                            var value = $(this).val();
                            if (value != '') {
                                switch (value) {
                                    case "1":
                                        //贊普
                                        $(".Zamp #count_" + id).show();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).hide();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    case "14":
                                        //孝道功德主
                                        $(".Zamp #count_" + id).hide();
                                        $(".Salvation #ServiceA_" + id).show();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).show();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    case "15":
                                        //光明功德主
                                        $(".Zamp #count_" + id).hide();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).show();
                                        $(".Salvation #ServiceC_" + id).hide();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    case "16":
                                        //發心功德主
                                        $(".Zamp #count_" + id).hide();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).show();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    case "18":
                                        //普度白米30台斤
                                        $(".Zamp #count_" + id).show();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).hide();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    case "19":
                                        //普度白米3台斤
                                        $(".Zamp #count_" + id).show();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).hide();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                    default:
                                        $(".Zamp #count_" + id).hide();
                                        $(".Salvation #ServiceA_" + id).hide();
                                        $(".Salvation #ServiceB_" + id).hide();
                                        $(".Salvation #ServiceC_" + id).hide();

                                        itemhide('ServiceA', 'tabletA', id);
                                        itemhide('ServiceB', 'tabletB', id);
                                        itemhide('ServiceC', 'tabletC', id);
                                        break;
                                }
                            }
                            else {
                                $(".Zamp #count_" + id).hide();
                                $(".Salvation #ServiceA_" + id).hide();
                                $(".Salvation #ServiceB_" + id).hide();
                                $(".Salvation #ServiceC_" + id).hide();

                                itemhide('ServiceA', 'tabletA', id);
                                itemhide('ServiceB', 'tabletB', id);
                                itemhide('ServiceC', 'tabletC', id);
                            }
                        }
                    });
                }

                if (originalId.indexOf("tabletA") > 0) {
                    var newId = originalId.slice(0, -1) + lastblessNum;

                    $("#" + newId).on("change", function () {
                        var originalId = $(this).attr('id');
                        if (originalId) {
                            var id = originalId.slice(-1);
                            var value = $(this).val();

                            if ($(".Salvation #ServiceA_" + id).is(":visible")) {
                                itemhide('ServiceB', '', id);
                            }
                            else {
                                itemhide('ServiceA', '', id);
                                itemhide('ServiceB', '', id);
                                itemhide('ServiceC', '', id);
                            }
                            if (value != '') {
                                switch (value) {
                                    case "顯考O公O府君":
                                        $("#bless_ServiceA_firstname_" + id).show();
                                        $("#bless_ServiceA_lastname_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();

                                        $("#bless_ServiceA_firstname_" + id + " label").text("顯考(姓氏)公");
                                        $("#bless_ServiceA_first_name_" + id).attr("placeholder", "請輸入顯考(姓氏)公");

                                        $("#bless_ServiceA_lastname_" + id + " label").text("(名字)府君");
                                        $("#bless_ServiceA_last_name_" + id).attr("placeholder", "請輸入(名字)府君");
                                        break;
                                    case "顯妣O母 氏OO夫人":
                                        $("#bless_ServiceA_firstname_" + id).show();
                                        $("#bless_ServiceA_momname_" + id).show();
                                        $("#bless_ServiceA_lastname_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();

                                        $("#bless_ServiceA_momname_" + id + " label").text("(夫姓)母");
                                        $("#bless_ServiceA_mom_name_" + id).attr("placeholder", "請輸入(夫姓)母");

                                        $("#bless_ServiceA_firstname_" + id + " label").text("(本姓)氏");
                                        $("#bless_ServiceA_first_name_" + id).attr("placeholder", "請輸入(本姓)氏");

                                        $("#bless_ServiceA_lastname_" + id + " label").text("(名字)夫人");
                                        $("#bless_ServiceA_last_name_" + id).attr("placeholder", "請輸入(名字)夫人");
                                        break;
                                    case "O氏歷代祖先":
                                        $("#bless_ServiceA_firstname_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();

                                        $("#bless_ServiceA_firstname_" + id + " label").text("(姓氏)氏");
                                        $("#bless_ServiceA_first_name_" + id).attr("placeholder", "請輸入(姓氏)氏");
                                        break;
                                    case "空白牌":
                                        $("#bless_ServiceA_reason_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();
                                        break;
                                    case "車號OOO車輛誤傷之生靈":
                                        $("#bless_ServiceA_licenseNum_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();
                                        break;
                                    case "無緣子女":
                                        $("#bless_ServiceA_deathname_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();

                                        $("#bless_ServiceA_deathname_" + id + " label").text("無緣子女(姓名)");
                                        $("#bless_ServiceA_deathname_" + id).attr("placeholder", "請輸入無緣子女(姓名)");
                                        break;
                                    case "累劫冤親債主":
                                        $("#bless_ServiceA_deathaddress_" + id).show();
                                        break;
                                    case "地基主":
                                        $("#bless_ServiceA_deathaddress_" + id).show();
                                        break;
                                    case "過去飼養一切動物之靈":
                                        $("#bless_ServiceA_deathname_" + id).show();
                                        $("#bless_ServiceA_deathaddress_" + id).show();

                                        $("#bless_ServiceA_deathname_" + id + " label").text("寵物(姓名)");
                                        $("#bless_ServiceA_deathname_" + id).attr("placeholder", "請輸入寵物(姓名)");
                                        break;
                                    case "十方法界一切有情眾生":
                                        $("#bless_ServiceA_deathaddress_" + id).show();
                                        break;
                                }
                            }
                        }
                    });
                }

                if (originalId.indexOf("tabletB") > 0) {
                    var newId = originalId.slice(0, -1) + lastblessNum;

                    $("#" + newId).on("change", function () {
                        var originalId = $(this).attr('id');
                        if (originalId) {
                            var id = originalId.slice(-1);
                            var value = $(this).val();
                            itemhide('ServiceA', '', id);
                            itemhide('ServiceB', '', id);
                            itemhide('ServiceC', '', id);
                            if (value != '') {
                                switch (value) {
                                    case "顯考O公O府君":
                                        $("#bless_ServiceB_firstname_" + id).show();
                                        $("#bless_ServiceB_lastname_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();

                                        $("#bless_ServiceB_firstname_" + id + " label").text("顯考(姓氏)公");
                                        $("#bless_ServiceB_first_name_" + id).attr("placeholder", "請輸入顯考(姓氏)公");

                                        $("#bless_ServiceB_lastname_" + id + " label").text("(名字)府君");
                                        $("#bless_ServiceB_last_name_" + id).attr("placeholder", "請輸入(名字)府君");
                                        break;
                                    case "顯妣O母 氏OO夫人":
                                        $("#bless_ServiceB_firstname_" + id).show();
                                        $("#bless_ServiceB_momname_" + id).show();
                                        $("#bless_ServiceB_lastname_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();

                                        $("#bless_ServiceB_momname_" + id + " label").text("(夫姓)母");
                                        $("#bless_ServiceB_mom_name_" + id).attr("placeholder", "請輸入(夫姓)母");

                                        $("#bless_ServiceB_firstname_" + id + " label").text("(本姓)氏");
                                        $("#bless_ServiceB_first_name_" + id).attr("placeholder", "請輸入(本姓)氏");

                                        $("#bless_ServiceB_lastname_" + id + " label").text("(名字)夫人");
                                        $("#bless_ServiceB_last_name_" + id).attr("placeholder", "請輸入(名字)夫人");
                                        break;
                                    case "O氏歷代祖先":
                                        $("#bless_ServiceB_firstname_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();

                                        $("#bless_ServiceB_firstname_" + id + " label").text("(姓氏)氏");
                                        $("#bless_ServiceB_first_name_" + id).attr("placeholder", "請輸入(姓氏)氏");
                                        break;
                                    case "空白牌":
                                        $("#bless_ServiceB_reason_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();
                                        break;
                                    case "車號OOO車輛誤傷之生靈":
                                        $("#bless_ServiceB_licenseNum_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();
                                        break;
                                    case "無緣子女":
                                        $("#bless_ServiceB_deathname_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();

                                        $("#bless_ServiceB_deathname_" + id + " label").text("無緣子女(姓名)");
                                        $("#bless_ServiceB_deathname_" + id).attr("placeholder", "請輸入無緣子女(姓名)");
                                        break;
                                    case "累劫冤親債主":
                                        $("#bless_ServiceB_deathaddress_" + id).show();
                                        break;
                                    case "地基主":
                                        $("#bless_ServiceB_deathaddress_" + id).show();
                                        break;
                                    case "過去飼養一切動物之靈":
                                        $("#bless_ServiceB_deathname_" + id).show();
                                        $("#bless_ServiceB_deathaddress_" + id).show();

                                        $("#bless_ServiceB_deathname_" + id + " label").text("寵物(姓名)");
                                        $("#bless_ServiceB_deathname_" + id).attr("placeholder", "請輸入寵物(姓名)");
                                        break;
                                    case "十方法界一切有情眾生":
                                        $("#bless_ServiceB_deathaddress_" + id).show();
                                        break;
                                }
                            }
                        }
                    });
                }

                if (originalId.indexOf("tabletC") > 0) {
                    var newId = originalId.slice(0, -1) + lastblessNum;

                    $("#" + newId).on("change", function () {
                        var originalId = $(this).attr('id');

                        if (originalId) {
                            var id = originalId.slice(-1);
                            var value = $(this).val();

                            if ($(".Salvation #ServiceA_" + id).is(":visible")) {
                                itemhide('ServiceB', '', id);
                            }
                            else {
                                itemhide('ServiceA', '', id);
                                itemhide('ServiceB', '', id);
                                itemhide('ServiceC', '', id);
                            }
                            if (value != '') {
                                switch (value) {
                                    case "顯考O公O府君":
                                        $("#bless_ServiceC_firstname_" + id).show();
                                        $("#bless_ServiceC_lastname_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();

                                        $("#bless_ServiceC_firstname_" + id + " label").text("顯考(姓氏)公");
                                        $("#bless_ServiceC_first_name_" + id).attr("placeholder", "請輸入顯考(姓氏)公");

                                        $("#bless_ServiceC_lastname_" + id + " label").text("(名字)府君");
                                        $("#bless_ServiceC_last_name_" + id).attr("placeholder", "請輸入(名字)府君");
                                        break;
                                    case "顯妣O母 氏OO夫人":
                                        $("#bless_ServiceC_firstname_" + id).show();
                                        $("#bless_ServiceC_momname_" + id).show();
                                        $("#bless_ServiceC_lastname_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();

                                        $("#bless_ServiceC_momname_" + id + " label").text("(夫姓)母");
                                        $("#bless_ServiceC_mom_name_" + id).attr("placeholder", "請輸入(夫姓)母");

                                        $("#bless_ServiceC_firstname_" + id + " label").text("(本姓)氏");
                                        $("#bless_ServiceC_first_name_" + id).attr("placeholder", "請輸入(本姓)氏");

                                        $("#bless_ServiceC_lastname_" + id + " label").text("(名字)夫人");
                                        $("#bless_ServiceC_last_name_" + id).attr("placeholder", "請輸入(名字)夫人");
                                        break;
                                    case "O氏歷代祖先":
                                        $("#bless_ServiceC_firstname_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();

                                        $("#bless_ServiceC_firstname_" + id + " label").text("(姓氏)氏");
                                        $("#bless_ServiceC_first_name_" + id).attr("placeholder", "請輸入(姓氏)氏");
                                        break;
                                    case "空白牌":
                                        $("#bless_ServiceC_reason_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();
                                        break;
                                    case "車號OOO車輛誤傷之生靈":
                                        $("#bless_ServiceC_licenseNum_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();
                                        break;
                                    case "無緣子女":
                                        $("#bless_ServiceC_deathname_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();

                                        $("#bless_ServiceC_deathname_" + id + " label").text("無緣子女(姓名)");
                                        $("#bless_ServiceC_deathname_" + id).attr("placeholder", "請輸入無緣子女(姓名)");
                                        break;
                                    case "累劫冤親債主":
                                        $("#bless_ServiceC_deathaddress_" + id).show();
                                        break;
                                    case "地基主":
                                        $("#bless_ServiceC_deathaddress_" + id).show();
                                        break;
                                    case "過去飼養一切動物之靈":
                                        $("#bless_ServiceC_deathname_" + id).show();
                                        $("#bless_ServiceC_deathaddress_" + id).show();

                                        $("#bless_ServiceC_deathname_" + id + " label").text("寵物(姓名)");
                                        $("#bless_ServiceC_deathname_" + id).attr("placeholder", "請輸入寵物(姓名)");
                                        break;
                                    case "十方法界一切有情眾生":
                                        $("#bless_ServiceC_deathaddress_" + id).show();
                                        break;
                                }
                            }
                        }
                    });
                }
            }
        });

    }

    //刪除
    $(".InputGroup").on("click", ".deletList", function () {
        $(this).parents('li').remove();
        var liCount = $('.InputGroup li').length;
        if (liCount == 1) {
            $('.DeletData').removeClass("active");
        }

    })

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
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
            // 所有通用必填欄位
            selector: ".required",
            checks: [{ fn: v => v.trim() !== "", msg: "上面有欄位為未填寫。" }]
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

        // 遍歷每個必填欄位-有條件 (普度項目=孝道功德主)
        if ($li.find(`#bless_service_${i}`).val() === "14") {

            const missing = [];
            const $A = $li.find(`#bless_tabletA_${i}`);
            const $C = $li.find(`#bless_tabletC_${i}`);

            if (!$A.val()) { missing.push("花雕木牌"); $A.addClass('unfilled'); }
            else { $A.removeClass('unfilled'); }

            if (!$C.val()) { missing.push("超薦中牌"); $C.addClass('unfilled'); }
            else { $C.removeClass('unfilled'); }

            if (missing.length) {
                showToastAndFocus($A, missing.join('、') + " 為必選，請重新選擇。");
                return false;
            }

            // 接著再分別驗證 A 與 C 的子欄位
            if (!validateTabletFields($A.val(), 'A', i)) return false;
            if (!validateTabletFields($C.val(), 'C', i)) return false;
        }

        // 遍歷每個必填欄位-有條件 (普度項目=光明功德主)
        if ($li.find(`#bless_service_${i}`).val() === "15") {

            const missing = [];
            const $B = $li.find(`#bless_tabletB_${i}`);

            if (!$B.val()) { missing.push("超薦大牌"); $B.addClass('unfilled'); }
            else { $B.removeClass('unfilled'); }

            if (missing.length) {
                showToastAndFocus($B, missing.join('、') + " 為必選，請重新選擇。");
                return false;
            }

            // 接著再分別驗證 B 的子欄位
            if (!validateTabletFields($B.val(), 'B', i)) return false;
        }

        // 遍歷每個必填欄位-有條件 (普度項目=發心功德主)
        if ($li.find(`#bless_service_${i}`).val() === "16") {

            const missing = [];
            const $C = $li.find(`#bless_tabletC_${i}`);

            if (!$C.val()) { missing.push("超薦中牌"); $C.addClass('unfilled'); }
            else { $C.removeClass('unfilled'); }

            if (missing.length) {
                showToastAndFocus($C, missing.join('、') + " 為必選，請重新選擇。");
                return false;
            }

            // 接著再分別驗證 C 的子欄位
            if (!validateTabletFields($C.val(), 'C', i)) return false;
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
        $('.InputGroup > li').each(function () {
            const id = Number($(this).attr('bless-id'));
            if (!validateBless(id)) {
                return false; // 中斷 each，相當於 return
            }
        });

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
                gotoChecked_ty();
            } else {
                alert('桃園威天宮 2025普度活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('桃園威天宮 2025普度活動尚未開始！');
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
        var tabletA = 0;
        if (res.StatusCode == 1) {
            for (var i = 1; i < res.listcount; i++) {
                addList();
            }

            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);
            $("#member_mail").val(res.AppEmail);

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

                    $("#bless_service_" + index).val(item.PurdueType).trigger("change");

                    if (item.PurdueType == "14") {
                        //孝道功德主

                        if (tabletA == 0) {
                            $(".Salvation #ServiceA_" + index).show();

                            var purdueItemArr = item.PurdueItem.split('-');

                            if (purdueItemArr.length > 1) {

                                $("#bless_tabletA_" + index).val(purdueItemArr[1]).trigger("change");

                                tabletChange(purdueItemArr[1], "ServiceA", index, item.DeathName, item.FirstName, item.MomName, item.LastName, item.Reason, item.LicenseNum,
                                    item.DeathCounty, item.Deathdist, item.DeathAddr);
                            }

                            tabletA++;
                        }
                        else {
                            $(".Salvation #ServiceC_" + index).show();

                            var purdueItemArr = item.PurdueItem1.split('-');

                            if (purdueItemArr.length > 1) {

                                $("#bless_tabletC_" + index).val(purdueItemArr[1]).trigger("change");

                                tabletChange(purdueItemArr[1], "ServiceC", index, item.DeathName1, item.FirstName1, item.MomName1, item.LastName1, item.Reason1, item.LicenseNum1,
                                    item.DeathCounty1, item.Deathdist1, item.DeathAddr1);
                            }
                        }

                    }
                    else if (item.PurdueType == "15") {
                        //光明功德主
                        $(".Salvation #ServiceB_" + index).show();

                        var purdueItemArr = item.PurdueItem.split('-');

                        if (purdueItemArr.length > 1) {

                            $("#bless_tabletB_" + index).val(purdueItemArr[1]).trigger("change");

                            tabletChange(purdueItemArr[1], "ServiceB", index, item.DeathName, item.FirstName, item.MomName, item.LastName, item.Reason, item.LicenseNum,
                                item.DeathCounty, item.Deathdist, item.DeathAddr);
                        }
                    }
                    else if (item.PurdueType == "16") {
                        //發心功德主
                        $(".Salvation #ServiceC_" + index).show();

                        var purdueItemArr = item.PurdueItem.split('-');

                        if (purdueItemArr.length > 1) {

                            $("#bless_tabletC_" + index).val(purdueItemArr[1]).trigger("change");

                            tabletChange(purdueItemArr[1], "ServiceC", index, item.DeathName, item.FirstName, item.MomName, item.LastName, item.Reason, item.LicenseNum, item.DeathCounty
                                , item.Deathdist, item.DeathAddr);
                        }
                    }
                    else {
                        //贊普、白米30台斤、白米3台斤
                        $(".Zamp #count_" + index).show();

                        $("#bless_count_" + index).val(item.Count);
                    }

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

    function gotoChecked_ty() {
        var listcount = $('.InputGroup > li').length;

        Appname = $("#member_name").val();                                                          //購買人姓名
        Appmobile = $("#member_tel").val();                                                         //購買人電話
        AppEmail = $("#member_mail").val();                                                         //購買人信箱

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
        purduetype_Tag = [];
        count_Tag = [];

        purdueA_Tag = [];
        purdueB_Tag = [];
        purdueC_Tag = [];

        purdue_deathname_Tag = [];
        purdue_firstname_Tag = [];
        purdue_momname_Tag = [];
        purdue_lastname_Tag = [];
        purdue_reason_Tag = [];
        purdue_licenseNum_Tag = [];
        purdue_zipCode_Tag = [];
        purdue_county_Tag = [];
        purdue_dist_Tag = [];
        purdue_addr_Tag = [];

        purdue2_deathname_Tag = [];
        purdue2_firstname_Tag = [];
        purdue2_momname_Tag = [];
        purdue2_lastname_Tag = [];
        purdue2_reason_Tag = [];
        purdue2_licenseNum_Tag = [];
        purdue2_zipCode_Tag = [];
        purdue2_county_Tag = [];
        purdue2_dist_Tag = [];
        purdue2_addr_Tag = [];

        $('.InputGroup > li').each(function () {
            const i = $(this).attr('bless-id'); // 真正的 id (可能是 1,3,...)

            name_Tag.push($("#bless_name_" + i).val().trim());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val().trim());                                     //祈福人國曆生日
            oversea_Tag.push($("#bless_oversea_" + i).val());                                          //國內-1 國外-2

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

            var purduetype = $("select[name='bless_service_" + i + "']").val();                             //普度項目
            purduetype_Tag.push(purduetype);
            count_Tag.push($("#bless_count_" + i).val());                                                   //普品、白米30台斤、白米3台斤 數量

            var purdueStringA = $("select[name='bless_tabletA_" + i + "']").val();                          //孝親功德主
            purdueA_Tag.push(purdueStringA);
            var purdueStringB = $("select[name='bless_tabletB_" + i + "']").val();                          //光明功德主
            purdueB_Tag.push(purdueStringB);
            var purdueStringC = $("select[name='bless_tabletC_" + i + "']").val();                          //發心功德主
            purdueC_Tag.push(purdueStringC);

            if (purdueStringA != "") {
                purdue_deathname_Tag.push($("#bless_ServiceA_death_name_" + i).val());                      //亡者姓名
                purdue_firstname_Tag.push($("#bless_ServiceA_first_name_" + i).val());                      //顯考(姓氏)公
                purdue_momname_Tag.push($("#bless_ServiceA_mom_name_" + i).val());                          //(夫姓) 母
                purdue_lastname_Tag.push($("#bless_ServiceA_last_name_" + i).val());                        //(名字)府君
                purdue_reason_Tag.push($("#bless_ServiceA_reason_name_" + i).val());                        //超薦事由
                purdue_licenseNum_Tag.push($("#bless_ServiceA_licenseNum_name_" + i).val());                //車牌(車牌數字)
                purdue_zipCode_Tag.push($("#bless_ServiceA_death_zipcode_" + i).val());                     //被超薦者郵遞區號
                purdue_county_Tag.push($("select[name='bless_ServiceA_death_county_" + i + "']").val());    //被超薦者縣市
                purdue_dist_Tag.push($("select[name='bless_ServiceA_death_district_" + i + "']").val());    //被超薦者區域
                purdue_addr_Tag.push($("#bless_ServiceA_death_address_" + i).val());                        //被超薦者部分地址

                purdue2_deathname_Tag.push($("#bless_ServiceC_death_name_" + i).val());                      //亡者姓名
                purdue2_firstname_Tag.push($("#bless_ServiceC_first_name_" + i).val());                      //顯考(姓氏)公
                purdue2_momname_Tag.push($("#bless_ServiceC_mom_name_" + i).val());                          //(夫姓) 母
                purdue2_lastname_Tag.push($("#bless_ServiceC_last_name_" + i).val());                        //(名字)府君
                purdue2_reason_Tag.push($("#bless_ServiceC_reason_name_" + i).val());                        //超薦事由
                purdue2_licenseNum_Tag.push($("#bless_ServiceC_licenseNum_name_" + i).val());                //車牌(車牌數字)
                purdue2_zipCode_Tag.push($("#bless_ServiceC_death_zipcode_" + i).val());                     //被超薦者郵遞區號
                purdue2_county_Tag.push($("select[name='bless_ServiceC_death_county_" + i + "']").val());    //被超薦者縣市
                purdue2_dist_Tag.push($("select[name='bless_ServiceC_death_district_" + i + "']").val());    //被超薦者區域
                purdue2_addr_Tag.push($("#bless_ServiceC_death_address_" + i).val());                        //被超薦者部分地址
            }
            else if (purdueStringB != "") {
                purdue_deathname_Tag.push($("#bless_ServiceB_death_name_" + i).val());                      //亡者姓名
                purdue_firstname_Tag.push($("#bless_ServiceB_first_name_" + i).val());                      //顯考(姓氏)公
                purdue_momname_Tag.push($("#bless_ServiceB_mom_name_" + i).val());                          //(夫姓) 母
                purdue_lastname_Tag.push($("#bless_ServiceB_last_name_" + i).val());                        //(名字)府君
                purdue_reason_Tag.push($("#bless_ServiceB_reason_name_" + i).val());                        //超薦事由
                purdue_licenseNum_Tag.push($("#bless_ServiceB_licenseNum_name_" + i).val());                //車牌(車牌數字)
                purdue_zipCode_Tag.push($("#bless_ServiceB_death_zipcode_" + i).val());                     //被超薦者郵遞區號
                purdue_county_Tag.push($("select[name='bless_ServiceB_death_county_" + i + "']").val());    //被超薦者縣市
                purdue_dist_Tag.push($("select[name='bless_ServiceB_death_district_" + i + "']").val());    //被超薦者區域
                purdue_addr_Tag.push($("#bless_ServiceB_death_address_" + i).val());                        //被超薦者部分地址
            }
            else if (purdueStringC != "") {
                purdue_deathname_Tag.push($("#bless_ServiceC_death_name_" + i).val());                      //亡者姓名
                purdue_firstname_Tag.push($("#bless_ServiceC_first_name_" + i).val());                      //顯考(姓氏)公
                purdue_momname_Tag.push($("#bless_ServiceC_mom_name_" + i).val());                          //(夫姓) 母
                purdue_lastname_Tag.push($("#bless_ServiceC_last_name_" + i).val());                        //(名字)府君
                purdue_reason_Tag.push($("#bless_ServiceC_reason_name_" + i).val());                        //超薦事由
                purdue_licenseNum_Tag.push($("#bless_ServiceC_licenseNum_name_" + i).val());                //車牌(車牌數字)
                purdue_zipCode_Tag.push($("#bless_ServiceC_death_zipcode_" + i).val());                     //被超薦者郵遞區號
                purdue_county_Tag.push($("select[name='bless_ServiceC_death_county_" + i + "']").val());    //被超薦者縣市
                purdue_dist_Tag.push($("select[name='bless_ServiceC_death_district_" + i + "']").val());    //被超薦者區域
                purdue_addr_Tag.push($("#bless_ServiceC_death_address_" + i).val());                        //被超薦者部分地址
            }

            //purdue_deathname_Tag.push($("#bless_ServiceA_death_name_" + i).val());                          //亡者姓名
            //purdue_firstname_Tag.push($("#bless_ServiceA_first_name_" + i).val());                          //顯考(姓氏)公
            //purdue_momname_Tag.push($("#bless_ServiceA_mom_name_" + i).val());                              //(夫姓) 母
            //purdue_lastname_Tag.push($("#bless_ServiceA_last_name_" + i).val());                            //(名字)府君
            //purdue_reason_Tag.push($("#bless_ServiceA_reason_name_" + i).val());                            //超薦事由
            //purdue_licenseNum_Tag.push($("#bless_ServiceA_licenseNum_name_" + i).val());                    //車牌(車牌數字)
            //purdue_zipCode_Tag.push($("#bless_ServiceA_death_zipcode_" + i).val());                         //被超薦者郵遞區號
            //purdue_county_Tag.push($("select[name='bless_ServiceA_death_county_" + i + "']").val());        //被超薦者縣市
            //purdue_dist_Tag.push($("select[name='bless_ServiceA_death_district_" + i + "']").val());        //被超薦者區域
            //purdue_addr_Tag.push($("#bless_ServiceA_death_address_" + i).val());                            //被超薦者部分地址

            //purdue_deathname_Tag.push($("#bless_ServiceB_death_name_" + i).val());                          //亡者姓名
            //purdue_firstname_Tag.push($("#bless_ServiceB_first_name_" + i).val());                          //顯考(姓氏)公
            //purdue_momname_Tag.push($("#bless_ServiceB_mom_name_" + i).val());                              //(夫姓) 母
            //purdue_lastname_Tag.push($("#bless_ServiceB_last_name_" + i).val());                            //(名字)府君
            //purdue_reason_Tag.push($("#bless_ServiceB_reason_name_" + i).val());                            //超薦事由
            //purdue_licenseNum_Tag.push($("#bless_ServiceB_licenseNum_name_" + i).val());                    //車牌(車牌數字)
            //purdue_zipCode_Tag.push($("#bless_ServiceB_death_zipcode_" + i).val());                         //被超薦者郵遞區號
            //purdue_county_Tag.push($("select[name='bless_ServiceB_death_county_" + i + "']").val());        //被超薦者縣市
            //purdue_dist_Tag.push($("select[name='bless_ServiceB_death_district_" + i + "']").val());        //被超薦者區域
            //purdue_addr_Tag.push($("#bless_ServiceB_death_address_" + i).val());                            //被超薦者部分地址

            //purdue_deathname_Tag.push($("#bless_ServiceC_death_name_" + i).val());                          //亡者姓名
            //purdue_firstname_Tag.push($("#bless_ServiceC_first_name_" + i).val());                          //顯考(姓氏)公
            //purdue_momname_Tag.push($("#bless_ServiceC_mom_name_" + i).val());                              //(夫姓) 母
            //purdue_lastname_Tag.push($("#bless_ServiceC_last_name_" + i).val());                            //(名字)府君
            //purdue_reason_Tag.push($("#bless_ServiceC_reason_name_" + i).val());                            //超薦事由
            //purdue_licenseNum_Tag.push($("#bless_ServiceC_licenseNum_name_" + i).val());                    //車牌(車牌數字)
            //purdue_zipCode_Tag.push($("#bless_ServiceC_death_zipcode_" + i).val());                         //被超薦者郵遞區號
            //purdue_county_Tag.push($("select[name='bless_ServiceC_death_county_" + i + "']").val());        //被超薦者縣市
            //purdue_dist_Tag.push($("select[name='bless_ServiceC_death_district_" + i + "']").val());        //被超薦者區域
            //purdue_addr_Tag.push($("#bless_ServiceC_death_address_" + i).val());                            //被超薦者部分地址
        });

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            AppEmail: AppEmail,
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
            purduetype_Tag: JSON.stringify(purduetype_Tag),
            count_Tag: JSON.stringify(count_Tag),
            purdueA_Tag: JSON.stringify(purdueA_Tag),
            purdueB_Tag: JSON.stringify(purdueB_Tag),
            purdueC_Tag: JSON.stringify(purdueC_Tag),
            purdue_deathname_Tag: JSON.stringify(purdue_deathname_Tag),
            purdue_firstname_Tag: JSON.stringify(purdue_firstname_Tag),
            purdue_momname_Tag: JSON.stringify(purdue_momname_Tag),
            purdue_lastname_Tag: JSON.stringify(purdue_lastname_Tag),
            purdue_reason_Tag: JSON.stringify(purdue_reason_Tag),
            purdue_licenseNum_Tag: JSON.stringify(purdue_licenseNum_Tag),
            purdue_zipCode_Tag: JSON.stringify(purdue_zipCode_Tag),
            purdue_county_Tag: JSON.stringify(purdue_county_Tag),
            purdue_dist_Tag: JSON.stringify(purdue_dist_Tag),
            purdue_addr_Tag: JSON.stringify(purdue_addr_Tag),
            purdue2_deathname_Tag: JSON.stringify(purdue2_deathname_Tag),
            purdue2_firstname_Tag: JSON.stringify(purdue2_firstname_Tag),
            purdue2_momname_Tag: JSON.stringify(purdue2_momname_Tag),
            purdue2_lastname_Tag: JSON.stringify(purdue2_lastname_Tag),
            purdue2_reason_Tag: JSON.stringify(purdue2_reason_Tag),
            purdue2_licenseNum_Tag: JSON.stringify(purdue2_licenseNum_Tag),
            purdue2_zipCode_Tag: JSON.stringify(purdue2_zipCode_Tag),
            purdue2_county_Tag: JSON.stringify(purdue2_county_Tag),
            purdue2_dist_Tag: JSON.stringify(purdue2_dist_Tag),
            purdue2_addr_Tag: JSON.stringify(purdue2_addr_Tag),
            listcount: listcount
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

<!-----普度選項變化欄位----->
<script>
    $("#bless_service_1").on("change", function () {
        var value = $(this).val();
        switch (value) {
            case "1":
                //贊普
                $(".Zamp #count_1").show();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").hide();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            case "14":
                //孝道功德主
                $(".Zamp #count_1").hide();
                $(".Salvation #ServiceA_1").show();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").show();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            case "15":
                //光明功德主
                $(".Zamp #count_1").hide();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").show();
                $(".Salvation #ServiceC_1").hide();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            case "16":
                //發心功德主
                $(".Zamp #count_1").hide();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").show();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            case "18":
                //普度白米30台斤
                $(".Zamp #count_1").show();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").hide();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            case "19":
                //普度白米3台斤
                $(".Zamp #count_1").show();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").hide();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
            default:
                $(".Zamp #count_1").hide();
                $(".Salvation #ServiceA_1").hide();
                $(".Salvation #ServiceB_1").hide();
                $(".Salvation #ServiceC_1").hide();

                itemhide('ServiceA', 'tabletA', 1);
                itemhide('ServiceB', 'tabletB', 1);
                itemhide('ServiceC', 'tabletC', 1);
                break;
        }
    });

    $("#bless_tabletA_1").on("change", function () {
        var value = $(this).val();

        if ($(".Salvation #ServiceA_1").is(":visible")) {
            itemhide('ServiceB', '', 1);
        }
        else {
            itemhide('ServiceA', '', 1);
            itemhide('ServiceB', '', 1);
            itemhide('ServiceC', '', 1);
        }
        switch (value) {
            case "顯考O公O府君":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_firstname_1").show();
                $("#bless_ServiceA_lastname_1").show();
                $("#bless_ServiceA_deathaddress_1").show();

                $("#bless_ServiceA_firstname_1" + " label").text("顯考(姓氏)公");
                $("#bless_ServiceA_first_name_1").attr("placeholder", "請輸入顯考(姓氏)公");

                $("#bless_ServiceA_lastname_1" + " label").text("(名字)府君");
                $("#bless_ServiceA_last_name_1").attr("placeholder", "請輸入(名字)府君");
                break;
            case "顯妣O母 氏OO夫人":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_firstname_1").show();
                $("#bless_ServiceA_momname_1").show();
                $("#bless_ServiceA_lastname_1").show();
                $("#bless_ServiceA_deathaddress_1").show();

                $("#bless_ServiceA_momname_1" + " label").text("(夫姓)母");
                $("#bless_ServiceA_mom_name_1").attr("placeholder", "請輸入(夫姓)母");

                $("#bless_ServiceA_firstname_1" + " label").text("(本姓)氏");
                $("#bless_ServiceA_first_name_1").attr("placeholder", "請輸入(本姓)氏");

                $("#bless_ServiceA_lastname_1" + " label").text("(名字)夫人");
                $("#bless_ServiceA_last_name_1").attr("placeholder", "請輸入(名字)夫人");
                break;
            case "O氏歷代祖先":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_firstname_1").show();
                $("#bless_ServiceA_deathaddress_1").show();

                $("#bless_ServiceA_firstname_1" + " label").text("(姓氏)氏");
                $("#bless_ServiceA_first_name_1").attr("placeholder", "請輸入(姓氏)氏");
                break;
            case "空白牌":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_reason_1").show();
                $("#bless_ServiceA_deathaddress_1").show();
                break;
            case "車號OOO車輛誤傷之生靈":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_licenseNum_1").show();
                $("#bless_ServiceA_deathaddress_1").show();
                break;
            case "無緣子女":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_deathname_1").show();
                $("#bless_ServiceA_deathaddress_1").show();

                $("#bless_ServiceA_deathname_1" + " label").text("無緣子女(姓名)");
                $("#bless_ServiceA_death_name_1").attr("placeholder", "請輸入無緣子女(姓名)");
                break;
            case "累劫冤親債主":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_deathaddress_1").show();
                break;
            case "地基主":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_deathaddress_1").show();
                break;
            case "過去飼養一切動物之靈":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_deathname_1").show();
                $("#bless_ServiceA_deathaddress_1").show();

                $("#bless_ServiceA_deathname_1" + " label").text("寵物(姓名)");
                $("#bless_ServiceA_death_name_1").attr("placeholder", "請輸入寵物(姓名)");
                break;
            case "十方法界一切有情眾生":
                itemhide('ServiceA', '', 1);
                $("#bless_ServiceA_deathaddress_1").show();
                break;
        }
    });

    $("#bless_tabletB_1").on("change", function () {
        var value = $(this).val();

        itemhide('ServiceA', '', 1);
        itemhide('ServiceB', '', 1);
        itemhide('ServiceC', '', 1);
        switch (value) {
            case "顯考O公O府君":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_firstname_1").show();
                $("#bless_ServiceB_lastname_1").show();
                $("#bless_ServiceB_deathaddress_1").show();

                $("#bless_ServiceB_firstname_1" + " label").text("顯考(姓氏)公");
                $("#bless_ServiceB_first_name_1").attr("placeholder", "請輸入顯考(姓氏)公");

                $("#bless_ServiceB_lastname_1" + " label").text("(名字)府君");
                $("#bless_ServiceB_last_name_1").attr("placeholder", "請輸入(名字)府君");
                break;
            case "顯妣O母 氏OO夫人":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_firstname_1").show();
                $("#bless_ServiceB_momname_1").show();
                $("#bless_ServiceB_lastname_1").show();
                $("#bless_ServiceB_deathaddress_1").show();

                $("#bless_ServiceB_momname_1" + " label").text("(夫姓)母");
                $("#bless_ServiceB_mom_name_1").attr("placeholder", "請輸入(夫姓)母");

                $("#bless_ServiceB_firstname_1" + " label").text("(本姓)氏");
                $("#bless_ServiceB_first_name_1").attr("placeholder", "請輸入(本姓)氏");

                $("#bless_ServiceB_lastname_1" + " label").text("(名字)夫人");
                $("#bless_ServiceB_last_name_1").attr("placeholder", "請輸入(名字)夫人");
                break;
            case "O氏歷代祖先":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_firstname_1").show();
                $("#bless_ServiceB_deathaddress_1").show();

                $("#bless_ServiceB_firstname_1" + " label").text("(姓氏)氏");
                $("#bless_ServiceB_first_name_1").attr("placeholder", "請輸入(姓氏)氏");
                break;
            case "空白牌":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_reason_1").show();
                $("#bless_ServiceB_deathaddress_1").show();
                break;
            case "車號OOO車輛誤傷之生靈":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_licenseNum_1").show();
                $("#bless_ServiceB_deathaddress_1").show();
                break;
            case "無緣子女":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_deathname_1").show();
                $("#bless_ServiceB_deathaddress_1").show();

                $("#bless_ServiceB_deathname_1" + " label").text("無緣子女(姓名)");
                $("#bless_ServiceB_death_name_1").attr("placeholder", "請輸入無緣子女(姓名)");
                break;
            case "累劫冤親債主":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_deathaddress_1").show();
                break;
            case "地基主":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_deathaddress_1").show();
                break;
            case "過去飼養一切動物之靈":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_deathname_1").show();
                $("#bless_ServiceB_deathaddress_1").show();

                $("#bless_ServiceB_deathname_1" + " label").text("寵物(姓名)");
                $("#bless_ServiceB_death_name_1").attr("placeholder", "請輸入寵物(姓名)");
                break;
            case "十方法界一切有情眾生":
                itemhide('ServiceB', '', 1);
                $("#bless_ServiceB_deathaddress_1").show();
                break;
        }
    });

    $("#bless_tabletC_1").on("change", function () {
        var value = $(this).val();

        if ($(".Salvation #ServiceA_1").is(":visible")) {
            itemhide('ServiceB', '', 1);
        }
        else {
            itemhide('ServiceA', '', 1);
            itemhide('ServiceB', '', 1);
            itemhide('ServiceC', '', 1);
        }
        switch (value) {
            case "顯考O公O府君":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_firstname_1").show();
                $("#bless_ServiceC_lastname_1").show();
                $("#bless_ServiceC_deathaddress_1").show();

                $("#bless_ServiceC_firstname_1" + " label").text("顯考(姓氏)公");
                $("#bless_ServiceC_first_name_1").attr("placeholder", "請輸入顯考(姓氏)公");

                $("#bless_ServiceC_lastname_1" + " label").text("(名字)府君");
                $("#bless_ServiceC_last_name_1").attr("placeholder", "請輸入(名字)府君");
                break;
            case "顯妣O母 氏OO夫人":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_firstname_1").show();
                $("#bless_ServiceC_momname_1").show();
                $("#bless_ServiceC_lastname_1").show();
                $("#bless_ServiceC_deathaddress_1").show();

                $("#bless_ServiceC_momname_1" + " label").text("(夫姓)母");
                $("#bless_ServiceC_mom_name_1").attr("placeholder", "請輸入(夫姓)母");

                $("#bless_ServiceC_firstname_1" + " label").text("(本姓)氏");
                $("#bless_ServiceC_first_name_1").attr("placeholder", "請輸入(本姓)氏");

                $("#bless_ServiceC_lastname_1" + " label").text("(名字)夫人");
                $("#bless_ServiceC_last_name_1").attr("placeholder", "請輸入(名字)夫人");
                break;
            case "O氏歷代祖先":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_firstname_1").show();
                $("#bless_ServiceC_deathaddress_1").show();

                $("#bless_ServiceC_firstname_1" + " label").text("(姓氏)氏");
                $("#bless_ServiceC_first_name_1").attr("placeholder", "請輸入(姓氏)氏");
                break;
            case "空白牌":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_reason_1").show();
                $("#bless_ServiceC_deathaddress_1").show();
                break;
            case "車號OOO車輛誤傷之生靈":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_licenseNum_1").show();
                $("#bless_ServiceC_deathaddress_1").show();
                break;
            case "無緣子女":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_deathname_1").show();
                $("#bless_ServiceC_deathaddress_1").show();

                $("#bless_ServiceC_deathname_1" + " label").text("無緣子女(姓名)");
                $("#bless_ServiceC_death_name_1").attr("placeholder", "請輸入無緣子女(姓名)");
                break;
            case "累劫冤親債主":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_deathaddress_1").show();
                break;
            case "地基主":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_deathaddress_1").show();
                break;
            case "過去飼養一切動物之靈":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_deathname_1").show();
                $("#bless_ServiceC_deathaddress_1").show();

                $("#bless_ServiceC_deathname_1" + " label").text("寵物(姓名)");
                $("#bless_ServiceC_death_name_1").attr("placeholder", "請輸入寵物(姓名)");
                break;
            case "十方法界一切有情眾生":
                itemhide('ServiceC', '', 1);
                $("#bless_ServiceC_deathaddress_1").show();
                break;
        }
    });

    function itemhide(name, Uppername, id) {
        if (Uppername != '') {
            $("#bless_" + Uppername + "_" + id).val('').trigger("change");
        }

        $("#bless_" + name + "_deathname_" + id).hide();
        $("#bless_" + name + "_firstname_" + id).hide();
        $("#bless_" + name + "_momname_" + id).hide();
        $("#bless_" + name + "_lastname_" + id).hide();
        $("#bless_" + name + "_reason_" + id).hide();
        $("#bless_" + name + "_licenseNum_" + id).hide();
        $("#bless_" + name + "_deathaddress_" + id).hide();
    }

    function tabletChange(purdueString, name, id, value_deathname, value_firstname, value_momname, value_lastname, value_reason, value_licenseNum, value_deathCounty, value_deathdist, valu_deathAddr) {
        switch (purdueString) {
            case "顯考O公O府君":
                $("#bless_" + name + "_firstname_" + id).show();
                $("#bless_" + name + "_lastname_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_firstname_" + id + " label").text("顯考(姓氏)公");
                $("#bless_" + name + "_first_name_" + id).attr("placeholder", "請輸入顯考(姓氏)公");
                $("#bless_" + name + "_first_name_" + id).val(value_firstname);

                $("#bless_" + name + "_lastname_" + id + " label").text("(名字)府君");
                $("#bless_" + name + "_last_name_" + id).attr("placeholder", "請輸入(名字)府君");
                $("#bless_" + name + "_last_name_" + id).val(value_lastname);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "顯妣O母 氏OO夫人":
                $("#bless_" + name + "_firstname_" + id).show();
                $("#bless_" + name + "_momname_" + id).show();
                $("#bless_" + name + "_lastname_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_momname_" + id + " label").text("(夫姓)母");
                $("#bless_" + name + "_mom_name_" + id).attr("placeholder", "請輸入(夫姓)母");
                $("#bless_" + name + "_mom_name_" + id).val(value_momname);

                $("#bless_" + name + "_firstname_" + id + " label").text("(本姓)氏");
                $("#bless_" + name + "_first_name_" + id).attr("placeholder", "請輸入(本姓)氏");
                $("#bless_" + name + "_first_name_" + id).val(value_firstname);

                $("#bless_" + name + "_lastname_" + id + " label").text("(名字)夫人");
                $("#bless_" + name + "_last_name_" + id).attr("placeholder", "請輸入(名字)夫人");
                $("#bless_" + name + "_last_name_" + id).val(value_lastname);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "O氏歷代祖先":
                $("#bless_" + name + "_firstname_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_firstname_" + id + " label").text("(姓氏)氏");
                $("#bless_" + name + "_first_name_" + id).attr("placeholder", "請輸入(姓氏)氏");
                $("#bless_" + name + "_first_name_" + id).val(value_firstname);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "空白牌":
                $("#bless_" + name + "_reason_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_reason_name_" + id).val(value_reason);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "車號OOO車輛誤傷之生靈":
                $("#bless_" + name + "_licenseNum_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_licenseNum_name_" + id).val(value_licenseNum);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "無緣子女":
                $("#bless_" + name + "_deathname_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_deathname_" + id + " label").text("無緣子女(姓名)");
                $("#bless_" + name + "_death_name_" + id).attr("placeholder", "請輸入無緣子女(姓名)");
                $("#bless_" + name + "_death_name_" + id).val(value_deathname);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "累劫冤親債主":
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "地基主":
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "過去飼養一切動物之靈":
                $("#bless_" + name + "_deathname_" + id).show();
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_deathname_" + id + " label").text("寵物(姓名)");
                $("#bless_" + name + "_death_name_" + id).attr("placeholder", "請輸入寵物(姓名)");
                $("#bless_" + name + "_death_name_" + id).val(value_deathname);

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
            case "十方法界一切有情眾生":
                $("#bless_" + name + "_deathaddress_" + id).show();

                $("#bless_" + name + "_death_county_" + id).val(value_deathCounty).trigger("change");
                $("#bless_" + name + "_death_district_" + id).val(value_deathdist).trigger("change");
                $("#bless_" + name + "_death_address_" + id).val(valu_deathAddr);
                break;
        }
    }

    function tabletCheced(purdueString, name, id) {
        var reslist;
        var isValid = true;

        switch (purdueString) {
            case "顯考O公O府君":
                reslist = ["bless_" + name + "_first_name_" + id, "bless_" + name + "_last_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "顯妣O母 氏OO夫人":
                reslist = ["bless_" + name + "_mom_name_" + id, "bless_" + name + "_first_name_" + id, "bless_" + name + "_last_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "O氏歷代祖先":
                reslist = ["bless_" + name + "_first_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "空白牌":
                reslist = ["bless_" + name + "_reason_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "車號OOO車輛誤傷之生靈":
                reslist = ["bless_" + name + "_licenseNum_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "無緣子女":
                reslist = ["bless_" + name + "_death_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "累劫冤親債主":
                reslist = ["bless_" + name + "_death_address_" + id];
                break;
            case "地基主":
                reslist = ["bless_" + name + "_death_address_" + id];
                break;
            case "過去飼養一切動物之靈":
                reslist = ["bless_" + name + "_death_name_" + id, "bless_" + name + "_death_address_" + id];
                break;
            case "十方法界一切有情眾生":
                reslist = ["bless_" + name + "_death_address_" + id];
                break;
        }

        reslist.forEach(function (value) {
            if ($("#" + value).val() == '') {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        return isValid;
    }

    // map: 下拉選到哪個文字，要檢查哪些欄位 suffix
    const tabletMap = {
        "顯考O公O府君": ["first_name", "last_name", "death_county", "death_district", "death_address"],
        "顯妣O母 氏OO夫人": ["mom_name", "first_name", "last_name", "death_county", "death_district", "death_address"],
        "O氏歷代祖先": ["first_name", "death_county", "death_district", "death_address"],
        "空白牌": ["reason_name", "death_county", "death_district", "death_address"],
        "車號OOO車輛誤傷之生靈": ["licenseNum_name", "death_county", "death_district", "death_address"],
        "無緣子女": ["death_name", "death_county", "death_district", "death_address"],
        "累劫冤親債主": ["death_county", "death_district", "death_address"],
        "地基主": ["death_county", "death_district", "death_address"],
        "過去飼養一切動物之靈": ["death_name", "death_county", "death_district", "death_address"],
        "十方法界一切有情眾生": ["death_county", "death_district", "death_address"]
    };

    // 通用：由 input 元素取得對應的 label 文字
    function getLabelText($input) {
        const id = $input.attr('id') || '';

        // special case：縣市 / 鄉鎮市區
        if (id.includes('_county_')) return '縣市';
        if (id.includes('_district_')) return '鄉鎮市區';

        // 下面才是既有邏輯
        let $grp = $input.closest('.FormInput');
        let txt = $grp.find('label').first().text().trim();
        if (!txt) {
            $grp.prevAll('.FormInput').each(function () {
                const t = $(this).find('label').first().text().trim();
                if (t) {
                    txt = t;
                    return false;
                }
            });
        }
        return txt.replace(/：|:$/g, '');
    }

    // 驗證單一 Service(A/C) 的子欄位
    function validateTabletFields(purdueString, key, i) {
        const suffixList = tabletMap[purdueString] || [];
        const missing = [];
        let $firstErrEl = null;

        suffixList.forEach(suffix => {
            const $el = $(`#bless_Service${key}_${suffix}_${i}`);
            const v = $el.val()?.trim();
            if (!v) {
                $el.addClass('unfilled');
                if (!missing.includes(suffix)) {
                    missing.push(getLabelText($el));
                }
                if (!$firstErrEl) $firstErrEl = $el;
            } else {
                $el.removeClass('unfilled');
            }
        });

        if (missing.length) {
            showToastAndFocus(
                $firstErrEl,
                missing.join('、') + " 為必填欄位，請重新輸入。"
            );
            return false;
        }
        return true;
    }

</script>
