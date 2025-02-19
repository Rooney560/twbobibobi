<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_lights_Fw.aspx.cs" Inherits="Temple.Temples.templeService_lights_Fw" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="斗六五路財神宮|線上點燈|2025犯太歲光明燈【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_lights_Fw.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="保必保庇提供斗六五路財神宮安太歲 光明燈線上點燈，犯太歲生肖 蛇 豬 虎 猴" />
    <!--簡介-->
    <meta property="og:description" content="保必保庇提供斗六五路財神宮安太歲 光明燈線上點燈，犯太歲生肖 蛇 豬 虎 猴" />
    <!--簡介-->
    <meta property="og:site_name" content="斗六五路財神宮|線上點燈|2025犯太歲光明燈【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Temples/images/temple/lights_Fw_2025.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Temples/images/temple/lights_Fw_2025.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Temples/images/temple/lights_Fw_2025.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>斗六五路財神宮|線上點燈|2025犯太歲光明燈【保必保庇】線上宮廟服務平臺</title>
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
        .EventServiceContent > div.mobile {
            display: none;
        }
        .web {
            display: inline;
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
        @media only screen and (max-width: 720px) {
            .content_a {
                font-size: 3.8vw;
            }
            .inputBtn input {
                font-size: 5vw;
                height: 10vw;
            }
            .EventServiceContent > div.mobile {
                display: inline;
            }
            .web {
                display: none;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=15" title="斗六五路財神宮">斗六五路財神宮</a></li>
                    <li>祈福點燈</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/lights_Fw_2025.jpg" width="1160" height="550" alt="保必保庇提供斗六五路財神宮安太歲光明燈線上點燈，犯太歲生肖 蛇 豬 虎 猴" 
                        title="斗六五路財神宮線上點燈２０２５犯太歲光明燈" />
                </div>
                <h1 class="TempleName">歡迎使用《斗六五路財神宮》線上點燈服務</h1>
                <div class="TempleServiceInfo">
                    <div class="EventTime">
                        <div>活動開始日期：</div>
                        <div id="startTime">2024/11/01 00:00</div>
                        <br />
                        <div>活動截止日期：</div>
                        <div id="endTime">2025/10/31 23:59</div>
                    </div>
                    <div class="EventServiceContent">
                        <div>
                            <h2>財庫燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_1.jpg">(看圖)</a></h2>
                            <p>祈求  財庫飽滿、會賺錢不是師父、能守住財才是真功夫、錢財守不住、左手來、右手出、要如何守住財、一定要點財庫燈、由護法財神來幫您守住錢財。費用500元。</p>
                        </div>
                        <div>
                            <h2>發財燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_2.jpg">(看圖)</a></h2>
                            <p>祈求  彩卷、股市、娛樂八大行業、偏財運亨通、八方進財、財源滾滾而來、想要輕鬆進財、一定要點發財燈、由偏財神爺為您開啟偏財運。費用500元。</p>
                        </div>
                        <div>
                            <h2>月老桃花燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_3.jpg">(看圖)</a></h2>
                            <p>祈求   增添桃花緣、異性緣、求取好人緣、穩定永固加深緣份、已婚者求婚姻圓滿 夫妻和諧恩愛、未婚者求賜好姻緣、戀愛中男女求賜感情穩定融合緣份永續，業務員 可增添桃花緣，業績節節上升。費用500元。</p>
                        </div>
                        <div>
                            <h2>貴人燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_4.jpg">(看圖)</a></h2>
                            <p>祈求 貴人明現，斬除小三，化解小人與冤親債主，小人轉化為貴人，福星高照，命主光彩，費用500元。</p>
                        </div>
                        <div>
                            <h2>安太歲 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_6.jpg">(看圖)</a></h2>
                            <p>生肖 <span style="color:red; " class="content_a">蛇｜犯太歲、豬｜沖太歲、虎｜刑太歲及害太歲、猴｜刑太歲及破太歲</span>之信眾，因流年犯太歲，宜安奉太歲以保平安，費用500元</p>
                        </div>
                        <div>
                            <h2>消災延壽燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_10.jpg">(看圖)</a></h2>
                            <p>化解 冤親債主、消災解厄、延壽添福。生肖屬豬(犯死符)、鼠(犯五鬼、官符)、虎(犯喪門、吊客)、蛇(犯病符)、馬(犯天狗星)、猴(犯白虎星)宜安奉消災延壽燈，
                                以保平安。費用500元。</p>
                        </div>
                        <div>
                            <h2>寵物平安燈 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_8.jpg">(看圖)</a></h2>
                            <p>現代人流行養毛小孩以及各種寵物，情感之深厚，已是家庭中非常重要的一份子！</p>
                            <p>尤其現今社會，年輕人或晚婚或不婚，人與貓狗寵物的彼此陪伴！牠是家人是兒女，更是精神依託！</p>
                            <p>希望牠能身體健康，無災無難,長命百歲！</p>
                            <p>五路財神宮 特別設立 [ 寵物平安燈 ] 為您的小寶貝祈福，讓牠能長久陪伴著您！費用500元。</p>
                        </div>
                        <br />
                        <div id="lights_an" runat="server">
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
                            <span style="color:red;" class="content_a" >圓傘護天</span><span>-以求天地泰平，這就是斗中法器所代表的意義。</span>
                        </div>
                        <div>
                            <h2>財神斗 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_11.jpg">(看圖)</a></h2>
                            <p>祈求  財庫飽滿、會賺錢不是師父、能守住財才是真功夫、錢財守不住、左手來、右手出、要如何守住財、一定要點財神斗、由護法財神來幫您守住錢財。
                                <span style="color:red;" class="" >費用1200元/一個月、3000元/三個月。</span></p>
                        </div>
                        <div>
                            <h2>發財斗 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_12.jpg">(看圖)</a></h2>
                            <p>祈求  彩卷、股市、娛樂八大行業、偏財運亨通、八方進財、財源滾滾而來、想要輕鬆進財、一定要點發財斗、由偏財神爺為您開啟偏財運。
                                <span style="color:red;" class="" >費用1200元/一個月、3000元/三個月。</span></p>
                        </div>
                        <div>
                            <h2>姻緣斗 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_13.jpg">(看圖)</a></h2>
                            <p>祈求   增添桃花緣、異性緣、求取好人緣、穩定永固加深緣份、已婚者求婚姻圓滿 夫妻和諧恩愛、未婚者求賜好姻緣、戀愛中男女求賜感情穩定融合緣份永續，
                                業務員 可增添桃花緣，業績節節上升。<span style="color:red;" class="" >費用1200元/一個月、3000元/三個月。</span></p>
                        </div>
                        <div>
                            <h2>貴人斗 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_14.jpg">(看圖)</a></h2>
                            <p>祈求 貴人明現，斬除小三，化解小人與冤親債主，小人轉化為貴人，福星高照，命主光彩，<span style="color:red;" class="" >費用1200元/一個月、3000元/三個月。</span></p>
                        </div>
                        <div>
                            <h2>消災延壽斗 <a target="_blank" class="content_a" href="https://bobibobi.tw/Temples/images/temple/product/Fw/1_15.jpg">(看圖)</a></h2>
                            <p>化解 冤親債主、消災解厄、延壽添福。生肖屬豬(犯死符)、鼠(犯五鬼、官符)、虎(犯喪門、吊客)、蛇(犯病符)、馬(犯天狗星)、猴(犯白虎星)宜安奉消災延壽斗，
                                以保平安。<span style="color:red;" class="" >費用1200元/一個月、3000元/三個月。</span></p>
                        </div>
                        <p>
                            <br />
                            <h2>凡點任一盞燈或安斗，即贈送開運錢母及感謝狀。<a target="_blank" class="content_a web" href="https://bobibobi.tw/Temples/images/temple/lights_Fw_01.png">(看圖)</a></h2>
                            <div class="TempleImg mobile">
                                <h2>錢母圖片如下圖：</h2>
                                <img src="images/temple/lights_Fw_01.png" width="1160" height="550" alt="" />
                            </div>
                        </p>

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
                                <div>（祈福人限填一位，每種點燈/安斗項目對應一位祈福人。如需多位或不同項目，請點選增加祈福人。）</div>
                                <div class="FormInput select">
                                    <label>點燈項目</label>
                                    <select name="bless_service_1" class="required3" id="bless_service_1">
                                        <option value="">請選擇</option>
                                        <option value="財庫燈">財庫燈 $500</option>
                                        <option value="發財燈">發財燈 $500</option>
                                        <option value="月老桃花燈">月老桃花燈 $500</option>
                                        <option value="貴人燈">貴人燈 $500</option>
                                        <option value="安太歲">安太歲 $500</option>
                                        <option value="消災延壽燈">消災延壽燈 $500</option>
                                        <option value="寵物平安燈">寵物平安燈 $500</option>
                                    </select>
                                </div>
                                <div class="FormInput select">
                                    <label>安斗項目</label>
                                    <select name="bless_service2_1" class="required3" id="bless_service2_1">
                                        <option value>請選擇</option>
                                        <option value="財神斗/一個月">財神斗/一個月 $1200</option>
                                        <option value="發財斗/一個月">發財斗/一個月 $1200</option>
                                        <option value="姻緣斗/一個月">姻緣斗/一個月 $1200</option>
                                        <option value="貴人斗/一個月">貴人斗/一個月 $1200</option>
                                        <option value="消災延壽斗/一個月">消災延壽斗/一個月 $1200</option>
                                        <option value="財神斗/三個月">財神斗/三個月 $3000</option>
                                        <option value="發財斗/三個月">發財斗/三個月 $3000</option>
                                        <option value="姻緣斗/三個月">姻緣斗/三個月 $3000</option>
                                        <option value="貴人斗/三個月">貴人斗/三個月 $3000</option>
                                        <option value="消災延壽斗/三個月">消災延壽斗/三個月 $3000</option>
                                    </select>
                                </div>
                                <div class="FormInput text_s">
                                    <label class="label" id="label_name_1">祈福人姓名</label><input name="bless_name_1" type="text" class="required" maxlength="5" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                    <input type="checkbox" class="checkedbox" id="bless_copy_name_1" />
                                    <label for="bless_copy_name_1" id="bless_checkednamelabel_1" style="width: auto;">同購買人姓名</label>
                                </div>
                                <div class="FormInput tel">
                                    <label class="label" id="label_tel_1">祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
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
                                    <label runat="server" id="label_sbirth_1">國曆生日</label><input name="bless_sbirth_1" type="text" class="datapicker required2" id="bless_sbirth_1" placeholder="請選擇國曆生日或農曆生日二擇一" />
                                </div>
                                <div class="FormInput email mail">
                                    <label>祈福人信箱</label><input name="bless_email_1" type="text" class="" id="bless_email_1" placeholder="請輸入祈福人Email(選填)"/>
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
                                <div class="FormInput text_s Pet" id="bless_pet_1">
                                    <div class="FormInput text_s">
                                        <label>寵物姓名</label><input name="bless_petname_1" type="text" class="petname" id="bless_petname_1" placeholder="請輸入寵物姓名" />
                                    </div>
                                    <div class="FormInput text_s">
                                        <label>寵物品種</label><input name="bless_pettype_1" type="text" class="pettype" id="bless_pettype_1" placeholder="請輸入寵物品種 (不知道請填無)" />
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
            alert('親愛的大德您好\n斗六五路財神宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!');
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

        $(".Pet").hide();

        $("#bless_service_1").change(function () {
            if ($(this).val() == "寵物平安燈") {
                $("#bless_pet_1").show();

                var label = document.getElementById("label_name_1");
                label.innerText = "飼主姓名";
                $("#bless_name_1").attr("placeholder", "請輸入飼主姓名");

                var label = document.getElementById("label_tel_1");
                label.innerText = "飼主電話";
                $("#bless_tel_1").attr("placeholder", "請輸入飼主聯絡電話");

                var label = document.getElementById("label_birth_1");
                label.innerText = "飼主農曆生日";

                var label = document.getElementById("label_sbirth_1");
                label.innerText = "飼主國曆生日";

                var label = document.getElementById("label_address_1");
                label.innerText = "飼主地址";
                $("#bless_address_1").attr("placeholder", "請輸入飼主地址");
            }
            else {
                $("#bless_pet_1").hide();

                var label = document.getElementById("label_name_1");
                label.innerText = "祈福人姓名";
                $("#bless_name_1").attr("placeholder", "請輸入祈福人姓名");

                var label = document.getElementById("label_tel_1");
                label.innerText = "祈福人電話";
                $("#bless_tel_1").attr("placeholder", "請輸入祈福人聯絡電話");

                var label = document.getElementById("label_birth_1");
                label.innerText = "農曆生日";

                var label = document.getElementById("label_sbirth_1");
                label.innerText = "國曆生日";

                var label = document.getElementById("label_address_1");
                label.innerText = "地址";
                $("#bless_address_1").attr("placeholder", "請輸入地址");
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
        $('.InputGroup > li:last').find('.Pet').each(function (index) {
            var originalId = $(this).attr('id');
            var newId = originalId.slice(0, -1) + lastblessNum;
            $(this).attr('id', newId);
            $(this).attr('name', newId);
            $(this).hide();
        });

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
                $(this).attr('id', newId);
                $(this).attr('name', newId);

                if (newId.indexOf('checkednamelabel') >= 0) {
                    $(this).attr('for', 'bless_copy_name_' + lastblessNum);
                }

                if (newId.indexOf('checkedtellabel') >= 0) {
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

            if (newId.indexOf('service') >= 0) {
                $("#" + newId).change(function () {
                    if ($(this).val() == "寵物平安燈") {
                        $("#bless_pet_" + lastblessNum).show();

                        var label = document.getElementById("label_name_" + lastblessNum);
                        label.innerText = "飼主姓名";
                        $("#bless_name_" + lastblessNum).attr("placeholder", "請輸入飼主姓名");

                        var label = document.getElementById("label_tel_" + lastblessNum);
                        label.innerText = "飼主電話";
                        $("#bless_tel_" + lastblessNum).attr("placeholder", "請輸入飼主聯絡電話");

                        var label = document.getElementById("label_birth_" + lastblessNum);
                        label.innerText = "飼主農曆生日";

                        var label = document.getElementById("label_sbirth_" + lastblessNum);
                        label.innerText = "飼主國曆生日";

                        var label = document.getElementById("label_address_" + lastblessNum);
                        label.innerText = "飼主地址";
                        $("#bless_address_" + lastblessNum).attr("placeholder", "請輸入飼主地址");
                    }
                    else {
                        $("#bless_pet_" + lastblessNum).hide();

                        var label = document.getElementById("label_name_" + lastblessNum);
                        label.innerText = "祈福人姓名";
                        $("#bless_name_" + lastblessNum).attr("placeholder", "請輸入祈福人姓名");

                        var label = document.getElementById("label_tel_" + lastblessNum);
                        label.innerText = "祈福人電話";
                        $("#bless_tel_" + lastblessNum).attr("placeholder", "請輸入祈福人聯絡電話");

                        var label = document.getElementById("label_birth_" + lastblessNum);
                        label.innerText = "農曆生日";

                        var label = document.getElementById("label_sbirth_" + lastblessNum);
                        label.innerText = "國曆生日";

                        var label = document.getElementById("label_address_" + lastblessNum);
                        label.innerText = "地址";
                        $("#bless_address_" + lastblessNum).attr("placeholder", "請輸入地址");
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
        var isValid2 = true;
        var isValid3 = true;
        var isValid4 = true;
        var isCheckedValid = $("#checkedprivate").is(":checked");

        var listcount = $('.InputGroup > li').last().attr('bless-id');

        // 遍歷每個必填欄位
        $('.required').each(function () {
            if ($(this).val() != null) {
                var value = $(this).val().trim();
                if (value === '') {
                    isValid = false;
                    $(this).addClass('unfilled');
                } else if (value != '' && $(this).hasClass('unfilled')) {
                    $(".Notice").text("");
                    $(".Notice").removeClass("active");
                    $(this).removeClass('unfilled');
                }

                if (value == "寵物平安燈") {
                    var fileDir = $(this).attr('id');
                    var suffix = fileDir.substr(fileDir.lastIndexOf("."));

                    if ($("#bless_petname_" + suffix).val() == "" || $("#bless_pettype_" + suffix).val() == "") {
                        isValid = false;
                        console.log("bless_petname_" + suffix);
                    }
                }
            }
        });

        // 遍歷每個生日欄位
        for (var i = 1; i <= listcount; i++) {
            var value_birth = $("#bless_birthday_" + i).val().trim();
            var value_sbirth = $("#bless_sbirth_" + i).val().trim();

            if (value_birth == '' && value_sbirth == '') {
                isValid = false;
                $('.required2').addClass('unfilled');
            } else if ((value_birth != '' || value_sbirth != '') && $('.required2').hasClass('unfilled')) {
                $(".Notice").text("");
                $(".Notice").removeClass("active");
                $('.required2').removeClass('unfilled');
            }
        }

        // 遍歷每個服務項目欄位
        for (var i = 1; i <= listcount; i++) {
            var value_service = $("#bless_service_" + i).val().trim();
            var value_service2 = $("#bless_service2_" + i).val().trim();

            if (value_service == '' && value_service2 == '') {
                isValid = false;
                isValid2 = false;

                $(".Notice").text("點燈/安斗項目不能為空。");
                $(".Notice").addClass("active");
                $('.required3').addClass('unfilled');
            } else if (value_service != '' && value_service2 != '') {
                isValid = false;
                isValid2 = false;

                $(".Notice").text("點燈/安斗項目只能擇一。");
                $(".Notice").addClass("active");
                $('.required3').addClass('unfilled');
            } else if ((value_service != '' || value_service2 != '') && $('.required3').hasClass('unfilled')) {
                $(".Notice").text("");
                $(".Notice").removeClass("active");
                $('.required3').removeClass('unfilled');
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
                $(".Notice").text("");
                $(".Notice").removeClass("active");
                $("#member_tel").removeClass('unfilled');
            }

            for (var i = 1; i <= listcount; i++) {

                //if ($("#bless_sendback_" + i).val() == 1) {
                //    // 遍歷每個必填欄位-有條件 (寄回欄位=1)
                //    var reslist = ["bless_rec_name_" + i, "bless_rec_tel_" + i, "bless_rec_county_" + i, "bless_rec_district_" + i, "bless_rec_address_" + i];

                //    reslist.forEach(function (value) {
                //        if ($("#" + value).val() == '') {
                //            isValid = false;
                //            $(this).addClass('unfilled');
                //        } else if (value != '' && $(this).hasClass('unfilled')) {
                //            $(this).removeClass('unfilled');
                //        }
                //    });
                //}

                value = $("#bless_tel_" + i).val().trim();
                if (value == "") {
                    $(".Notice").text("祈福人電話不能為空。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid4 = false;
                    break;
                }
                else if (!Isphone(value)) {
                    $(".Notice").text("祈福人電話格式錯誤。");
                    $(".Notice").addClass("active");
                    $("#bless_tel_" + i).addClass('unfilled');

                    isValid = false;
                    isValid4 = false;
                    break;
                }
                else {
                    if (value != '' && $("#bless_tel_" + i).hasClass('unfilled')) {
                        $("#bless_tel_" + i).removeClass('unfilled');
                    }
                }

                if ($("#bless_oversea_" + i).val() == "1") {
                    value = $("#bless_county_" + i).val();
                    if (value == '' || value == null) {
                        $(".Notice").text("祈福人地址 縣市為空，請重新選擇縣市。");
                        $(".Notice").addClass("active");
                        $("#bless_county_" + i).addClass('unfilled');

                        isValid = false;
                        isValid3 = false;
                        break;
                    }
                    else {
                        if (value != '' && $("#bless_county_" + i).hasClass('unfilled')) {
                            $("#bless_county_" + i).removeClass('unfilled');
                        }
                    }

                    value = $("#bless_district_" + i).val();
                    if (value == '' || value == null) {
                        $(".Notice").text("祈福人地址 區域為空，請重新選擇區域。");
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
                            gotoChecked_Fw();
                        }
                        else {
                            alert('親愛的大德您好\n斗六五路財神宮 2025點燈活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                        }
                    }
                    else {
                        alert('親愛的大德您好\n斗六五路財神宮 2025點燈活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                    }
                }
            } else {
                // 在這裡可以進行表單提交或其他相關處理
                // 有欄位未填寫
                if (!isValid) {
                    if (isValid4 && isValid3 && isValid2) {
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
                    if (item.LightsString.indexOf('個月') >= 0) {
                        $("#bless_service2_" + index).val(item.LightsString).trigger("change");
                    }
                    else {
                        $("#bless_service_" + index).val(item.LightsString).trigger("change");
                    }
                    $("#bless_petname_" + index).val(item.PetName);
                    $("#bless_pettype_" + index).val(item.PetType);

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

    function gotoChecked_Fw() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val().trim();      //購買人姓名
        Appmobile = $("#member_tel").val()      //購買人電話

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        leapMonth_Tag = [];
        birthtime_Tag = [];
        sbirth_Tag = [];
        email_Tag = [];
        oversea_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        LightsString_Tag = [];
        PetName_Tag = [];
        PetType_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val().trim());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val().trim());                                        //祈福人電話
            sex_Tag.push($("#bless_sex_" + i).val().trim());                                           //祈福人性別 善男 信女
            birth_Tag.push($("#bless_birthday_" + i).val().trim());                                    //祈福人農曆生日
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val().trim());                               //閏月 Y-是 N-否
            birthtime_Tag.push($("#bless_birthtime_" + i).val().trim());                               //祈福人農曆時辰
            sbirth_Tag.push($("#bless_sbirth_" + i).val().trim());                                     //祈福人國曆生日
            email_Tag.push($("#bless_email_" + i).val().trim());                                       //祈福人信箱
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
            if ($("#bless_service_" + i).val() != '') {
                LightsString_Tag.push($("#bless_service_" + i).val().trim());                          //服務項目
            }
            if ($("#bless_service2_" + i).val() != '') {
                LightsString_Tag.push($("#bless_service2_" + i).val().trim());                         //服務項目
            }
            PetName_Tag.push($("#bless_petname_" + i).val().trim());                                   //寵物姓名
            PetType_Tag.push($("#bless_pettype_" + i).val().trim());                                   //寵物品種
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
            oversea_Tag: JSON.stringify(oversea_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            LightsString_Tag: JSON.stringify(LightsString_Tag),
            PetName_Tag: JSON.stringify(PetName_Tag),
            PetType_Tag: JSON.stringify(PetType_Tag),
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

