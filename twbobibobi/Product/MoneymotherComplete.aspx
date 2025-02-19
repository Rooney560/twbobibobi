<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneymotherComplete.aspx.cs" Inherits="twbobibobi.Product.MoneymotherComplete" %>
<%@ Register src="~/Temples/SocialMedia.ascx" tagprefix="uc3" tagname="SocialMedia" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="新港奉天宮鎮宅錢母擺件" /><!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Product/MoneymotherComplete.aspx" /><!--網址：請補上網址-->
    <meta name="description" content="" /><!--簡介-->
    <meta property="og:description" content="" /><!--簡介-->
    <meta property="og:site_name" content="新港奉天宮鎮宅錢母擺件" /><!--標題-->
    <meta property="og:type" content="website" />
    <title>訂單結果</title><!--標題-->
    <!--抓取圖片-->
    <meta property="og:image" content="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
    <meta name="twitter:image:src" content="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
    <link rel="image_src" href="https://bobibobi.tw/Product/images/products/products_A_1.jpg" />
	
    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />

    <!--預設載入css-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
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
            setURL("ftg", "2290", "奉天宮");
        });
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
<div id="wrap"><!--#warp //start-->
    
    <div id="OrderComplete" class="OrderPupBox">
        <div class="OrderTitle">訂單完成</div>

        <div class="OrderContent">
            <div class="shopproducts">
                <div class="OrderproductItem" id="OrderItemA" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_A_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>鎮宅、開運錢母擺件</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>1480</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemAcount"><%=OrderItemAcount %>&nbsp; </span>個</div>
                        </div>
                        <div class="ItemInfo">
                            <ul id="OrderItemTag">
                                <!--
                                ↓↓↓以下可以刪除↓↓↓
                                <li>屬名雕刻服務-編號<span>1</span>：<span>無</span></li>
                                -->
                                <%=OrderItemTag %>
                            </ul>
                        </div>
                    </div>
                </div>
                <%--<div class="OrderproductItem" id="OrderItemB" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_B_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>開運隨身御守</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>499</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemBcount"><%=OrderItemBcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="OrderproductItem" id="OrderItemC" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_C_1.jpg" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>2024新港奉天宮黃金符令手鍊</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>2980</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemCcount"><%=OrderItemCcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>--%>
                <div class="OrderproductItem" id="OrderItemD" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_D_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(白色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemDcount"><%=OrderItemDcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem" id="OrderItemE" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_E_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(藍色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemEcount"><%=OrderItemEcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem" id="OrderItemF" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_F_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(粉色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemFcount"><%=OrderItemFcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>
                <div class="OrderproductItem" id="OrderItemG" runat="server">
                    <div class="shopProductsImg"><img src="https://bobibobi.tw/Product/images/products/products_G_1.png" width="1200" height="1200" alt=""/></div>
                    <div class="shopProductsInfo">
                        <h5>招財大嘴貓(橘色)</h5>
                        <div class="ItemInfo">
                            <label>金額</label>
                            <div>399</div>
                        </div>
                        <div class="ItemInfo">
                            <label>數量</label>
                            <div><span id="OrderItemGcount"><%=OrderItemGcount %>&nbsp; </span>個</div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="shopmemberInfo">
                <div class="shopmemberInfoTitle">購買人資料<span>（資料務必填寫正確，日後查詢訂單使用。聯絡電話請填09開頭號碼，例：0912345678。）</span></div>
                <div class="shopmemberForm">
                    <div><label>訂單編號</label><div class="OrderName"><%=OrderNumString %></div></div>
                    <div><label>購買人</label><div class="OrderName"><%=OrderName %></div></div>
                    <div><label>聯絡電話</label><div class="OrderTel"><%=OrderTel %></div></div>
                    <div><label>收件地址</label><div class="OrderAdd"><%=OrderZipCode %>&nbsp; <%=OrderAdd %></div></div>
                </div>

                <div class="OrderSum">購買總金額：<span><%=OrderTotal %>&nbsp; </span>元</div>
            </div>

                        <uc3:SocialMedia runat="server" id="SocialMedia" />
                    <br />
                <%--<div class="shopmemberInfo">
                    <p style="color: red;">因應農曆春節連續假期及貨運公司配送問題，將於2025/1/21至2025/02/02暫停出貨作業。</p>
                    <br />
                    <p style="color: red;">2025/1/20至2025/02/02成立的訂單，將於2025/02/03起陸續出貨。 祝大家小龍年行大運!</p>
                </div>--%>
            <div class="PayButton">
                <a href="https://bobibobi.tw/Product/MoneymotherIndex.aspx" title="回活動頁面">回活動頁面</a>
            </div>
        </div>

    </div>  
    

</div><!--#warp //end-->
</body>
</html>
