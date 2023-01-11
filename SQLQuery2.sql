SELECT StudentID, SubjectID, [1], [2], [3], [4] 
FROM (SELECT StudentID, SubjectID, Quarter, Grade FROM Grades) AS SourceTable
PIVOT (SUM(Grade) FOR Quarter IN ([1], [2], [3], [4])) AS PivotTable