USE [MSTPLFixedBarcodeReaderDB]
GO
/****** Object:  UserDefinedTableType [dbo].[BarcodeTransID]    Script Date: 06-02-2018 5:52:18 PM ******/
CREATE TYPE [dbo].[BarcodeTransID] AS TABLE(
	[ID] [bigint] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[BarcodeReaderMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarcodeReaderMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SerialNumber] [nvarchar](100) NULL,
	[Manufacturer] [nvarchar](50) NULL,
	[ModelNumber] [nvarchar](50) NULL,
	[CreationTime] [datetime] NULL,
 CONSTRAINT [PK_BarcodeReaderMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IPV4Address] [nvarchar](50) NOT NULL,
	[MACAddress] [nvarchar](100) NULL,
 CONSTRAINT [PK_DeviceMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorCodeMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorCodeMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ErrorCodeMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LaminatorBarcodeReaderMappingMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaminatorBarcodeReaderMappingMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LaminatorID] [int] NOT NULL,
	[BarcodeReaderID] [int] NOT NULL,
	[DeviceID] [int] NULL,
 CONSTRAINT [PK_LaminatorBarcodeReaderMappingMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LaminatorMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaminatorMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LaminatorNumber] [nvarchar](50) NULL,
	[CreationTime] [datetime] NULL,
 CONSTRAINT [PK_LaminatorMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction_Table]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction_Table](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Module_Serial_Number] [nvarchar](100) NULL,
	[CreationTime] [datetime] NULL,
	[LaminatorBarcodeReaderMappingID] [int] NOT NULL,
	[Displayed] [bit] NOT NULL,
	[ErrorID] [int] NULL,
	[IsErrorResolved] [bit] NULL,
 CONSTRAINT [PK_Transaction_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IsLoggedIn] [bit] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BarcodeReaderMaster] ADD  CONSTRAINT [DF_BarcodeReaderMaster_CreationTime]  DEFAULT (getdate()) FOR [CreationTime]
GO
ALTER TABLE [dbo].[LaminatorMaster] ADD  CONSTRAINT [DF_LaminatorMaster_CreationTime]  DEFAULT (getdate()) FOR [CreationTime]
GO
ALTER TABLE [dbo].[Transaction_Table] ADD  CONSTRAINT [DF_Transaction_Table_CreationTime]  DEFAULT (getdate()) FOR [CreationTime]
GO
ALTER TABLE [dbo].[Transaction_Table] ADD  CONSTRAINT [DF_Transaction_Table_Displayed]  DEFAULT ((0)) FOR [Displayed]
GO
ALTER TABLE [dbo].[Transaction_Table] ADD  CONSTRAINT [DF_Transaction_Table_ErrorID]  DEFAULT ((0)) FOR [ErrorID]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsLoggedIn]  DEFAULT ((0)) FOR [IsLoggedIn]
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster]  WITH NOCHECK ADD  CONSTRAINT [FK_BarcodeReader_LaminatorBarcodeReaderMappingMaster] FOREIGN KEY([BarcodeReaderID])
REFERENCES [dbo].[BarcodeReaderMaster] ([ID])
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster] NOCHECK CONSTRAINT [FK_BarcodeReader_LaminatorBarcodeReaderMappingMaster]
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster]  WITH CHECK ADD  CONSTRAINT [FK_LaminatorBarcodeReaderMappingMaster_DeviceMaster] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[DeviceMaster] ([ID])
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster] CHECK CONSTRAINT [FK_LaminatorBarcodeReaderMappingMaster_DeviceMaster]
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster]  WITH NOCHECK ADD  CONSTRAINT [FK_LaminatorMaster_LaminatorBarcodeReaderMappingMaster] FOREIGN KEY([LaminatorID])
REFERENCES [dbo].[LaminatorMaster] ([ID])
GO
ALTER TABLE [dbo].[LaminatorBarcodeReaderMappingMaster] NOCHECK CONSTRAINT [FK_LaminatorMaster_LaminatorBarcodeReaderMappingMaster]
GO
ALTER TABLE [dbo].[Transaction_Table]  WITH NOCHECK ADD  CONSTRAINT [FK_Transaction_Table_ErrorCodeMaster] FOREIGN KEY([ErrorID])
REFERENCES [dbo].[ErrorCodeMaster] ([ID])
GO
ALTER TABLE [dbo].[Transaction_Table] NOCHECK CONSTRAINT [FK_Transaction_Table_ErrorCodeMaster]
GO
ALTER TABLE [dbo].[Transaction_Table]  WITH NOCHECK ADD  CONSTRAINT [FK_Transaction_Table_LaminatorBarcodeReaderMappingMaster] FOREIGN KEY([LaminatorBarcodeReaderMappingID])
REFERENCES [dbo].[LaminatorBarcodeReaderMappingMaster] ([ID])
GO
ALTER TABLE [dbo].[Transaction_Table] NOCHECK CONSTRAINT [FK_Transaction_Table_LaminatorBarcodeReaderMappingMaster]
GO
/****** Object:  StoredProcedure [dbo].[InsertNewuser]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertNewuser]
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(100),
	@Password nvarchar(100),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Email nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT
	INTO UserMaster
	(Name, Password, FirstName, LastName, Email)
	VALUES
	(@UserName, @Password, @FirstName, @LastName, @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllBarcodeReaderDetails]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllBarcodeReaderDetails]
	@DeviceIP nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TempBarcodeReaderMaster TABLE
	(
		BarcodeReaderID int,
		BarcodeReaderSerialNumber nvarchar(100),
		LaminatorBarcodeReaderMappingID int,
		DeviceID int
	);

	INSERT INTO @TempBarcodeReaderMaster
	SELECT
		brm.ID as BarcodeReaderID,
		brm.SerialNumber as BarcodeReaderSerialNumber,
		lbr.ID as LaminatorBarcodeReaderMappingID,
		lbr.DeviceID as DeviceID
	FROM
		[dbo].[BarcodeReaderMaster] brm
	LEFT JOIN
		[dbo].[LaminatorBarcodeReaderMappingMaster] lbr
	ON
		brm.ID=lbr.BarcodeReaderID
		
	
	SELECT 
		tbrm.BarcodeReaderID as ID,
		tbrm.BarcodeReaderSerialNumber as SerialNumber,
		dm.IPV4Address 
	FROM
		@TempBarcodeReaderMaster tbrm 
	LEFT JOIN 
		DeviceMaster dm 
	ON 
		tbrm.DeviceID=dm.ID 
	WHERE 
		(@DeviceIP is null or dm.IPV4Address=@DeviceIP)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllBarcodeTransactions]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllBarcodeTransactions] 
	@Module_Serial_Number nvarchar(100)=null,
	@Barcode_Reader_Serial_Number nvarchar(100)=null,
	@Laminator_Number nvarchar(50)=null,
	@Barcode_Scan_Date date=null,
	@IsSearchOp bit=0,
	@BarcodeTransID bigint=null,
	@StartLaminatorID int= null,
	@EndLaminatorID int = null,
	@DeviceIP nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TempBarcodeMaster TABLE
	(
		BarcodeTransId bigint primary key,
		ModSerNum nvarchar(100),
		BarcodeReaderID int,
		LaminatorID int,
		CreationTime datetime,
		Displayed bit,
		ErrorID int,
		IsErrorResolved bit,
		ErrorMessage nvarchar(100),
		DeviceID int
	);
	INSERT INTO
		@TempBarcodeMaster
	SELECT
		tt.ID as BarcodeTransId,
		tt.Module_Serial_Number as ModSerNum,
		lbm.BarcodeReaderID as BarcodeReaderID,
		lbm.LaminatorID as LaminatorID,
		tt.CreationTime as CreationTime,
		tt.Displayed as Displayed,
		tt.ErrorID as ErrorID,
		tt.IsErrorResolved as IsErrorResolved,
		ecm.Message as ErrorMessage,
		lbm.DeviceID as DeviceID
	FROM
		Transaction_Table tt
	LEFT JOIN
		LaminatorBarcodeReaderMappingMaster lbm
	ON
		tt.LaminatorBarcodeReaderMappingID=lbm.ID
	LEFT JOIN
		ErrorCodeMaster ecm
	ON
		tt.ErrorID=ecm.ID	
		
	SELECT 
		tbm.BarcodeTransId as ID, 
		tbm.ModSerNum as Module_Serial_Number, 
		brm.SerialNumber as BarcodeReaderSerialNumber, 
		lm.LaminatorNumber as LaminatorNumber,
		tbm.CreationTime,
		tbm.ErrorID as ErrorID,
		tbm.ErrorMessage as ErrorMessage,
		tbm.IsErrorResolved as IsErrorResolved,
		dm.IPV4Address as DeviceIP
	FROM
		@TempBarcodeMaster tbm
	LEFT JOIN
		BarcodeReadermaster brm
	ON
		tbm.BarcodeReaderID=brm.ID
	LEFT JOIN
		LaminatorMaster lm
	ON
		tbm.LaminatorID=lm.ID
	LEFT JOIN
		DeviceMaster dm
	ON
		tbm.DeviceID = dm.ID
	WHERE
		(@BarcodeTransID is null or tbm.BarcodeTransId=@BarcodeTransID)
	AND
		(@IsSearchOp=1 or tbm.Displayed=@IsSearchOp)
	AND
		(@Module_Serial_Number is null or tbm.ModSerNum LIKE '%' + @Module_Serial_Number + '%')
	AND
		(@Barcode_Reader_Serial_Number is null or brm.SerialNumber LIKE '%' + @Barcode_Reader_Serial_Number + '%')
	AND
		(@Laminator_Number is null or lm.LaminatorNumber LIKE '%' + @Laminator_Number + '%')
	AND
		(@Barcode_Scan_Date is null or CAST(tbm.CreationTime as date)=CAST(@Barcode_Scan_Date as date))
	AND
		((@StartLaminatorID is null and @EndLaminatorID is null) or (tbm.LaminatorID between @StartLaminatorID and @EndLaminatorID))
	AND
		((@DeviceIP is null) or (dm.IPV4Address=@DeviceIP))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllDuplicateUsers]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllDuplicateUsers] 
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [dbo].[UserMaster] um WHERE um.Name=@UserName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllDuplicateUsersByEmail]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllDuplicateUsersByEmail] 
	-- Add the parameters for the stored procedure here
	@UserEmail nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [dbo].[UserMaster] um WHERE um.Email=@UserEmail
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBarcodeReaderDetailsByReaderID]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetBarcodeReaderDetailsByReaderID] 
	@BarcodeReaderID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[SerialNumber],
		[Manufacturer],
		[ModelNumber],
		[CreationTime]
	FROM 
		[dbo].[BarcodeReaderMaster] 
	WHERE 
		[ID]=@BarcodeReaderID 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDeviceDetailsByLaminatorBarcodeReaderMappingID]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetDeviceDetailsByLaminatorBarcodeReaderMappingID] 
	-- Add the parameters for the stored procedure here
	@LaminatorBarcodeReaderMappingID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		dm.ID,
		dm.IPV4Address,
		dm.MACAddress
	FROM 
		[dbo].[LaminatorBarcodeReaderMappingMaster] lbm
	LEFT JOIN
		[dbo].[DeviceMaster] dm
	ON
		lbm.DeviceID = dm.ID
	WHERE
		lbm.ID=@LaminatorBarcodeReaderMappingID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLaminatorBarcodeReaderMappingIDByBarcodeReaderID]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetLaminatorBarcodeReaderMappingIDByBarcodeReaderID]
	-- Add the parameters for the stored procedure here
	@BarcodeReaderID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ID from [dbo].[LaminatorBarcodeReaderMappingMaster] where BarcodeReaderID = @BarcodeReaderID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLaminatorDetailsbyLaminatorBarcodeMappingID]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetLaminatorDetailsbyLaminatorBarcodeMappingID] 
	-- Add the parameters for the stored procedure here
	@LaminatorBarcodeMappingID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TempLaminatorMaster TABLE(
		ID int,
		LaminatorNumber nvarchar(50),
		CreationTime datetime,
		LaminatorBarcodeMappingID int
	);

	INSERT INTO @TempLaminatorMaster
	SELECT 
		lm.ID as ID,
		lm.LaminatorNumber as LaminatorNumber,
		lm.CreationTime as CreationTime,
		lbm.ID
	FROM 
		LaminatorMaster lm 
	LEFT JOIN
		LaminatorBarcodeReaderMappingMaster lbm
	ON
		lm.ID = lbm.LaminatorID;

		SELECT
			[ID],
			[LaminatorNumber],
			[CreationTime],
			tlm.LaminatorBarcodeMappingID
		FROM
			@TempLaminatorMaster tlm
		WHERE
			tlm.LaminatorBarcodeMappingID = @LaminatorBarcodeMappingID

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetails]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUserDetails] 
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(100),
	@Password nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID, Name, Password, IsLoggedIn from [dbo].[UserMaster] where Name=@UserName and Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertNewuser]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertNewuser]
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(100),
	@Password nvarchar(100),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Email nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT
	INTO UserMaster
	(Name, Password, FirstName, LastName, Email)
	VALUES
	(@UserName, @Password, @FirstName, @LastName, @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_MarkBarcodeTransactionsDisplayed]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MarkBarcodeTransactionsDisplayed] 
	-- Add the parameters for the stored procedure here
	@BarcodeTransIds as BarcodeTransID readonly
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Transaction_Table SET Displayed = 1 where ID in (SELECT ID FROM @BarcodeTransIds)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReturnLoggedInUserNums]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReturnLoggedInUserNums]
	-- Add the parameters for the stored procedure here
	@CurrentUserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM UserMaster WHERE IsLoggedIn=1 AND ID!=@CurrentUserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveBarcodeTransaction]    Script Date: 06-02-2018 5:52:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveBarcodeTransaction] 
	-- Add the parameters for the stored procedure here
	@BarcodeTransactionID bigint=0,
	@ModSerNum nvarchar(100) = null,
	@CreationTime datetime,
	@LaminatorBarcodeReaderMappingID int,
	@ErrorID int=null,
	@IsErrorResolved bit=0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@BarcodeTransactionID <= 0)
		BEGIN
			INSERT INTO [dbo].[Transaction_Table] (Module_Serial_Number, CreationTime, LaminatorBarcodeReaderMappingID, ErrorID, IsErrorResolved) OUTPUT inserted.ID values (@ModSerNum, @CreationTime, @LaminatorBarcodeReaderMappingID, @ErrorID, @IsErrorResolved)
			--SELECT IDENT_CURRENT('[MSTPLFixedBarcodeReaderDB].[dbo].[Transaction_Table]')
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Transaction_Table] SET Module_Serial_Number=@ModSerNum, LaminatorBarcodeReaderMappingID=@LaminatorBarcodeReaderMappingID,IsErrorResolved=@IsErrorResolved  WHERE ID=@BarcodeTransactionID
		END
END
GO
