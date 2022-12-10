USE [studentTest]
GO
/****** Object:  Trigger [dbo].[update_academic_average_for_group_after_student_delete]    Script Date: 08/12/2022 17:36:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[update_academic_average_for_group_after_student_delete]
ON [dbo].[Students] AFTER DELETE 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @groupId INT;
	DECLARE @average REAL;

	SELECT TOP(1) @groupId = GroupId FROM deleted;
	SELECT @average = ROUND(AVG(s.AcademicPerformance), 2) FROM Students s WHERE GroupId = @groupId;

	UPDATE Groups 
	SET AcademicAverage = IIF(@average IS NOT NULL, @average, 0)
	WHERE Id = @groupId
END