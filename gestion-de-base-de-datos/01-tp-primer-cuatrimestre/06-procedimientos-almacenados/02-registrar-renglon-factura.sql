CREATE OR ALTER PROCEDURE dbo.RegistrarRenglonFactura
    @numero_factura int,
    @id_producto int,
    @cantidad smallint,
    @largo_cm tinyint = NULL,
    @ancho_cm tinyint = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @numero_renglon tinyint;
    DECLARE @precio_unitario smallmoney;
    DECLARE @precio_total money;

    IF NOT EXISTS (SELECT 1 FROM dbo.Factura WHERE numero_factura = @numero_factura)
    BEGIN
        RAISERROR('La factura no existe.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Producto WHERE ID_Producto = @id_producto)
    BEGIN
        RAISERROR('El producto no existe.', 16, 1);
        RETURN;
    END;

    IF @cantidad <= 0
    BEGIN
        RAISERROR('La cantidad debe ser mayor a cero.', 16, 1);
        RETURN;
    END;

    SELECT @numero_renglon = ISNULL(MAX(numero_renglon), 0) + 1
    FROM dbo.Renglon_factura
    WHERE numero_factura = @numero_factura;

    IF @largo_cm IS NOT NULL AND @ancho_cm IS NOT NULL
        SET @precio_unitario = dbo.PrecioRecorte(@id_producto, @largo_cm, @ancho_cm);
    ELSE
        SELECT @precio_unitario = precio_plancha
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
END;
