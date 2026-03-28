' ============================================================================
' OperationResult.vb - Operation Result Model
' Petty Cash Management System
' ============================================================================
' Purpose: Standard result object for service operations
' Layer: Models
' ============================================================================

''' <summary>
''' Represents the result of a service operation.
''' </summary>
Public Class OperationResult

#Region "Properties"

    ''' <summary>
    ''' Whether the operation was successful.
    ''' </summary>
    Public Property IsSuccess As Boolean

    ''' <summary>
    ''' Error message if the operation failed.
    ''' </summary>
    Public Property ErrorMessage As String

    ''' <summary>
    ''' Success or informational message.
    ''' </summary>
    Public Property Message As String

#End Region

#Region "Static Factory Methods"

    ''' <summary>
    ''' Creates a successful result.
    ''' </summary>
    Public Shared Function Success(Optional message As String = "Operation completed successfully.") As OperationResult
        Return New OperationResult With {
            .IsSuccess = True,
            .Message = message
        }
    End Function

    ''' <summary>
    ''' Creates a failed result with an error message.
    ''' </summary>
    Public Shared Function Failure(errorMessage As String) As OperationResult
        Return New OperationResult With {
            .IsSuccess = False,
            .ErrorMessage = errorMessage
        }
    End Function

#End Region

End Class

''' <summary>
''' Represents the result of an operation that returns data.
''' </summary>
''' <typeparam name="T">Type of the returned data.</typeparam>
Public Class OperationResult(Of T)
    Inherits OperationResult

    ''' <summary>
    ''' The data returned by the operation.
    ''' </summary>
    Public Property Data As T

#Region "Static Factory Methods"

    ''' <summary>
    ''' Creates a successful result with data.
    ''' </summary>
    Public Shared Overloads Function Success(data As T, Optional message As String = "Operation completed successfully.") As OperationResult(Of T)
        Return New OperationResult(Of T) With {
            .IsSuccess = True,
            .Message = message,
            .Data = data
        }
    End Function

    ''' <summary>
    ''' Creates a failed result with an error message.
    ''' </summary>
    Public Shadows Shared Function Failure(errorMessage As String) As OperationResult(Of T)
        Return New OperationResult(Of T) With {
            .IsSuccess = False,
            .ErrorMessage = errorMessage,
            .Data = Nothing
        }
    End Function

#End Region

End Class
