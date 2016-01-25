Public Class Price
    Private varId As Integer
    Private varPrice As Double

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

    Public Property Price As Double
        Get
            Return Me.varPrice
        End Get
        Set(value As Double)
            Me.varPrice = value
        End Set
    End Property
End Class
