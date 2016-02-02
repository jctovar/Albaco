Public Class Product
    Private varId As Integer
    Private varName As String
    Private varCategory As Integer
    Private varKey As String
    Private varCode As String
    Private varType As Integer
    Private varUnit As Integer
    Private varTareWeight As Double
    Private varDescription As String
    Private varPrice1 As Double
    Private varPrice2 As Double
    Private varPrice3 As Double
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
    Public Property TareWeight As Double
        Get
            Return Me.varTareWeight
        End Get
        Set(value As Double)
            Me.varTareWeight = value
        End Set
    End Property
    Public Property Price1 As Double
        Get
            Return Me.varPrice1
        End Get
        Set(value As Double)
            Me.varPrice1 = value
        End Set
    End Property
    Public Property Price2 As Double
        Get
            Return Me.varPrice2
        End Get
        Set(value As Double)
            Me.varPrice2 = value
        End Set
    End Property
    Public Property Price3 As Double
        Get
            Return Me.varPrice3
        End Get
        Set(value As Double)
            Me.varPrice3 = value
        End Set
    End Property
    Public Property Category As Integer
        Get
            Return Me.varCategory
        End Get
        Set(value As Integer)
            Me.varCategory = value
        End Set
    End Property
    Public Property Unit As Integer
        Get
            Return Me.varUnit
        End Get
        Set(value As Integer)
            Me.varUnit = value
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
    Public Property Key As String
        Get
            Return Me.varKey
        End Get
        Set(value As String)
            Me.varKey = value
        End Set
    End Property
    Public Property Code As String
        Get
            Return Me.varCode
        End Get
        Set(value As String)
            Me.varCode = value
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
    Public Property Type As Integer
        Get
            Return Me.varType
        End Get
        Set(value As Integer)
            Me.varType = value
        End Set
    End Property
End Class
