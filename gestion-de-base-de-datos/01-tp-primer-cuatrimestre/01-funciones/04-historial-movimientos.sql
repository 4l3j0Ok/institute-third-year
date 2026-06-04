CREATE FUNCTION dbo.HistorialMovimientos (@id_producto int)
RETURNS TABLE
AS
RETURN
(
    SELECT p.nombre AS nombre_producto, c.fecha, 'Compra' AS tipo_movimiento, c.cantidad 
    FROM dbo.Compras_Producto c JOIN dbo.Producto p ON c.id_producto = p.ID_Producto WHERE p.ID_Producto = @id_producto
    UNION ALL
    SELECT p.nombre, pe.fecha, 'Pedido', pe.cantidad 
    FROM dbo.Pedidos_Producto pe JOIN dbo.Producto p ON pe.id_producto = p.ID_Producto WHERE p.ID_Producto = @id_producto
    UNION ALL
    SELECT p.nombre, r.fecha, 'Recepcion', r.cantidad 
    FROM dbo.Recepcion_Producto r JOIN dbo.Producto p ON r.id_producto = p.ID_Producto WHERE p.ID_Producto = @id_producto
);
