<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BlutzuckerConverter._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Convert your sugar values.</h2>
            </hgroup> 
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent"> 
    <article>
        <table>
            <tr>
                <th>
                    <asp:Label ID="Label1" runat="server" Text="Sugarlevel">
                </asp:Label>

                </th>
                <td>
                    <asp:TextBox ID="sugarlevel" ToolTip="Hit Enter to update" runat="server" Width="140px"></asp:TextBox >

                </td>
                <td>
                    <asp:Label ID="lbl2" runat="server" Text="units">
                    </asp:Label>

                </td>
                <td> <asp:DropDownList ID="cbolevel" runat="server">
                <asp:ListItem Value="1">[mg/dl]</asp:ListItem>
                <asp:ListItem Value="2">[mmol/l]</asp:ListItem>
            </asp:DropDownList></td>

            </tr>
        </table>
        <h3>        
              <asp:Label ID="Label2" runat="server" Text="Result:">  </asp:Label>
        </h3>

        <p>
           
              <asp:TextBox ID="txtresult" ReadOnly="true" runat="server"></asp:TextBox>
        </p>
        <p>
        <asp:Button ID="Button1" runat="server" Text="Convert" /></p>
    </article>
</asp:Content>
