<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightsComplete.aspx.cs" Inherits="Temple.FET.Lights.LightsComplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="shortcut icon" href="favicon.ico" />
<meta content="minimum-scale=1,initial-scale=1,width=device-width,shrink-to-fit=no" name="viewport" />
<link href="https://fonts.googleapis.com/css?family=Noto+Sans+TC:wght@400;500;600;700&family=Roboto:300,400,500,600,700&amp;display=swap" rel="stylesheet" />
<title>遠傳祈福點燈服務</title>
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
<!--Simple Header-->
<div class="simple-header"> <a href="index.html" class="simple-header-logo"><img src="images/fetnet-logo.png" alt="FetnetLogo" class="simple-logo-img"></a> </div>
<!--Simple Header End-->
<div class="main">
  <section class="form-area ribbon-bg success-page">
    <div class="container-inside">
      <div class="success-pay">
        <div class="success-pay-pic"><img src="images/completed@2x.png"></div>
        <div class="success-pay-title">您已成功完成付款</div>
        <div class="success-pay-script">
          <p>將提供資料給廟方進行祈福作業<br>
            依廟方作業完成祈福及相關祈福內容寄送</p>
          <p>相關祈福作業問題可Email<br>
            dcb@fareastone.com.tw 確認</p>
        </div>
        <div class="success-pay-button">
          <button class="button-red" id="BackHome"><span class="text">返回首頁</span></button>
        </div>
      </div>
    </div>
  </section>
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
<script>
    //返回首頁
    $("#BackHome").on("click", function () {
        window.location = 'https://bobibobi.tw/FET/Lights/LightsIndex_Luer.aspx';
        //window.location = 'https://localhost:44329/FET/Lights/LightsIndex.aspx';
    });
</script>
</body>
</html>
