BEGIN TRANSACTION;

INSERT INTO InstrumentTreatments
SELECT 
	InstrumentId 
	,TreatmentId
FROM Instruments
WHERE TreatmentId IS NOT NULL;

ALTER TABLE Instruments
DROP CONSTRAINT FK_Instruments_TreatmentId;

ALTER TABLE Instruments
DROP COLUMN TreatmentId;

COMMIT TRANSACTION;