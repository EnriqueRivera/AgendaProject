INSERT INTO EventInstruments
SELECT 
	EventId
	,InstrumentId
FROM Events
WHERE InstrumentId IS NOT NULL;