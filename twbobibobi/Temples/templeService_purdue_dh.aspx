<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_purdue_dh.aspx.cs" Inherits="Temple.Temples.templeService_purdue_dh" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="台東東海龍門天聖宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_purdue_dh.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="台東東海龍門天聖宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/purdue_dh_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/purdue_dh_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/purdue_dh_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>台東東海龍門天聖宮|中元普度|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
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

        .EventServiceContent img {
            width: 75%;
            margin: 0 auto;
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
                
        /* Toast 容器 ------------------------------------ */
        .toast {
            position: fixed;
            bottom: 20px; /* 距離底部 20px */
            left: 50%; /* 水平置中 */
            transform: translateX(-50%) translateY(100px);
            /* 初始往下隱藏 100px */
            background: rgba(0, 0, 0, 0.8); /* 半透明黑底 */
            color: #fff; /* 白字 */
            padding: 10px 20px; /* 內距 */
            border-radius: 4px; /* 圓角 */
            opacity: 0; /* 初始透明 */
            transition: transform .3s ease, opacity .3s ease; /* 進出場動畫 */
            z-index: 9999; /* 最上層 */
            box-sizing: border-box;
            max-width: calc(100% - 40px); /* 左右各留 20px 安全邊距 */
            overflow-wrap: break-word; /* 自動換行 */
        }

        /* Toast 顯示時 -------------------------------- */
        .toast.visible {
            opacity: 1;
            transform: translateX(-50%) translateY(0);
        }

        /* 大螢幕時限制最大寬度 ------------------------ */
        @media (min-width: 768px) {
            .toast {
                max-width: 300px;
            }
        }

        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }

            .EventServiceContent img {
                width: 100%;
            }
        }

        /*電腦版*/
        @media only screen and (min-width: 576px) {
            .text_s input, .tel input, .mail input, .date input {
                width: 20vw;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=16" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                    <li>中元普度</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/purdue_dh_2025.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">台東東海龍門天聖宮</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2025/07/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/09/19 23:59</div>
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

                        <uc3:SocialMedia runat="server" id="SocialMedia" />
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
                            <label>購買人姓名</label><input name="member_name" maxlength="5" type="text" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>購買人信箱</label><input name="member_mail" type="text" class="required" id="member_mail" placeholder="請輸入購買人信箱"/>
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
                                        <option value="1">贊普 $1500</option>
                                        <option value="2">超薦『歷代祖先』 $500</option>
                                        <option value="3">超薦『往生親友』 $500</option>
                                        <option value="5">超薦『冤親債主』 $500</option>
                                        <option value="6">超薦『嬰靈(無緣子女)』 $500</option>
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
                                <div class="FormInput text_s">
                                    <label>備註</label><textarea name="bless_Remark_1" type="text" class="" id="bless_Remark_1" placeholder="請輸入問題內容"></textarea>
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
                                            <label>農曆生日</label><input name="bless_death_birthday_1" type="text" class="datapicker required4" id="bless_death_birthday_1" placeholder="請選擇農曆生日" />
                                        </div>
                                        <div class="FormInput select count">
                                            <label>閏月</label>
                                            <select name="bless_death_leapMonth_1" class="" id="bless_death_leapMonth_1">
                                                <option value="N">非閏月</option>

                                                <option value="Y">閏月</option>
                                            </select>
                                        </div>
                                        <div class="FormInput select count">
                                            <label>農曆時辰</label>
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

        if (!checkEndTime()) {
            alert('親愛的大德您好\n台東東海龍門天聖宮 2025普度活動已截止！！\n感謝您的支持, 謝謝!');
        }

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

        $('.InputGroup > li:last').find('textarea').each(function (index) {
            var originalId = $(this).attr('id');
            if (originalId != null) {
                var newId = originalId.slice(0, -1) + lastblessNum;
                $(this).attr('id', newId);
                $(this).attr('name', newId);

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
    // 工具：抓出所有 .required.unfilled 的 label 名稱
    function getMissingRequiredNames() {
        return $('.required.unfilled').map(function () {
            const $input = $(this);
            let $grp = $input.closest('.FormInput');
            // 嘗試讀同層 label
            let labelText = $grp.find('label').first().text().trim();
            if (!labelText) {
                // 如果是地址那種沒有 label (e.g. 祈福人地址)，就往上找前一個有 label 的群組
                $grp.prevAll('.FormInput').each(function () {
                    const txt = $(this).find('label').first().text().trim();
                    if (txt) {
                        labelText = txt;
                        return false;  // break
                    }
                });
            }
            return labelText.replace(/：|:/g, '');
        }).get();
    }

    // 顯示 Toast，3 秒後自動消失，並在關閉時執行 callback
    function showToast(msg, callback) {
        const $t = $(`<div class="toast">${msg}</div>`)
            .appendTo('body');
        // 進場
        requestAnimationFrame(() => $t.addClass('visible'));
        // 3 秒後退場並呼叫 callback
        setTimeout(() => {
            $t.removeClass('visible');
            $t.one('transitionend', () => {
                $t.remove();
                if (typeof callback === 'function') callback();
            });
        }, 1000);
    }

    // Toast 顯示完畢後再捲動＋聚焦
    function showToastAndFocus($el, msg) {
        showToast(msg, () => {
            // 等 toast 完全隱藏之後再聚焦，不搶畫面
            $(".Notice").text(msg).addClass("active");
            $el.addClass("unfilled");
            $el[0].scrollIntoView({ block: 'center' });
            $el.focus();
        });
    }

    function clearError($elem) {
        $elem.removeClass("unfilled");
    }

    function clearNotice() {
        $(".Notice").removeClass("active").text("");
    }

    // 通用驗證器清單
    const validators = [
        {
            // 購買人電話：非空 + 格式
            selector: "#member_tel",
            checks: [
                { fn: v => v !== "", msg: "購買人電話不能為空。" },
                { fn: Isphone, msg: "購買人電話格式錯誤。" }
            ]
        },
        {
            // 購買人信箱：非空 + 格式
            selector: "#member_mail",
            checks: [
                { fn: v => v !== "", msg: "購買人信箱不能為空。" },
                { fn: IsEmail, msg: "購買人信箱格式錯誤。" }
            ]
        },
        {
            // 所有通用必填欄位
            selector: ".required",
            checks: [{ fn: v => v.trim() !== "", msg: "上面有欄位為未填寫。" }]
        }
    ];

    // 針對每一位祈福人做驗證
    function validateBless(i) {
        const $li = $(`.InputGroup > li[bless-id=${i}]`);
        // 電話
        const tel = $li.find(`#bless_tel_${i}`).val().trim();
        if (!tel) {
            showToastAndFocus($li.find(`#bless_tel_${i}`), "祈福人電話不能為空。");
            return false;
        }
        if (!Isphone(tel)) {
            showToastAndFocus($li.find(`#bless_tel_${i}`), "祈福人電話格式錯誤。");
            return false;
        }
        clearError($li.find(`#bless_tel_${i}`));

        // 若國內才要檢查縣市 & 區域
        if ($li.find(`#bless_oversea_${i}`).val() === "1") {
            const county = $li.find(`#bless_county_${i}`).val();
            if (!county) {
                showToastAndFocus($li.find(`#bless_county_${i}`), "祈福人地址 縣市為空，請重新選擇縣市。");
                return false;
            }
            clearError($li.find(`#bless_county_${i}`));

            const district = $li.find(`#bless_district_${i}`).val();
            if (!district) {
                showToastAndFocus($li.find(`#bless_district_${i}`), "祈福人地址 區域為空，請重新選擇區域。");
                return false;
            }
            clearError($li.find(`#bless_district_${i}`));
        }

        // 農曆/國曆生日二擇一
        const birth = $li.find(`#bless_birthday_${i}`).val();
        const sbirth = $li.find(`#bless_sbirth_${i}`).val();
        if (!birth && !sbirth) {
            showToastAndFocus($li.find(".required2"), "請選擇農曆或國曆生日其中一項。");
            return false;
        }
        clearError($li.find(".required2"));

        // 遍歷每個必填欄位-有條件 (普度項目=超薦『歷代祖先』)
        if ($li.find(`#bless_service_${i}`).val() === "2") {
            // 要驗證的欄位 key 與對應顯示文字
            var fields = [
                { key: "first_name", label: "祖先姓氏" },
                { key: "death_county", label: "牌位地址 縣市" },
                { key: "death_district", label: "牌位地址 區域" },
                { key: "death_address", label: "牌位地址 部分地址" }
            ];
            for (var j = 0; j < fields.length; j++) {
                var f = fields[j];
                // 對應到 DOM id
                var $input = $li.find(`#bless_${f.key}_${i}`);
                var val = $input.val() == null ? "" : $input.val().trim();

                if (!val) {
                    // 只要有一個空就跳出並 focus
                    showToastAndFocus($input, `${f.label}為空，請重新輸入。`);
                    return false;
                }
                // 通過才清掉先前錯誤
                clearError($input);
            }
        }

        // 遍歷每個必填欄位-有條件 (普度項目=超薦『往生親友』)
        if ($li.find(`#bless_service_${i}`).val() === "3") {
            // 要驗證的欄位 key 與對應顯示文字
            var fields = [
                { key: "death_name", label: "往生親友姓名" },
                { key: "death_deathday", label: "往生日期" },
                { key: "death_county", label: "牌位地址 縣市" },
                { key: "death_district", label: "牌位地址 區域" },
                { key: "death_address", label: "牌位地址 部分地址" }
            ];
            for (var j = 0; j < fields.length; j++) {
                var f = fields[j];
                // 對應到 DOM id
                var $input = $li.find(`#bless_${f.key}_${i}`);
                var val = $input.val() == null ? "" : $input.val().trim();

                if (!val) {
                    // 只要有一個空就跳出並 focus
                    showToastAndFocus($input, `${f.label}為空，請重新輸入。`);
                    return false;
                }
                // 通過才清掉先前錯誤
                clearError($input);
            }
        }

        // 遍歷每個必填欄位-有條件 (普度項目=超薦『嬰靈(無緣子女)』)
        if ($li.find(`#bless_service_${i}`).val() === "6") {
            // 要驗證的欄位 key 與對應顯示文字
            var fields = [
                { key: "death_deathday", label: "往生日期" }
            ];
            for (var j = 0; j < fields.length; j++) {
                var f = fields[j];
                // 對應到 DOM id
                var $input = $li.find(`#bless_${f.key}_${i}`);
                var val = $input.val() == null ? "" : $input.val().trim();

                if (!val) {
                    // 只要有一個空就跳出並 focus
                    showToastAndFocus($input, `${f.label}為空，請重新輸入。`);
                    return false;
                }
                // 通過才清掉先前錯誤
                clearError($input);
            }
        }

        return true;
    }

    // 回到上一頁後若選過縣市但區域為空，強制清空縣市
    $(window).on("pageshow", function (e) {
        // 1. 購買人：縣市有、區域空 → 清空縣市
        const memberCounty = $("#member_county").val();
        const memberDistrict = $("#member_district").val();
        if (memberCounty && !memberDistrict) {
            $("#member_county").val("");
        }

        // 2. 祈福人：動態 N 個
        $(".InputGroup > li[bless-id]").each(function () {
            const $li = $(this);
            const id = $li.attr("bless-id");              // e.g. "1", "2", ...
            const $county = $li.find(`#bless_county_${id}`);
            const $district = $li.find(`#bless_district_${id}`);

            // 如果選了「國內」才需檢查
            if ($li.find(`#bless_oversea_${id}`).val() === "1") {
                if ($county.val() && !$district.val()) {
                    // 清空縣市，迫使使用者重選才會帶出新的區域
                    $county.val("");
                }
            }
        });
    });

    $("#subBtn").on("click", function () {
        // 先把前一次的狀態清掉
        clearNotice();
        $('.required').each((_, el) => clearError($(el)));

        // 1. 先跑通用 validators，但對 .required rule 不馬上跳出，只標記 .unfilled
        for (const rule of validators) {
            const $eles = $(rule.selector);
            for (let i = 0; i < $eles.length; i++) {
                const $el = $eles.eq(i);
                const val = $el.val();
                clearError($el);

                for (const check of rule.checks) {
                    if (!check.fn(val)) {
                        // 標記錯誤欄位
                        $el.addClass('unfilled');
                        // 如果是「非 .required」的 rule，就立刻提示並 return
                        if (rule.selector !== '.required') {
                            showToastAndFocus($el, check.msg);
                            return;
                        }
                        // 如果是 .required 這支，就只標記，繼續跑完所有 required
                    }
                }
            }
        }

        // 2. 全部通用檢查後，看看還有哪些 .required 還是 unfilled
        const missing = getMissingRequiredNames();
        if (missing.length) {
            // 去重、組字串
            const uniq = [...new Set(missing)];
            const msg = uniq.join('、') + ' 未填寫';
            // 聚焦到第一個錯誤欄位
            const $first = $('.required.unfilled').first();
            showToastAndFocus($first, msg);
            return;
        }

        // 3. 驗證所有祈福人
        const lastId = Number($('.InputGroup > li').last().attr('bless-id') || 0);
        for (let i = 1; i <= lastId; i++) {
            if (!validateBless(i)) {
                return;
            }
        }

        // 4. 隱私權同意
        if (!$("#checkedprivate").is(":checked")) {
            showToastAndFocus($("#checkedprivate"), "請勾選同意隱私權政策。");
            return;
        }

        // 5. 全部通過，送出
        console.log("所有欄位都已填寫正確，準備送出");
        // 如果活動時間判斷...
        if (checkedStartTime()) {
            if (checkEndTime()) {
                gotoChecked_dh();
            } else {
                alert('台東東海龍門天聖宮 2025普度活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('台東東海龍門天聖宮 2025普度活動尚未開始！');
            location = 'https://bobibobi.tw/Temples/temple.aspx';
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
            $("#member_mail").val(res.AppEmail);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);
                    //$("#bless_birthday_" + index).val(item.Birth);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_birthtime_" + index).val(item.BirthTime);
                    //$("#bless_sBirth_" + index).val(item.sBirth);
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
                    $("#bless_Remark_" + index).val(item.Remark);

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

        Appname = $("#member_name").val();                                                          //購買人姓名
        Appmobile = $("#member_tel").val();                                                         //購買人電話
        AppEmail = $("#member_mail").val();                                                         //購買人信箱

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        purduetype_Tag = [];
        remark_Tag = [];

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
            name_Tag.push($("#bless_name_" + i).val().trim());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val().trim());                                     //祈福人國曆生日
            oversea_Tag.push($("#bless_oversea_" + i).val());                                          //國內-1 國外-2

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
            remark_Tag.push($("#bless_Remark_" + i).val());                                             //備註

            var purduetype = $("select[name='bless_service_" + i + "']").val();                 //普度項目
            purduetype_Tag.push(purduetype);

            deathname_Tag.push($("#bless_death_name_" + i).val());                              //往生者姓名
            deathday_Tag.push($("#bless_death_deathday_" + i).val());                           //往生日期
            deathbirth_Tag.push($("#bless_death_birthday_" + i).val());                         //農曆生日
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
            AppEmail: AppEmail,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            birthtime_Tag: JSON.stringify(birthtime_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
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
        const $li = $(`.InputGroup > li[bless-id=${id}]`);

        // 定義所有超拔區塊要初始化的欄位：key → input/select id 後半，container → div id 前半
        const fields = [
            { key: 'death_name', container: 'bless_death' },                // 往生親友姓名
            { key: 'death_deathday', container: 'bless_deathdate' },        // 往生日期
            { key: 'death_birthday', container: 'bless_deathbirth' },       // 農曆生日
            { key: 'first_name', container: 'bless_firstname' },            // 祖先姓氏
            { key: 'death_county', container: 'bless_deathaddress' },       // 牌位地址 縣市
            { key: 'death_district', container: 'bless_deathaddress' },     // 牌位地址 區域
            { key: 'death_address', container: 'bless_deathaddress' },      // 牌位地址 部分地址
            { key: 'death_zipcode', container: 'bless_deathaddress' },      // 牌位地址 郵遞區號
        ];

        fields.forEach(f => {
            // 1) 清空 input/select
            const $input = $li.find(`#bless_${f.key}_${id}`);
            if ($input.is('select')) {
                $input.prop('selectedIndex', 0).trigger("change");
            } else {
                $input.val('');
            }
            $input.removeClass('unfilled');

            // 2) 隱藏外層 container
            $li.find(`#${f.container}_${id}`).hide();
        });
    }
</script>
