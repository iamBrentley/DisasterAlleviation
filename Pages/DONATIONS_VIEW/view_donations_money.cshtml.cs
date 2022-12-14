using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DAF_WEBAPP_V1._0._0.Pages.DONATIONS_VIEW
{
    public class view_donations_moneyModel : PageModel
    {
        public DONATIONS_MONEY.DonatedMoney donatedMoney = new DONATIONS_MONEY.DonatedMoney();
        public List<DONATIONS_MONEY.DonatedMoney> viewMoney = new List<DONATIONS_MONEY.DonatedMoney>();
        public String errorMsg = "error";
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\DAF_DB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "SELECT * FROM DONATION_MONEY";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                DONATIONS_MONEY.DonatedMoney donatedMoney = new DONATIONS_MONEY.DonatedMoney();

                                donatedMoney.date = read.GetDateTime(0).ToString();
                                donatedMoney.amount = read.GetString(1);
                                donatedMoney.donorName = read.GetString(2);
                                

                                viewMoney.Add(donatedMoney);

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
