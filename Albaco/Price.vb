Public Class Price
    Private varPrice As Integer
    Private varProduct As Integer
    Private varPriceValue As Double

    Sub New()

    End Sub
    Public Property Price As Integer
        Get
            Return Me.varPrice
        End Get
        Set(value As Integer)
            Me.varPrice = value
        End Set
    End Property
    Public Property Product As Integer
        Get
            Return Me.varProduct
        End Get
        Set(value As Integer)
            Me.varProduct = value
        End Set
    End Property
    Public Property PriceValue As Double
        Get
            Return Me.varPriceValue
        End Get
        Set(value As Double)
            Me.varPriceValue = value
        End Set
    End Property
End Class
