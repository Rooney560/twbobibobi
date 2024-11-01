<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxClientControl.ascx.cs" Inherits="EChatAdmin.Controls.AjaxClientControl" %>
<style>
  .ajaxButton
  {
  }
   .ajaxSelect
  {
  }
  .styleButton
  {
  }
  
  .validateTips { border: 1px solid transparent; padding: 0.3em;height: 30px; }
    .alertTips { border: 1px solid transparent; padding: 0.3em;height: 15px; }
    .ui-state-error-ex {
	    border: 1px solid #cd0a0a;
	    background: #fef1ec;
	    color: #cd0a0a;
        padding: 0.3em; 
    }
  </style>
<script language="javascript">

    var objSelectData = new Array(); //数组

    function AddSelect(parentID, parentValue, childValue, childText) {

        var objSelectObject = new Object();

        objSelectObject.ParentID = parentID;
        objSelectObject.ParentValue = parentValue;
        objSelectObject.ChildValue = childValue;
        objSelectObject.ChildText = childText;
        objSelectData.push(objSelectObject);
    }

    function ReloadChildList(parentID, childID, parentValue) {

        $("#" + childID).empty();

        $.each(objSelectData, function (i, objSelectObject) {
            if (objSelectObject.ParentID == parentID && objSelectObject.ParentValue == parentValue) {
                $("#" + childID).append("<option value='" + objSelectObject.ChildValue + "'>" + objSelectObject.ChildText + "</option>");
            }
        });
    }

    var PreSubmitHandler = null;
    var hasTextArea = false;
    $(function () {

        $(".ajaxSelect").each(function (i) {
            var parentSelectID = $(this).attr("id");

            $("#" + $(this).attr("ChildList") + " option").each(function (i) {

                var selectValue = "" + $(this).val();
                var selectValues = selectValue.split(",");
                AddSelect(parentSelectID, selectValues[0], selectValues[1], $(this).text());
            });


            ReloadChildList(parentSelectID, $(this).attr("ChildList"), $(this).val());

            $(this).change(function (event) {
                ReloadChildList($(this).attr("id"), $(this).attr("ChildList"), $(this).val());
            });
        });


        $("#accordion").accordion();
        $(".styleButton")
          .button();
        $(".ajaxButton")
          .button()
          .click(function (event) {
              if (PreSubmitHandler) {
                  if (!PreSubmitHandler($(this))) {
                      event.preventDefault();
                      return;
                  }
              }
              $(".ui-state-error-ex").removeClass("ui-state-error-ex");

              //alert($("input[type='text']").val());
              //var sdata = {APMark:APMark,PageIndex:PageIndex,StartDate:StartDate,EndDate:EndDate,PageSize:PageSize};


              var bValid = true;
              var tips = "" + $(this).attr('TipsID');

              tips = tips != null && tips != "undefined" && tips.length > 0 ? $("#" + tips) : null;
              
              var RequestChecker = "" + $(this).attr('RequestChecker');

              if (bValid && RequestChecker != null && RequestChecker != "undefined" && RequestChecker.length > 0) {
                  var Checkers = RequestChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");
                      bValid = bValid && ac_checkRequest($("#" + params[0]), ac_getLabel($("#" + params[0])), tips);
                  }
              }

              var DropChecker = "" + $(this).attr('DropChecker');

              if (bValid && DropChecker != null && DropChecker != "undefined" && DropChecker.length > 0) {
                  var Checkers = DropChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");
                      bValid = bValid && ac_checkDrop($("#" + params[0]), ac_getLabel($("#" + params[0])), tips);
                  }
              }

              var LengthChecker = "" + $(this).attr('LengthChecker');

              if (bValid && LengthChecker != null && LengthChecker != "undefined" && LengthChecker.length > 0) {
                  var Checkers = LengthChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");
                      bValid = bValid && ac_checkLength($("#" + params[0]), ac_getLabel($("#" + params[0])), params[1], params[2], tips);
                  }
              }

              var EqualChecker = "" + $(this).attr('EqualChecker');

              if (bValid && EqualChecker != null && EqualChecker != "undefined" && EqualChecker.length > 0) {
                  var Checkers = EqualChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");

                      bValid = bValid && ac_checkEqual($("#" + params[0]), $("#" + params[1]), ac_getLabel($("#" + params[0])), ac_getLabel($("#" + params[1])), tips);


                  }
              }

              var RegexChecker = "" + $(this).attr('RegexChecker');

              if (bValid && RegexChecker != null && RegexChecker != "undefined" && RegexChecker.length > 0) {
                  var Checkers = RegexChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");

                      var regex = new RegExp(params[1]);
                      bValid = bValid && ac_checkRegexp($("#" + params[0]), regex, ac_getLabel($("#" + params[0])), tips);
                  }
              }

              var CustomChecker = "" + $(this).attr('CustomChecker');

              if (bValid && CustomChecker != null && CustomChecker != "undefined" && CustomChecker.length > 0) {
                  var Checkers = CustomChecker.split("|");

                  for (var i = 0; i < Checkers.length; i++) {
                      var params = Checkers[i].split(",");
                      bValid = bValid && eval(params[1] + "()");
                  }
              }

              var RequestMethod = $(this).attr('RequestMethod');

              if (bValid && RequestMethod != null && RequestMethod != "undefined" && RequestMethod.length > 0) {

                  var handler;

                  var handlerMethod = $(this).attr('ServerID') + "_" + RequestMethod.substring((RequestMethod.indexOf(".") + 1));

                  eval("handler=" + handlerMethod + ";");

                  var dataParams = "RequestPageType=json&RequestMethod=" + RequestMethod;
                  $("input").each(function () {
                      if ($(this).attr("type") == "text" || $(this).attr("type") == "password" || $(this).attr("type") == "hidden"
                      || $(this).attr("type") == "radio" || $(this).attr("type") == "checkbox") {

                          var id = "" + $(this).attr("id");
                          if (id.indexOf("__") == -1) {

                              if (dataParams != "") {
                                  dataParams += "&";
                              }

                              var inputValue = "";
                              if ($(this).attr("type") == "radio" || $(this).attr("type") == "checkbox") {
                                  //alert($(this).prop('checked'));
                                  if ($(this).prop('checked') == undefined || $(this).prop('checked') == false) {
                                      inputValue = false;
                                  }
                                  else {
                                      inputValue = true;
                                  }
                              }
                              else {
                                  inputValue = $(this).val()
                              }
                              dataParams += $(this).attr("id") + "=" + encodeURIComponent(inputValue);
                          }
                      }
                  });

                  $("select").each(function () {
                      if (dataParams != "") {
                          dataParams += "&";
                      }

                      var selectid = "" + $(this).attr("id");

                      if (selectid.lastIndexOf("_") > 0) {
                          selectid = selectid.substring(selectid.lastIndexOf("_") + 1);
                      }
                      dataParams += selectid + "=" + encodeURIComponent($(this).val());
                  });

                  $("textarea").each(function () {
                      hasTextArea = true;
                      if (dataParams != "") {
                          dataParams += "&";
                      }

                      //alert($(this).attr("id") + "=" + encodeURIComponent($(this).val()));
                      dataParams += $(this).attr("id") + "=" + encodeURIComponent($(this).val());
                  });

                  ac_disableButton($(this));
                  ac_get_data(location.href, dataParams, handler);

              }

              event.preventDefault();
          });


    });

    function PrepareAutoSelect(RequestMethod, input, targetName, targetValue) {
        ExecuteAutoSelect(location.href + "?RequestPageType=json&RequestMethod=" + RequestMethod, input, targetName, targetValue);
    }

    function ExecuteAutoSelect(url, sel, targetName, targetValue) {

        // prepare auto select
        $.ajax({
            url: url,
            success: function (ret) {
                $(sel).autocomplete({
                    autoFocus: true,
                    delay: 500,
                    source: ret.DataSource,
                    select: function (event, ui) {
                        $(targetName).val(ui.item.label);
                        $(targetValue).val(ui.item.value);
                        $(sel).val(ui.item.label);
                        return false;
                    }
                }).data("ui-autocomplete")._renderItem = function (ul, item) {
                    return $("<li style='float:none;'>")
                        .append("<a style='width:98%'><b>" + item.label + "</a>")
                        .appendTo(ul);
                };
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });

    }

    function test() {
        //alert("test1");
        return true;
    }

    function ac_loadServerMethod(serverMethod, sdata, handler) {
        var urlParams = "RequestPageType=json&RequestMethod=" + serverMethod;
        var url = location.href;
        if (url.indexOf("?") > 0) {
            url = url + "&" + urlParams;
        }
        else {
            url = url + "?" + urlParams;
        }

        ac_get_data(url, sdata, handler);
    }

    function ac_getLabel(inputObject) {
        var id = inputObject.attr("id");
        var labelName = "";
        $("label").each(function () {
            if ($(this).attr("for") == id) {
                labelName = $(this).text();
            }
        });

        return labelName.replace(":", "").replace("：", "").replace(" ", "");
    }

    function ac_get_data(url, send_data, proc_handler) {
        if (url.indexOf("?") > 0) {
            url = url + "&randomkey=" + parseInt(1000 * Math.random()) + parseInt(1000 * Math.random());
        }
        else {
            url = url + "?randomkey=" + parseInt(1000 * Math.random()) + parseInt(1000 * Math.random());
        }
        postType = "get";
        if (hasTextArea == true) {
            postType = "post";
        }
        $.ajax({
            url: url,
            type: postType,
            data: send_data,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("訪問服務器失敗，請檢查網絡狀態是否有效。錯誤代碼：" + XMLHttpRequest, + "," + textStatus + "," + errorThrown);
                callbackHandler();
            },
            success: function (ret) {
                //alert(ret.status);
                ac_proc_ret(ret, proc_handler);
            }
        });
    }
    function ac_proc_ret(ret, proc_handler) {
        try {
            //res = $.parseJSON(ret); // ret is already JSON from .net

            callbackHandler(ret);

            if (ret.StatusCode == 1 || ret.StatusCode == 0) {
                proc_handler(ret);
            }
        } catch (e) {
            alert(e);
        }
    }

    function callbackHandler(res) {
        $(".ajaxButton").each(function () {
            ac_enableButton($(this));
        });

        var handler;
        try {
            eval("handler=OnValidate;");
        } catch (e) {
            handler = null;
        }

        if (handler) {
            handler(res);
        }
    }


    //-----UI Function-----------------
    function ac_checkDrop(o, n, tips, ui_handler) {
        if (o.val() == "-1") {
            o.addClass("ui-state-error-ex");
            o.focus();
            if (ui_handler)
                ui_handler();
            else
                ac_updateTips(n + " 下拉選單未選，請選擇有效項目。", tips);
            return false;
        } else {
            return true;
        }
    }

    function ac_checkLength(o, n, min, max, tips, ui_handler) {

        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error-ex");
            o.focus();
            if (ui_handler) {
                ui_handler();
            }
            else {
                ac_updateTips(n + "的長度必須是 " +
          min + " 至 " + max + "。", tips);
            }
            return false;
        } else {
            return true;
        }
    }
    function ac_checkRequest(o, n, tips, ui_handler) {
        if (o.val().length == 0) {
            o.addClass("ui-state-error-ex");
            o.focus();
            if (ui_handler)
                ui_handler();
            else
                ac_updateTips(n + "不能為空，請輸入或者選擇有效項目。", tips);
            return false;
        } else {
            return true;
        }
    }

    function ac_checkEqual(o, v, n, m, tips, ui_handler) {

        if (o.val() != v.val()) {
            o.addClass("ui-state-error-ex");
            o.focus();
            if (ui_handler)
                ui_handler();
            else {
                ac_updateTips(n + "與" + m + "的內容不相同，請重新輸入。", tips);
            }
            return false;
        } else {
            return true;
        }
    }
    function ac_checkRegexp(o, regexp, n, tips, ui_handler) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error-ex");
            o.focus();
            if (ui_handler)
                ui_handler();
            else
                ac_updateTips(n + "的格式不正確,請重新輸入。", tips);
            return false;
        } else {
            return true;
        }
    }

    function ac_alertTips(msg, input, tips, tipsOff) {
        if (input) {
            input.addClass("ui-state-error-ex");
            input.focus();
        }
        if (tipsOff != 1) {
            ac_updateTips(msg, tips);
        }
    }

    var tips_text = "";
    function ac_updateTips(t, o) {
        if (o) {
            tips = o;
        }
        else {
            tips = $(".validateTips");
        }
        tips_text = tips.html();

        tips
    .text(t)
    .addClass("ui-state-highlight");
        /*
        setTimeout(function() {
        tips.removeClass( "ui-state-highlight", 1500 );
        }, 500 );
        */
    }

    function ac_resetTips(o) {
        if (tips_text != "") {
            if (o) {
                tips = o;
            }
            else {
                tips = $(".validateTips");
            }
            tips
    .html(tips_text)
    .removeClass("ui-state-highlight");
        }
    }

    function ac_resetTipsWithText(t, o) {
        if (o) {
            tips = o;
        }
        else {
            tips = $(".validateTips");
        }

        tips
    .html(t)
    .removeClass("ui-state-highlight");

    }

    function ac_disableButton(button) {
        button.addClass("ui-state-disabled");
        button.attr("disabled", "disabled");
    }

    function ac_enableButton(button) {
        button.removeClass("ui-state-disabled");
        button.removeAttr("disabled");
    } 

 
</script>