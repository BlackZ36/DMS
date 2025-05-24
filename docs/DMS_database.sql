-- Đóng tất cả kết nối đến DB nếu đang tồn tại
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'DMSDatabase')
BEGIN
    ALTER DATABASE DMSDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DMSDatabase;
END
GO

-- Tạo lại database
CREATE DATABASE DMSDatabase;
GO

-- Sử dụng database vừa tạo
USE DMSDatabase;
GO

-- Tạo bảng Users
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Student', 'Recruiter', 'Staff', 'Admin')) DEFAULT 'Student',
    AvatarUrl NVARCHAR(255) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Tạo bảng Diplomas
CREATE TABLE Diplomas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId NVARCHAR(20) NOT NULL,
    StudentName NVARCHAR(100) NOT NULL,
    UniversityName NVARCHAR(200) NOT NULL,
    Major NVARCHAR(100) NOT NULL,
    GraduationDate DATE NOT NULL,
    DiplomaNumber NVARCHAR(50) NOT NULL UNIQUE,
    BlockchainHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
);
GO

-- Tạo bảng Blockchain
CREATE TABLE Blockchain (
    BlockIndex INT NOT NULL,
    BlockHash NVARCHAR(255) NOT NULL PRIMARY KEY,
    PreviousHash NVARCHAR(255) NOT NULL,
    Timestamp DATETIME NOT NULL,
    DataHash NVARCHAR(255) NOT NULL,
    Nonce INT NOT NULL
);
GO

-- Thêm 3 học sinh
INSERT INTO Users (Username, PasswordHash, Email, Role, AvatarUrl)
VALUES 
('student1', 'hashed_password_1', 'student1@example.com', 'Student', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740'),
('student2', 'hashed_password_2', 'student2@example.com', 'Student', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740'),
('student3', 'hashed_password_3', 'student3@example.com', 'Student', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740');

-- Thêm 1 nhà tuyển dụng
INSERT INTO Users (Username, PasswordHash, Email, Role, AvatarUrl)
VALUES 
('recruiter1', 'hashed_password_4', 'recruiter1@example.com', 'Recruiter', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740');

-- Thêm 1 nhân viên
INSERT INTO Users (Username, PasswordHash, Email, Role, AvatarUrl)
VALUES 
('staff1', 'hashed_password_5', 'staff1@example.com', 'Staff', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740');

-- Thêm 1 quản trị viên
INSERT INTO Users (Username, PasswordHash, Email, Role, AvatarUrl)
VALUES 
('admin1', 'hashed_password_6', 'admin1@example.com', 'Admin', 'https://img.freepik.com/free-psd/3d-rendering-teenager-boy-white-t-shirt_1142-53060.jpg?ga=GA1.1.1510998588.1731092744&semt=ais_hybrid&w=740');
