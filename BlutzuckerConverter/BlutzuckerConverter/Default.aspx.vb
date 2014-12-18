Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub



    Private Sub sugarlevel_TextChanged(sender As Object, e As EventArgs) Handles sugarlevel.TextChanged
        calc()
    End Sub

    Private Sub calc()
        Dim _return As String = ""
        Try
            Dim dbl As Double = CDbl(sugarlevel.Text)

            Select Case cbolevel.SelectedValue
                Case 2
                    dbl *= 18.02
                    _return = String.Format("{0} [mg/dl]", Format(dbl, "0.00"))
                Case 1
                    dbl /= 18.02
                    _return = String.Format("{0} [mmol/l]", Format(dbl, "0.00"))
            End Select
        Catch ex As Exception
            _return = "Bitte nur numerische Werte eingeben"

        End Try
        txtresult.Text = _return
    End Sub

    Private Sub cbolevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbolevel.SelectedIndexChanged
        calc()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        calc()
    End Sub
End Class