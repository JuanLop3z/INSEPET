--CREACION DE BASE DE DATOS--
CREATE DATABASE INSEPET;
GO

--EJECUTAR LA DB--
USE INSEPET;
GO

--TABLA DE USUARIO(login)--
create table Usuario(
IdUsuario int primary key identity(1,1),
Nombre varchar(50),
Clave varchar(50)
)
go


--TABLA DE PRODUCTOS--
CREATE TABLE Productos(
IdProducto int primary key identity(1,1),
Nombre varchar(100),
Precio int,
Stock int
)
go


--LA TABLA DE CLIENTES--
create table Clientes(
Identificacion varchar (50) primary key,
Nombres varchar(200),
)
go


--TABLA DE FACTURAS--
CREATE TABLE Facturas(
IdFactura int primary key identity (1,1),
IdProducto int,
IdCliente varchar(50),
Estado bit default 1,
Fecha datetime default getdate(),
FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
FOREIGN KEY (IdCliente) REFERENCES Clientes(Identificacion)
)
go

SELECT c.Identificacion, c.Nombres, j.nombre AS factura, a.Fecha FROM clientes c INNER JOIN facturas a ON c.Identificacion = a.IdCliente INNER JOIN productos j ON a.IdProducto = j.IdProducto WHERE a.IdProducto IS NOT NULL