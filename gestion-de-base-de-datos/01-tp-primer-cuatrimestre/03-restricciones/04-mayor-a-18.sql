ALTER TABLE dbo.Cliente
ADD CONSTRAINT CK_Cliente_Edad CHECK (FechaNacimiento <= DATEADD(YEAR, -18, GETDATE()));