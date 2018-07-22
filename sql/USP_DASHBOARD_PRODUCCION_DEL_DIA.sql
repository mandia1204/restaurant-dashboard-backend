USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PRODUCCION_DEL_DIA]    Script Date: 04/29/2017 11:36:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_PRODUCCION_DEL_DIA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_PRODUCCION_DEL_DIA]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PRODUCCION_DEL_DIA]    Script Date: 04/29/2017 11:36:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_DASHBOARD_PRODUCCION_DEL_DIA]
AS
BEGIN
SET NOCOUNT ON

DECLARE @date DATETIME
SET @date = CONVERT(DATE, GETDATE())
DECLARE @dateEnd datetime = DATEADD(DAY, 1, @date);

--nVenta:FLOAT
SELECT ISNULL(SUM(nVenta),0) [Value], --venta
	ISNULL(SUM(CASE WHEN ttipopedido='01' THEN nVenta ELSE 0 END),0) [Value2] --total_paloteo
FROM DBO.DPEDIDO 
WHERE 
	fRegistro >= @date 
	AND fRegistro< @dateEnd
	AND lImprime ='true' 
END
SET ANSI_NULLS OFF
GO

