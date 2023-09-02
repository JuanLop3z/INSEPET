--EJECUTAR LA DB--
USE INSEPET;
GO


--PROCEDIMIENTO PARA CREAR CLIENTES--
Create proc sp_CrearClientes(
@Identificacion varchar(50),
@Nombres varchar(200),
--Mensajes de salida--
@Registrado bit output,
@Mensaje varchar(100) output
)as 
begin
	--validando si el cliente existe--
	if(not exists(select * from Clientes where Identificacion = @Identificacion))
		begin
			insert into Clientes (Identificacion, Nombres)
			values (@Identificacion,@Nombres)
			set @Registrado = 1
			set @Mensaje = 'Cliente registrado'
		end	
		else
			begin
			set @Registrado = 0
			set @Mensaje = 'Cliente ya Existe'
			end
end
go

--PROCEDIMIENTO PARA REGISTRAR USUARIOS	--
Create proc sp_RegistrarUsuario(
@Nombre varchar(50),
@Clave varchar(50),
--Mensajes de salida--
@Registrado bit output,
@Mensaje varchar(100) output
)as 
begin
	--validando si usuario existe--
	if(not exists(select * from Usuario where Nombre = @Nombre))
		begin
			insert into Usuario(Nombre,Clave)values(@Nombre,@Clave)
			set @Registrado = 1
			set @Mensaje = 'Usuario registrado'
		end	
		else
			begin
			set @Registrado = 0
			set @Mensaje = 'Usuario ya Existe'
			end
end
go


--PROCEDIMIENTO PARA VALIDAR USUARIO--
create proc sp_ValidarUsuario(
@Nombre nvarchar(50),
@Clave nvarchar(50)
)
as
begin
	--valida los campos y devuelve el IdUsuario--
	if(exists(select * from Usuario where Nombre = @Nombre and Clave = @Clave))
		select IdUsuario from Usuario where Nombre = @Nombre and Clave = @Clave
	else
		select '0'
end
go


--PROCEDIMIENTO PARA REGISTRAR PRODUCTOS--
Create proc sp_CrearProducto(
@Nombre varchar(150),
@Precio int,
@Stock int,
--Mensajes de salida--
@Registrado bit output,
@Mensaje varchar(100) output
)as 
begin
	--validando si el Producto existe--
	if(not exists(select * from Productos where Nombre = @Nombre))
		begin
			insert into Productos (Nombre,Precio,Stock)
			values (@Nombre,@Precio,@Stock)
			set @Registrado = 1
			set @Mensaje = 'Producto Registrado Registrado'
		end	
		else
			begin
			set @Registrado = 0
			set @Mensaje = 'El Producto ya Existe'
			end
end
go


--PROCEDIMIENTO PARA REGISTRAR FACTURAS--
Create proc sp_RegistrarFacturas(
@IdCliente varchar(150),
@IdProducto varchar(100),
--Mensajes de salida--
@Registrado bit output,
@Mensaje varchar(100) output
)as 
begin
	set nocount on
	--validando si el producto existe--
	if(exists(select * from Productos where IdProducto = @IdProducto))
		begin
			insert into Facturas(IdCliente,IdProducto)
			values (@IdCliente, @IdProducto)
			set @Registrado = 1
			set @Mensaje = 'Factura registrada exitosamente'
		end	
		else
			begin
			set @Registrado = 0
			set @Mensaje = 'No se pudo registrar la factura'
			end
end
go
