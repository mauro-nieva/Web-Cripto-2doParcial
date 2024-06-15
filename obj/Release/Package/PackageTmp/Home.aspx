<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Web_Cripto_2doParcial.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="Content/bootstrap.css" rel="stylesheet" />
<link href="Content/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
<script src="Scripts/jquery-3.4.1.js"></script>
<link href="Content/Home.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
    <div class="container">................
        <a class="navbar-brand" runat="server" href="~/">Web-Cripto-2doParcial</a>
        <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Alternar navegación" aria-controls="navbarSupportedContent"
            aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
            <ul class="navbar-nav flex-grow-1">
                <li class="nav-item"><a class="nav-link" runat="server" href="~/">Inicio</a></li>
                <li class="nav-item"><a class="nav-link" runat="server" href="~/About">Acerca de</a></li>
                <li class="nav-item"><a class="nav-link" runat="server" href="~/Contact">Contacto</a></li>
            </ul>
        </div>
    </div>
</nav>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlProfile" runat="server" Visible="false">
                <br />
                <h1 class="h5">Datos de perfil</h1>
            <hr />
            <table class="table w-25 h-50">
                <tr>
                    <td rowspan="6" valign="top">
                        <asp:Image ID="imgProfile" runat="server" Width="50" Height="50" />
                    </td>
                </tr>
                <tr>
                    <td>ID: <asp:Label ID="lblId" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Name: <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Email: <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Verified Email: <asp:Label ID="lblVerified" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            </asp:Panel>
            <div id="div_Archivos">
                <h1 class="h5">Archivos de Google Drive</h1>
                <asp:GridView ID="gvArchivos" runat="server" Width="800px" Height="300px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" >
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
