<%@ Page Title="Főoldal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotNetHF._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1><br/>
                <h2>Városismereti vetélkedő adminisztrációs oldal.</h2>
            </hgroup>
            <p>
                &nbsp;Kizárólag belépés után érhető el!</p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Adminisztrációs felületek:</h3>
    <ol class="round">
        <li class="one">
            <h5>Kérdések</h5>
            Kérdések megtekintése város szerint, kérdés módosítása, új kérdés felvétele, kérdés törlése. <a runat="server" href="~/Questions/List">Tovább...</a></li>
        <li class="two">
            <h5>Játékosok</h5>
            Játékosok listázása, játékos által megválaszolt kérdések megtekintése. <a runat="server" href="~/Players/List">Tovább...</a></li>
        <li class="three">
            <h5>Regisztráció</h5>
            Regisztráció az oldalra. <a runat="server" href="~/Account/Register">Tovább...</a></li>
    </ol>
</asp:Content>
