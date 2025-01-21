-- Check all roles
SELECT * FROM dbo.AspNetRoles;

-- Check if an "Admin" role exists
SELECT * FROM dbo.AspNetRoles WHERE Name = 'Admin';
-- Switch to the correct database
USE MovieRentalDb; -- Replace with your actual database name
GO

-- List all tables in the current database
SELECT * FROM INFORMATION_SCHEMA.TABLES;
-- Check if an "Admin" role exists
SELECT * FROM dbo.AspNetRoles WHERE Name = 'Admin';
-- Check if the user exists
SELECT * FROM dbo.AspNetUsers WHERE Email = 'Maja13@gmail.com';
SELECT * FROM dbo.AspNetRoles;
SELECT * FROM dbo.AspNetUsers;

