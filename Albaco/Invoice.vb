Public Class Invoice
    Private varId As Integer
    Private varAccountId As Integer
    Private varCustomerId As Integer
    Private varStatus As String
    Private varTotal As Double
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
    Public Property Account As Integer
        Get
            Return Me.varAccountId
        End Get
        Set(value As Integer)
            Me.varAccountId = value
        End Set
    End Property
    Public Property Customer As Integer
        Get
            Return Me.varCustomerId
        End Get
        Set(value As Integer)
            Me.varCustomerId = value
        End Set
    End Property
    Public Property Status As String
        Get
            Return Me.varStatus
        End Get
        Set(value As String)
            Me.varStatus = value
        End Set
    End Property
    Public Property Total As Double
        Get
            Return Me.varTotal
        End Get
        Set(value As Double)
            Me.varTotal = value
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
