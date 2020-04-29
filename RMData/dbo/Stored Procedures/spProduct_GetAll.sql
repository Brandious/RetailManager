CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS

BEGIN
	
	set nocount on;

	SELECT 
			Id, ProductName, [Description], 
			QuantityInStock, RetailPrice, IsTaxable
	FROM [dbo].[Product] ORDER BY ProductName;
END
