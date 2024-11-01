<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_Fu.aspx.cs" Inherits="Temple.Temples.templeService_purdue_Fu" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="中元普度|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_Fu.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="中元普度|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>中元普度|西螺福興宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <style type="text/css">
        @media only screen and (max-width: 720px) {
            .DeathAddress > div:first-child {
                width: 20%;
            }
            .AppAddress > div:first-child {
                width: 20%;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=8" title="西螺福興宮">西螺福興宮</a></li>
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_Fu.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">西螺福興宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/06/24 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2024/08/20 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>甲辰年西螺街慶讚中元普度法會</h2>
                            <p>各品項普度服務開放登記 ‼️</p>
                            <p>報名參加【西螺福興宮】代辦中元普渡服務。</p>
                            <p>西螺街的中元普度依循古例由 西螺福興宮太平媽聯合附近街坊、鋪戶及全臺各地善信人等共同贊普；小月於農曆7月22日辦理普度，大月則於7月23日辦理。</p>
                        </div>
                        <div>
                            <h2>今年的西螺街普度將於甲辰農曆7月23日（國曆8月26日 禮拜一）上午9點隆重起鼓</h2>
                            <p>本宮延聘府城延陵道壇吳羅錠道長主壇，辦理超度堂上歷代九玄七祖、指定亡者、地基主、解冤親債主、超度嬰靈、超度動物靈，贊普代辦普濟無祀孤幽等服務。</p>
                            <p>誠摯邀請眾善信大德與太平媽一起廣施善緣普、廣施愛心，落實冥陽兩利，發揮太平有愛的精神。</p>
                        </div>
                        <div class="TempleImg">
                            <img src="images/temple/purdue_Fu2.jpg" width="1160" height="550" alt="" />
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
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span></span></div>
                                <div>（祈福人限填一位，每個普度項目對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                
                                <div class="FormInput select">
                                    <label>普度項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option selected="selected" value>請選擇</option>
                                        <option value="1">贊普普品 $1500</option>
                                        <option value="2">超度九玄七祖 $600</option>
                                        <option value="3">超度指名亡者 $600</option>
                                        <option value="4">超度地基主 $600</option>
                                        <option value="5">解冤親債主 $600</option>
                                        <option value="6">超度嬰靈 $600</option>
                                        <option value="11">超度動物靈 $600</option>
                                    </select>
                                </div>

                                <div class="Zamp">
                                    <div id="zampname_1" name="zampname_1">
                                        <div class="FormInput text_s">
                                            <label>祈福人姓名</label><input name="bless_zampname_1" maxlength="5" type="text" class="" id="bless_zampname_1" placeholder="請輸入祈福人姓名" />
                                        </div>
                                    </div>
                                    <div id="zampname2_1" name="zampname2_1">
                                        <div class="FormInput text_s">
                                            <label>祈福人姓名2</label><input name="bless_zampname2_1" maxlength="5" type="text" class="" id="bless_zampname2_1" placeholder="請輸入祈福人姓名2" />
                                        </div>
                                    </div>
                                    <div id="tel2_1" name="tel2_1">
                                        <div class="FormInput tel">
                                            <label>祈福人電話</label><input name="bless_tel_1" type="text" class="" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話" />
                                        </div>
                                    </div>
                                    <div id="zampbirth_1" name="zampbirth_1">
                                        <div class="FormInput date">
                                            <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker" id="bless_birthday_1" placeholder="請選擇農歷生日" />
                                        </div>
                                    </div>
                                    <div id="zampleapmonth_1" name="zampleapmonth_1">
                                        <div class="FormInput select count">
                                            <label>閏月</label>
                                            <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                                                <option value="N">非閏月</option>

                                                <option value="Y">閏月</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div id="zampbirthtime_1" name="zampbirthtime_1">
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
                                    </div>
                                    <div id="zampaddress_1" name="zampaddress_1">
                                        <div class="FormInput address">
                                            <label>地址</label>
                                            <div class="CusAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county " data-name="bless_county_1" data-id="bless_county_1"></div>
                                                <div data-role="district" data-style="addr-district " data-name="bless_district_1" data-id="bless_district_1"></div>
                                            </div>
                                            <input name="bless_address_1" type="text" class="" id="bless_address_1" placeholder="請輸入地址" />
                                        </div>
                                    </div>
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
                                    <div id="salvationname_1" name="salvationname_1">
                                        <div class="FormInput text_s">
                                            <label>祈福人姓名</label><input name="bless_salvationname_1" maxlength="5" type="text" class="" id="bless_salvationname_1" placeholder="請填寫陽世大德姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_death_1" name="bless_death_1">
                                        <div class="FormInput text_s">
                                            <label>超度對象的姓名</label><input name="bless_death_name_1" type="text" class="" id="bless_death_name_1" placeholder="請輸入超度對象的姓名" />
                                        </div>
                                    </div>
                                    <div id="bless_firstname_1" name="bless_firstname_1">
                                        <div class="FormInput text_s">
                                            <label>姓氏</label><input name="bless_first_name_1" type="text" class="" id="bless_first_name_1" placeholder="請輸入姓氏" />
                                        </div>
                                    </div>
                                    <div id="bless_deathaddress_1" name="bless_deathaddress_1" style="display: none;">
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
                                    <div id="bless_otherpurchase_1" name="bless_otherpurchase_1" style="display: none;">
                                        <div class="FormInput select addpurdue">
                                            <label>加購普品 $1500 元</label>
                                            <select name="bless_addpurdue_1" class="" id="bless_addpurdue_1">
                                                <option value="0">不加購</option>
                                                <option value="1">加購</option>
                                            </select>
                                        </div>
                                        <div class="FormInput select addGoldPaperCount">
                                            <label>加購金紙 $300 /份</label>
                                            <select name="bless_addGoldPaperCount_1" class="" id="bless_addGoldPaperCount_1">
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
            alert('親愛的大德您好\n西螺福興宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!');
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
                            templeinit(id);

                            var value = $(this).val();
                            if (value != '') {
                                switch (value) {
                                    case "1":
                                        //贊普
                                        $("#zampname_" + id).show();
                                        $("#zampname2_" + id).show();
                                        $("#tel2_" + id).show();
                                        $("#zampbirth_" + id).show();
                                        $("#zampleapmonth_" + id).show();
                                        $("#zampbirthtime_" + id).show();
                                        $("#zampaddress_" + id).show();
                                        $("#count_" + id).show();
                                        break;
                                    case "2":
                                        //九玄七祖
                                        $(".Salvation #salvationname_" + id).show();

                                        $(".Salvation #bless_firstname_" + id).show();

                                        $(".Salvation #bless_firstname_" + id + " label").text("祖先姓氏");
                                        $(".Salvation #bless_first_name_" + id).attr("placeholder", "請輸入祖先姓氏");


                                        $("#bless_deathaddress_" + id).show();
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").show();
                                        break;
                                    case "3":
                                        //亡者
                                        $(".Salvation #salvationname_" + id).show();

                                        $(".Salvation #bless_death_" + id).show();

                                        $(".Salvation #bless_death_" + id + " label").text("超度姓名");
                                        $(".Salvation #bless_death_name_" + id).attr("placeholder", "請輸入超度對象的姓名");

                                        $("#bless_deathaddress_" + id).show();
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").show();
                                        break;
                                    case "4":
                                        //地基主
                                        $(".Salvation #salvationname_" + id).show();

                                        $("#bless_deathaddress_" + id).show();
                                        $(".Salvation #bless_deathaddress_" + id + " label").text("超度地址");
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").hide();
                                        break;
                                    case "5":
                                        //冤親債主
                                        $(".Salvation #salvationname_" + id).show();

                                        $(".Salvation #bless_death_" + id).show();

                                        $(".Salvation #bless_death_" + id + " label").text("超度姓名");
                                        $(".Salvation #bless_death_name_" + id).attr("placeholder", "請輸入超度對象的姓名");

                                        $("#bless_deathaddress_" + id).show();
                                        $(".Salvation #bless_deathaddress_" + id + " label").text("超度對象地址");
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").hide();
                                        break;
                                    case "6":
                                        //嬰靈
                                        $(".Salvation #salvationname_" + id).show();

                                        $(".Salvation #bless_death_" + id).show();

                                        $(".Salvation #bless_death_" + id + " label").text("超度姓名");
                                        $(".Salvation #bless_death_name_" + id).attr("placeholder", "請輸入超度對象的姓名");

                                        $("#bless_deathaddress_" + id).show();
                                        $(".Salvation #bless_deathaddress_" + id + " label").text("超度對象地址");
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").hide();
                                        break;
                                    case "11":
                                        //動物靈
                                        $(".Salvation #salvationname_" + id).show();

                                        $(".Salvation #bless_death_" + id).show();

                                        $(".Salvation #bless_death_" + id + " label").text("超度姓名");
                                        $(".Salvation #bless_death_name_" + id).attr("placeholder", "請輸入超度對象的姓名");

                                        $("#bless_deathaddress_" + id).show();
                                        $(".Salvation #bless_deathaddress_" + id + " label").text("超度對象地址");
                                        $("#bless_otherpurchase_" + id).show();
                                        $("#bless_otherpurchase_" + id + " .addpurdue").hide();
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

            if ($("#bless_service_" + i).val() == 1) {
                // 遍歷每個必填欄位-有條件 (普度項目=贊普)
                var reslist = ["bless_zampname_" + i, "bless_tel_" + i, "bless_birth_" + i, "bless_county_" + i, "bless_district_" + i, "bless_address_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() === '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            if ($("#bless_service_" + i).val() == 2) {
                // 遍歷每個必填欄位-有條件 (普度項目=九玄七祖)
                var reslist = ["bless_salvationname_" + i, "bless_first_name_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() === '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            if ($("#bless_service_" + i).val() == 3 || $("#bless_service_" + i).val() == 6 || $("#bless_service_" + i).val() == 5 || $("#bless_service_" + i).val() == 12) {
                // 遍歷每個必填欄位-有條件 (普度項目=亡者 or 嬰靈 or 冤親債主 or 動物靈)
                var reslist = ["bless_salvationname_" + i, "bless_death_name_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

                reslist.forEach(function (value) {
                    if ($("#" + value).val() === '') {
                        isValid = false;
                        $(this).addClass('unfilled');
                    } else if (value != '' && $(this).hasClass('unfilled')) {
                        $(this).removeClass('unfilled');
                    }
                });
            }

            if ($("#bless_service_" + i).val() == 4) {
                // 遍歷每個必填欄位-有條件 (普度項目=地基主)
                var reslist = ["bless_salvationname_" + i, "bless_death_county_" + i, "bless_death_district_" + i, "bless_death_address_" + i];

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
            //alert("活動尚未開始!");

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_Fu();
                }
                else {
                    alert('親愛的大德您好\n西螺福興宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n西螺福興宮 2024普度活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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
                    $("#bless_service_" + index).val(item.PurdueType).trigger("change");
                    $("#bless_app_county_1").val(item.AppCounty).trigger("change");
                    $("#bless_app_district_1").val(item.Appdist).trigger("change");
                    $("#bless_app_address_1").val(item.AppAddr);
                    if (item.PurdueType == '1') {
                        $("#bless_zampname_" + index).val(item.Name);
                        $("#bless_zampname2_" + index).val(item.Name2);
                        $("#bless_tel_" + index).val(item.Mobile);
                        $("#bless_birthday_" + index).val(item.Birth);
                        $("#bless_leapMonth_" + index).val(item.LeapMonth);
                        $("#bless_birthtime_" + index).val(item.BirthTime);
                        $("#bless_county_" + index).val(item.County).trigger("change");
                        $("#bless_district_" + index).val(item.dist).trigger("change");
                        $("#bless_address_" + index).val(item.Addr);
                        $("#bless_count_" + index).val(item.Count);
                    }
                    else if (item.PurdueType == "2") {
                        $("#bless_salvationname_" + index).val(item.Name);
                        $("#bless_first_name_" + index).val(item.FirstName);
                        $("#bless_death_county_" + index).val(item.County).trigger("change");
                        $("#bless_death_district_" + index).val(item.dist).trigger("change");
                        $("#bless_death_address_" + index).val(item.Addr).trigger("change");
                        $("#bless_addpurdue_" + index).val(item.Count);
                        $("#bless_addGoldPaperCount_" + index).val(item.GoldPaperCount);
                    }
                    else {
                        $("#bless_death_name_" + index).val(item.DeathName);
                        $("#bless_death_county_" + index).val(item.County).trigger("change");
                        $("#bless_death_district_" + index).val(item.dist).trigger("change");
                        $("#bless_death_address_" + index).val(item.Addr);
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

    function gotoChecked_Fu() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                                  //申請人姓名
        Appmobile = $("#member_tel").val()                                  //申請人電話
        Appcounty = $("select[name='bless_app_county_1']").val()            //申請人縣市
        Appdist = $("select[name='bless_app_district_1']").val()            //申請人區域
        Appaddr = $("#bless_app_address_1").val()                           //申請人地址(部分)
        AppzipCode = $("#bless_app_zipcode_1").val();                       //申請人郵遞區號

        purduetype_Tag = [];

        zampname_Tag = [];
        zampname2_Tag = [];
        zampmobile_Tag = [];
        zampbirth_Tag = [];
        zampleapMonth_Tag = [];
        zampbirthtime_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        count_Tag = [];

        salvationname_Tag = [];
        firstname_Tag = [];
        deathname_Tag = [];
        dzipCode_Tag = [];
        dcounty_Tag = [];
        ddist_Tag = [];
        daddr_Tag = [];
        addpurdue_Tag = [];
        GoldPaperCount_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            var purduetype = $("select[name='bless_service_" + i + "']").val();                 //普度項目
            purduetype_Tag.push(purduetype);
                                                                                                //----------贊普
            zampname_Tag.push($("#bless_zampname_" + i).val());                                 //祈福人姓名
            zampname2_Tag.push($("#bless_zampname2_" + i).val());                               //祈福人姓名2
            zampmobile_Tag.push($("#bless_tel_" + i).val());                                    //祈福人電話
            zampbirth_Tag.push($("#bless_birthday_" + i).val());                                //祈福人農歷生日
            zampleapMonth_Tag.push($("#bless_leapMonth_" + i).val());                           //閏月 Y-是 N-否
            zampbirthtime_Tag.push($("#bless_birthtime_" + i).val());                           //祈福人農曆時辰
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            count_Tag.push($("#bless_count_" + i).val());                                       //普品數量
                                                                                                //----------贊普

                                                                                                //----------超拔
            salvationname_Tag.push($("#bless_salvationname_" + i).val());                       //祈福人姓名
            firstname_Tag.push($("#bless_first_name_" + i).val());                              //姓氏
            deathname_Tag.push($("#bless_death_name_" + i).val());                              //超度姓名
            dzipCode_Tag.push($("#bless_death_zipcode_" + i).val());                            //超度(牌位)郵遞區號
            dcounty_Tag.push($("select[name='bless_death_county_" + i + "']").val());           //超度(牌位)縣市
            ddist_Tag.push($("select[name='bless_death_district_" + i + "']").val());           //超度(牌位)區域
            daddr_Tag.push($("#bless_death_address_" + i).val());                               //超度(牌位)部分地址
            addpurdue_Tag.push($("#bless_addpurdue_" + i).val());                               //加購普品 1-加購 0-不加購
            GoldPaperCount_Tag.push($("#bless_addGoldPaperCount_" + i).val());                  //加購金紙
                                                                                                //----------超拔
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            AppzipCode: AppzipCode,
            purduetype_Tag: JSON.stringify(purduetype_Tag),
            zampname_Tag: JSON.stringify(zampname_Tag),
            zampname2_Tag: JSON.stringify(zampname2_Tag),
            zampmobile_Tag: JSON.stringify(zampmobile_Tag),
            zampbirth_Tag: JSON.stringify(zampbirth_Tag),
            zampleapMonth_Tag: JSON.stringify(zampleapMonth_Tag),
            zampbirthtime_Tag: JSON.stringify(zampbirthtime_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            count_Tag: JSON.stringify(count_Tag),
            salvationname_Tag: JSON.stringify(salvationname_Tag),
            firstname_Tag: JSON.stringify(firstname_Tag),
            deathname_Tag: JSON.stringify(deathname_Tag),
            dzipCode_Tag: JSON.stringify(dzipCode_Tag),
            dcounty_Tag: JSON.stringify(dcounty_Tag),
            ddist_Tag: JSON.stringify(ddist_Tag),
            daddr_Tag: JSON.stringify(daddr_Tag),
            addpurdue_Tag: JSON.stringify(addpurdue_Tag),
            GoldPaperCount_Tag: JSON.stringify(GoldPaperCount_Tag),
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
        templeinit('1');

        var value = $(this).val();
        switch (value) {
            case "1":
                //贊普
                $("#zampname_1").show();
                $("#zampname2_1").show();
                $("#tel2_1").show();
                $("#zampbirth_1").show();
                $("#zampleapmonth_1").show();
                $("#zampbirthtime_1").show();
                $("#zampaddress_1").show();
                $("#count_1").show();
                break;
            case "2":
                //九玄七祖
                $(".Salvation #salvationname_1").show();

                $(".Salvation #bless_firstname_1").show();

                $(".Salvation #bless_firstname_1 label").text("祖先姓氏");
                $(".Salvation #bless_first_name_1").attr("placeholder", "請輸入祖先姓氏");


                $("#bless_deathaddress_1").show();
                $("#bless_otherpurchase_1").show();
                $("#bless_otherpurchase_1 .addpurdue").show();
                break;
            case "3":
                //亡者
                $(".Salvation #salvationname_1").show();

                $(".Salvation #bless_death_1").show();

                $(".Salvation #bless_death_1 label").text("超度姓名");
                $(".Salvation #bless_death_name_1").attr("placeholder", "請輸入超度對象的姓名");

                $("#bless_deathaddress_1").show();
                $("#bless_otherpurchase_1").show();
                $("#bless_otherpurchase_1 .addpurdue").show();
                break;
            case "4":
                //地基主
                $(".Salvation #salvationname_1").show();

                $("#bless_deathaddress_1").show();
                $(".Salvation #bless_deathaddress_1 label").text("超度地址");
                $("#bless_otherpurchase_1 ").show();
                $("#bless_otherpurchase_1 .addpurdue").hide();
                break;
            case "5":
                //冤親債主
                $(".Salvation #salvationname_1").show();

                $(".Salvation #bless_death_1").show();

                $(".Salvation #bless_death_1 label").text("超度姓名");
                $(".Salvation #bless_death_name_1").attr("placeholder", "請輸入超度對象的姓名");

                $("#bless_deathaddress_1").show();
                $(".Salvation #bless_deathaddress_1 label").text("超度對象地址");
                $("#bless_otherpurchase_1").show();
                $("#bless_otherpurchase_1 .addpurdue").hide();
                break;
            case "6":
                //嬰靈
                $(".Salvation #salvationname_1").show();

                $(".Salvation #bless_death_1").show();

                $(".Salvation #bless_death_1 label").text("超度姓名");
                $(".Salvation #bless_death_name_1").attr("placeholder", "請輸入超度對象的姓名");

                $("#bless_deathaddress_1").show();
                $(".Salvation #bless_deathaddress_1 label").text("超度對象地址");
                $("#bless_otherpurchase_1").show();
                $("#bless_otherpurchase_1 .addpurdue").hide();
                break;
            case "11":
                //動物靈
                $(".Salvation #salvationname_1").show();

                $(".Salvation #bless_death_1").show();

                $(".Salvation #bless_death_1 label").text("超度姓名");
                $(".Salvation #bless_death_name_1").attr("placeholder", "請輸入超度對象的姓名");

                $("#bless_deathaddress_1").show();
                $(".Salvation #bless_deathaddress_1 label").text("超度對象地址");
                $("#bless_otherpurchase_1").show();
                $("#bless_otherpurchase_1 .addpurdue").hide();
                break;
        }
    });
</script>

<!-----初始化欄位----->
<script>
    function templeinit(id) {

        $("#zampname_" + id).hide();
        $("#zampname2_" + id).hide();
        $("#tel2_" + id).hide();
        $("#zampbirth_" + id).hide();
        $("#zampleapmonth_" + id).hide();
        $("#zampbirthtime_" + id).hide();
        $("#zampaddress_" + id).hide();
        $("#count_" + id).hide();

        $("#salvationname_" + id).hide();
        $("#bless_death_" + id).hide();
        $("#bless_firstname_" + id).hide();
        $("#bless_deathaddress_" + id).hide();
        $("#bless_otherpurchase_" + id).hide();
    }
</script>