﻿CREATE PROCEDURE [dbo].[spSale_Insert]
       @Id int output,
	   @CashierId nvarchar(128),
	   @SaleDate datetime2,
	   @SubTotal money,
	   @Tax money,
	   @Total money
AS
BEGIN
	set nocount on

	INSERT INTO dbo.Sale(CashierId, SaleDate, SubTotal, Tax, Total)
		VALUES(@CashierId, @SaleDate, @SubTotal, @Tax, @Total);

	SELECT @Id = @@IDENTITY;
END
