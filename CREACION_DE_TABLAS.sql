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
IdCliente varchar(50),
Fecha datetime default getdate(),
FOREIGN KEY (IdCliente) REFERENCES Clientes(Identificacion)
)
go


--TABLA DE LOS PRODUCTOS DE LA FACTURA--
Create table ProductosFactura(
IdFactura int,
IdProducto int,
FOREIGN KEY (IdFactura) REFERENCES Facturas(IdFactura),
FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
)
go
