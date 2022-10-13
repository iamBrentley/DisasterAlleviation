using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using DAF_WEBAPP_V1._0._0.Pages.DONATIONS_GOODS;

namespace DAF_WEBAPP_V1._0._0.Pages.DONATIONS_VIEW
{
    public class view_donations_goodsModel : PageModel
    {
        public DONATIONS_GOODS.DonatedGoods donatedGoods = new DONATIONS_GOODS.DonatedGoods();
        public List<DONATIONS_GOODS.DonatedGoods> viewGoods = new List<DONATIONS_GOODS.DonatedGoods>();
        public String errorMsg = "error";

        public List<DONATIONS_GOODS.DonatedGoods> ViewGoods { get => viewGoods; set => viewGoods = value; }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\DAF_DB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "SELECT * FROM DONATION_GOODS";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                DONATIONS_GOODS.DonatedGoods donatedGoods = new DONATIONS_GOODS.DonatedGoods();

                                donatedGoods.date = read.GetDateTime(0).ToString();
                                donatedGoods.numOfGoods = read.GetString(1);
                                donatedGoods.donorName = read.GetString(2);
                                donatedGoods.goodsDescription = read.GetString(3);

                                viewGoods.Add(donatedGoods);

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
