Public Class CustomerBox
    Public addCustomer As Boolean
    Public customerID As Integer
    Private customer As Customer

    Private Sub CustomerBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If addCustomer Then
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nuevo clientea")
        Else
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar cliente")

            Me.GetCustomer(customerID)
            Me.DisplayCustomer()
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If addCustomer Then
            ' Nuevo registro
            Dim customer As New Customer

            customer.Name = txtName.Text
            customer.Address_1 = txtAddress_1.Text
            customer.Address_2 = txtAddress_2.Text
            customer.PostalCode = txtPostalCode.Text
            customer.Phone = txtPhone.Text
            customer.Email = txtEmail.Text

            Try
                CustomerDB.AddCustomer(customer)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        Else
            ' Actualizacion de registro
            Dim customer As New Customer

            customer.Id = customerID
            customer.Name = txtName.Text
            customer.Address_1 = txtAddress_1.Text
            customer.Address_2 = txtAddress_2.Text
            customer.PostalCode = txtPostalCode.Text
            customer.Phone = txtPhone.Text
            customer.Email = txtEmail.Text

            Try
                CustomerDB.UpdateCustomer(customer)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub

    Private Sub GetCustomer(customerID As Integer)
        Try
            customer = CustomerDB.GetCustomer(customerID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub DisplayCustomer()
        txtName.Text = customer.Name
        txtAddress_1.Text = customer.Address_1
        txtAddress_2.Text = customer.Address_2
        txtPostalCode.Text = customer.PostalCode
        txtPhone.Text = customer.Phone
        txtEmail.Text = customer.Email
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class