<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lybc_dh.aspx.cs" Inherits="twbobibobi.Temples.templeService_lybc_dh" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="護國息災梁皇大法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lybc_dh.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="護國息災梁皇大法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lybc_dh.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lybc_dh.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lybc_dh.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>護國息災梁皇大法會|台東東海龍門天聖宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css?t=777777" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
     <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link href="css/Activity.css" rel="stylesheet" />
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

        #checkedprivate {
            vertical-align: middle;
            -webkit-transform: scale(1.2);
            -moz-transform: scale(1.2);
            -ms-transform: scale(1.2);
            transform: scale(1.2);
            -webkit-transform-origin: right;
            -moz-transform-origin: right;
            -ms-tranform-origin: right;
            transform-origin: right;
            height: 16px;
            width: 16px;
            margin-bottom: 4px;
            position: relative;
            border-radius: 2px;
        }

        .TempleImg img {
            padding-bottom: 5px;
        }
        
        video {
            max-width: 100%;
        }
        
        .ytvideo {
            max-width: 100%;
            width: 100%;
            height: 550px;
        }

        .text_s input, .tel input, .mail input, .date input {
            width: 16vw;
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
            .MemAddress > div:first-child {
                width: 20%;
            }
            .select select {
                width: 100%;
            }
            .text_s input, .tel input, .mail input, .date input {
                width: calc(100% - 12px);
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
                    <li>護國息災梁皇大法會</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="https://bobibobi.tw/Temples/images/temple/lybc_dh.jpg" width="1160" height="550" alt="台東東海龍門天聖宮２０２４護國息災梁皇大法會" title="台東東海龍門天聖宮２０２４護國息災梁皇大法會" />
                </div>
                <h1 class="TempleName">台東東海龍門天聖宮護國息災梁皇大法會</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2024/11/27 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2024/12/18 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <br />
                                <h1 class="TempleName">【2024 『天后護民•歷劫重建』護國息災梁皇大法會】</h1>
                                <br />
                                <p>✍️誠感天后威靈，縱遇祝融仍得諸天垂憐護佑；菩薩修行歷劫不退度眾初心，法會的隨喜捐款，都是助聖母『#一磚一瓦拼重建』的資糧‼️每一分捐款，
                                    都是對慈悲道場的護持➕對護法神明的敬仰➕與眾生結緣的善行➕對未來的祝福‼️我們在此心靈相聚，共修共振，喜迎 #媽祖🆚. #金剛護法 早日
                                    安座回道場，以其無量功德，普照眾生，護佑境土。</p>
                                <p>➖➖➖➖➖➖</p>
                                <h2>《慈悲道場懺法🆚.懺王🆚.梁皇寶懺》</h2>
                                <p>📖慈悲道場懺法，是 #寶誌禪師 等九位高僧，應 #梁武帝 要求為超渡其髮妻郗皇后所作，故俗稱『#梁皇寶懺』、『梁皇懺』，有『#懺王』的美稱，
                                    被喻為 #一生一定要參懺一次的法會 ‼️其中根據 #法華經、#華嚴經、#涅槃經、#楞伽經、#三千佛懺…等佛經中抄錄的1,275佛號而編成，共計兩千多
                                    次禮佛，跪拜禮讚之間，沐浴在諸佛菩薩的慈悲願海裡，可洗滌罪障、化解災厄、能與冤親債主解怨釋結，祈求陽世檀越除病消災。懺文說『#慈悲道
                                    場』四字乃 #彌勒菩薩 夢中顯聖傳授而得。</p>
                                <br />
                                <p>📖『#梁皇寶懺』乃是 #大乘佛教 中最具威力的懺悔法門之一 ‼️它不僅能夠消除眾生的業障➕還能夠增長智慧➕培養慈悲心➡️懺悔的過程中，我們不
                                    僅是在懺悔自己的過錯➕更是在為整個世界祈福➕讓所有的眾生都能共享這份慈悲的光輝‼️</p>
                                <p>➖➖➖➖➖➖</p>
                                <h2>《『天后護民•歷劫重建』護國息災梁皇大法會》</h2>
                                <p>1.神尊入壇日：12/15(日)~12/16(一)。</p>
                                <p>2.12/17(二)：</p>
                                <p>08:30-09:30 法會壇場灑淨、安位召請。</p>
                                <p>10:00-17:00 梁皇寶懺、過案薦食、三皈依。</p>
                                <p>3.12/18~12/20(三~五) 08:30-17:00 梁皇寶懺大法會。</p>
                                <p>4.12/21(六)</p>
                                <p>09:00-12:00 供佛齋天。</p>
                                <p>14:00-17:30 圓滿焰口。</p>
                                <p>5.神尊請回日：12/22(日)~12/23(一)。</p>
                                <p>➖➖➖➖➖➖</p>
                                <h2>✅『財寶袋』：一份 300元。 <span id="lybc1" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>✅『普度供桌』：一份1,500元(含金紙)。<span id="lybc2" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>✅『福慧水晶燈』：一盞500元/5天。<span id="lybc3" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <p style="color: red;">普度供桌-供品於活動後，會統一捐出給慈善公益團體。</p>
                                <br />
                                <div class="IndexVideo">
                                    <h1 class="TempleName">活動介紹</h1>
                                    <iframe class="ytvideo" src="https://www.youtube.com/embed/8EMuAkobA7Y?si=qwKcSOGJ7AQNn9ay" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                                </div>
                                <h1 class="TempleName">【災後重建捐款】</h1>
                                <p>一磚一瓦拚重建</p>
                                <p>請支持聖母聖業</p>
                                <br />
                                <h2>『重建募款』：伍佰元。 <span id="lybc4" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>『重建募款』：壹仟元。<span id="lybc5" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>『重建募款』：貳仟元。<span id="lybc6" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <br />
                                <div class="IndexVideo">
                                    <h1 class="TempleName">新聞影片</h1>
                                    <iframe class="ytvideo" src="https://www.youtube.com/embed/GAvaUcNoSRY?si=OMwshzsK339dqznM" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                                </div>
                            </div>
                        </div>

                        <uc3:SocialMedia runat="server" id="SocialMedia" />
                    </div>
                </div>

                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" type="text" maxlength="5" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" maxlength="10" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>

                        <!--可複製的區塊 //start-->
                        <ul class="InputGroup">

                            <!--li為動態複製欄位的部份-->
                            <li bless-id="1">
                                <div class="DeletData"><a href="javascript:;" class="deletList" title="刪除">
                                    <img src="images/deletData.svg" alt="" /></a></div>
                                <div class="FormTitle_B">祈福人<span>（每欄祈福人姓名限填一位姓名，每種項目對應一位祈福人。如需多位，請點選增加祈福人。）</span></div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" maxlength="5" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" maxlength="10" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
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
                                    <label>祈福人地址</label>
                                    <div class="CusAddress">
                                        <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                                        <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                                        <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                                    </div>
                                    <input name="bless_address_1" type="text" class="required" id="bless_address_1" placeholder="請輸入祈福人地址"/>
                                </div>
                                <div class="FormInput select">
                                    <label>活動項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value="1">財寶袋 $300</option>
                                        <option value="2">普度供桌 $1,500</option>
                                        <option value="3">福慧水晶燈 $500</option>
                                        <option value="4">重建募款 $500</option>
                                        <option value="5">重建募款 $1,000</option>
                                        <option value="6">重建募款 $2,000</option>
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
            alert('親愛的大德您好\n台東東海龍門天聖宮 護國息災梁皇大法會已截止！！\n感謝您的支持, 謝謝!');
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
                if (originalId) {
                    var newId = originalId.slice(0, -1) + lastblessNum;
                    $(this).attr('id', newId);
                    $(this).attr('name', newId);
                }
            });

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
                    $("#" + newId).val('1');
                }

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
            $('.RecAddress').twzipcode({ 'readonly': true });//如果需填地址，請加這一行
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
    $("#subBtn").on("click", function () {

        $(".Notice").text("");
        $(".Notice").removeClass("active");

        var isValid = true;
        var isValid2 = true;
        var isValid3 = true;
        var isCheckedValid = $("#checkedprivate").is(":checked");

        var listcount = $('.InputGroup > li').last().attr('bless-id');

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

        // 遍歷每個必填欄位
        for (var i = 1; i <= listcount; i++) {
            var value_birth = $("#bless_birthday_" + i).val().trim();
            var value_sbirth = $("#bless_sbirth_" + i).val().trim();

            if (value_birth == '' && value_sbirth == '') {
                isValid = false;
                $('.required2').addClass('unfilled');
            } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                $('.required2').removeClass('unfilled');
            }
        }

        var value = $("#member_tel").val().trim();
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
        else {
            if (value != '' && $("#member_tel").hasClass('unfilled')) {
                $("#member_tel").removeClass('unfilled');
            }

            for (var i = 1; i <= listcount; i++) {

                if ($("#bless_sendback_" + i).val() == 1) {
                    // 遍歷每個必填欄位-有條件 (寄回欄位=1)
                    var reslist = ["bless_rec_name_" + i, "bless_rec_tel_" + i, "bless_rec_county_" + i, "bless_rec_district_" + i, "bless_rec_address_" + i];

                    reslist.forEach(function (value) {
                        if ($("#" + value).val() == '') {
                            isValid = false;
                            $(this).addClass('unfilled');
                        } else if (value != '' && $(this).hasClass('unfilled')) {
                            $(this).removeClass('unfilled');
                        }
                    });
                }

                value = $("#bless_tel_" + i).val().trim();
                if (value == "") {
                    $(".Notice").text("祈福人電話不能為空。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid2 = false;
                    break;
                }
                else if (!Isphone(value)) {
                    $(".Notice").text("祈福人電話格式錯誤。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid2 = false;
                    break;
                }
                else {
                    if (value != '' && $("#bless_tel_" + i).hasClass('unfilled')) {
                        $("#bless_tel_" + i).removeClass('unfilled');
                    }
                }

                value = $("#bless_district_" + i).val();
                if (value == '' || value == null) {
                    $(".Notice").text("祈福人地址為空，請重新選擇地址縣市及區域。");
                    $(".Notice").addClass("active");
                    $("#bless_district_" + i).addClass('unfilled');

                    isValid = false;
                    isValid3 = false;
                    break;
                }
                else {
                    if (value != '' && $("#bless_district_" + i).hasClass('unfilled')) {
                        $("#bless_district_" + i).removeClass('unfilled');
                    }
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
                            gotoChecked_dh();
                        }
                        else {
                            alert('親愛的大德您好\n台東東海龍門天聖宮 2024護國息災梁皇大法會！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                        }
                    }
                    else {
                        alert('親愛的大德您好\n台東東海龍門天聖宮 2024護國息災梁皇大法會！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    }
                }
            } else {
                // 在這裡可以進行表單提交或其他相關處理
                // 有欄位未填寫
                if (!isValid) {
                    if (isValid2 && isValid3) {
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
                    $("#bless_service_" + index).val(item.LybcType);
                    $("#bless_Remark_" + index).val(item.Remark);

                    index++;
                });
            }

        }
    }

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val().trim();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

    function gotoChecked_dh() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val().trim();                      //購買人姓名
        Appmobile = $("#member_tel").val().trim();                      //購買人電話

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
        LybcType_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val().trim());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val().trim());                                     //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val().trim());                                       //祈福人信箱
            zipCode_Tag.push($("#bless_zipcode_" + i).val().trim());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val().trim());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val().trim());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val().trim());                                      //祈福人部分地址
            LybcType_Tag.push($("#bless_service_" + i).val().trim());                                  //服務項目
            remark_Tag.push($("#bless_Remark_" + i).val().trim());                                     //備註
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
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
            LybcType_Tag: JSON.stringify(LybcType_Tag),
            remark_Tag: JSON.stringify(remark_Tag),
            listcount: listcount
        };

        hasTextArea = true;
        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkedTime(endTime) {
        var startTime = new Date();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
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

