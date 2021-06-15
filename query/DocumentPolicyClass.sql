ALTER Table Documents ADD PolicyClassID int;

USE [GuardianEpmsTest]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDocumentPolicyClass1] 
	@PolicyClassID int, @TaskControlID int

AS
BEGIN

	Update Documents set PolicyClassID = @PolicyClassID 
		where TaskControlID = @TaskControlID;

END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateDocumentPolicyClass]    Script Date: 5/20/2021 11:04:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateDocumentPolicyClass]

AS
BEGIN
	DECLARE @TaskControlID1 int;
	DECLARE @PolicyClassID1 int;
	DECLARE @CountInt int;

	SELECT TOP 1 @TaskControlID1 = TaskControlID from Documents WHERE TaskControlID is not null ORDER BY TaskControlID
	SET @CountInt = 1;
	WHILE (@CountInt < (SELECT Count(*) from Documents WHERE TaskControlID is not null))
	BEGIN
		
		SELECT @PolicyClassID1 = PolicyClassID FROM TaskControl
			WHERE TaskControlID = @TaskControlID1;

		EXEC UpdateDocumentPolicyClass1 @PolicyClassID1, @TaskControlID1;

		SELECT TOP 1 @TaskControlID1 = TaskControlID from Documents 
			WHERE TaskControlID > @TaskControlID1 and TaskControlID is not null
			ORDER BY TaskControlID;

		SET @CountInt = @CountInt + 1;

	END
	SELECT 'Count' = @CountInt;
END;

GO
/****** Object:  StoredProcedure [dbo].[AddDocuments]    Script Date: 5/20/2021 2:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddDocuments]
@CustomerNo int,
@Description varchar(200),
@TaskControlID int,
@TaskControlTypeID int,
@PolicyClassID int


AS

	INSERT Documents
	   (CustomerNo,[Description],DocumentoPublico, TaskControlID,TaskControlTypeID, PolicyClassID)
		 VALUES
	   (@CustomerNo,@Description,0, @TaskControlID,@TaskControlTypeID,@PolicyClassID)
	           
Select @@IDENTITY;

GO
/****** Object:  StoredProcedure [dbo].[GetDocumentsByCustomerNo]    Script Date: 5/20/2021 2:40:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[GetDocumentsByCustomerNo]
@CustomerNo int,
@TaskContolID int,
@OldTaskContolID int,
@PolicyClassID int null

AS

SET CONCAT_NULL_YIELDS_NULL off
if (@PolicyClassID is null)
	set @PolicyClassID = 0

if (@PolicyClassID != 0)
Begin
	Select DocumentsID,D.CustomerNo,[Description] Description, 
			IIF(ISNULL(TaskControlTypeDesc, '') LIKE '%Home Owners%', REPLACE(ISNULL(TaskControlTypeDesc, ''), 'Home Owners', 'Residential Property'), ISNULL(TaskControlTypeDesc, '')) TaskControlTypeDesc, 
			ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
		from  Documents D
		left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
		left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
		where D.CustomerNo=@CustomerNo and D.PolicyClassID = @PolicyClassID
		order by Description
End
else
Begin
	if (@OldTaskContolID != 0 AND @TaskContolID != 0)
		Begin
			Select DocumentsID,D.CustomerNo,[Description] Description, 
			IIF(ISNULL(TaskControlTypeDesc, '') LIKE '%Home Owners%', REPLACE(ISNULL(TaskControlTypeDesc, ''), 'Home Owners', 'Residential Property'), ISNULL(TaskControlTypeDesc, '')) TaskControlTypeDesc, 
			ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
			from  Documents D
			left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
			left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
			where D.CustomerNo= @CustomerNo AND TC.TaskControlID = @TaskContolID

			UNION

			Select DocumentsID,D.CustomerNo,[Description] Description, ISNULL(TaskControlTypeDesc, '') TaskControlTypeDesc, ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
			from  Documents D
			left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
			left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
			where D.CustomerNo = @CustomerNo AND D.TaskControlID = @OldTaskContolID

			order by Description
		End
	ELSE IF @TaskContolID != 0
		Begin
			Select DocumentsID,D.CustomerNo,[Description] Description, 
			IIF(ISNULL(TaskControlTypeDesc, '') LIKE '%Home Owners%', REPLACE(ISNULL(TaskControlTypeDesc, ''), 'Home Owners', 'Residential Property'), ISNULL(TaskControlTypeDesc, '')) TaskControlTypeDesc, 
			ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
			from  Documents D
			left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
			left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
			where D.CustomerNo = @CustomerNo AND TC.TaskControlID = @TaskContolID
			order by Description
		END
	ELSE IF @OldTaskContolID != 0
		Begin
			Select DocumentsID,D.CustomerNo,[Description] Description, 
			IIF(ISNULL(TaskControlTypeDesc, '') LIKE '%Home Owners%', REPLACE(ISNULL(TaskControlTypeDesc, ''), 'Home Owners', 'Residential Property'), ISNULL(TaskControlTypeDesc, '')) TaskControlTypeDesc, 
			ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
			from  Documents D
			left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
			left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
			where D.CustomerNo = @CustomerNo AND TC.TaskControlID = @OldTaskContolID
			order by Description
		END
	ELSE
		Begin
			Select DocumentsID,D.CustomerNo,[Description] Description, 
			IIF(ISNULL(TaskControlTypeDesc, '') LIKE '%Home Owners%', REPLACE(ISNULL(TaskControlTypeDesc, ''), 'Home Owners', 'Residential Property'), ISNULL(TaskControlTypeDesc, '')) TaskControlTypeDesc, 
			ISNULL(D.TaskControlID, '') TaskControlID,D.TaskControlTypeID
			from  Documents D
			left outer join TaskControl TC on TC.TaskControlID = D.TaskControlID
			left outer join TaskControlType TCT on TCT.TaskControlTypeID = TC.TaskControlTypeID
			where D.CustomerNo=@CustomerNo
			order by Description
		END
END
