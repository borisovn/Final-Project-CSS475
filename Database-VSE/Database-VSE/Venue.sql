CREATE TABLE [dbo].[Venue] (
    [VenueName] NVARCHAR (50)  NOT NULL,
    [City]      NVARCHAR (50)  NULL,
    [State]     NVARCHAR (4)   NULL,
    [ZIPCODE]   NVARCHAR (6)   NULL,
    [Address]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([VenueName] ASC)
);

