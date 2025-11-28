<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="Temple.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body runat="server">
    <table width="1000" border="0" style="margin: 0px auto; background-color: #fff">
        <thead>
            <tr>
                <td style="font-size: 12px; color: #404040; line-height: 30px">親愛的<span style="font-size: 12px; color: #0066ff; line-height: 30px; margin: 0px 3px">楊星星</span>
                    您好，以下是您訂購商品的發票開立通知，<%--詳情也可參閱『<wbr>電子發票流程說明』--%>。
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <table width="100%" border="0" style="padding: 10px 0px; border: 1px solid #666666">
                        <tbody>
                            <tr>
                                <td width="240" rowspan="2" valign="top" style="padding: 0px 10px; border-right: 1px dotted #666666">
                                    <table width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td height="25" align="center">
                                                    <img src="Temples/images/Logo_no.png" width="220" height="50" style="vertical-align: middle" class="CToWUd" data-bit="iit"/></td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="font-size: 15px; line-height: 30px; color: #f00">*此為發票開立通知，非正式發票*</td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="font-size: 30px; line-height: 30px">
                                                    <b style="margin: 0px 3px 0px 0px"><%=yearl %></b>年<span style="padding: 0px 3px">
                                                        <b style="padding: 0px 3px 0px 0px"><%=month %></b>-<b style="padding: 0px 0px 0px 3px">06</b></span>月</td>
                                            </tr>
                                            <tr>
                                                <td align="center"><%=UniformInvoiceNum %></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="baseline">
                                                                    <span style="margin: 0px 5px 0px 0px"><%=nowDate %></span>
                                                                    <span><%=nowTime %></span>
                                                                </td>
                                                                <td valign="baseline" align="right"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="baseline">隨機碼：<span style="font-size: 15px; line-height: 15px"><%=random_number %></span></td>
                                                                <td valign="baseline" align="right">總計：<span id="m_-963141664531758777totalPrice"><%=total %></span>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="baseline">賣方：<span style="font-size: 15px; line-height: 15px"><%=taxpayerID %></span></td>
                                                                <td valign="baseline" align="right"><%=buytaxpayerID %></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img src="<%=barDataUri %>" width="230" height="36" class="CToWUd" data-bit="iit" /></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <img src="<%=qrDataUri1 %>" width="90" class="CToWUd" data-bit="iit" />
                                                    <img src="https://bobibobi.tw/Temples/images/unnamed.gif" width="26" height="90" class="CToWUd" data-bit="iit" />
                                                    <img src="<%=qrDataUri2 %>" width="90" class="CToWUd" data-bit="iit" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td valign="top" style="padding: 0px 10px">
                                    <p style="font-size: 18px; line-height: 20px; text-align: center; margin: 0px; padding: 0px">銷貨明細</p>
                                    <table width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td valign="top" style="font-size: 13px; line-height: 15px"><%=bName %><br />
                                                    營業人地址：<%=bAddress %>
                                                </td>
                                                <td width="200" valign="top" style="font-size: 13px; line-height: 15px">訂單號碼：<span><%=OrderID %></span><br />
                                                    發票號碼：<span><%=UniformInvoiceNum2 %></span><br />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table width="100%" border="0" cellspacing="0">
                                        <thead>
                                            <tr>
                                                <th>項目</th>
                                                <th>品名</th>
                                                <th>數量</th>
                                                <th>單價</th>
                                                <th>金額</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style="padding: 3px">線上服務費</td>
                                                <td style="padding: 3px">大甲鎮瀾宮-光明燈</td>
                                                <td align="center">1</td>
                                                <td align="right">620</td>
                                                <td align="right">620</td>
                                                <td align="center">TX</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" style="padding: 0px 10px">
                                    <table width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <table border="0" style="margin: 0px 20px 0px 0px">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">總計：</td>
                                                                                <td align="right" width="90" style="padding: 0px 10px 0px 0px">$<span>620</span></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td valign="top">合計：<span style="margin: 0px 0px 0px 50px">1</span>項
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td>
                    <table width="100%" border="0">
                        <tbody>
                            <tr>
                                <td valign="top">●</td>
                                <td valign="top">依財政部令此副本僅供參考，不可直接兌獎。<br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">●</td>
                                <td valign="top">統一發票給獎辦法第11條規定，發票金額為0，不能兌換。</td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tfoot>
    </table>
</body>

</html>
