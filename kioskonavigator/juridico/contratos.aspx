<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true" CodeBehind="contratos.aspx.cs" Inherits="kioskotem.juridico.contratos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
      <h1>
       <%-- Recibo S.A.--%>
     Contratos
        <small><asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="../inicio/inicio.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <%--<li><a href="#">Bienvenido</a></li>--%>
        <li class="active">Contratos </li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               
                <br/>
                 Mes:
                    <asp:DropDownList ID="cbomes" runat="server">
                        <asp:ListItem Value="1">Enero</asp:ListItem>
                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Abril</asp:ListItem>
                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                        <asp:ListItem Value="6">Junio</asp:ListItem>
                        <asp:ListItem Value="7">Julio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                <br/>
                <br/>
                

                Año:
                    <asp:DropDownList ID="cboanio" runat="server">
                        <asp:ListItem>2019</asp:ListItem>
                        <asp:ListItem>2020</asp:ListItem>
                        <asp:ListItem>2021</asp:ListItem>
                        <asp:ListItem>2022</asp:ListItem>
                        <asp:ListItem>2023</asp:ListItem>
                        <asp:ListItem>2024</asp:ListItem>
                        <asp:ListItem>2025</asp:ListItem> 
                        <asp:ListItem>2026</asp:ListItem>
                        <asp:ListItem>2027</asp:ListItem> 
                        <asp:ListItem>2028</asp:ListItem>
                        <asp:ListItem>2029</asp:ListItem> 
                        <asp:ListItem>2030</asp:ListItem>
                    </asp:DropDownList>
                <br/>
                <br/>
                <br/>
                <asp:Button ID="cmdbuscar" runat="server" Text="Buscar" 
                    onclick="cmdbuscar_Click"></asp:Button>
                <br/>
                <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
                <br/>
                <asp:GridView ID="dtgnominas" runat="server" AutoGenerateColumns="False" 
                    GridLines="None" CellPadding="4" ForeColor="#333333" AllowPaging="True" 
                        onrowcommand="dtgnominas_RowCommand" 
                        onpageindexchanging="dtgnominas_PageIndexChanging" 
                        onselectedindexchanged="dtgnominas_SelectedIndexChanged" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                
                                
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="iIdInfoKiosko" runat="server" Text='<%# Bind("iIdArchivosAlta") %>' Visible="false" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Mes">
                                    <ItemTemplate>
                                        <asp:Label Width="70px" style=" text-align:right;" ID="mes" runat="server" Text='<%# Bind("mes") %>'  Visible="true" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Año">
                                    <ItemTemplate>
                                        <asp:Label Width="50px" style=" text-align:right;" ID="anio" runat="server" Text='<%# Bind("anio") %>'  Visible="true" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archivo">
                                    <ItemTemplate>
                                        <asp:Label Width="300px" style=" text-align:center;" ID="nombrearchivo" runat="server" Text='<%# Bind("cNombreArchivo") %>'  Visible="true" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:CommandField ButtonType="Image" 
                                    SelectImageUrl="../imagenes/pdf2.png" 
                                    ShowSelectButton="True">
                                <HeaderStyle Width="50px" />
                                </asp:CommandField>
                                          
                                
                                          
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        

    </section>
</asp:Content>
