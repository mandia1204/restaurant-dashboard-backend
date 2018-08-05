USE [VENTAS]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_MOZO_PRODUCCION_MES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_MOZO_PRODUCCION_MES]
GO

USE [VENTAS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[USP_DASHBOARD_MOZO_PRODUCCION_MES] 2017,6
CREATE Procedure [dbo].[USP_DASHBOARD_MOZO_PRODUCCION_MES]
@YEAR INT,
@MONTH TINYINT
AS
BEGIN
SET NOCOUNT ON

DECLARE @date DATETIME
SET @date = CONVERT(DATE,CONCAT(@YEAR,'-',@MONTH,'-01'))
DECLARE @dateEnd datetime = DATEADD(MONTH, 1, @date);

SELECT TOP 10 m.tDetallado [Key], SUM(d.ncantidad) [Value] 
FROM DPEDIDO d 
	INNER JOIN (SELECT TCODIGO,tDetallado FROM TTABLA WHERE TTABLA = 'MOZO') m ON m.TCODIGO = d.tmozod
WHERE 
	d.tmozod != '' AND d.tmozod != '0000'
	AND d.fregistro >= @date AND d.fregistro < @dateEnd
GROUP BY m.tDetallado
ORDER BY 2 desc

END
GO

