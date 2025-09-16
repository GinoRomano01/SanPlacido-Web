 if NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'SanPlacido'
)
BEGIN
    CREATE DATABASE SanPlacido;
END
go
use SanPlacido

go
create table TipodeDni(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);
go
create table TipodeUsuario(
Id int primary key not null identity,
Nombre varchar(20),
FechaBorrado DATETIME NULL default null
);
go
create table TipodeRol(
Id int primary key not null identity,
Nombre varchar(20),
FechaBorrado DATETIME NULL default null
);
go
create table Localidad(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);
go
create table TipodeProducto(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);
go
create table TipodeMadera(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);
go
create table TipodeAcabado(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);
go
create table Categoria(
Id int primary key not null identity,
Nombre varchar(30),
FechaBorrado DATETIME NULL default null
);

go



go

/*NumerodeVenta int identity(100000,1) not null, */

create table DetallesdeProducto(
Id int primary key identity not null,
Ancho decimal(10,2),
Largo decimal(10,2),
Alto decimal(10,2),
IdTipodeProducto int,
IdTipodeMadera int,
IdTipodeAcabado int,

FechaBorrado DATETIME NULL default null,

foreign key (IdTipodeProducto) references TipodeProducto(Id),
foreign key (IdTipodeMadera) references TipodeMadera(Id),
foreign key (IdTipodeAcabado) references TipodeAcabado(Id)
)
go

create table Producto(
Id int primary key identity not null,
NombredelProducto varchar(100),
URLImagen varchar(500),
PrecioUnitario decimal(10,2),
FechaBorrado DATETIME NULL default null,
IdCategoria int,
IdDetallesdeProducto int,

foreign key (IdCategoria) references Categoria(Id),
foreign key (IdDetallesdeProducto) references DetallesdeProducto(Id)
)

create table Clientes (
Id int primary key not null identity,
DNI varchar(300),
Nombre varchar(30),
Apellido varchar(30),
Telefono varchar(20),
Calle varchar(60), 
Numero int,
FechaBorrado DATETIME NULL default null,
IdLocalidad int,
IdTipodeDni int,
foreign key (IdLocalidad) references Localidad(Id),
foreign key (IdTipodeDni) references TipodeDni(Id)
)
go
create table Usuario(
Id int primary key not null identity,
NombredeUsuario varchar(40),
Contraseña varchar(30),
CorreoElectronico varchar(50),
Restablecer int null,
Confirmado int null,
Token varchar(700) null,
IdTipodeUsuario int,
IdTipodeRol int,
IdCliente int,
FechaBorrado DATETIME NULL default null,
foreign key (IdTipodeUsuario) references TipodeUsuario (Id),
foreign key (IdTipodeRol) references TipodeRol (Id),
foreign key (IdCliente) references Clientes (Id) 
)
go
create table Carrito (
Id int primary key identity not null,
Cantidad int,
IdProducto int,
IdCliente int,

foreign key (IdProducto) references Producto (Id),
foreign key (IdCliente) references Clientes(Id)
)
