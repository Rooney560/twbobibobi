<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogPurlScanner.aspx.cs" Inherits="twbobibobi.LogPurlScanner" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>purl 修復工具 - 依日誌掃描 templeCheck（purl 或旗標=1）</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <style>
        body { font-family: Segoe UI, "Microsoft JhengHei", Arial; margin:24px; }
        .form-row { margin-bottom: 12px; }
        .hint { color:#666; font-size:12px; }
        .ok { color: #0a7f00; }
        .warn { color:#b35c00; }
        .err { color:#b00020; }
        .badge { display:inline-block; padding:2px 8px; border-radius:12px; background:#eee; margin-left:6px; font-size:12px; }
        .grid { margin-top: 16px; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <h2>purl 修復工具</h2>
    <div class="form-row">
        <asp:Label runat="server" AssociatedControlID="txtDate" Text="日期(YYYYMMDD)："></asp:Label>
        <asp:TextBox runat="server" ID="txtDate" Width="120" MaxLength="8" />
        &nbsp;&nbsp;
        <asp:Label runat="server" AssociatedControlID="txtCode" Text="代號(可選)："></asp:Label>
        <asp:TextBox runat="server" ID="txtCode" Width="100" placeholder="da/h/wu 或 3/4/6" />
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="btnScan" Text="掃描日誌" OnClick="btnScan_Click" />
        <span class="hint">可用 <code>?date=20250820&code=da</code> 直接過濾宮廟（代號或 AdminID）。</span>
    </div>
    <div class="form-row">
        <asp:Literal runat="server" ID="litStatus"></asp:Literal>
    </div>

    <div class="grid">
        <asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="False" GridLines="None" BorderStyle="None" CssClass="table"
            AllowPaging="True" PageSize="200" OnPageIndexChanging="gvResults_PageIndexChanging"
            EmptyDataText="沒有符合條件的記錄。">
            <Columns>
                <asp:BoundField HeaderText="時間" DataField="LogTimeText" />
                <asp:BoundField HeaderText="頁面" DataField="PageName" />
                <asp:TemplateField HeaderText="原始網址">
                    <ItemTemplate>
                        <a href="<%# Eval("RawUrl") %>" target="_blank"><%# Eval("DisplayUrl") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="觸發類型" DataField="MatchType" />
                <asp:BoundField HeaderText="參數鍵" DataField="MatchKey" />
                <asp:BoundField HeaderText="參數值" DataField="MatchValue" />
                <asp:BoundField HeaderText="Kind(服務)" DataField="Kind" />
                <asp:BoundField HeaderText="a(AdminID)" DataField="A" />
                <asp:BoundField HeaderText="aid(ApplicantID)" DataField="Aid" />
                <asp:BoundField HeaderText="宮廟" DataField="TempleName" />
                <asp:BoundField HeaderText="宮廟代碼" DataField="TempleCode" />
                <asp:BoundField HeaderText="服務" DataField="ServiceName" />
                <asp:TemplateField HeaderText="建議字串">
                    <ItemTemplate>
                        <%# Eval("SuggestedText") %><span class="badge">補到 PostURL</span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</form>
</body>
</html>
