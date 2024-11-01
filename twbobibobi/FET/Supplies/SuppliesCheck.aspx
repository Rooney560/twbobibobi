<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuppliesCheck.aspx.cs" Inherits="Temple.FET.Supplies.SuppliesCheck" %>

<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="shortcut icon" href="favicon.ico" />
<meta content="minimum-scale=1,initial-scale=1,width=device-width,shrink-to-fit=no" name="viewport" />
<link href="https://fonts.googleapis.com/css?family=Noto+Sans+TC:wght@400;500;600;700&family=Roboto:300,400,500,600,700&amp;display=swap" rel="stylesheet" />
<title>企業補財庫服務</title>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.css" />
<link rel="stylesheet" href="css/main.css" />
<script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
<meta name="keywords" content="" />
<meta name="description" content="" />
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=G-4YWFRTFCTT"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'G-4YWFRTFCTT');
    </script>
</head>

<body class="">
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
<!--Simple Header-->
<div class="simple-header"> <a href="index.html" class="simple-header-logo"><img src="images/fetnet-logo.png" alt="FetnetLogo" class="simple-logo-img"></a> </div>
<!--Simple Header End-->
<div class="main">
  <section class="form-area ribbon-bg">
    <div class="container-inside">
      <div class="area-title">
        <div class="fui-container forcont nop">
          <div class="fui-cart-container">
            <div class="column">
              <h2>資料確認</h2>
            </div>
            <div class="column">
              <button class="back-home"><span class="text">上一步</span></button>
            </div>
          </div>
        </div>
      </div>
      <div class="form-list-group">
        <div class="grid-area form-group-gap">
          <div class="half-grid">
            <div class="light-list-area">
              <div class="light-list-area-inner">
                <div class="light-list-area-line1">
                  <div class="line1-left"></div>
                  <div class="line1-right">
                    <button class="button-edit" id="EditOrder"><span class="text">修改</span></button>
                  </div>
                </div>
                <div class="light-list-area-line2">
                  <table width="100%" border="0">
                      <%=OrderInfo %>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
        
      </div>
        
      <div class="total-area">
        <h4>金額計算</h4>
        <div class="fui-container allwidth forcont nop">
          <div class="fui-cart-container nopadding">
            <div class="column"> 總交易金額 </div>
            <div class="column"> <%=Total %> </div>
          </div>
          <div class="fui-cart-container nopadding">
            <div class="column"> 實際付款金額 </div>
            <div class="column"> <%=Total %> </div>
          </div>
          <div class="fui-cart-container nopadding all-money-area">
            <div class="column"></div>
            <div class="column all-money"><span class="all-money-txt">結帳金額</span> <span id="Cost" class="Cost"><%=Total %></span> </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</div>
<div class="steps-bar">
  <div class="steps-bar-container">
    <div class="steps-bar-preview">
      <div class="fui-container">
        <div class="fui-cart-container">
          <div class="column">
            <div class="steps-area">
              <div class="step-item"><span class="text">輸入資料</span></div>
              <div class="step-item step-active"><span class="text">付款</span></div>
            </div>
          </div>
          <div class="column">
            <div class="total-area-bar">
              <p><span>結帳金額</span>$ <%=Total %></p>
            </div>
            <div class="button-area">
              <button class="button-submit" id="mobile_fet_pay" runat="server"><span class="text">遠傳門號付款</span></button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<!--Simple Footer-->
<div class="simple-footer">
  <div class="simple-footer-container">
    <div class="simple-footer-logo"> <a href="index.html"><img src="images/fetnet-footer-logo.png" alt="FetnetLogo" class="simple-logo-img"></a></div>
    <div class="simple-footer-info">遠傳電信版權所有 © Far EasTone<span class="simple-footer-txt">Telecommunications Co., Ltd</span></div>
  </div>
</div>
<!--Simple Footer End--> 

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script> 
<script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.js"></script> 
<script> 
    $(".slider-area").slick({
        dots: false,
        infinite: false,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1025,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,

                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });
    $(document).ready(function () {
        $('input[type=radio][name=abroad]').change(function () {
            if (this.value == '國外') {
                $(".custom-area2").show();
                $(".custom-area").hide();
            }
            else {
                $(".custom-area2").hide();
                $(".custom-area").show();
            }
        });
    });

</script>
    
<!-----付款按鈕----->
<script>
    //修改資料
    $("#EditOrder").on("click", function () {
        var getUrlString = location.href;
        var url = new URL(getUrlString);

        switch (url.searchParams.get('kind')) {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                switch (url.searchParams.get('a')) {
                    case "6":
                        window.location = 'https://bobibobi.tw/Fet/Supplies/SuppliesIndex.aspx' + window.location.search;
                        break;
                }
                break;
        }
    })

    //上一步
    $(".back-home").on("click", function () {
        var getUrlString = location.href;
        var url = new URL(getUrlString);

        switch (url.searchParams.get('kind')) {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                switch (url.searchParams.get('a')) {
                    case "6":
                        window.location = 'https://bobibobi.tw/Fet/Supplies/SuppliesIndex.aspx' + window.location.search;
                        break;
                }
                break;
        }
    })

    //遠傳手機門號付款
    $("#mobile_fet_pay").on("click", function () {
        //付款串接放這裡
        nextStep('FetCSP');
    })

    //前往付款
    function nextStep(ChargeType) {
        var getUrlString = location.href;
        var url = new URL(getUrlString);
        var kind = url.searchParams.get('kind');
        var a = url.searchParams.get('a');

        data = {
            AppMobile: $("#AppMobile").text(),
            Total: $("#Cost").text(),
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
            if (res.Timeover == 1) {
                alert("此申請人付款時間已超時20分鐘，請重新申請。");
                location = res.backURL;
            }
            else {
                alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
            }
        }
    }

</script>
</body>
</html>
