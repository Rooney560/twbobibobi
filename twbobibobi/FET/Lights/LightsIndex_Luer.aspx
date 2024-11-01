<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightsIndex_Luer.aspx.cs" Inherits="Temple.FET.Lights.LightsIndex_Luer" %>

<%@ Register src="~/Controls/AjaxClientControl.ascx" tagname="AjaxClientControl" tagprefix="uc4" %>

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
    <uc4:AjaxClientControl ID="AjaxClientControl1" runat="server" />
<!--Simple Header-->
<div class="simple-header"> <a href="index.html" class="simple-header-logo"><img src="images/fetnet-logo.png" alt="FetnetLogo" class="simple-logo-img"></a> </div>
<!--Simple Header End-->
<div class="main">
  <section class="kv">
    <div class="inside-container">
      <h1>遠傳祈福點燈服務<br /><鹿耳門聖母廟財利燈></h1>
      <div class="kv-script">
        <p>活動說明</p>
        <ul>
          <li>1.此為「遠傳帳單代收 好禮龍厚哩」活動優惠</li>
          <li>2.用戶請在2024/4/12前於本頁面使用遠傳帳單代收訂購鹿耳門聖母廟財利燈，<br />
              訂購完成後，系統將於2024/5/31前於所出帳電信帳單折抵訂購金額600元，<br />
              未依上述時間完成訂購者，恕不享有折抵優惠</li>
          <li>3.限收到本活動簡訊者符合優惠資格，經轉發者恕不適用</li>
        </ul>
        <p>使用說明</p>
        <ul>
          <li>1.請於下方填寫購買者與被祈福者完整資料</li>
          <li>2.點燈資訊將於資料填寫後提供給廟方進行祈福作業</li>
          <li>3.點燈時間 : 將依廟方作業進行</li>
          <li>4.客服聯絡 : tp@fareastone.com.tw 將盡快於一週內回覆</li>
        </ul>
      </div>
    </div>
  </section>
  <section class="form-area ribbon-bg">
    <div class="container-inside">
      <div class="area-title">
        <h2>購買者資料</h2>
      </div>
      <div class="form-list-group">
        <div class="">
          <div class="grid-area form-group-gap">
            <div class="long-grid">
              <div class="form-group">
                <label for="input-buyer" class="">購買者</label>
                <div class="text-input">
                  <input id="member_name" name="member_name" type="text" placeholder="請輸入姓名" aria-label="請輸入姓名" maxlength="200" value="" />
                </div>
              </div>
            </div>
            <div class="long-grid">
              <div class="form-group">
                <label for="input-buyer-mobile" class="">購買者手機門號</label>
                <div class="text-input">
                  <input id="member_tel" name="member_tel" type="text" placeholder="請輸入手機號碼" aria-label="請輸入手機號碼" maxlength="200" value="" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>          
    <div class="container-inside">
      <div class="area-title">
        <h2>被祈福者資料填寫</h2>
      </div>
      <div class="form-list-group">
        <div class="people">
          <div class="people-area">
            <p class="area-title-script mb10">選擇服務項目</p>
            <div class="checkbox-group slider-area">
              <label>
              <input type="checkbox" name="Group1" value="財利燈 600元/月" id="Group1_0" onclick="return  false;" checked="checked" />
              <div class="radio-content">
                <div class="radio-content-inner">
                  <div class="radio-content-txt">   財利燈 600元/月</div>
                </div>
              </div>
              </label>
            </div>
            <div class="page2">
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者姓名</label>
                    <div class="text-input">
                      <input id="bless_name_1" name="bless_name_1" type="text" placeholder="請輸入負責人姓名or公司名"class="required" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
                <div class="long-grid">
                  <div class="form-group">
                    <label for="sex" class="">性別</label>
                    <div class="text-input">
                      <div class="radio-group2">
                        <label>
                        <input name="sex" type="radio" id="Group2_0" value="善男" checked>
                        <div class="radio-content">
                          <div class="radio-content-inner">
                            <div class="radio-content-txt">善男</div>
                          </div>
                        </div>
                        </label>
                        <label>
                        <input type="radio" name="sex" value="信女" id="Group2_1">
                        <div class="radio-content">
                          <div class="radio-content-inner">
                            <div class="radio-content-txt">信女</div>
                          </div>
                        </div>
                        </label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">被祈福者電話</label>
                    <div class="text-input">
                      <input id="bless_tel_1" name="bless_tel_1" type="text" placeholder="請輸入祈福人聯絡電話"class="required" aria-label="請輸入姓名" maxlength="200" value="" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group-gap mgap30">
                <div class="long-grid gap10">
                  <div class="form-group">
                    <label for="birthday" class="mb10">被祈福者農曆生日 (民國年)</label>
                  </div>
                </div>
                <div class="form-group calendar">
                  <div class="text-input">
                    <div class="grid-area">
                      <div class="short-grid">
                        <input id="input-birthday-year" type="text" placeholder="民國年" class="required" aria-label="民國年" maxlength="200" value="" />
                      </div>
                      <div class="short-grid">
                        <select id="input-birthday-month">
                          <option>1</option>
                          <option>2</option>
                          <option>3</option>
                          <option>4</option>
                          <option>5</option>
                          <option>6</option>
                          <option>7</option>
                          <option>8</option>
                          <option>9</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                        </select>
                      </div>
                      <div class="short-grid">
                        <select id="input-birthday-day">
                          <option>1</option>
                          <option>2</option>
                          <option>3</option>
                          <option>4</option>
                          <option>5</option>
                          <option>6</option>
                          <option>7</option>
                          <option>8</option>
                          <option>9</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                          <option>24</option>
                          <option>25</option>
                          <option>26</option>
                          <option>27</option>
                          <option>28</option>
                          <option>29</option>
                          <option>30</option>
                          <option>31</option>
                        </select>
                      </div>
                    </div>
                  </div>
                </div>
                <p class="sp-txt">※ 民國年 = 西元年 - 1911 </p>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">閏月</label>
                      <div class="long-grid">
                          <select name="bless_leapMonth_1" class="" id="bless_leapMonth_1">
                              <option value="N">非閏月</option>

                              <option value="Y">閏月</option>
                          </select>
                      </div>
                  </div>
                </div>
              </div>
              <div class="grid-area form-group-gap">
                <div class="long-grid">
                  <div class="form-group">
                    <label for="input-name" class="">農曆時辰</label>
                      <div class="long-grid">
                        <select id="input-birthday-time">
                            <option>吉</option>

                            <option value="子">子(23:00-01:00)</option>

                            <option value="丑">丑(01:00-03:00)</option>

                            <option value="寅">寅(03:00-05:00)</option>

                            <option value="卯">卯(05:00-07:00)</option>

                            <option value="辰">辰(07:00-09:00)</option>

                            <option value="巳">巳(09:00-11:00)</option>

                            <option value="午">午(11:00-13:00)</option>

                            <option value="未">未(13:00-15:00)</option>

                            <option value="申">申(15:00-17:00)</option>

                            <option value="酉">酉(17:00-19:00)</option>

                            <option value="戌">戌(19:00-21:00)</option>

                            <option value="亥">亥(21:00-23:00)</option>
                        </select>
                      </div>
                  </div>
                </div>
              </div>
              <div class="form-group-gap mgap20">
                <div class="long-grid gap10">
                  <div class="form-group">
                    <label for="abroad" class="mb10">被祈福者居住地址</label>
                  </div>
                </div>
                <div class="custom-area" style="display:block;">
                  <div class="form-group">
                    <div class="text-input">
                        <div class="grid-area CusAddress">
                            <div data-role="zipcode" data-style="addr-zip" data-placeholder="" data-name="bless_zipcode_1" data-id="bless_zipcode_1"></div>
                            <div data-role="county" data-style="addr-county required" data-name="bless_county_1" data-id="bless_county_1"></div>
                            <div data-role="district" data-style="addr-district required" data-name="bless_district_1" data-id="bless_district_1"></div>
                            <div class="long-grid">
                                <input id="bless_address_1" name="bless_address_1" type="text" placeholder="請輸入地址" aria-label="請輸入地址" maxlength="200" value="" class="mw">
                            </div>
                        </div>
                    </div>
                  </div>
                </div>
              </div>
                
                        <div class="Notice">
                            <!--警告說明-->
                        </div>

            </div>
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
              <div class="step-item step-active"><span class="text">輸入資料</span></div>
              <div class="step-item"><span class="text">付款</span></div>
            </div>
          </div>
          <div class="column">
            <div class="button-area">
              <button class="button-submit subBtn" id="subBtn"><span class="text">下一步</span></button>
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

    
<script type="text/javascript" src="js/jquery-ui.js"></script>
<link href="css/jquery-ui.css" rel="stylesheet" type="text/css"/>
<link href="css/jquery-ui.theme.css" rel="stylesheet" type="text/css"/>
<!-----顯示選單----->
<script>
    var aid = '<%=aid %>';
    var a = '<%=a %>';
    $(function () {
        if (aid != 0) {
            ac_loadServerMethod("editinfo", null, editinfo);
        }
    })
</script>
<!-----縣市外掛----->
<!--<script type="text/javascript" src="js/twzipcode.js"></script>-->
<script type="text/javascript" src="js/jquery.twzipcode.min.js"></script>
<script>
    $('.CusAddress').twzipcode({
        'css': [
            'addr-county', //縣市
            'addr-distrcit',  // 鄉鎮市區
            'addr-zip' // 郵遞區號
        ],
        'readonly': true
    });
</script>
    
<!-----必填欄位檢查----->
<script>
    var regex = "^民國\\d{2,3}年(0?[1-9]|1[012])月(0?[1-9]|[12][0-9]|3[01])日$";  // 民國日期格式
    $("#subBtn").on("click", function () {
        var isValid = true;
        var isBirth = true;

        // 遍歷每個必填欄位
        $('.required').each(function () {
            var value = $(this).val();
            var text = this;
            if (value === '' || value === null) {
                isValid = false;
                $(this).addClass('unfilled');
            } else if (value != '' && $(this).hasClass('unfilled')) {
                $(this).removeClass('unfilled');
            }
        });

        var today = new Date();
        var ss = $("#input-birthday-year").val();
        var lyear = (today.getFullYear() - 1911)
        if ($("#input-birthday-year").val() == "" || ss > lyear) {
            isValid = false;
            isBirth = false;
            $(this).addClass('unfilled');
        }
        else {
            $(this).removeClass('unfilled');
        }


        if (isValid) {
            // 所有欄位都已填寫
            console.log('所有欄位都已填寫');

            gotoChecked_Luer();
        } else {
            // 在這裡可以進行表單提交或其他相關處理
            // 有欄位未填寫
            if (isBirth) {
                $(".Notice").text("請檢查上方欄位是否都已填寫。");
                $(".Notice").addClass("active");
            }
            else {
                $(".Notice").text("請檢查上方生日欄位格式是否正確。正確格式：民國xxx年");
                $(".Notice").addClass("active");
            }
        }
    })

    //導向確認資料頁面
    function gotochecked(res) {
        // 重導到相關頁面
        if (res.StatusCode == 1) {
            if (res.redirect) {
                window.location = res.redirect;
            }
        } else {
            alert("資料錯誤！請重新再試一次，若還是不行，請洽客服。");
        }
    }



    //更新之前輸入的資料
    function editinfo(res) {
        var index = 1;
        if (res.StatusCode == 1) {
            $("#member_name").val(res.AppName);
            $("#member_tel").val(res.AppMobile);

            if (res.DataSource != null) {
                $.each(res.DataSource, function (i, item) {
                    $("#bless_name_" + index).val(item.Name);
                    $("#bless_tel_" + index).val(item.Mobile);
                    $("#bless_sex_" + index).val(item.Sex);

                    if (item.Sex == "善男") {
                        $("#Group2_0").attr("checked", true);
                    }
                    else {
                        $("#Group2_1").attr("checked", true);
                    }

                    $("#input-birthday-year").val(res.year);
                    $("#input-birthday-month").val(res.month);
                    $("#input-birthday-day").val(res.day);


                    $("#input-birthday-time").val(item.BirthTime);
                    $("#bless_leapMonth_" + index).val(item.LeapMonth);
                    $("#bless_county_" + index).val(item.County).trigger("change");
                    $("#bless_district_" + index).val(item.dist).trigger("change");
                    $("#bless_address_" + index).val(item.Addr);

                    $("#bless_service_" + index).val(item.SuppliesType).trigger("change");

                    index++;
                });
            }

        }
    }

    $(".OrderForm").on("change", ".unfilled", function () {
        var value = $(this).val();
        if (value != '') {
            $(this).removeClass('unfilled');
        }
    });

    function gotoChecked_Luer() {
        var listcount = 1;

        Appname = $("#member_name").val();      //申請人姓名
        Appmobile = $("#member_tel").val()      //申請人電話

        name_Tag = [];
        mobile_Tag = [];
        sex_Tag = [];
        birth_Tag = [];
        birthTime_Tag = [];
        leapMonth_Tag = [];
        homenum_Tag = [];
        email_Tag = [];
        zipCode_Tag = [];
        county_Tag = [];
        dist_Tag = [];
        addr_Tag = [];
        lightstype_Tag = [];

        for (var i = 1; i <= listcount; i++) {
            name_Tag.push($("#bless_name_" + i).val());                                         //祈福人姓名
            mobile_Tag.push($("#bless_tel_" + i).val());                                        //祈福人電話
            sex_Tag.push($('input:radio[name=sex]:checked').val());                             //祈福人性別

            var birth = "民國" + $("#input-birthday-year").val() + "年" + $("#input-birthday-month").val() + "月" + $("#input-birthday-day").val() + "日";
            birth_Tag.push(birth);                                                              //祈福人農歷生日
            birthTime_Tag.push($("#input-birthday-time").val());                                //祈福人農曆時辰
            leapMonth_Tag.push($("#bless_leapMonth_" + i).val());                               //閏月 Y-閏月 N-非閏月
            zipCode_Tag.push($("#bless_zipcode_" + i).val());                                   //祈福人郵遞區號
            county_Tag.push($("select[name='bless_county_" + i + "']").val());                  //祈福人縣市
            dist_Tag.push($("select[name='bless_district_" + i + "']").val());                  //祈福人區域
            addr_Tag.push($("#bless_address_" + i).val());                                      //祈福人部分地址

            var lightstype = 9;                                                               //服務項目
            lightstype_Tag.push(lightstype);
        }

        data = {
            Appname: Appname,
            Appmobile: Appmobile,
            name_Tag: JSON.stringify(name_Tag),
            mobile_Tag: JSON.stringify(mobile_Tag),
            sex_Tag: JSON.stringify(sex_Tag),
            birth_Tag: JSON.stringify(birth_Tag),
            birthTime_Tag: JSON.stringify(birthTime_Tag),
            leapMonth_Tag: JSON.stringify(leapMonth_Tag),
            homenum_Tag: JSON.stringify(homenum_Tag),
            email_Tag: JSON.stringify(email_Tag),
            zipCode_Tag: JSON.stringify(zipCode_Tag),
            county_Tag: JSON.stringify(county_Tag),
            dist_Tag: JSON.stringify(dist_Tag),
            addr_Tag: JSON.stringify(addr_Tag),
            lightstype_Tag: JSON.stringify(lightstype_Tag),
            listcount: listcount
        };

        ac_loadServerMethod("gotochecked", data, gotochecked);
    }

    function checkEndTime() {
        //var startTime = $("#startTime").val();
        var startTime = new Date();
        var endTime = $("#endTime").text();
        if (Date.parse(endTime).valueOf() < Date.parse(startTime).valueOf()) {
            return false;
        }
        return true;
    }

    function checkedStartTime() {
        //var startTime = $("#startTime").val();
        var endTime = new Date();
        var startTime = $("#startTime").text();
        if (Date.parse(endTime).valueOf() >= Date.parse(startTime).valueOf()) {
            return true;
        }
        return false;
    }
</script>
</body>
</html>
