USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_VENTA_DEL_DIA]    Script Date: 04/24/2017 20:15:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_VENTA_DEL_DIA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_VENTA_DEL_DIA]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_VENTA_DEL_DIA]    Script Date: 04/24/2017 20:15:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_DASHBOARD_VENTA_DEL_DIA]
AS
BEGIN
SET NOCOUNT ON

DECLARE @date DATETIME
DECLARE @year INT
DECLARE @month INT
DECLARE @day INT

SET @date = GETDATE()

SELECT @year =YEAR(@date), @month=MONTH(@date), @day= DAY(@date)
--NVENTA:FLOAT
SELECT ISNULL(SUM(NVENTA),0) total,
	'Ventas del día' AS title
FROM dbo.MDOCUMENTO
WHERE tTipoDocumento='01' 
	AND YEAR(fRegistro) = @year 
	AND MONTH(fRegistro)=@month
	AND DAY(fRegistro)=@day

END
SET ANSI_NULLS OFF
GO

