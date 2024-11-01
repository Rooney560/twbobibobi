<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_EmperorGuansheng_ty.aspx.cs" Inherits="Temple.Temples.templeService_EmperorGuansheng_ty" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="關聖帝君聖誕暨天赦日 招財補運活動|桃園威天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_EmperorGuansheng_ty.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="關聖帝君聖誕暨天赦日 招財補運活動|桃園威天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>關聖帝君聖誕暨天赦日 招財補運活動|桃園威天宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=14" title="桃園威天宮">桃園威天宮</a></li>
                    <li>關聖帝君聖誕暨天赦日 招財補運活動</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/emperorGuansheng_ty.jpg" width="1160" height="550" alt="" />
                    <img src="images/temple/emperorGuansheng_ty_02.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">桃園威天宮</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2024/06/18 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2024/07/26 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>▌關聖帝君聖誕千秋 祝壽謝恩功德祿位</h2>
                            </div>
                            <div>
                                <p>桃園威天宮 甲辰龍年建廟十週年</p>
                                <p>關聖帝君聖誕千秋祝壽謝恩功德祿位</p>
                                <p>活動時間 113年6月15日~113年8月5日</p>
                                <p>還願謝恩功德主</p>
                                <p>感恩帝君長久以來的護佑，於帝君聖誕期間設立-謝恩祝壽功德祿位，祈求平安、發財。</p>
                                <br />
                                <h2>麒麟狀功德主 $10000元</h2>
                                <p>立麒麟狀招財祿位，特別贈送甲辰龍年限定的【龍行天下黃金項鏈】，讓您招財避邪，事業順利賺飽飽！<span id="emperorGuansheng1" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>富貴狀功德主 $ 3000元</h2>
                                <p>立富貴狀招財祿位，特別贈送限量的【關公忠義牌】<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_emperorGuansheng_ty_02.png">(看圖)</a>和【文創關公馬克杯】<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_emperorGuansheng_ty_01.png">(看圖)</a>，讓您財庫滿盈發發發！<span id="emperorGuansheng2" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>忠義狀功德主 $  800元</h2>
                                <p>立忠義狀招財祿位，特別贈送【文創關公馬克杯】<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_emperorGuansheng_ty_01.png">(看圖)</a>，讓您身心安樂，閣家平安！<span id="emperorGuansheng3" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <br />
                                <p>113/07/27 11:00 康熙皇帝親臨祝壽大典</p>
                                <p>113/07/28 14:00 擲茭祈龜賜平安活動</p>
                            </div>
                        </div>
                        <div class="TempleImg">
                            <img src="images/temple/luck_ty.jpg" width="1160" height="550" alt="" />
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>▌甲辰年關公聖誕暨天赦日 招財補運</h2>
                            </div>
                            <div>
                                <p>國曆7/29（農曆6月24日）</p>
                                <p>關聖帝君 是當代玉皇大帝</p>
                                <p>也是武財神</p>
                                <p>甲辰龍年關公聖誕 恰逢天赦日</p>
                                <p>更是千載難逢補財庫的大好日</p>
                                <p>威天宮天赦日招財補運</p>
                                <p>幫信眾大德運晉財入，祈福謝恩</p>
                                <p>為期三天的佛前供燈（供燈時間7/29~7/31）</p>
                                <p>補運過後將會寄出您的補運物品</p>
                                <br />
                                <h2>招財補運 $1280元</h2>
                                <p>補運物品：黃晶發財樹、關公龍鬚麵、關公手機貼、威天宮香火袋<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_luck_ty_01.jpg">(看圖)</a><span id="luck1" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                <h2>威天宮十周年 招財補運【祝壽】紀念版 $5880元</h2>
                                <p>補運物品：黃晶發財樹、關公龍鬚麵、關公手機貼、威天宮香火袋、加贈  甲辰龍年關公祝壽紀念酒<a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_luck_ty_02.jpg">(看正面圖)</a><a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/ty/product_luck_ty_03.jpg">(看背面圖)</a><span id="luck2" style="color:red" class="content_a" runat="server">(已額滿)</span></p>
                                
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
                        <div class="FormInput date">
                            <label>國歷生日</label><input name="member_birthday" type="text" class="datapicker required" id="member_birthday" placeholder="請選擇國歷生日" />
                        </div>
                        <div class="FormInput mail">
                            <label>申請人Email</label><input name="member_mail" type="tel" class="" id="member_mail" placeholder="請輸入Email(選填)"/>
                        </div>
                        <div class="FormInput address">
                            <label>地址</label>
                            <div class="MemAddress">
                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="member_zipcode" data-id="member_zipcode"></div>
                                <div data-role="county" data-style="addr-county required" data-name="member_county" data-id="member_county"></div>
                                <div data-role="district" data-style="addr-district required" data-name="member_district" data-id="member_district"></div>
                            </div>
                            <input name="member_address" type="text" class="required" id="member_address" placeholder="請輸入地址" />
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span>（祈福人限填一位，每個關聖帝君聖誕或招財補運對應一位祈福人。如需多位，請點選增加祈福人。）</span></div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
                                </div>
                                <div class="FormInput date">
                                    <label>國歷生日</label><input name="bless_birthday_1" type="text" class="datapicker required" id="bless_birthday_1" placeholder="請選擇祈福人國歷生日" />
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
                                    <label>活動項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="忠義狀功德主">忠義狀功德主 $800</option>
                                        <option value="富貴狀功德主">富貴狀功德主 $3000</option>
                                        <option value="招財補運">招財補運 $1280</option>
                                        <option value="招財補運紀念版">招財補運【祝壽】紀念版 $5880</option>
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
            alert('親愛的大德您好\n桃園威天宮 2024關聖帝君聖誕暨天赦日 招財補運活動已截止！！\n感謝您的支持, 謝謝!');
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

                if (newId.indexOf('service') >= 0) {
                    $("#" + newId).val('忠義狀功德主');
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
    var regex = "^民國\\d{2,3}年(0?[1-9]|1[012])月(0?[1-9]|[12][0-9]|3[01])日$";  // 民國日期格式
    $("#subBtn").on("click", function () {
        var isValid = true;
        var isBirth = true;

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
                    gotoChecked_ty();
                }
                else {
                    alert('親愛的大德您好\n桃園威天宮 2024關聖帝君聖誕活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n桃園威天宮 2024關聖帝君聖誕活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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
            if (res.overnumType == 21) {
                alert("關聖帝君聖誕已額滿，感謝大德的支持。");
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
            $("#member_birthday").val(res.AppBirth);
            $("#member_mail").val(res.AppEmail);
            $("#member_county").val(res.AppCounty).trigger("change");
            $("#member_district").val(res.Appdist).trigger("change");
            $("#member_address").val(res.AppAddr);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_service_" + index).val(item.EmperorGuanshengString);

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

    function gotoChecked_ty() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                      //申請人姓名
        Appmobile = $("#member_tel").val();                     //申請人電話
        Appbirth = $("#member_birthday").val();                 //申請人國歷生日
        Appemail = $("#member_mail").val();                     //申請人Email
        AppzipCode = $("#member_zipcode").val();                //申請人郵遞區號
        Appcounty = $("select[name='member_county']").val();    //申請人縣市
        Appdist = $("select[name='member_district']").val();    //申請人區域
        Appaddr = $("#member_address").val();                   //申請人部分地址

        name_Tag = [];
        mobile_Tag = [];
        birth_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        EmperorGuanshengString_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人國曆生日
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            EmperorGuanshengString_Tag.push($("#bless_service_" + i).val());                    //服務項目
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appbirth: Appbirth,
            Appemail: Appemail,
            AppzipCode: AppzipCode,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            EmperorGuanshengString_Tag: JSON.stringify(EmperorGuanshengString_Tag),
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
