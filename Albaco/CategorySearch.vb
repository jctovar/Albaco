Public Class CategorySearch
    Private Sub CategorySearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Catalogo de lineas")

        Me.FillDatagrid()
    End Sub

    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = CategoryDB.GetAllCategories

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = tableview
            DataGridView1.AutoResizeColumns()
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Refresh()
        Catch ex As Exception
            'MessageBox.Show(ex.Message, ex.GetType.ToString)
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub AddCategory()
        Dim frmCategory As New CategoryBox

        frmCategory.addCategory = True

        Dim result As DialogResult = frmCategory.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub

    Private Sub UpdateCategory()
        Dim frmCategory As New CategoryBox

        frmCategory.addCategory = False
        frmCategory.categoryID = DataGridView1(0, DataGridView1.CurrentRow.Index).Value

        Dim result As DialogResult = frmCategory.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub

    Private Sub DeleteCategory()
        If MessageBox.Show("Esta seguro de eliminar el registro?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Dim category As New Category

            category = CategoryDB.GetCategory(DataGridView1(0, DataGridView1.CurrentRow.Index).Value)

            Dim result As Boolean = CategoryDB.DeleteCategory(category)

            If result Then
                FillDatagrid()
            Else
                MessageBox.Show("Ocurrio un error de integridad o el registro ya no existe!", Application.ProductName)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Me.UpdateCategory()
    End Sub

    Private Sub CerrarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub NuevoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem1.Click
        Me.AddCategory()
    End Sub

    Private Sub EditarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem1.Click
        Me.UpdateCategory()
    End Sub

    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click
        Me.DeleteCategory()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.Space Then
            Me.UpdateCategory()
        End If
    End Sub
End Class