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
DECLARE @year INT
DECLARE @month INT
DECLARE @day INT

SET @date = GETDATE()
SELECT @year =YEAR(@date), @month=MONTH(@date), @day= DAY(@date)
--nVenta:FLOAT
SELECT ISNULL(SUM(nVenta),0) AS total FROM DBO.DPEDIDO 
WHERE 
	YEAR(fRegistro)= @year 
	AND MONTH(fRegistro)= @month 
	AND DAY(fRegistro)= @day
	AND lImprime ='true' 
END
SET ANSI_NULLS OFF
GO

