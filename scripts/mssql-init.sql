-- MSSQL Initialization Script for maERP
-- This script creates the database and user for maERP

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'maerp_01')
BEGIN
    CREATE DATABASE maerp_01;
END
GO

-- Create login if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'maerp')
BEGIN
    CREATE LOGIN maerp WITH PASSWORD = 'maerp123!', CHECK_POLICY = OFF;
END
GO

-- Use the maerp database
USE maerp_01;
GO

-- Create user if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'maerp')
BEGIN
    CREATE USER maerp FOR LOGIN maerp;
END
GO

-- Grant necessary permissions
ALTER ROLE db_owner ADD MEMBER maerp;
GO

PRINT 'maERP database and user setup completed successfully.';
GO