CREATE TRIGGER dbo.tr_producto_precio_m2_superficie_cero
ON dbo.Producto
AFTER INSERT, UPDATE
AS
BEGIN
    -- No mostrar output de filas afectadas por el trigger.
    SET NOCOUNT ON;

    UPDATE p
    SET precio_m2 = 0
    FROM dbo.Producto p
        INNER JOIN inserted i ON i.ID_Producto = p.ID_Producto
    WHERE ISNULL(i.superficie_m2, 0) = 0
        AND ISNULL(p.precio_m2, 0) <> 0;
END;