Public Class InvoiceBox
    Public addInvoice As Boolean
    Public invoiceID As Integer
    Private invoice As Invoice
    Private Sub InvoiceBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutocompleteItems()

        If addInvoice Then
            ' New ticket
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Nueva venta")

            cmbCustomer.SelectedIndex = 1
        Else
            'Edit ticket
            Me.Text = String.Format("{0} - {1}", Application.ProductName, "Editar venta")

            Me.DisplayInvoice()
        End If
    End Sub
    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = InvoiceDB.GetAllProductsOfInvoice(invoiceID)

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)

            With DataGridView1
                .AutoGenerateColumns = True
                .DataSource = tableview
                .AutoResizeColumns()
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Refresh()
            End With
        Catch ex As Exception
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub GetInvoice(invoiceID As Integer)
        Try
            invoice = InvoiceDB.GetInvoice(invoiceID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub
    Public Sub AutocompleteItems()
        ' Fill dropdown lists
        With cmbCustomer
            .DataSource = InvoiceDB.GetCustomersList()
            .DisplayMember = "customer_name"
            .ValueMember = "customer_id"
        End With

        With cmbProduct
            .DataSource = InvoiceDB.GetProductsList()
            .DisplayMember = "product_name"
            .ValueMember = "product_id"
        End With
    End Sub
    Private Sub UpdateInvoice()
        Dim product As New Product
        product = ProductDB.GetProduct(cmbProduct.SelectedValue)

        If InvoiceDB.AddProductToInvoice(invoiceID, cmbProduct.SelectedValue, txtQuantity.Text, product.Price1) = True Then
            Me.DisplayInvoice()
        End If
    End Sub
    Private Sub DisplayInvoice()
        Me.GetInvoice(invoiceID)

        txtInvoiceID.Text = invoice.Id
        txtDate.Text = invoice.Timestamp.ToLongDateString
        cmbCustomer.SelectedValue = invoice.Customer
        txtTotal.Text = invoice.Total

        FillDatagrid()
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.D Then
            Me.DeleteProduct()
        End If
    End Sub
    Private Sub DeleteProduct()
        If DataGridView1.RowCount > 1 Then
            If MessageBox.Show("Esta seguro de remover el producto seleccionado?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                ' Remove row
                InvoiceDB.DeleteProductForomInvoice(DataGridView1(0, DataGridView1.CurrentRow.Index).Value, DataGridView1(1, DataGridView1.CurrentRow.Index).Value)
                ' Refresh
                Me.DisplayInvoice()
            End If
        Else
            MessageBox.Show("Añada un producto y luego elimine este producto", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Me.UpdateInvoice()
    End Sub
End Class