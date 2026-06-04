SELECT
    ID_Producto AS [@ID_Producto],
    nombre AS [@nombre],
    precio_plancha AS [@precio_plancha],
    largo_cm AS [@largo_cm],
    ancho_cm AS [@ancho_cm]
FROM dbo.Producto
FOR XML PATH('Producto'), ROOT('Productos');
GO

DECLARE @productos_xml xml;

SET @productos_xml = (
    SELECT
        ID_Producto,
        nombre,
        precio_plancha,
        largo_cm,
        ancho_cm
    FROM dbo.Producto
    FOR XML PATH('Producto'), ROOT('Productos')
);

INSERT INTO dbo.Proveedor (nombre, fecha_lista_precios, productos_XML, productos_JSON)
VALUES ('Proveedor XML', GETDATE(), @productos_xml, NULL);
