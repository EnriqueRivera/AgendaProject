ALTER TABLE Events
DROP CONSTRAINT PK_Events_InstrumentId;

ALTER TABLE Events
DROP COLUMN InstrumentId;