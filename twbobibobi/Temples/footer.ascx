<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="Temple.Temples.footer" %>

    <script type="text/javascript">
        $(function () {
            var getUrlString = location.href;
            var url = new URL(getUrlString);
            var purl = url.searchParams.get('purl');

            var checkedTWMurl = false;
            var checkedHsurl = false;

            checkedTWMurl = location.search.indexOf('twm') >= 0 ? true : false;
            checkedCHTurl = location.search.indexOf('cht') >= 0 ? true : false;
            checkedHsurl = location.search.indexOf('hsdemo') >= 0 ? true : false;
            if (purl) {
                purl = purl.toLowerCase();
                checkedTWMurl = location.search.indexOf('purl') >= 0 && purl == 'twm' ? true : false;
            }

            if (checkedTWMurl) {
                $("#fet").hide();
                $("#Fet").hide();
                $("#cht").hide();
                $("#twm").show();

            }
            else if (checkedCHTurl) {
                $("#fet").hide();
                $("#Fet").hide();
                $("#cht").show();
                $("#twm").hide();
            }
            else {
                $("#fet").show();
                $("#Fet").show();
                $("#cht").show();
                $("#twm").show();
            }

            if (checkedHsurl) {
                $(".Demo").hide();
            }
            else {
                $(".Demo").show();
            }

            if (location.search.indexOf('twm') >= 0) {
                //alert($("title").html());
                $(document).attr("title", "(TWM)" + $("title").html());
                //$.each($("a"), function (i, n) {
                //    var $href = $(this).attr("href");
                //    if ($href.indexOf('.aspx') > 0 && $href.indexOf('twm') < 0) {
                //        if ($href.indexOf('?') > 0) {
                //            $(this).attr("href", $href + "&twm=1");
                //        }
                //        else {
                //            $(this).attr("href", $href + "?twm=1");
                //        }
                //    }
                //});
            }

            // 用 URL 物件解析
            var path = url.pathname.toLowerCase();
            var kind = url.searchParams.get("kind");
            var a = url.searchParams.get("a");

            // path 包含 purdue_da，或 (kind=2 且 a=3) 就隱藏
            if (path.includes("purdue_da")
                || (kind === "2" && a === "3")) {
                $("#shop").hide();
            }

            if (location.search.indexOf('line') >= 0) {
                $(document).attr("title", "(LINE)" + $("title").html());
            }

            if (location.search.indexOf('fb') >= 0) {

                if (location.search.indexOf('fbad') >= 0) {
                    $(document).attr("title", "(FB廣告)" + $("title").html());
                }
                else if (location.search.indexOf('fbda') >= 0) {
                    $(document).attr("title", "(FBDA)" + $("title").html());
                    $("#shop").hide();
                }
                else {
                    $(document).attr("title", "(FB)" + $("title").html());
                }
            }

            if (location.search.indexOf('ig') >= 0) {
                $(document).attr("title", "(IG)" + $("title").html());
            }

            if (location.search.indexOf('cht') >= 0) {
                $(document).attr("title", "(CHT)" + $("title").html());
            }

            if (location.search.indexOf('fetsms') >= 0) {
                $(document).attr("title", "(fetSMS)" + $("title").html());
            }

            if (location.search.indexOf('jkos') >= 0) {
                $(document).attr("title", "(街口)" + $("title").html());
            }

            if (location.search.indexOf('pxpayplues') >= 0) {
                $(document).attr("title", "(全支付)" + $("title").html());
            }

            //大樓電梯
            if (location.search.indexOf('elv') >= 0) {
                $(document).attr("title", "(ELV)" + $("title").html());
            }

            if (location.search.indexOf('gads') >= 0) {
                $(document).attr("title", "(GADS)" + $("title").html());
            }

            if (location.search.indexOf('tads') >= 0) {
                $(document).attr("title", "(TADS)" + $("title").html());
            }

            var adminlist = ["inda", "inh", "inwu", "inFu", "inLuer", "inty", "inFw", "indh", "inLk", "inma", "inwjsan"];
            adminlist.forEach(function (item, index, array) {
                if (location.search.indexOf(item) >= 0) {
                    $(document).attr("title", "(" + item.toUpperCase() + ")" + $("title").html());
                }
            });

            // 設定特定宮廟的對應名稱
            var templeMap = {
                "ad": "FB廣告",
                "da": "大甲鎮瀾宮",
                "h": "新港奉天宮",
                "wu": "北港武德宮",
                "fu": "西螺福興宮",
                "luer": "台南正統鹿耳門聖母廟",
                "ty": "桃園威天宮",
                "fw": "斗六五路財神宮",
                "dh": "台東東海龍門天聖宮",
                "hs": "五股賀聖宮",
                "lk": "鹿港城隍廟",
                "ma": "玉敕大樹朝天宮",
                "jb": "進寶財神廟",
                "wjsan": "台灣道教總廟無極三清總道院",
                "ld": "桃園龍德宮",
                "sx": "神霄玉府財神會館",
                "wh": "基隆悟玄宮",
                "st": "松柏嶺受天宮",
                "sl": "中寮石龍宮",
                "nt": "台中南天宮",
                "bj": "池上北極玄天宮",
                "sbbt": "慈惠石壁部堂",
                "bpy": "真武山受玄宮",
                "ssy": "壽山巖觀音寺"
            };

            // 設定網址參數對應的標題名稱
            var paramTitleMap = {
                "twm": "TWM",
                "cht": "CHT",
                "line": "LINE",
                "fb": "FB",
                "ig": "IG",
                "fetsms": "fetSMS",
                "jkos": "街口",
                "gads": "GADS",
                "elv": "ELV",
                "tads": "TADS",
                "pxpayplues": "全支付",
                "applepay": "APPLEPAY"
            };

            var searchParams = new URLSearchParams(window.location.search);
            var matchedParam = null;
            var matchedValue = "";

            // **找到第一個符合條件的參數**
            searchParams.forEach(function (value, key) {
                if (key == 'purl') {
                    value = value.toLowerCase();
                    if (value.startsWith("fb") && value.length > 2) {
                        // 自動對應粉絲團 (fbda → 大甲鎮瀾宮粉絲團)
                        var templeKey = value.substring(2); // 取得宮廟代碼，例如 fbda → da

                        if (templeKey == "ad") {
                            matchedParam = value;
                            matchedValue = templeMap[templeKey];
                        }
                        else if (templeMap[templeKey]) {
                            matchedParam = value;
                            matchedValue = templeMap[templeKey] + "粉絲團";
                        }
                    } else if (value.startsWith("in") && value.length > 2) {
                        // 自動對應立牌 (inda → 大甲鎮瀾宮立牌)
                        var templeKey = value.substring(2); // 取得宮廟代碼，例如 inda → da
                        if (templeMap[templeKey]) {
                            matchedParam = value;
                            matchedValue = templeMap[templeKey] + "立牌";
                        }
                    } else if (paramTitleMap[value]) {
                        // 直接對應 paramTitleMap
                        matchedParam = value;
                        matchedValue = paramTitleMap[value];
                    }
                }
            });

            // **如果找到符合條件的參數**
            if (matchedParam) {
                // **只刪除最前面的括號內容（例如：(xxx) 標題），後面有的不動**
                document.title = document.title.replace(/^\([^()]*\)\s*/, "");

                // **修改標題**
                document.title = "(" + matchedValue + ") " + document.title;

                // **統一處理 <a> 標籤，將該參數附加到超連結中**
                //$("a").each(function () {
                //    var $link = $(this);
                //    var href = $link.attr("href");

                //    if (href && href.indexOf(".aspx") > 0) {
                //        var parts = href.split("?");
                //        var baseUrl = parts[0];
                //        var queryString = parts[1] || "";

                //        var linkParams = new URLSearchParams(queryString);

                //        // 如果有 matchedParam（即 purl 有值），就設置進網址（無論原本有沒有 purl）
                //        if (matchedParam) {
                //            linkParams.set("purl", matchedParam);
                //        }

                //        // 組合新的 href 並回填
                //        var newHref = baseUrl + "?" + linkParams.toString();
                //        $link.attr("href", newHref);
                //    }
                //});
            } else if (searchParams.has("purl")) {
                // 有 purl 但無對應，刪除舊的括號
                document.title = document.title.replace(/^\([^()]*\)\s*/, "");
            }

        });
    </script>
    <style type="text/css">
        .footMenuList ul li {
            width: 50%;
            margin-right: 0;
        }

    </style>
        <footer class="Demo">
             <hr style="padding-bottom: 5px;" />
            <div class="footer">
                <div class="footMenu">
                    <div class="footTitle">網站服務</div>
                    <div class="footMenuList">
                        <ul>
                            <li><a href="https://bobibobi.tw/Temples/temple.aspx">合作宮廟</a></li>
                            <li><a href="https://shop.bobibobi.tw/">文創商品</a></li>
                            <li><a href="https://bobibobi.tw/Temples/news.aspx">最新消息</a></li>
                            <li><a href="https://bobibobi.tw/Temples/service.aspx">信眾服務</a></li>
                            <li><a href="https://bobibobi.tw/Temples/ShoppingGuide.aspx">購物說明</a></li>
                            <li><a href="https://bobibobi.tw/Temples/PrivacyPolicy.aspx">隱私權政策</a></li>
                            <li><a href="https://bobibobi.tw/Temples/articleColumn.aspx">文章專欄</a></li>
                            <li></li>
                        </ul>
                    </div>
                </div>
                <div class="footContact">
                    <div class="footTitle">聯絡方式</div>
                    <div class="footContactList">
                        <ul>
                            <li><a href="tel:0436092299">
                                <img src="https://bobibobi.tw/Temples/images/foot_icon_01.png" width="28" height="30" alt="" /><span>04-3609-2299</span></a></li>
                            <%--<li><a href="tel:0436092299">
                                <img src="https://bobibobi.tw/Temples/images/foot_icon_02.png" width="28" height="30" alt="" /><span>04-3609-2299</span></a></li>--%>
                            <li><a href="mailto:service@appssp.com">
                                <img src="https://bobibobi.tw/Temples/images/foot_icon_03.png" width="28" height="30" alt="" /><span>service@appssp.com</span></a></li>
                        </ul>
                    </div>
                </div>
                <div class="footCommunity">
                    <div class="footTitle">社群媒體</div>
                    <div class="footCommunityList">
                        <ul>
                            <li><a href="https://www.facebook.com/profile.php?id=100086934594859" target="_blank">
                                <img src="https://bobibobi.tw/Temples/images/community_icon_01.png" width="45" height="45" alt="" /></a></li>
                            <li><a href="https://page.line.me/bobibobi.tw" target="_blank">
                                <img src="https://bobibobi.tw/Temples/images/community_icon_02.png" width="45" height="45" alt="" /></a></li>
                            <li><a href="https://www.youtube.com/@bobibobi.tw1" target="_blank">
                                <img src="https://bobibobi.tw/Temples/images/community_icon_03.png" width="45" height="45" alt="" /></a></li>
                            <li><a href="https://www.instagram.com/bobibobi.tw" target="_blank">
                                <img src="https://bobibobi.tw/Temples/images/community_icon_04.png" width="45" height="45" alt="" /></a></li>
                        </ul>
                    </div>
                </div>
                <div class="footLogo">
                    <div class="footLogoList">
                        <ul>
                            <li id="cht">
                                <img src="https://bobibobi.tw//Temples/images/foot_logo_03.png" alt="中華電信" class="CoLogo_T" /></li>
                            <li id="fet">
                                <img src="https://bobibobi.tw//Temples/images/foot_logo_02.png" alt="遠傳電信" class="CoLogo_T" /></li>
                            <li id="twm">
                                <img src="https://bobibobi.tw//Temples/images/foot_logo_04.png" alt="台灣大哥大" class="CoLogo_T" /></li>
                            <li id="99">
                                <img src="https://bobibobi.tw//Temples/images/foot_logo_05.png?t=5678" alt="九九商通" title="九九商通" class="CoLogo_T" /></li>
                        </ul>
                    </div>
                    <div class="copyright">Copyright©2022-<span id="NowYear"></span> 九九商通科技有限公司<br>
                        All rights reserved.</div>
                </div>
            </div>
        </footer>
