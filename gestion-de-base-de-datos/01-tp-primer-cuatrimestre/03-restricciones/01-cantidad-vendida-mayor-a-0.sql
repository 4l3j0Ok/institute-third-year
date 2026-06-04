ALTER TABLE dbo.Renglon_factura
ADD CONSTRAINT CK_Renglon_Cantidad CHECK (cantidad > 0);