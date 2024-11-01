<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lights_Fu.aspx.cs" Inherits="Temple.Temples.templeService_lights_Fu" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="祈福點燈|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lights_Fu.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:site_name" content="祈福點燈|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lights_Fu_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lights_Fu_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lights_Fu_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>祈福點燈|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
        @media only screen and (max-width: 720px) {
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=8" title="西螺福興宮">西螺福興宮</a></li>
                    <li>祈福點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lights_Fu_2025.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">歡迎使用《西螺福興宮》線上點燈服務</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/11/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/10/31 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <h2>點燈祈福時間：每年農曆1/9「天公生」啟燈 - 農曆12/24「送神日」左右謝燈</h2>
                        <br />
                        <div>
                            <h2>光明燈</h2>
                            <p>本宮為配合信仰需要，於元辰殿設光明燈，供善男信女點燈。祈求逢凶化吉、解除災厄、男增百福、女納千祥、元辰光彩、消災解厄、五穀豐收、財源廣進、
                                軍中安康、事業發達、植福延壽、闔家平安，費用600元。
                                <span id="light1" style="color:red; display:none;">(已額滿)</span></p>
                        </div>
                        <div>
                            <h2>安太歲</h2>
                            <p>太歲即為「歲星」、「年神」，每年依照六十甲子流年，搭配十二地支（生肖）輪值，六十甲子皆有專屬星君輪值，主司輪值流年的吉凶禍福，所謂「太歲當
                                頭座、無福恐有禍」。為祈求流年平安，凡是太歲星君輪值當年、正沖、對沖、偏沖者，需安奉太歲星君，祈願流年運勢順遂、諸事平安，費用300元。
                                <span id="light2" style="color:red; display:none;">(已額滿)</span></p>
                            <p>民國一一四年歲次乙巳，值年太歲吳遂星君。凡家中生肖屬蛇（太歲輪值），屬豬（正沖），屬虎、猴（對沖），屬雞、羊、兔、牛（偏沖）者，須請安奉太歲星君。</p>
                        </div>
                        <div>
                            <h2>財神燈</h2>
                            <p>天官武財神趙公明、天官文財神比干兩位財神，主司文武財運及財庫。在財神殿安奉「財神燈」，祈願在工作事業上，營利運作、業務投資等，獲得到文武財神
                                的護佑，營運利市、利祿亨通、財源廣進、工商倍利，費用600元。
                                <span id="light3" style="color:red; display:none;">(已額滿)</span></p>
                        </div>
                        <div>
                            <h2>藥師佛燈</h2>
                            <p>釋迦佛祖殿安奉「藥師琉璃光如來」佛像，亦在佛祖殿旁設「藥師佛燈」，供信眾點燈供佛。以宣揚「藥師琉璃光如來」的十二大願，祈願善男子善女人在藥師佛
                                本願的護佑與加持，得以身居善處安住、清淨無礙、無諸疾苦、資具豐足、消災延壽，費用600元。<span id="light4" style="color:red; display:none;">(已額滿)</span></p>
                        </div>
                        <div>
                            <h2>文昌燈</h2>
                            <p>文昌帝君、魁斗星君主司丹籍桂簿、功名利祿、文運智慧、祿馬貴人。在文昌殿安奉「文昌燈」，學子祈願求學讀書、升學考試、檢定認證等，得以文運昌盛、考
                                試順利、名標金榜。出社會工作者，祈願祿馬貴人扶助、工作順利、步步高升、前途光明、事業成功，費用600元。
                                <span id="light5" style="color:red; display:none;">(已額滿)</span></p>
                        </div>
                        <div>
                            <h2>觀音佛祖燈</h2>
                            <p>觀音佛祖以慈悲與智慧度化世人，護祐善男子善女人福慧增長所願如意。於觀音佛祖殿內點燈供養，祈願慧日普照、福慧增長、護身吉祥。費用600元。
                                <span id="light6" style="color:red; display:none;">(已額滿)</span></p>
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
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" maxlength="5" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每種點燈項目對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" maxlength="5" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
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
                                <div class="FormInput email mail">
                                    <label>祈福人信箱</label><input name="bless_email_1" type="text" class="" id="bless_email_1" placeholder="請輸入祈福人Email(選填)"/>
                                </div>
                                <div class="FormInput address">
                                    <label>地址</label>
                                    <div class="CusAddress">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="required" id="bless_address_1" placeholder="請輸入地址"/>
                                </div>
                                <div class="FormInput select">
                                    <label>點燈項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value>請選擇</option>
                                        <option value="光明燈">光明燈 $600</option>
                                        <option value="安太歲">安太歲 $300</option>
                                        <option value="財神燈">財神燈 $600</option>
                                        <option value="藥師佛燈">藥師佛燈 $600</option>
                                        <option value="文昌燈">文昌燈 $600</option>
                                        <option value="觀音佛祖燈">觀音佛祖燈 $600</option>
                                    </select>
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

        if (!checkEndTime()) {
            alert('親愛的大德您好\n西螺福興宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!');
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
</script>

<!-----增減祈福人----->
<script>
    var originalField = $('.InputGroup > li').first().clone();

    //增加
    function addList() {
        var lastblessNum = parseInt($('.InputGroup > li').last().attr('bless-id')) + 1;
        console.log(lastblessNum);


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
        var isValid = true;
        var isCheckedValid = $("#checkedprivate").is(":checked");

        var listcount = $('.InputGroup > li').last().attr('bless-id');

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
            var value_sbirth = $("#bless_sbirth_" + i).val();

            if (value_birth == '' && value_sbirth == '') {
                isValid = false;
                $('.required2').addClass('unfilled');
            } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                $('.required2').removeClass('unfilled');
            }
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

                if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                    if (checkEndTime()) {
                        gotoChecked_Fu();
                    }
                    else {
                        alert('親愛的大德您好\n西螺福興宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    }
                }
                else {
                    alert('親愛的大德您好\n西螺福興宮 2025點燈活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
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
        } else {
            if (res.overnumType == 3) {
                alert("光明燈已額滿，請重新填單。或至其他宮廟點燈。");
            }
            else if (res.overnumType == 4) {
                alert("太歲燈已額滿，請重新填單。或至其他宮廟點燈。");
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
            for (var i = 1; i < res.listcount; i++) {
                addList();
            }

            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);
                    //$("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    //$("#bless_sBirth_" + index).val(item.sBirth);
                    $("#bless_email_" + index).val(item.Email);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_service_" + index).val(item.LightsString);

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

    function gotoChecked_Fu() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();      //購買人姓名
        Appmobile = $("#member_tel").val()      //購買人電話

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        email_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        LightsString_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val());                                     //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人信箱
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            LightsString_Tag.push($("#bless_service_" + i).val());                              //服務項目
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            email_Tag: JSON.stringify(email_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            LightsString_Tag: JSON.stringify(LightsString_Tag),
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