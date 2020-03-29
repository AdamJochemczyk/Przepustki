CREATE TABLE [dbo].[Table]
(
	[IdOsoby] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Imie] NVARCHAR(50) NOT NULL, 
    [Nazwisko] NVARCHAR(50) NOT NULL, 
    [Strefa_poruszania] NCHAR(10) NOT NULL, 
    [Cel_podrozy] NCHAR(10) NOT NULL, 
    [Numer_dowodu] NCHAR(10) NOT NULL
)
