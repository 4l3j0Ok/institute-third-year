-- ============================================
-- TABLAS BASE
-- PKs NONCLUSTERED para dejar libre el CLUSTERED
-- ============================================

CREATE TABLE dbo.Departamento (
    deptoNro INT NOT NULL,
    nombredepto VARCHAR(50),
    sueldototal MONEY,
    director INT NULL,

    CONSTRAINT PK_Departamento
        PRIMARY KEY NONCLUSTERED (deptoNro)
);
GO


CREATE TABLE dbo.Empleado (
    dni INT NOT NULL,
    nombre VARCHAR(30),
    apellidos VARCHAR(30),
    fechanac DATE,
    direccion VARCHAR(50),
    sexo VARCHAR(10),
    sueldo SMALLMONEY,
    DNIsupervisor INT NULL,
    deptoNro INT NULL,

    CONSTRAINT PK_Empleado
        PRIMARY KEY NONCLUSTERED (dni),

    CONSTRAINT FK_Empleado_Supervisor
        FOREIGN KEY (DNIsupervisor)
        REFERENCES dbo.Empleado(dni),

    CONSTRAINT FK_Empleado_Departamento
        FOREIGN KEY (deptoNro)
        REFERENCES dbo.Departamento(deptoNro)
);
GO


CREATE TABLE dbo.Proyecto (
    nroproy INT NOT NULL,
    nombreproyecto VARCHAR(50),
    deptoNro INT NULL,

    CONSTRAINT PK_Proyecto
        PRIMARY KEY NONCLUSTERED (nroproy),

    CONSTRAINT FK_Proyecto_Departamento
        FOREIGN KEY (deptoNro)
        REFERENCES dbo.Departamento(deptoNro)
);
GO


CREATE TABLE dbo.Trabaja_en (
    dni INT NOT NULL,
    nroproy INT NOT NULL,
    horas INT,

    CONSTRAINT PK_Trabaja_en
        PRIMARY KEY NONCLUSTERED (dni, nroproy),

    CONSTRAINT FK_TrabajaEn_Empleado
        FOREIGN KEY (dni)
        REFERENCES dbo.Empleado(dni),

    CONSTRAINT FK_TrabajaEn_Proyecto
        FOREIGN KEY (nroproy)
        REFERENCES dbo.Proyecto(nroproy)
);
GO


-- Departamento.director -> Empleado.dni
-- Se agrega después por la referencia circular
ALTER TABLE dbo.Departamento
ADD CONSTRAINT FK_Departamento_Director
    FOREIGN KEY (director)
    REFERENCES dbo.Empleado(dni);
GO


-------

-- a) SELECT * FROM empleado WHERE fechanac BETWEEN 'dd/mm/aaaa' AND 'dd/mm/aaaa' ORDER BY fechanac DESC

CREATE CLUSTERED INDEX IX_Empleado_FechaNac ON Empleado(fechanac DESC)

-- b) SELECT * FROM empleado WHERE dni = xx.xxx.xxx AND sexo = 'M'

CREATE UNIQUE NONCLUSTERED INDEX IX_Empleado_Dni ON Empleado(dni ASC)

-- c) SELECT * FROM empleado WHERE apellidos = 'UnApellido' AND nombre = 'UnNombre' ORDER BY apellidos,
-- nombre DESC

CREATE NONCLUSTERED INDEX IX_Empleado_ApellidoNombre ON Empleado(apellidos ASC, nombre DESC)

-- d) SELECT * FROM departamento d INNER JOIN empleado e ON d.director = e.dni WHERE d.deptoNro = xxxxx

CREATE UNIQUE NONCLUSTERED INDEX IX_Departamento_DeptoNro ON Departamento(deptoNro ASC)

-- e) SELECT e.dni, e.nombre, e.apellidos FROM empleado e INNER JOIN Trabaja_en t ON e.dni = t.dni INNER
-- JOIN Proyecto p ON t.nroproy = p.nroproy WHERE p.nombreproyecto = 'UnProyectoCualquiera'

CREATE NONCLUSTERED INDEX IX_Proyecto_Nombre ON Proyecto(nombreproyecto ASC)
CREATE UNIQUE NONCLUSTERED INDEX IX_Proyecto_NroProyecto ON Proyecto(nroproy ASC)
CREATE NONCLUSTERED INDEX IX_TrabajaEn_NroProyecto ON Trabaja_en(nroproy ASC)
CREATE NONCLUSTERED INDEX IX_TrabajaEn_Dni ON Trabaja_en(dni ASC)
-- CREATE UNIQUE NONCLUSTERED INDEX IX_Empleado_Dni ON Empleado(dni ASC) -- Ya existe


-- f) Crear un índice de cobertura para la consulta del punto e).

CREATE NONCLUSTERED INDEX IX_Empleado_Cobertura ON Empleado(dni ASC) INCLUDE (nombre, apellidos)

