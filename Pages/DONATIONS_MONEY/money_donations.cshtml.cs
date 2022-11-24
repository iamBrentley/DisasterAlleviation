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
        public List<DonatedMoney> listMoney = new List<DonatedMoney>();//list of the class created below containing variables where user entered values are going to be stored
        //listMoney is the object we have created in order to reference the DonatedMoney class below as a list
        public DonatedMoney donatedMoney = new DonatedMoney();//creating a constructor of the class so we can reference it
        public String errorMsg = "";//string to contain error message
        public String successMsg = "";//string to contain success message
        public void OnGet()
        {
        }

        public void OnPost() //this is the method our form is going to execute when we hit submit
        {
            //below we referencing our variables from the object class DonatedMoney with the constructor we made called donatedMoney in order to store the value the user entered
            //in the form and by using Request.Form and specifying which value we want the variable to store, pretty straight forward stuff here
            donatedMoney.date = Request.Form["date"];
            donatedMoney.amount = Request.Form["money"];
            donatedMoney.donorName = Request.Form["donorName"];

            if (donatedMoney.date.Length == 0 || donatedMoney.amount.Length == 0)//this if statement is pretty self explanatory
            {
                errorMsg = "Please fill in all required fields";
                return;
            }

            try//we are tryng to store the values we get from the form into our database, with a local database and Azure
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

    public class DonatedMoney//public class for storing our user entered variables, we storing these in a public class in order to reference them in a list as an object of the class, so that we arent scope limited
    {
        public String id;
        public String date;
        public String amount;
        public String donorName;
    }
}
