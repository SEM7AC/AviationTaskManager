# AviationTaskManager
A desktop app for tracking supplemental maintenance tasks after major inspections in aviation.

### Overview
The **Aviation Task Manager** is a desktop application designed to streamline supplemental task tracking in aviation workflows. It allows admins and mechanics to manage customizable checklists for tasks not covered by standard maintenance manuals. The app ensures operational efficiency, accountability, and traceability for every task completed.

---

### Features
#### Core Features:
- **User Roles**:
  - Admin: Create, edit, and manage task groups based on SOPs.
  - Mechanic: View assigned tasks, mark steps as complete or "N/A," and add supplemental steps.
- **Custom Task Groups**:
  - Organize tasks under categories such as “Pre-Departure” or “Post-Flight Cleanup.”
  - Assign task groups to specific aircraft.
- **Completion Tracking**:
  - Requires a PIN sign-off for completed tasks.
  - Logs user name and timestamp for traceability.
  - Final sign-off to close a TaskList after all steps are resolved.

#### UI/UX Enhancements:
- Progress indicators for task groups (color-coded completion statuses).
- Optional comment fields for annotations.
- Easy-to-navigate dashboard to manage tasks tied to aircraft tail numbers.

#### Data Management:
- Built on SQLite for robust, embedded data storage.
- Supports multiple users with secure logins and shared
---

### Planned Features
- **Export to PDF**:
  - Generate detailed reports of completed task groups for audits and records.
- **Reminders and Notifications**:
  - Alert users about pending tasks based on deadlines.
- **Template Library**:
  - Preloaded task group templates for common workflows.

---

###Technology Stack
- **Frontend**: WPF (Windows Presentation Foundation) for a clean, modern user interface.
- **Backend**: C# with SQLite for data persistence.
- **Libraries**:
  - `Microsoft.Data.Sqlite` for database integration.
  - `System.Text.Json` (or similar) for future data exports.

---

### Getting Started
#### Prerequisites:
- Windows OS with .NET runtime installed.
- Visual Studio (latest version recommended) with .NET desktop development workload.

#### Setup Instructions:
1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/SEM7AC/AviationTaskManager.git
2. Open the solution file in Visual Studio:
   Open the `AviationTaskManager.sln` file in Visual Studio.
3. Restore NuGet packages:
   Navigate to Tools > NuGet Package Manager > Manage NuGet Packages for Solution and install any missing packages.
4. Build the project:
   Press Ctrl+Shift+B or go to Build > Build Solution to build the project.
5. Run the application:
   Press F5 or click the Start button in Visual Studio to launch the app.

#### Contributing:

I welcome contributions to improve the Aviation Task Manager! If you’d like to help:

1. Fork the repository:
   ```bash
   git fork https://github.com/SEM7AC/AviationTaskManager.git

2. Create a new feature branch:
   ```bash
   git checkout -b feature-name

3. Submit a pull request for review.

#### License

This project is licensed under the MIT License.
   
