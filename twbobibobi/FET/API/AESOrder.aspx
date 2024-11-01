<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AESOrder.aspx.cs" Inherits="Temple.FET.API.AESOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="測試解密字串："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="測試解密" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="測試解密結果："></asp:Label>
            <br />
            <br />
            
            <asp:Label ID="Label3" runat="server" Text="正式解密字串："></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:Button ID="Button2" runat="server" Text="正式解密" OnClick="Button2_Click" />
            <br />
            <asp:Label ID="Label4" runat="server" Text="正式解密結果："></asp:Label>
        </div>
    </form>
</body>
</html>
