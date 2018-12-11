CREATE TABLE [dbo].[Modelo]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Nome] VARCHAR(50) NOT NULL, 
    [MarcaId] INT NOT NULL, 
    CONSTRAINT [FK_Modelo_Marca] FOREIGN KEY ([MarcaId]) REFERENCES [Marca]([Id])
)
