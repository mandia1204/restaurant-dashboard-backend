USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_ANULACIONES_DEL_MES]    Script Date: 04/29/2017 11:32:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_ANULACIONES_DEL_MES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_ANULACIONES_DEL_MES]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_ANULACIONES_DEL_MES]    Script Date: 04/29/2017 11:32:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[USP_DASHBOARD_ANULACIONES_DEL_MES] 2017,4
CREATE Procedure [dbo].[USP_DASHBOARD_ANULACIONES_DEL_MES]
@YEAR INT,
@MONTH TINYINT
AS
BEGIN
SET NOCOUNT ON

DECLARE @date DATETIME
SET @date = CONVERT(DATE,CONCAT(@YEAR,'-',@MONTH,'-01'))
DECLARE @dateEnd datetime = DATEADD(MONTH, 1, @date);

SELECT
	tMotivoEliminacion [Key], Count(tMotivoEliminacion) [Value]
FROM APEDIDO a
WHERE tMotivoEliminacion <> '' 
	AND fRegistroAnulado >= @date 
	AND fRegistroAnulado < @dateEnd
GROUP BY tMotivoEliminacion

END
GO

