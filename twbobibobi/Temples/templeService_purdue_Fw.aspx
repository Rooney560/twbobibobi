<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_Fw.aspx.cs" Inherits="Temple.Temples.templeService_purdue_Fw" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="中元普度|斗六五路財神宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_Fw.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="中元普度|斗六五路財神宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>中元普度|斗六五路財神宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=15" title="斗六五路財神宮">斗六五路財神宮</a></li>
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_Fw.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">斗六五路財神宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/06/24 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2024/08/29 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>何謂贊普</h2>
                            <p>贊普是贊助供養六道眾生齋食及法食的意思，不僅能施食供養，最主要是召請鬼道眾生來受法食，仗神佛的慈悲願力，讓一切餓鬼，皆能得度，成就無上功德。</p>
                            <p>報名參加【斗六五路財神宮】代辦中元普渡服務。</p>
                            <p>即完成中元普渡，植福、轉好運！</p>
                            <p>普品貼上善信大德的姓名於農曆7月15日，擺在法會現場、宴請四方兄弟。</p>
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
                                <div class="Zamp">
                                    <div id="zampname_1" name="zampname_1">
                                        <div class="FormInput text_s">
                                            <label>祈福人姓名</label><input name="bless_zampname_1" maxlength="5" type="text" class="" id="bless_zampname_1" placeholder="請輸入祈福人姓名"/>
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
                                            <input name="bless_address_1" type="text" class="" id="bless_address_1" placeholder="請輸入地址"/>
                                        </div>
                                    </div>
                                    <div class="FormInput select">
                                        <label>普度項目</label>
                                        <select name="bless_service_1" class="required" id="bless_service_1">
                                            <option selected="selected" value>請選擇</option>
                                            <option value="1">贊普 $1200</option>
                                            <option value="17">誦經迴向 $1200</option>
                                        </select>
                                    </div>
                                    <div id="bless_otherpurchase_1" name="bless_otherpurchase_1">
                                        <div class="FormInput select addGoldPaperCount">
                                            <label>捐獻白米 $200 /份</label>
                                            <select name="bless_addRiceCount_1" class="" id="bless_addRiceCount_1">
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

                                <%--<div class="Salvation">
                                    <div id="salvationname_1" name="salvationname_1">
                                        <div class="FormInput text_s">
                                            <label>祈福人姓名</label><input name="bless_salvationname_1" type="text" class="" id="bless_salvationname_1" placeholder="請填寫陽世大德姓名"/>
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
                                    <div id="salvationbirth_1" name="salvationbirth_1">
                                        <div class="FormInput date">
                                            <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker" id="bless_birthday_1" placeholder="請選擇農歷生日" />
                                        </div>
                                    </div>
                                    <div id="salvationleapmonth_1" name="salvationleapmonth_1">
                                        <div class="FormInput select count">
                                            <label>閏月</label>
                                            <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                                                <option value="N">非閏月</option>

                                                <option value="Y">閏月</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div id="bless_deathaddress_1" name="bless_deathaddress_1" style="display:none;">
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
                                </div>--%>
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
            alert('親愛的大德您好\n斗六五路財神宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!');
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

        $('.InputGroup > li:last .Zamp').find('div').each(function (index) {
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

        // 遍歷每個生日欄位
        $('.datapicker').each(function () {
            var value = $(this).val();
            var text = this;

            if (value === '' || value === null) {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (!value.match(regex)) {
                isValid = false;
                isBirth = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        if (isValid) {
            // 所有欄位都已填寫
            console.log('所有欄位都已填寫');
            //alert("活動尚未開始!");

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_Fw();
                }
                else {
                    alert('親愛的大德您好\n斗六五路財神宮 2024普度活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n斗六五路財神宮 2024普度活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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

                    $("#bless_zampname_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_addRichCount_" + index).val(item.Count_rice);

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

    function gotoChecked_Fw() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();      //申請人姓名
        Appmobile = $("#member_tel").val()      //申請人電話

        purduetype_Tag = [];

        zampname_Tag = [];
        zampmobile_Tag = [];
        zampbirth_Tag = [];
        zampleapMonth_Tag = [];
        zampbirthtime_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        count_rice_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            var purduetype = $("select[name='bless_service_" + i + "']").val();                 //普度項目
            purduetype_Tag.push(purduetype);
            //----------贊普
            zampname_Tag.push($("#bless_zampname_" + i).val());                                 //祈福人姓名
            zampmobile_Tag.push($("#bless_tel_" + i).val());                                    //祈福人電話
            zampbirth_Tag.push($("#bless_birthday_" + i).val());                                //祈福人農歷生日
            zampleapMonth_Tag.push($("#bless_leapMonth_" + i).val());                           //閏月 Y-是 N-否
            zampbirthtime_Tag.push($("#bless_birthtime_" + i).val());                           //祈福人農曆時辰
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            count_rice_Tag.push($("#bless_addRiceCount_" + i).val());                           //捐獻白米數量
            //----------贊普

        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            purduetype_Tag: JSON.stringify(purduetype_Tag),
            zampname_Tag: JSON.stringify(zampname_Tag),
            zampmobile_Tag: JSON.stringify(zampmobile_Tag),
            zampbirth_Tag: JSON.stringify(zampbirth_Tag),
            zampleapMonth_Tag: JSON.stringify(zampleapMonth_Tag),
            zampbirthtime_Tag: JSON.stringify(zampbirthtime_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            count_rice_Tag: JSON.stringify(count_rice_Tag),
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
