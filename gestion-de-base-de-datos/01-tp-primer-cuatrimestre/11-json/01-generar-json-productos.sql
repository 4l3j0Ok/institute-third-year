DECLARE @productos_json varchar(max);

SET @productos_json = (
    SELECT
        ID_Producto,
        nombre,
        precio_plancha,
        largo_cm,
        ancho_cm
    FROM dbo.Producto
    FOR JSON PATH, ROOT('Productos')
);

INSERT INTO dbo.Proveedor (nombre, fecha_lista_precios, productos_XML, productos_JSON)
VALUES ('Proveedor JSON', GETDATE(), NULL, @productos_json);
