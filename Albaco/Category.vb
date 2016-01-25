Public Class Category
    Private varId As Integer
    Private varName As String
    Private varDescription As String

    Sub New()

    End Sub

    Public Property Id As Integer
        Get
            Return Me.varId
        End Get
        Set(value As Integer)
            Me.varId = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return Me.varName
        End Get
        Set(value As String)
            Me.varName = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return Me.varDescription
        End Get
        Set(value As String)
            Me.varDescription = value
        End Set
    End Property
End Class
