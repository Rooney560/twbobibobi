<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templeInfo.aspx.cs" Inherits="Temple.Temples.templeInfo" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>
<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content=<%=title%>|合作宮廟|【保必保庇】線上宮廟服務平台 />
    <!--標題-->
    <meta property="og:url" content=<%=ogurl %> />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content=<%=title%>|合作宮廟|【保必保庇】線上宮廟服務平台 />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title><%=title%>|合作宮廟|【保必保庇】線上宮廟服務平台</title>
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
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async="" src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
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
                    <li><%=title%></li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="TempleImg">
                    <img src="<%=imgURL%>" width="600" height="400" alt="" />
                </div>
                <h1 class="TempleName"><%=title%></h1>
                <div class="TempleInfo">
                    <!--介紹說明放這裡-->
                    <p><%=description %></p>
                </div>
                <div class="TempleService">
                    <ul>
                        <%=servicelist %>
                    </ul>
                </div>
                <div class="BackBtn">
                    <a href="temple.aspx">回上一層</a>
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

    function ActivityTime(kind, a) {
        data = {
            kind: kind,
            admin: a
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

            if (StartDate == "0" && EndDate == "0") {
                location.href = res.URL;
            }
            else {
                let today = new Date() //現在
                let someday = new Date(StartDate) //活動開始
                let endday = new Date(EndDate)  //活動結束
                var diff = someday.getTime() - today.getTime(); //diff = 活動開始 - 現在
                var diff_end = endday.getTime() - today.getTime(); //diff_end = 現在 - 活動結束
                var month = diff / 1000 / 60 / 60 / 24 / 30; //現在與活動開始時間差幾個月
                var month_end = diff_end / 1000 / 60 / 60 / 24 / 30; //現在與活動結束時間差幾個月

                //alert("today:" + today + ", sDate:" + someday + ", eDate:" + endday + ", diff:" + diff + ", diff_end:" + diff_end + ", month:" + month);

                if (month < 1 && diff_end >= 0) {
                    if (diff <= 0 && diff_end >= 0) {
                        location.href = res.URL;
                    }
                    else {
                        alert(Name + " " + Service + " 活動即將開始！");
                    }
                }
                else {
                    //alert(Math.abs(month_end));
                    if (diff_end <= 0 && Math.abs(month_end) <= 4) {
                        alert(Name + " " + Service + " 活動已結束！");
                    }
                    else {
                        alert(Name + " " + Service + " 活動尚未開始！");
                    }
                }
            }
        }
    }
</script>