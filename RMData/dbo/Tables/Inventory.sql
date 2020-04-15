CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 1,
    [PurchaseDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
    [PurchasePrice] MONEY NOT NULL , 
   
)
