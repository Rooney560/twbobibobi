<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateOrderTest.aspx.cs" Inherits="twbobibobi.FET.API.CreateOrderTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>API 呼叫設定</h3>
        <label>Environment:</label>
        <asp:RadioButtonList ID="rblEnv" runat="server">
            <asp:ListItem Value="UAT">UAT</asp:ListItem>
            <asp:ListItem Value="Prod" Selected="True">Prod</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <label>API URL:</label>
        <asp:TextBox ID="txtApiUrl" runat="server" Width="400px" Text="https://yourdomain/CreateOrder.aspx"></asp:TextBox>
        <br /><br />
        <h3>訂單內容 (paramContent 組成)</h3>
        <label>Total Amount:</label>
        <asp:TextBox ID="txtTotalAmount" runat="server" Text="1280"></asp:TextBox>
        <br />
        <label>FET Order Number:</label>
        <asp:TextBox ID="txtFetOrderNumber" runat="server" Text="GSSO20250518024975"></asp:TextBox>
        <br /><br />
        <h4>申請人 Applicant</h4>
        <asp:Panel runat="server">
            <label>Name:</label><asp:TextBox ID="txtAppName" runat="server" Text="陳美燕"></asp:TextBox><br />
            <label>Mobile:</label><asp:TextBox ID="txtAppMobile" runat="server" Text="0958377015"></asp:TextBox><br />
            <label>Receipt:</label><asp:TextBox ID="txtAppReceipt" runat="server" Text="陳美燕"></asp:TextBox><br />
            <label>ZipCode:</label><asp:TextBox ID="txtAppZip" runat="server" Text="300"></asp:TextBox><br />
            <label>City:</label><asp:TextBox ID="txtAppCity" runat="server" Text="新竹市"></asp:TextBox><br />
            <label>Region:</label><asp:TextBox ID="txtAppRegion" runat="server" Text="*"></asp:TextBox><br />
            <label>Address:</label><asp:TextBox ID="txtAppAddress" runat="server" Text="大學路13號"></asp:TextBox><br />
            <label>Send(Y/N):</label><asp:TextBox ID="txtAppSend" runat="server" Text="Y"></asp:TextBox><br />
            <label>Email:</label><asp:TextBox ID="txtAppEmail" runat="server" Text="meiyen3003@gmail.com"></asp:TextBox><br />
            <label>Receipt Mobile:</label><asp:TextBox ID="txtAppReceiptMobile" runat="server" Text="0958377015"></asp:TextBox><br />
            <label>Birthday Type(1陽/2陰):</label><asp:TextBox ID="txtAppBirthdayType" runat="server" Text="1"></asp:TextBox><br />
            <label>Birthday(YYYYMMDD):</label><asp:TextBox ID="txtAppBirthday" runat="server" Text="6890109"></asp:TextBox><br />
            <label>Lunar Birthday(code):</label><asp:TextBox ID="txtAppLunarBirthday" runat="server" Text="68914948929"></asp:TextBox><br />
            <label>Lunar Birth Time:</label><asp:TextBox ID="txtAppLunarTime" runat="server" Text="子"></asp:TextBox><br />
            <label>Lunar Leap(N/Y):</label><asp:TextBox ID="txtAppLunarLeap" runat="server" Text="N"></asp:TextBox><br />
        </asp:Panel>
        <br />
        <h4>商品 Items</h4>
        <label>Product Code:</label><asp:TextBox ID="txtProductCode" runat="server" Text="appssp30041"></asp:TextBox><br />
        <label>Qty:</label><asp:TextBox ID="txtQty" runat="server" Text="1"></asp:TextBox><br />
        <label>Unit Price:</label><asp:TextBox ID="txtUnitPrice" runat="server" Text="1280"></asp:TextBox><br />
        <br />
        <h4>祈福人 PrayedPerson (單筆)</h4>
        <asp:Panel runat="server">
            <label>Seq:</label><asp:TextBox ID="txtPrayedSeq" runat="server" Text="42727"></asp:TextBox><br />
            <label>Name:</label><asp:TextBox ID="txtPrayedName" runat="server" Text="陳美燕"></asp:TextBox><br />
            <label>Birthday(YYYYMMDD):</label><asp:TextBox ID="txtPrayedBirthday" runat="server" Text="0690109"></asp:TextBox><br />
            <label>Lunar Birthday:</label><asp:TextBox ID="txtPrayedLunarBirthday" runat="server" Text="0681122"></asp:TextBox><br />
            <label>Leap Month(N/Y):</label><asp:TextBox ID="txtPrayedLeap" runat="server" Text="N"></asp:TextBox><br />
            <label>Oversea (0/1):</label><asp:TextBox ID="txtPrayedOversea" runat="server" Text="1"></asp:TextBox><br />
            <label>ZipCode:</label><asp:TextBox ID="txtPrayedZip" runat="server" Text="300"></asp:TextBox><br />
            <label>City:</label><asp:TextBox ID="txtPrayedCity" runat="server" Text="新竹市"></asp:TextBox><br />
            <label>Region:</label><asp:TextBox ID="txtPrayedRegion" runat="server" Text="*"></asp:TextBox><br />
            <label>Address:</label><asp:TextBox ID="txtPrayedAddress" runat="server" Text="大學路13號"></asp:TextBox><br />
            <label>Birth Time:</label><asp:TextBox ID="txtPrayedTime" runat="server" Text="子"></asp:TextBox><br />
            <label>MSISDN:</label><asp:TextBox ID="txtPrayedMsisdn" runat="server" Text="0958377015"></asp:TextBox><br />
            <label>Offering Qty:</label><asp:TextBox ID="txtPrayedOfferingQty" runat="server" Text="1"></asp:TextBox><br />
        </asp:Panel>
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Encrypt &amp; Send" OnClick="btnSend_Click" />
        <br /><br />
        <h3>Response</h3>
        <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" Rows="15" Columns="80"></asp:TextBox>
    </form>
</body>
</html>
