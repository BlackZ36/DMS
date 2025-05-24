# 🎓 DMS - Diploma Management System

**DMS (Diploma Management System)** is a secure web application built with **.NET** and **React/ViteJS** for managing, issuing, and verifying academic diplomas. The system uses **blockchain-style hash chaining** to ensure diploma integrity and prevent tampering.

---

## 🚀 Tech Stack

- **Backend**: ASP.NET Core (.NET 6+)
- **Frontend**: React + ViteJS
- **Database**: Microsoft SQL Server
- **Authentication**: JWT-based with role management
- **Blockchain Integration**: Custom blockchain-style structure for diploma verification

---

## 🧩 Features

- 🔐 **Role-Based Access**:
  - `Student`: View owned diplomas
  - `Recruiter`: Verify diplomas by blockchain hash
  - `Staff`: Issue new diplomas
  - `Admin`: Manage users and system settings

- 📜 **Diploma Management**:
  - Issue, view, and manage diplomas
  - Each diploma is hashed and stored with tamper-evident hash chaining

- ⛓️ **Blockchain-Inspired Hashing**:
  - Diplomas are stored in a chain of hashes (block-style)
  - Each block contains hash, previous hash, timestamp, and nonce

- 👤 **User Accounts**:
  - Unique username & email
  - Avatar image support
  - JWT-based login and role access

---

## 🏗️ Database Schema

### `Users`
| Column       | Type             | Description                      |
|--------------|------------------|----------------------------------|
| Id           | INT              | Primary key                      |
| Username     | NVARCHAR(50)     | Unique username                  |
| PasswordHash | NVARCHAR(255)    | Hashed password                  |
| Email        | NVARCHAR(100)    | Unique email                     |
| Role         | NVARCHAR(20)     | Student / Recruiter / Staff / Admin |
| AvatarUrl    | NVARCHAR(255)    | Link to profile avatar           |
| CreatedAt    | DATETIME         | Created timestamp                |

### `Diplomas`
| Column          | Type            | Description                    |
|-----------------|-----------------|--------------------------------|
| Id              | INT             | Primary key                    |
| StudentId       | NVARCHAR(20)    | Student’s internal ID          |
| StudentName     | NVARCHAR(100)   | Full name                      |
| UniversityName  | NVARCHAR(200)   | Institution name               |
| Major           | NVARCHAR(100)   | Field of study                 |
| GraduationDate  | DATE            | Graduation date                |
| DiplomaNumber   | NVARCHAR(50)    | Unique diploma number          |
| BlockchainHash  | NVARCHAR(255)   | Hash used for verification     |
| CreatedAt       | DATETIME        | Issued date                    |
| Nonce           | INT             | Proof-of-work value            |

---

## ⚙️ Getting Started

### Prerequisites
- .NET 6 SDK or later
- Node.js + npm
- SQL Server (LocalDB or remote)
- Git

### Backend Setup
```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend Setup
```bash
cd frontend
npm install
npm run dev
```
