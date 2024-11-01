<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_h.aspx.cs" Inherits="Temple.Temples.templeService_purdue_h" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="中元普度|新港奉天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_h.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="中元普度|新港奉天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>中元普度|新港奉天宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
        })(window, document, 'script', 'dataLayer', 'GTM-NGRZRR4V');</script>
    <!-- End Google Tag Manager -->
</head>
<body>
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
                    <img src="images/temple/purdue_h.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">新港奉天宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/06/24 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2024/07/31 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>【甲辰年中元普渡。靈寶啟燈消灾拔薦齋壇超度法會】</h2>
                            <p>即日起開始受理贊普。</p>
                            <p>法會日期：農曆七月初六日至七月初八日（國曆8/21~8/23）</p>
                            <p>法會科儀內容：</p>
                            <p>祝燈祈福、赦罪法會、運香叩禱、三官妙經、獻供七獻、普度植福等</p>
                        </div>
                        <div>
                            <p>新港奉天宮甲辰年中元普度靈寶啟燈消灾拔薦齋壇超度法會</p>
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
                            <label>申請人姓名</label><input name="member_name" maxlength="5" type="text" class="required" id="member_name" placeholder="請輸入申請人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>申請人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入申請人電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>申請人信箱</label><input name="member_mail" type="email" class="" id="member_mail" placeholder="請輸入電子信箱(選填)" />
                        </div>
                        <div class="FormInput date birth">
                            <label>農歷生日</label><input name="member_birthday" type="text" class="datapicker required" id="member_birthday" placeholder="請選擇農歷生日" />
                        </div>
                        <div class="FormInput select count">
                            <label>閏月</label>
                            <select name="member_leapMonth" class="required" id="member_leapMonth">
                                <option value="N">非閏月</option>

                                <option value="Y">閏月</option>
                            </select>
                        </div>
                        <div class="FormInput select count">
                            <label>農歷時辰</label>
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
                        <div class="FormInput address">
                            <label>申請人地址</label>
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
                                <textarea name="tp_a" rows="2" cols="20" id="tp_a" placeholder="可填寫同戶姓名及農歷生日；可不填。"></textarea>
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
                                    <label>祈福人姓名</label><input name="bless_name_1" maxlength="5" type="text" class="" id="bless_name_1" placeholder="請輸入祈福人姓名" />
                                </div>
                                <div class="FormInput date birth">
                                    <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker" id="bless_birthday_1" placeholder="請選擇農歷生日" />
                                </div>
                                <div class="FormInput select count">
                                    <label>閏月</label>
                                    <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                                        <option value="N">非閏月</option>

                                        <option value="Y">閏月</option>
                                    </select>
                                </div>
                                <div class="FormInput select count">
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
                                <div class="FormInput address">
                                    <label>祈福人地址</label>
                                    <div class="CusAddress">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county " data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district " data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="" id="bless_address_1" placeholder="請輸入地址" />
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
                                <textarea name="m_btxt" rows="2" cols="20" id="m_btxt"></textarea>
                            </li>
                        </ul>
                        <!--可複製的區塊 //end-->

                        <%--<div class="FormAddList"><a href="javascript:addList();" title="增加祈福人">✚ 增加祈福人</a></div>--%>

                        <div class="Notice">
                            <!--警告說明-->
                        </div>

                        <div class="FormButtom">
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
            alert('親愛的大德您好\n新港奉天宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!');
        }

        $("input[type='tel']").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
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
    var regex = "^民國\\d{2,3}年(0?[1-9]|1[012])月(0?[1-9]|[12][0-9]|3[01])日$";  // 民國日期格式
    $("#subBtn").on("click", function () {
        var isValid = true;
        var isBirth = true;

        // 遍歷每個必填欄位
        $('.required').each(function () {
            var value = $(this).val();
            if (value === '' || value === null) {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        if ($('#item5').is(":checked") == 1) {
            // 遍歷每個必填欄位-有條件 (普度項目=九玄七祖)
            var reslist = ["bless_first_name_1", "bless_death_county_1", "bless_death_district_1", "bless_death_address_1"];

            reslist.forEach(function (value) {
                if ($("#" + value).val() === '') {
                    isValid = false;
                    $(this).addClass('unfilled');
                } else if (value != '' && $(this).hasClass('unfilled')) {
                    $(this).removeClass('unfilled');
                }
            });
        }

        if ($('#item6').is(":checked") == 1) {
            // 遍歷每個必填欄位-有條件 (普度項目=功德迴向往生者)
            var reslist = ["bless_death_name_1", "bless_death_county_2", "bless_death_district_2", "bless_death_address_2"];

            reslist.forEach(function (value) {
                if ($("#" + value).val() === '') {
                    isValid = false;
                    $(this).addClass('unfilled');
                } else if (value != '' && $(this).hasClass('unfilled')) {
                    $(this).removeClass('unfilled');
                }
            });
        }

        for (var i = 4; i < 10; i++) {
            if ($('#item' + i).is(":checked") == 1) {
                // 遍歷每個必填欄位-有條件 (普度項目=拔度法會 (個人))
                var reslist = ["bless_name_1", "bless_county_1", "bless_district_1", "bless_address_1", "bless_birthday_1"];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() === '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }
        }

        if (isValid) {
            // 所有欄位都已填寫
            console.log('所有欄位都已填寫');

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_h();
                }
                else {
                    alert('親愛的大德您好\n新港奉天宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n新港奉天宮 2024普度活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
            }
        } else {
            // 在這裡可以進行表單提交或其他相關處理
            // 有欄位未填寫
            if (isBirth) {
                $(".Notice").text("請檢查上方欄位是否都已填寫。");
                $(".Notice").addClass("active");
            }
            else {
                $(".Notice").text("請檢查上方生日欄位格式是否正確。正確格式：民國xx年xx月xx日");
                $(".Notice").addClass("active");
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
                    alert("此申請人已完成付款動作，請重新申請。")
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

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#member_birthday").val(item.AppBirth);
                    $("#member_leapMonth").val(item.AppLeapMonth);
                    $("#member_birthtime").val(item.AppBirthTime);
                    $("#member_mail").val(item.Email);
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
                    $("#bless_birthday_1").val(item.Birth);
                    $("#bless_leapMonth_1").val(item.LeapMonth);
                    $("#bless_birthtime_1").val(item.BirthTime);
                    $("#bless_county_1").val(item.County).trigger("change");
                    $("#bless_district_1").val(item.dist).trigger("change");
                    $("#bless_address_1").val(item.Addr);

                    if (item.PurdueNum != "0") {
                        $("#bless_PurdueNum_1").val(item.PurdueNum);
                    }

                    if (item.RiceNum != "0") {
                        $("#bless_RiceNum_1").val(item.RiceNum);
                    }

                    if (item.mMoneyNum != "0") {
                        $("#bless_mMoneyNum_1").val(item.mMoneyNum);
                    }

                    $("#m_btxt").val(item.Remark);

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

        Appname = $("#member_name").val();                                  //申請人姓名
        Appmobile = $("#member_tel").val()                                  //申請人電話
        AppBirth = $("#member_birthday").val()                              //申請人農歷生日
        AppleapMonth = $("#member_leapMonth").val();                        //閏月 Y-是 N-否
        Appbirthtime = $("#member_birthtime").val();                        //申請人農曆時辰
        AppEmail = $("#member_mail").val()                                  //申請人信箱
        Appcounty = $("select[name='bless_app_county_1']").val()            //申請人縣市
        Appdist = $("select[name='bless_app_district_1']").val()            //申請人區域
        Appaddr = $("#bless_app_address_1").val()                           //申請人地址(部分)
        AppzipCode = $("#bless_app_zipcode_1").val();                       //申請人郵遞區號

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
        Birth = $("#bless_birthday_1").val()                                //農歷生日
        leapMonth = $("#bless_leapMonth_1").val();                          //閏月 Y-是 N-否
        birthtime = $("#bless_birthtime_1").val();                          //農曆時辰
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
        m_btxt = $("#m_btxt").val();                                        //備註說明

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            AppEmail: AppEmail,
            AppBirth: AppBirth,
            AppleapMonth: AppleapMonth,
            Appbirthtime: Appbirthtime,
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
            Birth: Birth,
            leapMonth: leapMonth,
            birthtime: birthtime,
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
            m_btxt: m_btxt
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
