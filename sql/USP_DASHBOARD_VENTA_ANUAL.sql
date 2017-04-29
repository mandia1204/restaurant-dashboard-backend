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


CREATE Procedure [dbo].[USP_DASHBOARD_VENTA_ANUAL]
AS
BEGIN

set nocount on

SELECT MONTH(fRegistro) mes,SUM(nventa) total FROM dbo.MDOCUMENTO
WHERE tTipoDocumento= '01' AND YEAR(fRegistro)='2017'
GROUP BY MONTH(fRegistro)

END

SET ANSI_NULLS OFF

GO

