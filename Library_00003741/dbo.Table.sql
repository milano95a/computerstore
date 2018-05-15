CREATE TABLE [dbo].Item
(
	[id] INT NOT NULL PRIMARY KEY, 
    [description] NVARCHAR(MAX) NOT NULL, 
    [characteristics] NVARCHAR(MAX) NOT NULL, 
    [brand] NVARCHAR(MAX) NOT NULL, 
    [type] NVARCHAR(MAX) NOT NULL, 
    [priceIn] FLOAT NOT NULL, 
    [priceOut] FLOAT NOT NULL
)
