' ============================================================================
' Category.vb - Category Model
' Petty Cash Management System
' ============================================================================
' Purpose: Represents expense categories
' Layer: Models
' ============================================================================

''' <summary>
''' Represents an expense category.
''' </summary>
Public Class Category

#Region "Properties"

    ''' <summary>
    ''' Unique identifier for the category.
    ''' </summary>
    Public Property CategoryId As Integer

    ''' <summary>
    ''' Category code (E5200, E5300, E7800, E7510).
    ''' </summary>
    Public Property CategoryCode As String

    ''' <summary>
    ''' Descriptive name for the category.
    ''' </summary>
    Public Property CategoryName As String

    ''' <summary>
    ''' Timestamp when the category was created.
    ''' </summary>
    Public Property CreatedAt As DateTime

#End Region

#Region "Static Methods"

    ''' <summary>
    ''' Gets the default categories for the system.
    ''' </summary>
    Public Shared Function GetDefaultCategories() As List(Of Category)
        Return New List(Of Category) From {
            New Category With {.CategoryCode = "E5200", .CategoryName = "Vehicle Parts"},
            New Category With {.CategoryCode = "E5300", .CategoryName = "Office Items"},
            New Category With {.CategoryCode = "E7800", .CategoryName = "Physical Hardware"},
            New Category With {.CategoryCode = "E7510", .CategoryName = "Treatments & Staff"}
        }
    End Function

    ''' <summary>
    ''' Gets the category name by code.
    ''' </summary>
    Public Shared Function GetNameByCode(code As String) As String
        Select Case code?.ToUpper()
            Case "E5200" : Return "Vehicle Parts"
            Case "E5300" : Return "Office Items"
            Case "E7800" : Return "Physical Hardware"
            Case "E7510" : Return "Treatments & Staff"
            Case Else : Return "Unknown"
        End Select
    End Function

#End Region

End Class
