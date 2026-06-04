CREATE OR ALTER PROCEDURE dbo.InsertarProductoConFoto
    @nombre varchar(50),
    @precio_plancha smallmoney,
    @precio_m2 smallmoney,
    @largo_cm smallint,
    @ancho_cm tinyint,
    @stock_minimo smallint,
    @foto varbinary(max)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Producto
        (nombre, precio_plancha, precio_m2, largo_cm, ancho_cm, superficie_m2, stock_minimo, foto)
    VALUES
        (
            @nombre,
            @precio_plancha,
            @precio_m2,
            @largo_cm,
            @ancho_cm,
            dbo.SuperficieM2(@largo_cm, @ancho_cm),
            @stock_minimo,
            @foto
        );
END;
GO

DECLARE @foto varbinary(max);

SELECT @foto = BulkColumn
FROM OPENROWSET(BULK 'C:\imagenes\producto.jpg', SINGLE_BLOB) AS imagen;

EXEC dbo.InsertarProductoConFoto
    @nombre = 'Producto con foto',
    @precio_plancha = 10000,
    @precio_m2 = 5000,
    @largo_cm = 200,
    @ancho_cm = 100,
    @stock_minimo = 5,
    @foto = @foto;
