CREATE TABLE [dbo].[Items] (
    [Id]       INT            NOT NULL,
    [Category] NVARCHAR (MAX) NOT NULL,
    [Type]     NVARCHAR (MAX) NOT NULL,
    [Brand]    NVARCHAR (MAX) NOT NULL,
    [Model]    NVARCHAR (MAX) NOT NULL,
	[Characteristics] NVARCHAR (MAX) NOT NULL,
    [PriceIn]  FLOAT (53)     NOT NULL,
    [PriceOut] FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

