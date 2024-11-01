<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_supplies2.aspx.cs" Inherits="Temple.Temples.templeService_supplies2" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="呈疏補庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_supplies2.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="呈疏補庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>呈疏補庫|北港武德宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
                    <li><a href="templeInfo.aspx?a=6" title="北港武德宮">北港武德宮</a></li>
                    <li>呈疏補庫</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/supplies_wu2.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">北港武德宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/03/12 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2024/04/17 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>財庫是甚麼?就是你在世間所能承接裝盛、所能積累的財富的多寡。嚴格來說，財是流量，庫則是存量，有財無庫，財來則財去，累積不了多少，總是剩餘不多。也因此十方善信來到財神祖廟，最嚮往的一個法門，就是“補財庫”。</h2>
                        </div>
                        <div>
                            <h2>▌農曆三月十五日為五路武財神聖誕日。</h2>
                            <p>每年接近此時節，分靈出去的大小宮廟甚或神壇與一般家戶都會不約而同踴躍回娘家為老財神爺祝壽。按例皆由歌仔戲及布袋戲團作大戲三日(三月十三、十四、十五)為祝壽揭開序幕，並於農曆十四日晚上十一點，備妥壽糕壽桃壽麵開始祭典。本宮亦準備釀造一年的福酒(福圓釀造的酒)，供信徒取飲。祈佑信徒福氣滿滿、財源滾滾。過往自財神爺聖誕前一兩週起信眾的車輛與進香團遊覽車即開始川流不息、鬧熱滾滾，為的就是向大家敬愛的老武財公說聲“生日快樂“！</p>
                        </div>
                        <div>
                            <h2>▌降文開示-癸卯年</h2>
                            <p>顧名思義補財庫，填平累世虛不前</p>
                            <p>化凶制煞去災厄，補庫增實聚寶氣</p>
                            <p>煥然一章定氣象，正本善業載功果</p>
                            <p>事半功倍定笑懷，興惡不濟心思異</p>
                            <p>事倍功半棄不前；元辰光彩有依附</p>
                            <p>箇中道理知ㄧ二，受用不盡人間世</p>
                            <p>世外桃源歡喜心，財庫合恣於善用</p>
                            <p>增福納祥定可擇。</p>
                        </div>
                        <div>
                            <h2>▌如何補財庫</h2>
                            <p>補財庫的方式，是透過敬呈給天官武財神、三官大帝與諸神的補財庫金與文疏，祈求消災、赦罪、解厄、植福、進財的禮俗。財神祖廟的補財庫金，規格內容全為早年老武財公降鸞所示，外覆的疏文即是"消災植福文疏"，原因是，欲求財庫得補，須先擺脫災障束縛，故敬備相對應的金紙財帛與鮮花香燭等祈請三界公解厄消災、赦罪後賜福，再求請天官武財神補財運，也因此俗稱的補財庫金內容計有：天金、尺金、天官錢、天庫錢、虎錢、補運錢及財寶神衣。</p>
                        </div>
                        <div>
                            <h2>▌注意事項</h2>
                            <p>報名完成後,保必保庇線上宮廟服務平台會在4/18號統一將報名資料提交北港武德宮,於農曆三月十五日子時(4/23 23:00) 北港武德宮管理委員會將舉行天官武財神聖誕祝壽大典, 同時為遠距善信祈補財庫,完成後廟方會寄送感謝狀至您的收件地址</p>
                        </div>
                        <div>
                            <h2>補財庫-呈疏補庫 $650 /份</h2>
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
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
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
                                    <label>農歷生日</label><input name="bless_birthday_1" type="text" class="datapicker" id="bless_birthday_1" placeholder="請選擇農歷生日"/>
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
                                    <select name="bless_birthTime_1" class="" id="bless_birthTime_1">
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
                                    <label>市話</label><input name="bless_homenum_1" type="tel" class="" id="bless_homenum_1" placeholder="請輸入市話"/>
                                </div>
                                <div class="FormInput email mail">
                                    <label>Email</label><input name="bless_email_1" type="text" class="required" id="bless_email_1" placeholder="請輸入Email"/>
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
                                        <option value="2">呈疏補庫 ($650)</option>
                                    </select>
                                </div>
                                <div class="FormInput select count">
                                    <label>數量</label>
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
                                    </select>
                                </div>
                                <div class="FormInput text_s">
                                    <label>備註</label><textarea name="bless_Remark_1" type="text" class="" id="bless_Remark_1" ></textarea>
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
            alert('親愛的大德您好\n北港武德宮 2024呈疏補庫活動已截止！！\n感謝您的支持, 謝謝!');
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

            if (newId.indexOf('birthTime') >= 0) {
                $("#" + newId).val('吉');
            }

            if (newId.indexOf('count') >= 0) {
                $("#" + newId).val('1');
            }

            if (newId.indexOf('service') >= 0) {
                $("#" + newId).val('2');
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

            if (checkEndTime()) {
                gotoChecked_wu();
            }
            else {
                alert('親愛的大德您好\n北港武德宮 2024呈疏補庫活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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
                    $("#bless_sex_" + index).val(item.Sex);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_birthTime_" + index).val(item.BirthTime);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_homenum_" + index).val(item.Homenum);
                    $("#bless_email_" + index).val(item.Email);
                    $("#bless_count_" + index).val(item.Count);
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

        Appname = $("#member_name").val();      //申請人姓名
        Appmobile = $("#member_tel").val()      //申請人電話

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
        count_Tag = [];
        suppliestype_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農歷生日
            birthTime_Tag.push($("#bless_birthTime_" + i).val());                               //祈福人農曆時辰
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-閏月 N-非閏月
            homenum_Tag.push($("#bless_homenum_" + i).val());                                   //市話
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人email
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            count_Tag.push($("#bless_count_" + i).val());                                       //數量
            remark_Tag.push($("#bless_Remark_" + i).val());                                     //備註

            var suppliestype = $("select[name='bless_service_" + i + "']").val();               //服務項目
            suppliestype_Tag.push(suppliestype);
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
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
            count_Tag: JSON.stringify(count_Tag),
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

