CREATE OR ALTER PROCEDURE dbo.ProcesarRecepcionProductos
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_producto int;
    DECLARE @fecha smalldatetime;
    DECLARE @cantidad int;
    DECLARE @cantidad_pedido int;

    DECLARE cur_recepcion CURSOR FOR
        SELECT id_producto, fecha, cantidad
    FROM dbo.Recepcion_Producto
    WHERE fecha_procesado IS NULL;

    OPEN cur_recepcion;

    FETCH NEXT FROM cur_recepcion
    INTO @id_producto, @fecha, @cantidad;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO dbo.Compras_Producto
            (id_producto, fecha, cantidad)
        VALUES
            (@id_producto, @fecha, @cantidad);

        SELECT TOP 1
            @cantidad_pedido = cantidad
        FROM dbo.Pedidos_Producto
        WHERE id_producto = @id_producto
        ORDER BY fecha;

        IF @cantidad_pedido IS NOT NULL
        BEGIN
            IF @cantidad_pedido <= @cantidad
            BEGIN
                DELETE TOP (1)
                FROM dbo.Pedidos_Producto
                WHERE id_producto = @id_producto;
            END
            ELSE
            BEGIN
                UPDATE dbo.Pedidos_Producto
                SET cantidad = cantidad - @cantidad
                WHERE id_producto = @id_producto
                    AND fecha = (
                      SELECT MIN(fecha)
                    FROM dbo.Pedidos_Producto
                    WHERE id_producto = @id_producto
                  );
            END;
        END;

        UPDATE dbo.Producto
        SET stock = ISNULL(stock, 0) + @cantidad
        WHERE ID_Producto = @id_producto;

        UPDATE dbo.Recepcion_Producto
        SET fecha_procesado = GETDATE()
        WHERE id_producto = @id_producto
            AND fecha = @fecha
            AND cantidad = @cantidad
            AND fecha_procesado IS NULL;

        SET @cantidad_pedido = NULL;

        FETCH NEXT FROM cur_recepcion
        INTO @id_producto, @fecha, @cantidad;
    END;

    CLOSE cur_recepcion;
    DEALLOCATE cur_recepcion;
END;
