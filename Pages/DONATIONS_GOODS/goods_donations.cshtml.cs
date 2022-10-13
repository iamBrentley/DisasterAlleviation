using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DAF_WEBAPP_V1._0._0.Pages.DONATIONS_GOODS
{
    public class goods_donationsModel : PageModel
    { 
        public DonatedGoods donatedGoods = new DonatedGoods();
        public List<DonatedGoods> listGoods = new List<DonatedGoods>();
    
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            donatedGoods.date = Request.Form["date"];
            donatedGoods.numOfGoods = Request.Form["numGoods"];
            donatedGoods.donorName = Request.Form["donorName"];
            donatedGoods.goodsDescription = Request.Form["description"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\DAF_DB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString)) 
                {
                    con.Open();
                    String sql = "INSERT INTO DONATION_GOODS " +
                                 "(DONATION_DATE, NUM_OF_GOODS, DONOR_NAME, DESC_OF_GOODS) VALUES " +
                                 "(@date, @numGoods, @donorName, @description);";

                    using (SqlCommand cmd = new SqlCommand(sql, con)) 
                    {
                        cmd.Parameters.AddWithValue("@date", donatedGoods.date);
                        cmd.Parameters.AddWithValue("@numGoods", donatedGoods.numOfGoods);
                        cmd.Parameters.AddWithValue("@donorName", donatedGoods.donorName);
                        cmd.Parameters.AddWithValue("@description", donatedGoods.goodsDescription);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class DonatedGoods
    {
        public String id;
        public String date;
        public String numOfGoods;
        public String donorName;
        public String category;
        public String goodsDescription;
    }
}
