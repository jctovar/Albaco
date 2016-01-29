Public Class StoreBox
    Public addStore As Boolean
    Public storeID As Integer
    Private store As Store

    Private Sub StoreBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If addStore Then
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nuevo almacen")
        Else
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar almacen")

            Me.GetStore(storeID)
            Me.DisplayStore()
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If addStore Then
            ' Nuevo registro
            Dim store As New Store

            store.Name = txtName.Text
            store.Address = txtAddress.Text
            store.Phone = txtPhone.Text

            Try
                StoreDB.AddStore(store)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        Else
            ' Actualizacion de registro
            Dim store As New Store

            store.ID = storeID
            store.Name = txtName.Text
            store.Address = txtAddress.Text
            store.Phone = txtPhone.Text

            Try
                StoreDB.UpdateStore(store)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub

    Private Sub GetStore(storeID As Integer)
        Try
            store = StoreDB.GetStore(storeID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub DisplayStore()
        txtName.Text = store.Name
        txtAddress.Text = store.Address
        txtPhone.Text = store.Phone
    End Sub
End Class