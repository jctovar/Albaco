Public Class Customer
    Private varId As Integer
    Private varName As String
    Private varAddress_1 As String
    Private varAddress_2 As String
    Private varPostalCode As String
    Private varPhone As String
    Private varEmail As String

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

    Public Property Address_1 As String
        Get
            Return Me.varAddress_1
        End Get
        Set(value As String)
            Me.varAddress_1 = value
        End Set
    End Property

    Public Property Address_2 As String
        Get
            Return Me.varAddress_2
        End Get
        Set(value As String)
            Me.varAddress_2 = value
        End Set
    End Property

    Public Property PostalCode As String
        Get
            Return Me.varPostalCode
        End Get
        Set(value As String)
            Me.varPostalCode = value
        End Set
    End Property

    Public Property Phone As String
        Get
            Return Me.varPhone
        End Get
        Set(value As String)
            Me.varPhone = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return Me.varEmail
        End Get
        Set(value As String)
            Me.varEmail = value
        End Set
    End Property
End Class