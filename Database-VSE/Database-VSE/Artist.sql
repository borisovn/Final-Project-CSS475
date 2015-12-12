CREATE TABLE [dbo].[Artist] (
    [ArtistName]        NVARCHAR (50)  NOT NULL,
    [Review]            NCHAR (1)      NULL,
    [ArtistDescription] NVARCHAR (MAX) NULL,
    [Genre]             NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([ArtistName] ASC),
    CONSTRAINT [Check_Review] CHECK ([Review]='0' OR [Review]='1' OR [Review]='2' OR [Review]='3' OR [Review]='4' OR [Review]='5')
);

