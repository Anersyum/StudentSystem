USE [studentTest]
GO
/****** Object:  Trigger [dbo].[validate_group_before_update]    Script Date: 08/12/2022 17:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[validate_group_before_update]
ON [dbo].[Students] AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @student INT;
	DECLARE @oldGroup INT;
	DECLARE @newGroup INT;
	DECLARE @oldDepartment INT;
	DECLARE @newDepartment INT;

	SELECT TOP(1) @student = Id FROM inserted;
	SELECT TOP(1) @oldGroup = GroupId FROM deleted;
	SELECT TOP(1) @newGroup = GroupId FROM inserted;
	SELECT TOP(1) @oldDepartment = Groups.DepartmentId FROM Groups WHERE Id = @oldGroup;
	SELECT TOP(1) @newDepartment = Groups.DepartmentId FROM Groups WHERE Id = @newGroup;

	IF (@oldDepartment !=  @newDepartment)
		BEGIN
			UPDATE Students SET GroupId = @oldGroup WHERE Id = @student;
			THROW 50020, 'Group must be in the same department', 1;
		END
END;