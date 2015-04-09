<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SQLTest.aspx.cs" Inherits="WebApplication1.SQLTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select * from circle.dbo.Students"></asp:SqlDataSource>
    </form>
</body>
</html>
