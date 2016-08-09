<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XSiteAsp.aspx.cs" Inherits="PlariumEx.SiteAsp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="TableX" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataSourceID="XIdSqlDataSource" DataKeyNames="Id" OnSelectedIndexChanged="TableX_SelectedIndexChanged" Visible="False">
            
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
        
       
        <asp:SqlDataSource ID="XIdSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Parametr1], [Parametr2] FROM [TableX] WHERE ([Id] = @Id)">
            <SelectParameters>
                <asp:ControlParameter ControlID="IdXTextBox" DefaultValue="1" Name="Id" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="XSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Parametr1], [Parametr2] FROM [TableX]"></asp:SqlDataSource>
        
       
        <br />
        <asp:Button ID="SelectAllXButton" runat="server" Text="Select all X" OnClick="SelectAllXButton_Click" />
&nbsp;<asp:Button ID="SaveChengesXButton" runat="server" Text="Save chenges" OnClick="SaveChengesXButton_Click" />
        <br />
        <asp:Button ID="SelectByIdXButton" runat="server" Text="Select by Id" OnClick="SelectByIdXButton_Click" />
&nbsp;<asp:TextBox ID="IdXTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="SelectIdLabel" runat="server" Text="Select Id = Null"></asp:Label>
        <br />
        <asp:Label ID="LabelPrX1" runat="server" Text="ParametrX1"></asp:Label>
&nbsp;<asp:TextBox ID="PrX1TextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelPrX2" runat="server" Text="ParametrX2"></asp:Label>
&nbsp;<asp:TextBox ID="PrX2TextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="AddXButton" runat="server" Text="Add" OnClick="AddXButton_Click" />
        &nbsp;<asp:Button ID="UpdateXButton" runat="server" Text="Update" OnClick="UpdateXButton_Click" />
        &nbsp;<asp:Button ID="DeleteXButton" runat="server" Text="Delete" OnClick="DeleteXButton_Click" />
        
    </div>
    </form>
</body>
</html>
