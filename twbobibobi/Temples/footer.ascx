<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="Temple.Temples.footer" %>

    <script type="text/javascript">
        $(function () {
            if (location.search.indexOf('twm') >= 0) {
                $("#fet").hide();
                $("#Fet").hide();
                $("#cht").hide();
                $("#twm").show();

            }
            else {
                $("#fet").show();
                $("#Fet").show();
                $("#cht").show();
                $("#cht").show();
            }

            if (location.search.indexOf('twm') >= 0) {
                //alert($("title").html());
                $(document).attr("title", "(TWM)" + $("title").html());
                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0 && $href.indexOf('twm') < 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&twm=1");
                        }
                        else {
                            $(this).attr("href", $href + "?twm=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('line') >= 0) {
                $(document).attr("title", "(LINE)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&line=1");
                        }
                        else {
                            $(this).attr("href", $href + "?line=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('fb') >= 0) {

                if (location.search.indexOf('fbad') >= 0) {
                    $(document).attr("title", "(FB廣告)" + $("title").html());
                    //$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                    $.each($("a"), function (i, n) {
                        var $href = $(this).attr("href");
                        if ($href.indexOf('.aspx') > 0) {
                            if ($href.indexOf('?') > 0) {
                                $(this).attr("href", $href + "&fbad=1");
                            }
                            else {
                                $(this).attr("href", $href + "?fbad=1");
                            }
                        }
                    });
                }
                else if (location.search.indexOf('fbda') >= 0) {
                    $(document).attr("title", "(FBDA)" + $("title").html());
                    $("#shop").hide();
                    $.each($("a"), function (i, n) {
                        var $href = $(this).attr("href");
                        if ($href.indexOf('.aspx') > 0) {
                            if ($href.indexOf('?') > 0) {
                                $(this).attr("href", $href + "&fbda=1");
                            }
                            else {
                                $(this).attr("href", $href + "?fbda=1");
                            }
                        }
                    });
                }
                else {
                    $(document).attr("title", "(FB)" + $("title").html());
                    //$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                    $.each($("a"), function (i, n) {
                        var $href = $(this).attr("href");
                        if ($href.indexOf('.aspx') > 0) {
                            if ($href.indexOf('?') > 0) {
                                $(this).attr("href", $href + "&fb=1");
                            }
                            else {
                                $(this).attr("href", $href + "?fb=1");
                            }
                        }
                    });
                }
            }

            if (location.search.indexOf('ig') >= 0) {
                $(document).attr("title", "(IG)" + $("title").html());
                //$(document).attr("title", "２０２４(FB)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");
                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&ig=1");
                        }
                        else {
                            $(this).attr("href", $href + "?ig=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('cht') >= 0) {
                $(document).attr("title", "(CHT)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&cht=1");
                        }
                        else {
                            $(this).attr("href", $href + "?cht=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('fetsms') >= 0) {
                $(document).attr("title", "(fetSMS)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&fetsms=1");
                        }
                        else {
                            $(this).attr("href", $href + "?fetsms=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('jkos') >= 0) {
                $(document).attr("title", "(街口)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&jkos=1");
                        }
                        else {
                            $(this).attr("href", $href + "?jkos=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('pxpayplues') >= 0) {
                $(document).attr("title", "(全支付)" + $("title").html());

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&pxpayplues=1");
                        }
                        else {
                            $(this).attr("href", $href + "?pxpayplues=1");
                        }
                    }
                });
            }

            //大樓電梯
            if (location.search.indexOf('elv') >= 0) {
                $(document).attr("title", "(ELV)" + $("title").html());

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&elv=1");
                        }
                        else {
                            $(this).attr("href", $href + "?elv=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('gads') >= 0) {
                $(document).attr("title", "(GADS)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&gads=1");
                        }
                        else {
                            $(this).attr("href", $href + "?gads=1");
                        }
                    }
                });
            }

            if (location.search.indexOf('tads') >= 0) {
                $(document).attr("title", "(TADS)" + $("title").html());
                //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                $.each($("a"), function (i, n) {
                    var $href = $(this).attr("href");
                    if ($href.indexOf('.aspx') > 0) {
                        if ($href.indexOf('?') > 0) {
                            $(this).attr("href", $href + "&tads=1");
                        }
                        else {
                            $(this).attr("href", $href + "?tads=1");
                        }
                    }
                });
            }

            var adminlist = ["inda", "inh", "inwu", "inFu", "inLuer", "inty", "inFw", "indh", "inLk", "inma", "inwjsan"];
            adminlist.forEach(function (item, index, array) {

                if (location.search.indexOf(item) >= 0) {
                    $(document).attr("title", "(" + item.toUpperCase() + ")" + $("title").html());
                    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                    $.each($("a"), function (i, n) {
                        var $href = $(this).attr("href");
                        if ($href.indexOf('.aspx') > 0) {
                            if ($href.indexOf('?') > 0) {
                                $(this).attr("href", $href + "&" + item +"=1");
                            }
                            else {
                                $(this).attr("href", $href + "?" + item +"=1");
                            }
                        }
                    });
                }
                //if (location.search.indexOf('inda') >= 0) {
                //    $(document).attr("title", "(INDA)" + $("title").html());
                //    //$(document).attr("title", "２０２４(LINE)中元普渡線上報名開始囉~|最新消息|【保必保庇】線上宮廟服務平台");

                //    $.each($("a"), function (i, n) {
                //        var $href = $(this).attr("href");
                //        if ($href.indexOf('.aspx') > 0) {
                //            if ($href.indexOf('?') > 0) {
                //                $(this).attr("href", $href + "&inda=1");
                //            }
                //            else {
                //                $(this).attr("href", $href + "?inda=1");
                //            }
                //        }
                //    });
                //}
            });

        });
    </script>

        <footer>
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
                            <li><a href="https://bobibobi.tw/Temples/LightsGuide.aspx">燈種說明</a></li>
                            <li><a href="https://bobibobi.tw/Temples/ZodiacFortune.aspx">生肖運勢</a></li>
                            <li><a href="https://bobibobi.tw/Temples/NewYearNotes.aspx">過年注意事項</a></li>
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
                            <li><a href="line://ti/p/@bobibobi.tw" target="_blank">
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
                            <li>
                                <img src="https://bobibobi.tw//Temples/images/foot_logo_01.png" alt="薪薪網元" class="CoLogo_T" /></li>
                        </ul>
                    </div>
                    <div class="copyright">Copyright©2022-<span id="NowYear"></span> 保必保庇線上點燈祈福平台<br>
                        All rights reserved.</div>
                </div>
            </div>
        </footer>
