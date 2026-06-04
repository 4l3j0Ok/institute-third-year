CREATE OR ALTER PROCEDURE dbo.RegistrarRenglonFactura_TryCatch
    @numero_factura int,
    @id_producto int,
    @cantidad smallint,
    @largo_cm tinyint = NULL,
    @ancho_cm tinyint = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @numero_renglon tinyint;
        DECLARE @precio_unitario smallmoney;
        DECLARE @precio_total money;

        SELECT @numero_renglon = ISNULL(MAX(numero_renglon), 0) + 1
        FROM dbo.Renglon_factura
        WHERE numero_factura = @numero_factura;

        SELECT @precio_unitario =
            CASE
                WHEN @largo_cm IS NOT NULL AND @ancho_cm IS NOT NULL
                    THEN dbo.PrecioRecorte(@id_producto, @largo_cm, @ancho_cm)
                ELSE precio_plancha
            END
        FROM dbo.Producto
        WHERE ID_Producto = @id_producto;

        SET @precio_total = @precio_unitario * @cantidad;

        INSERT INTO dbo.Renglon_factura
            (numero_factura, numero_renglon, id_producto, cantidad, largo_cm, ancho_cm, preciounitario, preciototal)
        VALUES
            (@numero_factura, @numero_renglon, @id_producto, @cantidad, @largo_cm, @ancho_cm, @precio_unitario, @precio_total);

        UPDATE dbo.Factura
        SET montototal = ISNULL(montototal, 0) + @precio_total
        WHERE numero_factura = @numero_factura;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
