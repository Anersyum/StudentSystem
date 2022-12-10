USE [studentTest]
GO
/****** Object:  Trigger [dbo].[update_academic_average_for_group_after_student_update]    Script Date: 08/12/2022 17:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[update_academic_average_for_group_after_student_update]
	ON [dbo].[Students] 
	AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @oldGroupId INT;
	DECLARE @newGroupId INT;
	DECLARE @oldAverage REAL;
	DECLARE @newAverage REAL;

	SELECT TOP(1) @oldGroupId = GroupId FROM deleted;
	SELECT TOP(1) @newGroupId= GroupId FROM inserted;
	SELECT @oldAverage = ROUND(AVG(s.AcademicPerformance), 2) FROM Students s WHERE GroupId = @oldGroupId;
	SELECT @newAverage = ROUND(AVG(s.AcademicPerformance), 2) FROM Students s WHERE GroupId = @newGroupId;

	UPDATE Groups 
	SET AcademicAverage = IIF(@oldAverage IS NOT NULL, @oldAverage, 0)
	WHERE Id = @oldGroupId;

	UPDATE Groups 
	SET AcademicAverage = IIF(@newAverage IS NOT NULL, @newAverage, 0)
	WHERE Id = @newGroupId;
END