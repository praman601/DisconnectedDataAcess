<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="disconnectedDAtaAccess.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Get data" OnClick="Button1_Click" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="undo" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="TotalMarks" HeaderText="TotalMarks" SortExpression="TotalMarks" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="Button2" runat="server" Text="Update database" OnClick="Button2_Click" />
        
        <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>
        </form>
        
</body>
</html>
