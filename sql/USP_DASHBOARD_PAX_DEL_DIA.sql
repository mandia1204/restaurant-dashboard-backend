USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PAX_DEL_DIA]    Script Date: 04/29/2017 11:32:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_PAX_DEL_DIA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_PAX_DEL_DIA]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PAX_DEL_DIA]    Script Date: 04/29/2017 11:32:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_DASHBOARD_PAX_DEL_DIA]
AS
BEGIN
SET NOCOUNT ON

DECLARE @date DATETIME
DECLARE @year INT
DECLARE @month INT
DECLARE @day INT

SET @date = GETDATE()
SELECT @year =YEAR(@date), @month=MONTH(@date), @day= DAY(@date)
--NADULTO:smallint
SELECT ISNULL(SUM(NADULTO),0) AS total
FROM dbo.MPEDIDO
WHERE tEstadoPedido!='03' 
	AND YEAR(fRegistro) = @year 
	AND MONTH(fRegistro)=@month
	AND DAY(fRegistro)=@day
END
GO

