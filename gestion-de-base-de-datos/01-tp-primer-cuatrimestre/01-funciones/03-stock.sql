CREATE FUNCTION dbo.StockActual (@id_producto int)
RETURNS int
AS
BEGIN
    DECLARE @comprado int, @vendido int;
    
    SELECT @comprado = ISNULL(SUM(cantidad), 0) FROM dbo.Recepcion_Producto WHERE id_producto = @id_producto;
    SELECT @vendido = ISNULL(SUM(cantidad), 0) FROM dbo.Renglon_factura WHERE id_producto = @id_producto;
    
    RETURN @comprado - @vendido;
END;