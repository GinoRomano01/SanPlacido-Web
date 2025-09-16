use SanPlacido

go

GO
CREATE PROCEDURE sp_verProductoPorId
    @IdProducto INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.Id AS IdProducto,
        p.NombredelProducto,
        p.URLImagen,
        p.PrecioUnitario,
        c.Nombre AS Categoria,
        dp.Id AS IdDetallesdeProducto,
        dp.Largo,
        dp.Ancho,
        dp.Alto,
        tp.Nombre AS TipoProducto,
        tm.Nombre AS TipoMadera,
        ta.Nombre AS TipoAcabado
    FROM Producto p
    INNER JOIN DetallesdeProducto dp 
        ON p.IdDetallesdeProducto = dp.Id
    INNER JOIN Categoria c 
        ON p.IdCategoria = c.Id
    INNER JOIN TipodeProducto tp 
        ON dp.IdTipodeProducto = tp.Id
    INNER JOIN TipodeMadera tm 
        ON dp.IdTipodeMadera = tm.Id
    INNER JOIN TipodeAcabado ta 
        ON dp.IdTipodeAcabado = ta.Id
    WHERE 
        p.FechaBorrado IS NULL
        AND dp.FechaBorrado IS NULL
        AND c.FechaBorrado IS NULL
        AND tp.FechaBorrado IS NULL
        AND tm.FechaBorrado IS NULL
        AND ta.FechaBorrado IS NULL
        AND p.Id = @IdProducto;
END


go
CREATE PROCEDURE DetallesdeProductoU
    @Id INT,
    @Ancho DECIMAL(10,2),
    @Largo DECIMAL(10,2),
    @Alto DECIMAL(10,2),
    @IdTipodeProducto INT,
    @IdTipodeMadera INT,
    @IdTipodeAcabado INT
AS
BEGIN
    UPDATE DetallesdeProducto
    SET Ancho = @Ancho,
        Largo = @Largo,
        Alto = @Alto,
        IdTipodeProducto = @IdTipodeProducto,
        IdTipodeMadera = @IdTipodeMadera,
        IdTipodeAcabado = @IdTipodeAcabado
    WHERE Id = @Id AND FechaBorrado IS NULL
END
GO

CREATE PROCEDURE ProductoU
    @IdProducto INT,
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(10,2),
    @Url NVARCHAR(500),
    @IdCategoria INT,
    @Ancho DECIMAL(10,2),
    @Largo DECIMAL(10,2),
    @Alto DECIMAL(10,2),
    @IdTipodeProducto INT,
    @IdTipodeMadera INT,
    @IdTipodeAcabado INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdDetallesdeProducto INT;

    -- Obtener el detalle que corresponde al producto
    SELECT @IdDetallesdeProducto = IdDetallesdeProducto
    FROM Producto
    WHERE Id = @IdProducto;

    IF @IdDetallesdeProducto IS NULL
    BEGIN
        RAISERROR('El producto no existe o no tiene detalle asociado', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Actualizar detalles
        UPDATE DetallesdeProducto
        SET Ancho = @Ancho,
            Largo = @Largo,
            Alto = @Alto,
            IdTipodeProducto = @IdTipodeProducto,
            IdTipodeMadera = @IdTipodeMadera,
            IdTipodeAcabado = @IdTipodeAcabado
        WHERE Id = @IdDetallesdeProducto;

        -- Actualizar producto
        UPDATE Producto
        SET NombredelProducto = @Nombre,
            PrecioUnitario = @Precio,
            URLImagen = @Url,
            IdCategoria = @IdCategoria
        WHERE Id = @IdProducto;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END
/*
CREATE PROCEDURE ProductoU
    @IdProducto INT,
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(10,2),
    @Url NVARCHAR(500),
    @IdCategoria INT,
    @IdDetallesdeProducto INT,
    @Ancho DECIMAL(10,2),
    @Largo DECIMAL(10,2),
    @Alto DECIMAL(10,2),
    @IdTipodeProducto INT,
    @IdTipodeMadera INT,
    @IdTipodeAcabado INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1️ Actualizar la tabla DetallesdeProducto
    UPDATE DetallesdeProducto
    SET Ancho = @Ancho,
        Largo = @Largo,
        Alto = @Alto,
        IdTipodeProducto = @IdTipodeProducto,
        IdTipodeMadera = @IdTipodeMadera,
        IdTipodeAcabado = @IdTipodeAcabado
    WHERE Id = @IdDetallesdeProducto;

    -- 2️ Actualizar la tabla Producto (sin tocar IdDetallesdeProducto)
    UPDATE Producto
    SET NombredelProducto = @Nombre,
        PrecioUnitario = @Precio,
        URLImagen = @Url,
        IdCategoria = @IdCategoria
    WHERE Id = @IdProducto;
END

go
*/
-- Eliminar
CREATE PROCEDURE ProductoD
    @Id INT
AS
BEGIN
    UPDATE Producto
    SET FechaBorrado = GETDATE()
    WHERE Id = @Id
END
GO
GO


go
CREATE PROCEDURE CategoriaS
AS
BEGIN
    SELECT Id, Nombre FROM Categoria WHERE FechaBorrado IS NULL
END
GO

-- SP para Tipo de Producto
CREATE PROCEDURE TipoProductoS
AS
BEGIN
    SELECT Id, Nombre FROM TipodeProducto WHERE FechaBorrado IS NULL
END
GO

-- SP para Tipo de Madera
CREATE PROCEDURE TipoMaderaS
AS
BEGIN
    SELECT Id, Nombre FROM TipodeMadera WHERE FechaBorrado IS NULL
END
GO

-- SP para Tipo de Acabado
CREATE PROCEDURE TipoAcabadoS
AS
BEGIN
    SELECT Id, Nombre FROM TipodeAcabado WHERE FechaBorrado IS NULL
END
GO


CREATE PROCEDURE sp_getLocalidades
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre FROM Localidad WHERE FechaBorrado IS NULL;
END
GO

CREATE PROCEDURE sp_getTiposDeDni
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre FROM TipodeDni WHERE FechaBorrado IS NULL;
END
GO



go
create procedure sp_verProductos
as
begin
	SET NOCOUNT ON;

    SELECT 
        p.Id AS IdProducto,
        p.NombredelProducto,
        p.URLImagen,
        p.PrecioUnitario,
        c.Nombre AS Categoria,
        dp.Largo,
        dp.Ancho,
        dp.Alto,
        tp.Nombre AS TipoProducto,
        tm.Nombre AS TipoMadera,
        ta.Nombre AS TipoAcabado
    FROM Producto p
    INNER JOIN DetallesdeProducto dp 
        ON p.IdDetallesdeProducto = dp.Id
    INNER JOIN Categoria c 
        ON p.IdCategoria = c.Id
    INNER JOIN TipodeProducto tp 
        ON dp.IdTipodeProducto = tp.Id
    INNER JOIN TipodeMadera tm 
        ON dp.IdTipodeMadera = tm.Id
    INNER JOIN TipodeAcabado ta 
        ON dp.IdTipodeAcabado = ta.Id
    WHERE 
        p.FechaBorrado IS NULL
        AND dp.FechaBorrado IS NULL
        AND c.FechaBorrado IS NULL
        AND tp.FechaBorrado IS NULL
        AND tm.FechaBorrado IS NULL
        AND ta.FechaBorrado IS NULL;
END;



go
create procedure sp_verUsuarios

as
begin
	select
	c.Id as IdCliente,
	c.DNI,
	c.Nombre,
	c.Apellido,
	c.Telefono,
	c.Calle,
	c.Numero,
	c.IdLocalidad,
	c.IdTipodeDni,
	u.Id as IdUsuario,
	u.NombredeUsuario,
	u.Contraseña,
	u.CorreoElectronico,
	u.IdTipodeUsuario,
	u.IdTipodeRol,
	u.IdCliente,
	r.Id as IdRol,
	r.Nombre,
	t.Id as IdTipodeUsuario,
	t.Nombre
	from Usuario as u
	inner join Clientes as c on u.IdCliente = c.Id
	inner join  TipodeRol as r on u.IdTipoderol = r.Id
	inner join TipodeUsuario as t on u.IdTipodeUsuario = t.Id
	where u.FechaBorrado is null
end
go

CREATE PROCEDURE Producto_Insertar
(
    @NombredelProducto VARCHAR(100),
    @URLImagen VARCHAR(500),
    @PrecioUnitario DECIMAL(10,2),
    @IdCategoria INT,
    @Ancho DECIMAL(10,2),
    @Largo DECIMAL(10,2),
    @Alto DECIMAL(10,2),
    @IdTipodeProducto INT,
    @IdTipodeMadera INT,
    @IdTipodeAcabado INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdDetalles INT;

    -- Insertar en Detalles
    INSERT INTO DetallesdeProducto (Ancho, Largo, Alto, IdTipodeProducto, IdTipodeMadera, IdTipodeAcabado)
    VALUES (@Ancho, @Largo, @Alto, @IdTipodeProducto, @IdTipodeMadera, @IdTipodeAcabado);

    SET @IdDetalles = SCOPE_IDENTITY();

    -- Insertar en Producto
    INSERT INTO Producto (NombredelProducto, URLImagen, PrecioUnitario, IdCategoria, IdDetallesdeProducto)
    VALUES (@NombredelProducto, @URLImagen, @PrecioUnitario, @IdCategoria, @IdDetalles);
END;
GO
create procedure sp_crearUsuarios(
@NombredeUsuario varchar(100),
@Contraseña varchar(100),
@CorreoElectronico varchar(200),
@IdTipodeUsuario int,
@IdTipodeRol int,
@IdCliente int
)
as
begin
	
	insert into Usuario (
	NombredeUsuario,
	Contraseña,
	CorreoElectronico,
	IdTipodeUsuario,
	IdTipodeRol,IdCliente,
	FechaBorrado)values(
	@NombredeUsuario,
	@Contraseña,
	@CorreoElectronico,
	@IdTipodeUsuario,
	@IdTipoderol,
	@IdCliente,
	null
	)
 
end
go

CREATE PROCEDURE sp_crearClienteUsuario
    @DNI NVARCHAR(20),
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Telefono NVARCHAR(20),
    @Calle NVARCHAR(50),
    @Numero INT,
    @IdLocalidad INT,
    @IdTipodeDni INT,
    @IdCliente INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes (DNI, Nombre, Apellido, Telefono, Calle, Numero, IdLocalidad, IdTipodeDni)
    VALUES (@DNI, @Nombre, @Apellido, @Telefono, @Calle, @Numero, @IdLocalidad, @IdTipodeDni);

    SET @IdCliente = SCOPE_IDENTITY();
END




go
create procedure sp_editarUsuarios(
@Id int,
@NombredeUsuario varchar(100),
@Contraseña varchar(100),
@CorreoElectronico varchar(200),
@IdTipodeUsuario int,
@IdTipodeRol int

)
as
begin
	update Usuario 
	set NombredeUsuario = @NombredeUsuario,
	Contraseña = @Contraseña,
	CorreoElectronico = @CorreoElectronico,
	IdTipodeUsuario = @IdTipodeUsuario,
	IdTipodeRol = @IdTipodeRol
	where Id = @Id and FechaBorrado is null

end
go
create procedure sp_eliminarUsuarios(
@Id int
)
as
begin

	update Usuario
	set  FechaBorrado = GETDATE()
	where Id = @Id 
end
go
create procedure sp_crearCliente(
@DNI varchar(40),
@Nombre varchar(30),
@Apellido varchar(30),
@Telefono varchar(20),
@Calle varchar(60),
@Numero int,
@IdLocalidad int,
@IdTipodeDni int
)
as
begin
	insert into Clientes(DNI,Nombre,Apellido,Telefono,Calle,Numero,IdLocalidad,IdTipodeDni)
	values(@DNI,@Nombre,@Apellido,@Telefono,@Calle,@Numero,@IdLocalidad,@IdTipodeDni)
end
go
create procedure sp_editarCliente(
@Id int,
@DNI varchar(40),
@Nombre varchar(30),
@Apellido varchar(30),
@Telefono varchar(20),
@Calle varchar(60),
@Numero int,
@IdLocalidad int,
@IdTipodeDni int
)
as
begin
	update Clientes
	set DNI = @DNI,
	Nombre = @Nombre,
	Apellido = @Apellido,
	Telefono = @Telefono,
	Calle = @Calle,
	Numero = @Numero,
	IdLocalidad = @IdLocalidad,
	IdTipodeDni = @IdTipodeDni
	where Id = @Id and FechaBorrado is null

end
go
create procedure sp_eliminarClientes(
@Id int
)
as
begin

	update Clientes
	set  FechaBorrado = GETDATE()
	where Id = @Id 
end
go

create procedure _ValidarUsuario(
@NombredeUsuario varchar(100),
@Contraseña varchar(100)
)
as
begin
	select 
	NombredeUsuario,Contraseña
	
	from Usuario
	where NombredeUsuario = @NombredeUsuario and Contraseña = @Contraseña

end
go