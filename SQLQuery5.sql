-- Delete all rows from the Items table
DELETE FROM Items;

-- Reset the identity column to start from 1
DBCC CHECKIDENT ('Items', RESEED, 0);
