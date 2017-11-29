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
            <asp:Button ID="btn_Test" runat="server" OnClick="Clic_BtnConnexionBase" Text="CONNEXION BASE" />
            <asp:Label ID="lbl_Test" runat="server" Text="Nothing"></asp:Label>
            <button id="btn_AppelJavascript">CONNEXION Javascript</button>
            </br>
            </br>
            <div>
                <span>BOOMCRAFT</span>
                </br>
            </div>
            </br>
            </br>
            <div>
                <span>FARM VILLAGE</span>
                </br>
                <button id="btn_FV_DoTransaction">FV_DoTransaction</button>
            </div>
        </div>
    </form>
</body>
</html>
