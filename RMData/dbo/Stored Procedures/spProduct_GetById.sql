CREATE PROCEDURE [dbo].[spProduct_GetById]
	@id int
AS

BEGIN
	set nocount on;
		SELECT 
			Id, ProductName, [Description], 
			QuantityInStock, RetailPrice, IsTaxable
	FROM [dbo].[Product] WHERE Id = @id ORDER BY ProductName;
END