<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lights_ty_mom.aspx.cs" Inherits="Temple.Temples.templeService_lights_ty_mom" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="桃園威天宮|孝親謝恩祈福活動|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lights_ty_mom.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="桃園威天宮|孝親謝恩祈福活動|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lights_ty_mom_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lights_ty_mom_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lights_ty_mom_2025.jpg" />

    <!-- 預覽影片 -->
    <meta property="og:video" content="https://www.youtube.com/embed/YLzwppX3XZ4" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="桃園威天宮 孝親祈福活動" />
    <meta name="twitter:description" content="點孝親祈福燈，為父母添壽祈福！母親節活動限時開放，加贈香氛加濕器，報名請點👉https://st.bobibobi.tw/7d8saj" />
    <meta name="twitter:image" content="https://bobibobi.tw/Temples/images/temple/lights_ty_mom_2025.jpg" />

    <title>桃園威天宮|孝親謝恩祈福活動|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
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
       
        .ytvideo {
            max-width: 100%;
            width: 100%;
            height: 550px;
            margin-bottom: -5px;
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
    <script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "VideoObject",
      "name": "桃園威天宮 孝親祈福活動",
      "description": "🎁母親節獻感恩・威天宮孝親祈福🎁\n點【孝親祈福燈】，為父母恩親祈福添壽💖\n加贈【香氛加濕器】\n手刀報名👉https://st.bobibobi.tw/7d8saj",
      "thumbnailUrl": "https://img.youtube.com/vi/YLzwppX3XZ4/maxresdefault.jpg",
      "uploadDate": "2025-04-02T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/YLzwppX3XZ4",
      "contentUrl": "https://bobibobi.tw/Temples/templeService_lights_ty_mom.aspx"
    }
    </script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async="" src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=14" title="桃園威天宮">桃園威天宮</a></li>
                    <li>孝親謝恩祈福活動</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lights_ty_mom_2025.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">歡迎使用《桃園威天宮》母親節孝親祈福謝恩活動</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2025/04/03 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2025/05/08 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <p>關聖帝君忠義仁愛的精神，讓眾神推舉祂為第十八代玉皇大帝，關聖帝君不僅為至孝之神，更特別護祐孝順之人。</p>
                                <p>桃園威天宮遵奉 關聖帝君聖示，在母親節期間舉辦【孝親祈福謝恩活動】來歡慶母親節。</p>
                                <p>在 帝君座前點一盞【孝親祈福燈】，為您的父母、祖父母、養育恩親祈求 關聖帝君降臨護祐，為親人添福、添平安，並贈送點燈的大德 關公加持的【保佑香氛加濕器】，護祐您的恩親平安健康、事事順心！</p>
                                <p>5月10日星期六與11日星期日下午2點，威天宮將盛大舉辦【孝親祈福謝恩儀式】。</p>
                                <p>敬邀有點【孝親祈福燈】的大德們帶恩親來威天宮，在 關公座前感謝恩親的撫育之恩。</p>
                                <p>並免費贈送參加的大德 帝君座前加持的【愛心太陽餅禮盒】，讓恩親們吃了身體健康、財運旺旺來！</p>
                            </div>
                            <div>
                                <h2>孝親祈福燈 | 點【孝親祈福燈】送【保佑香氛加濕器】結緣金880元。
                                    <span id="light1" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                            </div>
                        </div>

                        <div class="">
                            <iframe class="ytvideo" src="https://youtube.com/embed/YLzwppX3XZ4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
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
                        <div class="FormInput mail">
                            <label>購買人信箱</label><input name="member_mail" type="text" class="required" id="member_mail" placeholder="請輸入購買人信箱"/>
                        </div>
                        <div class="FormInput date">
                            <label>國歷生日</label><input name="member_birthday" type="text" class="datapicker required" id="member_birthday" placeholder="請選擇國歷生日" />
                        </div>
                        <div class="FormInput address">
                            <label>購買人地址</label>
                            <div class="MemAddress">
                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="member_zipcode" data-id="member_zipcode"></div>
                                <div data-role="county" data-style="addr-county required" data-name="member_county" data-id="member_county"></div>
                                <div data-role="district" data-style="addr-district required" data-name="member_district" data-id="member_district"></div>
                            </div>
                            <input name="member_address" type="text" class="required" id="member_address" placeholder="請輸入購買人地址。" />
                        </div>
                       <%-- <div class="FormInput">
                            <label class="emailalert"></label>
                            <span style="color: red;">我們將根據購買人地址寄送發票，請填寫正確的購買人地址。</span>
                        </div>--%>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span>（祈福人限填一位，每盞燈對應一位祈福人。如需多位，請點選增加祈福人。）</span></div>
                                <div class="FormInput select">
                                    <label>活動項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="孝親祈福燈">孝親祈福燈 $880</option>
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
                                <div class="FormInput date">
                                    <label>國歷生日</label><input name="bless_birthday_1" type="text" class="datapicker required" id="bless_birthday_1" placeholder="請選擇祈福人國歷生日" />
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
            alert('親愛的大德您好\n桃園威天宮 2025孝親祈福活動已截止！！\n感謝您的支持, 謝謝!');
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

            //更新所有動態產生的ID編號  
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
                    $("#" + newId).val('孝親祈福燈');
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


            $('.DeletData').addClass("active");

            dateSelect();//有日期選擇時使用
            $('.CusAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
            $('.MemAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
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
            // 購買人區域：有縣市就一定要有區域
            selector: "#member_district",
            checks: [
                {
                    fn: v => {
                        const county = $("#member_county").val();
                        if (!county) return false; // 縣市未選
                        return (v || "").trim() !== ""; // 區域必填
                    },
                    msg: "購買人地址未完整選擇，請重新選擇縣市與區域。"
                }
            ]
        },
        {
            // 所有通用必填欄位
            selector: ".required",
            checks: [{ fn: v => (v || "").trim() !== "", msg: "上面有欄位未填寫。" }]
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
        const sbirth = $li.find(`#bless_sbirthday_${i}`).val();
        if (!birth && !sbirth) {
            showToastAndFocus($li.find(".required2"), "請選擇農曆或國曆生日其中一項。");
            return false;
        }
        clearError($li.find(".required2"));

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
        if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
            if (checkEndTime()) {
                gotoChecked_ty();
            } else {
                alert('桃園威天宮 2025孝親謝恩祈福活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('桃園威天宮 2025孝親謝恩祈福活動尚未開始！');
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
            if (res.overnumType == 21) {
                alert("孝親祈福燈已額滿，感謝大德的支持。");
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
            $("#member_mail").val(res.AppEmail);
            $("#member_birthday").val(res.AppsBirth);
            $("#member_county").val(res.AppCounty).trigger("change");
            $("#member_district").val(res.Appdist).trigger("change");
            $("#member_address").val(res.AppAddr);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_birthday_" + index).val(item.sBirth);
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
                    $("#bless_service_" + index).val(item.LightsString);
                    $("#bless_remark_" + index).val(item.Remark);

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

        Appname = $("#member_name").val();                      //購買人姓名
        Appmobile = $("#member_tel").val();                     //購買人電話
        Appemail = $("#member_mail").val();                     //購買人Email
        Appsbirth = $("#member_birthday").val();                //購買人國歷生日
        AppzipCode = $("#member_zipcode").val();                //購買人郵遞區號
        Appcounty = $("select[name='member_county']").val();    //購買人縣市
        Appdist = $("select[name='member_district']").val();    //購買人區域
        Appaddr = $("#member_address").val();                   //購買人部分地址

        name_Tag = [];
        mobile_Tag = [];
        sbirth_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                                     //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                                    //祈福人電話
            sbirth_Tag.push($("#bless_birthday_" + i).val());                                               //祈福人國曆生日
            oversea_Tag.push($("#bless_oversea_" + i).val());                                               //國內-1 國外-2

            if ($("#bless_oversea_" + i).val() == "1") {
                zipCode_Tag.push($("#bless_zipcode_" + i).val().trim());                                    //祈福人郵遞區號
                county_Tag.push($("select[name='bless_county_" + i + "']").val().trim());                   //祈福人縣市
                dist_Tag.push($("select[name='bless_district_" + i + "']").val().trim());                   //祈福人區域
            }
            else {
                zipCode_Tag.push("0");
                county_Tag.push("");
                dist_Tag.push("");
            }
            addr_Tag.push($("#bless_address_" + i).val().trim());                                           //祈福人部分地址
            remark_Tag.push($("#bless_Remark_" + i).val());                                                 //備註
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appemail: Appemail,
            Appsbirth: Appsbirth,
            AppzipCode: AppzipCode,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sbirth_Tag: JSON.stringify(sbirth_Tag),
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
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
