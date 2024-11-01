<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lights_Lk.aspx.cs" Inherits="Temple.Temples.templeService_lights_Lk" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="祈福點燈|鹿港城隍廟|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lights_Lk.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:site_name" content="祈福點燈|鹿港城隍廟|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lights_Lk_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lights_Lk_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lights_Lk_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>祈福點燈|鹿港城隍廟|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    
    <style type="text/css">
        .content_a {
            font-size: 1.2vw;
        }
        @media only screen and (max-width: 720px) {
            .RecAddress > div:first-child {
                width: 20%;
            }
            .content_a {
                font-size: 5vw;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=21" title="鹿港城隍廟">鹿港城隍廟</a></li>
                    <li>祈福點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lights_Lk_2025.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">歡迎使用《鹿港城隍廟》線上點燈服務</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/11/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/10/31 23:59</div>
                    </div>
                        <div class="EventServiceContent">
                            <h2>於本廟點燈者附贈品，信眾可選擇寄回贈品(需自行負擔運費100元)</h2>
                            <h2>本廟每月初一、初十、十五會為點燈者誦經消災</h2>
                            <br />
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK008.jpg">轉運納福燈(看圖)</a> | 運勢轉順、福氣滿門、功成名就，費用1000元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/007.jpg">贈304餐具組3件式(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK009.jpg">光明燈上層(看圖)</a> | 元神光彩、前途光明、萬事如意，費用1000元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/007.jpg">贈304餐具組3件式(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK003.jpg">文魁智慧燈(看圖)</a> | 學業進步、金榜題名、升遷如意，費用500元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/002.jpg">贈文魁筆組四件式(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK004.jpg">元神光明燈(看圖)</a> | 元神光彩、前途光明、萬事如意，費用500元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/008.jpg">贈城隍符令鑰匙圈(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK005.jpg">太歲平安符(看圖)</a> | 流年平安、消災解厄、身體健康，費用500元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/004.jpg">贈淨身包一包(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK006.jpg">正財福報燈(看圖)</a> | 財運亨通、財庫圓滿、事業順利，費用500元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/009.jpg">贈招財龍銀一組(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK0010.jpg">偏財旺旺燈(看圖)</a> | 添補財庫、好運旺來、福運連年，費用500元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/009.jpg">贈招財龍銀一組(看圖)</a></h2>
                            </div>
                            <div>
                                <h2><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/LK0011.jpg">廣進安財庫(看圖)</a> | 留住財庫、財運亨通、財庫飽滿，費用300元。<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Lk/010.jpg">贈招財進寶鑰匙圈(看圖)</a></h2>
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
                                <div class="FormInput tel">
                                    <label>祈福人市話</label><input name="bless_homenum_1" type="tel" class="" id="bless_homenum_1" placeholder="請輸入祈福人市話(選填)"/>
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
                                        <option value="轉運納福燈">轉運納福燈 $1000</option>
                                        <option value="光明燈上層">光明燈上層 $1000</option>
                                        <option value="文魁智慧燈">文魁智慧燈 $500</option>
                                        <option value="元神光明燈">元神光明燈 $500</option>
                                        <option value="太歲平安符">太歲平安符 $500</option>
                                        <option value="正財福報燈">正財福報燈 $500</option>
                                        <option value="偏財旺旺燈">偏財旺旺燈 $500</option>
                                        <option value="廣進安財庫">廣進安財庫 $300</option>
                                    </select>
                                </div>
                                <div class="FormInput select">
                                    <label>贈品處理方式</label>
                                    <select name="bless_sendback_1" class="required" id="bless_sendback_1">
                                        <option value="N">不寄回</option>

                                        <option value="Y">寄回（運費+$100）</option>
                                    </select>
                                </div>
                                <div class="Receive" id="bless_receive_1">
                                    <div class="FormInput text_s">
                                        <label>收件人姓名</label><input name="bless_rec_name_1" type="text" class="receivename" id="bless_rec_name_1" placeholder="請輸入收件人姓名" />
                                    </div>
                                    <div class="FormInput tel">
                                        <label>收件人電話</label><input name="bless_rec_tel_1" type="tel" class="receivetype" id="bless_rec_tel_1" placeholder="請輸入收件人電話" />
                                    </div>
                                    <div class="FormInput address">
                                        <label>收件人地址</label>
                                        <div class="RecAddress">
                                            <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_rec_zipcode_1" data-id="bless_rec_zipcode_1"></div>
                                            <div data-role="county" data-style="addr-county required2" data-name="bless_rec_county_1" data-id="bless_rec_county_1"></div>
                                            <div data-role="district" data-style="addr-district required2" data-name="bless_rec_district_1" data-id="bless_rec_district_1"></div>
                                        </div>
                                        <input name="bless_rec_address_1" type="text" class="required2" id="bless_rec_address_1" placeholder="請輸入收件人地址" />
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
            alert('親愛的大德您好\n鹿港城隍廟 2025點燈活動已截止！！\n感謝您的支持, 謝謝!');
        }

        $("input[type='tel']").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
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

            if (newId.indexOf('sendback') >= 0) {
                $("#" + newId).val('N')

                $("#" + newId).change(function () {
                    if ($(this).val() == "Y") {
                        $("#bless_receive_" + lastblessNum).show();
                    }
                    else {
                        $("#bless_receive_" + lastblessNum).hide();
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
        $('.InputGroup > li:last .ReAddress').find('div[data-role]').each(function (index) {
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

            if ($(this).attr("id").indexOf("sendback") >= 0) {
                if ($(this).val() == 'Y') {
                    var fileDir = $(this).attr('id');
                    var suffix = fileDir.substr(fileDir.lastIndexOf("."));

                    var reslist = ["bless_recname_" + suffix, "bless_rectel_" + suffix, "bless_reccounty_" + suffix, "bless_recdistrict_" + suffix, "bless_recaddress_" + suffix];

                    reslist.forEach(function (value2) {
                        if ($("#" + value2).val() == '') {
                            isValid = false;
                            $(this).addClass('unfilled');
                        } else if (value2 != '' && $(this).hasClass('unfilled')) {
                            $(this).removeClass('unfilled');
                        }
                    });
                }
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
                        gotoChecked_Lk();
                    }
                    else {
                        alert('親愛的大德您好\n鹿港城隍廟 2025點燈活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    }
                }
                else {
                    alert('親愛的大德您好\n鹿港城隍廟 2025點燈活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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
                    $("#bless_sex_" + index).val(item.Sex);
                    //$("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    //$("#bless_sBirth_" + index).val(item.sBirth);
                    $("#bless_email_" + index).val(item.Email);
                    $("#bless_homenum_" + index).val(item.Homenum);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_service_" + index).val(item.LightsString);

                    $("#bless_sendback_" + index).val(item.Sendback).trigger("change");
                    if (item.Sendback == "1") {
                        $("#bless_rec_name_" + index).val(item.rName);
                        $("#bless_rec_tel_" + index).val(item.rMobile);
                        $("#bless_rec_county_" + index).val(item.rCounty).trigger("change");
                        $("#bless_rec_district_" + index).val(item.rdist).trigger("change");
                        $("#bless_rec_address_" + index).val(item.rAddr);
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

    function gotoChecked_Lk() {
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
        homenum_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        LightsString_Tag = [];

        sendback_Tag = [];
        rname_Tag = [];
        rmobile_Tag = [];
        rzipCode_Tag = [];
        rcounty_Tag = [];
        rdist_Tag = [];
        raddr_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val());                                     //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人信箱
            homenum_Tag.push($("#bless_homenum_" + i).val());                                   //祈福人市話
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            LightsString_Tag.push($("#bless_service_" + i).val());                              //燈種

            var sendback = $("select[name='bless_sendback_" + i + "']").val();
            sendback_Tag.push(sendback);                                                        //寄送方式 N-不寄回 Y-寄回(加收運費120元) }
            rname_Tag.push($("#bless_rec_name_" + i).val());                                    //收件人姓名
            rmobile_Tag.push($("#bless_rec_tel_" + i).val());                                   //收件人電話
            rzipCode_Tag.push($("#bless_rec_zipcode_" + i).val());                              //收件人郵政區號
            rcounty_Tag.push($("select[name='bless_rec_county_" + i + "']").val());             //收件人縣市
            rdist_Tag.push($("select[name='bless_rec_district_" + i + "']").val());             //收件人區域
            raddr_Tag.push($("#bless_rec_address_" + i).val());
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
            homenum_Tag: JSON.stringify(homenum_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            LightsString_Tag: JSON.stringify(LightsString_Tag),
            sendback_Tag: JSON.stringify(sendback_Tag),
            rname_Tag: JSON.stringify(rname_Tag),
            rmobile_Tag: JSON.stringify(rmobile_Tag),
            rzipCode_Tag: JSON.stringify(rzipCode_Tag),
            rcounty_Tag: JSON.stringify(rcounty_Tag),
            rdist_Tag: JSON.stringify(rdist_Tag),
            raddr_Tag: JSON.stringify(raddr_Tag),
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
