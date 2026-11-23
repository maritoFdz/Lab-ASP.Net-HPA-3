<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUD.Pages.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center my-4">Listado de Registros</h1>

    <div class="text-center mb-4">
        <a href="Crud.aspx?op=C" class="btn btn-success">Crear Registro</a>
    </div>

    <asp:GridView ID="gvusuarios" runat="server" AutoGenerateColumns="false" class="table table-borderless table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Opciones del Administrador">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Read" CssClass="btn form-control-sm btn-info" ID="btnRead" OnClick="BtnRead_Click" CommandName="Leer" CommandArgument='<%# Eval("IdUsuario") %>' />
                    <asp:Button runat="server" Text="Update" CssClass="btn form-control-sm btn-warning" ID="btnUpdate" OnClick="BtnUpdate_Click" CommandName="Actualizar" CommandArgument='<%# Eval("IdUsuario") %>' />
                    <asp:Button runat="server" Text="Delete" CssClass="btn form-control-sm btn-danger" ID="btnDelete" OnClick="BtnDelete_Click" CommandName="Eliminar" CommandArgument='<%# Eval("IdUsuario") %>' OnClientClick="return confirm('¿Seguro que quieres eliminar este usuario?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="idUsuario" HeaderText="idUsuario" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Edad" HeaderText="Edad" />
            <asp:BoundField DataField="Correo" HeaderText="Correo" />
            <asp:BoundField DataField="FechaNacimiento" HeaderText="FechaNacimiento" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="FechaDeInsercion" HeaderText="FechaDeInsercion" />
        </Columns>
    </asp:GridView>
</asp:Content>