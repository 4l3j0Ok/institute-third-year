ALTER TABLE dbo.Producto
ADD stock int NULL;
GO
UPDATE dbo.Producto
SET stock = dbo.StockActual(ID_Producto);