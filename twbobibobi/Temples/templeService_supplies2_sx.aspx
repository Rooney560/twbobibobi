﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies2_sx.aspx.cs" Inherits="twbobibobi.Temples.templeService_supplies2_sx" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="神霄玉府財神會館|供香轉運|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_supplies2_sx.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="神霄玉府財神會館|供香轉運|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/supplies2_sx.jpg?t=55282" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/supplies2_sx.jpg?t=55282" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/supplies2_sx.jpg?t=55282" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>神霄玉府財神會館|供香轉運|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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

        .select select {
            width: calc(17vw + 10px);
        }

        .text_s input, .tel input, .mail input, .date input {
            width: 17vw;
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

            .select select {
                width: 100%;
            }

            .text_s input, .tel input, .mail input, .date input {
                width: calc(100% - 12px);
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
                    <li><a href="#" title="神霄玉府財神會館">神霄玉府財神會館</a></li>
                    <li>供香轉運</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="https://bobibobi.tw/Temples/images/temple/supplies2_sx.jpg?t=55828" width="1160" height="550" alt="小龍蛇年旺財氣，供香轉運好運到" title="小龍蛇年旺財氣，供香轉運好運到" />
                </div>
                <h1 class="TempleName">歡迎使用《神霄玉府財神會館》供香轉運線上報名服務 </h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <%--<div class="EventTime">
                            <div>活動開始日期：</div>
                            <div id="startTime">2025/01/10 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2025/02/03 23:59</div>
                        </div>--%>
                        <div class="EventServiceContent">
                            <div>
                                <p>好香從這輩子開始燒起，廣結善緣從供香開始</p>
                                <p>為什麼要供香？除了供給神明外，當你供香給宮廟，其他信眾也會使用到您供的香，在無形之中與人結緣，而形成一個美好的循環，久而久之您的磁場也會慢慢的改善。</p>
                                <p>我們的香都是使用沉香，另外我們會請道長幫您念疏文，供香給神明。</p>
                                <p>「香」，是有形的人們對無形神聖的虔心禮敬與供奉；</p>
                                <p>「香」，上通天下達地，上可供諸天神佛，下能施六道眾生。</p>
                                <p>「供香」，不只是您添油香的功德，每一份供香，就如同您親自在廟裡燃點上等水沉香，透過您的虔心，透過大眾的雙手，延續神祇濟世救民的香火!</p>
                            </div>
                        </div>
                        <div class="TempleImg">
                            <img src="images/temple/supplies2_sx_01.jpg?t=55828" width="1160" height="550" alt="保必保庇提供神霄玉府財神會館供香轉運" 
                                title="保必保庇提供神霄玉府財神會館供香轉運" />
                            <img src="images/temple/supplies2_sx_02.jpg?t=55828" width="1160" height="550" alt="保必保庇提供神霄玉府財神會館供香轉運" 
                                title="保必保庇提供神霄玉府財神會館供香轉運" />
                            <img src="images/temple/supplies2_sx_03.jpg?t=55828" width="1160" height="550" alt="保必保庇提供神霄玉府財神會館供香轉運" 
                                title="保必保庇提供神霄玉府財神會館供香轉運" />
                            <img src="images/temple/supplies2_sx_04.jpg?t=55828" width="1160" height="550" alt="保必保庇提供神霄玉府財神會館供香轉運" 
                                title="保必保庇提供神霄玉府財神會館供香轉運" />
                            <img src="images/temple/supplies2_sx_05.jpg?t=55828" width="1160" height="550" alt="保必保庇提供神霄玉府財神會館供香轉運" 
                                title="保必保庇提供神霄玉府財神會館供香轉運" />
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
                        <div class="FormInput text_s">
                            <label>購買人Email</label><input name="member_mail" type="text" class="required" id="member_mail" placeholder="請輸入Email"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個供香轉運對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput select">
                                    <label>服務項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="23">烏日神霄玉府/沉香每台斤 $1200</option>
                                        <option value="24">草屯敦和宮/沉香每台斤 $1500</option>
                                        <option value="25">竹山紫南宮/沉香每台斤 $1500</option>
                                        <option value="26">竹山金天宮（老袓廟）/沉香每台斤 $1500</option>
                                        <option value="27">埔里地母廟/沉香每台斤 $1500</option>
                                        <option value="28">松柏嶺受天宮/沉香每台斤 $1500</option>
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
                                <div class="FormInput email mail">
                                    <label>祈福人信箱</label><input name="bless_email_1" type="text" class="" id="bless_email_1" placeholder="請輸入祈福人Email(選填)"/>
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
                                    並已取得當事人同意，為「保必保庇線上宮廟服務平台」之所有交易行為，新薪網元得基於
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

        //$(".img1").show();
        //$(".img2").hide();

        //var getUrlString = location.href;
        //var url = new URL(getUrlString);

        //if (url.searchParams.get('ad') == '2') {
        //    $(".img1").hide();
        //    $(".img2").show();
        //}

        //if (!checkEndTime()) {
        //    alert('親愛的大德您好\n神霄玉府財神會館 2025供香轉運已截止！！\n感謝您的支持, 謝謝!');
        //}

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

        $(".Receive").hide();

        $("#bless_sendback_1").change(function () {
            if ($(this).val() == "Y") {
                $("#bless_receive_1").show();
            }
            else {
                $("#bless_receive_1").hide();
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
    $('.RecAddress').twzipcode({
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
            $('.InputGroup > li:last').find('.Receive').each(function (index) {
                var originalId = $(this).attr('id');
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);
                $(this).hide();
            });

            $('.InputGroup > li:last .RecAddress').find('div[data-role]').each(function (index) {
                var originalId = $(this).attr('data-id');
                var originalName = $(this).attr('data-name');
                var newId = originalId.slice(0, -1) + lastblessNum;
                var newName = originalName.slice(0, -1) + lastblessNum;
                $(this).attr('data-id', newId);
                $(this).attr('data-name', newId);
            });

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
                    $("#" + newId).val('23');
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
            $('.RecAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
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
        var isValid2 = true;
        var isValid3 = true;
        var isValid4 = true;
        var isCheckedValid = $("#checkedprivate").is(":checked");

        var value = $("#member_tel").val().trim();
        var value_email = $("#member_mail").val().trim();
        if (value == "") {
            $(".Notice").text("購買人電話不能為空。");
            $(".Notice").addClass("active");
            $("#member_tel").addClass('unfilled');
        }
        else if (!Isphone(value)) {
            $(".Notice").text("購買人電話格式錯誤。");
            $(".Notice").addClass("active");
            $("#member_tel").addClass('unfilled');
        }
        else if (value_email == "") {
            $(".Notice").text("購買人信箱不能為空。");
            $(".Notice").addClass("active");
            $("#member_mail").addClass('unfilled');
        }
        else if (!IsEmail(value_email)) {
            $(".Notice").text("購買人信箱格式錯誤。");
            $(".Notice").addClass("active");
            $("#member_mail").addClass('unfilled');
        }
        else {
            if (isValid4) {
                if (value != '' && $("#member_tel").hasClass('unfilled')) {
                    $("#member_tel").removeClass('unfilled');
                }

                if (value_email != '' && $("#member_mail").hasClass('unfilled')) {
                    $("#member_mail").removeClass('unfilled');
                }

                for (var i = 1; i <= listcount; i++) {
                    value = $("#bless_tel_" + i).val().trim();
                    if (value == "") {
                        $(".Notice").text("祈福人電話不能為空。");
                        $(".Notice").addClass("active");
                        $("#bless_tel_" + i).addClass('unfilled');

                        isValid = false;
                        isValid2 = false;
                        break;
                    }
                    else if (!Isphone(value)) {
                        $(".Notice").text("祈福人電話格式錯誤。");
                        $(".Notice").addClass("active");
                        $("#bless_tel_" + i).addClass('unfilled');

                        isValid = false;
                        isValid2 = false;
                        break;
                    }
                    else {
                        if (value != '' && $("#bless_tel_" + i).hasClass('unfilled')) {
                            $("#bless_tel_" + i).removeClass('unfilled');
                        }

                        if ($("#bless_oversea_" + i).val() == "1") {
                            value = $("#bless_county_" + i).val();
                            if (value == '' || value == null) {
                                $(".Notice").text("祈福人地址 縣市為空，請重新選擇縣市。");
                                $(".Notice").addClass("active");
                                $("#bless_county_" + i).addClass('unfilled');

                                isValid = false;
                                isValid3 = false;
                                break;
                            }
                            else {
                                if (value != '' && $("#bless_county_" + i).hasClass('unfilled')) {
                                    $("#bless_county_" + i).removeClass('unfilled');
                                }
                            }

                            value = $("#bless_district_" + i).val();
                            if (value == '' || value == null) {
                                $(".Notice").text("祈福人地址 區域為空，請重新選擇區域。");
                                $(".Notice").addClass("active");
                                $("#bless_district_" + i).addClass('unfilled');

                                isValid = false;
                                isValid3 = false;
                                break;
                            }
                            else {
                                if (value != '' && $("#bless_district_" + i).hasClass('unfilled')) {
                                    $("#bless_district_" + i).removeClass('unfilled');
                                }
                            }
                        }

                        var value_birth = $("#bless_birthday_" + i).val();
                        var value_sbirth = $("#bless_sbirthday_" + i).val();

                        if (value_birth == '' && value_sbirth == '') {
                            isValid = false;
                            $('.required2').addClass('unfilled');
                        } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                            $('.required2').removeClass('unfilled');
                        }
                    }
                }

                if (isValid2 && isValid3 && isValid4) {
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
                }

                if (isValid) {
                    if (!isCheckedValid) {
                        $(".Notice").text("請勾選同意隱私權政策使用。");
                        $(".Notice").addClass("active");
                    }
                    else {
                        // 所有欄位都已填寫
                        console.log('所有欄位都已填寫');
                        //alert("活動尚未開始!");

                        gotoChecked_sx();
                        //if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                        //    if (checkEndTime()) {
                        //    }
                        //    else {
                        //        alert('親愛的大德您好\n神霄玉府財神會館 2025補財庫已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                        //    }
                        //}
                        //else {
                        //    alert('親愛的大德您好\n神霄玉府財神會館 2025補財庫尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                        //}
                    }
                } else {
                    // 在這裡可以進行表單提交或其他相關處理
                    // 有欄位未填寫
                    if (!isValid) {
                        if (isValid2 && isValid3 && isValid4) {
                            $(".Notice").text("請檢查上方欄位是否都已填寫。");
                            $(".Notice").addClass("active");
                        }
                    }
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

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);
                    //$("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    //$("#bless_sbirthday_" + index).val(item.sBirth);
                    $("#bless_email_" + index).val(item.Email);
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

                    $("#bless_sendback_" + index).val(item.AppSendback).trigger("change");
                    if (item.AppSendback == "Y") {
                        $("#bless_rec_name_" + index).val(item.ReceiptName);
                        $("#bless_rec_tel_" + index).val(item.ReceiptMobile);
                        $("#bless_rec_county_" + index).val(item.ApprCounty).trigger("change");
                        $("#bless_rec_district_" + index).val(item.Apprdist).trigger("change");
                        $("#bless_rec_address_" + index).val(item.ApprAddr);
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

    function gotoChecked_sx() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                      //購買人姓名
        Appmobile = $("#member_tel").val();                     //購買人電話
        Appemail = $("#member_mail").val();                     //購買人Email

        //sendback_Tag = $("select[name='bless_sendback_1']").val().trim();                          //寄送方式 N-不寄回 Y-寄回(加收運費100元)
        //rname_Tag = $("#bless_rec_name_1").val().trim();                                           //收件人姓名
        //rmobile_Tag = $("#bless_rec_tel_1").val().trim();                                          //收件人電話
        //rzipCode_Tag = $("#bless_rec_zipcode_1").val().trim();                                     //收件人郵政區號
        //rcounty_Tag = $("select[name='bless_rec_county_1']").val().trim();                         //收件人縣市
        //rdist_Tag = $("select[name='bless_rec_district_1']").val().trim();                         //收件人區域
        //raddr_Tag = $("#bless_rec_address_1").val().trim();

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        email_Tag = [];
        homenum_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        SuppliesType_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                                 //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                                //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                                   //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val());                                            //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                                       //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                                       //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirthday_" + i).val());                                          //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val().trim());                                        //祈福人信箱
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
            SuppliesType_Tag.push($("#bless_service_" + i).val().trim());                               //服務項目
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appemail: Appemail,
            //sendback_Tag: sendback_Tag,
            //rname_Tag: rname_Tag,
            //rmobile_Tag: rmobile_Tag,
            //rzipCode_Tag: rzipCode_Tag,
            //rcounty_Tag: rcounty_Tag,
            //rdist_Tag: rdist_Tag,
            //raddr_Tag: raddr_Tag,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            email_Tag: JSON.stringify(email_Tag),
            homenum_Tag: JSON.stringify(homenum_Tag),
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
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
