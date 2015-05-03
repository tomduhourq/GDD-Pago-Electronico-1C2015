IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.schemata
WHERE   schema_name = 'VIDA_ESTATICA' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA VIDA_ESTATICA AUTHORIZATION gd'
END

IF OBJECT_ID('VIDA_ESTATICA.Pais') IS NOT NULL
BEGIN
	DROP TABLE VIDA_ESTATICA.Pais;
END;

CREATE TABLE VIDA_ESTATICA.Pais (
	id numeric(18, 0) PRIMARY KEY,
	descripcion varchar(250) NOT NULL);

INSERT INTO VIDA_ESTATICA.Pais 
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM gd_esquema.Maestra;

GO