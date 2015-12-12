CREATE TABLE [dbo].[User] (
    [UserId]    INT       IDENTITY(1,1)     NOT NULL ,
    [LastName]  NVARCHAR (50) NULL,
    [FirstName] NVARCHAR (50) NULL,
    [LoginName] NVARCHAR (50) NULL,
    [Password]  NVARCHAR (50) NULL,
    [email]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([email] ASC),
	 UNIQUE NONCLUSTERED ([LoginName] ASC)
);

