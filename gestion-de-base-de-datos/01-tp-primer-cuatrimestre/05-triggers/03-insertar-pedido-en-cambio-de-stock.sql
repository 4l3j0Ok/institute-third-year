CREATE OR ALTER TRIGGER dbo.tr_renglon_factura_actualiza_stock
ON dbo.Renglon_factura
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    ;
    WITH
        Ventas
        AS
        (
            SELECT id_producto, SUM(cantidad) AS cantidad_vendida
            FROM inserted
            GROUP BY id_producto
        )
    UPDATE p
    SET stock = ISNULL(p.stock, 0) - v.cantidad_vendida
    FROM dbo.Producto p
        INNER JOIN Ventas v ON v.id_producto = p.ID_Producto;

    INSERT INTO dbo.Pedidos_Producto
        (id_producto, fecha, cantidad)
    SELECT p.ID_Producto,
        GETDATE(),
        ISNULL(uc.cantidad, 0)
    FROM dbo.Producto p
        INNER JOIN (
        SELECT DISTINCT id_producto
        FROM inserted
    ) i ON i.id_producto = p.ID_Producto
    OUTER APPLY (
        SELECT TOP 1
            cp.cantidad
        FROM dbo.Compras_Producto cp
        WHERE cp.id_producto = p.ID_Producto
        ORDER BY cp.fecha DESC
    ) uc
    WHERE p.stock < p.stock_minimo
        AND uc.cantidad IS NOT NULL;
END;