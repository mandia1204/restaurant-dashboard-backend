USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_ANULACIONES]    Script Date: 04/29/2017 11:32:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_ANULACIONES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_ANULACIONES]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_ANULACIONES]    Script Date: 04/29/2017 11:32:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[USP_DASHBOARD_ANULACIONES]
AS
BEGIN
SET NOCOUNT ON

SELECT TOP 10 
	a.fRegistroAnulado fecha, a.tObservacion observacion,a.tMotivoEliminacion tipo
FROM APEDIDO a
--INNER JOIN TTABLA t ON t.TCODIGO=a.tMotivoEliminacion AND t.TTABLA='MOTIVOELIMINACION'   
WHERE tMotivoEliminacion <> '' 
ORDER BY a.fRegistroAnulado DESC

END
GO

