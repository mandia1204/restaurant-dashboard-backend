USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_VENTA_ANUAL]    Script Date: 04/24/2017 20:10:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_VENTA_ANUAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_VENTA_ANUAL]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_VENTA_ANUAL]    Script Date: 04/24/2017 20:10:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

--[USP_DASHBOARD_VENTA_ANUAL] 2016
CREATE Procedure [dbo].[USP_DASHBOARD_VENTA_ANUAL]
@YEAR INT 
AS
BEGIN

SET NOCOUNT ON

DECLARE @date DATETIME
SET @date = CONVERT(DATE,CONCAT(@YEAR,'-01-01'))
DECLARE @dateEnd datetime = DATEADD(YEAR, 1, @date);

SELECT 
    MONTH(fRegistro) [Key], SUM(nventa) [Value] 
FROM dbo.MDOCUMENTO
WHERE tTipoDocumento= '01' 
    AND fRegistro >= @date 
	AND fRegistro < @dateEnd
GROUP BY MONTH(fRegistro)

END

SET ANSI_NULLS OFF

GO

