Imports System.Web.Security
Imports DotNetOpenAuth.AspNet
Imports Microsoft.AspNet.Membership.OpenAuth

Public Class RegisterExternalLogin
    Inherits System.Web.UI.Page

    Protected Property ProviderName As String
        Get
            Return If(DirectCast(ViewState("ProviderName"), String), String.Empty)
        End Get
        Private Set(value As String)
            ViewState("ProviderName") = value
        End Set
    End Property

    Protected Property ProviderDisplayName As String
        Get
            Return If(DirectCast(ViewState("PropertyProviderDisplayName"), String), String.Empty)
        End Get
        Private Set(value As String)
            ViewState("ProviderDisplayName") = value
        End Set
    End Property

    Protected Property ProviderUserId As String
        Get
            Return If(DirectCast(ViewState("ProviderUserId"), String), String.Empty)
        End Get

        Private Set(value As String)
            ViewState("ProviderUserId") = value
        End Set
    End Property

    Protected Property ProviderUserName As String
        Get
            Return If(DirectCast(ViewState("ProviderUserName"), String), String.Empty)
        End Get

        Private Set(value As String)
            ViewState("ProviderUserName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ProcessProviderResult()
        End If
    End Sub

    Protected Sub logIn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CreateAndLoginUser()
    End Sub

    Protected Sub cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RedirectToReturnUrl()
    End Sub

    Private Sub ProcessProviderResult()
        ' Ergebnis von einem Authentifizierungsanbieter in der Anforderung verarbeiten
        ProviderName = OpenAuth.GetProviderNameFromCurrentRequest()

        If String.IsNullOrEmpty(ProviderName) Then
            Response.Redirect(FormsAuthentication.LoginUrl)
        End If

        ' Weiterleitungs-URL für OpenAuth-Überprüfung erstellen
        Dim redirectUrl As String = "~/Account/RegisterExternalLogin"
        Dim returnUrl As String = Request.QueryString("ReturnUrl")
        If Not String.IsNullOrEmpty(returnUrl) Then
            redirectUrl &= "?ReturnUrl=" & HttpUtility.UrlEncode(returnUrl)
        End If

        ' OpenAuth-Nutzlast überprüfen
        Dim authResult As AuthenticationResult = OpenAuth.VerifyAuthentication(redirectUrl)
        ProviderDisplayName = OpenAuth.GetProviderDisplayName(ProviderName)
        If Not authResult.IsSuccessful Then
            Title = "Fehler bei der externen Anmeldung."
            userNameForm.Visible = False
            
            providerMessage.Text = String.Format("Fehler bei der externen Anmeldung {0}.", ProviderDisplayName)
            
            ' Wenn Sie diesen Fehler anzeigen möchten, aktivieren Sie Seitenablaufverfolgung in der Datei "web.config" (<system.web><trace enabled="true"/></system.web>), und besuchen Sie "~/Trace.axd".
            Trace.Warn("OpenAuth", String.Format("Fehler beim Überprüfen der Authentifizierung mit ""{0}"")", ProviderDisplayName), authResult.Error)
            Return
        End If

        ' Der Benutzer hat sich mit dem Anbieter erfolgreich angemeldet.
        ' Überprüfen, ob der Benutzer bereits lokal registriert ist
        If OpenAuth.Login(authResult.Provider, authResult.ProviderUserId, createPersistentCookie:=False) Then
            RedirectToReturnUrl()
        End If

        ' Anbieterdetails in "ViewState" speichern
        ProviderName = authResult.Provider
        ProviderUserId = authResult.ProviderUserId
        ProviderUserName = authResult.UserName

        ' Abfragezeichenfolge aus der Aktion entfernen
        Form.Action = ResolveUrl(redirectUrl)

        If (User.Identity.IsAuthenticated) Then
            ' Der Benutzer ist bereits authentifiziert. Externe Anmeldung hinzufügen und an Rückgabe-URL weiterleiten.
            OpenAuth.AddAccountToExistingUser(ProviderName, ProviderUserId, ProviderUserName, User.Identity.Name)
            RedirectToReturnUrl()
        Else
            ' Der Benutzer ist neu. Nach dem gewünschten Mitgliedschaftsnamen fragen.
            userName.Text = authResult.UserName
        End If
    End Sub

    Private Sub CreateAndLoginUser()
        If Not IsValid Then
            Return
        End If

        Dim createResult As CreateResult = OpenAuth.CreateUser(ProviderName, ProviderUserId, ProviderUserName, userName.Text)

        If Not createResult.IsSuccessful Then
            
            userNameMessage.Text = createResult.ErrorMessage
            
        Else
            ' Der Benutzer wurde erstellt und erfolgreich zugeordnet.
            If OpenAuth.Login(ProviderName, ProviderUserId, createPersistentCookie:=False) Then
                RedirectToReturnUrl()
            End If
        End If
    End Sub

    Private Sub RedirectToReturnUrl()
        Dim returnUrl As String = Request.QueryString("ReturnUrl")
        If Not String.IsNullOrEmpty(returnUrl) And OpenAuth.IsLocalUrl(returnUrl) Then
            Response.Redirect(returnUrl)
        Else
            Response.Redirect("~/")
        End If
    End Sub
End Class