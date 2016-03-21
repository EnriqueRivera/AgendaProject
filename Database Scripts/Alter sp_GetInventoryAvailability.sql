ALTER PROCEDURE [dbo].[sp_GetInventoryAvailability]
	@drawerId AS INT,
	@year AS INT,
	@month AS INT
AS
BEGIN

;WITH InstrumentsAndTreatments AS (
	SELECT 
		i.InstrumentId
		,i.Name
		,i.Quantity
		,i.UsesLeft
		,i.MaxUses
		,COUNT(it.InstrumentId) AS UsedOn
	FROM 
		Instruments i
		LEFT JOIN InstrumentTreatments it ON i.InstrumentId = it.InstrumentId
	WHERE 
		i.DrawerId = @drawerId
		AND i.IsDeleted = 0
	GROUP BY 
		i.InstrumentId
		,i.Name
		,i.Quantity
		,i.UsesLeft
		,i.MaxUses
)

SELECT 
	i.InstrumentId
	,i.Name AS InstrumentName
	,i.Quantity
	,CASE WHEN i.UsedOn = 0 THEN '' ELSE i.UsesLeft END AS UsesLeft
	,i.UsedOn
	,i.MaxUses
	,ic.InstrumentCommentId
	,CASE WHEN ic.InstrumentCommentId IS NULL THEN '' ELSE ic.Comment END AS Comment
	,CAST(CASE WHEN ic.InstrumentCommentId IS NULL THEN '0' ELSE '1' END AS bit) AS IsChecked
FROM 
	InstrumentsAndTreatments i 
	CROSS JOIN (
		SELECT CAST(CONCAT(@year, '-', @month,'-1') AS DATE) AS year_month
	) dt
	LEFT JOIN InstrumentComments ic ON i.InstrumentId = ic.InstrumentId
										AND year(dt.year_month) = year(ic.CommentDate) 
										AND month(dt.year_month) = month(ic.CommentDate)
ORDER BY InstrumentName;

END