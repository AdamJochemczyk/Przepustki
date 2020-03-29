CREATE TABLE [dbo].[Table]
(
	[IdPojazdu] INT NOT NULL  IDENTITY PRIMARY KEY, 
    [NrRej] NCHAR(10) NOT NULL, 
    [Masa] NCHAR(10) NOT NULL, 
    [Marka] NCHAR(10) NOT NULL, 
    [Typ] NCHAR(10) NOT NULL, 
    [Osoby] INT NOT NULL, 
    [Firma] NCHAR(10) NOT NULL, 
    [Dowód_wjazdu] NCHAR(10) NOT NULL
)
