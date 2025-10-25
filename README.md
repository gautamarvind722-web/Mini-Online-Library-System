# Mini-Online-Library-System
Mini Online Library System
A simple web‑based library management application.  
It allows users to browse, lend, return books, and allows administrators to manage book inventory and user records.

## Table of Contents
- [About](#about)  
- [Features](#features)  
- [Tech Stack](#tech-stack)  
- [Architecture / Folder Structure](#architecture--folder-structure)  
- [Installation](#installation)  
- [Usage](#usage)  
- [Configuration](#configuration)  
- [Contributing](#contributing)  
- [License](#license)  
- [Contact](#contact)

## About
This project aims to provide a **mini online library system** with a frontend built in Angular, backend in C# (.NET), and a SQL‑based database. The goal is to support basic library operations such as adding books, updating inventory, recording loans/returns, and letting users view available books.

## Features
- User view of available books  
- Administrator functions: add • edit • delete books  
- Loan/return tracking  
- Book inventory count  
- Database script included for initial setup  
- Simple, clean UI  

## Tech Stack
- **Frontend**: Angular (TypeScript, HTML, CSS)  
- **Backend**: C# (ASP.NET Core Web API)  
- **Database**: SQL (script provided)  
- **Repository Languages**: C# (~52%) · TypeScript (~34%) · HTML (~14%) ([github.com](https://github.com/gautamarvind722-web/Mini-Online-Library-System))  

## Architecture / Folder Structure
```
/
├── Backend/
│   └── Mini‑Backend/     ← ASP.NET Core Web API project
├── FrontEnd/
│   └── MiniAngularApplication/ ← Angular project
├── DBScript.txt           ← SQL script for database & tables
└── README.md              ← This file
```

## Installation
1. Clone the repository:  
   ```bash
   git clone https://github.com/gautamarvind722-web/Mini-Online-Library-System.git
   cd Mini-Online-Library-System
   ```

2. Database setup:  
   - Run the SQL script `DBScript.txt` in your SQL Server / database engine of choice to create the required tables and seed data.

3. Backend setup:  
   - Navigate to `Backend/Mini‑Backend/`  
   - Open the `.sln` in Visual Studio (or your preferred IDE)  
   - Update the connection string in `appsettings.json` to point to your database  
   - Build and run the Web API (`dotnet run` or via IDE)

4. Frontend setup:  
   - Navigate to `FrontEnd/MiniAngularApplication/`  
   - Install dependencies:  
     ```bash
     npm install
     ```  
   - Update API base URL (if needed) in environment file  
   - Run the Angular app:  
     ```bash
     ng serve
     ```  
   - Open browser at `http://localhost:4200` (or whichever port)

## Usage
- Open the Angular application in your browser.  
- As Admin: Login (if auth implemented) → Add/Edit/Delete books → View inventory.  
- As User: Browse books → Request loan → Return book.  
- Check the backend API endpoints to verify/extend functionality (e.g., `/api/books`, `/api/loans`).

## Configuration
- **Backend**: `Backend/Mini‑Backend/appsettings.json` – define your database connection string, logging levels, etc.  
- **Frontend**: `FrontEnd/MiniAngularApplication/src/environments/environment.ts` – update `apiUrl` to match your backend hosting address.

## Contributing
Contributions, issues and feature requests are welcome! Feel free to fork the project and submit pull requests.  
Please follow standard GitHub workflow: fork → branch → commit → PR.

## License
This project is licensed under the [MIT License](LICENSE) (if you choose MIT or change accordingly).  

## Contact
Created by **Gautam Arvind** – feel free to reach out if you have any questions or feedback.
