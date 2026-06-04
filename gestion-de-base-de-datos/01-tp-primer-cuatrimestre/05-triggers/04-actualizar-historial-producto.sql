CREATE OR ALTER TRIGGER dbo.tr_producto_historial_precio_plancha
ON dbo.Producto
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(precio_plancha)
    BEGIN
        INSERT INTO dbo.His_Producto
            (ID_Producto, precio_plancha, fecha_hasta)
        SELECT d.ID_Producto,
            d.precio_plancha,
            CAST(GETDATE() AS date)
        FROM deleted d
            INNER JOIN inserted i ON i.ID_Producto = d.ID_Producto
        WHERE ISNULL(d.precio_plancha, 0) <> ISNULL(i.precio_plancha, 0);
    END
END;