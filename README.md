# Contact Manager Application

## 📋 Project Overview

A comprehensive Contact Management System built with modern .NET technologies that allows users to manage contact information through a web interface with full CRUD operations, CSV import capabilities, and real-time data manipulation.

## 🚀 Features

### Core Functionality
- **Contact Management**: Full CRUD operations for contact records
- **CSV Import**: Bulk import contacts from CSV files
- **Real-time Editing**: Inline editing with AJAX for seamless user experience
- **Advanced Search**: Client-side filtering across all contact fields
- **Smart Sorting**: Multi-column sorting with data type awareness
- **Data Validation**: Comprehensive client and server-side validation
- **Error Handling**: Global exception handling with user-friendly messages

### Technical Features
- **Responsive UI**: Bootstrap-based responsive design
- **AJAX Operations**: Asynchronous data operations without page reloads
- **Inline Editing**: Edit contacts directly in the table view
- **Bulk Operations**: Efficient handling of multiple records
- **Export Ready**: Structured data for potential export functionality

## 🛠 Technology Stack

### Backend
- **ASP.NET MVC**: Web application framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Database management system
- **AutoMapper**: Object-object mapping
- **FluentValidation**: Validate data
- **C#**: Primary programming language

### Frontend
- **JavaScript/jQuery**: Client-side interactivity
- **Bootstrap 5**: UI framework and styling
- **AJAX**: Asynchronous HTTP requests
- **HTML5/CSS3**: Markup and styling

### Architecture
- **N-Layer Architecture**: Separation of concerns
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling



## 🗃 Data Model

### Contact Entity
- **Id**: Unique identifier (Primary Key)
- **Name**: Contact full name (Required)
- **DateOfBirth**: Birth date with validation
- **Married**: Boolean marital status
- **Phone**: Contact phone number
- **Salary**: Decimal salary information

## 🔧 Key Components

### CSV Import System
- Supports standard CSV format
- Automatic data mapping and validation
- Batch processing for performance
- Error handling for malformed data

### Inline Editing Interface
- Click-to-edit functionality
- Real-time validation
- Save/Cancel operations
- No page reload required

### Search & Sort Engine
- Multi-field search capability
- Type-aware sorting (text, numbers, dates)
- Client-side processing for performance
- Persistent UI state

### Error Handling
- Global exception middleware
- Structured error responses
- User-friendly error messages
- Development vs Production modes

## 🎯 Usage Examples

### Adding Contacts
1. Manual entry via web form
2. Bulk import via CSV upload
3. Inline creation in data table

### Editing Contacts
1. Click "Edit" button on any contact row
2. Modify data directly in the table
3. Save changes or cancel editing

### Managing Data
- Search contacts using the search box
- Sort by clicking column headers
- Delete individual contacts
- Import large datasets via CSV

## 📊 CSV Format

Expected CSV structure:
```csv
Name,DateOfBirth,Married,Phone,Salary
John Doe,1990-01-15,True,555-1234,50000.00
Jane Smith,1985-06-20,False,555-5678,60000.00
