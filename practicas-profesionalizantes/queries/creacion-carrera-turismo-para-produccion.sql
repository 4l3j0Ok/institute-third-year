/* Estados de carrera */
use instituto_db;
IF NOT EXISTS (
    SELECT 1
FROM CarreraEstados
WHERE Descripcion = 'Activa'
)
BEGIN
    INSERT INTO CarreraEstados
        (CarreraEstadoId, Descripcion)
    VALUES
        (
            ISNULL((SELECT MAX(CarreraEstadoId)
            FROM CarreraEstados), 0) + 1,
            'Activa'
    );
END

IF NOT EXISTS (
    SELECT 1
FROM CarreraEstados
WHERE Descripcion = 'Inactiva'
)
BEGIN
    INSERT INTO CarreraEstados
        (CarreraEstadoId, Descripcion)
    VALUES
        (
            ISNULL((SELECT MAX(CarreraEstadoId)
            FROM CarreraEstados), 0) + 1,
            'Inactiva'
    );
END

IF NOT EXISTS (
    SELECT 1
FROM CarreraEstados
WHERE Descripcion = 'Borrador'
)
BEGIN
    INSERT INTO CarreraEstados
        (CarreraEstadoId, Descripcion)
    VALUES
        (
            ISNULL((SELECT MAX(CarreraEstadoId)
            FROM CarreraEstados), 0) + 1,
            'Borrador'
    );
END


/* Carreras */

DECLARE @carreraEstadoId INT;

SELECT @carreraEstadoId = CarreraEstadoId
FROM CarreraEstados
WHERE Descripcion = 'Borrador';

IF NOT EXISTS (
    SELECT 1
FROM Carreras
WHERE Nombre = 'Tecnicatura Superior en Guia de Turismo'
)
BEGIN
    INSERT INTO Carreras
        (
        CarreraId,
        Titulo,
        Nombre,
        DescripcionCorta,
        JefeCatedra,
        AnioInicio,
        AnioFin,
        Activo,
        PlanEstudio,
        Resolucion,
        Correlatividades,
        ImagenDescriptiva,
        NumeroExpediente,
        CantidadHoras,
        Duracion,
        CarreraEstadoId,
        CarrerasCodigoBloque,
        CarrerasCodiBloque
        )
    VALUES
        (
            ISNULL((SELECT MAX(CarreraId)
            FROM Carreras), 0) + 1,
            'Tecnico Superior en Guia de Turismo',
            'Tecnicatura Superior en Guia de Turismo',
            'Turismo',
            '',
            2026,
            0,
            1,
            '',
            '',
            NULL,
            '',
            '',
            1792,
            3,
            @carreraEstadoId,
            1,
            NULL
    );
END


/* Años de la carrera */

DECLARE @carrId INT;

SELECT @carrId = CarreraId
FROM Carreras
WHERE Nombre = 'Tecnicatura Superior en Guia de Turismo';


IF NOT EXISTS (
    SELECT 1
FROM AniosCarreras
WHERE CarreraId = @carrId
    AND AnioCarrera = 1
)
BEGIN
    INSERT INTO AniosCarreras
        (
        AnioCarreraId,
        AnioCarrera,
        CantidadMaterias,
        CargaHorariaCompleta,
        CarreraId,
        AniosCarrerasCodigoBloque,
        AniosCarrerasCodiBloque
        )
    VALUES
        (
            ISNULL((SELECT MAX(AnioCarreraId)
            FROM AniosCarreras), 0) + 1,
            1,
            1,
            NULL,
            @carrId,
            @carrId,
            CONCAT('0', CAST(@carrId AS VARCHAR(10)), '1')
    );
END


IF NOT EXISTS (
    SELECT 1
FROM AniosCarreras
WHERE CarreraId = @carrId
    AND AnioCarrera = 2
)
BEGIN
    INSERT INTO AniosCarreras
        (
        AnioCarreraId,
        AnioCarrera,
        CantidadMaterias,
        CargaHorariaCompleta,
        CarreraId,
        AniosCarrerasCodigoBloque,
        AniosCarrerasCodiBloque
        )
    VALUES
        (
            ISNULL((SELECT MAX(AnioCarreraId)
            FROM AniosCarreras), 0) + 1,
            2,
            2,
            NULL,
            @carrId,
            @carrId,
            CONCAT('0', CAST(@carrId AS VARCHAR(10)), '2')
    );
END


IF NOT EXISTS (
    SELECT 1
FROM AniosCarreras
WHERE CarreraId = @carrId
    AND AnioCarrera = 3
)
BEGIN
    INSERT INTO AniosCarreras
        (
        AnioCarreraId,
        AnioCarrera,
        CantidadMaterias,
        CargaHorariaCompleta,
        CarreraId,
        AniosCarrerasCodigoBloque,
        AniosCarrerasCodiBloque
        )
    VALUES
        (
            ISNULL((SELECT MAX(AnioCarreraId)
            FROM AniosCarreras), 0) + 1,
            3,
            3,
            NULL,
            @carrId,
            @carrId,
            CONCAT('0', CAST(@carrId AS VARCHAR(10)), '3')
    );
END
