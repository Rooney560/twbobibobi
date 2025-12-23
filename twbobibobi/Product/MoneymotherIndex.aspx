<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneymotherIndex.aspx.cs" Inherits="twbobibobi.Product.MoneymotherIndex" %>

<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc2" %>
<%@ Register src="~/Controls/PopupMessageDialogControl.ascx" tagname="PopupMessageDialogControl" tagprefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="新港奉天宮鎮宅錢母擺件" /><!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Product/MoneymotherIndex.aspx" /><!--網址：請補上網址-->
    <meta name="description" content="【奉天承運 開運擺件】
自古廟宇是神佛與人溝通的據點，人們心靈寄託之處，古代富貴人家常在家裏明顯處，掛上一幅寺廟建築相關圖畫，代表主人家精神提升與智慧展現。
欣逢 “新港媽祖”蒞台 400週年慶，新港奉天宮授權精品設計團隊，設計以新港奉天宮廟門造型之文創信物，作品以新港奉天宮廟門、媽祖與虎爺大二將軍作為設計元素。
有新港奉天宮正門之作品，收藏安置皆有置身新港奉天宮現場之氛圍。作品含『媽祖+虎爺錢母』各一枚，保庇平安的同時，也讓您財源滾滾來。
整體配色採金色，大氣外形，更象徵著開運、招財、金好額。
每件作品皆再新港奉天宮正殿過爐完成。安放明亮位置，增加正能量讓新港媽祖虎爺大二將軍天天給您賜福招財，
新港媽祖虎爺神威顯赫
保佑我們，繼承新生的氣運。" /><!--簡介-->
    <meta property="og:description" content="【奉天承運 開運擺件】
自古廟宇是神佛與人溝通的據點，人們心靈寄託之處，古代富貴人家常在家裏明顯處，掛上一幅寺廟建築相關圖畫，代表主人家精神提升與智慧展現。
欣逢 “新港媽祖”蒞台 400週年慶，新港奉天宮授權精品設計團隊，設計以新港奉天宮廟門造型之文創信物，作品以新港奉天宮廟門、媽祖與虎爺大二將軍作為設計元素。
有新港奉天宮正門之作品，收藏安置皆有置身新港奉天宮現場之氛圍。作品含『媽祖+虎爺錢母』各一枚，保庇平安的同時，也讓您財源滾滾來。
整體配色採金色，大氣外形，更象徵著開運、招財、金好額。
每件作品皆再新港奉天宮正殿過爐完成。安放明亮位置，增加正能量讓新港媽祖虎爺大二將軍天天給您賜福招財，
新港媽祖虎爺神威顯赫
保佑我們，繼承新生的氣運。" /><!--簡介-->
    <meta property="og:site_name" content="新港奉天宮鎮宅錢母擺件" /><!--標題-->
    <meta property="og:type" content="website" />
    <title>新港奉天宮鎮宅錢母擺件</title><!--標題-->
    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
	
    <link rel="shortcut icon" href="https://bobibobi.tw/Product/images/favicon.png" />
    <link href="https://bobibobi.tw/Product/images/favicon.png" rel="apple-touch-icon" sizes="192x192" />
    
    <!--預覽影片-->
    <meta property="og:video" content="https://www.youtube.com/embed/8tdGf8JjhgY" />
    <meta property="og:video:type" content="text/html" />
    <meta property="og:video:width" content="640" />
    <meta property="og:video:height" content="360" />

    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="新港奉天宮｜2026 開運錢母擺件發售中" />
    <meta name="twitter:description" content="開臺媽祖X金虎爺聯名！2026最強開運錢母擺件限量開賣，附介紹影片說明與購買連結。" />
    <meta name="twitter:image" content="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />

    <!--預設載入css-->
    <link href="https://bobibobi.tw/Product/css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="https://bobibobi.tw/Product/css/style.css" />
    <link href="https://bobibobi.tw/Product/css/jquery-ui.min.css" rel="stylesheet" />
    <script src="https://bobibobi.tw/Product/js/jquery-2.1.4.min.js"></script>
    <script src="https://bobibobi.tw/Product/js/jquery.ui.min.js"></script>
    <style type="text/css">

        .ProductLeft {
            margin-right: 18%;
        }
        .web {
            display: block;
        }
        .PayButton ul li.mobile {
            display: none;
        }
        
        .ytvideo {
            max-width: 100%;
            width: 100%;
            height: 550px;
            margin-bottom: -5px;
        }
        
        /* 容器：預設為橫向排列 */
        .invoice-types {
          display: flex;
          flex-wrap: wrap;
          gap: 3vw;                /* 選項之間的水平/垂直間距 */
          margin: 1vw 0;             /* 上下 margin */
        }


        /* radio 與文字的間距 */
        .invoice-types input[type="radio"] {
          margin-right: 0.5rem;
        }

        .invoice-types label {
            font-size: 1.2vw;
        }

        .OrderinvoiceType, .OrderCarrierCode, .OrderinvCode, .OrderinvName, .OrderEmail{
            font-size: 1.2vw;
            line-height: calc(2vw + 12px);
            text-align: left;
        }

        /*手機板*/
        @media only screen and (max-width: 720px) {
            .PayButton ul li {
                margin: 0 auto;
                padding: 1.5vw 0;
                width: 88% !important;
                display: block;
                text-align: center;
            }

                .PayButton ul li input {
                    margin: 0 auto;
                    padding: 1.5vw 0;
                    width: 88% !important;
                }

                .PayButton ul li:last-child {
                    margin-right: auto;
                }
            .web {
                display: none;
            }

            .PayButton ul li.mobile {
                display: block;
            }

            .invoice-types {
                flex-direction: column;
                gap: 1vw;
            }
            .invoice-types label {
                font-size: 7vw;
            }
            .shopmemberForm label {
                font-size: 4.5vw;
                width: 25vw;
                line-height: calc(5vw + 12px);
            }
            
            .OrderinvoiceType, .OrderCarrierCode, .OrderinvCode, .OrderinvName, .OrderEmail, .OrderinvoiceNum{
                font-size: 4.5vw;
            }
        
        }
    </style>

    <style type="text/css">
        .GiftW .GiftImgList li {
            width: 48%;
        }
    </style>
    <script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "VideoObject",
      "name": "新港奉天宮開運錢母擺件",
      "description": "2026最強開運錢母擺件\n新港奉天宮開臺媽祖X金虎爺\n讓您新的一年財運大爆發\nhttps://st.bobibobi.tw/6sh4w6",
      "thumbnailUrl": "https://img.youtube.com/vi/8tdGf8JjhgY/maxresdefault.jpg",
      "uploadDate": "2026-01-03T00:00:00+08:00",
      "embedUrl": "https://www.youtube.com/embed/8tdGf8JjhgY",
      "contentUrl": "https://bobibobi.tw/Product/MoneymotherIndex.aspx"
    }
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
    <uc3:PopupMessageDialogControl ID="PopupMessageDialogControl1" runat="server" />
    <uc2:AjaxClientControl ID="AjaxClientControl1" runat="server" />
    <div id="wrap"><!--#warp //start-->
    
        <div id="BgFixed"></div>
    
        <header><!--#頁首 //start-->
        
            <div id="Menu">
                <ul class="MenuList">
                    <li><a href="https://www.facebook.com/profile.php?id=100086934594859" title="粉絲專頁" target="_blank"><div>粉絲專頁</div></a></li>
                    <li><a href="https://bobibobi.tw/SearchLog.aspx" title="訂單查詢" target="_blank"><div><img src="https://bobibobi.tw/Product/images/icon_member.png" width="19" height="25" alt=""/></div><div>訂單查詢</div></a></li>
                </ul>
            </div>
        </header><!--#頁首 //end-->    
    
        <article><!--#內容區塊 //start-->
        
            <div class="decoCircle">
                <div class="deco_Circle_A"><div class="deco_Circle_Type1"></div></div>
                <div class="deco_Circle_B"><div class="deco_Circle_Type1"></div></div>
                <div class="deco_Circle_C"><div class="deco_Circle_Type2"></div></div>
            </div>
        
            <section id="Banner"><!--第一區塊 //start-->
                <div class="deco_lamp">
                    <div class="deco_lamp_L"></div>
                    <div class="deco_lamp_R"></div>
                </div>
                <div class="WebTitleBk">
                
                
                    <h1 class="WebTitle"><img src="images/webTitle.png?t=9865235896" width="807" height="243" alt="求平安，開好運"/></h1>
                </div>
            </section><!--第一區塊 //end-->
        
            <section id="EventInfo"><!--第二區塊 //start-->
                <div class="EventContent">
                    <h3>【奉天承運 開運擺件】</h3>
                    <div class="content TxtW nextpart">自古廟宇是神佛與人溝通的據點，人們心靈寄託之處，古代富貴人家常在家裏明顯處，掛上一幅寺廟建築相關圖畫，代表主人家精神提升與智慧展現。 欣逢 “新港媽祖”蒞台 400週年慶，新港奉天宮授權精品設計團隊，設計以新港奉天宮廟門造型之文創信物，作品以新港奉天宮廟門、媽祖與虎爺大二將軍作為設計元素。有新港奉天宮正門之作品，收藏安置皆有置身新港奉天宮現場之氛圍。</div>
                
                    <h3>【產品細節】</h3>
                    <div class="content TxtW">作品含『媽祖+虎爺錢母』各一枚，保庇平安的同時，也讓您財源滾滾來。 整體配色採金色，大氣外形，更象徵著開運、招財、金好額。 每件作品皆再新港奉天宮正殿過爐完成。安放明亮位置，增加正能量讓新港媽祖虎爺大二將軍天天給您賜福招財， 新港媽祖虎爺神威顯赫 保佑我們，繼承新生的氣運。</div>
                </div>
            </section><!--第二區塊 //end-->
        
            <section id="Video"><!--影片區塊 //start-->
                <div class="">
                    <div class="">
                        <iframe class="ytvideo" src="https://www.youtube.com/embed/8tdGf8JjhgY?si=IIrTlhH-xeTN6MTt" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                    </div>
                    <div class="">
                        <iframe class="ytvideo" src="https://www.youtube.com/embed/I6LZOzixkgY" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                    </div>
                </div>
            </section><!--影片區塊 //end-->
        
            <section id="Gift"><!--第三區塊 //start-->
            
                <div class="deco_gift" style="top: 0;">
                    <div class="deco_gift_lamp_L"></div>
                    <div class="deco_gift_lamp_R"></div>
                    <div class="deco_gift_cloud_L"></div>
                    <div class="deco_gift_cloud_R_1"></div>
                    <div class="deco_gift_cloud_R_2"></div>
                </div>
            
            
            
                <h2>鎮宅擺件祈平安<br />錢母加持助開運</h2>
                <div class="GiftTitle">
                    <span>平安就是幸福</span><span>幸福來自感恩</span><span>感恩媽祖照顧</span><span>請祢繼續護佑</span>
                </div>
                <div class="GiftInfo content TxtB Spart"><%--2026乙巳蛇年迎新春，小龍賜福，錢母加持～讓您好運旺旺來！虎爺香火袋把平安帶著走！--%></div>
            
                <div class="GiftContent GiftW"><!--電腦版-->
                    <ul class="GiftImgList">
                        <li><img src="https://bobibobi.tw/Product/images/sample/img02.png" width="500" height="400" alt="壓轎金贈送"/></li>
                        <li><img src="https://bobibobi.tw/Product/images/sample/img03.png" width="500" height="400" alt="六家宮廟祈福過香火"/></li>
                    </ul>
                    <div class="GiftTxt">
                        <ul>
                            <li>
                                <h5>鎮宅錢母擺件</h5>
                                <div class="content TxtB">獨家開賣，新港奉天宮授權鎮宅錢母擺件。</div>
                            </li>
                            <li>
                                <h5>壓轎金贈送</h5>
                                <div class="content TxtB">只要購買<span style="color: #9A0F1B;">鎮宅、開運錢母擺件</span>，將會贈送一份壓轎金，與商品一起寄出，此為大甲媽祖繞境時鑾轎駐駕於新港奉天宮時的壓轎金。</div>
                            </li>
                            <li>
                                <h5>祈福過香火</h5>
                                <div class="content TxtB">鎮宅錢母擺件皆有在新港奉天宮祈福過香火，有媽祖的庇佑與加持。</div>
                            </li>
                        </ul>
                    </div>
                
                </div>
            
                <div class="GiftContent GiftM"><!--手機版-->
                    <ul>
                        <li>
                            <img src="https://bobibobi.tw/Product/images/sample/sample_1.jpg" width="500" height="400" alt="鎮宅錢母擺件"/>
                            <h5>鎮宅錢母擺件</h5>
                            <div class="content TxtB">獨家開賣，新港奉天宮授權鎮宅錢母擺件。</div>
                        </li>
                        <li>
                            <img src="https://bobibobi.tw/Product/images/sample/sample_2.jpg" width="500" height="400" alt="壓轎金贈送"/>
                            <h5>壓轎金贈送</h5>
                            <div class="content TxtB">只要購買<span style="color: #9A0F1B;">鎮宅、開運錢母擺件</span>，將會贈送一份壓轎金，與商品一起寄出。</div>
                        </li>
                        <li>
                            <img src="https://bobibobi.tw/Product/images/sample/sample_3.jpg" width="500" height="400" alt="祈福過香火"/>
                            <h5>祈福過香火</h5>
                            <div class="content TxtB">鎮宅錢母擺件皆有在新港奉天宮祈福過香火，有媽祖的庇佑與加持。</div>
                        </li>
                    </ul>
                </div>
            
                <div class="GiftDecoBg"></div>
                <div class="deco_gift" style="bottom: 0;">
                    <div class="deco_gift_btm_L"></div>
                    <div class="deco_gift_btm_R"></div>
                </div>
            
            </section><!--第三區塊 //end-->
        
            <section id="Products"><!--第四區塊 //start-->
                <h3>商品介紹</h3>
                <div class="ProductsList">
                    <div class="Products">
                        <ul class="ProductsImg">
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_1.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_1.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_11.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_11.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_7.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_7.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_4.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_4.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_5.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_5.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                            <li><a href="https://bobibobi.tw/Product/images/products/products_A_6.jpg" data-fancybox><img src="https://bobibobi.tw/Product/images/products/products_A_6.jpg" width="1200" height="1200" alt="鎮宅、開運錢母擺件" title="鎮宅、開運錢母擺件"/></a></li>
                        </ul>
                        <h5>鎮宅、開運錢母擺件 <span id="type_1" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsPrice">原價：2480 / 活動價：<span>1480</span></div>
                        <div class="ProductsContent content TxtW">新港奉天宮獨家授權富御琉金製作的錢母擺件產品，擺放家中財位可鎮宅、招財、避邪、保平安，數量有限、售完為止。商品尺寸：長18cm x 寬6cm x 高12cm。
                        <a id="Description" class="ProductsContent content TxtW" style="text-decoration:underline">詳細說明</a>
                        </div>
                        <!--TO工程師：↓↓↓影片封面照片及Youtube連結請更新↓↓↓
                        <div class="ProductsVideo">
                            <a href="https://www.youtube.com/watch?v=6DhryEFFKa8" data-fancybox title="鎮宅、開運錢母擺件" style="background: url('https://bobibobi.tw/Product/images/products/products_A_video.jpg');">
                                <img src="https://bobibobi.tw/Product/images/videoPlay.png" width="101" height="117" alt=""/>
                            </a>
                        </div>-->
                    </div>
                                 
                    <%--<div class="Products">
                        <ul class="ProductsImg">
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_D_1.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_D_1.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_D_2.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_D_2.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                        </ul>
                        <h5>招財大嘴貓(白色) <span id="type_5" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsPrice">原價：799 / 活動價：<span>399</span></div>
                        <div class="ProductsContent content TxtW">大嘴招財貓, 能吃就是福. 笑口常開好運來。風水擺設.收銀櫃檯.家廳玄關迎賓納福。尺寸為高：24cm、長：15cm、寬：10cm。
                        </div>
                        <!--TO工程師：↓↓↓影片封面照片及Youtube連結請更新↓↓↓
                        <div class="ProductsVideo">
                            <a href="https://www.youtube.com/watch?v=6DhryEFFKa8" data-fancybox title="鎮宅、開運錢母擺件" style="background: url('https://bobibobi.tw/Product/images/products/products_A_video.jpg');">
                                <img src="https://bobibobi.tw/Product/images/videoPlay.png" width="101" height="117" alt=""/>
                            </a>
                        </div>-->
                    </div>
                    
                    <div class="Products ProductLeft">
                        <ul class="ProductsImg">
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_E_1.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_E_1.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_E_2.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_E_2.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                        </ul>
                        <h5>招財大嘴貓(藍色) <span id="type_6" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsPrice">原價：799 / 活動價：<span>399</span></div>
                        <div class="ProductsContent content TxtW">大嘴招財貓, 能吃就是福. 笑口常開好運來。風水擺設.收銀櫃檯.家廳玄關迎賓納福。尺寸為高：30cm、長：20cm、寬：19cm。
                        </div>
                        <!--TO工程師：↓↓↓影片封面照片及Youtube連結請更新↓↓↓
                        <div class="ProductsVideo">
                            <a href="https://www.youtube.com/watch?v=6DhryEFFKa8" data-fancybox title="鎮宅、開運錢母擺件" style="background: url('https://bobibobi.tw/Product/images/products/products_A_video.jpg');">
                                <img src="https://bobibobi.tw/Product/images/videoPlay.png" width="101" height="117" alt=""/>
                            </a>
                        </div>-->
                    </div>
                    
                    <div class="Products bobi">
                        <ul class="ProductsImg">
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_F_1.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_F_1.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_F_2.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_F_2.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                        </ul>
                        <h5>招財大嘴貓(粉色) <span id="type_7" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsPrice">原價：799 / 活動價：<span>399</span></div>
                        <div class="ProductsContent content TxtW">大嘴招財貓, 能吃就是福. 笑口常開好運來。風水擺設.收銀櫃檯.家廳玄關迎賓納福。尺寸為高：24cm、長：15cm、寬：10cm。
                        </div>
                        <!--TO工程師：↓↓↓影片封面照片及Youtube連結請更新↓↓↓
                        <div class="ProductsVideo">
                            <a href="https://www.youtube.com/watch?v=6DhryEFFKa8" data-fancybox title="鎮宅、開運錢母擺件" style="background: url('https://bobibobi.tw/Product/images/products/products_A_video.jpg');">
                                <img src="https://bobibobi.tw/Product/images/videoPlay.png" width="101" height="117" alt=""/>
                            </a>
                        </div>-->
                    </div>
                    
                    <div class="Products bobi">
                        <ul class="ProductsImg">
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_G_1.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_G_1.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_G_2.png" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_G_2.png" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                        </ul>
                        <h5>招財大嘴貓(橘色) <span id="type_8" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsPrice">原價：799 / 活動價：<span>399</span></div>
                        <div class="ProductsContent content TxtW">大嘴招財貓, 能吃就是福. 笑口常開好運來。風水擺設.收銀櫃檯.家廳玄關迎賓納福。尺寸為高：24cm、長：15cm、寬：10cm。
                        </div>
                        <!--TO工程師：↓↓↓影片封面照片及Youtube連結請更新↓↓↓
                        <div class="ProductsVideo">
                            <a href="https://www.youtube.com/watch?v=6DhryEFFKa8" data-fancybox title="鎮宅、開運錢母擺件" style="background: url('https://bobibobi.tw/Product/images/products/products_A_video.jpg');">
                                <img src="https://bobibobi.tw/Product/images/videoPlay.png" width="101" height="117" alt=""/>
                            </a>
                        </div>-->
                    </div>--%>
                    
                    <div class="Products bobi">
                        <ul class="ProductsImg">
                            <li>
                                <a href="https://bobibobi.tw/Product/images/products/products_H_1.jpg" data-fancybox>
                                    <img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/>
                                </a>
                            </li>
                        </ul>
                        <h5>午時水/1罐 $168 + 運費$60 <br />
                            午時水/3罐 $388 + 運費$90 <br />
                            午時水/6罐 $688 + 免運費 <br />
                            <span id="type_9" runat="server" style="color: red;">( 已售完 )</span></h5>
                        <div class="ProductsContent content TxtW">一年一度開運之水，只在端午中午誕生！
                                                                  我們特別選在陽氣最盛的端午正午，曬製午時水，
                                                                  更邀請神霄玉府的法師和道長親臨加持，將祈福能量注入瓶中，為你轉運開運、避邪祈安！
                                                                  每瓶皆為獨立祈福封存非飲用品｜僅供灑淨、祈福、招財使用
                                                                  數量有限，錯過只能等明年！
                        <a id="Description2" class="ProductsContent content TxtW" style="text-decoration:underline">詳細說明</a>
                        </div>
                    </div>
                    
                </div>
            
            </section><!--第四區塊 //end-->
        
            <section id="Shopcart"><!--第六區塊 //start-->
                <h3>訂購單</h3>
            
                <div class="shopproducts">
                    <div id="productItem_A">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_A_1.jpg" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>鎮宅、開運錢母擺件 <span id="product_1" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>1480</div>
                            </div>
                            <div class="ItemInfo" id="productCount_1" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_1" type="button" class="CountBtn" id="ItemLess_1" value="-" />
                                    <input name="Item_1" type="number" id="Item_1" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_1" type="button" class="CountBtn" id="ItemPlus_1" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div id="productItem_D">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_D_1.png" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>招財大嘴貓(白色) <span id="product_5" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>399</div>
                            </div>
                            <div class="ItemInfo" id="productCount_5" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_4" type="button" class="CountBtn" id="ItemLess_4" value="-" />
                                    <input name="Item_4" type="number" id="Item_4" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_4" type="button" class="CountBtn" id="ItemPlus_4" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="productItem_E">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_E_1.png" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>招財大嘴貓(藍色) <span id="product_6" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>399</div>
                            </div>
                            <div class="ItemInfo" id="productCount_6" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_5" type="button" class="CountBtn" id="ItemLess_5" value="-" />
                                    <input name="Item_5" type="number" id="Item_5" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_5" type="button" class="CountBtn" id="ItemPlus_5" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="productItem_F" class="bobi">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_F_1.png" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>招財大嘴貓(粉色) <span id="product_7" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>399</div>
                            </div>
                            <div class="ItemInfo" id="productCount_7" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_6" type="button" class="CountBtn" id="ItemLess_6" value="-" />
                                    <input name="Item_6" type="number" id="Item_6" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_6" type="button" class="CountBtn" id="ItemPlus_6" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="productItem_G" class="bobi">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_G_1.png" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>招財大嘴貓(橘色) <span id="product_8" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>399</div>
                            </div>
                            <div class="ItemInfo" id="productCount_8" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_7" type="button" class="CountBtn" id="ItemLess_7" value="-" />
                                    <input name="Item_7" type="number" id="Item_7" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_7" type="button" class="CountBtn" id="ItemPlus_7" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div id="productItem_H" class="bobi">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>午時水/罐 $168 + 運費$60 <span id="product_9" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>228</div>
                            </div>
                            <div class="ItemInfo" id="productCount_9" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_8" type="button" class="CountBtn" id="ItemLess_8" value="-" />
                                    <input name="Item_8" type="number" id="Item_8" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_8" type="button" class="CountBtn" id="ItemPlus_8" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="productItem_I" class="bobi">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>午時水/3罐 $388 + 運費$90 <span id="product_10" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>478</div>
                            </div>
                            <div class="ItemInfo" id="productCount_10" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_9" type="button" class="CountBtn" id="ItemLess_9" value="-" />
                                    <input name="Item_9" type="number" id="Item_9" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_9" type="button" class="CountBtn" id="ItemPlus_9" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="productItem_J" class="bobi">
                        <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                        <div class="shopProductsInfo">
                            <h5>午時水/6罐 $688 + 免運費 <span id="product_11" runat="server" style="color: red;">( 已售完 )</span></h5>
                            <div class="ItemInfo">
                                <label>金額</label>
                                <div>688</div>
                            </div>
                            <div class="ItemInfo" id="productCount_11" runat="server">
                                <label>數量</label>
                                <div>
                                    <input name="ItemLess_10" type="button" class="CountBtn" id="ItemLess_10" value="-" />
                                    <input name="Item_10" type="number" id="Item_10" min="0" value="0" readonly="readonly" class="ItemCount" />
                                    <input name="ItemPlus_10" type="button" class="CountBtn" id="ItemPlus_10" value="+" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            
                <div class="shopmemberInfo">
                    <div class="shopmemberInfoTitle">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                    <div class="shopmemberForm">
                        <div><label>購買人</label><div><input name="Name" type="text" id="Name" placeholder="請輸入購買人姓名" /></div></div>
                        <div><label>聯絡電話</label><div><input name="Tel" type="text" id="Tel" placeholder="請輸入購買人電話" /></div></div>
                        <div><label>電子信箱</label><div><input name="Email" type="text" id="Email" placeholder="請輸入購買人信箱" /></div></div>
                        <div><label>收件地址</label><div><div class="twzipcode"></div><div><input name="Add" type="text" id="Add" placeholder="請輸入收件地址" /></div></div></div>
                    </div>
                    
                    <!--↓↓發票資料↓↓-->
                    <div class="shopmemberInfoTitle">發票資料</div>
                
                    <!-- 發票類型選擇 -->
                    <div class="invoice-types">
                        <label class="label">
                            <input type="radio" name="InvoiceType" class="checkedbox" value="1" checked="checked" />
                            一般電子發票
                        </label>
                        <label class="label">
                            <input type="radio" name="InvoiceType" class="checkedbox" value="2" />
                            手機載具發票
                        </label>
                        <%--<label>
                        <input type="radio" name="InvoiceType" class="checkedbox" value="3" />
                        發票捐贈
                      </label>--%>
                        <label class="label">
                            <input type="radio" name="InvoiceType" class="checkedbox" value="4" />
                            公司發票
                        </label>

                        <!-- 載具代碼輸入 + 挑選常用按鈕 -->
                        <div id="InvoiceNum" class="shopmemberForm">
                            <div>
                                <div><label>統一編號 </label>
                                    <input id="InvoiceCode" type="text"
                                    placeholder="請輸入統一編號" style="margin-bottom: 10px;" /><br />
                                </div>
                                <div>
                                    <label>公司名稱 </label>
                                    <input id="InvoiceName" type="text"
                                        placeholder="請輸入公司名稱"  />
                                </div>
                                <br />
                            </div>
                        </div>
                        <div id="MobileCarrier" class="shopmemberForm" style="width: 100%;">
                            <div>
                                <div>
                                    <label>手機載具</label>
                                    <input id="CarrierCode" type="text"
                                        placeholder="請輸入手機載具 ex:/TRM+O+P" />
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <!-- 載具選擇（僅手機條碼／會員／自然人） -->
                    <%--<div class="carrier-types" style="margin: 1vw 0;">
                        <label>
                            <input type="radio" name="CarrierOption" value="member" />
                            會員載具</label>
                        <label>
                            <input type="radio" name="CarrierOption" value="mobile" />
                            手機條碼載具</label>
                        <label>
                            <input type="radio" name="CarrierOption" value="citizen" />
                            自然人憑證載具</label>
                    </div>--%>
                    
                    <!--↑↑發票資料↑↑-->
                    <br />
                    <div><input id="CheckOrder" type="button" value="檢查資料及付款" /></div>
                    
                </div>
                <br />
                <%--<div class="shopmemberInfo">
                    <p style="color: red;">因應農曆春節連續假期及貨運公司配送問題，將於2026/1/21至2026/02/02暫停出貨作業。</p>
                    <br />
                    <p style="color: red;">2026/1/20至2026/02/02成立的訂單，將於2026/02/03起陸續出貨。 祝大家小龍年行大運!</p>
                </div>--%>
            </section><!--第六區塊 //end-->
        
        </article><!--#內容區塊 //end-->
    
    
        <footer><!--#頁尾 //start-->
            <div class="FooterLogo" style="display: none;">
                <ul>
                    <li style="width: 32%; margin-right: 13px;"><img src="https://bobibobi.tw/Product/images/logo_fet.png" style="height:56px; margin-top: -15px;" /></li>
                    <li style="width: 32%; margin-right: 0;"><img src="https://bobibobi.tw/Product/images/foot_logo_03.png" style="height:30px; margin-top: -15px;" /></li>
                    <li style="width: 32%; margin-right: 0;"><img src="https://bobibobi.tw/Product/images/foot_logo_04.png" style="margin-top: -15px;" /></li>
                    <li><img src="https://bobibobi.tw/Product/images/footerLogo.png" style="height:56px" alt=""/></li>
                </ul>
            </div>
            <div class="footerContact footerTxt">
                    <div><a href="https://bobibobi.tw/Temples/ShoppingGuide.aspx">購物說明</a></div>
                    <div><a href="https://bobibobi.tw/Temples/PrivacyPolicy.aspx">隱私權政策</a></div>
            </div>
            <br />
            <div class="footerinfo">九九商通科技有限公司</div>
            <div class="footerContact footerTxt">
                <div><label>服務專線</label><a href="tel:0436092299">04-3609-2299</a></div>
                <div><label>服務信箱</label><a href="mailto:service@appssp.com">service@appssp.com</a></div>
            </div>
            <div class="serviceTime footerTxt"><label>服務時間</label><span>週一至週五10:00-17:00</span></div>
            <div class="copyright footerTxt">Copyright©2022-2025 九九商通科技有限公司 <span>All rights reserved.</span></div>
        </footer><!--#頁尾 //end-->
    
    </div><!--#warp //end-->
    
    <div id="alertBox" class="AlertPupBox" style="display: none;">
        <div class="PupContent">
            <div class="AlertTitle">請先完成您的訂購單</div>
            <div id="AlertContent">
            
            </div>
            <div class="CloseBtn"><input type="button" value="關閉" /></div>
        </div>    
    </div>    
    
    <div id="alertBox_End" class="AlertPupBox" style="display: none;">
        <div class="PupContent">
            <div class="AlertTitle">活動已截止</div>
            <div id="AlertContent_End">
                目前開運商品皆已販售完畢，感謝您的支持！
            </div>
            <div class="CloseBtn"><input type="button" value="關閉" /></div>
        </div>    
    </div>    
    
    <!--訂單確認-->
    <div id="checkOrder" class="OrderPupBox" style="display: none;">
        <div class="OrderTitle">訂單確認與付款</div>
    
        <div class="OrderContent">
            <div class="shopproducts">
                <div class="OrderproductItem OrderItemA">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_A_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>鎮宅、開運錢母擺件</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>1480</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemAcount">5</span>個</div>
                        </div>
                        <div class="ItemInfo">
                            <ul id="OrderItemTag">
                                <!--TO工程師：↓↓↓以下範例可以刪除↓↓↓
                                <li>屬名雕刻服務-編號<span>1</span>：<span>無</span></li>-->
                            </ul>
                        </div>
                    </div>
                </div>
                <%--<div class="OrderproductItem">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_C_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>2024新港奉天宮黃金符令手鍊</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>2980</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemCcount">2</span>個</div>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="OrderproductItem">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_B_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>開運隨身御守</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>499</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemBcount">2</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemD">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_D_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(白色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemDcount">0</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemE">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_E_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(藍色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemEcount">0</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemF">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_F_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(粉色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemFcount">0</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemG">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_G_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(橘色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemGcount">0</span>個</div>
                        </div>
                    </div>
                </div>--%>
                <div class="OrderproductItem OrderItemH">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>午時水/罐 $168 + 運費$60</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>228</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemHcount">0</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemI">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>午時水/3罐 $388 + 運費$90</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>478</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemIcount">0</span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem OrderItemJ">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_H_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>午時水/6罐 $688 + 免運費</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>688</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemJcount">0</span>個</div>
                        </div>
                    </div>
                </div>
            </div>
        
            <div class="shopmemberInfo">
                <div class="shopmemberInfoTitle">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                <div class="shopmemberForm">
                    <div><label>購買人</label><div class="OrderName"></div></div>
                    <div><label>聯絡電話</label><div class="OrderTel"></div></div>
                    <div><label>電子信箱</label><div class="OrderEmail"></div></div>
                    <div><label>收件地址</label><div class="OrderAdd"></div></div>
                    <div><label>發票類型</label><div class="OrderinvoiceType"></div></div>
                    <div id="orderCarrierCode"><label>手機載具</label><div class="OrderCarrierCode"></div></div>
                    <div id="orderCompany">
                        <label>統一編號</label><div class="OrderinvCode"></div>
                        <label>公司名稱</label><div class="OrderinvName"></div>
                    </div>
                </div>

                <div class="OrderSum">購買總金額：<span></span>元</div>
            </div>
            <div class="PayButton">
                <ul>
                    <%--<li class="mobile" style="color: red;">因應農曆春節連續假期及貨運公司配送問題，將於2026/1/21至2026/02/02暫停出貨作業。</li>
                    <li class="mobile" style="color: red;">2026/1/20至2026/02/02成立的訂單，將於2026/02/03起陸續出貨。 祝大家小龍年行大運!</li>--%>
                    <li><input name="EditOrder" type="button" id="EditOrder" value="修改" /></li>
                    <li><input name="mobile_pay" type="button" id="mobile_pay" value="遠傳門號付款" /></li>
                    <li><input name="card_pay" type="button" id="card_pay" value="信用卡付款" /></li>
                    <li id="LINEPay" runat="server">
                        <input name="Line_pay" type="button" id="Line_pay" runat="server" value="LINE PAY" /></li>
                    <li id="JkosPay" runat="server">
                        <input name="Jkos_pay" type="button" id="Jkos_pay" runat="server" value="街口支付" /></li>
                    <%--<li><input name="mobile_cht_pay" type="button" id="mobile_cht_pay" value="中華門號付款" /></li>
                    <li><input name="mobile_twm_pay" type="button" id="mobile_twm_pay" value="台哥大門號付款" /></li>--%>
                    <%--<li><input name="Google_pay" type="image" id="Google_pay" src="https://bobibobi.tw/Product/images/Google_Pay.png" /></li>--%>
                </ul>
            </div>
               <%--     <br />
                <div class="web">
                    <p style="color: red;">因應農曆春節連續假期及貨運公司配送問題，將於2026/1/21至2026/02/02暫停出貨作業。</p>
                    <br />
                    <p style="color: red;">2026/1/20至2026/02/02成立的訂單，將於2026/02/03起陸續出貨。 祝大家小龍年行大運!</p>
                </div>--%>
        </div>
    
    </div>   
    
        <div id="dialog">
            <div class="DescriptionList" style="white-space: pre-wrap;">
                                      【奉天承運 開運擺件】
自古廟宇是神佛與人溝通的據點，人們心靈寄託之處，古代富貴人家常在家裏明顯處，掛上一幅寺廟建築相關圖畫，代表主人家精神提升與智慧展現。
欣逢 “新港媽祖”蒞台 400週年慶，新港奉天宮授權精品設計團隊，設計以新港奉天宮廟門造型之文創信物，作品以新港奉天宮廟門、媽祖與虎爺大二將軍作為設計元素。
有新港奉天宮正門之作品，收藏安置皆有置身新港奉天宮現場之氛圍。作品含『媽祖+虎爺錢母』各一枚，保庇平安的同時，也讓您財源滾滾來。
整體配色採金色，大氣外形，更象徵著開運、招財、金好額。
每件作品皆再新港奉天宮正殿過爐完成。安放明亮位置，增加正能量讓新港媽祖虎爺大二將軍天天給您賜福招財，
新港媽祖虎爺神威顯赫
保佑我們，繼承新生的氣運。
            </div>
        </div>
    
        <div id="dialog2">
            <div class="DescriptionList" style="white-space: pre-wrap;">
             各位敲碗的午時水來囉，在端午極陽之日我們請到了神霄玉府的法師和道長來為我們做科儀加持午時水。


🔆《午時水》用途與功效
🌞 驅邪避煞：取自陽氣最旺的午時，具純陽之氣，驅除陰煞與不祥氣場。

🧘‍♂️ 安神鎮心：用於洗臉、擦身，有助於穩定情緒、安定心神。

🛁 淨身除穢：可加入沐浴或泡腳水中，淨化身心、祛除病氣與疲憊。

🏠 淨宅轉運：灑於居家四隅或門檻，可清除煞氣、提升正能量。

👶 兒童避驚：小兒體弱易驚，可用午時水擦拭額頭或泡澡以安魂定魄。

💰 招財開運：午時水加鹽或五色豆放財位，有助驅走晦氣、招來財氣。



🔮 午時水的招財實用法
1. 🌾 五行開運法
材料：午時水 + 鹽巴（一撮）+ 五色豆（紅、黑、綠、黃、白）

做法：裝入透明瓶罐，放置於財位（如玄關左側、客廳對角、收銀台等）

效果：提升流年財氣、穩定財運、防漏財

2. 🧧 財神爺灑淨法
以午時水加入少量鹽，輕輕灑於財神爺或貴人方位周邊，祈求招財納福。

3. 🪞 鏡前招財法
每日清晨用午時水擦拭辦公桌、鏡子、手機螢幕等，象徵「財氣通達、事業順利」。

4. 🧴 午時水淨手法
每天外出做生意前，雙手用午時水清洗，象徵洗去晦氣、開啟財門。

5. 🔖 搭配咒語或祈願語
撒水或使用時可默念：

「午時純陽，財源廣進，吉氣臨門，萬事興隆」
也可配合個人信仰唸佛號、持咒等。

            </div>
        </div>
</body>
</html>


<script>
    var dialogwidth;
    if (document.body.scrollWidth < 767) {
        dialogwidth = document.body.scrollWidth;
    }
    else {
        dialogwidth = 480;
    }

    var yourPosition = {
        my: "center top",
        at: "center top+50"
    };


    function setURL(c, v, t) {
        if (location.search.indexOf(c) >= 0) {
            //alert($("title").html());
            $(document).attr("title", "(" + t + ")" + $("title").html());
            $.each($("a"), function (i, n) {
                var $href = $(this).attr("href");
                if (typeof ($href) != "undefined") {
                    if ($href.indexOf('.aspx') > 0 && $href.indexOf(c) < 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&" + c + "=" + v);
                        }
                        else {
                            $(this).attr("href", $href + "?" + c + "=" + v);
                        }
                    }
                }
            });
        }
    }

    $(document).ready(function () {
        if (location.search.indexOf('ftg') >= 0) {
            $(".bobi").hide();
        }
        else {
            $(".bobi").show();
        }

        setURL("ftg", "2290", "奉天宮");
        setURL("twm", "2290", "TWM");
        setURL("line", "2290", "LINE");
        setURL("jkos", "2290", "街口");
        setURL("ig", "2290", "IG");
        setURL("cht", "2290", "CHT");
        setURL("fetsms", "2290", "fetSMS");
        setURL("elv", "2290", "ELV");
        setURL("gads", "2290", "GADS");
        setURL("tads", "2290", "TADS");

        // 快取區塊
        var $numSection = $('#InvoiceNum'),
            $mobileSection = $('#MobileCarrier'),
            $radios = $('input[name="InvoiceType"]');

        // 根據選擇顯示／隱藏
        function updateInvoiceFields() {
            var sel = $radios.filter(':checked').val();
            if (sel === '1') {
                $numSection.hide();
                $mobileSection.hide();
            }
            else if (sel === '2') {
                $numSection.hide();
                $mobileSection.show();
            }
            else if (sel === '4') {
                $numSection.show();
                $mobileSection.hide();
            }
        }

        // 綁定 change 事件
        $radios.on('change', updateInvoiceFields);

        // 初始執行一次
        updateInvoiceFields();
        //if (location.search.indexOf('twm') >= 0) {
        //    //alert($("title").html());
        //    $(document).attr("title", "(TWM)" + $("title").html());
        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0 && $href.indexOf('twm') < 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&twm=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?twm=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('line') >= 0) {
        //    $(document).attr("title", "(LINE)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&line=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?line=1");
        //            }
        //        }
        //    });
        //}

        if (location.search.indexOf('fb') >= 0) {


            if (location.search.indexOf('fbad') >= 0) {
                setURL("fbad", "2290", "FB廣告");
                //$(document).attr("title", "(FB廣告)" + $("title").html());
                ////$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                //$.each($("a"), function (i, n) {
                //    var $href = $(this).attr("href");
                //    if ($href.indexOf('.aspx') > 0) {
                //        if ($href.indexOf('?') > 0) {
                //            $(this).attr("href", $href + "&fbad=1");
                //        }
                //        else {
                //            $(this).attr("href", $href + "?fbad=1");
                //        }
                //    }
                //});
            }
            else if (location.search.indexOf('fbda') >= 0) {
                setURL("fbda", "2290", "FBDA");
                //$(document).attr("title", "(FBDA)" + $("title").html());
                ////$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                //$.each($("a"), function (i, n) {
                //    var $href = $(this).attr("href");
                //    if ($href.indexOf('.aspx') > 0) {
                //        if ($href.indexOf('?') > 0) {
                //            $(this).attr("href", $href + "&fbda=1");
                //        }
                //        else {
                //            $(this).attr("href", $href + "?fbda=1");
                //        }
                //    }
                //});
            }
            else {
                setURL("fb", "2290", "FB");
                //$(document).attr("title", "(FB)" + $("title").html());
                ////$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                //$.each($("a"), function (i, n) {
                //    var $href = $(this).attr("href");
                //    if ($href.indexOf('.aspx') > 0) {
                //        if ($href.indexOf('?') > 0) {
                //            $(this).attr("href", $href + "&fb=1");
                //        }
                //        else {
                //            $(this).attr("href", $href + "?fb=1");
                //        }
                //    }
                //});
            }
        }

        //if (location.search.indexOf('ig') >= 0) {
        //    $(document).attr("title", "(IG)" + $("title").html());
        //    //$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&ig=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?ig=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('cht') >= 0) {
        //    $(document).attr("title", "(CHT)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&cht=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?cht=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('fetsms') >= 0) {
        //    $(document).attr("title", "(fetSMS)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&fetsms=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?fetsms=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('jkos') >= 0) {
        //    $(document).attr("title", "(街口)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&jkos=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?jkos=1");
        //            }
        //        }
        //    });
        //}

        ////大樓電梯
        //if (location.search.indexOf('elv') >= 0) {
        //    $(document).attr("title", "(ELV)" + $("title").html());

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&elv=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?elv=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('gads') >= 0) {
        //    $(document).attr("title", "(GADS)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&gads=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?gads=1");
        //            }
        //        }
        //    });
        //}

        //if (location.search.indexOf('tads') >= 0) {
        //    $(document).attr("title", "(TADS)" + $("title").html());
        //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

        //    $.each($("a"), function (i, n) {
        //        var $href = $(this).attr("href");
        //        if ($href.indexOf('.aspx') > 0) {
        //            if ($href.indexOf('?') > 0) {
        //                $(this).attr("href", $href + "&tads=1");
        //            }
        //            else {
        //                $(this).attr("href", $href + "?tads=1");
        //            }
        //        }
        //    });
        //}

        $("#dialog").dialog({
            autoOpen: false,
            show: {
                effect: "slide",
                duration: 1000
            },
            hide: {
                effect: "explode",
                duration: 1000
            },
            modal: true,
            width: dialogwidth,
            position: yourPosition,
            closeOnEscpe: true,
            title: "商品說明",
            focus: function (ev, data) {
                $("#dialog .ui-dialog-titlebar-close").focus();
            }
        });

        $("#dialog2").dialog({
            autoOpen: false,
            show: {
                effect: "slide",
                duration: 1000
            },
            hide: {
                effect: "explode",
                duration: 1000
            },
            modal: true,
            width: dialogwidth,
            position: yourPosition,
            closeOnEscpe: true,
            title: "商品說明",
            focus: function (ev, data) {
                $("#dialog2 .ui-dialog-titlebar-close").focus();
            }
        });
    });

//預設載入
$(window).on("load", function(){
    roundPlace();
})
    
//自適應
$( window ).resize(function() {
    roundPlace();
});    
    
//判斷網頁高度
function roundPlace(){
    bw = $(window).width();
    $("#WWid").text(bw)
    
    bh = $(window).height() / 2;
    
    rw = $(".Round").outerWidth() / 2;
    rh = $(".Round").outerHeight() / 2;
    
    rLeft = bw - rw;
    rTop = bh - rh;
    
    $(".Round").css({
        "left" : rLeft+"px",
        "top" : rTop+"px",
        "opacity" : 1
    });
}
</script>


<!-------------套件 */start---------------->
<!--輪播-->
<link rel="stylesheet" type="text/css" href="https://bobibobi.tw/Product/css/slick.css"/>
<link rel="stylesheet" type="text/css" href="https://bobibobi.tw/Product/css/slick-theme.css"/>
<script type="text/javascript" src="https://bobibobi.tw/Product/js/slick.min.js"></script>
<script>
$('.ProductsImg').slick({
    dots:true,
    arrows:false
});

</script>

<!--彈跳視窗-->
<link rel="stylesheet" type="text/css" href="https://bobibobi.tw/Product/css/fancybox.css"/>
<script type="text/javascript" src="https://bobibobi.tw/Product/js/fancybox.umd.js"></script>
<script>
    Fancybox.bind('[data-fancybox]', {});    
</script>

<!--縣市區-->
<script src="https://cdn.jsdelivr.net/npm/jquery-twzipcode@1.7.14/jquery.twzipcode.min.js"></script>
<script>
$(".twzipcode").twzipcode({zipcodeIntoDistrict: true, language: 'lang/zh-tw'});
</script>
<!-------------套件 */end---------------->




<!--購物車程式-->
<script>
//產品數量增加
$(".CountBtn").on("click", function(){
    btnType = $(this).attr("id");
    ItemNum = btnType.match(/\d+/g);
    ItemNowCount = parseInt($("#Item_"+ItemNum).val());//目前數量

    // 1. 先準備一張 map，指定哪些 ItemNum 的最大數量是多少
    //    沒在 map 裡的，就視為沒限制（可以用任何數量）。
    var maxCounts = {
        "8": 1,
        "9": 1,
        "10": 1
        // 如果之後想要其他商品也限制成 0~N，把像 "12": 2 加進來即可
    };
    // 預設最大值：如果對照表裡沒找到，就回傳一個大值（例如 999）
    var maxCount = maxCounts[ItemNum] !== undefined ? maxCounts[ItemNum] : 999;

    if (btnType.indexOf("Plus") > -1) {
        // 只有在「現在數量 < 最大值」時，才讓它 +1
        if (ItemNowCount < maxCount) {
            ItemNowCount = ItemNowCount + 1;
            $("#Item_" + ItemNum).val(ItemNowCount);

            if (ItemNum == 1) {
                $("#ItemTag").append('<li id="engrave_' + ItemNowCount + '"><label>編號<span>' + ItemNowCount + '</span></label><div><label><input type="radio" name="engrave' + ItemNowCount + '" value="n" id="engrave_' + ItemNowCount + '_n" checked="checked"><span>無</span></label><label><input type="radio" name="engrave' + ItemNowCount + '" value="y" id="engrave_' + ItemNowCount + '_y"><span>有</span></label></div><div><input name="engraveName" type="text" id="engraveName_' + ItemNowCount + '"  readonly="readonly" maxlength="5" placeholder="限4個字"></div></li>');
            }
        }
        else {
            // 超過最大值，就可以提示一下或直接略過
            alert("此商品最多只能選 0～" + maxCount + " 件。");
        }        
    }else if(btnType.indexOf("Less") > -1) {
        if(ItemNowCount != 0){
            $("#Item_"+ItemNum).val(ItemNowCount - 1);
            if(ItemNum == 1){$("#ItemTag li:last").remove();}
        }
    }
    
})
</script>


<!--訂單確認-->
<script>
$("#CheckOrder").on("click", function(){
    //pd_A = $("#Item_1").val();//產品1數量
    pd_A = typeof ($("#Item_1").val()) == "undefined" ? 0 : $("#Item_1").val();     //產品1數量
    //pd_B = $("#Item_2").val();//產品2數量
    //pd_C = $("#Item_3").val();//產品3數量
    pd_D = typeof ($("#Item_4").val()) == "undefined" ? 0 : $("#Item_4").val();     //產品4數量
    pd_E = typeof ($("#Item_5").val()) == "undefined" ? 0 : $("#Item_5").val();     //產品5數量
    pd_F = typeof ($("#Item_6").val()) == "undefined" ? 0 : $("#Item_6").val();     //產品6數量
    pd_G = typeof ($("#Item_7").val()) == "undefined" ? 0 : $("#Item_7").val();     //產品7數量
    pd_H = typeof ($("#Item_8").val()) == "undefined" ? 0 : $("#Item_8").val();     //產品8數量
    pd_I = typeof ($("#Item_9").val()) == "undefined" ? 0 : $("#Item_9").val();     //產品9數量
    pd_J = typeof ($("#Item_10").val()) == "undefined" ? 0 : $("#Item_10").val();   //產品10數量
    cusName = $('#Name').val();//購買人姓名
    cusTel = $('#Tel').val();//購買人電話
    cusEmail = $('#Email').val();//購買人信箱
    cusAdd = $("select[name='county']").val() + $("select[name='district']").val() + $('#Add').val();//購買人地址
    Money = (pd_A * 1480)                                                                            //購買總金額
        /*+ (pd_B * 499) + (pd_C * 2980)*/
        + (pd_D * 399)
        + (pd_E * 399)
        + (pd_F * 399)
        + (pd_G * 399)
        + (pd_H * 228)
        + (pd_I * 478)
        + (pd_J * 688);   
    
    
    
    //初始化
    $('#OrderItemTag').empty();//清空確認訂單頁裡的所有屬名雕刻服務名單 
    checkComplet = 0;//判斷是否所有欄位皆已填寫
    alertTxt = "";//提示文字
    
    
    //未選擇商品
    if (pd_A == 0 /*&& pd_B == 0 && pd_C == 0*/ && pd_D == 0 && pd_E == 0 && pd_F == 0 && pd_G == 0 && pd_H == 0 && pd_I == 0 && pd_J == 0) {
        alertTxt = "請增加您要購買的商品數量。";
        checkComplet = 1;
    }else{
        
        $(".OrderSum span").text(Money);

        $(".OrderItemA").hide();
        $(".OrderItemD").hide();
        $(".OrderItemE").hide();
        $(".OrderItemF").hide();
        $(".OrderItemG").hide();
        $(".OrderItemH").hide();
        $(".OrderItemI").hide();
        $(".OrderItemJ").hide();

        if (pd_A > 0) {
            $("#OrderItemAcount").text(pd_A);
            $(".OrderItemA").show();
        }
        else {
            $(".OrderItemA").hide();
        }

        if (pd_D > 0) {
            $("#OrderItemDcount").text(pd_D);
            $(".OrderItemD").show();
        }
        else {
            $(".OrderItemD").hide();
        }

        if (pd_E > 0) {
            $("#OrderItemEcount").text(pd_E);
            $(".OrderItemE").show();
        }
        else {
            $(".OrderItemE").hide();
        }
        if (pd_F > 0) {
            $("#OrderItemFcount").text(pd_F);
            $(".OrderItemF").show();
        }
        else {
            $(".OrderItemF").hide();
        }
        if (pd_G > 0) {
            $("#OrderItemGcount").text(pd_G);
            $(".OrderItemG").show();
        }
        else {
            $(".OrderItemG").hide();
        }
        if (pd_H > 0) {
            $("#OrderItemHcount").text(pd_H);
            $(".OrderItemH").show();
        }
        else {
            $(".OrderItemH").hide();
        }
        if (pd_I > 0) {
            $("#OrderItemIcount").text(pd_I);
            $(".OrderItemI").show();
        }
        else {
            $(".OrderItemI").hide();
        }
        if (pd_J > 0) {
            $("#OrderItemJcount").text(pd_J);
            $(".OrderItemJ").show();
        }
        else {
            $(".OrderItemJ").hide();
        }
        //$("#OrderItemBcount").text(pd_B);
        //$("#OrderItemCcount").text(pd_C);
        //$("#OrderItemDcount").text(pd_D);
        //$("#OrderItemEcount").text(pd_E);
        //$("#OrderItemFcount").text(pd_F);
        //$("#OrderItemGcount").text(pd_G);
        
        if(pd_A != 0){
            checkTimes = 0;//抓取編號
            $('#ItemTag li').each(function() {
                checkTimes = checkTimes + 1;
            });
        }

        if(cusName == ""){
            $('#Name').css("background","#ffc8c8");
            alertTxt = alertTxt + "購買人姓名未填寫。<br>";
            checkComplet = checkComplet + 1;
        }else{
            $('#Name').css("background","");
            $(".OrderName").text(cusName);
        }

        if(cusTel == ""){
            $('#Tel').css("background","#ffc8c8");
            alertTxt = alertTxt + "購買人電話未填寫。<br>";
            checkComplet = checkComplet + 1;
        }
        else if (!Isphone(cusTel)){
            $('#Tel').css("background", "#ffc8c8");
            alertTxt = alertTxt + "購買人電話格式錯誤。<br>";
            checkComplet = checkComplet + 1;
        }
        else {
            $('#Tel').css("background","");
            $(".OrderTel").text(cusTel);
        }

        if (cusEmail == "") {
            $('#Email').css("background", "#ffc8c8");
            alertTxt = alertTxt + "購買人信箱未填寫。<br>";
            checkComplet = checkComplet + 1;
        }
        else if (!IsEmail(cusEmail)) {
            $('#Email').css("background", "#ffc8c8");
            alertTxt = alertTxt + "購買人信箱格式錯誤。<br>";
            checkComplet = checkComplet + 1;
        }
        else {
            $('#Email').css("background", "");
            $(".OrderEmail").text(cusEmail);
        }

        if($("select[name='county']").val() == ""){
            $('select[name="county"]').css("background","#ffc8c8");
            alertTxt = alertTxt + "未選擇縣市。<br>";
            checkComplet = checkComplet + 1;
        }else{
            $('select[name="county"]').css("background","");
        }

        if($("select[name='district']").val() == ""){
            $('select[name="district"]').css("background","#ffc8c8");
            alertTxt = alertTxt + "未選擇鄉鎮市區。<br>";
            checkComplet = checkComplet + 1;
        }else{
            $('select[name="district"]').css("background","");
        }

        if(cusAdd == ""){
            $('#Add').css("background","#ffc8c8");
            alertTxt = alertTxt + "購買人地址未填寫。<br>";
            checkComplet = checkComplet + 1;
        }else{
            $('#Add').css("background","");
            $(".OrderAdd").text(cusAdd);
        }


        // 2. 如果 OTP 合格，再驗證發票欄位
        isValid = true;
        var invoiceType = $('input[name="InvoiceType"]:checked').val();

        if (invoiceType === '1') {
            // 一般電子發票
            $(".OrderinvoiceType").text("一般電子發票");
            $(".OrderCarrierCode").text("");
            $(".OrderinvCode").text("");
            $(".OrderinvName").text("");
            $("#orderCarrierCode").hide();
            $("#orderCompany").hide();
        }
        else if (invoiceType === '2') {
            // 手機載具發票 → 檢查手機載具欄位
            var carrier = $('#CarrierCode').val() ? $('#CarrierCode').val().trim() : '';
            if (!carrier) {
                checkComplet = checkComplet + 1;

                alertTxt = alertTxt + "請輸入手機載具條碼。<br>";
                $('#CarrierCode').focus();
            } else {
                checkComplet = 0;
                $(".OrderinvoiceType").text("手機載具發票");
                $(".OrderCarrierCode").text(carrier);
                $(".OrderinvCode").text("");
                $(".OrderinvName").text("");
                $("#orderCarrierCode").show();
                $("#orderCompany").hide();
            }
        }
        else if (invoiceType === '4') {
            // 公司發票 → 檢查統編 + 公司名稱
            var invCode = $('#InvoiceCode').val() ? $('#InvoiceCode').val().trim() : '';
            var invName = $('#InvoiceName').val() ? $('#InvoiceName').val().trim() : '';
            if (!invCode || !invName) {
                checkComplet = checkComplet + 1;
                if (!invCode) $('#InvoiceCode').addClass('unfilled'); else $('#InvoiceCode').removeClass('unfilled');
                if (!invName) $('#InvoiceName').addClass('unfilled'); else $('#InvoiceName').removeClass('unfilled');

                alertTxt = alertTxt + "請填寫統一編號與公司名稱。<br>";
                (invCode ? $('#InvoiceName') : $('#InvoiceCode')).focus();
            } else {
                checkComplet = 0;
                $(".OrderinvoiceType").text("公司發票");
                $(".OrderCarrierCode").text("");
                $(".OrderinvCode").text(invCode);
                $(".OrderinvName").text(invName);
                $("#orderCarrierCode").hide();
                $("#orderCompany").show();
            }
        }
    }
    
    if(checkComplet > 0 ){
        $("#AlertContent").html(alertTxt);
        $("#alertBox").fadeIn();
    }else if(checkComplet == 0){
        $("#checkOrder").fadeIn();
    }
})

//關閉提示
$(".CloseBtn input").on("click", function(){
    $("#alertBox").fadeOut();
})    

//關閉訂單確認頁
$("#EditOrder").on("click", function(){
    $("#checkOrder").fadeOut();
})
</script>


<!----------TO工程師：各欄位取值與付款按鈕---------->
<script>
//取得各欄位的值
function formData(){
    pd_A = typeof ($("#Item_1").val()) == "undefined" ? 0 : $("#Item_1").val();     //鎮宅、開運錢母擺件數量
    //pd_B = $("#Item_2").val();//開運隨身御守數量
    //pd_C = $("#Item_3").val();//2024新港奉天宮黃金符令手鍊
    pd_D = typeof ($("#Item_4").val()) == "undefined" ? 0 : $("#Item_4").val();     //招財大嘴貓(白色)數量
    pd_E = typeof ($("#Item_5").val()) == "undefined" ? 0 : $("#Item_5").val();     //招財大嘴貓(藍色)數量
    pd_F = typeof ($("#Item_6").val()) == "undefined" ? 0 : $("#Item_6").val();     //招財大嘴貓(粉色)數量
    pd_G = typeof ($("#Item_7").val()) == "undefined" ? 0 : $("#Item_7").val();     //招財大嘴貓(橘色)數量
    pd_H = typeof ($("#Item_8").val()) == "undefined" ? 0 : $("#Item_8").val();     //午時水/1罐數量
    pd_I = typeof ($("#Item_9").val()) == "undefined" ? 0 : $("#Item_9").val();     //午時水/3罐數量
    pd_J = typeof ($("#Item_10").val()) == "undefined" ? 0 : $("#Item_10").val();   //午時水/6罐數量
    cusName = $('#Name').val();//購買人姓名
    cusTel = $('#Tel').val();//購買人電話
    cusEmail = $('#Email').val();//購買人信箱
    cusAdd = $("select[name='county']").val() + $("select[name='district']").val() + $('#Add').val();//購買人地址
    cusCounty = $("select[name='county']").val();
    cusDistrict = $("select[name='district']").val();
    cusAddr = $("#Add").val();
    cusZipCode = $("input[name='zipcode']").val();
    Money = (pd_A * 1480)                                                                            //購買總金額
        /*+ (pd_B * 499) + (pd_C * 2980)*/
        + (pd_D * 399)
        + (pd_E * 399)
        + (pd_F * 399)
        + (pd_G * 399)
        + (pd_H * 228)
        + (pd_I * 478)
        + (pd_J * 688);
    invType = $('input[name="InvoiceType"]:checked').val();
    carrier = $('#CarrierCode').val() ? $('#CarrierCode').val().trim() : '';
    invCode = $('#InvoiceCode').val() ? $('#InvoiceCode').val().trim() : '';
    invName = $('#InvoiceName').val() ? $('#InvoiceName').val().trim() : '';
    
    //下方取消註解後，可於console查看結果
    console.log("擺件數量：" + pd_A,
        //"御守數量：" + pd_B,
        //"黃金符令手鍊數量：" + pd_C,
        "招財大嘴貓(白色)數量：" + pd_D,
        "招財大嘴貓(藍色)數量：" + pd_E,
        "招財大嘴貓(粉色)數量：" + pd_F,
        "招財大嘴貓(橘色)數量：" + pd_G,
        "午時水/1罐數量：" + pd_H,
        "午時水/3罐數量：" + pd_I,
        "午時水/6罐數量：" + pd_J,
        "姓名：" + cusName,
        "電話：" + cusTel,
        "信箱：" + cusEmail,
        "地址：" + cusAdd,
        "發票類型：" + invType,
        "手機載具：" + carrier,
        "統一編號：" + invCode,
        "公司名稱：" + invName,
        Money + "元");
}
    

//遠傳手機門號付款
$("#mobile_pay").on("click", function(){
    //付款串接放這裡
    formData();
    nextStep('FetCSP');
})

    //中華手機門號付款
    $("#mobile_cht_pay").on("click", function () {
        //付款串接放這裡
        formData();
        nextStep('ChtCSP');
    })

    //台哥大手機門號付款
    $("#mobile_twm_pay").on("click", function () {
        //付款串接放這裡
        formData();
        nextStep('TwmCSP');
    })

    //LINEPAY付款
    $("#Line_pay").on("click", function () {
        //付款串接放這裡
        formData();
        nextStep('LinePay');
    })

    //街口支付付款
    $("#Jkos_pay").on("click", function () {
        //付款串接放這裡
        formData();
        nextStep('JkosPay');
    })

    //APPLEAPAY付款
    $("#apple_pay").on("click", function () {
        //付款串接放這裡
        formData();
        nextStep('ApplePay');
    })

//信用卡付款
$("#card_pay").on("click", function(){
    //付款串接放這裡
    formData();
    nextStep('CreditCard');
})

    
//Google付款
//$("#Google_pay").on("click", function(){
//    //付款串接放這裡
//    formData();
//    nextStep('GOOGLEPAY');
//})    

    //前往付款
    function nextStep(ChargeType) {
        data = {
            pd_A: pd_A,
            //pd_B: pd_B,
            //pd_C: pd_C,
            pd_D: pd_D,
            pd_E: pd_E,
            pd_F: pd_F,
            pd_G: pd_G,
            pd_H: pd_H,
            pd_I: pd_I,
            pd_J: pd_J,
            cusName: cusName,
            cusTel: cusTel,
            cusEmail: cusEmail,
            cusCounty: cusCounty,
            cusDistrict: cusDistrict,
            cusAddr: cusAddr,
            cusZipCode: cusZipCode,
            invType: invType,
            carrier: carrier,
            invCode: invCode,
            invName: invName,
            Money: Money,
            ChargeType: ChargeType
        };

        ac_loadServerMethod("gotopay", data, gotopay);
    }

    //導向付款頁面
    function gotopay(res) {
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            if (res.redirect) {
                window.location = res.redirect;
            }
        } else {
            if (res.Stock == 1) {
                //擺件
                if (res.overStatus == -1) {
                    alert("鎮宅、開運錢母擺件數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("鎮宅、開運錢母擺件數量不足，請重新購買。");
                }

            }
            else if (res.Stock == 3) {
                //香火袋
                if (res.overStatus == -1) {
                    alert("開運隨身御守數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("開運隨身御守數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 4) {
                //香火袋
                if (res.overStatus == -1) {
                    alert("2024新港奉天宮黃金符令手鍊數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("2024新港奉天宮黃金符令手鍊數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 5) {
                //招財大嘴貓(白色)
                if (res.overStatus == -1) {
                    alert("招財大嘴貓(白色)數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("招財大嘴貓(白色)數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 6) {
                //招財大嘴貓(藍色)
                if (res.overStatus == -1) {
                    alert("招財大嘴貓(藍色)數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("招財大嘴貓(藍色)數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 7) {
                //招財大嘴貓(粉色)
                if (res.overStatus == -1) {
                    alert("招財大嘴貓(粉色)數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("招財大嘴貓(粉色)數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 8) {
                //招財大嘴貓(橘色)
                if (res.overStatus == -1) {
                    alert("招財大嘴貓(橘色)數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("招財大嘴貓(橘色)數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 9) {
                //午時水/1罐
                if (res.overStatus == -1) {
                    alert("午時水/1罐數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("午時水/1罐數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 10) {
                //午時水/3罐
                if (res.overStatus == -1) {
                    alert("午時水/3罐數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("午時水/3罐數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 11) {
                //午時水/6罐
                if (res.overStatus == -1) {
                    alert("午時水/6罐數量已額滿，請重新購買。");
                }
                else if (res.overStatus == -2) {
                    alert("午時水/6罐數量不足，請重新購買。");
                }
            }
            else if (res.Stock == 12) {
                // 手機載具驗證失敗
                alert("手機載具格式錯誤，請重新輸入！如一直錯誤，請聯繫客服");
            }
            else if (res.Stock == 13) {
                // 電子發票綁定失敗
                alert("電子發票綁定資料錯誤，請重新輸入！如一直錯誤，請聯繫客服！");
            }
            else {
                alert("前往付款失敗！請重新再試一次，若還是不行，請洽客服。");
            }
        }
    }

    var dialogwidth;
    if (document.body.scrollWidth < 767) {
        dialogwidth = document.body.scrollWidth;
    }
    else {
        dialogwidth = 480;
    }

    //詳細說明
    $("#Description").on("click", function () {
        $("#dialog").dialog("open");
        $("#dialog").focus();
        return false;
    })
    $("#Description2").on("click", function () {
        $("#dialog2").dialog("open");
        $("#dialog2").focus();
        return false;
    })

</script>
