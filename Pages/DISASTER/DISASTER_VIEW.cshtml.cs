using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAF_WEBAPP_V1._0._0.Pages.DONATIONS_GOODS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DAF_WEBAPP_V1._0._0.Pages.DISASTER
{
    public class DISASTER_VIEWModel : PageModel
    {
        public DISASTER.DisasterCapture disasterCap = new DISASTER.DisasterCapture();
        public List<DISASTER.DisasterCapture> viewDisaster = new List<DISASTER.DisasterCapture>();

        public List<DISASTER.DisasterCapture> ViewDisaster { get; set; }

        public String errorMsg = "error";
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Brentley\\Documents\\DAF_DATABASE.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "SELECT * FROM D_CAPTURE";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                DISASTER.DisasterCapture disasterCap = new DISASTER.DisasterCapture();

                                disasterCap.id = "" + read.GetInt32(0);
                                disasterCap.disasterName = read.GetString(1);
                                disasterCap.startDate = read.GetString(2);
                                disasterCap.endDate = read.GetString(3);
                                disasterCap.location = read.GetString(4);
                                disasterCap.description = read.GetString(5);

                                ViewDisaster.Add(disasterCap);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return;
            }
        }
    }
}
