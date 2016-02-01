Public Class ProductBox
    Public addProduct As Boolean
    Public productID As Integer
    Private product As Product
    Private price As Price

    Private Sub ProductBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GetCategoriesList()
        Me.GetUnitsList()
        Me.GetTypesList()

        If addProduct Then
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nuevo producto")
        Else
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar producto")

            Me.GetProduct(productID)
            Me.DisplayProduct()
        End If
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If addProduct Then
            ' Nuevo registro
            Dim product As New Product

            product.Category = cmbCategory.SelectedValue.ToString
            product.Name = UCase(txtName.Text)
            product.Key = txtKey.Text
            product.Code = txtCode.Text
            product.Unit = cmbUnits.SelectedValue.ToString
            product.Type = cmbType.SelectedValue.ToString
            product.Description = txtDescription.Text

            If txtTareWeight.Enabled = False Then
                product.TareWeight = 0
            Else
                product.TareWeight = txtTareWeight.Text
            End If

            Try
                ProductDB.AddProduct(product)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        Else
            ' Actualizacion de registro

            product.Id = productID
            product.Category = cmbCategory.SelectedValue.ToString
            product.Name = UCase(txtName.Text)
            product.Key = txtKey.Text
            product.Code = txtCode.Text
            product.Unit = cmbUnits.SelectedValue.ToString
            product.Type = cmbType.SelectedValue.ToString
            product.TareWeight = txtTareWeight.Text
            product.Description = txtDescription.Text
            'product.PriceSell = txtPriceSell.Text

            If txtTareWeight.Enabled = False Then
                product.TareWeight = 0
            Else
                product.TareWeight = txtTareWeight.Text
            End If

            Try
                ProductDB.UpdateProduct(product)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub

    Private Sub GetCategoriesList()
        cmbCategory.DataSource = ProductDB.GetCategoriesList
        cmbCategory.DisplayMember = "Name"
        cmbCategory.ValueMember = "Id"
    End Sub

    Private Sub GetUnitsList()
        cmbUnits.DataSource = ProductDB.GetUnitsList
        cmbUnits.DisplayMember = "unit_name"
        cmbUnits.ValueMember = "unit_id"
    End Sub

    Private Sub GetTypesList()
        cmbType.DataSource = ProductDB.GetTypeList
        cmbType.DisplayMember = "type_name"
        cmbType.ValueMember = "type_id"
    End Sub

    Private Sub getPricesList()

    End Sub

    Private Sub GetProduct(productID As Integer)
        Try
            product = ProductDB.GetProduct(productID)
            DataGridView1.DataSource = ProductDB.GetPrices(productID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub DisplayProduct()
        txtName.Text = product.Name
        txtKey.Text = product.Key
        txtDescription.Text = product.Description
        txtCode.Text = product.Code
        txtTareWeight.Text = product.TareWeight
        cmbCategory.SelectedValue = product.Category
        cmbUnits.SelectedValue = product.Unit
        cmbType.SelectedValue = product.Type

        If product.TareWeight > 0 Then
            CheckBox1.CheckState = CheckState.Checked
            txtTareWeight.Enabled = True
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) 
        If CheckBox1.CheckState = CheckState.Checked Then
            txtTareWeight.Enabled = True
        Else
            txtTareWeight.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class