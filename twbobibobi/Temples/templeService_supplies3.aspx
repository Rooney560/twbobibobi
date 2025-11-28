<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies3.aspx.cs" Inherits="Temple.Temples.templeService_supplies3" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="企業補財庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_supplies3.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="企業補財庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>企業補財庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
        
    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>
    <style>
        .icon {
            width: 600px;
        }
        
        .FormInput label.emailalert {
            display: inline-block;
        }

        @media (max-width:720px) {
            .icon {
                width: 100%;
            }

            .FormInput label.emailalert {
                display: none;
            }
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
                    <li><a href="templeInfo.aspx?a=6" title="北港武德宮">北港武德宮</a></li>
                    <li>企業補財庫</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/supplies3_wu.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">企業補財庫 X 北港武德宮</h1>
                <div class="TempleServiceInfo">
                    <%--<div class="EventTime">
                        <div>活動開始日期：</div>
                        <div>2023/10/15 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div>2023/11/21 23:59</div>
                    </div>--%>
                    <div class="EventServiceContent">
                        <div>
                            <h2>▌活動內容：</h2>
                            <p>企業補庫免煩惱,<span style="color: red;">遠傳電信</span>獨家與全臺最大財神廟<span style="color: red;">『北港武德宮』</span>合作，推出企業補財庫，提供   月月補庫2次(初一、十五各一次)，讓您在打拼事業的同時，有如神助~業績節節高升!!
</p>
                        </div>
                        <div>
                            <h2>▌參加辦法：</h2>
                            <p>只要在下方填好公司資料 or 負責人資料，完成線上報名，我方會將報名資料傳給北港武德宮，廟方將會在每月初一與十五幫貴公司補財庫。(補財庫費用每月1300元）</p>
                        </div>
                        <div>
                            <h2>▌注意事項</h2>
                            <p>1.	付費只能使用遠傳電信帳單，費用為每月1300元</p>
                            <p>2.	此服務為訂閱式服務，訂閱後將每個月從遠傳電信帳單中收取費用</p>
                            <p>3.	如欲取消請提早一個月來電取消，客服電話：04-36092299</p>
                            <p>4.	每月26號(農曆)前報名，補財庫於次月農曆初一開始第一次，十五第二次</p>
                            <p>5.	訂閱後每月26號(農曆)收費，收費成功才會將資料給予北港武德宮進行次月的補財庫服務</p>
                            <p>6.	收費失敗將有專人撥打聯繫電話聯絡，如聯繫不上將視為取消服務</p>
                            <p>7.	北港武德宮 推行"無紙功德"環保理念" 原紙本感謝狀之提供改為 Email提供電子感謝狀。</p>
                        </div>
                        <input type="button" id="subBtn2" class="subBtn" value="報名按鈕"/>
                        <br />
                        <%--<div>
                            <h2>▌降文開示-庚寅年</h2>
                            <p>度功甘露指三千</p>
                        </div>--%>
                    </div>
                </div>


                <!--訂購表單-->
                <!--說明：
            1.必填欄位請於input或select增加class="required"。
            2.需動態產生表單，請使用<ul class="InputGroup">包覆，搭配<li bless-id="{編號}">使用。
            3.每個欄位呈現為<div class="FormInput {項目}">，項目請由下方自行挑選複製使用，若有缺的話，亦可通知補上。
            4.因欄位搭配很多JS的生成及檢核，若有使用到"地址"及"生日(或日期)"的部份，需特別注意JS的部份。
        -->
                <div id="OrderForm" runat="server" class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>
                        <div class="FormInput text_s">
                            <label>購買人Email</label><input name="member_email" type="text" class="required" id="member_email" placeholder="請輸入購買人Email(必填)"/>
                        </div>
                        <div class="FormInput">
                            <label class="emailalert"></label>
                            <span style="color: red;">北港武德宮 推行"無紙功德"環保理念" 原紙本感謝狀之提供改為 Email提供電子感謝狀。</span>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">
                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" id="bless_name_1" placeholder="請輸入負責人姓名or公司名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
                                </div>
                                <div class="FormInput select">
                                    <label>性別</label>
                                    <select name="bless_sex_1" class="required" id="bless_sex_1">
                                        <option selected="selected" value>請選擇</option>
                                        <option value="善男">善男</option>
                                        <option value="信女">信女</option>
                                    </select>
                                </div>
                                <div class="FormInput date birth">
                                    <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker required" id="bless_birthday_1" placeholder="請選擇農歷生日"/>
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
                                <div class="FormInput tel">
                                    <label>市話</label><input name="bless_homenum_1" type="tel" class="" id="bless_homenum_1" placeholder="請輸入市話(選填)"/>
                                </div>
                                <div class="FormInput email mail">
                                    <label>Email</label><input name="bless_email_1" type="text" class="" id="bless_email_1" placeholder="請輸入Email(選填)"/>
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
                                    <label>服務項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="3">企業補財庫 (1300/月)</option>
                                    </select>
                                </div>
                                <div class="FormInput text_s">
                                    <label>備註</label><textarea name="bless_Remark_1" type="text" class="" id="bless_Remark_1" ></textarea>
                                </div>
                            </li>

                        </ul>
                        <!--可複製的區塊 //end-->

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

        //if (!checkEndTime()) {
        //    alert('親愛的大德您好\n北港伍德公 企業補財庫活動已截止！！\n感謝您的支持, 謝謝!');
        //}

        $("#subBtn2").on("click", function () {
            location = 'https://templeonline.fetnet.net/worship?merechandiseId=GSPYCPA000032505000045&brandId=CPA00003&categoryId=pudu_rc&_ga=2.158792378.1274450527.1750402841-1606902412.1678867216';
        });

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
        console.log("bless-id-" + lastblessNum);


        var newField = originalField.clone();
        //newField.find('input, select').val('');

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
        });

        $('.InputGroup > li:last').find('textarea').each(function (index) {
            var originalId = $(this).attr('id');
            var newId = originalId.slice(0, -1) + lastblessNum;
            $(this).attr('id', newId);
            $(this).attr('name', newId);
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

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

</script>

<!-----必填欄位檢查----->
<script>
    $("#subBtn").on("click", function () {
        var listcount = $('.InputGroup > li').last().attr('bless-id');
        var isValid = true;
        var isValid_Mobile = true;
        var isValid_dist = true;
        var isValid_birth = true;
        var isCheckedValid = $("#checkedprivate").is(":checked");

        var value = $("#member_tel").val().trim();
        var value_email = $("#member_email").val().trim();
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
            $(".Notice").text("購買人Email不能為空。");
            $(".Notice").addClass("active");
            $("#member_email").addClass('unfilled');
            $("#member_email").focus();
        }
        else if (!IsEmail(value_email)) {
            $(".Notice").text("購買人Email格式錯誤。");
            $(".Notice").addClass("active");
            $("#member_email").addClass('unfilled');
            $("#member_email").focus();
        }
        else {
            if (value != '' && $("#member_tel").hasClass('unfilled')) {
                $("#member_tel").removeClass('unfilled');
            }

            for (var i = 1; i <= listcount; i++) {
                var value_birth = $("#bless_birthday_" + i).val();
                var value_sbirth = $("#bless_sbirthday_" + i).val();

                if (value_birth == '' && value_sbirth == '') {
                    $(".Notice").text("祈福人農曆生日或國曆生日不能為空。");
                    $(".Notice").addClass("active");
                    $('.required2').addClass('unfilled');

                    isValid = false;
                    isValid_birth = false;
                    break;
                } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                    $('.required2').removeClass('unfilled');
                }

                value = $("#bless_tel_" + i).val().trim();
                //value_email = $("#bless_email_" + i).val().trim();
                if (value == "") {
                    $(".Notice").text("祈福人電話不能為空。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid_Mobile = false;
                    break;
                }
                else if (!Isphone(value)) {
                    $(".Notice").text("祈福人電話格式錯誤。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid_Mobile = false;
                    break;
                }
                else {
                    if (value != '' && $("#bless_tel_" + i).hasClass('unfilled')) {
                        $("#bless_tel_" + i).removeClass('unfilled');
                    }
                }

                if ($("#bless_oversea_" + i).val() == "1") {
                    value = $("#bless_county_" + i).val();
                    if (value == '' || value == null) {
                        $(".Notice").text("祈福人地址 縣市為空，請重新選擇縣市。");
                        $(".Notice").addClass("active");
                        $("#bless_county_" + i).addClass('unfilled');

                        isValid = false;
                        isValid_dist = false;
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
                        isValid_dist = false;
                        break;
                    }
                    else {
                        if (value != '' && $("#bless_district_" + i).hasClass('unfilled')) {
                            $("#bless_district_" + i).removeClass('unfilled');
                        }
                    }
                }
            }

            if (isValid_Mobile && isValid_dist && isValid_birth) {

                // 遍歷每個必填欄位
                $('.required').each(function () {
                    var value = $(this).val().trim();
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

                    gotoChecked_wu();
                    //if (checkedStartTime()) {
                    //    if (checkEndTime()) {
                    //        gotoChecked_wu();
                    //    }
                    //    else {
                    //        alert('親愛的大德您好\n北港武德宮 2025天官武財神聖誕補財庫活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    //    }
                    //}
                    //else {
                    //    alert('親愛的大德您好\n北港武德宮 2025天官武財神聖誕補財庫尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    //}
                }
            } else {
                // 在這裡可以進行表單提交或其他相關處理
                // 有欄位未填寫
                if (!isValid) {
                    if (isValid_Mobile && isValid_dist && isValid_birth) {
                        $(".Notice").text("請檢查上方欄位是否都已填寫。");
                        $(".Notice").addClass("active");
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
            for (var i = 1; i < res.listcount; i++) {
                addList();
            }

            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);
            $("#member_email").val(res.AppEmail);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
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
        var value = $(this).val();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

    function gotoChecked_wu() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();      //購買人姓名
        Appmobile = $("#member_tel").val()      //購買人電話
        Appemail = $("#member_email").val()      //購買人電話

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
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農歷生日
            birthTime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-閏月 N-非閏月
            homenum_Tag.push($("#bless_homenum_" + i).val());                                   //市話
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人email
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            remark_Tag.push($("#bless_Remark_" + i).val());                                     //備註

            var suppliestype = $("select[name='bless_service_" + i + "']").val();               //服務項目
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

