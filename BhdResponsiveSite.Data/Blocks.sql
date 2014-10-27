CREATE TABLE [dbo].[Blocks]
(
	[BlockId_pkey] INT NOT NULL PRIMARY KEY, 
    [PageId_FK] INT NOT NULL, 
    [BlockName] VARCHAR(50) NULL, 
    [BlockContent] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Blocks_Page] FOREIGN KEY (PageId_FK) REFERENCES Pages(PageID_PK) 
)
