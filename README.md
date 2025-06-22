# Crystal Report Viewer for ASP.NET MVC 📊

![Crystal Reports](https://img.shields.io/badge/Crystal%20Reports-SP34-blue) ![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue) ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022-green) ![Oracle](https://img.shields.io/badge/Oracle-19c%2B-orange) ![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC%205-yellow)

Welcome to the **Crystal Report Viewer**, an **ASP.NET MVC web application** built in **Visual Studio 2022** using **.NET Framework 4.8**. This project integrates **SAP Crystal Reports SP34** with an **Oracle database** to generate and display dynamic user reports in a web browser. It uses the `GetReportData` stored procedure to fetch data from the `sec_user_profile` table, binds it to a Crystal Report, and renders it via a web-based `CrystalReportViewer`. Perfect for web-based reporting solutions! 🚀

---

## 📋 Table of Contents

- [About the Project](#about-the-project-)
- [Features](#features-)
- [Prerequisites](#prerequisites-)
- [Installation](#installation-)
  - [Step 1: Install Visual Studio 2022](#step-1-install-visual-studio-2022)
  - [Step 2: Install SAP Crystal Reports SP34](#step-2-install-sap-crystal-reports-sp34)
  - [Step 3: Set Up Oracle Database](#step-3-set-up-oracle-database)
  - [Step 4: Clone the Repository](#step-4-clone-the-repository)
  - [Step 5: Configure Project Dependencies](#step-5-configure-project-dependencies)
- [Oracle Stored Procedure](#oracle-stored-procedure-)
- [Dataset Creation](#dataset-creation-)
- [Report File Generation](#report-file-generation-)
- [Usage](#usage-)
- [Deployment](#deployment-)
- [Project Structure](#project-structure-)
- [Troubleshooting](#troubleshooting-)
- [Contributing](#contributing-)
- [License](#license-)
- [Acknowledgements](#acknowledgements-)
- [Contact](#contact-)

---

## 📖 About the Project

The **Crystal Report Viewer** is an ASP.NET MVC web application that fetches user data from an **Oracle database** using the `GetReportData` stored procedure, binds it to a **SAP Crystal Report** (`CustomerReport.rpt`), and displays it in a web view. The application uses a controller (`ReportController`) to handle report generation and a view to render the report in a browser, ideal for generating user profile reports or similar web-based summaries.

### Why Use This Project? 🤔
- Integrate **Oracle 19c+** with Crystal Reports in ASP.NET MVC 5.
- Use stored procedures for secure data retrieval.
- Deliver professional, browser-based reports with export capabilities.

---

## 🌟 Features

- **Oracle Database Integration** 🗄️: Fetches data via the `GetReportData` stored procedure.
- **Dynamic Dataset Creation** 📊: Populates a `DataTable` with user data (`user_id`, `user_nm`, `user_descrip`).
- **Crystal Reports Rendering** 📈: Uses `ReportDocument` to bind and display `CustomerReport.rpt`.
- **ASP.NET MVC Architecture** 🌐: Leverages controllers and views for report delivery.
- **Export Capabilities** 📄: Supports exporting reports to PDF, Excel, or other formats.

---

## 🛠️ Prerequisites

- **Windows 10/11** (64-bit, Windows 11 21H2+ recommended).
- **Visual Studio 2022** (v17.4+, Community/Professional/Enterprise).
  - Workloads: ASP.NET and web development, .NET Desktop Development (for Crystal Reports designer).
- **SAP Crystal Reports for Visual Studio SP34** (64-bit).
- **SAP Crystal Reports Runtime SP34** (64-bit, optional 32-bit for deployment).
- **.NET Framework 4.8** (included with Visual Studio 2022).
- **Oracle Database** (19c or higher, Express/Enterprise Edition).
- **Oracle.ManagedDataAccess** NuGet package (v21.9.0+).
- **Git** (for cloning the repository).
- **IIS Express** (included with Visual Studio) or **IIS** for hosting.

---

## ⚙️ Installation

Follow these steps to set up the project locally.

### Step 1: Install Visual Studio 2022
1. Download from [Microsoft’s official site](https://visualstudio.microsoft.com/downloads/).
2. Install with:
   - **ASP.NET and web development** workload.
   - **.NET Desktop Development** (for Crystal Reports designer).
   - **.NET Framework 4.8** under Individual Components.
3. Restart your system if prompted.

### Step 2: Install SAP Crystal Reports SP34
1. Download **Crystal Reports for Visual Studio SP34 (64-bit)** from the [SAP Download Portal](https://origin.softwaredownloads.sap.com/public/file/0020000000674372023) (requires SAP account).
2. Uninstall previous Crystal Reports versions.
3. Run the installer as administrator:
   - Select “Install Crystal Reports for Visual Studio.”
   - Complete the installation.
4. Install **Crystal Reports Runtime SP34 (64-bit)** for web hosting.
5. Verify:
   - In Visual Studio 2022, Add > New Item > Reporting, confirm “Crystal Report” template.

### Step 3: Set Up Oracle Database
1. Install **Oracle Database 19c+** or use an existing instance.
2. Create a user/schema (e.g., `REPORT_USER`):
   ```sql
   CREATE USER REPORT_USER IDENTIFIED BY password;
   GRANT CONNECT, RESOURCE, CREATE SESSION TO REPORT_USER;
   ```
3. Create the `rpt_user_table` table (for demo purposes):
   ```sql
   CREATE TABLE rpt_user_table (
       user_id NVARCHAR2(15) NOT NULL,
       user_nm NVARCHAR2(50) NOT NULL,
       user_descrip NVARCHAR2(100) NOT NULL
   );

   INSERT INTO rpt_user_table (user_id, user_nm, user_descrip) VALUES ('01', 'user_01', 'admin');
   INSERT INTO rpt_user_table (user_id, user_nm, user_descrip) VALUES ('02', 'user_02', 'User');
   INSERT INTO rpt_user_table (user_id, user_nm, user_descrip) VALUES ('03', 'user_03', 'Manager');
   COMMIT;
   ```
4. Note: The `GetReportData` procedure queries `sec_user_profile`. Ensure this table exists with the same schema (`user_id`, `user_nm`, `user_descrip`) or modify the procedure to use `rpt_user_table`:
   ```sql
   -- If using rpt_user_table, update procedure (see Oracle Stored Procedure section)
   ```
5. Configure TNS in `tnsnames.ora`:
   ```
   ORCL =
     (DESCRIPTION =
       (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))
       (CONNECT_DATA = (SERVICE_NAME = ORCL))
     )
   ```

### Step 4: Clone the Repository
1. Open a terminal:
   ```bash
   git clone https://github.com/reton2008/Crystal-Report-VS2022.git
   cd Crystal-Report-VS2022
   ```

### Step 5: Configure Project Dependencies
1. Open `Crystal-Report-VS2022.sln` in **Visual Studio 2022**.
2. Install NuGet packages:
   - Right-click project > Manage NuGet Packages.
   - Install `Oracle.ManagedDataAccess` (v21.9.0+).
   - Install `CrystalDecisions.CrystalReports.Engine` (v13.0.4000+, included with SP34).
3. Verify **.NET Framework 4.8**:
   - Right-click project > Properties > Set “Target framework” to .NET Framework 4.8.
4. Update `Web.config` with Oracle connection string:
   ```xml
   <connectionStrings>
     <add name="OracleConnection" connectionString="Data Source=ORCL;User Id=REPORT_USER;Password=password;" providerName="Oracle.ManagedDataAccess.Client" />
   </connectionStrings>
   ```
5. Build the solution (Ctrl+Shift+B).

---

## 🗄️ Oracle Stored Procedure

The application uses the `GetReportData` stored procedure to fetch user data.

1. **Create the Stored Procedure**:
   - Connect to Oracle (e.g., via SQL Developer).
   - Execute:
     ```sql
     CREATE OR REPLACE PROCEDURE GetReportData(p_refCursor OUT SYS_REFCURSOR) AS
     BEGIN
         OPEN p_refCursor FOR
             SELECT user_id, user_nm, user_descrip
             FROM sec_user_profile;
     END GetReportData;
     /
     ```
2. **Grant Execute Permission**:
   ```sql
   GRANT EXECUTE ON GetReportData TO REPORT_USER;
   ```
3. **Note on Table Discrepancy**:
   - The procedure queries `sec_user_profile`, but the provided table is `rpt_user_table`. If `sec_user_profile` doesn’t exist, modify the procedure to use `rpt_user_table`:
     ```sql
     CREATE OR REPLACE PROCEDURE GetReportData(p_refCursor OUT SYS_REFCURSOR) AS
     BEGIN
         OPEN p_refCursor FOR
             SELECT user_id, user_nm, user_descrip
             FROM rpt_user_table;
     END GetReportData;
     /
     ```
   - Ensure the table used matches the report schema.

---

## 📊 Dataset Creation

The application fetches data from Oracle using `GetReportData` and populates a `DataTable` in `ReportController`.

1. **Code in `ReportController.cs`**:
   ```csharp
   using Oracle.ManagedDataAccess.Client;
   using CrystalDecisions.CrystalReports.Engine;
   using System.Data;
   using System.Web.Mvc;

   namespace Crystal_Report_VS2022.Controllers
   {
       public class ReportController : Controller
       {
           public ActionResult ViewReport()
           {
               string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
               DataTable dt = new DataTable("UserProfile");

               try
               {
                   using (OracleConnection conn = new OracleConnection(connectionString))
                   {
                       conn.Open();
                       using (OracleCommand cmd = new OracleCommand("GetReportData", conn))
                       {
                           cmd.CommandType = CommandType.StoredProcedure;
                           cmd.Parameters.Add("p_refCursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                           using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                           {
                               adapter.Fill(dt);
                           }
                       }
                   }

                   DataSet ds = new DataSet();
                   ds.Tables.Add(dt);

                   ReportDocument report = new ReportDocument();
                   report.Load(Server.MapPath("~/Reports/CustomerReport.rpt"));
                   report.SetDataSource(ds);

                   ViewBag.ReportSource = report;
                   return View();
               }
               catch (Exception ex)
               {
                   ViewBag.Error = $"Error: {ex.Message}";
                   return View("Error");
               }
           }
       }
   }
   ```

2. **View Code (`ViewReport.cshtml`)**:
   ```cshtml
   @using CrystalDecisions.CrystalReports.Engine
   @model dynamic

   @{
       ViewBag.Title = "User Report";
   }

   <h2>User Report</h2>
   @if (ViewBag.Error != null)
   {
       <div class="alert alert-danger">@ViewBag.Error</div>
   }
   else
   {
       <div>
           @Html.Raw(CrystalDecisions.CrystalReports.Engine.ReportDocument.GetHtmlContent(ViewBag.ReportSource as ReportDocument))
       </div>
   }
   ```

3. **Configuration**:
   - Update `Web.config` connection string.
   - Ensure `Reports/CustomerReport.rpt` exists.

---

## 📄 Report File Generation

The project uses `CustomerReport.rpt` to display user data (`user_id`, `user_nm`, `user_descrip`).

1. **Open the Report**:
   - Double-click `CustomerReport.rpt` in Solution Explorer to open in **Crystal Reports Designer**.

2. **Report Schema**:
   - Bound to the `UserProfile` DataTable with fields: `user_id`, `user_nm`, `user_descrip`.
   - Includes header, details section, and footer.

3. **Creating a New Report**:
   - Right-click project > Add > New Item > Reporting > Crystal Report.
   - Name it (e.g., `NewReport.rpt`).
   - In Crystal Reports Wizard:
     - Select “Using the Report Wizard” > “Standard Report.”
     - Choose “ADO.NET (XML)” and export dataset:
       ```csharp
       ds.WriteXml(Server.MapPath("~/dataset.xml"));
       ```
     - Design layout (drag fields, add headers).
   - Update `ReportController.cs`:
     ```csharp
     report.Load(Server.MapPath("~/Reports/NewReport.rpt"));
     ```

4. **Updating the Report**:
   - If dataset changes, refresh schema:
     - Open `CustomerReport.rpt` > Database > Database Expert > Refresh.
   - Save and rebuild.

---

## 🚀 Usage

1. **Run the Application**:
   - Set `Crystal-Report-VS2022` as startup project.
   - Press **F5** to run with IIS Express.
   - Navigate to `/Report/ViewReport` (e.g., `http://localhost:port/Report/ViewReport`).

2. **View the Report**:
   - The report displays in the browser with navigation, zoom, and export options.

3. **Export the Report**:
   - Add export action in `ReportController.cs`:
     ```csharp
     public ActionResult ExportReport()
     {
         // Same data fetching logic as ViewReport
         // ...
         Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
         return File(stream, "application/pdf", "UserReport.pdf");
     }
     ```

---

## 🌍 Deployment

1. **Publish to IIS**:
   - Right-click project > Publish > Web Server (IIS).
   - Choose folder or server, then publish.
2. **Install Crystal Reports Runtime**:
   - Install **Crystal Reports Runtime SP34 (64-bit)** on the server.
3. **Configure Oracle**:
   - Ensure server connects to Oracle (TNS or direct connection string).
   - Install `Oracle.ManagedDataAccess` or Oracle Client.
4. **Update Web.config**:
   - Set production database connection string.
5. **Set Permissions**:
   - Grant IIS application pool access to `Reports` folder.

---

## 📂 Project Structure

```
Crystal-Report-VS2022/
├── Crystal-Report-VS2022.sln       # Solution file
├── Crystal-Report-VS2022/
│   ├── Controllers/
│   │   ├── ReportController.cs     # Handles report generation
│   ├── Views/
│   │   ├── Report/
│   │   │   ├── ViewReport.cshtml   # Displays report
│   ├── Reports/
│   │   ├── CustomerReport.rpt      # Crystal Report file
│   ├── Web.config                  # Configuration
│   ├── Packages/                   # NuGet packages
├── Scripts/
│   ├── CreateRptUserTable.sql      # Creates rpt_user_table
│   ├── GetReportData.sql           # Stored procedure
├── Images/                         # Screenshots (add your own)
├── README.md                       # Project documentation
```

---

## 🛠️ Troubleshooting

- **Crystal Report Viewer Not Loading** ⚠️:
  - Ensure **Crystal Reports Runtime SP34 (64-bit)** is installed.
  - Verify `CrystalDecisions.*` assemblies in `bin`.
- **Designer Fails to Load** 🚫:
  - Reinstall **Crystal Reports SP34 64-bit** as administrator.
  - Check Windows 11 compatibility ([SAP KBA 3204578](https://userapps.support.sap.com/sap/support/knowledge/en/3204578)).
- **Oracle Connection Errors** 🔌:
  - Validate `Web.config` connection string.
  - Ensure `Oracle.ManagedDataAccess` is installed.
  - Check TNS or network access.
- **Stored Procedure Errors** 🚨:
  - Verify `GetReportData` exists and has permissions.
  - Ensure `sec_user_profile` exists or update to `rpt_user_table`.
- **HTTP Errors** 🌐:
  - Check IIS configuration (.NET CLR v4.0, Integrated pipeline).
- **Build Errors** 🛑:
  - Confirm **.NET Framework 4.8** is installed.
  - Clean and rebuild: Build > Clean Solution, then Ctrl+Shift+B.

---

## 🤝 Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/new-feature`.
3. Commit: `git commit -m "Add new feature"`.
4. Push: `git push origin feature/new-feature`.
5. Open a pull request.

See [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md).

---

## 📜 License

Licensed under the **MIT License** - see [LICENSE](LICENSE).

---

## 🙌 Acknowledgements

- [SAP Crystal Reports](https://www.sap.com/products/technology-platform/crystal-reports.html)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [Oracle Database](https://www.oracle.com/database/)
- [TekTutorialsHub](https://www.tektutorialshub.com/crystal-reports/crystal-reports-for-visual-studio-download/)
- [Stack Overflow](https://stackoverflow.com/) and [SAP Community](https://community.sap.com/)

---

## 📬 Contact

- **GitHub**: [reton2008](https://github.com/reton2008)
- **Email**: reton2008@gmail.com (replace with your email)

File issues or feature requests on [GitHub Issues](https://github.com/reton2008/Crystal-Report-VS2022/issues).

---

![GitHub Stars](https://img.shields.io/github/stars/reton2008/Crystal-Report-VS2022?style=social) ![GitHub Forks](https://img.shields.io/github/forks/reton2008/Crystal-Report-VS2022?style=social)
