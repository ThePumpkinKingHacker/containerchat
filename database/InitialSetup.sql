IF NOT EXISTS (select * from sysobjects where name='User' and xtype='U')
	CREATE TABLE [User] (
		[ID] int NOT NULL IDENTITY,
		[UserName] varchar(255) NOT NULL,
		[Password] varchar(255) NOT NULL,
		[RoleEnum] tinyint NOT NULL, --0=Admin,1=User
		CONSTRAINT User_PK PRIMARY KEY (ID)	
	)
GO

IF NOT EXISTS (select * from sysobjects where name='Message' and xtype='U')
	CREATE TABLE [Message] (
		[ID] int NOT NULL IDENTITY,
		[UserID] int NOT NULL,
		[Timestamp] datetime NOT NULL,
		[Content] varchar(1000) NOT NULL,
		CONSTRAINT Message_PK PRIMARY KEY (ID),
		CONSTRAINT Message_FK1 FOREIGN KEY (UserID) REFERENCES [User]
	);
GO
