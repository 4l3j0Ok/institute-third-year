CREATE TABLE dbo.Factura
(
    numero_factura int NOT NULL PRIMARY KEY,
    fechahora smalldatetime NOT NULL DEFAULT GETDATE(),
    id_cliente int NOT NULL REFERENCES dbo.Cliente(id_cliente),
    montototal smallmoney NULL
)
