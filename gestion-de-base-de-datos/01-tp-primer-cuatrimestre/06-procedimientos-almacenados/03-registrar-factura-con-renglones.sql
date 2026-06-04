IF TYPE_ID('dbo.TipoRenglonesFactura') IS NULL
    CREATE TYPE dbo.TipoRenglonesFactura AS TABLE
    (
        id_producto int NOT NULL,
        cantidad smallint NOT NULL,
        largo_cm tinyint NULL,
        ancho_cm tinyint NULL
    );
GO

CREATE OR ALTER PROCEDURE dbo.RegistrarFacturaConRenglones
    @id_cliente int,
    @renglones dbo.TipoRenglonesFactura READONLY,
    @numero_factura int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE id_cliente = @id_cliente)
    BEGIN
        RAISERROR('El cliente no existe.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (SELECT 1 FROM @renglones)
    BEGIN
        RAISERROR('La factura debe tener al menos un renglón.', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM @renglones WHERE cantidad <= 0)
    BEGIN
        RAISERROR('Todos los renglones deben tener cantidad mayor a cero.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM @renglones r
        LEFT JOIN dbo.Producto p ON p.ID_Producto = r.id_producto
        WHERE p.ID_Producto IS NULL
    )
    BEGIN
        RAISERROR('Uno o más productos no existen.', 16, 1);
        RETURN;
    END;

    SELECT @numero_factura = ISNULL(MAX(numero_factura), 0) + 1
    FROM dbo.Factura;

    INSERT INTO dbo.Factura (numero_factura, fechahora, id_cliente, montototal)
    VALUES (@numero_factura, GETDATE(), @id_cliente, 0);

    INSERT INTO dbo.Renglon_factura
        (numero_factura, numero_renglon, id_producto, cantidad, largo_cm, ancho_cm, preciounitario, preciototal)
    SELECT
        @numero_factura,
        ROW_NUMBER() OVER (ORDER BY r.id_producto),
        r.id_producto,
        r.cantidad,
        r.largo_cm,
        r.ancho_cm,
        CASE
            WHEN r.largo_cm IS NOT NULL AND r.ancho_cm IS NOT NULL
                THEN dbo.PrecioRecorte(r.id_producto, r.largo_cm, r.ancho_cm)
            ELSE p.precio_plancha
        END,
        r.cantidad *
        CASE
            WHEN r.largo_cm IS NOT NULL AND r.ancho_cm IS NOT NULL
                THEN dbo.PrecioRecorte(r.id_producto, r.largo_cm, r.ancho_cm)
            ELSE p.precio_plancha
        END
    FROM @renglones r
    INNER JOIN dbo.Producto p ON p.ID_Producto = r.id_producto;

    UPDATE dbo.Factura
    SET montototal = (
        SELECT SUM(preciototal)
        FROM dbo.Renglon_factura
        WHERE numero_factura = @numero_factura
    )
    WHERE numero_factura = @numero_factura;
END;
