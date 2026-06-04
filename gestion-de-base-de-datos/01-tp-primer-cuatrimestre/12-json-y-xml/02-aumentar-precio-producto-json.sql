DECLARE @id_proveedor int = 2;
DECLARE @id_producto int = 1;
DECLARE @porcentaje_aumento decimal(5,2) = 10;
DECLARE @posicion int;
DECLARE @precio_actual decimal(10,2);
DECLARE @nuevo_precio decimal(10,2);

SELECT @posicion = j.[key],
    @precio_actual = JSON_VALUE(j.value, '$.precio_plancha')
FROM dbo.Proveedor p
CROSS APPLY OPENJSON(p.productos_JSON, '$.Productos') j
WHERE p.id_proveedor = @id_proveedor
    AND JSON_VALUE(j.value, '$.ID_Producto') = @id_producto;

SET @nuevo_precio = @precio_actual * (1 + @porcentaje_aumento / 100);

UPDATE dbo.Proveedor
SET productos_JSON = JSON_MODIFY
(
    productos_JSON,
    CONCAT('$.Productos[', @posicion, '].precio_plancha'),
    @nuevo_precio
)
WHERE id_proveedor = @id_proveedor
    AND productos_JSON IS NOT NULL;
