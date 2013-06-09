<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DotNetHF.Questions.Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h6>Város:</h6>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" Height="20px" Width="157px">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" SelectCommand="SELECT * FROM [City]"></asp:SqlDataSource>
    <h6>Kérdések:</h6>
    <asp:GridView ID="QuestionsGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" EmptyDataText="Nincs kérdés a városban" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" CancelText="Mégse" DeleteText="Törlés" EditText="Módosítás" SelectText="Kiválaszt" />
            <asp:BoundField DataField="Question" HeaderText="Kérdés" SortExpression="Question" />
            <asp:BoundField DataField="Position" HeaderText="Pozíció" SortExpression="Position" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" DeleteCommand="DELETE FROM [Questions] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Questions] ([Question], [Date], [Position], [Answer1], [Answer2], [Answer3], [Answer4], [RightAnswer], [Image], [CityID]) VALUES (@Question, @Date, @Position, @Answer1, @Answer2, @Answer3, @Answer4, @RightAnswer, @Image, @CityID)" SelectCommand="SELECT * FROM [Questions] WHERE ([CityID] = @CityID)" UpdateCommand="UPDATE [Questions] SET [Question] = @Question, [Date] = @Date, [Position] = @Position, [Answer1] = @Answer1, [Answer2] = @Answer2, [Answer3] = @Answer3, [Answer4] = @Answer4, [RightAnswer] = @RightAnswer, [Image] = @Image, [CityID] = @CityID WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Question" Type="String" />
            <asp:Parameter DbType="Date" Name="Date" />
            <asp:Parameter Name="Position" Type="String" />
            <asp:Parameter Name="Answer1" Type="String" />
            <asp:Parameter Name="Answer2" Type="String" />
            <asp:Parameter Name="Answer3" Type="String" />
            <asp:Parameter Name="Answer4" Type="String" />
            <asp:Parameter Name="RightAnswer" Type="String" />
            <asp:Parameter Name="Image" Type="String" />
            <asp:Parameter Name="CityID" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="CityID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Question" Type="String" />
            <asp:Parameter DbType="Date" Name="Date" />
            <asp:Parameter Name="Position" Type="String" />
            <asp:Parameter Name="Answer1" Type="String" />
            <asp:Parameter Name="Answer2" Type="String" />
            <asp:Parameter Name="Answer3" Type="String" />
            <asp:Parameter Name="Answer4" Type="String" />
            <asp:Parameter Name="RightAnswer" Type="String" />
            <asp:Parameter Name="Image" Type="String" />
            <asp:Parameter Name="CityID" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        </asp:SqlDataSource>
<h6>
    Részletek:
</h6>     
<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="SqlDataSource3" Height="50px" Width="125px">
    <EmptyDataTemplate>
        <asp:Button ID="InsertButton" runat="server" CommandName="New" Text="Új kérdés"/>       
    </EmptyDataTemplate>
    <Fields>       
        <asp:TemplateField HeaderText="Kérdés" SortExpression="Question">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Question") %>'></asp:TextBox>   
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat=server 
                    ControlToValidate=TextBox2
                    ErrorMessage="Kérdés megadása kötelező"> *
                </asp:RequiredFieldValidator>            
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Question") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat=server 
                    ControlToValidate=TextBox2
                    ErrorMessage="Kérdés megadása kötelező"> *
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Dátum" SortExpression="Date">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat=server 
                    ControlToValidate=TextBox3
                    ErrorMessage="Dátumformátum: yyyy.MM.dd. hh:mm:ss" 
                    ValidationExpression="[0-2][0-9][0-9][0-9]\.[0-1][0-9]\.[0-3][0-9]\. [0-2][0-9]:[0-5][0-9]:[0-5][0-9]" />
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat=server 
                    ControlToValidate=TextBox3
                    ErrorMessage="Dátumformátum: yyyy.MM.dd. hh:mm:ss" 
                    ValidationExpression="[0-2][0-9][0-9][0-9]\.[0-1][0-9]\.[0-3][0-9]\. [0-2][0-9]:[0-5][0-9]:[0-5][0-9]" />
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pozíció" SortExpression="Position">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Position") %>'></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat=server 
                    ControlToValidate=TextBox4
                    ErrorMessage="Pozícióformátum: #.#,#.#" 
                    ValidationExpression="[0-9]*\.[0-9]*,[0-9]*\.[0-9]*" />
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Position") %>'></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat=server 
                    ControlToValidate=TextBox4
                    ErrorMessage="Pozícióformátum: #.#,#.#" 
                    ValidationExpression="[0-9]*\.[0-9]*,[0-9]*\.[0-9]*" />
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Position") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Válaszlehetőség" SortExpression="Answer1">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Answer1") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat=server 
                    ControlToValidate=TextBox5
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Answer1") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat=server 
                    ControlToValidate=TextBox5
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Answer1") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Válaszlehetőség" SortExpression="Answer2">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Answer2") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat=server 
                    ControlToValidate=TextBox6
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Answer2") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat=server 
                    ControlToValidate=TextBox6
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Answer2") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Válaszlehetőség" SortExpression="Answer3">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Answer3") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat=server 
                    ControlToValidate=TextBox7
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Answer3") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat=server 
                    ControlToValidate=TextBox7
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Answer3") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Válaszlehetőség" SortExpression="Answer4">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Answer4") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat=server 
                    ControlToValidate=TextBox8
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Answer4") %>'></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat=server 
                    ControlToValidate=TextBox8
                    ErrorMessage="Válaszlehetőség megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Answer4") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Helyes válasz" SortExpression="RightAnswer">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("RightAnswer") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat=server 
                    ControlToValidate=TextBox9
                    ErrorMessage="Helyes válasz megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("RightAnswer") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat=server 
                    ControlToValidate=TextBox9
                    ErrorMessage="Helyes válasz megadása kötelező"> 
                </asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label9" runat="server" Text='<%# Bind("RightAnswer") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Kép" SortExpression="Image">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Image") %>'></asp:TextBox>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Image") %>'></asp:TextBox>
            </InsertItemTemplate>
            <ItemTemplate>               
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("Image") %>' Width="300" Height="300"/>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Város" SortExpression="CityID">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("CityID") %>'>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" SelectCommand="SELECT * FROM [City]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="QuestionsGridView" Name="ID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("CityID") %>'>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" SelectCommand="SELECT * FROM [City]"></asp:SqlDataSource>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>              
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" CancelText="Mégse" DeleteText="Törlés" EditText="Módosítás" NewText="Új" UpdateText="Módosít" />
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:questionsConnectionString1 %>" SelectCommand="SELECT q.*, c.[Name] FROM [Questions] q, [City] c WHERE (q.[ID] = @ID and q.[CityID] = c.[ID])" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [Questions] WHERE [ID] = @original_ID" InsertCommand="INSERT INTO [Questions] ([Question], [Date], [Position], [Answer1], [Answer2], [Answer3], [Answer4], [RightAnswer], [Image], [CityID]) VALUES (@Question, @Date, @Position, @Answer1, @Answer2, @Answer3, @Answer4, @RightAnswer, @Image, @CityID)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Questions] SET [Question] = @Question, [Date] = @Date, [Position] = @Position, [Answer1] = @Answer1, [Answer2] = @Answer2, [Answer3] = @Answer3, [Answer4] = @Answer4, [RightAnswer] = @RightAnswer, [Image] = @Image, [CityID] = @CityID WHERE [ID] = @original_ID">
    <DeleteParameters>
        <asp:Parameter Name="original_ID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Question" Type="String" />
        <asp:Parameter DbType="Date" Name="Date" />
        <asp:Parameter Name="Position" Type="String" />
        <asp:Parameter Name="Answer1" Type="String" />
        <asp:Parameter Name="Answer2" Type="String" />
        <asp:Parameter Name="Answer3" Type="String" />
        <asp:Parameter Name="Answer4" Type="String" />
        <asp:Parameter Name="RightAnswer" Type="String" />
        <asp:Parameter Name="Image" Type="String" />
        <asp:Parameter Name="CityID" Type="Int32" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="QuestionsGridView" Name="ID" PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="Question" Type="String" />
        <asp:Parameter DbType="Date" Name="Date" />
        <asp:Parameter Name="Position" Type="String" />
        <asp:Parameter Name="Answer1" Type="String" />
        <asp:Parameter Name="Answer2" Type="String" />
        <asp:Parameter Name="Answer3" Type="String" />
        <asp:Parameter Name="Answer4" Type="String" />
        <asp:Parameter Name="RightAnswer" Type="String" />
        <asp:Parameter Name="Image" Type="String" />
        <asp:Parameter Name="CityID" Type="Int32" />
        <asp:Parameter Name="original_ID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
    <h6>Kérdés exportálása XML fájlként</h6>
        <asp:Button ID="Button2" runat="server" Text="Export" OnClick="Button2_Click" />
    <h6>Kérdés exportálása JSON-ként</h6>
        <asp:Button ID="Button3" runat="server" Text="Export" OnClick="Button3_Click" />
        <h6>Új kérdés feltöltése XML fájlból</h6>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Feltöltés" />
        
</asp:Content>
