Imports System.ComponentModel

Public Class InvoicesAdmin
    Public Conn As Boolean = True

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.Razon = "" Then My.Settings.Razon = "Razón social"

        Me.Text = String.Format("{0} - {1}", Application.ProductName, My.Settings.Razon)

        txtStatus.Text = "Conectando a la base de datos..."

    End Sub
    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()

    End Sub
    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
        Dim frmAbout As New AboutBox

        frmAbout.ShowDialog()
    End Sub

    Private Sub ClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientesToolStripMenuItem.Click
        Dim frmCustomers As New CustomerSearch

        frmCustomers.ShowDialog()
    End Sub

    Private Sub ProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductosToolStripMenuItem.Click
        Dim frmProducts As New ProductSearch

        frmProducts.ShowDialog()
    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If MessageBox.Show("Esta seguro de cerrar la aplicación?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            ' Cancel the Closing event from closing the form.
            e.Cancel = True
        End If
    End Sub

    Private Sub ConfigurarPaginaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurarPaginaToolStripMenuItem.Click
        ' Initialize the dialog's PrinterSettings property to hold user
        ' defined printer settings.
        PageSetupDialog1.PageSettings =
            New System.Drawing.Printing.PageSettings

        ' Initialize dialog's PrinterSettings property to hold user
        ' set printer settings.
        PageSetupDialog1.PrinterSettings =
            New System.Drawing.Printing.PrinterSettings

        'Do not show the network in the printer dialog.
        PageSetupDialog1.ShowNetwork = False

        'Show the dialog storing the result.
        Dim result As DialogResult = PageSetupDialog1.ShowDialog()

        ' If the result is OK, display selected settings in
        ' ListBox1. These values can be used when printing the
        ' document.
        If (result = DialogResult.OK) Then
            Dim results() As Object = New Object() _
                {PageSetupDialog1.PageSettings.Margins,
                 PageSetupDialog1.PageSettings.PaperSize,
                 PageSetupDialog1.PageSettings.Landscape,
                 PageSetupDialog1.PrinterSettings.PrinterName,
                 PageSetupDialog1.PrinterSettings.PrintRange}

        End If
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem.Click
        Dim frmInvoice As New InvoiceBox

        frmInvoice.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim frmInvoice As New InvoiceBox

        frmInvoice.ShowDialog()
    End Sub

    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        Try
            tableview = InvoiceDB.GetAllInvoices

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = tableview
            DataGridView1.AutoResizeColumns()
            DataGridView1.Refresh()
        Catch ex As Exception
            Me.Conn = False
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub OpcionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpcionesToolStripMenuItem.Click
        Dim frmConfig As New ConfigBox

        frmConfig.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Dim rowindex As Integer
        Dim frmProduct As New ProductBox

        rowindex = DataGridView1.CurrentRow.Index

        'frmProduct.SearchProduct(DataGridView1(0, rowindex).Value)
        frmProduct.ShowDialog()

    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosToolStripMenuItem.Click
        Dim frmUsers As New UserSearch

        frmUsers.ShowDialog()
    End Sub

    Private Sub AlmacenesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlmacenesToolStripMenuItem.Click
        Dim frmStores As New StoreSearch

        frmStores.ShowDialog()
    End Sub

    Private Sub CategoríasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoríasToolStripMenuItem.Click
        Dim frmCategories As New CategorySearch

        frmCategories.ShowDialog()
    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'MsgBox("You have performed Left Mouse Click")
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(Windows.Forms.Cursor.Position.X, Windows.Forms.Cursor.Position.Y)
        End If
    End Sub

    Private Sub InvoicesAdmin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Me.Conn = True Then
            Me.FillDatagrid()
        End If
    End Sub

    Private Sub tmrNetwork_Tick(sender As Object, e As EventArgs) Handles tmrNetwork.Tick
        If MySqlDataBase.NetworkStatus() = True Then
            txtNetwork.Text = "Existe conexión al servidor"
        Else
            txtNetwork.Text = "Sin conexión al servidor..."
        End If
    End Sub
End Class