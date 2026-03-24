-- Create target database and schema
IF DB_ID('ClarionAIProdoc') IS NULL
BEGIN
  CREATE DATABASE ClarionAIProdoc;
END
GO
USE ClarionAIProdoc;
GO
IF SCHEMA_ID('clarion') IS NULL
BEGIN
  EXEC('CREATE SCHEMA clarion');
END
GO
