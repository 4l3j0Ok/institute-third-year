CREATE PROCEDURE dbo.EliminarEmpleado
    @dni INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        DELETE FROM dbo.Empleado WHERE dni = @dni

        COMMIT TRANSACTION
    END TRY

    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
    END CATCH
END;
