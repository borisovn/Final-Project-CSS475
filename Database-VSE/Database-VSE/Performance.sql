CREATE TABLE [dbo].[Performance] (
    [PerformanceName] NVARCHAR (50)  NOT NULL,
    [Artist]          NVARCHAR (50)  NOT NULL,
    [Venue]           NVARCHAR (50)  NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Quantity]        INT            NULL,
    [TimeStarts]      DATETIME       NULL,
    [TimeEnds]        DATETIME       NULL,
    CONSTRAINT [PK_Performance] PRIMARY KEY CLUSTERED ([PerformanceName] ASC),
    UNIQUE NONCLUSTERED ([PerformanceName] ASC),
    CONSTRAINT [FK_dbo.Performance.Venue] FOREIGN KEY ([Venue]) REFERENCES [dbo].[Venue] ([VenueName]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Performance.Artist] FOREIGN KEY ([Artist]) REFERENCES [dbo].[Artist] ([ArtistName]) ON DELETE CASCADE
);

