USE GD1C2015;
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

IF OBJECT_ID('VIDA_ESTATICA.Cliente') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Cliente;
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

IF OBJECT_ID('VIDA_ESTATICA.Retiro') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Retiro;
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

IF OBJECT_ID('VIDA_ESTATICA.Pais') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Pais;
END;

IF OBJECT_ID('VIDA_ESTATICA.Item_Factura') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Item_Factura;
END;

IF OBJECT_ID('VIDA_ESTATICA.Items') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Items;
END;

IF OBJECT_ID('VIDA_ESTATICA.Factura') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Factura;
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
	respuesta varchar(100) NOT NULL,
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
	cod numeric(18,0) NOT NULL,
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
	valor decimal NOT NULL,
	duracion int NOT NULL,
	costo_transaccion INT NOT NULL
	PRIMARY KEY(id)
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
	fecha_nac DATETIME NOT NULL,
	mail varchar(50) NOT NULL,
	nacionalidad numeric(18,0),
	tipo_documento numeric(18,0),
	usuario varchar(25),
	activo bit NOT NULL
		CONSTRAINT "cliente_activo" DEFAULT 1,
	PRIMARY KEY (id),
	FOREIGN KEY (nacionalidad) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (tipo_documento) REFERENCES VIDA_ESTATICA.Tipo_Documento(id),
	FOREIGN KEY (usuario) REFERENCES VIDA_ESTATICA.Usuario(name)
)

CREATE TABLE VIDA_ESTATICA.Cuenta(
	id numeric(16,0) NOT NULL IDENTITY,
	num_cuenta numeric(16,0),
	fecha_creacion DATETIME,
	estado numeric(4,0),
	pais numeric(18,0),
	fecha_cierre DATETIME,
	saldo numeric(15, 1) NOT NULL DEFAULT 0.0,
	tipo_moneda numeric(4,0),
	tipo_cuenta numeric(4,0),
	cod_cli numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (pais) REFERENCES VIDA_ESTATICA.Pais(id),
	FOREIGN KEY (estado) REFERENCES VIDA_ESTATICA.Estado_Cuenta(id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (tipo_cuenta) REFERENCES VIDA_ESTATICA.Tipo_Cuenta(id),	
	FOREIGN KEY (cod_cli) REFERENCES VIDA_ESTATICA.Cliente(id)
)

CREATE TABLE VIDA_ESTATICA.Emisor(
	id numeric(4,0) IDENTITY NOT NULL,
	nombre varchar(40) NOT NULL,
	PRIMARY KEY(id)
)

CREATE TABLE VIDA_ESTATICA.Tarjeta(
	numero numeric(18,0) NOT NULL,
	fecha_emision DATETIME,
	fecha_vencimiento DATETIME,
	cod_seguridad numeric(18,0),
	emisor numeric(4,0),
	cod_cli numeric(18, 0)
	PRIMARY KEY (numero),
	FOREIGN KEY (emisor) REFERENCES VIDA_ESTATICA.Emisor(id),
	FOREIGN KEY (cod_cli) REFERENCES VIDA_ESTATICA.Cliente(id)
)

CREATE TABLE VIDA_ESTATICA.Deposito(
	id numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	tipo_moneda numeric(4,0),
	tarjeta numeric(18,0),
	cuenta_destino numeric(16,0),
	emisor numeric(4,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (tarjeta) REFERENCES VIDA_ESTATICA.Tarjeta(numero),
	FOREIGN KEY (cuenta_destino) REFERENCES VIDA_ESTATICA.Cuenta(id),
	FOREIGN KEY (emisor) REFERENCES VIDA_ESTATICA.Emisor(id)
)

CREATE TABLE VIDA_ESTATICA.Transferencia(
	id numeric(18,0) IDENTITY NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	costo int NOT NULL,
	cuenta_origen numeric(16, 0),
	cuenta_destino numeric(16,0),
	tipo_moneda numeric(4,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (cuenta_origen) REFERENCES VIDA_ESTATICA.Cuenta(id),
	FOREIGN KEY (cuenta_destino) REFERENCES VIDA_ESTATICA.Cuenta(id)
)

CREATE TABLE VIDA_ESTATICA.Cheque(
	id numeric(18,0) IDENTITY NOT NULL,
	id_egreso numeric(18,0) NOT NULL,
	fecha DATETIME,
	importe numeric(15,2) NOT NULL,
	cuenta_destino numeric(16,0),
	tipo_moneda numeric(4,0),
	cod_banco numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_moneda) REFERENCES VIDA_ESTATICA.Moneda(id),
	FOREIGN KEY (cuenta_destino) REFERENCES VIDA_ESTATICA.Cuenta(id)
)

CREATE TABLE VIDA_ESTATICA.Retiro (
	id numeric(18, 0) IDENTITY NOT NULL,
	fecha datetime,
	importe numeric(18,2),
	cuenta_destino numeric(16, 0),
	moneda numeric(4, 0),
	PRIMARY KEY(id),
	FOREIGN KEY(cuenta_destino) REFERENCES VIDA_ESTATICA.Cuenta(id),
	FOREIGN KEY(moneda) REFERENCES VIDA_ESTATICA.Moneda(id)
)

CREATE TABLE VIDA_ESTATICA.Factura(
id_factura NUMERIC(18,0) NOT NULL IDENTITY (1,1),
id_cliente NUMERIC(18,0) NOT NULL,
fecha DATETIME, 
PRIMARY KEY(id_factura),
FOREIGN KEY(id_cliente) references VIDA_ESTATICA.Cliente);

CREATE TABLE VIDA_ESTATICA.Items(
id_item NUMERIC(18,0) NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255),
PRIMARY KEY(id_item));

CREATE TABLE VIDA_ESTATICA.Item_Factura (
id_item_factura NUMERIC(18,0) NOT NULL IDENTITY(1,1),
id_item NUMERIC(18,0) NOT NULL,
num_cuenta NUMERIC(16,0) NOT NULL,
monto NUMERIC(18,2),
facturado BIT, 
id_factura NUMERIC(18,0),
fecha DATETIME,
PRIMARY KEY(id_item_factura),
FOREIGN KEY (id_factura) references VIDA_ESTATICA.Factura,
FOREIGN KEY(id_item) references VIDA_ESTATICA.Items,
FOREIGN KEY (num_cuenta) references VIDA_ESTATICA.Cuenta);

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
('admin', 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7', GETDATE(), NULL, 0, 1, 'Dog?', 'F5F4EE52452AD2CF44C2C8C42518A734678F09FBFD809DB1B0BE3AD40DB87403');

INSERT INTO VIDA_ESTATICA.Rol_Usuario VALUES
('admin', 1),
('admin', 2)

-- exec sp_columns Rol_Usuario;

--
-- MIGRATION
--

-- Two columns contains different values, we need to add all the posibilities
INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc
FROM gd_esquema.Maestra 
ORDER BY Cli_Pais_Codigo;
INSERT INTO VIDA_ESTATICA.Pais
SELECT DISTINCT Cuenta_Pais_Codigo, Cuenta_Pais_Desc
FROM [GD1C2015].[gd_esquema].[Maestra]
WHERE Cuenta_Pais_Codigo not in (
SELECT distinct Cli_Pais_Codigo FROM [GD1C2015].[gd_esquema].[Maestra]);

SET IDENTITY_INSERT VIDA_ESTATICA.Tipo_Documento ON 
INSERT INTO VIDA_ESTATICA.Tipo_Documento(id, descripcion)
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc
FROM gd_esquema.Maestra;
SET IDENTITY_INSERT VIDA_ESTATICA.Tipo_Documento OFF

INSERT INTO VIDA_ESTATICA.Banco
SELECT DISTINCT Banco_Cogido,Banco_Nombre,Banco_Direccion FROM gd_esquema.Maestra
WHERE Banco_Cogido is not null;

INSERT INTO VIDA_ESTATICA.Estado_Cuenta Values
('PendienteActivacion'),
('Cerrada'),
('Inhabilitada'),
('Habilitada');

INSERT INTO VIDA_ESTATICA.Emisor
SELECT DISTINCT Tarjeta_Emisor_Descripcion
FROM gd_esquema.Maestra where(Tarjeta_Emisor_Descripcion is not null)

INSERT INTO VIDA_ESTATICA.Moneda(descripcion) Values('Dolar');

INSERT INTO VIDA_ESTATICA.Tipo_Cuenta(descripcion,valor,duracion, costo_transaccion) Values
('Gratuita',0,365, 120),
('Bronce',40,365, 80),
('Plata',80,365, 60),
('Oro',120,600, 20);

INSERT INTO VIDA_ESTATICA.Cliente
SELECT DISTINCT Cli_Nombre, Cli_Apellido, Cli_Nro_Doc, Cli_Dom_Calle,Cli_Dom_Nro,Cli_Dom_Piso,
Cli_Dom_Depto,Cli_Fecha_Nac,Cli_Mail,Cli_Pais_Codigo, Cli_Tipo_Doc_Cod, NULL, 1
FROM [GD1C2015].[gd_esquema].[Maestra]

GO
UPDATE VIDA_ESTATICA.Cliente
SET usuario = 'admin'
WHERE id = 1;
GO

INSERT INTO VIDA_ESTATICA.Cuenta(num_cuenta, fecha_creacion, estado, pais, fecha_cierre, tipo_cuenta, tipo_moneda, cod_cli)
SELECT DISTINCT 
Cuenta_Numero
,Cuenta_Fecha_Creacion
,4, -- Habilitada
Cuenta_Pais_Codigo,Cuenta_Fecha_Cierre
,1 -- Gratuita
,1, -- Dolares
(SELECT id FROM VIDA_ESTATICA.Cliente WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
FROM gd_esquema.Maestra 
GO 

-- Calculo antes de migrar los depósitos, retiros y transferencias el saldo inicial de las cuentas
UPDATE VIDA_ESTATICA.Cuenta
SET saldo = (SELECT ISNULL(SUM(Deposito_Importe),0) - ISNULL(SUM(Retiro_Importe), 0) + ISNULL(SUM(Trans_Importe), 0)
			 FROM gd_esquema.Maestra
			 WHERE num_cuenta = Cuenta_Numero)
GO

INSERT INTO VIDA_ESTATICA.Tarjeta (numero, fecha_emision, fecha_vencimiento, cod_seguridad, emisor, cod_cli)
SELECT DISTINCT 
Tarjeta_Numero, 
Tarjeta_Fecha_Emision, 
Tarjeta_Fecha_Vencimiento, 
Tarjeta_Codigo_Seg,
(SELECT DISTINCT id FROM VIDA_ESTATICA.Emisor WHERE nombre = m.[Tarjeta_Emisor_Descripcion]) 'emisor' , 
(SELECT id FROM VIDA_ESTATICA.Cliente WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) 'cod_cli'
FROM gd_esquema.Maestra m
WHERE Tarjeta_Emisor_Descripcion IS NOT NULL;

GO

-- Extra update para Tarjeta
/*UPDATE VIDA_ESTATICA.Tarjeta
SET cod_cli = 1
WHERE id in (1,2,3,4,5,6);*/

BEGIN TRANSACTION
SET IDENTITY_INSERT VIDA_ESTATICA.Factura ON;
	INSERT INTO VIDA_ESTATICA.Factura (id_factura, fecha, id_cliente)
		SELECT DISTINCT 
		[Factura_Numero], 
		CONVERT(DATETIME, [Factura_Fecha], 103), 
		(SELECT id FROM VIDA_ESTATICA.Cliente WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Factura_Numero] IS NOT NULL
SET IDENTITY_INSERT VIDA_ESTATICA.Factura OFF;
COMMIT;

BEGIN TRANSACTION
	INSERT INTO VIDA_ESTATICA.Items(descripcion)
		SELECT DISTINCT [Item_Factura_Descr]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Item_Factura_Descr] IS NOT NULL
COMMIT;		

BEGIN TRANSACTION
	INSERT INTO VIDA_ESTATICA.Item_Factura (id_factura, id_item, num_cuenta, monto,facturado, fecha)
	SELECT 
	[Factura_Numero],
	(SELECT id_item FROM VIDA_ESTATICA.Items WHERE descripcion = [Item_Factura_Descr]) 'id_item',
	(SELECT id FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = [Cuenta_Numero]), 
	[Item_Factura_Importe], 
	1 'facturado', 
	CONVERT(DATETIME, [Transf_Fecha], 103)
	FROM [GD1C2015].gd_esquema.Maestra WHERE Item_Factura_Descr IS NOT NULL
COMMIT;
GO

GO

--
-- TRIGGERS
--

IF OBJECT_ID('VIDA_ESTATICA.updateSaldoAfterDeposit') IS NOT NULL
BEGIN
	DROP TRIGGER VIDA_ESTATICA.updateSaldoAfterDeposit;
END;

GO

IF OBJECT_ID('VIDA_ESTATICA.updateSaldoAfterRetiro') IS NOT NULL
BEGIN
	DROP TRIGGER VIDA_ESTATICA.updateSaldoAfterRetiro;
END;
GO

IF OBJECT_ID('VIDA_ESTATICA.updateSaldoAfterTransferencia') IS NOT NULL
BEGIN
	DROP TRIGGER VIDA_ESTATICA.updateSaldoAfterTransferencia;
END;
GO

-- Trigger para cambiar el saldo de la cuenta cuando alguien deposita.
CREATE TRIGGER updateSaldoAfterDeposit ON VIDA_ESTATICA.Deposito
AFTER INSERT
AS BEGIN TRANSACTION

	DECLARE @uImporte numeric(15, 2);
	DECLARE @uCuenta numeric(16, 0);
	
	-- Necesito la última fila insertada.
	SELECT TOP 1 @uImporte = importe, @uCuenta = cuenta_destino 
	FROM inserted 
	ORDER BY id DESC;
	
	UPDATE VIDA_ESTATICA.Cuenta
	SET saldo = saldo + @uImporte
	WHERE id = @uCuenta;
COMMIT;

GO

CREATE TRIGGER updateSaldoAfterRetiro ON VIDA_ESTATICA.Cheque
AFTER INSERT
AS BEGIN TRANSACTION

	DECLARE @uImporte numeric(15, 2);
	DECLARE @uCuenta numeric(16, 0);
	
	-- Necesito la última fila insertada.
	SELECT TOP 1 @uImporte = importe, @uCuenta = cuenta_destino
	FROM inserted 
	ORDER BY id DESC;
	
	UPDATE VIDA_ESTATICA.Cuenta
	SET saldo = saldo - @uImporte
	WHERE id = @uCuenta;
COMMIT;
GO

CREATE TRIGGER VIDA_ESTATICA.updateSaldoAfterTransferencia ON VIDA_ESTATICA.Transferencia
AFTER INSERT
AS BEGIN TRANSACTION

	DECLARE @uImporte numeric(15, 2);
	DECLARE @uCuenta numeric(16, 0);
	DECLARE @oCuenta numeric(16, 0);
	DECLARE @costo int;
	
	-- Necesito la última fila insertada.
	SELECT TOP 1 @uImporte = importe, @uCuenta = cuenta_destino, @oCuenta = cuenta_origen
	FROM inserted 
	ORDER BY id DESC;
	
	SELECT @costo = costo_transaccion 
	FROM VIDA_ESTATICA.Tipo_Cuenta
	WHERE id = (SELECT tipo_cuenta FROM VIDA_ESTATICA.Cuenta WHERE id = @uCuenta)
	
	UPDATE VIDA_ESTATICA.Cuenta
	SET saldo = saldo + @uImporte
	WHERE id = @uCuenta;
	
	UPDATE VIDA_ESTATICA.Cuenta
	SET saldo = saldo - @uImporte - @costo
	WHERE id = @oCuenta;
COMMIT;
GO

BEGIN TRANSACTION
SET IDENTITY_INSERT VIDA_ESTATICA.Deposito ON;
INSERT INTO VIDA_ESTATICA.Deposito (id, cuenta_destino, importe, tipo_moneda, tarjeta, fecha, emisor)
		SELECT [Deposito_Codigo],(SELECT id FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = [Cuenta_Numero]),[Deposito_Importe], 1, [Tarjeta_Numero],[Deposito_Fecha], 1
	    FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Deposito_Codigo IS NOT NULL
SET IDENTITY_INSERT VIDA_ESTATICA.Deposito OFF;
COMMIT;
GO

BEGIN TRANSACTION
SET IDENTITY_INSERT VIDA_ESTATICA.Retiro ON;
INSERT INTO VIDA_ESTATICA.Retiro(id , fecha, importe, moneda, cuenta_destino)
SELECT
[Retiro_Codigo],
Retiro_Fecha, 
Retiro_Importe, 
1, 
(SELECT id FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = [Cuenta_Numero])
FROM gd_esquema.Maestra
WHERE Retiro_Codigo IS NOT NULL;
SET IDENTITY_INSERT VIDA_ESTATICA.Retiro OFF;
COMMIT;

INSERT INTO VIDA_ESTATICA.Cheque(id_egreso, cod_banco, cuenta_destino, fecha, importe, tipo_moneda)
SELECT DISTINCT Cheque_Numero, Banco_Cogido, c.id, Cheque_Fecha, Cheque_Importe, 1
FROM gd_esquema.Maestra m
INNER JOIN VIDA_ESTATICA.Cuenta c
ON c.num_cuenta = m.Cuenta_Numero
WHERE Cheque_Numero IS NOT NULL;
GO

INSERT INTO VIDA_ESTATICA.Transferencia(cuenta_origen, cuenta_destino, fecha, costo, tipo_moneda, importe)
SELECT DISTINCT
(SELECT id FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = [Cuenta_Numero]),
(SELECT id FROM VIDA_ESTATICA.Cuenta WHERE num_cuenta = [Cuenta_Dest_Numero]), 
 Transf_Fecha, 
[Trans_Costo_Trans],
1,
Trans_Importe
FROM gd_esquema.Maestra
WHERE Transf_Fecha IS NOT NULL;

GO

-- Stored Procedures

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

IF OBJECT_ID('VIDA_ESTATICA.roles_usuario') IS NOT NULL
BEGIN
	DROP FUNCTION VIDA_ESTATICA.roles_usuario;
END;
GO

IF OBJECT_ID('VIDA_ESTATICA.agregarRol') IS NOT NULL
BEGIN
	DROP PROCEDURE VIDA_ESTATICA.agregarRol;
END;
GO

IF OBJECT_ID('VIDA_ESTATICA.agregarCliente') IS NOT NULL
BEGIN
	DROP PROCEDURE VIDA_ESTATICA.agregarCliente;
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

CREATE PROCEDURE VIDA_ESTATICA.agregarRol(@nombreRol varchar(255), @ret numeric(18,0) output)
AS BEGIN
	INSERT INTO VIDA_ESTATICA.Rol (nombre, activo) VALUES (@nombreRol, 1)
	SET @ret = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE VIDA_ESTATICA.agregarCliente(@nombreCliente varchar(255), @apellidoCliente varchar(255), 
												@documentoCliente numeric(8,0), @domCalleCliente varchar(50), 
												@domNroCliente numeric(6,0), @domPisoCliente numeric(2,0), 
												@domDptoCliente varchar(1), @fecNacCliente Datetime, 
												@mailCliente varchar(50), @nacionalidadCliente numeric(18, 0),
												@tipoDocCliente numeric(18, 0), @usuarioCliente varchar(25),
												@activo bit, @ret numeric(18,0) output)
AS BEGIN
	INSERT INTO VIDA_ESTATICA.Cliente (nombre, apellido, documento, dom_calle, dom_nro, 
									   dom_piso, dom_dpto, fecha_nac, mail, nacionalidad, 
									   tipo_documento, usuario, activo) VALUES (@nombreCliente, @apellidoCliente, @documentoCliente,
																		@domCalleCliente, @domNroCliente, @domPisoCliente,
																		@domDptoCliente, @fecNacCliente, @mailCliente,
																		@nacionalidadCliente, @tipoDocCliente, @usuarioCliente, @activo)
	SET @ret = SCOPE_IDENTITY()
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