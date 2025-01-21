-- Check all roles
SELECT * FROM dbo.AspNetRoles;

-- Check if an "Admin" role exists
SELECT * FROM dbo.AspNetRoles WHERE Name = 'Admin';
-- List all tables in the current database
SELECT * FROM INFORMATION_SCHEMA.TABLES;
