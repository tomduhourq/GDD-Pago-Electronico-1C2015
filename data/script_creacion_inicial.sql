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

IF OBJECT_ID('VIDA_ESTATICA.Funcionalidad_Rol') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Funcionalidad_Rol;
END;

IF OBJECT_ID('VIDA_ESTATICA.Rol_Usuario') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Rol_Usuario;
END;

IF OBJECT_ID('VIDA_ESTATICA.Usuario') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Usuario;
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

IF OBJECT_ID('VIDA_ESTATICA.Tipo_Documento') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Tipo_Documento;
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
	name varchar(25) NOT NULL,
	pass varchar(100) NOT NULL,
	fecha_creacion DATETIME NOT NULL,
	ultima_mod DATETIME,
	intentos_login numeric(18, 0) NOT NULL
		CONSTRAINT "intentos_login_0" DEFAULT 0,
	activo bit NOT NULL
		CONSTRAINT "usuario_activo" DEFAULT 1,
	pregunta varchar(50) NOT NULL,
	respuesta varchar(25) NOT NULL,
	PRIMARY KEY (name)
)

-- Un usuario puede tener más de un Rol

CREATE TABLE VIDA_ESTATICA.Rol_Usuario (
	usuario varchar(25),
	rol numeric(18, 0),
	FOREIGN KEY (usuario) REFERENCES VIDA_ESTATICA.Usuario(name),
	FOREIGN KEY (rol) REFERENCES VIDA_ESTATICA.Rol(id),
	PRIMARY KEY (usuario, rol)
)

CREATE TABLE VIDA_ESTATICA.Pais (
	id numeric(18,0) NOT NULL,
	descripcion varchar(250) NOT NULL,
	PRIMARY KEY (id)
)


CREATE TABLE VIDA_ESTATICA.Banco(
	cod numeric(18,0) IDENTITY NOT NULL,
	nombre varchar(40) NOT NULL,
	direccion varchar(50),
	PRIMARY KEY (cod),
)



CREATE TABLE VIDA_ESTATICA.Estado_Cuenta(
	id numeric(4,0) IDENTITY NOT NULL,
	descripcion varchar(40) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Moneda(
	id numeric(4,0) IDENTITY NOT NULL,
	descripcion varchar(40) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Tipo_Cuenta(
	id numeric(4,0) IDENTITY NOT NULL,
	descripcion varchar(40) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Cuenta(
	id numeric(18,0) IDENTITY NOT NULL,
	cod_banco numeric(18,0) NOT NULL,
	nro_cuenta numeric(16,0) NOT NULL,
	fecha_creacion DATETIME,
	estado numeric(4,0),
	pais numeric(18,0),
	fecha_cierre DATETIME,
	tipo_moneda numeric(4,0),
	tipo_cuenta numeric(4,0),
	PRIMARY KEY (id, cod_banco),
	FOREIGN KEY (pais) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (cod_banco) REFERENCES VIDA_ESTATICA.Banco(cod),
	FOREIGN KEY (estado) REFERENCES VIDA_ESTATICA.Estado_Cuenta(id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (tipo_cuenta) REFERENCES VIDA_ESTATICA.Tipo_Cuenta(id)	
)


CREATE TABLE VIDA_ESTATICA.Tipo_Documento(
	id numeric(18,0) IDENTITY NOT NULL,
	descripcion varchar(255) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Cliente (
	id numeric(18,0) IDENTITY NOT NULL,
	nombre varchar(20) NOT NULL,
	apellido varchar(25) NOT NULL,
	documento numeric(8,0),
	dom_calle varchar(50),
	dom_nro numeric(6,0),
	dom_piso numeric(2,0),
	dom_dpto varchar(1),
	dom_pais numeric(18,0),
	fecha_nac DATETIME NOT NULL,
	mail varchar(50) NOT NULL,
	nacionalidad numeric(18,0),
	cuenta numeric(18,0),
	banco numeric(18,0),
	tipo_documento numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (nacionalidad) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (dom_pais) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (cuenta, banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco),
	FOREIGN KEY (tipo_documento) REFERENCES VIDA_ESTATICA.Tipo_Documento(id)
)


CREATE TABLE VIDA_ESTATICA.Emisor(
	id numeric(4,0) IDENTITY NOT NULL,
	nombre varchar(40) NOT NULL,
	PRIMARY KEY(id)
)


CREATE TABLE VIDA_ESTATICA.Tarjeta(
	id numeric(18,0) IDENTITY NOT NULL,
	numero numeric(18,0) NOT NULL,
	fecha_emision DATETIME,
	fecha_vencimiento DATETIME,
	cod_seguridad numeric(18,0),
	emisor numeric(4,0),
	cuenta numeric(18,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (emisor) REFERENCES VIDA_ESTATICA.Emisor(id),
	FOREIGN KEY (cuenta, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)


CREATE TABLE VIDA_ESTATICA.Deposito(
	cod numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	tipo_moneda numeric(4,0),
	tarjeta_id numeric(18,0),
	cuenta_destino numeric(18,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (cod),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (tarjeta_id) REFERENCES VIDA_ESTATICA.Tarjeta(id),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)


CREATE TABLE VIDA_ESTATICA.Transferencia(
	id numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	costo numeric(10,2) NOT NULL,
	cuenta_destino numeric(18,0),
	tipo_moneda numeric(4,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)

CREATE TABLE VIDA_ESTATICA.Cheque(
	id numeric(18,0) IDENTITY NOT NULL,
	retiro_fecha DATETIME,
	retiro_codigo numeric(18,0) NOT NULL,
	retiro_importe numeric(15,2) NOT NULL,
	cheque_importe numeric(15,2) NOT NULL,
	cuenta_destino numeric(18,0),
	tipo_moneda numeric(4,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (cuenta_destino, cod_banco) REFERENCES VIDA_ESTATICA.Cuenta(id, cod_banco)
)
--
-- DATA INSERT
--

INSERT INTO VIDA_ESTATICA.Funcionalidad VALUES
('ABM Cliente'),
('ABM Cuenta'),
('ABM de Usuario'),
('ABM Rol'),
('Consulta saldos'),
('Depositos'),
('Facturacion'),
('Listados'),
('Retiros'),
('Transferencias')

INSERT INTO VIDA_ESTATICA.Rol (nombre, activo) VALUES
('Administrador General', 1),
('Cliente', 1) 

INSERT INTO VIDA_ESTATICA.Usuario VALUES 
('admin', 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7', GETDATE(), NULL, 0, 1, 'Dog?', 'Dawg');

INSERT INTO VIDA_ESTATICA.Rol_Usuario VALUES
('admin', 1),
('admin', 2)

exec sp_columns Rol_Usuario;

--
-- MIGRATION
--

INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM gd_esquema.Maestra;

INSERT INTO VIDA_ESTATICA.Tipo_Documento(descripcion)
SELECT DISTINCT Cli_Tipo_Doc_Desc
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

-- Stored Procedures
GO

IF OBJECT_ID('VIDA_ESTATICA.updateIntentos') IS NOT NULL
BEGIN
	DROP PROCEDURE VIDA_ESTATICA.updateIntentos;
END;
GO

IF OBJECT_ID('VIDA_ESTATICA.addFuncionalidad') IS NOT NULL
BEGIN
	DROP PROCEDURE VIDA_ESTATICA.addFuncionalidad;
END;
GO

CREATE PROCEDURE VIDA_ESTATICA.addFuncionalidad(@rol varchar(255), @func varchar(255)) AS
BEGIN
	INSERT INTO VIDA_ESTATICA.Funcionalidad_Rol (rol, funcionalidad)
		VALUES ((SELECT id FROM VIDA_ESTATICA.Rol WHERE nombre = @rol),
		        (SELECT id FROM VIDA_ESTATICA.Funcionalidad WHERE nombre = @func))
END
GO

CREATE PROCEDURE VIDA_ESTATICA.updateIntentos(@intentos_login numeric(18, 0),@nombre varchar(25) , @ret numeric(18,0) output)
AS BEGIN
  IF(@intentos_login = 2)
	BEGIN
	  UPDATE VIDA_ESTATICA.Usuario SET activo=0, intentos_login=@intentos_login WHERE name=@nombre
	  SET @ret = 1
	END
  ELSE
	BEGIN
	  UPDATE VIDA_ESTATICA.Usuario set intentos_login=@intentos_login WHERE name=@nombre
	  SET @ret = 2
	END
END
GO

CREATE FUNCTION VIDA_ESTATICA.roles_usuario(@username varchar(255))
RETURNS @roles TABLE (rol int, nombre varchar(255)) AS
BEGIN
	INSERT INTO @roles
		SELECT id, nombre
		FROM
			VIDA_ESTATICA.Rol_Usuario JOIN VIDA_ESTATICA.Rol
			ON Rol_Usuario.rol = Rol.id
		WHERE Rol_Usuario.usuario = @username
	RETURN
END
GO

EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='ABM Cliente';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='ABM Cuenta';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='ABM Rol';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='ABM de Usuario';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Consulta saldos';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Depositos';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Facturacion';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Listados';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Retiros';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Administrador General', @func ='Transferencias';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Cliente', @func ='Consulta saldos';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Cliente', @func ='Depositos';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Cliente', @func ='Retiros';
EXEC VIDA_ESTATICA.addFuncionalidad @rol='Cliente', @func ='Transferencias';


