    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="service.aspx.cs" Inherits="Temple.Temples.service" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta property="og:title" content="信眾服務|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/service.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="信眾服務|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>信眾服務|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <style type="text/css">
        .ServiceTempleList {
            width: calc(100% - 11vw);
        }

        .ServiceType {
            width: 10vw;
            margin-right: 1vw;
            text-align: right;
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
        <article id="Service" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li>信眾服務</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                
                <!--↓↓範例 /Start↓↓-->
                <div class="ServiceBk">
                    <div class="ServiceTitle"><span>祈福點燈</span></div>
                    <!--大項目服務名稱-->
                    <ul class="ServiceList">

                        <!--細項服務項目-->
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_04.png" width="50" height="50" alt="" /></div>
                                <div>光明燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <!--↓↓提供服務宮廟 (一個li為一個宮廟)↓↓-->
                                    <%--<li><a href="templeService_lights_da.aspx?t=3" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <!--↑↑提供服務宮廟↑↑-->
                                    <li><a href="templeService_lights_h.aspx?t=3" title="新港奉天宮">新港奉天宮</a></li>
                                    <li><a href="templeService_lights_wu.aspx?t=3" title="北港武德宮">北港武德宮</a></li>
                                    <li><a href="templeService_lights_Fu.aspx?t=3" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a href="templeService_lights_Luer.aspx?t=3" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_ty.aspx?t=3" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=3" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a href="templeService_lights_dh.aspx?t=3" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a href="templeService_lights_Lk.aspx?t=3" title="鹿港城隍廟">鹿港城隍廟</a></li>--%>
                                    
                                    <li><a onclick="ActivityTime(1, 3)" href="javascript: void(0)" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <li><a onclick="ActivityTime(1, 4)" href="javascript: void(0)" title="新港奉天宮">新港奉天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                    <li><a onclick="ActivityTime(1, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(1, 10)" href="javascript: void(0)" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a onclick="ActivityTime(1, 21)" href="javascript: void(0)" title="鹿港城隍廟">鹿港城隍廟</a></li>
                                    <li><a onclick="ActivityTime(1, 23)" href="javascript: void(0)" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                    <li><a onclick="ActivityTime(1, 35)" href="javascript: void(0)" title="松柏嶺受天宮">松柏嶺受天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_08.png" width="50" height="50" alt="" /></div>
                                <div>安太歲</div>
                            </div>
                            <!--Icon加服務種類-->
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_da.aspx?t=4" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <li><a href="templeService_lights_h.aspx?t=4" title="新港奉天宮">新港奉天宮</a></li>
                                    <li><a href="templeService_lights_wu.aspx?t=4" title="北港武德宮">北港武德宮</a></li>
                                    <li><a href="templeService_lights_Fu.aspx?t=4" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a href="templeService_lights_Luer.aspx?t=4" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_ty.aspx?t=4" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=4" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a href="templeService_lights_dh.aspx?t=4" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a href="templeService_lights_Lk.aspx?t=4" title="鹿港城隍廟">鹿港城隍廟</a></li>--%>
                                    
                                    
                                    <li><a onclick="ActivityTime(1, 3)" href="javascript: void(0)" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <li><a onclick="ActivityTime(1, 4)" href="javascript: void(0)" title="新港奉天宮">新港奉天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                    <li><a onclick="ActivityTime(1, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(1, 10)" href="javascript: void(0)" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a onclick="ActivityTime(1, 21)" href="javascript: void(0)" title="鹿港城隍廟">鹿港城隍廟</a></li>
                                    <li><a onclick="ActivityTime(1, 23)" href="javascript: void(0)" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                    <li><a onclick="ActivityTime(1, 35)" href="javascript: void(0)" title="松柏嶺受天宮">松柏嶺受天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_07.png" width="50" height="50" alt="" /></div>
                                <div>文昌燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_da.aspx?t=5" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <li><a href="templeService_lights_Fu.aspx?t=5" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a href="templeService_lights_Luer.aspx?t=5" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_dh.aspx?t=5" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a href="templeService_lights_Lk.aspx?t=5" title="鹿港城隍廟">鹿港城隍廟</a></li>--%>
                                    
                                    <li><a onclick="ActivityTime(1, 3)" href="javascript: void(0)" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                    <li><a onclick="ActivityTime(1, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(1, 10)" href="javascript: void(0)" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a onclick="ActivityTime(1, 21)" href="javascript: void(0)" title="鹿港城隍廟">鹿港城隍廟</a></li>
                                    <li><a onclick="ActivityTime(1, 23)" href="javascript: void(0)" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                    <li><a onclick="ActivityTime(1, 35)" href="javascript: void(0)" title="松柏嶺受天宮">松柏嶺受天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_09.png" width="50" height="50" alt="" /></div>
                                <div>財神燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_wu.aspx?t=6" title="北港武德宮">北港武德宮</a></li>
                                    <li><a href="templeService_lights_Fu.aspx?t=6" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a href="templeService_lights_ty.aspx?t=6" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=6" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a href="templeService_lights_dh.aspx?t=6" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a href="templeService_lights_Lk.aspx?t=6" title="鹿港城隍廟">鹿港城隍廟</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                    <li><a onclick="ActivityTime(1, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a onclick="ActivityTime(1, 21)" href="javascript: void(0)" title="鹿港城隍廟">鹿港城隍廟</a></li>
                                    <li><a onclick="ActivityTime(1, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                    <li><a onclick="ActivityTime(1, 35)" href="javascript: void(0)" title="松柏嶺受天宮">松柏嶺受天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_09.png" width="50" height="50" alt="" /></div>
                                <div>福財燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <li><a onclick="ActivityTime(1, 23)" href="javascript: void(0)" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_10.png" width="50" height="50" alt="" /></div>
                                <div>姻緣燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=7" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=7" title="斗六五路財神宮">斗六五路財神宮</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 10)" href="javascript: void(0)" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_10.png" width="50" height="50" alt="" /></div>
                                <div>月老桃花燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=7" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=7" title="斗六五路財神宮">斗六五路財神宮</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_11.png" width="50" height="50" alt="" /></div>
                                <div>貴人燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=10" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_ty.aspx?t=10" title="桃園威天宮">桃園威天宮</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_12.png" width="50" height="50" alt="" /></div>
                                <div>福壽燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=11" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 10)" href="javascript: void(0)" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_12.png" width="50" height="50" alt="" /></div>
                                <div>福祿燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=11" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_13.png" width="50" height="50" alt="" /></div>
                                <div>寵物平安燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Luer.aspx?t=12" title="台南正統鹿耳門聖母廟">台南正統鹿耳門聖母廟</a></li>
                                    <li><a href="templeService_lights_Fw.aspx?t=12" title="斗六五路財神宮">斗六五路財神宮</a></li>--%>

                                    <li><a onclick="ActivityTime(1, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_01.png" width="50" height="50" alt="" /></div>
                                <div>藥師佛燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_Fu.aspx?t=8" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a href="templeService_lights_ty.aspx?t=8" title="桃園威天宮">桃園威天宮</a></li>--%>
                                    
                                    <li><a onclick="ActivityTime(1, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(1, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(1, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_14.png" width="50" height="50" alt="" /></div>
                                <div>龍王燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_dh.aspx?t=14" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>--%>
                                    
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_15.png" width="50" height="50" alt="" /></div>
                                <div>虎爺燈</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_lights_dh.aspx?t=14" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>--%>
                                    
                                    <li><a onclick="ActivityTime(1, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
                <!--↑↑範例 /End↑↑-->

                <div class="ServiceBk">
                    <div class="ServiceTitle"><span>中元普渡</span></div>
                    <ul class="ServiceList">
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_06.png" width="50" height="50" alt="" /></div>
                                <div>中元普渡</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>                                    
                                    <li><a onclick="ActivityTime(2, 8)" href="javascript: void(0)" title="西螺福興宮">西螺福興宮</a></li>
                                    <li><a onclick="ActivityTime(2, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(2, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a onclick="ActivityTime(2, 16)" href="javascript: void(0)" title="台東東海龍門天聖宮">台東東海龍門天聖宮</a></li>
                                    <li><a onclick="ActivityTime(2, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>

                <div class="ServiceBk">
                    <div class="ServiceTitle"><span>補財庫</span></div>
                    <ul class="ServiceList">
                        <li style="display: none;">
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>下元補庫</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <li><a onclick="ActivityTime(4, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li style="display: none;">
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>財神聖誕補庫</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_supplies2.aspx" title="北港武德宮">北港武德宮</a></li>--%>

                                    <li><a onclick="ActivityTime(5, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>企業補財庫</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_supplies3.aspx" title="北港武德宮">北港武德宮</a></li>--%>

                                    <li><a onclick="ActivityTime(6, 6)" href="javascript: void(0)" title="北港武德宮">北港武德宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>天赦日補運</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_supplies_ty.aspx" title="桃園威天宮">桃園威天宮</a></li>--%>

                                    <li><a onclick="ActivityTime(7, 14)" href="javascript: void(0)" title="桃園威天宮">桃園威天宮</a></li>
                                    <li><a onclick="ActivityTime(7, 23)" href="javascript: void(0)" title="玉敕大樹朝天宮">玉敕大樹朝天宮</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>補財庫</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_supplies_ty.aspx" title="桃園威天宮">桃園威天宮</a></li>--%>
                                    <li><a onclick="ActivityTime(16, 15)" href="javascript: void(0)" title="斗六五路財神宮">斗六五路財神宮</a></li>
                                    <li><a onclick="ActivityTime(16, 21)" href="javascript: void(0)" title="鹿港城隍廟">鹿港城隍廟</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
                
                <div class="ServiceBk">
                    <div class="ServiceTitle"><span>代拜服務</span></div>
                    <ul class="ServiceList">
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_06.png" width="50" height="50" alt="" /></div>
                                <div>供香轉運</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <li><a onclick="ActivityTime(19, 33)" href="javascript: void(0)" title="神霄玉府財神會館">神霄玉府財神會館</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_06.png" width="50" height="50" alt="" /></div>
                                <div>供花供果</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <li><a onclick="ActivityTime(21, 31)" href="javascript: void(0)" title="台灣道教總廟無極三清總道院">台灣道教總廟無極三清總道院</a></li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_06.png" width="50" height="50" alt="" /></div>
                                <div>祈安植福</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <li><a onclick="ActivityTime(23, 35)" href="javascript: void(0)" title="松柏嶺受天宮">松柏嶺受天宮</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
                
                <div class="ServiceBk" style="display: none;">
                    <div class="ServiceTitle"><span>建醮</span></div>
                    <ul class="ServiceList">
                        <li>
                            <div class="ServiceType">
                                <div>
                                    <img src="images/icon_05.png" width="50" height="50" alt="" /></div>
                                <div>七朝清醮</div>
                            </div>
                            <div class="ServiceTempleList">
                                <ul>
                                    <%--<li><a href="templeService_supplies.aspx" title="北港武德宮">北港武德宮</a></li>--%>

                                    <li><a onclick="ActivityTime(13, 3)" href="javascript: void(0)" title="大甲鎮瀾宮">大甲鎮瀾宮</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
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
    $(function () {
        $("header").addClass("active");

        var buttons = [];

        // 先把所有 a[onclick^='ActivityTime'] 收集起來
        $("a[onclick^='ActivityTime']").each(function () {
            var onclickText = $(this).attr("onclick"); // e.g. ActivityTime(13, 3)
            var args = onclickText.match(/ActivityTime\((\d+),\s*(\d+)\)/);

            if (args && args.length >= 3) {
                var kind = args[1];
                var a = args[2];
                var $btn = $(this);

                buttons.push({
                    kind: kind,
                    a: a,
                    $btn: $btn
                });
            }
        });

        // 如果沒有按鈕就不用打 API
        if (buttons.length === 0) {
            return;
        }

        // 組成批次請求資料
        var requestData = buttons.map(function (b) {
            return { Kind: parseInt(b.kind), A: parseInt(b.a) };
        });

        // 🚀 改用 JSON 專用版本
        ac_loadServerMethodJson("getActivityTimeBatch", { list: requestData }, function (res) {
            if (res.StatusCode === 1 && res.ActivityList) {
                let today = new Date();

                res.ActivityList.forEach(function (item) {
                    var btnInfos = buttons.filter(function (b) {
                        return b.kind == item.Kind && b.a == item.AdminID;
                    });

                    btnInfos.forEach(function (btnInfo) {
                        if (btnInfo && item.StartDate !== "0" && item.EndDate !== "0") {
                            let endday = new Date(item.EndDate);

                            if (endday.getTime() < today.getTime()) {
                                btnInfo.$btn.closest("li").hide();
                            }
                        }
                    });
                });
            }
        });
    })

    function ActivityTime(kind, a) {
        data = {
            kind: kind,
            a: a
        };

        ac_loadServerMethod("getActivityTime", data, getActivityTime);
    }

    function getActivityTime(res) {
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            var Name = res.Name;
            var StartDate = res.StartDate;
            var EndDate = res.EndDate;
            var Service = res.Service;

            let $btn = $("a[onclick*='ActivityTime(" + res.Kind + "," + res.A + ")']"); // 找到呼叫這個活動的按鈕

            if (StartDate == "0" && EndDate == "0") {
                // 延遲導頁，避免伺服器還沒寫完 Response 就被切斷
                setTimeout(function () {
                    location.href = res.URL;
                }, 150);
            }
            else {
                let today = new Date() //現在
                let someday = new Date(StartDate) //活動開始
                let endday = new Date(EndDate)  //活動結束
                var diff = someday.getTime() - today.getTime(); //diff = 活動開始 - 現在
                var diff_end = endday.getTime() - today.getTime(); //diff_end = 現在 - 活動結束
                //var month = diff / 1000 / 60 / 60 / 24 / 30; //現在與活動開始時間差幾個月
                //var month_end = diff_end / 1000 / 60 / 60 / 24 / 30; //現在與活動結束時間差幾個月

                //alert("today:" + today + ", sDate:" + someday + ", eDate:" + endday + ", diff:" + diff + ", diff_end:" + diff_end + ", month:" + month);

                if (diff <= 0 && diff_end >= 0) {
                    // 活動進行中 → 照常導頁（同樣加延遲）
                    setTimeout(function () {
                        location.href = res.URL;
                    }, 150);
                }
                else if (diff_end < 0) {
                    // 活動已結束 → 直接隱藏按鈕
                    $btn.closest("li").hide(); // 或 $btn.remove();
                }

                //if (month < 1 && diff_end >= 0) {
                //    if (diff <= 0 && diff_end >= 0) {
                //        location.href = res.URL;
                //    }
                //    else {
                //        alert(Name + " " + Service + " 活動即將開始！");
                //    }
                //}
                //else {
                //    //alert(Math.abs(month_end));
                //    if (diff_end <= 0 && Math.abs(month_end) <= 4) {
                //        alert(Name + " " + Service + " 活動已結束！");
                //    }
                //    else {
                //        alert(Name + " " + Service + " 活動尚未開始！");
                //    }
                //}
            }
        }
    }
</script>
