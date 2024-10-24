Library Management System
This project is a Library Management System (LMS) built using ASP.NET Core with Entity Framework Core and PostgreSQL for the backend. The frontend is created using Razor Pages.

Features:
Add, Edit, Delete Books.
List all books.
Issue a book to a user.
Return a book.
Search for books.
Track the number of books issued.
Display available copies of books.
Technologies Used:
Backend: ASP.NET Core 6.0, C#, Entity Framework Core, PostgreSQL
Frontend: Razor Pages (HTML, CSS)
Database: PostgreSQL
ORM: Entity Framework Core



Steps to Set Up the Project
1. Prerequisites
Make sure you have the following installed:

.NET 6 SDK
PostgreSQL
Visual Studio 2022 or Visual Studio Code
Git (if cloning from a repository)


2. Clone the Repository
To clone the project from GitHub:

git clone https://github.com/Vijaykr856/LMS.git


3. Database Setup (PostgreSQL)
Create a PostgreSQL Database: Open pgAdmin or use the psql command-line interface to create a database:



CREATE DATABASE LMS;
Configure Database in the Project:

Open the project in your editor (e.g., Visual Studio).
Navigate to the appsettings.json file.
Replace the connection string with your PostgreSQL database details:


{
  "ConnectionStrings": {
    "LibraryDb": "Host=localhost;Database=LMS;Username=your_username;Password=your_password"
  }
}


4. Install Dependencies
Run the following command to restore the required NuGet packages:


dotnet restore


5. Run Migrations to Set Up the Database Schema
To create the tables in your PostgreSQL database, run the following command:


dotnet ef database update


This will apply the migrations and create the necessary database schema (tables for Books, IssuedBooks, etc.).

6. Run the Application
Once the database is set up, you can run the project:


dotnet run or Use Run Button From Visual Studio


Open your browser and navigate to https://localhost:5001 or https://localhost:5000 (depending on your default ports).
Flow of the System
Step 1: Add a Book
Navigate to the Books page and click on "Add New Book."
Fill in the details like title, author, genre, total copies, and available copies, then click "Add Book."
The book will be saved to the database.


Step 2: List All Books/ Home
Go to the Books page to see a list of all books in the system.
You can edit or delete books from this page.


Step 3: Issue a Book
Navigate to the Issue Book page.
Select a book from the dropdown of available books and enter the person to whom the book is being issued.
After issuing, the available copies of the book will be updated.


Step 4: View Issued Books
Go to the Issued Books page to view all currently issued books.
You can return a book from this list by clicking the "Return" button.


Step 5: Return a Book
Click the "Return Book" button for the book you want to return.
The system will mark the book as returned, and the available copies of the book will be updated.




Project Structure


LibraryManagementSystem/
│
├── Controllers/          # Backend logic (e.g., BooksController, IssuedBooksController)
├── Models/               # Database models (e.g., Book, IssuedBook)
├── Pages/                # Razor Pages for frontend (e.g., Index.cshtml, Issue.cshtml)
├── Migrations/           # Entity Framework Core migrations
├── appsettings.json      # Configuration file (database connection, etc.)
├── Program.cs            # Entry point of the application

Flow Steps of the System
Add/Edit/Delete Books: These operations involve interacting with the Books table in the database, where users can maintain the library's catalog.

Issue a Book:

The IssuedBooks table tracks books issued to users.
Only books with available copies can be issued, and after issuing, the available count is decreased.
Return a Book:

When a book is returned, the system updates the ReturnDate in the IssuedBooks table and increases the available copies in the Books table.
List of Issued Books: Shows all books currently issued, providing the ability to mark them as returned.