<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeService_TaoistJiaoCeremony_da.aspx.cs" Inherits="twbobibobi.Temples.templeService_TaoistJiaoCeremony_da" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="重修慶成祈安七朝清醮活動|大甲鎮瀾宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/templeService_TaoistJiaoCeremony_da.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="重修慶成祈安七朝清醮活動|大甲鎮瀾宮|合作宮廟|【保必保庇】線上宮廟服務平臺" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>重修慶成祈安七朝清醮活動|大甲鎮瀾宮|合作宮廟|【保必保庇】線上宮廟服務平臺</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css?t=15963258" />
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

        .select select {
            width: calc(20vw + 10px);
        }

        .TempleImg img {
            padding-bottom: 5px;
        }
        
        video {
            max-width: 100%;
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
                    <li><a href="https://bobibobi.tw/Temples/templeInfo.aspx?a=3" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                    <li>重修慶成祈安七朝清醮活動</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="images/temple/taoistJiaoCeremony_da01.jpg?t=8895874" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da02.jpg?t=8857885" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da03.jpg?t=8857885" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da04.jpg?t=8857885" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da05.jpg?t=8857885" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da06.jpg?t=8857885" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da07.jpg?t=8895874" width="1160" height="550" alt="" />
                    <img src="images/temple/taoistJiaoCeremony_da08.jpg?t=8877777" width="1160" height="550" alt="" />
                </div>
                <h1 class="TempleName">大甲鎮瀾宮重修慶成祈安七朝清醮活動</h1>
                <div class="TempleServiceInfo">                    
                    <div class="EventServiceContent">
                        <div class="EventTime">
                            <div>活動開始日期：</div>
                        <div id="startTime">2024/09/23 00:00</div>
                            <br />
                            <div>活動截止日期：</div>
                            <div id="endTime">2024/12/01 23:59</div>
                        </div>
                        <div class="EventServiceContent">
                            <div>
                                <h2>普渡施食 $1000元 <span style="color: red;">(最多可填兩位祈福人姓名)</span>
                                <span id="taoistJiaoCeremony1" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>王船添載(天錢天庫) $600元 <span style="color: red;">(只能填一位祈福人姓名；活動截止日期：2024/11/15 23:59)</span>
                                <span id="taoistJiaoCeremony2" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>王船添載(添載物資) $600元 <span style="color: red;">(只能填一位祈福人姓名；活動截止日期：2024/11/15 23:59)</span>
                                <span id="taoistJiaoCeremony3" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <h2>公斗 $1000元 <span style="color: red;">(最多可填六位祈福人姓名)</span>
                                <span id="taoistJiaoCeremony4" style="color:red" class="content_a" runat="server">(已額滿)</span></h2>
                                <br />
                                <h2>燃放水燈(大) $2200元 <span style="color: red;">(最多可填兩位祈福人姓名；活動截止日期：2024/11/25 23:59)</span>
                                <span id="taoistJiaoCeremony5" style="color:red" class="content_a" runat="server">(已額滿)</span></h2><h5>(高3尺/面寬2尺2/深度1尺2)</h5>
                                <br />
                                <h2>燃放水燈(中) $1000元 <span style="color: red;">(最多可填兩位祈福人姓名；活動截止日期：2024/11/25 23:59)</span>
                                <span id="taoistJiaoCeremony6" style="color:red" class="content_a" runat="server">(已額滿)</span></h2><h5>(高2尺/面寬1尺3/深度8吋)</h5>
                                <br />
                                <h2>燃放水燈(小) $600元 <span style="color: red;">(最多可填兩位祈福人姓名；活動截止日期：2024/11/25 23:59)</span>
                                <span id="taoistJiaoCeremony7" style="color:red" class="content_a" runat="server">(已額滿)</span></h2><h5>(高1尺2/面寬1尺/深度5吋)</h5>
                                <br />
                                <div class="track-temple">
                                    <button type="button" class="d-lg-inline-flex btn bg-custom-primary d-flex justify-content-center align-items-center font-weight-bold text-white rounded-12px waves-effect waves-light" data-toggle="modal" data-target="#ModalContentShow" style="font-size: 1.25rem; letter-spacing: 1px;">
                                        <span class="mr-2">活動說明</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24">
                                            <path fill="currentColor" d="M11.7 18q-2.4-.125-4.05-1.85T6 12q0-2.5 1.75-4.25T12 6q2.425 0 4.15 1.65T18 11.7l-2.1-.625q-.325-1.35-1.4-2.212T12 8q-1.65 0-2.825 1.175T8 12q0 1.425.863 2.5t2.212 1.4zm1.2 3.95q-.225.05-.45.05H12q-2.075 0-3.9-.788t-3.175-2.137T2.788 15.9T2 12t.788-3.9t2.137-3.175T8.1 2.788T12 2t3.9.788t3.175 2.137T21.213 8.1T22 12v.45q0 .225-.05.45L20 12.3V12q0-3.35-2.325-5.675T12 4T6.325 6.325T4 12t2.325 5.675T12 20h.3zm7.625.55l-4.275-4.275L15 22l-3-10l10 3l-3.775 1.25l4.275 4.275z"></path>
                                        </svg></button>

                                    <div class="modal fade" id="ModalContentShow" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="z-index: 9999; display: none;" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document" style="max-width: 45rem; margin: 1.75rem auto;">
                                            <div class="modal-content rounded-12px">
                                                <div class="modal-header">
                                                    <h5 class="h5 modal-title title-font">活動說明
                                                    </h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" aria-hidden="true">
                                                            <path fill="#161616" d="M6.4 19L5 17.6l5.6-5.6L5 6.4L6.4 5l5.6 5.6L17.6 5L19 6.4L13.4 12l5.6 5.6l-1.4 1.4l-5.6-5.6z"></path>
                                                        </svg></button>
                                                </div>
                                                <div class="modal-body w-100">
                                                    <div class="gridLayout bg-transparent" style="grid-template-columns: 100%;">
                                                        <div class="listGroupItem py-4 px-3">
                                                            <img decoding="async" loading="lazy" width="800" height="600" src="images/temple/taoistJiaoCeremony_da09.jpg?t=8877777" alt="Image 1" class="img-fluid rounded-12px ignore">
                                                            <img decoding="async" loading="lazy" width="800" height="600" src="images/temple/taoistJiaoCeremony_da10.png?t=8888888" alt="Image 1" class="img-fluid rounded-12px ignore">
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
                        </div>
                        <p>
                            <br />
                            <div class="IndexVideo">
                                <h1 class="TempleName">活動介紹</h1>
                                <video src="https://bobibobi.tw/v/TaoistJiaoCeremony_da_v2.mp4" controls>
                                    <source src="https://bobibobi.tw/v/TaoistJiaoCeremony_da_v2.mp4"></source>
                                </video>
                            </div>    
                            <br />
                            <a href="https://yun30.pse.is/6eabyl" target="_blank">FB粉絲募集中！現在只要在保必保庇粉絲團按讚+分享，截圖並私訊小編，就可獲得錢母小紅包！點此跳轉
                                <img src="https://bobibobi.tw/Temples/images/community_icon_01.png" style="width: 36px; display: inline;" width="45" height="45" alt="" /></a>
                            <br />
                            <a href="line://ti/p/@bobibobi.tw" target="_blank">另外加碼！加入LINE好友並填寫註冊資料，即可獲得錢母小紅包！
                                數量有限，送完為止喔！點此跳轉<img src="https://bobibobi.tw/Temples/images/community_icon_02.png" style="width: 36px; display: inline;" width="45" height="45" alt="" /></a>
                        </p>
                    </div>
                </div>

                <div class="OrderForm">
                    <form>
                        <div class="FormTitle_A">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                        <div class="FormInput text_s">
                            <label>購買人姓名</label><input name="member_name" type="text" class="required" id="member_name" placeholder="請輸入購買人姓名"/>
                        </div>
                        <div class="FormInput tel">
                            <label>購買人電話</label><input name="member_tel" type="tel" class="required" id="member_tel" placeholder="請輸入聯絡電話"/>
                        </div>
                        <div class="FormInput mail">
                            <label>購買人Email</label><input name="member_mail" type="text" class="" id="member_mail" placeholder="請輸入Email(選填)"/>
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
                                <div class="FormTitle_B">祈福人<span>（每欄祈福人姓名限填一位姓名，每種服務項目有其對應祈福人數量限制。如欄位不夠，請點選增加祈福人。）</span></div>
                                <div class="FormInput text_s">
                                    <label>祈福人姓名</label><input name="bless_name_1" type="text" maxlength="5" class="required" id="bless_name_1" placeholder="請輸入祈福人姓名"/>
                                </div>
                                <div id="zampname2_1" name="zampname2_1" class="FormInput text_s name2">
                                    <label>祈福人姓名2</label><input name="bless_name2_1" type="text" maxlength="5" class="" id="bless_name2_1" placeholder="請輸入祈福人姓名2"/>
                                </div>
                                <div id="zampname3_1" name="zampname3_1" class="FormInput text_s name3">
                                    <label>祈福人姓名3</label><input name="bless_name3_1" type="text" maxlength="5" class="" id="bless_name3_1" placeholder="請輸入祈福人姓名3"/>
                                </div>
                                <div id="zampname4_1" name="zampname4_1" class="FormInput text_s name4">
                                    <label>祈福人姓名4</label><input name="bless_name4_1" type="text" maxlength="5" class="" id="bless_name4_1" placeholder="請輸入祈福人姓名4"/>
                                </div>
                                <div id="zampname5_1" name="zampname5_1" class="FormInput text_s name5">
                                    <label>祈福人姓名5</label><input name="bless_name5_1" type="text" maxlength="5" class="" id="bless_name5_1" placeholder="請輸入祈福人姓名5"/>
                                </div>
                                <div id="zampname6_1" name="zampname6_1" class="FormInput text_s name6">
                                    <label>祈福人姓名6</label><input name="bless_name6_1" type="text" maxlength="5" class="" id="bless_name6_1" placeholder="請輸入祈福人姓名6"/>
                                </div>
                                <div class="FormInput tel">
                                    <label>祈福人電話</label><input name="bless_tel_1" type="tel" class="required" id="bless_tel_1" placeholder="請輸入祈福人聯絡電話"/>
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
                                        <option value="1">祈安七朝清醮-普渡施食 $1000</option>
                                        <option value="2">祈安七朝清醮-王船添載(天錢天庫) $600</option>
                                        <option value="3">祈安七朝清醮-王船添載(添載物資) $600</option>
                                        <option value="4">祈安七朝清醮-公斗 $1000</option>
                                        <option value="5">祈安七朝清醮-燃放水燈(大) $2200</option>
                                        <option value="6">祈安七朝清醮-燃放水燈(中) $1000</option>
                                        <option value="7">祈安七朝清醮-燃放水燈(小) $600</option>
                                    </select>
                                </div>
                                <div class="Zamp">
                                    <div id="bless_zamp_1" name="bless_zamp_1" style="display:none;">
                                        <div class="FormInput select">
                                            <label>寄送方式</label>
                                            <select name="bless_sendback_1" class="required" id="bless_sendback_1">
                                                <option selected="selected" value="N">不寄回(會轉送給弱勢團體)</option>
                                                <option value="Y">寄回(加收運費250元)</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div id="bless_rec_1" name="bless_rec_1" style="display:none;">
                                        <div class="FormInput text_s">
                                            <label>收件人姓名</label><input name="bless_rec_name_1" type="text" maxlength="5" class="required2" id="bless_rec_name_1" placeholder="請輸入收件人姓名" />
                                        </div>
                                        <div class="FormInput tel">
                                            <label>收件人電話</label><input name="bless_rec_tel_1" type="tel" class="required2" id="bless_rec_tel_1" placeholder="請輸入收件人聯絡電話" />
                                        </div>
                                        <div class="FormInput address">
                                            <label>收件人地址</label>
                                            <div class="RecAddress">
                                                <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_rec_zipcode_1" data-id="bless_rec_zipcode_1"></div>
                                                <div data-role="county" data-style="addr-county required2" data-name="bless_rec_county_1" data-id="bless_rec_county_1"></div>
                                                <div data-role="district" data-style="addr-district required2" data-name="bless_rec_district_1" data-id="bless_rec_district_1"></div>
                                            </div>
                                            <input name="bless_rec_address_1" type="text" class="required2" id="bless_rec_address_1" placeholder="請輸入地址" />
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

        //var getUrlString = location.href;
        //var url = new URL(getUrlString);
        //if (url.searchParams.get('ad') == '2') {
        //    $(".IndexVideo").show();
        //}
        //else {
        //    $(".IndexVideo").hide();
        //}

        $("header").addClass("active");
        $("#shop").hide();

        $("#zampname3_1").hide();
        $("#zampname4_1").hide();
        $("#zampname5_1").hide();
        $("#zampname6_1").hide();
        $(".Zamp #bless_zamp_1").show();
        $(".Zamp #bless_rec_1").hide();

        $("#bless_sendback_1").on("change", function () {
            var value = $(this).val();
            switch (value) {
                case "Y":
                    //寄回
                    $(".Zamp #bless_rec_1").show();
                    break;
                default:
                    //不寄回
                    $(".Zamp #bless_rec_1").hide();
                    break;
            }
        });

        if (!checkEndTime()) {
            alert('親愛的大德您好\n大甲鎮瀾宮 重修慶成祈安七朝清醮活動已截止！！\n感謝您的支持, 謝謝!');
        }

        if (!checkedTime('2024-11-15 23:59:59')) {
            $("#bless_service_1 option[value='2']").remove();
            $("#bless_service_1 option[value='3']").remove();
        }

        if (!checkedTime('2024-11-25 23:59:59')) {
            $("#bless_service_1 option[value='5']").remove();
            $("#bless_service_1 option[value='6']").remove();
            $("#bless_service_1 option[value='7']").remove();
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

    $('.RecAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });

    $('.InputGroup > li:last .Zamp').find('div').each(function (index) {
        var originalId = $(this).attr('id');
        if (originalId) {
            $("#" + originalId).hide();
            $("#" + originalId).hide();
        }
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

                if (newId.indexOf('sendback') >= 0) {
                    $("#" + newId).val('N');
                }

                if (newId.indexOf('service') >= 0) {
                    $("#" + newId).val('1');

                    if (!checkedTime('2024-11-15 23:59:59')) {
                        $("#" + newId + " option[value='2']").remove();
                        $("#" + newId + " option[value='3']").remove();
                    }

                    if (!checkedTime('2024-11-25 23:59:59')) {
                        $("#" + newId + " option[value='5']").remove();
                        $("#" + newId + " option[value='6']").remove();
                        $("#" + newId + " option[value='7']").remove();
                    }

                    $("#" + newId).on("change", function () {
                        var originalId = $(this).attr('id');
                        if (originalId) {
                            var id = originalId.slice(-1);
                            var value = $(this).val();
                            if (value != '') {
                                switch (value) {
                                    case "1":
                                        //祈安七朝清醮-普渡施食"
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).show();
                                        $(".Zamp #bless_rec_" + id).hide();

                                        $("#bless_sendback_" + id).on("change", function () {
                                            var value = $(this).val();
                                            switch (value) {
                                                case "Y":
                                                    //寄回
                                                    $(".Zamp #bless_rec_" + id).show();
                                                    break;
                                                default:
                                                    //不寄回
                                                    $(".Zamp #bless_rec_" + id).hide();
                                                    break;
                                            }
                                        });
                                        break;
                                    case "2":
                                        //祈安七朝清醮-王船添載(天錢天庫)"
                                        $("#zampname2_" + id).hide();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    case "3":
                                        //祈安七朝清醮-王船添載(添載物資)"
                                        $("#zampname2_" + id).hide();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    case "4":
                                        //祈安七朝清醮-公斗"
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).show();
                                        $("#zampname4_" + id).show();
                                        $("#zampname5_" + id).show();
                                        $("#zampname6_" + id).show();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    case "5":
                                        //祈安七朝清醮-燃放水燈(大)"
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    case "6":
                                        //祈安七朝清醮-燃放水燈(中)"
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    case "7":
                                        //祈安七朝清醮-燃放水燈(小)"
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).hide();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                    default:
                                        $("#zampname2_" + id).show();
                                        $("#zampname3_" + id).hide();
                                        $("#zampname4_" + id).hide();
                                        $("#zampname5_" + id).hide();
                                        $("#zampname6_" + id).hide();
                                        $(".Zamp #bless_zamp_" + id).show();
                                        $(".Zamp #bless_rec_" + id).hide();
                                        break;
                                }

                            }
                            else {
                                $("#zampname2_" + id).show();
                                $("#zampname3_" + id).hide();
                                $("#zampname4_" + id).hide();
                                $("#zampname5_" + id).hide();
                                $("#zampname6_" + id).hide();
                                $(".Zamp #bless_zamp_" + id).show();
                                $(".Zamp #bless_rec_" + id).hide();
                            }
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

            $('.InputGroup > li:last .RecAddress').find('div[data-role]').each(function (index) {
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

            $("#zampname3_" + lastblessNum).hide();
            $("#zampname4_" + lastblessNum).hide();
            $("#zampname5_" + lastblessNum).hide();
            $("#zampname6_" + lastblessNum).hide();
            $(".Zamp #bless_zamp_" + lastblessNum).show();
            $(".Zamp #bless_rec_" + lastblessNum).hide();

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
        var listcount = $('.InputGroup > li').last().attr('bless-id');
        var isValid = true;
        var isValid2 = true;

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

        for (var i = 1; i <= listcount; i++) {

            if ($("#bless_service_" + i).val() == "1") {
                if ($("#bless_sendback_" + i).val() == "Y") {
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
            }
            else {
                $("#bless_sendback_" + i).val("N");
            }

            if (!checkedTime('2024-11-25 23:59:59')) {
                if ($("#bless_service_" + i).val() != 1) {
                    if ($("#bless_service_" + i).val() != 4) {
                        isValid = false;
                        isValid2 = false
                    }
                }
            }
        }

        if (isValid) {
            // 所有欄位都已填寫
            console.log('所有欄位都已填寫');
            //alert("活動尚未開始!");

            if (location.search.indexOf('ad') >= 0 || checkedStartTime()) {
                if (checkEndTime()) {
                    gotoChecked_da();
                }
                else {
                    alert('親愛的大德您好\n大甲鎮瀾宮 2024重修慶成祈安七朝清醮活動已截止！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
                }
            }
            else {
                alert('親愛的大德您好\n大甲鎮瀾宮 2024重修慶成祈安七朝清醮活動尚未開始！！\n感謝您的支持, 謝謝!'); location = 'https://bobibobi.tw/Temples/temple.aspx'
            }
        } else {
            // 在這裡可以進行表單提交或其他相關處理
            // 有欄位未填寫
            if (isValid2) {
                $(".Notice").text("請檢查上方欄位是否都已填寫。");
                $(".Notice").addClass("active");
            }
            else {
                $(".Notice").text("您選得服務項目已截止，請重新選擇。");
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
            $("#member_county").val(res.AppCounty).trigger("change");
            $("#member_district").val(res.Appdist).trigger("change");
            $("#member_address").val(res.AppAddr);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);
                    $("#bless_service_" + index).val(item.TaoistJiaoCeremonyType);

                    $(".Zamp #bless_zamp_" + index).hide();
                    $(".Zamp #bless_rec_" + index).hide();

                    var value = item.TaoistJiaoCeremonyType;
                    var id = index;
                    switch (value) {
                        case 1:
                            //祈安七朝清醮-普渡施食"
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();

                            $("#bless_name2_" + index).val(item.Name2);
                            $("#bless_sendback_" + index).val(item.Sendback).trigger("change");
                            if (item.Sendback == "Y") {
                                $("#bless_rec_name_" + index).val(item.rName);
                                $("#bless_rec_tel_" + index).val(item.rMobile);
                                $("#bless_rec_county_" + index).val(item.rCounty).trigger("change");
                                $("#bless_rec_district_" + index).val(item.rdist).trigger("change");
                                $("#bless_rec_address_" + index).val(item.rAddr);
                            }
                            break;
                        case 2:
                            //祈安七朝清醮-王船添載(天錢天庫)"
                            $("#zampname2_" + id).hide();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();
                            break;
                        case 3:
                            //祈安七朝清醮-王船添載(添載物資)"
                            $("#zampname2_" + id).hide();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();
                            break;
                        case 4:
                            //祈安七朝清醮-公斗"
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).show();
                            $("#zampname4_" + id).show();
                            $("#zampname5_" + id).show();
                            $("#zampname6_" + id).show();

                            $("#bless_name2_" + index).val(item.Name2);
                            $("#bless_name3_" + index).val(item.Name3);
                            $("#bless_name4_" + index).val(item.Name4);
                            $("#bless_name5_" + index).val(item.Name5);
                            $("#bless_name6_" + index).val(item.Name6);
                            break;
                        case 5:
                            //祈安七朝清醮-燃放水燈(大)"
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();
                            break;
                        case 6:
                            //祈安七朝清醮-燃放水燈(中)"
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();
                            break;
                        case 7:
                            //祈安七朝清醮-燃放水燈(小)"
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();
                            break;
                        default:
                            $("#zampname2_" + id).show();
                            $("#zampname3_" + id).hide();
                            $("#zampname4_" + id).hide();
                            $("#zampname5_" + id).hide();
                            $("#zampname6_" + id).hide();

                            $("#bless_name2_" + index).val(item.Name2);
                            break;
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

    function gotoChecked_da() {
        var listcount = $('.InputGroup > li').last().attr('bless-id');

        Appname = $("#member_name").val();                      //購買人姓名
        Appmobile = $("#member_tel").val();                     //購買人電話
        Appemail = $("#member_mail").val();                     //購買人Email
        AppzipCode = $("#member_zipcode").val();                //購買人郵遞區號
        Appcounty = $("select[name='member_county']").val();    //購買人縣市
        Appdist = $("select[name='member_district']").val();    //購買人區域
        Appaddr = $("#member_address").val();                   //購買人部分地址

        name_Tag = [];
        name2_Tag = [];
        name3_Tag = [];
        name4_Tag = [];
        name5_Tag = [];
        name6_Tag = [];
        mobile_Tag = [];
        birth_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];

        sendback_Tag = [];
        rname_Tag = [];
        rmobile_Tag = [];
        rzipCode_Tag = [];
        rcounty_Tag = [];
        rdist_Tag = [];
        raddr_Tag = [];
        TaoistJiaoCeremonyType_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            name2_Tag.push($("#bless_name2_" + i).val());                                   //祈福人姓名2
            name3_Tag.push($("#bless_name3_" + i).val());                                   //祈福人姓名3
            name4_Tag.push($("#bless_name4_" + i).val());                                   //祈福人姓名4
            name5_Tag.push($("#bless_name5_" + i).val());                                   //祈福人姓名5
            name6_Tag.push($("#bless_name6_" + i).val());                                   //祈福人姓名6
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址
            TaoistJiaoCeremonyType_Tag.push($("#bless_service_" + i).val());                    //服務項目
            if ($("#bless_service_" + i).val() == "1") {
                var sendback = $("select[name='bless_sendback_" + i + "']").val();
                sendback_Tag.push(sendback);                                                        //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費250元)
            }
            else {
                sendback_Tag.push('N');                                                        //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費250元)
            }
            rname_Tag.push($("#bless_rec_name_" + i).val());                                    //收件人姓名
            rmobile_Tag.push($("#bless_rec_tel_" + i).val());                                   //收件人電話
            rzipCode_Tag.push($("#bless_rec_zipcode_" + i).val());                              //收件人郵政區號
            rcounty_Tag.push($("select[name='bless_rec_county_" + i + "']").val());             //收件人縣市
            rdist_Tag.push($("select[name='bless_rec_district_" + i + "']").val());             //收件人區域
            raddr_Tag.push($("#bless_rec_address_" + i).val());                                 //收件人部分地址
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            Appemail: Appemail,
            AppzipCode: AppzipCode,
            Appcounty: Appcounty,
            Appdist: Appdist,
            Appaddr: Appaddr,
            name_Tag: JSON.stringify(name_Tag),
            name2_Tag: JSON.stringify(name2_Tag),
            name3_Tag: JSON.stringify(name3_Tag),
            name4_Tag: JSON.stringify(name4_Tag),
            name5_Tag: JSON.stringify(name5_Tag),
            name6_Tag: JSON.stringify(name6_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            sendback_Tag: JSON.stringify(sendback_Tag),
            rname_Tag: JSON.stringify(rname_Tag),
            rmobile_Tag: JSON.stringify(rmobile_Tag),
            rzipCode_Tag: JSON.stringify(rzipCode_Tag),
            rcounty_Tag: JSON.stringify(rcounty_Tag),
            rdist_Tag: JSON.stringify(rdist_Tag),
            raddr_Tag: JSON.stringify(raddr_Tag),
            TaoistJiaoCeremonyType_Tag: JSON.stringify(TaoistJiaoCeremonyType_Tag),
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

<!-----選項變化欄位----->
<script>
    $("#bless_service_1").on("change", function () {
        var value = $(this).val();
        if (value != '') {
            switch (value) {
                case "1":
                    //祈安七朝清醮-普渡施食"
                    $("#zampname2_1").show();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").show();
                    $(".Zamp #bless_rec_1").hide();

                    $("#bless_sendback_1").on("change", function () {
                        var value = $(this).val();
                        switch (value) {
                            case "Y":
                                //寄回
                                $(".Zamp #bless_rec_1").show();
                                break;
                            default:
                                //不寄回
                                $(".Zamp #bless_rec_1").hide();
                                break;
                        }
                    });
                    break;
                case "2":
                    //祈安七朝清醮-王船添載(天錢天庫)"
                    $("#zampname2_1").hide();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                case "3":
                    //祈安七朝清醮-王船添載(添載物資)"
                    $("#zampname2_1").hide();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                case "4":
                    //祈安七朝清醮-公斗"
                    $("#zampname2_1").show();
                    $("#zampname3_1").show();
                    $("#zampname4_1").show();
                    $("#zampname5_1").show();
                    $("#zampname6_1").show();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                case "5":
                    //祈安七朝清醮-燃放水燈(大)"
                    $("#zampname2_1").show();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                case "6":
                    //祈安七朝清醮-燃放水燈(中)"
                    $("#zampname2_1").show();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                case "7":
                    //祈安七朝清醮-燃放水燈(小)"
                    $("#zampname2_1").show();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").hide();
                    $(".Zamp #bless_rec_1").hide();
                    break;
                default:
                    $("#zampname2_1").show();
                    $("#zampname3_1").hide();
                    $("#zampname4_1").hide();
                    $("#zampname5_1").hide();
                    $("#zampname6_1").hide();
                    $(".Zamp #bless_zamp_1").show();
                    $(".Zamp #bless_rec_1").hide();
                    break;
            }
        }
        else {
            $("#zampname2_1").show();
            $("#zampname3_1").hide();
            $("#zampname4_1").hide();
            $("#zampname5_1").hide();
            $("#zampname6_1").hide();
            $(".Zamp #bless_zamp_1").show();
            $(".Zamp #bless_rec_1").hide();
        }
    });
</script>
