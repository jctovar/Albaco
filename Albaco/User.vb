Public Class User
    Private varId As Integer
    Private varName As String
    Private varUsername As String
    Private varPhone As String
    Private varPassword As String
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

    Public Property Username As String
        Get
            Return Me.varUsername
        End Get
        Set(value As String)
            Me.varUsername = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return Me.varPassword
        End Get
        Set(value As String)
            Me.varPassword = value
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
