<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertQuestionNotification.aspx.cs" Inherits="DotNetHF.Questions.InsertQuestionNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        A következő két kérdés hasonlít:</p>
    <p>
        Eredeti:</p>
    <p>
        &nbsp;</p>
    <p>
        Feltölteni kívánt:</p>
    <p>
        &nbsp;</p>
    Felül szeretné írni?<br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Felülírás" />
    <asp:Button ID="Button2" runat="server" Text="Mégse" OnClick="Button2_Click" />
</asp:Content>
