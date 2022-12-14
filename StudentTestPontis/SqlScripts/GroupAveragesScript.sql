USE [studentTest]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW GroupsWithAverages
AS
SELECT [g].[Id], [g].[Name], [g].[DepartmentId], 
ROUND(AVG(CAST([s].[AcademicPerformance] as float)), 2) [AcademicAverage],
COUNT([s].[Id]) [StudentCount]
FROM [Groups] [g]
INNER JOIN [Students] [s] ON [s].[GroupId] = [g].[Id]
GROUP BY [g].[Name], [g].[Id], [g].[DepartmentId];