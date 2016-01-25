Public Class Invoice
    Private varId As Integer
    Private varTimestamp As Date

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

    Public Property Timestamp As Date
        Get
            Return Nothing
        End Get
        Set(value As Date)
        End Set
    End Property
End Class
