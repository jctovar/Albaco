Public Class UserSearch
    Private Sub UsersSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Usuarios del sistema")

        Me.FillDatagrid()
    End Sub

    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = UserDB.GetAllUsers

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = tableview
            DataGridView1.AutoResizeColumns()
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub AddUser()
        Dim frmUser As New UserBox

        frmUser.addUser = True

        Dim result As DialogResult = frmUser.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub

    Private Sub UpdateUser()
        Dim frmUser As New UserBox

        frmUser.addUser = False
        frmUser.userID = DataGridView1(0, DataGridView1.CurrentRow.Index).Value

        Dim result As DialogResult = frmUser.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub

    Private Sub DeleteUser()
        If MessageBox.Show("Esta seguro de eliminar el registro?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Dim user As New User

            user = UserDB.GetUser(DataGridView1(0, DataGridView1.CurrentRow.Index).Value)

            Dim result As Boolean = UserDB.DeleteUser(user)

            If result Then
                FillDatagrid()
            Else
                MessageBox.Show("Ocurrio un error o el registro ya no existe!", Application.ProductName)
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Me.UpdateUser()
    End Sub

    Private Sub EditarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem1.Click
        Me.UpdateUser()
    End Sub

    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click
        Me.DeleteUser()
    End Sub

    Private Sub NuevoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem1.Click
        Me.AddUser()
    End Sub

    Private Sub CerrarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.Space Then
            Me.UpdateUser()
        End If
    End Sub
End Class