Public Class UserBox
    Public addUser As Boolean
    Public userID As Integer
    Private user As User
    Private Sub UserBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GetRolList()
        Me.GetStatuslist()

        If addUser Then
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nuevo usuario")
        Else
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar usuario")

            Me.GetUser(userID)
            Me.DisplayUser()
        End If
    End Sub
    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If addUser Then
            ' Nuevo registro
            Dim user As New User

            user.Username = txtUsername.Text
            user.Name = txtName.Text
            user.Password = txtPassword.Text
            user.Email = txtEmail.Text
            user.Phone = txtPhone.Text
            user.Status = cmbStatus.SelectedValue.ToString
            user.Profile = cmbRol.SelectedValue.ToString

            Try
                UserDB.AddUser(user)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        Else
            ' Actualizacion de registro
            Dim user As New User

            user.Id = userID
            user.Username = txtUsername.Text
            user.Name = txtName.Text
            user.Password = txtPassword.Text
            user.Email = txtEmail.Text
            user.Phone = txtPhone.Text
            user.Status = cmbStatus.SelectedValue.ToString
            user.Profile = cmbRol.SelectedValue.ToString

            Try
                UserDB.UpdateUsere(user)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub
    Private Sub GetUser(userID As Integer)
        Try
            user = UserDB.GetUser(userID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub
    Private Sub DisplayUser()
        txtName.Text = user.Name
        txtUsername.Text = user.Username
        txtPassword.Text = user.Password
        txtPhone.Text = user.Phone
        txtEmail.Text = user.Email
        cmbStatus.SelectedValue = user.Status
        cmbRol.SelectedValue = user.Profile
    End Sub
    Private Sub GetRolList()
        cmbRol.DataSource = UserDB.GetProfileList
        cmbRol.DisplayMember = "rol_description"
        cmbRol.ValueMember = "rol_id"
    End Sub
    Private Sub GetStatuslist()
        cmbStatus.DataSource = UserDB.GetEnableList
        cmbStatus.DisplayMember = "status_name"
        cmbStatus.ValueMember = "status_id"
    End Sub
End Class