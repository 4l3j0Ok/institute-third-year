-- OPCIÓN 1: Columna calculada no persistida.
-- El valor se calcula al momento de la consulta.
CREATE TABLE dbo.Producto(
    ID_Producto int NOT NULL IDENTITY (1,1) PRIMARY KEY,
    nombre varchar(50) NULL,
    precio_plancha smallmoney NULL,
    precio_m2 smallmoney NULL,
    largo_cm smallint NULL,
    ancho_cm tinyint NULL,
    superficie_m2 AS ((largo_cm * ancho_cm) / 10000.0),
    stock_minimo smallint NOT NULL,
    foto varbinary(max)
);
-- Opción 2: Columna calculada persistida
-- El valor se calcula al momento de la inserción o actualización y se almacena en disco.
-- Ideal si la consulta es muy frecuente o si el cálculo es costoso.
CREATE TABLE dbo.Producto(
    ID_Producto int NOT NULL IDENTITY (1,1) PRIMARY KEY,
    nombre varchar(50) NULL,
    precio_plancha smallmoney NULL,
    precio_m2 smallmoney NULL,
    largo_cm smallint NULL,
    ancho_cm tinyint NULL,
    superficie_m2 AS ((largo_cm * ancho_cm) / 10000.0) PERSISTED,
    stock_minimo smallint NOT NULL,
    foto varbinary(max)
);