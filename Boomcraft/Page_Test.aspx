<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_Test.aspx.cs" Inherits="Boomcraft.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- **************************************** APPEL DES SCRIPTS **************************************** -->
    <script src="<%= Page.ResolveUrl("~/Javascript/jquery-3.2.1.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Javascript/Boomcraft.js") %>" type="text/javascript"></script>

    <!-- ****************************************  **************************************** -->
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:Button ID="btn_ConnexionBase" runat="server" OnClick="clic_BtnConnexionBase" Text="CONNEXION BASE" />
            <button id="btn_AppelJavascript">CONNEXION Javascript</button>
        </div>
    </form>
</body>
</html>
