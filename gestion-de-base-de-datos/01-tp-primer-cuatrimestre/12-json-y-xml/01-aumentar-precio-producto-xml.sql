DECLARE @id_proveedor int = 1;
DECLARE @id_producto int = 1;
DECLARE @porcentaje_aumento decimal(5,2) = 10;

UPDATE dbo.Proveedor
SET productos_XML.modify('
    replace value of
    (/Productos/Producto[ID_Producto = sql:variable("@id_producto")]/precio_plancha/text())[1]
    with
    (xs:decimal((/Productos/Producto[ID_Producto = sql:variable("@id_producto")]/precio_plancha/text())[1])
     * (1 + sql:variable("@porcentaje_aumento") div 100))
')
WHERE id_proveedor = @id_proveedor
  AND productos_XML IS NOT NULL;