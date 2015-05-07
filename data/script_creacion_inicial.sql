IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.schemata
WHERE   schema_name = 'VIDA_ESTATICA' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA VIDA_ESTATICA AUTHORIZATION gd'
END

IF OBJECT_ID('VIDA_ESTATICA.Rol') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Rol;
END;

CREATE TABLE VIDA_ESTATICA.Rol (
	id INT PRIMARY KEY IDENTITY(1,1),
	nombre varchar(13) NOT NULL,
	funcionalidades varchar(255) NOT NULL,
	estado char(1) NOT NULL);
	
INSERT INTO VIDA_ESTATICA.Rol VALUES
(1, 'Administrador General', 'ALL', 'S'),
(2, 'Cliente', 'CUENTA, DEPOSITOS, RETIRO, TRANSFERENCIAS, FACTURACION, SALDOS', 'S');

-- Login

IF OBJECT_ID('VIDA_ESTATICA.Usuario') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Usuario;
END;

-- SQL SERVER 2008 R2 no soporta SHA256, 
-- así que hay que traer todo encriptado desde la app.

CREATE TABLE VIDA_ESTATICA.Usuario (
	id INT PRIMARY KEY IDENTITY(1,1),
	name varchar(60) NOT NULL,
	pass varchar(25) NOT NULL,
	fecha_creacion DATETIME NOT NULL,
	ultima_mod DATETIME,
	pregunta varchar(50) NOT NULL,
	respuesta varchar(25) NOT NULL,
	rol numeric(18, 0) NOT NULL REFERENCES VIDA_ESTATICA.Rol(id));

INSERT INTO VIDA_ESTATICA.Usuario VALUES 
(1, 'admin', 'w23e', GETDATE(), NULL, 'Dog?', 'Dawg', 1);
	
IF OBJECT_ID('VIDA_ESTATICA.Cliente') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Cliente;
END;

-- TODO: tabla clientes

IF OBJECT_ID('VIDA_ESTATICA.Pais') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Pais;
END;

CREATE TABLE VIDA_ESTATICA.Pais (
	id INT PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(250) NOT NULL);

INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM gd_esquema.Maestra;
