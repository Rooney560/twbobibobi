<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_dh.aspx.cs" Inherits="Temple.Temples.templeService_purdue_dh" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="中元普度|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_dh.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="中元普度|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>中元普度|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_dh.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">台東東海龍門天聖宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/06/24 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2024/08/28 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>📢農曆七月普渡開放報名📢</h2>
                            <p>‼️歡迎發心護持法會▪️天地人三界結善緣‼️</p>
                            <p>【一燈照萬明➖七月普渡】</p>
                            <p>➖➖➖➖➖➖➖➖➖</p>
                            <p>《發心護持法會項目》</p>
                            <p>🈶普渡供桌供品代訂 一份1,500元(不含金紙)。</p>
                            <p>🈶隨喜功德 $500(冤親債主、歷代祖先、往生親友、地基主、嬰靈)。</p>
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
                            <label>申請人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">
                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個普度項目對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" maxlength="5" type="text" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
                                </div>
                                <div class="FormInput date">
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
                                <div class="FormInput address">
                                    <label>祈福人地址</label>
                                    <div class="CusAddress">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="required" id="bless_address_1" placeholder="請輸入祈福人地址"/>
                                </div>
                                <div class="FormInput select">
                                    <label>普度項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option selected="selected" value>請選擇</option>
                                        <option value="1">贊普 $1500</option>
                                        <option value="2">超薦『歷代祖先』 $500</option>
                                        <option value="3">超薦『往生親友』 $500</option>
                                        <option value="5">超薦『冤親債主』 $500</option>
                                        <option value="6">超薦『嬰靈(無緣子女)』 $500</option>
                                    </select>
                                </div>
                                <div class="Salvation">
                                    <div id="bless_death_1" name="bless_death_1">
                                        <div class="FormInput text_s">
                                            <label>往生親友姓名</label><input name="bless_death_name_1" type="text" class="required3" id="bless_death_name_1" placeholder="請輸入往生親友姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_deathdate_1" name="bless_deathdate_1">
                                        <div class="FormInput date">
                                            <label>往生親友死亡日期</label><input name="bless_death_deathday_1" type="text" class="datapicker required3" id="bless_death_deathday_1" placeholder="請選擇往生親友死亡日期(國曆)" />
                                        </div>
                                    </div>
                                    <div id="bless_firstname_1" name="bless_firstname_1">
                                        <div class="FormInput text_s">
                                            <label>姓氏</label><input name="bless_first_name_1" type="text" class="required4" id="bless_first_name_1" placeholder="請輸入姓氏" />
                                        </div>
                                    </div>
                                    <div id="bless_deathbirth_1" name="bless_deathbirth_1">
                                        <div class="FormInput date">
                                            <label>農歷生日</label><input name="bless_death_birthday_1" type="text" class="datapicker required4" id="bless_death_birthday_1" placeholder="請選擇農歷生日" />
                                        </div>
                                        <div class="FormInput select count">
                                            <label>閏月</label>
                                            <select name="bless_death_leapMonth_1" class="" id="bless_death_leapMonth_1">
                                                <option value="N">非閏月</option>

                                                <option value="Y">閏月</option>
                                            </select>
                                        </div>
                                        <div class="FormInput select count">
                                            <label>農歷時辰</label>
                                            <select name="bless_death_birthtime_1" class="" id="bless_death_birthtime_1">
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
                                    </div>
                                    <div id="bless_deathaddress_1" name="bless_deathaddress_1">
                                        <div class="FormInput address">
                                            <label>牌位所在地址</label>
                                            <div class="DeathAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_death_zipcode_1" data-id="bless_death_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county " data-name="bless_death_county_1" data-id="bless_death_county_1"></div>
                                                <div data-role="district" data-style="addr-district " data-name="bless_death_district_1" data-id="bless_death_district_1"></div>
                                            </div>
                                            <input name="bless_death_address_1" type="text" class="" id="bless_death_address_1" placeholder="請輸入地址" />
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
            alert('親愛的大德您好\n台東東海龍門天聖宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!');
        }

        $("input[type='tel']").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        templeinit(1);
        
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

        $('.InputGroup > li:last .DeathAddress').find('div[data-role]').each(function (index) {
            var originalId = $(this).attr('data-id');
            var originalName = $(this).attr('data-name');
            var newId = originalId.slice(0, -1) + lastblessNum;
            var newName = originalName.slice(0, -1) + lastblessNum;
            $(this).attr('data-id', newId);
            $(this).attr('data-name', newId);
        });

        $('.InputGroup > li:last .Salvation').find('div').each(function (index) {
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
                $("#" + originalId).hide();
                $("#" + originalId).hide();
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
                            templeinit(id);
                            if (value != '') {
                                switch (value) {
                                    //case "1":
                                    //    //贊普
                                    //    $("#bless_death_" + id).hide();
                                    //    $("#bless_firstname_" + id).hide();
                                    //    break;
                                    case "2":
                                        //超薦『歷代祖先』
                                        $("#bless_firstname_" + id).show();
                                        $("#bless_deathaddress_" + id).show();

                                        $("#bless_firstname_" + id + " label").text("祖先姓氏");
                                        $("#bless_first_name_" + id).attr("placeholder", "請輸入祖先姓氏");

                                        $("#bless_deathaddress_" + id).show();
                                        $("#bless_deathaddress_" + id + " label").text("牌位地址");
                                        break;
                                    case "3":
                                        //超薦『往生親友』
                                        $("#bless_death_" + id).show();
                                        $("#bless_deathdate_" + id).show();
                                        $("#bless_deathaddress_" + id).show();

                                        $("#bless_death_" + id + " label").text("往生親友姓名");
                                        $("#bless_death_name_" + id).attr("placeholder", "請輸入往生親友者的姓名");

                                        $("#bless_deathdate_" + id + " label").text("往生日期");
                                        $("#bless_death_deathday_" + id).attr("placeholder", "請輸入往生(卒日)日期(國曆)");

                                        $("#bless_deathaddress_" + id).show();
                                        $("#bless_deathaddress_" + id + " label").text("牌位地址");
                                        break;
                                    case "5":
                                        //超薦『冤親債主』
                                        $("#bless_death_" + id).hide();
                                        $("#bless_deathdate_" + id).hide();
                                        $("#bless_deathbirth_" + id).hide();
                                        $("#bless_firstname_" + id).hide();
                                        $("#bless_deathaddress_" + id).hide();
                                        //$("#bless_death_" + id).show();
                                        //$("#bless_deathbirth_" + id).show();
                                        //$("#bless_deathaddress_" + id).show();

                                        //$("#bless_death_" + id + " label").text("超薦者姓名");
                                        //$("#bless_death_name_" + id).attr("placeholder", "請輸入超薦者的姓名");

                                        //$("#bless_death_birthday_" + id).attr("placeholder", "請輸入超薦者農曆生日");

                                        //$("#bless_deathaddress_" + id).show();
                                        //$("#bless_deathaddress_" + id + " label").text("地址");
                                        break;
                                    case "6":
                                        //超薦『嬰靈(無緣子女)』
                                        $("#bless_deathdate_" + id).show();

                                        $("#bless_deathdate_" + id + " label").text("往生日期");
                                        $("#bless_death_deathday_" + id).attr("placeholder", "請輸入『嬰靈』離開年月日(國曆)");
                                        break;
                                    default:
                                        $("#bless_death_" + id).hide();
                                        $("#bless_deathdate_" + id).hide();
                                        $("#bless_deathbirth_" + id).hide();
                                        $("#bless_firstname_" + id).hide();
                                        $("#bless_deathaddress_" + id).hide();
                                        break;
                                }
                            }
                            else {
                                $("#bless_death_" + id).hide();
                                $("#bless_deathdate_" + id).hide();
                                $("#bless_deathbirth_" + id).hide();
                                $("#bless_firstname_" + id).hide();
                                $("#bless_deathaddress_" + id).hide();
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
    var regex = "^民國\\d{2,3}年(0?[1-9]|1[012])月(0?[1-9]|[12][0-9]|3[01])日$";  // 民國日期格式
    $("#subBtn").on("click", function () {
        var isValid = true;
        var isBirth = true;

        var listcount = $('.InputGroup > li').last().attr('bless-id');

        // 遍歷每個必填欄位
        $('.required').each(function () {
            var value = $(this).val();
            var text = this;
            if (value === '' || value === null) {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        for (var i = 1; i <= listcount; i++) {

            if ($("#bless_service_" + i).val() == 2) {
                // 遍歷每個必填欄位-有條件 (普度項目=超薦『歷代祖先』)
                var reslist = ["bless_first_name_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() == '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            if ($("#bless_service_" + i).val() == 3) {
                // 遍歷每個必填欄位-有條件 (普度項目=超薦『往生親友』)
                var reslist = ["bless_death_name_" + i, "bless_death_deathday_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() == '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            //if ($("#bless_sendback_" + i).val() == 5) {
            //    // 遍歷每個必填欄位-有條件 (普度項目=超薦『冤親債主』)
            //    var reslist = ["bless_death_name_" + i, "bless_death_birthday_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

            //    reslist.forEach(function (value) {
            //        if ($("#" + value).val() == '') {
            //            isValid = false;
            //            $(this).addClass('unfilled');
            //        } else if (value != '' && $(this).hasClass('unfilled')) {
            //            $(this).removeClass('unfilled');
            //        }
            //    });
            //}

            if ($("#bless_service_" + i).val() == 6) {
                // 遍歷每個必填欄位-有條件 (普度項目=超薦『嬰靈(無緣子女)』)
                var reslist = ["bless_death_deathday_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() == '') {
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
            //alert("活動尚未開始!");

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_dh();
                }
                else {
                    alert('親愛的大德您好\n台東東海龍門天聖宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n台東東海龍門天聖宮 2024普度活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);

                    $("#bless_service_" + index).val(item.PurdueType).trigger("change");
                    if (item.PurdueType == '2') {
                        $("#bless_first_name_" + index).val(item.FirstName);
                        $("#bless_death_county_" + index).val(item.DeathCounty).trigger("change");
                        $("#bless_death_district_" + index).val(item.Deathdist).trigger("change");
                        $("#bless_death_address_" + index).val(item.DeathAddr);
                    }
                    else if (item.PurdueType == "3") {
                        $("#bless_death_name_" + index).val(item.DeathName);
                        $("#bless_death_deathday_" + index).val(item.Deathday);
                        $("#bless_death_county_" + index).val(item.DeathCounty).trigger("change");
                        $("#bless_death_district_" + index).val(item.Deathdist).trigger("change");
                        $("#bless_death_address_" + index).val(item.DeathAddr);
                    }
                    else if (item.PurdueType == "5") {
                        $("#bless_death_name_" + index).val(item.DeathName);
                        $("#bless_death_birthday_" + index).val(item.DeathBirthday);
                        $("#bless_death_leapMonth_" + index).val(item.DeathLeapMonth);
                        $("#bless_death_birthtime_" + index).val(item.DeathBirthTime);
                        $("#bless_death_county_" + index).val(item.DeathCounty).trigger("change");
                        $("#bless_death_district_" + index).val(item.Deathdist).trigger("change");
                        $("#bless_death_address_" + index).val(item.DeathAddr);
                    }
                    else if (item.PurdueType == "6") {
                        $("#bless_death_deathday_" + index).val(item.Deathday);
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

    function gotoChecked_dh() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();      //申請人姓名
        Appmobile = $("#member_tel").val()      //申請人電話

        name_Tag = [];
        mobile_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        purduetype_Tag = [];

        deathname_Tag = [];
        deathday_Tag = [];
        deathbirth_Tag = [];
        deathleapMonth_Tag = [];
        deathbirthtime_Tag = [];
        firstname_Tag = [];
        deathzipCode_Tag = [];
        deathcounty_Tag = [];
        deathdist_Tag = [];
        deathaddr_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農歷生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址

            var purduetype = $("select[name='bless_service_" + i + "']").val();                 //普度項目
            purduetype_Tag.push(purduetype);

            deathname_Tag.push($("#bless_death_name_" + i).val());                              //往生者姓名
            deathday_Tag.push($("#bless_death_deathday_" + i).val());                           //往生日期
            deathbirth_Tag.push($("#bless_death_birthday_" + i).val());                         //農歷生日
            deathleapMonth_Tag.push($("#bless_death_leapMonth_" + i).val());                    //閏月 Y-是 N-否
            deathbirthtime_Tag.push($("#bless_death_birthtime_" + i).val());                    //農曆時辰
            firstname_Tag.push($("#bless_first_name_" + i).val());                              //姓氏
            deathzipCode_Tag.push($("#bless_death_zipcode_" + i).val());                        //郵遞區號
            deathcounty_Tag.push($("select[name='bless_death_county_" + i + "']").val());       //縣市
            deathdist_Tag.push($("select[name='bless_death_district_" + i + "']").val());       //區域
            deathaddr_Tag.push($("#bless_death_address_" + i).val());                           //部分地址
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            purduetype_Tag: JSON.stringify(purduetype_Tag),

            deathname_Tag: JSON.stringify(deathname_Tag),
            deathday_Tag: JSON.stringify(deathday_Tag),
            deathbirth_Tag: JSON.stringify(deathbirth_Tag),
            deathleapMonth_Tag: JSON.stringify(deathleapMonth_Tag),
            deathbirthtime_Tag: JSON.stringify(deathbirthtime_Tag),
            firstname_Tag: JSON.stringify(firstname_Tag),
            deathzipCode_Tag: JSON.stringify(deathzipCode_Tag),
            deathcounty_Tag: JSON.stringify(deathcounty_Tag),
            deathdist_Tag: JSON.stringify(deathdist_Tag),
            deathaddr_Tag: JSON.stringify(deathaddr_Tag),
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
        templeinit(1);
        switch (value) {
            //case "1":
            //    //贊普
            //    $("#zampname2_1").show();
            //    $(".Zamp #bless_zamp_1").show();
            //    $(".Zamp #bless_rec_1").hide();
            //    $("#bless_death_1").hide();
            //    $("#bless_firstname_1").hide();
            //    break;
            case "2":
                //超薦『歷代祖先』
                $("#bless_firstname_1").show();
                $("#bless_deathaddress_1").show();

                $("#bless_firstname_1" + " label").text("祖先姓氏");
                $("#bless_first_name_1").attr("placeholder", "請輸入祖先姓氏");

                $("#bless_deathaddress_1").show();
                $("#bless_deathaddress_1" + " label").text("牌位地址");
                break;
            case "3":
                //超薦『往生親友』
                $("#bless_death_1").show();
                $("#bless_deathdate_1").show();
                $("#bless_deathaddress_1").show();

                $("#bless_death_1" + " label").text("往生親友姓名");
                $("#bless_death_name_1").attr("placeholder", "請輸入往生親友者的姓名");

                $("#bless_deathdate_1" + " label").text("往生日期");
                $("#bless_death_deathday_1").attr("placeholder", "請輸入往生(卒日)日期(國曆)");

                $("#bless_deathaddress_1").show();
                $("#bless_deathaddress_1" + " label").text("牌位地址");
                break;
            case "5":
                //超薦『冤親債主』
                $("#bless_death_1").hide();
                $("#bless_deathdate_1").hide();
                $("#bless_deathbirth_1").hide();
                $("#bless_firstname_1").hide();
                $("#bless_deathaddress_1").hide();
                //$("#bless_death_1").show();
                //$("#bless_deathbirth_1").show();
                //$("#bless_deathaddress_1").show();

                //$("#bless_death_1" + " label").text("超薦者姓名");
                //$("#bless_death_name_1").attr("placeholder", "請輸入超薦者的姓名");

                //$("#bless_death_birthday_1").attr("placeholder", "請輸入超薦者農曆生日");

                //$("#bless_deathaddress_1").show();
                //$("#bless_deathaddress_1" + " label").text("地址");
                break;
            case "6":
                //超薦『嬰靈(無緣子女)』
                $("#bless_deathdate_1").show();

                $("#bless_deathdate_1" + " label").text("往生日期");
                $("#bless_death_deathday_1").attr("placeholder", "請輸入『嬰靈』離開年月日(國曆)");
                break;
            default:
                $("#bless_death_1").hide();
                $("#bless_deathdate_1").hide();
                $("#bless_deathbirth_1").hide();
                $("#bless_firstname_1").hide();
                $("#bless_deathaddress_1").hide();
                break;
        }
    });
</script>


<!-----初始化欄位----->
<script>
    function templeinit(id) {
        $("#bless_death_" + id).hide();
        $("#bless_deathdate_" + id).hide();
        $("#bless_deathbirth_" + id).hide();
        $("#bless_firstname_" + id).hide();
        $("#bless_deathaddress_" + id).hide();
    }
</script>
