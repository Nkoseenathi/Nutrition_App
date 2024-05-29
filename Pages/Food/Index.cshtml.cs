using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Nutrition_App.Pages.Food
{
    public class Index : PageModel
    {

        public List<NutritionalInfo> foodList { get; set; } = [];//Stores food items that are read from database
        public void OnGet()
        {
            try
            {
                //Create connection String
                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
                //connect to the database
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "Select * from nutrition";//Select everything from database
                    //Connect to the database
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //create an object to read the sql results from database
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Read results if they are available
                            while (reader.Read())
                            {
                                NutritionalInfo nutritionInfo = new NutritionalInfo();
                                String[] temp = reader.GetString(0).Split(",");
                                nutritionInfo.Food = temp[0];
                                //Check if there is serving size if not assign "N/A" to serving property
                                if (temp.Length > 1)
                                    nutritionInfo.Serving = temp[1];
                                else
                                    nutritionInfo.Serving = "N/A";
                                //Assign properties to their correspong table columns
                                nutritionInfo.Calories = reader.GetString(1);
                                nutritionInfo.Total_Fat = reader.GetDouble(2);
                                nutritionInfo.Total_Carbo_hydrate = reader.GetByte(3);
                                nutritionInfo.Sugars = reader.GetByte(4);
                                nutritionInfo.Protein = reader.GetByte(5);
                                nutritionInfo.Vitamin_A = reader.GetByte(6);
                                nutritionInfo.Vitamin_C = reader.GetByte(7);
                                nutritionInfo.Iron = reader.GetByte(8);
                                temp = reader.GetString(9).Split(new char[] { ',', ' ' });
                                nutritionInfo.Food_Type = temp[0];
                                //Add the nutritionInfo object to the list
                                foodList.Add(nutritionInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.Write("We have an error: " + ex);
            }
        }

    }
    //Class for creating food objects
    public class NutritionalInfo
    {
        public string Food { get; set; } = "";
        public string Serving { get; set; } = "";
        public string Food_Type { get; set; } = "";
        public string Calories { get; set; } = "";
        public double Total_Fat { get; set; }
        public byte Total_Carbo_hydrate { get; set; }
        public byte Sugars { get; set; }
        public byte Protein { get; set; }
        public byte Vitamin_A { get; set; }
        public byte Vitamin_C { get; set; }
        public byte Iron { get; set; }


    }
}
