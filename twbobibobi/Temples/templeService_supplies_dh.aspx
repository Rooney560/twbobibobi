<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies_dh.aspx.cs" Inherits="Temple.Temples.templeService_supplies_dh" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
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
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


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
        
        .FormCount {
            margin: 1vw 0;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=16" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                    <li>天貺納福添運法會</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/supplies_dh.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">台東東海龍門天聖宮</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2024/07/05 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2024/07/11 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>【2024甲辰年．開天門『天貺納福添運法會』的獨特】</h2>
                            </div>
                            <div>
                                <p>活動日期：07月11日甲辰龍年6月初6日(四) 時間:晚上07：30 虔敬設案焚香乞神憐 燃燈照亮指引民心安 農曆6月初6，「開天門」在民間被稱為天貺節，「貺」有賜予之意。</p>
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
                                <h2>『32天帝燈』</h2>
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
                                <p>每份科儀為：600元整。虎爺制邪燈+招貴人疏文+蓮花+金紙。<span id="supplies8" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                            </div>
                        </div>
                        <p><a href="line://ti/p/@bobibobi.tw" target="_blank">line好友募集中！
現在只要加入保必保庇line好友並填寫註冊資料，就送錢母小紅包！數量有限，敬請把握喔！<img src="https://bobibobi.tw/Temples/images/community_icon_02.png" style="width: 36px; display:inline;" width="45" height="45" alt="" /></a></p>
                    </div>
                </div>

                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>申請人姓名</label><input name="member_name" type="text" class="required" id="member_name" placeholder="請輸入申請人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>申請人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個天貺納福添運法會對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
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
                                <div class="FormInput address">
                                    <label>祈福人地址</label>
                                    <div class="CusAddress">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="required" id="bless_address_1" placeholder="請輸入祈福人地址"/>
                                </div>
                                <div class="FormInput select FormCount">
                                    <label>活動項目</label>
                                    <span>【開天門】 - 三十二天帝燈 $3000</span>
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
                                    <span>【開天門】 - 黑虎將軍補財庫 $800</span>
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
                                    <span>【開天門】 - 消災解厄科儀 $600</span>
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
                                    <span>【開天門】 - 身體康健科儀 $600</span>
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
                                    <span>【開天門】 - 補運科儀 $600</span>
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
                                    <span>【開天門】 - 補財庫科儀 $600</span>
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
                                    <span>【開天門】 - 補文昌科儀 $600</span>
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
                                    <span>【開天門】 - 招貴人科儀 $600</span>
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
                            </li>

                        </ul>
                        <!--可複製的區塊 //end-->

                        <div class="FormAddList"><a href="javascript:addList();" title="增加祈福人">✚ 增加祈福人</a></div>

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
            alert('親愛的大德您好\n台東東海龍門天聖宮 2024天貺納福添運法會已截止！！\n感謝您的支持, 謝謝!');
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
            $('.InputGroup > li:last').find('input').each(function (index) {
                var originalId = $(this).attr('id');
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);
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
    $("#subBtn").on("click", function () {
        var listcount = $('.InputGroup > li').last().attr('bless-id');
        var isValid = true;

        // 遍歷每個數量欄位
        $('.count').each(function () {
            var value = $(this).find("option:selected").text();
            isValid = false;
            $(this).addClass('unfilled');
            if (value == '1') {
                isValid = true;
                $('.count').removeClass('unfilled');
                return false;
            }
        });

        // 遍歷每個必填欄位
        $('.required').each(function () {
            var value = $(this).val();
            if (value === '') {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        // 遍歷每個必填欄位
        for (var i = 1; i <= listcount; i++) {
            var value_birth = $("#bless_birthday_" + i).val();
            var value_sbirth = $("#bless_sbirthday_" + i).val();

            if (value_birth == '' && value_sbirth == '') {
                isValid = false;
                $('.required2').addClass('unfilled');
            } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                $('.required2').removeClass('unfilled');
            }
        }

        if (isValid) {
            // 所有欄位都已填寫
            console.log('所有欄位都已填寫');
            //alert("活動尚未開始!");

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_dh();
                }
                else {
                    alert('親愛的大德您好\n台東東海龍門天聖宮 2024天貺納福添運法會已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n台東東海龍門天聖宮 2024天貺納福添運法會尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
            }
        } else {
            // 在這裡可以進行表單提交或其他相關處理
            // 有欄位未填寫
            $(".Notice").text("請檢查上方欄位是否都已填寫。");
            $(".Notice").addClass("active");
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

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    $("#bless_sbirthday_" + index).val(item.sBirth);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_count1_" + index).val(item.Count1);
                    $("#bless_count2_" + index).val(item.Count2);
                    $("#bless_count3_" + index).val(item.Count3);
                    $("#bless_count4_" + index).val(item.Count4);
                    $("#bless_count5_" + index).val(item.Count5);
                    $("#bless_count6_" + index).val(item.Count6);
                    $("#bless_count7_" + index).val(item.Count7);
                    $("#bless_count8_" + index).val(item.Count8);

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

        Appname = $("#member_name").val();                      //申請人姓名
        Appmobile = $("#member_tel").val();                     //申請人電話

        name_Tag = [];
        mobile_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];

        count1_Tag = [];
        count2_Tag = [];
        count3_Tag = [];
        count4_Tag = [];
        count5_Tag = [];
        count6_Tag = [];
        count7_Tag = [];
        count8_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農歷生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirthday_" + i).val());                                  //祈福人國歷生日
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址

            count1_Tag.push($("select[name='bless_count1_" + i + "']").val());                  //三十二天帝燈
            count2_Tag.push($("select[name='bless_count2_" + i + "']").val());                  //黑虎將軍補財庫
            count3_Tag.push($("select[name='bless_count3_" + i + "']").val());                  //消災解厄科儀
            count4_Tag.push($("select[name='bless_count4_" + i + "']").val());                  //身體康健科儀
            count5_Tag.push($("select[name='bless_count5_" + i + "']").val());                  //補運科儀
            count6_Tag.push($("select[name='bless_count6_" + i + "']").val());                  //補財庫科儀
            count7_Tag.push($("select[name='bless_count7_" + i + "']").val());                  //補文昌科儀
            count8_Tag.push($("select[name='bless_count8_" + i + "']").val());                  //招貴人科儀
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            count1_Tag: JSON.stringify(count1_Tag),
            count2_Tag: JSON.stringify(count2_Tag),
            count3_Tag: JSON.stringify(count3_Tag),
            count4_Tag: JSON.stringify(count4_Tag),
            count5_Tag: JSON.stringify(count5_Tag),
            count6_Tag: JSON.stringify(count6_Tag),
            count7_Tag: JSON.stringify(count7_Tag),
            count8_Tag: JSON.stringify(count8_Tag),
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
