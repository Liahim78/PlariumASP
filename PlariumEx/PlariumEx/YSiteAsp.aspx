<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YSiteAsp.aspx.cs" Inherits="PlariumEx.YSiteAsp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:GridView ID="TableY" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataSourceID="YSqlDataSource" DataKeyNames="Id" OnSelectedIndexChanged="TableY_SelectedIndexChanged" Visible="False">
            
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ButtonType="Button" SelectText="Выбрать" ShowSelectButton="true" />
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Parametr1" HeaderText="Parametr1" SortExpression="Parametr1" />
                <asp:BoundField DataField="Parametr2" HeaderText="Parametr2" SortExpression="Parametr2" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />

        </asp:GridView>
        
       
        <asp:SqlDataSource ID="YIdSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Parametr1], [Parametr2] FROM [TableY] WHERE ([Id] = @Id)">
            <SelectParameters>
                <asp:ControlParameter ControlID="IdYTextBox" DefaultValue="1" Name="Id" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        
       
        <asp:SqlDataSource ID="YSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Parametr1], [Parametr2] FROM [TableY]"></asp:SqlDataSource>
        
       
    
    
            <br />
            <asp:Label ID="UserNameLabel" runat="server" Text="User Name"></asp:Label>
&nbsp;<asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="LogInButton" runat="server" OnClick="LogInButton_Click" Text="LOG IN" />
            <br />
        
       
    
    
        <asp:Button ID="SelectAllYButton" runat="server" Text="Select all Y" OnClick="SelectAllXButton_Click" Visible="False" />
&nbsp;<asp:Button ID="DeblockButton" runat="server" Text="Deblock" OnClick="DeblockButton_Click" Visible="False" />
        <br />
        <asp:Button ID="SelectByIdYButton" runat="server" Text="Select by Id" OnClick="SelectByIdXButton_Click" Visible="False" />
&nbsp;<asp:TextBox ID="IdYTextBox" runat="server" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="SelectIdLabel" runat="server" Text="Select Id = Null" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="LabelPrY1" runat="server" Text="ParametrY1" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="PrY1TextBox" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="LabelPrY2" runat="server" Text="ParametrY2" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="PrY2TextBox" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:Button ID="AddYButton" runat="server" Text="Add" OnClick="AddXButton_Click" Visible="False" />
        &nbsp;<asp:Button ID="UpdateYButton" runat="server" Text="Update" OnClick="UpdateXButton_Click" Visible="False" />
        &nbsp;<asp:Button ID="DeleteYButton" runat="server" Text="Delete" OnClick="DeleteXButton_Click" Visible="False" />
        
    </div>
    </form>
</body>
</html>
