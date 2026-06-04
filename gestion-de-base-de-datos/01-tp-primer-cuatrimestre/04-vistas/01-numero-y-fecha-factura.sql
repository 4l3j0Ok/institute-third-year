CREATE VIEW dbo.vw_DetalleFacturas AS
SELECT 
    f.numero_factura, 
    f.fechahora AS "Fecha y hora de la factura",
    c.NombreYApellido AS "Nombre del cliente", 
    rf.cantidad AS "Cantidad",
    p.nombre AS "Nombre del producto", 
    rf.preciototal AS "Precio total"
FROM dbo.Factura f
INNER JOIN dbo.Cliente c ON f.id_cliente = c.id_cliente
INNER JOIN dbo.Renglon_factura rf ON f.numero_factura = rf.numero_factura
INNER JOIN dbo.Producto p ON rf.id_producto = p.ID_Producto;