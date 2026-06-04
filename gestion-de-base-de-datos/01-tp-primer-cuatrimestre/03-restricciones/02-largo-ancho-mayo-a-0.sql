ALTER TABLE dbo.Producto
ADD CONSTRAINT CK_Producto_Dimensiones CHECK (largo_cm > 0 AND ancho_cm > 0);