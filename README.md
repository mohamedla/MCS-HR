## MCS HR App

**Version:** .NET 8.0

**Type:** ASP.NET MVC Application

### Getting Started

1. **Download or Clone:**

   - Download the project folder or clone it using Git.

2. **Database Connection:**

   - Open `appsettings.json` and modify the `MCSHR` connection string to your target Microsoft SQL Server database.

3. **Run the Project:**

   - Open the solution in Visual Studio or your preferred IDE.
   - Run the project.

4. **Alternative Deployment:**

   - Navigate to `bin/release/.net8.0`.
   - Use the content directly in IIS and access the application.

### Features

* **Employee List Screen:**

   - Displays a list of all employees.
   - Provides buttons for adding new employees and uploading a tab-delimited employee data file.
   - Shows employee details with edit and delete buttons.
   - Filters data by employment type and overtime hour rate.
   - Calculates and displays overtime hours based on employee type.

* **Add, Edit, Delete Forms:**

   - Currently accessed through the employee list screen.
   - Planned improvement: Implement popup screens for better user experience.

### Planned Improvements

* **Pagination:**

   - Implement pagination for the employee list screen to handle large datasets.

* **Search:**

   - Add search functionality for other employee data fields.

* **Popup Forms:**

   - Enhance user experience by moving add, edit, and delete forms to popup screens.

* **Overtime Rate Setup:**

   - Create a dedicated setup screen to modify overtime hour rate calculation formulas.

### Contribution

* This document will be updated with further changes and improvements.
* Feel free to share your ideas for enhancing the application in the comments.
