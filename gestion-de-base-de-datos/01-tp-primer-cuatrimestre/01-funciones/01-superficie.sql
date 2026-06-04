CREATE FUNCTION dbo.SuperficieM2 (@largo decimal(10,2), @ancho decimal(10,2))
RETURNS decimal(10,2)
AS
BEGIN
    IF @largo IS NULL OR @ancho IS NULL RETURN 0;
    RETURN (@largo / 100.0) * (@ancho / 100.0);
END;