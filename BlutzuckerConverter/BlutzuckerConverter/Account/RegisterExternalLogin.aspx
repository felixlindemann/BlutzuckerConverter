<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RegisterExternalLogin.aspx.vb" Inherits="BlutzuckerConverter.RegisterExternalLogin" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Mit Ihrem <%: ProviderDisplayName %>-Konto registrieren</h1>
        <h2><%: ProviderUserName %>.</h2>
    </hgroup>

    
    <asp:Label runat="server" ID="providerMessage" CssClass="field-validation-error" />
    

    <asp:PlaceHolder runat="server" ID="userNameForm">
        <fieldset>
            <legend>Zuordnungsformular</legend>
            <p>
                Sie sind authentifiziert mit <strong><%: ProviderDisplayName %></strong> als
                <strong><%: ProviderUserName %></strong>. Bitte geben Sie unten einen Benutzernamen für die aktuelle Website ein, und
                klicken Sie dann auf die Schaltfläche "Anmelden".
            </p>
            <ol>
                <li class="email">
                    <asp:Label runat="server" AssociatedControlID="userName">Benutzername</asp:Label>
                    <asp:TextBox runat="server" ID="userName" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userName"
                        Display="Dynamic" ErrorMessage="Der Benutzername ist erforderlich." ValidationGroup="NewUser" />
                    
                    <asp:Label runat="server" ID="userNameMessage" CssClass="field-validation-error" />
                    
                </li>
            </ol>
            <asp:Button runat="server" Text="Anmelden" ValidationGroup="NewUser" OnClick="logIn_Click" />
            <asp:Button runat="server" Text="Abbrechen" CausesValidation="false" OnClick="cancel_Click" />
        </fieldset>
    </asp:PlaceHolder>
</asp:Content>
