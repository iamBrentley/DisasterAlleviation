using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using DAF_WEBAPP_V1._0._0.Pages.DONATIONS_MONEY;

namespace DAF_WEBAPP_V1._0._0.Pages.DISASTER
{
    public class DISASTER_DONATEModel : PageModel
    {
        public List<DisasterCapture> listCaptureDisaster = new List<DisasterCapture>();
        public DisasterCapture disasterCapture = new DisasterCapture();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            disasterCapture.disasterName = Request.Form["disastername"];
            disasterCapture.startDate = Request.Form["startdate"].ToString();
            disasterCapture.endDate = Request.Form["enddate"].ToString();
            disasterCapture.location = Request.Form["locOfDisaster"];
            disasterCapture.description = Request.Form["description"];

            if (disasterCapture.disasterName == "" || disasterCapture.startDate == "" || disasterCapture.endDate == "" || disasterCapture.location == "" || disasterCapture.description == "")
            {
                errorMsg = "Please fill in all required fields";
                return;
            }
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Brentley\\Documents\\DAF_DATABASE.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "INSERT INTO D_CAPTURE " +
                                 "(DISASTER_NAME, START_DATE, END_DATE, LOCATION, DESCRIPTION) VALUES " +
                                 "(@disastername, @startdate, @enddate, @locOfDisaster, @description);";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@disastername", disasterCapture.disasterName);
                        cmd.Parameters.AddWithValue("@startdate", disasterCapture.startDate);
                        cmd.Parameters.AddWithValue("@enddate", disasterCapture.endDate);
                        cmd.Parameters.AddWithValue("@locOfDisaster", disasterCapture.location);
                        cmd.Parameters.AddWithValue("@description", disasterCapture.description);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                errorMsg = e.Message;
                return;
            }

            disasterCapture.disasterName = "";
            disasterCapture.startDate = "";
            disasterCapture.endDate = "";
            disasterCapture.location = "";
            disasterCapture.description = "";
            successMsg = "Disaster captured";

        }
    }
    public class DisasterCapture 
    {
        public string id;
        public string disasterName;
        public string startDate;
        public string endDate;
        public string location;
        public string description;
    }
}
