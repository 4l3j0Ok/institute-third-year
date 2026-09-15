--SQL SERVER SYNTAX


CREATE OR ALTER FUNCTION dbo.SumarNumeros(@num1 INT = 0, @num2 INT = 0) RETURNS INT
AS
BEGIN
    RETURN @num1 + @num2
END;

GO

CREATE OR ALTER FUNCTION dbo.RestarNumeros(@num1 INT = 0, @num2 INT = 0) RETURNS INT
AS
BEGIN
    RETURN @num1 - @num2
END;

GO

CREATE OR ALTER FUNCTION dbo.DiferenciaSumaYResta(@num1 INT = 0, @num2 INT = 0) RETURNS INT
AS
BEGIN
    DECLARE @result_suma INT = dbo.SumarNumeros(@num1, @num2)
    DECLARE @result_resta INT = dbo.RestarNumeros(@num1, @num2)
    DECLARE @diferencia INT = @result_suma - @result_resta
    RETURN @diferencia
END;

GO

SELECT dbo.DiferenciaSumaYResta(2,1) as "Resultado"; -- Esperado: 2

