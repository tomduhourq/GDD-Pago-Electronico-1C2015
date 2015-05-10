IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.schemata
WHERE   schema_name = 'VIDA_ESTATICA' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA VIDA_ESTATICA AUTHORIZATION gd'
END

--
-- DROP TABLES
--

IF OBJECT_ID('VIDA_ESTATICA.Usuario') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Usuario;
END;

IF OBJECT_ID('VIDA_ESTATICA.Funcionalidad_Rol') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Funcionalidad_Rol;
END;

IF OBJECT_ID('VIDA_ESTATICA.Rol') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Rol;
END;

IF OBJECT_ID('VIDA_ESTATICA.Funcionalidad') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Funcionalidad;
END;

IF OBJECT_ID('VIDA_ESTATICA.Cliente') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Cliente;
END;

IF OBJECT_ID('VIDA_ESTATICA.Cuenta') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Cuenta;
END;

IF OBJECT_ID('VIDA_ESTATICA.Pais') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Pais;
END;

IF OBJECT_ID('VIDA_ESTATICA.Direccion') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Direccion;
END;

IF OBJECT_ID('VIDA_ESTATICA.Documento') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Documento;
END;



--
-- CREATE TABLES
--

CREATE TABLE VIDA_ESTATICA.Rol (
	id numeric(18, 0) IDENTITY,
	nombre varchar(25) NOT NULL,
	activo bit NOT NULL
		CONSTRAINT activo DEFAULT 1,
	PRIMARY KEY (id)
)

INSERT INTO VIDA_ESTATICA.Rol (nombre, activo) VALUES
('Administrador General', 1),
('Cliente', 1) 


CREATE TABLE VIDA_ESTATICA.Funcionalidad (
	id numeric(18, 0) IDENTITY,
	nombre varchar(255) NOT NULL,
	PRIMARY KEY (id)
)



CREATE TABLE VIDA_ESTATICA.Funcionalidad_Rol (
	rol numeric(18, 0) NOT NULL REFERENCES VIDA_ESTATICA.Rol(id),
	funcionalidad numeric(18, 0) NOT NULL ,
	FOREIGN KEY (rol) REFERENCES VIDA_ESTATICA.Rol(id),
	FOREIGN KEY (funcionalidad) REFERENCES VIDA_ESTATICA.Funcionalidad(id)
)

-- SQL SERVER 2008 R2 no soporta SHA256, 
-- así que hay que traer todo encriptado desde la app.

-- LOGIN

CREATE TABLE VIDA_ESTATICA.Usuario (
	id numeric(18, 0) IDENTITY,
	name varchar(60) NOT NULL,
	pass varchar(25) NOT NULL,
	fecha_creacion DATETIME NOT NULL,
	ultima_mod DATETIME,
	pregunta varchar(50) NOT NULL,
	respuesta varchar(25) NOT NULL,
	rol numeric(18, 0) NOT NULL REFERENCES VIDA_ESTATICA.Rol(id));


INSERT INTO VIDA_ESTATICA.Usuario VALUES 
('admin', 'w23e', GETDATE(), NULL, 'Dog?', 'Dawg', 1);


CREATE TABLE VIDA_ESTATICA.Pais (
	id varchar(250) PRIMARY KEY,
	descripcion varchar(250) NOT NULL);

	
CREATE TABLE VIDA_ESTATICA.Direccion(
	id numeric(18,0) IDENTITY,
	dom_calle varchar(50)NOT NULL,
	dom_nro numeric(6,0) NOT NULL,
	dom_dpto varchar(1),
	PRIMARY KEY (id)
)


CREATE TABLE VIDA_ESTATICA.Cuenta(
	id numeric(18,0) IDENTITY,
	nro_cuenta numeric(16,0),
	fecha_creacion DATETIME,
	estado numeric(4,0),
	PRIMARY KEY (id)	
)


CREATE TABLE VIDA_ESTATICA.Documento(
	id numeric(18,0) IDENTITY,
	tipo_doc_cod numeric(5,0),
	tipo_doc_desc varchar(10),
	PRIMARY KEY (id)
)


CREATE TABLE VIDA_ESTATICA.Cliente (
	id numeric(18,0) IDENTITY,
	nombre varchar(20),
	apellido varchar(25),
	direccion numeric(18,0),
	fecha_nac DATETIME,
	mail varchar(50),
	nacionalidad varchar(250),
	PRIMARY KEY (id),
	FOREIGN KEY (direccion) REFERENCES VIDA_ESTATICA.Direccion(id),
	FOREIGN KEY (nacionalidad) REFERENCES VIDA_ESTATICA.Pais(id)
)



--
-- INSERT DATA
--

INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM gd_esquema.Maestra;

