<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="CRUD.Pages.Crud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <h2 class="text-center my-4"><asp:Label ID="lblTitulo" runat="server" /></h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label">Nombre</asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="150" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtCorreo" CssClass="form-label">Correo</asp:Label>
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" MaxLength="150" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtEdad" CssClass="form-label">Edad</asp:Label>
                <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="mb-4">
                <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="form-label">Fecha de nacimiento</asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <asp:Button ID="btnCreate" runat="server" Text="Guardar" CssClass="btn btn-success" Visible="false" OnClick="btnCreate_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-warning" Visible="false" OnClick="btnUpdate_Click" />
            <asp:HyperLink ID="btnVolver" runat="server" NavigateUrl="~/Pages/Index.aspx" CssClass="btn btn-secondary">Volver</asp:HyperLink>
        </div>
    </div>
</asp:Content>