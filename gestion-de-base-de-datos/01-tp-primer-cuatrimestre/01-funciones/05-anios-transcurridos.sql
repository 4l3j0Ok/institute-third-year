CREATE FUNCTION dbo.AniosTranscurridos (@fecha date)
RETURNS int
AS
BEGIN
    IF @fecha IS NULL RETURN 0;
    RETURN DATEDIFF(YEAR, @fecha, GETDATE());
END;