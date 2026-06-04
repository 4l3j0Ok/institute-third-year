CREATE FUNCTION dbo.PrecioRecorte (@id_producto int, @largo decimal(10,2), @ancho decimal(10,2))
RETURNS smallmoney
AS
BEGIN
    DECLARE @precio_m2 smallmoney;
    DECLARE @largo_orig decimal(10,2), @ancho_orig decimal(10,2);
    
    SELECT @precio_m2 = precio_m2, @largo_orig = largo_cm, @ancho_orig = ancho_cm 
    FROM dbo.Producto WHERE ID_Producto = @id_producto;
    IF @precio_m2 IS NULL OR @largo IS NULL OR @ancho IS NULL OR @largo > @largo_orig OR @ancho > @ancho_orig
        RETURN 0;

    RETURN dbo.SuperficieM2(@largo, @ancho) * @precio_m2;
END;