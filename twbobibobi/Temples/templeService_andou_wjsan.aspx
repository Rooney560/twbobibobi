<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_andou_wjsan.aspx.cs" Inherits="twbobibobi.Temples.templeService_andou_wjsan" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="台灣道教總廟無極三清總道院|線上安斗|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_andou_wjsan.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇提供台灣道教總廟無極三清總道院線上安斗，財神斗、事業斗、平安斗、文昌斗、藥師斗、元神斗、福祿壽斗、觀音斗" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇提供台灣道教總廟無極三清總道院線上安斗，財神斗、事業斗、平安斗、文昌斗、藥師斗、元神斗、福祿壽斗、觀音斗" />
    <!--簡介-->
    <meta property="og:site_name" content="台灣道教總廟無極三清總道院|線上安斗|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/andou_wjsan_2025.jpg?t=55688" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/andou_wjsan_2025.jpg?t=55688" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/andou_wjsan_2025.jpg?t=55688" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>台灣道教總廟無極三清總道院|線上安斗|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
     <script type="text/javascript" src="js/bootstrap.min.js"></script>
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=31" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                    <li>祈福斗燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/andou_wjsan_2025.jpg?t=55688" width="1160" height="550" alt="保必保庇提供台灣道教總廟無極三清總道院線上安斗，財神斗、事業斗、平安斗、文昌斗、藥師斗、元神斗、福祿壽斗、觀音斗" 
                        title="台灣道教總廟無極三清總道院線上安斗２０２６祈福斗燈" />
                </div>
                <h1 class="TempleName">歡迎使用《台灣道教總廟無極三清總道院》線上安斗服務</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2025/11/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2026/10/31 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div id="andou_an" runat="server">
                            <h1 class="TempleName">什麼是安斗?斗是什麼?</h1>
                            <span style="color:red;" class="content_a" >“斗”指的是天上的星宿</span>
                            <span>，道教認為北斗星群主掌人的死、災、厄、病，因此有北斗註死，而斗姆為北斗眾星之母；南斗星群主掌人的生、福、壽、祿，因此有”南斗福壽”，
                                而太歲就是在執行這些工作的執行者，若能安斗祭祀，弼可消災納福趨吉避凶。</span>
                            <h2>安斗、拜斗就是在、朝拜自己的本命元辰、可使元辰光彩、 消災賜福、祈求平安。</h2>
                            <h2>斗燈是由油燈、米、斗燈傘、鏡、劍、秤、剪以及尺所組成。</h2>
                            <span style="color:red;" class="content_a" >燈</span><span>-光照亮前程。</span><br />
                            <span style="color:red;" class="content_a" >鏡</span><span>-去邪魔之意，光照到鏡子所產生的折射對著八字元辰，讓本命光彩前程似錦。</span><br />
                            <span style="color:red;" class="content_a" >青龍持劍</span><span>-除妖魔護其元辰。</span><br />
                            <span style="color:red;" class="content_a" >朱雀拿剪</span><span>-剪去邪魔纏運。</span><br />
                            <span style="color:red;" class="content_a" >白虎拿秤</span><span>-添加機運。</span><br />
                            <span style="color:red;" class="content_a" >玄武帶尺</span><span>-統兵顧神。</span><br />
                            <span style="color:red;" class="content_a" >斗中放米</span><span>-以求增運補氣。</span><br />
                            <span style="color:red;" class="content_a" >圓傘護天</span><span>-以求天地泰平，這就是斗中法器所代表的意義。</span><br />
                            <br />
                            <h1 class="TempleName">財神斗</h1>
                            <p>希望增強財運、事業順遂的人，經商者可祈求財源廣進，業務、銷售人員提升業績，求職者與上班族盼望升遷加薪。財運不穩、易破財者可藉財神斗補運擋煞。</p>
                            <h1 class="TempleName">事業斗</h1>
                            <p>希望工作順利、職場升遷、創業成功的人，包括上班族、主管、創業者、企業家、業務員、求職者等。想要晉升、加薪、換好工作者，可透過事業斗提升運勢，增加貴人相助。創業者、企業經營者則可求業務穩定發展、財運亨通。</p>
                            <h1 class="TempleName">平安斗</h1>
                            <p>趨吉避凶、保護自身與家人平安的人，包括長輩、孩童、學生、孕婦、經常出差或開車者、體弱多病者等、家庭主婦、長期奔波的工作者或容易遇到意外、犯小人的人，祈求神明庇佑家宅安穩、身心健康、出入平安，讓生活順遂、無災無難。</p>
                            <h1 class="TempleName">文昌斗</h1>
                            <p>學生、考生、教師、研究人員、公職考生、文職人員等，希望提升智慧、考試順利、事業發展者可點燃。特別適合準備升學考試、國家考試、升遷評鑑的人，亦適用於寫作、創作、研究工作者，祈求文昌帝君賜予靈感與智慧，助考運亨通、學業進步、事業順遂。</p>
                            <h1 class="TempleName">藥師斗</h1>
                            <p>身體欠安者、長期病患、醫護人員、照顧者、希望增強健康運勢者。病後康復者、年長者、孕婦、體弱多病者，祈求藥師佛庇佑身心安康、病痛消除，手術前後、身體不適時點燃，以求平安健康。</p>
                            <h1 class="TempleName">元神斗</h1>
                            <p>精神不佳、容易疲憊、情緒低落、思緒混亂、運勢低迷者。經常失眠、壓力大、易犯小人、決策困難者，可透過點燃元神斗調整元神氣場，穩定心神，增強智慧與自信。低潮期或感覺運勢受阻時點燃，祈求神明護佑，讓元神清明、運勢提升、身心和諧。</p>
                            <h1 class="TempleName">福祿壽斗</h1>
                            <p>增添福氣、財運、壽命者，特別是長輩、家庭支柱、事業有成者。祈求家庭和樂、財運旺盛、身體健康的人，特別適合銀髮族、退休人士、企業家、家運興旺者點燃，求福星高照、祿位提升、壽命延長。</p>
                            <h1 class="TempleName">觀音斗</h1>
                            <p>獲得慈悲庇佑、消災解厄、增強智慧、身心平靜者，特別適合信仰觀世音菩薩、心靈迷惘、經歷困難、求子、求健康、祈求家庭和諧者。孕婦、家庭主婦、修行者、壓力大的人點燃，祈求觀音菩薩慈悲護持，賜予平安順遂、消除業障、遠離災禍，讓人生更圓滿祥和。</p>
                            <br />
                            <h2>廟方安斗後將寄送感謝狀至您的收件地址。</h2>
                            <p><h2 style="color: red;" class="">安斗費用，3000元/12個月。</h2></p>
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
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" id="member_name" maxlength="5" placeholder="請輸入購買人姓名"/>
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
                                <div>（祈福人限填一位，每種安斗項目對應一位祈福人。如需多位，請點選增加祈福人。）</div>
                                <div class="FormInput select">
                                    <label>安斗項目</label>
                                    <select name="bless_service_1" class="required" id="bless_service_1">
                                        <option value>請選擇</option>
                                        <%=andoulist %>
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
                                    <label runat="server" id="label_birth_1">農曆生日</label><input name="bless_birthday_1" type="text" class="datapicker required2" id="bless_birthday_1" placeholder="請選擇農曆生日或國曆生日二擇一" />
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
                                    <label>國曆生日</label><input name="bless_sbirthday_1" type="text" class="datapicker required2" id="bless_sbirthday_1" placeholder="請選擇國曆生日或農曆生日二擇一" />
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
            alert('親愛的大德您好\n台灣道教總廟無極三清總道院 2025安斗活動已截止！！\n感謝您的支持, 謝謝!');
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
        if (checkedStartTime()) {
            if (checkEndTime()) {
                gotoChecked_wjsan();
            } else {
                alert('台灣道教總廟無極三清總道院 2025安斗活動已截止！');
                location = 'https://bobibobi.tw/Temples/temple.aspx';
            }
        } else {
            alert('台灣道教總廟無極三清總道院 2025安斗活動尚未開始！');
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
                    $("#bless_service_" + index).val(item.AnDouString);
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

    function gotoChecked_wjsan() {
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
        AnDouString_Tag = [];
        remark_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val().trim());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirthday_" + i).val().trim());                                     //祈福人國曆生日
            oversea_Tag.push($("#bless_oversea_" + i).val());                                          //國內-1 國外-2

            if ($("#bless_oversea_" + i).val() == "1") {
                zipCode_Tag.push($("#bless_zipcode_" + i).val().trim());                                   //祈福人郵遞區號
                county_Tag.push($("select[name='bless_county_" + i + "']").val().trim());                  //祈福人縣市
                dist_Tag.push($("select[name='bless_district_" + i + "']").val().trim());                  //祈福人區域
            }
            else {
                zipCode_Tag.push("0");
                county_Tag.push("");
                dist_Tag.push("");
            }
            addr_Tag.push($("#bless_address_" + i).val().trim());                                      //祈福人部分地址
            remark_Tag.push($("#bless_Remark_" + i).val());                                             //備註
            AnDouString_Tag.push($("#bless_service_" + i).val().trim());                              //服務項目
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
            AnDouString_Tag: JSON.stringify(AnDouString_Tag),
            listcount: listcount
        };

        hasTextArea = true;
        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkEndTime() {
        //var startTime = $("#startTime").val().trim();
        var startTime = new Date();
        var endTime = $("#endTime").text();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
    }

    function checkedStartTime() {
        //var startTime = $("#startTime").val().trim();
        var endTime = new Date();
        var startTime = $("#startTime").text();
        if (Date.parse(endTime).valueOf() >= Date.parse(startTime).valueOf()) {
            return true;
        }
        return false;
    }
</script>
