CREATE TRIGGER tr_ejemplo ON Empleado
AFTER INSERT
AS
BEGIN
    IF NOT UPDATE(sueldo)
        RETURN;
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN DELETED d
            ON i.dni = d.dni
        WHERE i.sueldo < d.sueldo
    )
    BEGIN
        RAISERROR(
            'El sueldo no puede ser menor',
            16,
            1
        );
        ROLLBACK TRANSACTION;

        RETURN
    END
END;

