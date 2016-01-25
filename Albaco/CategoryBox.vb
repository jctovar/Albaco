Public Class CategoryBox
    Public addCategory As Boolean
    Public categoryID As Integer
    Private category As Category

    Private Sub CategoryBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If addCategory Then
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nueva categoría")
        Else
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar categoría")

            Me.GetCategory(categoryID)
            Me.DisplayCategory()
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If addCategory Then
            ' Nuevo registro
            Dim category As New Category

            category.Name = txtName.Text
            category.Description = txtDescription.Text

            Try
                CategoryDB.AddCategory(category)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        Else
            ' Actualizacion de registro
            Dim category As New Category

            category.Id = categoryID
            category.Name = txtName.Text
            category.Description = txtDescription.Text

            Try
                CategoryDB.UpdateCategory(category)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub

    Private Sub GetCategory(categoryID As Integer)
        Try
            category = CategoryDB.GetCategory(categoryID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub DisplayCategory()
        txtName.Text = category.Name
        txtDescription.Text = category.Description
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class