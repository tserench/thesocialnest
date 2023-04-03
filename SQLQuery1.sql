CREATE TABLE [dbo].[FriendOfUser] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserId]   NVARCHAR (128) NOT NULL,
    [FriendId] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    FOREIGN KEY ([FriendId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

/*----------------------------------------------------------------------------*/
CREATE TABLE [dbo].[FriendRequest] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          NVARCHAR (128) NOT NULL,
    [RequestSenderId] NVARCHAR (128) NOT NULL,
    [Time]            DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    FOREIGN KEY ([RequestSenderId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
/*----------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Post] (
    [PostId]     INT            IDENTITY (1, 1) NOT NULL,
    [Content]       TEXT           NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [Time]       DATETIME       NOT NULL,
    [Visibility] NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([PostId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
/*----------------------------------------------------------------------------*/
CREATE TABLE [dbo].[PostComment] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [PostId]  INT            NOT NULL,
    [UserId]  NVARCHAR (128) NOT NULL,
    [Comment] TEXT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
/*----------------------------------------------------------------------------*/
CREATE TABLE [dbo].[PostReact] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [PostId] INT            NOT NULL,
    [UserId] NVARCHAR (128) NOT NULL,
    [React]  BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);