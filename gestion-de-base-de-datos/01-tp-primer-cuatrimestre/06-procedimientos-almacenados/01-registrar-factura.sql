CREATE OR ALTER PROCEDURE dbo.RegistrarFactura
    @id_cliente int,
    @numero_factura int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE id_cliente = @id_cliente)
    BEGIN
        RAISERROR('El cliente no existe.', 16, 1);
        RETURN;
    END;

    SELECT @numero_factura = ISNULL(MAX(numero_factura), 0) + 1
    FROM dbo.Factura;

    INSERT INTO dbo.Factura (numero_factura, fechahora, id_cliente, montototal)
    VALUES (@numero_factura, GETDATE(), @id_cliente, 0);
END;
