' ============================================================================
' IRepository.vb - Repository Interface
' Petty Cash Management System
' ============================================================================
' Purpose: Defines the contract for all repository classes
' Layer: Data Access Layer
' ============================================================================

''' <summary>
''' Generic repository interface for CRUD operations.
''' </summary>
''' <typeparam name="T">The entity type.</typeparam>
Public Interface IRepository(Of T)

    ''' <summary>
    ''' Gets an entity by its ID.
    ''' </summary>
    Function GetById(id As Integer) As T

    ''' <summary>
    ''' Gets all entities.
    ''' </summary>
    Function GetAll() As List(Of T)

    ''' <summary>
    ''' Adds a new entity and returns the new ID.
    ''' </summary>
    Function Add(entity As T) As Integer

    ''' <summary>
    ''' Updates an existing entity.
    ''' </summary>
    Sub Update(entity As T)

    ''' <summary>
    ''' Deletes an entity by ID.
    ''' </summary>
    Sub Delete(id As Integer)

End Interface
