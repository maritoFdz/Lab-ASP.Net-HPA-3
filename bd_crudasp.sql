-- base de datos y tabla del laboratorio CRUD
create database if not exists crudasp;
use crudasp;

drop table if exists usuarios;
create table usuarios (
    idUsuario int auto_increment primary key,
    Nombre varchar(150) not null,
    Edad int not null,
    Correo varchar(150) not null,
    FechaNacimiento date not null,
    FechaModificacion datetime default current_timestamp on update current_timestamp,
    FechaDeInsercion datetime default current_timestamp,
    Anulado tinyint(1) not null default 1,
    FechaEliminacion datetime null,
    unique index index_Correo (Correo)
);

-- procedimientos almacenados
drop procedure if exists sp_load;
drop procedure if exists sp_read;
drop procedure if exists insert_user;
drop procedure if exists sp_update;
drop procedure if exists sp_delete;

delimiter //

-- lista solo los usuarios activos
create procedure sp_load()
begin
    select idUsuario, Nombre, Edad, Correo, FechaNacimiento, FechaDeInsercion
    from usuarios
    where Anulado = 1
    order by idUsuario;
end //

-- trae un usuario por id
create procedure sp_read(in _idUsuario int)
begin
    select * from usuarios where idUsuario = _idUsuario;
end //

create procedure insert_user(
    in _nombre varchar(150),
    in _correo varchar(150),
    in _edad int,
    in _fechanacimiento date)
begin
    insert into usuarios (Nombre, Correo, Edad, FechaNacimiento)
    values (_nombre, _correo, _edad, _fechanacimiento);
end //

create procedure sp_update(
    in _idUsuario int,
    in _nombre varchar(150),
    in _correo varchar(150),
    in _edad int,
    in _fechanacimiento date)
begin
    update usuarios
    set Nombre = _nombre, Correo = _correo, Edad = _edad, FechaNacimiento = _fechanacimiento
    where idUsuario = _idUsuario;
end //

-- borrado logico: marca como anulado y guarda la fecha
create procedure sp_delete(in _idUsuario int)
begin
    update usuarios set Anulado = 0, FechaEliminacion = now() where idUsuario = _idUsuario;
end //

delimiter ;

-- datos de prueba
call insert_user('Claudia', 'claudiashifer@gmail.com', 30, '2000-02-04');
call insert_user('Irina', 'irinafong@gmail.com', 20, '2026-11-23');
call insert_user('Elena', 'konzerta@gmail.com', 32, '2026-11-09');

call sp_load();
