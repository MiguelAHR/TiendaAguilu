create database tienda;
go
use tienda;
go
create table roles(
	id int primary key identity,
	tipo varchar(100) unique,
	descripcion varchar(500) null,
)

create table usuarios(
	id int primary key identity,
	nombre varchar(100),
	apellidos varchar(100),
	direccion varchar(255) null,
	correo varchar(100) unique,
	clave varchar(100),
	intentos int,
	sesion datetime,
	rolId int,
	foreign key (rolId) references Roles(id)
)

create table categorias(
	id int primary key identity,
	nombre varchar(100) unique,
	descripcion varchar(500) null
)

create table productos(
	id int primary key identity,
	codigo char(4) unique,
	nombre varchar(100),
	descripcion varchar(500) null,
	precio decimal(10, 2),
	cantidad int,
	categoriaId int,
	foreign key (categoriaId) references categorias(id)
)

create login usuarioTienda with password = 'UPN2024';
create user usuarioTienda for login usuarioTienda;
grant select, update, insert, delete on schema::dbo to usuarioTienda;