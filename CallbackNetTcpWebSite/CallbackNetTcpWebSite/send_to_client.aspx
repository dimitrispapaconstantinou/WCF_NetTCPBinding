<%@ Page Language="C#" AutoEventWireup="true" CodeFile="send_to_client.aspx.cs" Inherits="send_to_client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <br />
          Enter message
          <br />
          <asp:TextBox ID="MessageBox" runat="server" Width="230px"></asp:TextBox>
          --<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send to Desktop app" Width="132px" />
        </div>
    </form>
</body>
</html>
