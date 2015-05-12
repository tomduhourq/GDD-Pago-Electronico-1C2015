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

IF OBJECT_ID('VIDA_ESTATICA.Deposito') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Deposito;
END;

IF OBJECT_ID('VIDA_ESTATICA.Transferencia') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Transferencia;
END;

IF OBJECT_ID('VIDA_ESTATICA.Cheque') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Cheque;
END;

IF OBJECT_ID('VIDA_ESTATICA.Tarjeta') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Tarjeta;
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

IF OBJECT_ID('VIDA_ESTATICA.Moneda') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Moneda;
END;

IF OBJECT_ID('VIDA_ESTATICA.Tipo_Cuenta') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Tipo_Cuenta;
END;


IF OBJECT_ID('VIDA_ESTATICA.Emisor') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Emisor;
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
-- as� que hay que traer todo encriptado desde la app.

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
	tipo varchar(20),
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
	documento numeric(8,0),
	direccion numeric(18,0),
	fecha_nac DATETIME NOT NULL,
	mail varchar(50) NOT NULL,
	nacionalidad numeric(18,0),
	cuenta numeric(18,0),
	banco numeric(18,0),
	tipo_documento numeric(5,0),
	PRIMARY KEY (id),
	FOREIGN KEY (direccion) REFERENCES VIDA_ESTATICA.Direccion(id),
	FOREIGN KEY (nacionalidad) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (cuenta, banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco),
	FOREIGN KEY (tipo_documento) REFERENCES VIDA_ESTATICA.Documento(tipo_doc_cod)
)


CREATE TABLE VIDA_ESTATICA.Emisor(
	nombre varchar(30),
	PRIMARY KEY (nombre)
)


CREATE TABLE VIDA_ESTATICA.Tarjeta(
	id numeric(18,0) IDENTITY NOT NULL,
	numero numeric(18,0) NOT NULL,
	fecha_emision DATETIME,
	fecha_vencimiento DATETIME,
	cod_seguridad numeric(18,0),
	emisor varchar(30),
	cuenta numeric(18,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (emisor) REFERENCES VIDA_ESTATICA.Emisor(nombre),
	FOREIGN KEY (cuenta, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)


CREATE TABLE VIDA_ESTATICA.Deposito(
	cod numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	tipo_moneda varchar(20),
	tarjeta_id numeric(18,0),
	cuenta_destino numeric(18,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (cod),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(tipo),
	FOREIGN KEY (tarjeta_id) REFERENCES VIDA_ESTATICA.Tarjeta(id),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)


CREATE TABLE VIDA_ESTATICA.Transferencia(
	id numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	costo numeric(10,2) NOT NULL,
	cuenta_destino numeric(18,0),
	tipo_moneda varchar(20),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(tipo),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)

CREATE TABLE VIDA_ESTATICA.Cheque(
	id numeric(18,0) IDENTITY NOT NULL,
	retiro_fecha DATETIME,
	retiro_codigo numeric(18,0) NOT NULL,
	retiro_importe numeric(15,2) NOT NULL,
	cheque_importe numeric(15,2) NOT NULL,
	cuenta_destino numeric(18,0),
	tipo_moneda varchar(20),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(tipo),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)
--
-- DATA INSERT
--

INSERT INTO VIDA_ESTATICA.Rol (nombre, activo) VALUES
('Administrador General', 1),
('Cliente', 1) 

INSERT INTO VIDA_ESTATICA.Usuario VALUES 
('admin', 'w23e', GETDATE(), NULL, 'Dog?', 'Dawg', 1);


--
-- MIGRATION
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
FROM gd_esquema.Maestra where(Cuenta_Estado is not null)

INSERT INTO VIDA_ESTATICA.Emisor
SELECT DISTINCT Tarjeta_Emisor_Descripcion
FROM gd_esquema.Maestra where(Tarjeta_Emisor_Descripcion is not null)

--SET IDENTITY_INSERT VIDA_ESTATICA.BANCO ON
--INSERT INTO VIDA_ESTATICA.BANCO (COD, NOMBRE, DIRECCION)
--SELECT DISTINCT BANCO_COGIDO, BANCO_NOMBRE, BANCO_DIRECCION 
--FROM GD_ESQUEMA.MAESTRA
--SET IDENTITY_INSERT VIDA_ESTATICA.BANCO OFF