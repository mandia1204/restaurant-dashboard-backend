USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PRODUCTOS_VENTAS_MES]    Script Date: 04/24/2017 20:10:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_DASHBOARD_PRODUCTOS_VENTAS_MES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_DASHBOARD_PRODUCTOS_VENTAS_MES]
GO

USE [VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_DASHBOARD_PRODUCTOS_VENTAS_MES]    Script Date: 04/24/2017 20:10:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

--[USP_DASHBOARD_PRODUCTOS_VENTAS_MES] 2016,3
CREATE Procedure [dbo].[USP_DASHBOARD_PRODUCTOS_VENTAS_MES]
@YEAR INT,
@MONTH INT
AS
BEGIN

set nocount on

SELECT prod.tTipoProducto [Key], SUM(doc.nVenta) [Value]
FROM DDOCUMENTO det
	INNER JOIN MDOCUMENTO doc on doc.tDocumento = det.tDocumento
	INNER JOIN TPRODUCTO prod on prod.tCodigoProducto = det.tCodigoProducto
WHERE doc.tTipoDocumento = '01' AND YEAR(doc.fRegistro) = @YEAR AND MONTH(doc.fRegistro)=@MONTH
GROUP BY prod.tTipoProducto

END

SET ANSI_NULLS OFF

GO

