SELECT TOP 0 *
INTO dbo.Producto_CopiaJSON
FROM dbo.Producto;
GO

DECLARE @productos_json varchar(max);

SELECT TOP 1 @productos_json = productos_JSON
FROM dbo.Proveedor
WHERE productos_JSON IS NOT NULL
ORDER BY id_proveedor DESC;

INSERT INTO dbo.Producto_CopiaJSON
    (nombre, precio_plancha, precio_m2, largo_cm, ancho_cm, superficie_m2, stock_minimo, foto)
SELECT
    nombre,
    precio_plancha,
    NULL,
    largo_cm,
    ancho_cm,
    dbo.SuperficieM2(largo_cm, ancho_cm),
    1,
    NULL
FROM OPENJSON(@productos_json, '$.Productos')
WITH
(
    ID_Producto int '$.ID_Producto',
    nombre varchar(50) '$.nombre',
    precio_plancha smallmoney '$.precio_plancha',
    largo_cm smallint '$.largo_cm',
    ancho_cm tinyint '$.ancho_cm'
);
