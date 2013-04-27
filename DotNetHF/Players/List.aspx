<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DotNetHF.Players.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h6>
        Játékosok:
    </h6>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display." ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" CancelText="Mégse" SelectText="Kiválaszt" />
            <asp:BoundField DataField="ID" HeaderText="Azonosító" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="UserName" HeaderText="Felhasználónév" SortExpression="UserName" />
            <asp:BoundField DataField="Points" HeaderText="Pontok" SortExpression="Points" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" DeleteCommand="DELETE FROM [Players] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Players] ([UserName], [Points]) VALUES (@UserName, @Points)" SelectCommand="SELECT [ID], [UserName], [Points] FROM [Players]" UpdateCommand="UPDATE [Players] SET [UserName] = @UserName, [Points] = @Points WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Points" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Points" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <h6>
        Megválaszolt kérdések:
    </h6>
    <asp:ListView ID="ListView2" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource3">
        <AlternatingItemTemplate>
            <span style="background-color: #FFFFFF; color: #284775;">Kérdés:
            <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
            <br />
            Kérdés azonosító:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Város:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            <br />
<br /></span>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <span style="background-color: #999999;">Question:
            <asp:TextBox ID="QuestionTextBox" runat="server" Text='<%# Bind("Question") %>' />
            <br />
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            <br /><br /></span>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <span>Nincs megjeleníthető kérdés</span>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <span style="">Question:
            <asp:TextBox ID="QuestionTextBox" runat="server" Text='<%# Bind("Question") %>' />
            <br />Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
            <br /><br /></span>
        </InsertItemTemplate>
        <ItemTemplate>
            <span style="background-color: #E0FFFF; color: #333333;">Kérdés:
            <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
            <br />
            Kérdés azonosító:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Város:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            <br />
<br /></span>
        </ItemTemplate>
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server" style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                <span runat="server" id="itemPlaceholder" />
            </div>
            <div style="text-align: center;background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;">
            </div>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <span style="background-color: #E2DED6; font-weight: bold;color: #333333;">Kérdés:
            <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
            <br />
            Kérdés azonosító:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Város:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            <br />
<br /></span>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" SelectCommand="SELECT q.[Question], q.[ID], c.[Name] FROM [Questions] q, [PlayerToQuestion] pq, [Players] p, [City] c WHERE (q.[ID] = pq.[QuestionID] and c.[ID] = q.[CityID] and pq.[PlayerID] = @ID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br/>
    </asp:Content>
