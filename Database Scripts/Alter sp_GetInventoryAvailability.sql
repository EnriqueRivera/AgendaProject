ALTER PROCEDURE [dbo].[sp_GetInventoryAvailability]
	@drawerId AS INT,
	@year AS INT,
	@month AS INT
AS
BEGIN

SELECT 
	i.InstrumentId
	,i.Name AS InstrumentName
	,i.Quantity
	--,dt.year_month
	,i.TreatmentId
	,CASE WHEN i.TreatmentId IS NULL THEN '' ELSE t.Name END AS TreatmentName
	,CASE WHEN i.TreatmentId IS NULL THEN '' ELSE i.UsesLeft END AS UsesLeft
	,i.MaxUses
	,ic.InstrumentCommentId
	--,ic.CommentDate
	,CASE WHEN ic.InstrumentCommentId IS NULL THEN '' ELSE ic.Comment END AS Comment
	,CAST(CASE WHEN ic.InstrumentCommentId IS NULL THEN '0' ELSE '1' END AS bit) AS IsChecked
FROM 
	Instruments i 
	CROSS JOIN (
		SELECT CAST(CONCAT(@year, '-', @month,'-1') AS DATE) AS year_month
	) dt
	LEFT JOIN Treatments t ON i.TreatmentId = t.TreatmentId
	LEFT JOIN InstrumentComments ic ON i.InstrumentId = ic.InstrumentId
										AND year(dt.year_month) = year(ic.CommentDate) 
										AND month(dt.year_month) = month(ic.CommentDate)
WHERE 
	i.DrawerId = @drawerId
	AND i.IsDeleted = 0
ORDER BY InstrumentName;

END