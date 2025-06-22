//using AccessData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace CrystalReportTutorial
{
    public partial class ReportPage : System.Web.UI.Page
    {
        //user_information_class user_information_class_param = new user_information_class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            // Oracle connection and stored procedure call
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(conString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetReportData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Output cursor parameter
                    cmd.Parameters.Add("p_refCursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // Load Crystal Report
                        ReportDocument crystalReport = new ReportDocument();
                        crystalReport.Load(Server.MapPath("~/EmployeeReport.rpt"));
                        crystalReport.SetDataSource(ds.Tables[0]);

                        // Display the report
                        CrystalReportViewer1.ReportSource = crystalReport;

                        // Optional: Export report for download
                        ExportReport(crystalReport);
                    }
                }
            }
        }
        private void ExportReport(ReportDocument report)
        {
            // Configure export options for PDF download
            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Report");
        }
        
    }
}