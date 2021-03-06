USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES]    Script Date: 04/24/2017 20:10:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES]    Script Date: 04/24/2017 20:10:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

--[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES] 2018,4
CREATE Procedure [dbo].[USP_DASHBOARD_PLATOS_MAS_VENDIDOS_MES]
@YEAR INT,
@MONTH INT
AS
BEGIN

SET NOCOUNT ON

DECLARE @date DATETIME
SET @date = CONVERT(DATE,CONCAT(@YEAR,'-',@MONTH,'-01'))
DECLARE @dateEnd datetime = DATEADD(MONTH, 1, @date);

SELECT TOP 10
	prod.tResumido [Key], Count(prod.tResumido) [Value]
FROM DDOCUMENTO det
	INNER JOIN MDOCUMENTO doc on doc.tDocumento = det.tDocumento
	INNER JOIN TPRODUCTO prod on prod.tCodigoProducto = det.tCodigoProducto
WHERE 
	doc.tTipoDocumento = '01' 
	AND tTipoProducto='01' 
	AND doc.fRegistro >= @date 
	AND doc.fRegistro< @dateEnd
GROUP BY prod.tResumido
order by count(prod.tResumido) DESC

END

SET ANSI_NULLS OFF

GO

