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

IF OBJECT_ID('VIDA_ESTATICA.Estado_Cuenta') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Estado_Cuenta;
END;

IF OBJECT_ID('VIDA_ESTATICA.Banco') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Banco;
END;

IF OBJECT_ID('VIDA_ESTATICA.Direccion') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Direccion;
END;

IF OBJECT_ID('VIDA_ESTATICA.Pais') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Pais;
END;

IF OBJECT_ID('VIDA_ESTATICA.Documento') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Documento;
END;

IF OBJECT_ID('VIDA_ESTATICA.Deposito') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Deposito;
END;

IF OBJECT_ID('VIDA_ESTATICA.Moneda') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Moneda;
END;

IF OBJECT_ID('VIDA_ESTATICA.Tipo_Cuenta') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Tipo_Cuenta;
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
	id numeric(18, 0) IDENTITY NOT NULL,
	name varchar(60) NOT NULL,
	pass varchar(25) NOT NULL,
	fecha_creacion DATETIME NOT NULL,
	ultima_mod DATETIME,
	pregunta varchar(50) NOT NULL,
	respuesta varchar(25) NOT NULL,
	rol numeric(18, 0) NOT NULL REFERENCES VIDA_ESTATICA.Rol(id)
)


INSERT INTO VIDA_ESTATICA.Usuario VALUES 
('admin', 'w23e', GETDATE(), NULL, 'Dog?', 'Dawg', 1);


CREATE TABLE VIDA_ESTATICA.Pais (
	id numeric(18,0) NOT NULL,
	descripcion varchar(250) NOT NULL,
	PRIMARY KEY (id)
)

	
CREATE TABLE VIDA_ESTATICA.Direccion(
	id numeric(18,0)IDENTITY NOT NULL,
	dom_calle varchar(50),
	dom_nro numeric(6,0),
	dom_piso numeric(2,0),
	dom_dpto varchar(1),
	dom_pais numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (dom_pais) REFERENCES VIDA_ESTATICA.Pais(id)
)

CREATE TABLE VIDA_ESTATICA.Banco(
	cod numeric(18,0) IDENTITY NOT NULL,
	nombre varchar(40) NOT NULL,
	direccion numeric(18,0) NOT NULL,
	PRIMARY KEY (cod),
	FOREIGN KEY (direccion) REFERENCES VIDA_ESTATICA.Direccion(id)
)


CREATE TABLE VIDA_ESTATICA.Estado_Cuenta(
	id numeric(4,0) IDENTITY NOT NULL,
	estado varchar(40) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Moneda(
	tipo varchar(20) NOT NULL,
	PRIMARY KEY (tipo)
)

CREATE TABLE VIDA_ESTATICA.Tipo_Cuenta(
	tipo varchar(20) NOT NULL,
	PRIMARY KEY (tipo)
)
CREATE TABLE VIDA_ESTATICA.Cuenta(
	id numeric(18,0) IDENTITY NOT NULL,
	cod_banco numeric(18,0) NOT NULL,
	nro_cuenta numeric(16,0) NOT NULL,
	fecha_creacion DATETIME,
	estado numeric(4,0),
	pais numeric(18,0),
	fecha_cierre DATETIME,
	tipo_moneda varchar(20),
	tipo_cuenta varchar(20),
	PRIMARY KEY (id, cod_banco),
	FOREIGN KEY (pais) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (cod_banco) REFERENCES VIDA_ESTATICA.Banco(cod),
	FOREIGN KEY (estado) REFERENCES VIDA_ESTATICA.Estado_Cuenta(id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(tipo),
	FOREIGN KEY (tipo_cuenta) REFERENCES VIDA_ESTATICA.Tipo_Cuenta(tipo)
	
)


CREATE TABLE VIDA_ESTATICA.Documento(
	tipo_doc_cod numeric(5,0) NOT NULL,
	tipo_doc_desc varchar(10) NOT NULL,
	PRIMARY KEY (tipo_doc_cod)
)


CREATE TABLE VIDA_ESTATICA.Cliente (
	id numeric(18,0) IDENTITY NOT NULL,
	nombre varchar(20) NOT NULL,
	apellido varchar(25) NOT NULL,
	direccion numeric(18,0),
	fecha_nac DATETIME NOT NULL,
	mail varchar(50) NOT NULL,
	nacionalidad numeric(18,0),
	cuenta numeric(18,0),
	banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (direccion) REFERENCES VIDA_ESTATICA.Direccion(id),
	FOREIGN KEY (nacionalidad) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (cuenta, banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)


CREATE TABLE VIDA_ESTATICA.Deposito(
	cod numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,0) NOT NULL,
	tipo_moneda varchar(20),
	PRIMARY KEY (cod),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(tipo)
)


--
-- INSERT DATA
--

INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM gd_esquema.Maestra;

INSERT INTO VIDA_ESTATICA.Direccion
SELECT DISTINCT Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, Cli_Pais_Codigo
FROM gd_esquema.Maestra

INSERT INTO VIDA_ESTATICA.Documento
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc
FROM gd_esquema.Maestra

INSERT INTO VIDA_ESTATICA.Estado_Cuenta
SELECT DISTINCT Cuenta_Estado
FROM gd_esquema.Maestra

--INSERT INTO VIDA_ESTATICA.Banco
--SELECT DISTINCT Banco_Cogido, Banco_Nombre, Banco_Direccion
--FROM gd_esquema.Maestra