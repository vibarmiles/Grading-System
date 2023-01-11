WITH TableGrades AS (SELECT G.[StudentID], Student.[Name], Sub.[SubjectName], G.[Quarter], G.[Grade] 
FROM Grades G 
INNER JOIN StudentsView Student 
ON Student.[StudentID]=G.[StudentID] 
INNER JOIN Subjects Sub 
ON Sub.[SubjectID]=G.[SubjectID])

SELECT * FROM TableGrades
PIVOT (SUM(Grade) FOR Quarter IN ([1], [2], [3], [4])) AS PivotTable