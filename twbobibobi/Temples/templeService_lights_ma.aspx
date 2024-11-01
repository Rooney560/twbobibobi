<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lights_ma.aspx.cs" Inherits="twbobibobi.Temples.templeService_lights_ma" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="祈福點燈|玉敕大樹朝天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lights_ma.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。
        線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,太歲燈,安太歲,財神燈,財利燈,藥師佛燈,觀音燈,貴人燈,事業燈,文昌燈,姻緣燈,寵物平安燈,
        福壽燈,虎爺燈,補財庫,蛇年點燈,114年點燈,2025點燈,新春點燈,錢母,發財金" />
    <!--簡介-->
    <meta property="og:site_name" content="祈福點燈|玉敕大樹朝天宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lights_ma_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lights_ma_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lights_ma_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>祈福點燈|玉敕大樹朝天宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
     <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link href="css/Activity.css?t=9987452" rel="stylesheet" />
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=14" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                    <li>祈福點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lights_ma_2025.jpg" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">歡迎使用《玉敕大樹朝天宮》線上點燈服務</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/11/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/02/01 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div class="TempleImg">
                            <img src="images/temple/lights-04.jpg?t=9987452" width="600" alt="" />
                        </div>
                        <div>
                            <h1 class="TempleName">安奉值年太歲燈</h1>
                            <h2>誠聘府城延陵道壇吳政憲道長主持年初起點安燈及年底謝燈科儀!</h2>
                            <p>民國114年/歲次乙巳蛇年<br />
                                蛇「犯太歲」<br />
                                豬「沖太歲」<br />
                                虎「刑太歲及害太歲」<br />
                                猴「刑太歲及破太歲」</p>
                            <p>犯沖太歲流年總會讓人感到不安，難免讓人產生不好的聯想。因此，前往廟宇祭拜並祀奉太歲星君，安定心神，祈求整年順順利利。費用$500元，
                                <span style="color:red;" class="content_a">限量250名。</span><span id="light2" style="color:red;" class="content_a" runat="server">(已額滿)</span></p>
                            <br />
                            <div class="track-temple">
                                <button type="button" class="w-100 d-lg-inline-flex btn bg-custom-primary d-flex justify-content-center align-items-center font-weight-bold text-white rounded-12px waves-effect waves-light" data-toggle="modal" data-target="#ModalContentShow" style="font-size: 1.25rem; letter-spacing: 1px;">
                                    <span class="mr-2">犯沖太歲說明</span>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24">
                                        <path fill="currentColor" d="M11.7 18q-2.4-.125-4.05-1.85T6 12q0-2.5 1.75-4.25T12 6q2.425 0 4.15 1.65T18 11.7l-2.1-.625q-.325-1.35-1.4-2.212T12 8q-1.65 0-2.825 1.175T8 12q0 1.425.863 2.5t2.212 1.4zm1.2 3.95q-.225.05-.45.05H12q-2.075 0-3.9-.788t-3.175-2.137T2.788 15.9T2 12t.788-3.9t2.137-3.175T8.1 2.788T12 2t3.9.788t3.175 2.137T21.213 8.1T22 12v.45q0 .225-.05.45L20 12.3V12q0-3.35-2.325-5.675T12 4T6.325 6.325T4 12t2.325 5.675T12 20h.3zm7.625.55l-4.275-4.275L15 22l-3-10l10 3l-3.775 1.25l4.275 4.275z"></path>
                                    </svg></button>

                                <div class="modal fade" id="ModalContentShow" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="z-index: 9999; display: none;" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document" style="max-width: 45rem; margin: 1.75rem auto;">
                                        <div class="modal-content rounded-12px">
                                            <div class="modal-header">
                                                <h5 class="h5 modal-title title-font">生肖犯沖太歲分為以下形式
                                                </h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" aria-hidden="true">
                                                        <path fill="#161616" d="M6.4 19L5 17.6l5.6-5.6L5 6.4L6.4 5l5.6 5.6L17.6 5L19 6.4L13.4 12l5.6 5.6l-1.4 1.4l-5.6-5.6z"></path>
                                                    </svg></button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="wrapper-lamp-container mb-3">
                                                    <div class="block relative shadow-sm p-3 mb-3" style="border-radius: .75em">
                                                        <h5 class="h5 title-font mb-2">犯太歲</h5>
                                                        <p class="textContent">【太歲當頭坐，無喜必有禍】在當年度容易運勢不順，工作運、財 運、犯小人等，都可能會受到影響，還要小心血光之災！</p>
                                                    </div>
                                                    <div class="block relative shadow-sm p-3 mb-3" style="border-radius: .75em">
                                                        <h5 class="h5 title-font mb-2">沖太歲</h5>
                                                        <p class="textContent">沖即與值年相差六年者正對太歲生肖。受沖者在當年事業與財運上容易諸事不順遂等。</p>
                                                    </div>
                                                    <div class="block relative shadow-sm p-3 mb-3" style="border-radius: .75em">
                                                        <h5 class="h5 title-font mb-2">刑太歲</h5>
                                                        <p class="textContent">
                                                            <span class="block">又稱偏沖指自己的出生年與流年所屬生肖相差三年者，「刑」即刑律。</span>
                                                            <span class="block">注意防官司、犯口舌、罰款等。</span>
                                                        </p>
                                                    </div>
                                                    <div class="block relative shadow-sm p-3 mb-3" style="border-radius: .75em">
                                                        <h5 class="h5 title-font mb-2">害太歲</h5>
                                                        <p class="textContent">指出生年的地支跟流年地支相害！即陷害。易招陷害、投訴等。</p>
                                                    </div>
                                                    <div class="block relative shadow-sm p-3 mb-3" style="border-radius: .75em">
                                                        <h5 class="h5 title-font mb-2">害太歲</h5>
                                                        <p class="textContent">
                                                            <span class="block">指本命生肖與流年生肖產生相破，這六組地支均為相破的關係【子=酉】、【卯=午】、【辰=丑】、【戌=未】【寅=亥】、【巳=申】等。</span>
                                                            <span class="block">「破」有破壞、破裂、破財之意，要注意防漏財，破友誼、破壞身體。</span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn bg-custom-primary font-weight-bold text-white w-100 rounded-12px waves-effect waves-light" data-dismiss="modal" style="font-size: 20px;">關閉</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="TempleImg">
                            <img src="images/temple/lights-03.jpg?t=9987452" width="600" alt="" />
                        </div>
                        <div>
                            <h1 class="TempleName">光明燈</h1>
                            <h2>誠聘府城延陵道壇吳政憲道長主持年初起點安燈及年底謝燈科儀!</h2>
                            <p>天上聖母諸佛菩薩庇佑點亮元辰光彩破除黑暗、照亮前程、祈求庇佑一整年平安，運途順遂。費用$500元，
                                <span style="color:red;" class="content_a">限量350名。</span><span id="light1" style="color:red;" class="content_a" runat="server">(已額滿)</span></p>
                        </div>
                        <div class="TempleImg">
                            <img src="images/temple/lights-05.jpg?t=9987452" width="600" alt="" />
                        </div>
                        <br />
                        <div>
                            <h1 class="TempleName">五文昌燈</h1>
                            <h2>誠聘府城延陵道壇吳政憲道長主持年初起點安燈及年底謝燈科儀!</h2>
                            <p>五文昌帝君，庇佑精、氣、神飽滿增啓智慧、提升考運、功名利祿工作仕途升遷。費用$500元，
                                <span style="color:red;" class="content_a">限量300名。</span><span id="light3" style="color:red;" class="content_a" runat="server">(已額滿)</span></p>
                        </div>
                        <div class="TempleImg">
                            <img src="images/temple/lights_ma-06.jpg?t=9987452" width="600" alt="" />
                        </div>
                        <br />
                        <div>
                            <h1 class="TempleName">福財燈</h1>
                            <h2>誠聘府城延陵道壇吳政憲道長主持年初起點安燈及年底謝燈科儀!</h2>
                            <p>福德正神、天官五路武財神、萬善爺，庇佑財運亨通、財源廣進事業興旺、貴人提攜。費用$500元，
                                <span style="color:red;" class="content_a">限量250名。</span><span id="light4" style="color:red;" class="content_a" runat="server">(已額滿)</span></p>
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
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" id="member_name" maxlength="5" placeholder="請輸入購買人姓名"/>
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
                                        <option value="光明燈">光明燈 $500</option>
                                        <option value="太歲燈">太歲燈 $500</option>
                                        <option value="五文昌燈">五文昌燈 $500</option>
                                        <option value="福財燈">福財燈 $500</option>
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
            alert('親愛的大德您好\n玉敕大樹朝天宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!');
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
                        gotoChecked_ma();
                    }
                    else {
                        alert('親愛的大德您好\n玉敕大樹朝天宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    }
                }
                else {
                    alert('親愛的大德您好\n玉敕大樹朝天宮 2025點燈活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
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
            if (res.overnumType == 3) {
                alert("光明燈已額滿，請重新填單。或至其他宮廟點燈。");
            }
            else if (res.overnumType == 4) {
                alert("太歲燈已額滿，請重新填單。或至其他宮廟點燈。");
            }
            else if (res.overnumType == 5) {
                alert("五文昌燈已額滿，請重新填單。或至其他宮廟點燈。");
            }
            else if (res.overnumType == 6) {
                alert("福財燈已額滿，請重新填單。或至其他宮廟點燈。");
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
                    $("#bless_service_" + index).val(item.LightsString);

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

    function gotoChecked_ma() {
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
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        LightsString_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val());                                     //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val());                                       //祈福人信箱
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            LightsString_Tag.push($("#bless_service_" + i).val());                              //服務項目
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
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            LightsString_Tag: JSON.stringify(LightsString_Tag),
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
