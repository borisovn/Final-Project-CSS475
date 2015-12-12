CREATE TABLE [dbo].[UserHistory] (
    [UserHistroryId]   INT      IDENTITY(1,1)      NOT NULL,
    [userID]          INT           NOT NULL,
    [TicketID]        INT           NOT NULL,
    [PerformanceName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserHistory] PRIMARY KEY CLUSTERED ([UserHistroryId] ASC),
    CONSTRAINT [FK_dbo.UserHistory.userID] FOREIGN KEY ([userID]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_dbo.UserHistory.TicketNumber] FOREIGN KEY ([TicketID]) REFERENCES [dbo].[Ticket] ([TicketId]),
    CONSTRAINT [FK_dbo.UserHistory.PerformanceName] FOREIGN KEY ([PerformanceName]) REFERENCES [dbo].[Performance] ([PerformanceName])
);

