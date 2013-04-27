<%@ Page Title="Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DotNetHF.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Városismereti vetélkedő információs oldal</h2>
    </hgroup>

    <article>
        <p>        
            Városismereti vetélkedő adminisztrrációs oldal.
        </p>

        <p>        
            Lehetőség van kérdések módosítására, felhasználók megtekintésére.
        </p>

    </article>

    <aside>
        <h3>Navigáció</h3>
        <p>        
            Elérhető oldalak:
        </p>
        <ul>
            <li><a runat="server" href="~/">Főoldal</a></li>
            <li><a id="A1" runat="server" href="~/Questions/List">Kérdések</a></li>
            <li><a id="A2" runat="server" href="~/Players/List">Játékosok</a></li>
            <li><a runat="server" href="~/About">Info</a></li>
            <li><a runat="server" href="~/Contact">Kapcsolat</a></li>
        </ul>
    </aside>
</asp:Content>