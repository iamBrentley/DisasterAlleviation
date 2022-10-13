using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DAF_WEBAPP_V1._0._0.Pages.DONATIONS_MONEY
{
    public class money_donationsModel : PageModel
    {
        public List<DonatedMoney> listMoney = new List<DonatedMoney>();
        public DonatedMoney donatedMoney = new DonatedMoney();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            donatedMoney.date = Request.Form["date"];
            donatedMoney.amount = Request.Form["money"];
            donatedMoney.donorName = Request.Form["donorName"];

            if (donatedMoney.date.Length == 0 || donatedMoney.amount.Length == 0)
            {
                errorMsg = "Please fill in all required fields";
                return;
            }

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\DAF_DB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "INSERT INTO DONATION_MONEY " +
                                 "(DATE, AMOUNT_R, DONOR_NAME) VALUES " +
                                 "(@date, @amount, @donorName);";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@date", donatedMoney.date);
                        cmd.Parameters.AddWithValue("@amount", donatedMoney.amount);
                        cmd.Parameters.AddWithValue("@donorName", donatedMoney.donorName);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return;
            }

            donatedMoney.date = "";
            donatedMoney.amount = "";
            donatedMoney.donorName = "";
            successMsg = "We appreciate your donation!";

        }

    }

    public class DonatedMoney
    {
        public String id;
        public String date;
        public String amount;
        public String donorName;
    }
}
