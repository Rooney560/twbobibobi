<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchLog.aspx.cs" Inherits="Temple.SearchLog" %>

<%@ Register src="Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc2" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="保必保庇數位平台-資料查詢" /><!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/SearchLog.aspx" /><!--網址：請補上網址-->
    <meta name="description" content="保必保庇數位平台" /><!--簡介-->
    <meta property="og:description" content="保必保庇數位平台" /><!--簡介-->
    <meta property="og:site_name" content="保必保庇數位平台-資料查詢" /><!--標題-->
    <meta name="keywords" content="遠傳電信,行動電話,多媒體服務,小額付費,簡訊,門號,帳單,繳費,手機"/>
    <meta property="og:type" content="website" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.png" />
    <meta name="twitter:image:src" content="images/fb.png" />
    <link rel="image_src" href="images/fb.png" />
    
    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />
    <title>保必保庇數位平台-資料查詢</title>
    <!--============================== css start ==============================-->
    <!--預設載入css-->
    <link href="css/search/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/search/style.css" />
    <link href="https://bobibobi.tw/Temples/css/css-loader.css" rel="stylesheet" />

    <!--輪播-->
    <link rel="stylesheet" type="text/css" href="css/search/slick.css"/>
    <link rel="stylesheet" type="text/css" href="css/search/slick-theme.css"/>
    <style type="text/css">
    </style>
    <!--============================== css close ==============================-->
    <!--============================== script start ==============================-->
    <script src="scripts/js/jquery-3.2.1.min.js"></script>
    
    <style type="text/css">
        .lights {
            height: 474px;
            margin-top : -150px;
        }
        .lights div {
            display: block;
            position: relative;
            height: 474px;
        }
        .lights > div {
            top: 0;
            height: 474px;
        }
        .lights > div > img, .Lights img {
            width: 100%;
            margin: 0 auto;
        }
        .Lights  {
            top: -474px;
        }
        .lights span {
            position: relative;
            font-size: 20px;
            color: black;
            z-index: 999;
            display: block;
            font-family: '標楷體';
        }
        .lights .lightsType {
            top: 350px;
            width: 100%;
            letter-spacing: 10px;
            text-align: center;
        }
        .lights .name {
            top: 350px;
            margin: 0 auto;
            display: block;
            writing-mode: vertical-lr;
            left: -0.4vw;
        }

        
        .lights2 {
            height: 900px;
        }
        .lights2 div {
            display: block;
            position: relative;
            height: 800px;
        }
        .lights2 > div {
            top: 0;
            height: 800px;
        }
        .lights2 > div > img, .Lights img {
            width: 100%;
            margin: 0 auto;
        }
        /*.Lights  {
            top: -474px;
        }*/
        .lights2 span {
            position: absolute;
            font-size: 20px;
            color: black;
            z-index: 999;
            display: block;
            font-family: '標楷體';
        }
        .lights2 .lightsType {
            top: 440px;
            width: 100%;
            text-align: center;
            margin-left: 1vw;
        }

        @media only screen and (max-width: 720px) {
            .lights2 span {
                font-size: 15px;
            }

            .lights span {
                font-size: 15px;
            }
        }
    </style>

    <!--輪播-->
    <script src="scripts/js/slick.min.js"></script>
    <script type="text/javascript">
        //全域變數
        var applicantID = '<%=applicantID%>';

        //預設載入
        $(window).on("load", function () {
            roundPlace();

            $(document).keydown(function (event) {
                if (event.keyCode == 27) {
                    // 判断是否按下 ESC 键
                    pupClose();
                }
            });

            //關閉按鈕
            $(".ClosePup a").on("click", function () {
                pupClose();
            })

            //打開搜尋結果
            $("#DataSearch").on("click", function () {
                $(".loader").addClass("is-active");
                /*ajax放這*/
                data = {
                     //購買人
                    m_Name: $("#m_Name").val(), // 姓名
                    m_Mobile: $("#m_Mobile").val() // 號碼
                    //m_Num2String: $("#m_Num2String").val() // 訂單編號
                };

                var getUrlString = location.href;
                var url = new URL(getUrlString);
                if (url.searchParams.has('test')) {
                    pupOpen();
                }
                else {
                    ac_loadServerMethod("checkedapplicant", data, checkedapplicant);
                }

                //callback pupOpen(); //顯示結果
            })
        })

        function checkedapplicant(res) {
            $(".loader").removeClass("is-active");
            $("#DataResult").fadeIn();

            // 重導到相關頁面
            if (res.StatusCode == 1) {
                $("#Total").text("$ " + res.Total);
                $(".PupContent li").remove();
                $(".PupContent ul").append(res.Datalist);

                var resultQty = $(".PupContent ul li").length;

                //判斷資料數量
                if (resultQty == 0) {
                    $(".NoData").show();
                    $('.PupContent ul').hide();
                } else if (resultQty == 1) {
                    $(".NoData").hide();
                    $('.PupContent ul').show();
                } else if (resultQty > 1) {
                    $(".NoData").hide();
                    $('.PupContent ul').show().slick({
                        dots: true,
                        arrows: false
                    });
                }

            } else {
                $(".NoData").show();
                $("#Total").text("$ 0");
                $('.PupContent ul').hide();
            }
        }

        jQuery(document).ready(function ($) {
            if (applicantID != '0') {
                $("#DataResult").fadeIn();

                $("#Total").text("$ <%=Total %>");

                $(".PupContent li").remove();
                $(".PupContent ul").append("<%=Datalist %>");

                var resultQty = $(".PupContent ul li").length;

                //判斷資料數量
                if (resultQty == 0) {
                    $(".NoData").show();
                    $('.PupContent ul').hide();
                } else if (resultQty == 1) {
                    $(".NoData").hide();
                    $('.PupContent ul').show();
                } else if (resultQty > 1) {
                    $(".NoData").hide();
                    $('.PupContent ul').show().slick({
                        dots: true,
                        arrows: false
                    });
                }
            }
        });

        //自適應
        $(window).resize(function () {
            roundPlace();
        });

        //判斷網頁高度
        function roundPlace() {
            bw = $(window).width() / 2;
            bh = $(window).height() / 2;

            rw = $(".Round").outerWidth() / 2;
            rh = $(".Round").outerHeight() / 2;

            rLeft = bw - rw;
            rTop = bh - rh;

            $(".Round").css({
                "left": rLeft + "px",
                "top": rTop + "px",
                "opacity": 1
            });
        }

        //搜尋結果呈現
        function pupOpen() {
            $("#DataResult").fadeIn();
            var resultQty = $(".PupContent ul li").length;

            var index = 0;
            $(".PupContent ul li").each(function () {
                var name = $('.name', this);
                var img = $('img', this);

                //if (name.length > 0) {
                //    alert($('.name', this).text());
                //}

                if (img.length > 0) {
                    img.each(function () {
                        var imgh = $(this).height();
                        $('.PupContent ul li .Light').eq(index).css("top", ("-" + imgh + "px"))

                        var url = img.attr('src');
                        if (url.indexOf('lights2') >= 0) {
                            $(".PupContent ul li .lights2 span").css('color', "#752006");
                            $('.PupContent ul li .lights2').css("height", ((imgh * 1.1) + "px"))

                            var h = imgh * (440 / 723);
                            $('.PupContent ul li .lightsType').eq(index).css("top", h + "px");
                        }
                        if (url.indexOf('lights.png') >= 0) {
                            $('.PupContent ul li .lights').css("height", ((imgh * 1.1) + "px"))

                            var h = imgh * (350 / 700);
                            $('.PupContent ul li .lightsType').eq(index).css("top", h + "px");
                            $('.PupContent ul li .name').css("top", h + "px");
                        }
                    });
                }

                index++;
            });

            //判斷資料數量
            if (resultQty == 0) {
                $(".NoData").show();
                $('.PupContent ul').hide();
            } else if (resultQty == 1) {
                $(".NoData").hide();
                $('.PupContent ul').show();
            } else if (resultQty > 1) {
                $(".NoData").hide();
                $('.PupContent ul').show().slick({
                    dots: true,
                    arrows: false
                });
            }
        }

        //搜尋結果關閉並清除結果
        function pupClose() {
            $("#DataResult").fadeOut(500, function () { $(".PupContent ul").remove(); $(".PupContent").append("<ul></ul>"); });
            if (applicantID != '0') {
                location.href = "https://bobibobi.tw/SearchLog.aspx";
            }
        }
    </script>
    <!--============================== script close ==============================-->
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
    <uc2:AjaxClientControl ID="AjaxClientControl1" runat="server" />
    <div class="loader loader-default" data-text="搜尋中，請稍等。"></div>
    <div id="wrap">    
        <!--表單 Start-->
	    <div id="FormBox">
            <div class="title"><h1>保必保庇數位平台</h1></div>
            <div class="Subtitle">資料查詢</div>

            <div class="Form">
                <!--to：工程師 搜尋表單 //start-->  
                <form>
                    <div class="infoTxt">請輸入購買人資料</div>
                    <div class="inputTxt"><input type="text" id="m_Name" placeholder="請輸入購買人姓名" /></div>
                    <div class="inputTxt"><input type="text" id="m_Mobile" placeholder="請輸入購買人手機" /></div>
                    <%--<div class="inputTxt">
                        <select name="m_Year" class="" id="m_Year">
                            <option value="2023" selected="selected">2023</option>
                            <option value="2024">2024</option>
                        </select>
                    </div>--%>
                    <%--<div class="inputTxt"><input type="text" id="m_Num2String" placeholder="請輸入訂單編號" /></div>--%>
                    <div class="inputBtn"><input type="button" id="DataSearch" value="查詢" /></div>
                </form>
                <!--to：工程師 搜尋表單 //end-->
            </div>
        </div>
        <!--表單 End-->
    
    
        <!--頁面美術設定 Start-->
        <div id="WebView">
            <div class="Fan">
                <div><img src="images/fan_L.png" width="282" height="284" alt=""/></div>
                <div><img src="images/fan_R.png" width="282" height="284" alt=""/></div>
            </div>
            <div class="lantern">
                <div><img src="images/lantern_L.png" width="222" height="641" alt=""/></div>
                <div><img src="images/lantern_R.png" width="222" height="641" alt=""/></div>
            </div>
            <div class="leaf">
                <div><img src="images/leaf_L.png" width="188" height="319" alt=""/></div>
                <div><img src="images/leaf_R.png" width="188" height="319" alt=""/></div>
            </div>
            <div class="curtain">
                <div><img src="images/curtain_L.png" width="392" height="539" alt=""/></div>
                <div><img src="images/curtain_R.png" width="392" height="539" alt=""/></div>
            </div>
            <div class="temple">
                <div>
                    <img src="images/lantern_F_L.png" width="188" height="373" alt=""/>
                    <div class="Light"><img src="images/lantern_F_Light.png" width="315" height="315" alt=""/></div>
                </div>
                <div>
                    <img src="images/lantern_F_R.png" width="188" height="373" alt=""/>
                    <div class="Light"><img src="images/lantern_F_Light.png" width="315" height="315" alt=""/></div>
                </div>
            </div>
            <div class="Round" style="opacity: 0;"></div>
            <div class="bg_flower"></div>
        </div>
        <!--頁面美術設定 End-->
    
    </div><!--//warp end-->

    <div id="DataResult">

        <div class="PubBox">
            <div class="PupTitle">結果</div>
            <div class="DataUser">
                <div><div>總金額</div><div id="Total">$ 0</div></div>
            </div>
            <div class="PupContent">
                <div class="NoData">查無資料</div>
                <%--<div class="lights2"> 
                    <div> 
                    <span class="lightsType">文昌燈</span> 
                    <span class="name">吳保庇</span> 
                    <img src="images/lights2.png?t=77777" width="188" height="373" alt="" /> 
                    <div class="Lights Light"> 
                    <img src="images/lights2_Light.png?t=77777" width="315" height="315" alt="" /> 
                    </div></div></div>--%>
                <ul>          
                    <li >
                        <div>
                            <div>購買人姓名</div>
                            <div>楊O興</div>
                        </div>
                        <div>
                            <div>購買人手機</div>
                            <div>0934***020</div>
                        </div>
                        <div>
                            <div>宮廟名稱</div>
                            <div>大甲鎮瀾宮</div>
                        </div>
                        <div>
                            <div>訂單編號</div>
                            <div>Y0006</div>
                        </div>
                        <div>
                            <div>祈福人姓名</div>
                            <div>楊O興</div>
                        </div>
                        <div>
                            <div>祈福人電話</div>
                            <div>0934***020</div>
                        </div>
                        <div>
                            <div>農歷生日</div>
                            <div>民國78年12月20日</div>
                        </div>
                        <div>
                            <div>居住地址</div>
                            <div>400 臺中市中區潭陽路59巷**號***</div>
                        </div>
                        <div>
                            <div>服務項目</div>
                            <div>光明燈</div>
                        </div>
                        <div>
                            <div>金額</div>
                            <div>620</div>
                        </div>
                        <div>
                            <div>狀態</div>
                            <div>已付款</div>
                        </div>
                        <div class="lights2">
                            <div>
                                <span class="lightsType">光　明　燈<br />
                                    楊Ｏ興</span>
                                <img src="images/lights2.png?t=77777" alt="">
                                <div class="Light2 Light">
                                    <img src="images/lights2_Light.png?t=77777" alt="">
                                </div>
                            </div>
                        </div>
                    </li>
                    <!--t○工程師：li標籤內為資料結果，模擬6筆資料//start-->
                    <li>
                        <div><div>宮廟名稱</div><div>大甲鎮瀾宮</div></div>
                        <div><div>訂單編號</div><div>A12345</div></div>
                        <div><div>居住地址</div><div>台中市中區臺灣大道一段888巷888弄888號</div></div>
                        <div><div>生日(農曆)</div><div>民國088年8月8日</div></div>
                        <div><div>燈種</div><div>光明燈</div></div>
                        <div><div>點燈年度</div><div>112年</div></div>
                        <div><div>金額</div><div>620元</div></div>
                        <div><div>狀態</div><div>已繳款</div></div>
                        <div class="lights">
                            <div>
                                <span class="lightsType">元神光明燈</span>
                                <span class="name">楊Ｏ興</span>
                                <img src="images/lights.png?t=77777" alt="">
                                <div class="Light2 Light">
                                    <img src="images/lights_Light.png?t=77777" alt="">
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div><div>宮廟名稱</div><div>大甲鎮瀾宮</div></div>
                        <div><div>訂單編號</div><div>B12345</div></div>
                        <div><div>居住地址</div><div>台中市中區臺灣大道一段888巷888弄888號</div></div>
                        <div><div>生日(農曆)</div><div>民國088年8月8日</div></div>
                        <div><div>燈種</div><div>光明燈</div></div>
                        <div><div>點燈年度</div><div>112年</div></div>
                        <div><div>金額</div><div>620元</div></div>
                        <div><div>狀態</div><div>已繳款</div></div>
                    </li>
                    <li>
                        <div><div>宮廟名稱</div><div>大甲鎮瀾宮</div></div>
                        <div><div>訂單編號</div><div>C12345</div></div>
                        <div><div>居住地址</div><div>台中市中區臺灣大道一段888巷888弄888號</div></div>
                        <div><div>生日(農曆)</div><div>民國088年8月8日</div></div>
                        <div><div>燈種</div><div>光明燈</div></div>
                        <div><div>點燈年度</div><div>112年</div></div>
                        <div><div>金額</div><div>620元</div></div>
                        <div><div>狀態</div><div>已繳款</div></div>
                    </li>
                    <li>
                        <div><div>宮廟名稱</div><div>大甲鎮瀾宮</div></div>
                        <div><div>訂單編號</div><div>D12345</div></div>
                        <div><div>居住地址</div><div>台中市中區臺灣大道一段888巷888弄888號</div></div>
                        <div><div>生日(農曆)</div><div>民國088年8月8日</div></div>
                        <div><div>燈種</div><div>光明燈</div></div>
                        <div><div>點燈年度</div><div>112年</div></div>
                        <div><div>金額</div><div>620元</div></div>
                        <div><div>狀態</div><div>已繳款</div></div>
                    </li>
                    <li>
                        <div><div>宮廟名稱</div><div>大甲鎮瀾宮</div></div>
                        <div><div>訂單編號</div><div>E12345</div></div>
                        <div><div>居住地址</div><div>台中市中區臺灣大道一段888巷888弄888號</div></div>
                        <div><div>生日(農曆)</div><div>民國088年8月8日</div></div>
                        <div><div>燈種</div><div>光明燈</div></div>
                        <div><div>點燈年度</div><div>112年</div></div>
                        <div><div>金額</div><div>620元</div></div>
                        <div><div>狀態</div><div>已繳款</div></div>
                    </li>
                    <!--t○工程師：li標籤內為資料結果，模擬6筆資料 //end-->
                
                </ul>
            </div>
            <div class="ClosePup">
                <a href="javascript:;" title="關閉"><span></span><span></span></a>
            </div>
        </div>
    
    </div>  
</body>
</html>
