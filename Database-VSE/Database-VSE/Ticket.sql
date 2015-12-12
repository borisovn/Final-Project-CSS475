CREATE TABLE [dbo].[Ticket] (
    [TicketId]        INT      IDENTITY(1,1)      NOT NULL,
    [TicketNumber]    INT           NULL,
    [PerformanceName] NVARCHAR (50) NOT NULL,
    [Price]           DECIMAL (18)  NULL,
    PRIMARY KEY CLUSTERED (TicketId ASC),
    CONSTRAINT [FK_dbo.Ticket.PerformanceName] FOREIGN KEY ([PerformanceName]) REFERENCES [dbo].[Performance] ([PerformanceName]) ON DELETE CASCADE
);

