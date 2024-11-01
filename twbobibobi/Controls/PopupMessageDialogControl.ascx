<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupMessageDialogControl.ascx.cs" Inherits="EChatAdmin.Controls.PopupMessageDialogControl" %>
<div id="dialog-message" title="Download complete" style="display: none;">
  <p>
    <span class="ui-icon ui-icon-info" style="float:left; margin:0 7px 50px 0;"></span>
    <span class="dlg_msg"></span>
  </p>
</div>
<script>
    function msgbox_show(msg, alertDismissed) {
        $("#dialog-message").find(".dlg_msg").text(msg);
        $("#dialog-message").dialog({
            draggable: false,
            modal: true,
            title: "系統提示",
            buttons: {
                "確 定": function () {
                    if (alertDismissed) alertDismissed();
                    $(this).dialog("close");
                }
            }
        });

    }

    function msgbox_show_with_output(msg, alertDismissed, customButtom_Handler) {
        $("#dialog-message").find(".dlg_msg").text(msg);
        $("#dialog-message").dialog({
            draggable: false,
            modal: true,
            title: "系統提示",
            buttons: {
                "導出預付卡記錄": function () {
                    $(this).dialog("close");

                    if (customButtom_Handler)
                        customButtom_Handler();

                },
                "確 定": function () {
                    if (alertDismissed)
                        alertDismissed();
                    $(this).dialog("close");
                }
            }
        });

    }

    $(function () {
        var PopupMessage = "<%#PopupMessage %>";
        var RedirectUrl = "<%#RedirectUrl %>";

        if (PopupMessage.length > 0) {
            msgbox_show(PopupMessage, function () {
                if (RedirectUrl.length > 0) {
                    location.href = RedirectUrl;
                }
            });
        }
    });
  </script>
