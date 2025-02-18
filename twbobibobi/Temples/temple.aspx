<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="temple.aspx.cs" Inherits="Temple.Temples.temple" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="合作宮廟|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/temple.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="大甲鎮瀾宮 北港武德宮 西螺福興宮 新港奉天宮 桃園威天宮 台南正統鹿耳門聖母廟 台東東海龍門天聖宮 斗六五路財神廟 鹿港城隍廟 玉敕大樹朝天宮 台灣道教總廟無極三清總道院" />
    <!--簡介-->
    <meta property="og:description" content="大甲鎮瀾宮 北港武德宮 西螺福興宮 新港奉天宮 桃園威天宮 台南正統鹿耳門聖母廟 台東東海龍門天聖宮 斗六五路財神廟 鹿港城隍廟 玉敕大樹朝天宮 台灣道教總廟無極三清總道院" />
    <!--簡介-->
    <meta property="og:site_name" content="合作宮廟|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>合作宮廟|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <script>
        //copyRight抓取目前年份
        $(window).on("load", function () {
            var $mydate = new Date();
            $("#NowYear").text($mydate.getFullYear());
        })
    </script>

    <style type="text/css">

        @media only screen and (min-width: 720px) {
            .IndexTempleImg img {
                height: 200px;
            }
        }
    </style>
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
                    <li>合作宮廟</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="PageTempleList">
                    <ul id="TempleList">
                        <%=TempleList %>
<%--                        <!--↓↓範例(一個li為一筆資料)↓↓-->
                        <li>
                            <a href="templeInfo.aspx?a=3" title="大甲鎮瀾宮">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">大甲鎮瀾宮</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_01">祈福點燈</li>
                                        <li class="Tag_03">中元普渡</li>
                                        <li class="Tag_04">備用01</li>
                                        <li class="Tag_05">備用02</li>
                                        <li class="Tag_06">備用03</li>
                                    </ul>
                                </div>
                            </a>
                        </li>
                        <!--↑↑範例↑↑-->


                        <li>
                            <a href="templeInfo.aspx?a=4" title="新港奉天宮">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample_h.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">新港奉天宮</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_01">祈福點燈</li>
                                        <li class="Tag_03">中元普渡</li>
                                    </ul>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="templeInfo.aspx?a=6" title="北港武德宮">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample_wu.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">北港武德宮</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_01">祈福點燈</li>
                                        <li class="Tag_02">補財庫</li>
                                        <li class="Tag_03">中元普渡</li>
                                    </ul>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="templeInfo.aspx?a=8" title="西螺福興宮">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample_Fu.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">西螺福興宮</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_03">中元普渡</li>
                                    </ul>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="templeInfo.aspx?a=9" title="桃園大廟景福宮">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample_Jing.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">桃園大廟景福宮</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_03">中元普渡</li>
                                    </ul>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="templeInfo.aspx?a=10" title="台南正統鹿耳門聖母廟">
                                <div class="IndexTempleImg">
                                    <img src="images/temple/sample_Luer.jpg" width="600" height="400" alt="" /></div>
                                <div class="IndexTempleName">台南正統鹿耳門聖母廟</div>
                                <div class="IndexTempleTag">
                                    <ul>
                                        <li class="Tag_03">中元普渡</li>
                                    </ul>
                                </div>
                            </a>
                        </li>--%>
                    </ul>
                </div>


                <!--頁碼示意-->
                <div class="pageCtrl">
                    <ul>
                        <li id="PageCtrl_F"><a href="#" title="第前頁">
                            <img src="images/pageCtrl_L1.png" width="30" height="30" alt="" /></a></li>
                        <li id="PageCtrl_P"><a href="#" title="上一頁">
                            <img src="images/pageCtrl_L2.png" width="30" height="30" alt="" /></a></li>
                        <li><a href="#" class="active" title="第1頁">1</a></li>
                        <%--<li><a href="#" title="第2頁">2</a></li>
                        <li><a href="#" title="第3頁">3</a></li>
                        <li><a href="#" title="第4頁">4</a></li>
                        <li><a href="#" title="第5頁">5</a></li>--%>
                        <li id="PageCtrl_N"><a href="#" title="下一頁">
                            <img src="images/pageCtrl_R2.png" width="30" height="30" alt="" /></a></li>
                        <li id="PageCtrl_L"><a href="#" title="最後頁">
                            <img src="images/pageCtrl_R1.png" width="30" height="30" alt="" /></a></li>
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
    })
</script>
