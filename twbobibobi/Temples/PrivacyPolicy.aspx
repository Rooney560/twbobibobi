<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivacyPolicy.aspx.cs" Inherits="twbobibobi.Temples.PrivacyPolicy" %>

<%@ Register src="~/Temples/footer.ascx" tagprefix="uc1" tagname="footer" %>
<%@ Register src="~/Temples/header.ascx" tagprefix="uc2" tagname="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta property="og:title" content="隱私權政策|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:url" content="https://bobibobi.tw/Temples/PrivacyPolicy.aspx" />
    <!--網址：請補上網址-->
    <meta name="description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:description" content="世代信仰，數位傳承 - 與全臺知名宮廟合作，提供宮廟服務線上報名，讓您在忙碌之餘也可以透過線上報名的方式,參與宮廟的服務。" />
    <!--簡介-->
    <meta property="og:site_name" content="隱私權政策|【保必保庇】線上宮廟服務平台" />
    <!--標題-->
    <meta property="og:type" content="website" />

    <!--抓取圖片-->
    <meta property="og:image" content="images/fb.jpg" />
    <meta name="twitter:image:src" content="images/fb.jpg" />
    <link rel="image_src" href="images/fb.jpg" />


    <link rel="shortcut icon" href="images/favicon.png" />
    <link href="images/favicon.png" rel="apple-touch-icon" sizes="192x192" />


    <title>隱私權政策|【保必保庇】線上宮廟服務平台</title>
    <!--標題-->

    <!--資源項目-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

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
        <article id="Service" class="page">
            <!--本頁路徑-->
            <nav class="breadcrumb">
                <div class="Here">目前位置：</div>
                <ul>
                    <li><a href="../index.aspx" title="首頁">首頁</a></li>
                    <li>隱私權政策</li>
                </ul>
            </nav>

            <!--本頁內容-->
            <section>
                <div class="Content">
                    <div style="text-align: center;">
                        <h2>隱私權政策</h2>
                    </div>
                    <div class="">
                        <br />
                        <h2>新薪網元股份有限公司(以下稱「新薪網元」)絕對尊重用戶的隱私權，為使用戶瞭解新薪網元 (下稱：本公司)如何蒐集、應用及保護您所提供的個人資訊，
                            請詳閱下列之隱私權保護政策(Privacy Policy; 以下稱本政策)。</h2>
                        <br />

                        <h2>一、適用範圍</h2>
                        <h1>本政策適用範圍包括:</h1>
                        <p>1.	本公司為整體營運蒐集、處理及利用您因使用本公司的網站/應用程式 (以下簡稱「APP」)、服務、參與活動、申請加入本公司會員及申辦服務等所提供
                            的個人資料及其他資訊。</p>
                        <p>2.	本公司的供應商或合作夥伴 (包括但不限於商業組織、政府機關等) 或關係企業受本公司委託或與本公司合作時，如涉及蒐集、處理或利用個人資料者，
                            本公司將依本政策規定辦理。</p>
                        
                        <h2>二、資料之蒐集</h2>
                        <h1>當您申請、使用本公司資訊服務時，本公司所蒐集的資料，蒐集及利用目的請參見下表。蒐集對象包括但不限於客戶本人（或其代表人）及法定代理人等自然
                            人或公司之相關必要資料。其他內容詳見下列告知事項：</h1>
                        <h3>■ 個資蒐集告知事項</h3>
                        <h1>新薪網元(下稱”本公司”)，因經營宮廟服務而蒐集、處理或利用之個人資料時，皆以尊重您的權益為基礎，並以誠實信用之方式及以下原則為之：</h1>
                        <h3>■ 本公司蒐集您個人資料之目的：</h3>
                        
                        <table cellspacing="1" cellpadding="0" style="width:80%; border-spacing:0.5pt">
                            <tbody>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">040 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">行銷</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">063 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">非公務機關依法定義務所進行個人</span>
                                            <br>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">資料之蒐集處理及利用</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">067 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">信用卡、現金卡、轉帳卡或電子</span>
                                            <br>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">票證</span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold"> </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">業務</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">069 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">契約、類似契約或其他法律關係</span>
                                            <br>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">事務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">072 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">政令宣導</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">081 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">個人資料之合法交易業務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">085 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">旅外國人急難救助</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">090 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">消費者、客戶管理與服務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">091 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">消費者保護</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">104 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">帳務管理及債權交易業務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">127 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">募款（包含公益勸募）</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">129 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">會計與相關服務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">133 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">經營電信業務與電信加值網路業務</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">135 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">資（通）訊服務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">136 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">資（通）訊與資料庫管理</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">137 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">資（通）訊安全管理</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">148 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">網路購物及其他電子商務服務</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">152 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">廣告或商業行為管理</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">153 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">影視、音樂與媒體</span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">管理</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">154 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">徵信</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">157 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">調查、統計與研究分析</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#ffffff">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">181 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">其他經營合於營業登記項目或組</span>
                                            <br>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">織章程所定之業務</span>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height:39pt">
                                    <td style="width:63pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos; font-weight:bold">182 </span>
                                            <span style="font-family:&#39;等线&#39;; font-weight:bold">其他諮詢與顧問服務</span>
                                        </p>
                                    </td>
                                    <td style="width:225pt; border:1.5pt solid #e8ecf0; padding:2.25pt 5.25pt; vertical-align:middle; background-color:#f1f3f5">
                                        <p style="margin-top:0pt; margin-bottom:8pt; line-height:116%; widows:0; orphans:0; font-size:12pt">
                                            <span style="font-family:Aptos">&nbsp;</span>
                                        </p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>■ 客戶資料蒐集方式</h3>
                        <h1>本公司僅會請您提供主管機關公告之個人資料類別，屬電信業務或電信加值網路業務之執行(包含加值服務、各項優惠措施、行銷、活動訊息、市場調查、帳務
                            處理、欠費催繳或其它電信相關作業等事項) 必要之個人資料，包含但不限載於各項申請文件之客戶本人(或其代表人) 及法定代理人之姓名、身分證字號、其
                            他足資辨識身分之證明文件、住址、聯絡電話、電子信箱、出生年月日及其它相關必要資料、或蒐集其它因使用服務所產生之資料（包含但不限於：位置、網頁
                            瀏覽紀錄、裝置資訊）。</h1>
                        <h1>您的個人資料的處理、利用之期間、地區、對象及方式：</h1>
                        <h3>■ 期間: 資料收集目的之存續期間、本公司因執行業務所需的保存期間、或依相關法令規範所訂之保存年限(包括但不限於民法、電信管理法、電信相關業務管
                            理規則等)。</h3>
                        <h3>■ 地區：中華民國境內及境外地區（主管機關禁止者不在此限）。</h3>
                        <h3>■ 對象：本公司、本公司委託或有合作、業務往來關係之關係企業及合作廠商。</h3>
                        <h3>■ 個人資料利用方式：以必要的方式利用您個人資料，例如</h3>
                        <p>•	為您建立客戶／會員檔案以便我們管理帳戶。</p>
                        <p>•	為提供客戶服務而與您聯繫（包含寄送商品、向您通知與您申辦之服務有關的資訊，或回覆您的提問、申訴）。</p>
                        <p>•	寄送帳務訊息至您的地址或電子郵件信箱；藉由電話聯繫或簡訊通知您有關帳務資訊。</p>
                        <p>•	以您的各項聯繫方式向您提供行銷資訊，例如優惠方案、促銷活動、本公司的其他服務，以及其他更適合您的商品／服務。</p>
                        <p>•	分析您的個人資料以維持、保護、開發及增進本公司的服務，或依分析結果向您推薦我們所提供的其他您可能有興趣或適合您的商品／服務。</p>
                        <p>•	分析您的個人資料，並以統計數據、趨勢或其他無法識別您的身分之形式產出結果，對外提供給我們的企業客戶（例如讓企業客戶瞭解他們的門市熱點、消費
                            者年齡／性別分布、消費者停駐時間等）。</p>
                        <p>•	將您的個人資料提供給受本公司委託的第三人（例如行銷／分析／調查／廣告／公關業者、物流業者、金流業者、資訊服務業者等），在受委託的範圍內協助
                            我們達成蒐集目的。我們會對受委託的第三人執行必要的監督，以確保您的個人資料安全。</p>
                        <p>•	違約行為的預防、調查與權利使用。</p>
                        <h3>■ 個人資料使用權利：</h3>
                        <h1>本公司保有您的個人資料時，您可以依個人資料保護法規定行使以下權利：</h1>
                        <p>1.	查詢、閱覽您的個人資料</p>
                        <p>2.	製給複製本，或補充更正您的個人資料</p>
                        <p>3.	請求停止蒐集、處理或利用您的個人資料</p>
                        <p>4.	請求刪除您的個人資料</p>
                        <h1>為保障您的個人資料安全，行使前揭權利時，須由本人向本公司客戶服務聯繫窗口檢具身分證明文件提出申請並填具本公司作業申請表。相關申請資料提供之方式
                            、處理期限、查詢費用等事項，均依法令、本公司營業規章及服務契約相關規定辦理。本公司得依個人資料保護法第10條、第11條規定，執行業務所必須及法定保
                            存期間等考量，核決是否接受您的申請</h1>
                        <h1>僅在您選擇同意提供個人相關資料的情況下，本公司才會針對個人資料進行蒐集、處理與利用，您可以自由選擇是否提供您的個人資料。惟若您選擇不提供，將影
                            響服務申辦或其完整性。</h1>
                        <br />
                        <h2>三、 資料之使用</h2>
                        <h3>■ 與第三方分享資料之原則</h3>
                        <h1>本公司對於您的個人資料，將依蒐集時所闡述之特定目的及相關法令規定之範圍內使用。本公司絕不會在未經您同意之情況下，任意將您的個人資料提供他人。但
                            以下情況，本公司得在符合本政策原則下，與第三人共用您的資料：</h1>
                        <p>•	為了提供其他服務或優惠權益，需要與提供該服務或優惠第三者共用您的資料時，會在活動時提供充分說明，您可以自由選擇是否接受這項特定服務或優惠。</p>
                        <p>•	司法單位/政府機關因公眾安全依法要求本公司提供特定個人資料時，本公司將依司法單位/政府機關合法正式的程序，以及對本公司所有使用者安全考量下做
                            可能必要的配合。</p>
                        <h2>四、 資料之保護</h2>
                        <h3>■ 資料風險管理與責任</h3>
                        <h1>本公司為確保所蒐集、處理或利用之用戶資料安全，於網路、系統、作業程序等均以嚴密的管控措施加以保護。並將資訊安全風險納入本公司風險管理體系，定期
                            召開管理會議，透過不同層級組織與職責的運作，落實風險辨識、分析、管理與報告，並採取必要因應對策。對內部人員亦施予完善的個資保護訓練，同時要求及
                            監督合作之第三方廠商嚴格保密、遵守相關義務與符合相關主管機關之規定。</h1>
                        <h3>■ 第三方稽核</h3>
                        <h1>本公司亦定期執行內外部稽核，透過第三方驗證機構以ISO 27001資訊安全管理與BS 10012個人資訊管理之國際標準進行認證，持續檢視強化保護機制。</h1>
                        <br />
                        <h2>五、 資安之處理機制</h2>
                        <h3>■ 客戶申訴管道</h3>
                        <h1>本公司設有免付費客戶服務專線(04-36092299)，受理客戶對個人資料之相關處理需求、申訴與諮詢。</h1>
                        <h3>■ 事件調查與處理</h3>
                        <h1>針對個資事件，本公司一向採取「零容忍」原則，如發生個人資料遭侵害事件，權責單位包含資安、法務、客服部門將依個人資料保護法及本公司之事件通報處理
                            程序與相關規範調查處理。如有違反保密義務者，將受相關法律及本公司內部規定之處分(包括但不限於終止合作)。</h1>
                        <br />
                        <h2>六、 網站使用安全說明</h2>
                        <h3>■ 使用者身份驗證與安全管控</h3>
                        <h1>本公司網站會員登入機制採取SSO (Single Sign On) 機制，由本公司內部資料庫及主機做會員驗證及認證動作。非會員無法使用及登入任何服務。加入會員均須
                            填寫相關資料及簡訊授權開通動作。</h1>
                        <h1>與客戶或交易有關的頁面均採用https加密方式進行，防制任何資料竊取行為。本公司網路均有多層防火牆，可實質保護主機安全。</h1>
                        <h3>■ 使用者注意事項</h3>
                        <h1>當您在登入頁面勾選了「保持登入」並且登入以後，系統會將登入資料加密儲存在您的電腦中，只要您沒有清除cookie，沒有主動選擇登出，並且使用同一台電腦
                            ，在期限內使用網站，進入需要登入的服務頁面時，系統會自動將您的登入資料送回主機進行認證，讓您保持自動登入，方便使用各項服務。</h1>
                        <h1>請妥善保管您的密碼及或任何個人資料，不要將任何個人資料，尤其是密碼提供給任何人。在您完成購物、取閱電子郵件等程序使用後，務必記得登出帳戶，若您
                            是與他人共享電腦或使用公共電腦，切記要關閉瀏覽器視窗，以防止他人讀取您的個人資料或信件。</h1>
                        <h1>因此若您使用公用/共用電腦，請不要勾選「保持登入」，並且記得每次離開時點選登出鍵，保護您的資訊安全。</h1>
                        <br />
                        <h2>七、 Cookie之運用</h2>
                        <h1>為了讓系統能夠辨識用戶的資料，我們的網站或應用程式伺服器將會紀錄Cookie資訊，系統將以Cookie方式記錄用戶行為。伺服器會將所需的資訊經由瀏覽器寫入
                            用戶的硬碟中，下次瀏覽器在要求伺服器傳回網頁時，會將Cookie的資料先傳給伺服器，伺服器可依據Cookie的資料判斷使用者，網頁伺服器可針對用戶之不同喜
                            好來執行不同動作或傳回特定的資訊。</h1>
                        <h1>本公司會在下列情況下，在您的電腦中寫入或讀取Cookies資料：</h1>
                        <p>•	為提供個別化的服務，方便用戶使用。</p>
                        <p>•	統計分析瀏覽模式，以改善服務。</p>
                        <br />
                        <h2>八、 隱私權保護政策的修正</h2>
                        <h1>本公司隱私權保護政策將因應科技發展趨勢、相關法規之修訂或其他環境變遷等因素而為適當之修改，以落實保障使用者隱私權之立意。該修改過之條款經總經理
                            核准後實施，並立即刊登於網站上。倘若您於修改後繼續使用本公司服務時，視為已閱讀、瞭解並同意接受修改後之內容。</h1>
                        <p>謝謝您閱讀 新薪網元科技股份有限公司 的隱私權政策，如有任何問題，請隨時聯繫 新薪網元科技股份有限公司 。</p>
                    </div>
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

