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

        public List<NutritionalInfo> foodList { get; set; } = [];
        public void OnGet()
        {
            try
            {
                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustSeverCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "Select * from nutrition";

                     using (SqlCommand command = new SqlCommand(sql,connection)){
                        using(SqlDataReader reader  = command.ExecuteReader()){
                            while(reader.Read()){
                                NutritionalInfo nutritionInfo= new NutritionalInfo();
                                String[] temp= reader.GetString(0).Split(",");
                                nutritionInfo.Food = temp[0];
                                nutritionInfo.Serving = temp[1];
                                nutritionInfo.Calories = reader.GetInt32(1);
                                nutritionInfo.Total_Fat = reader.GetDouble(2);
                                nutritionInfo.Total_Carbo_hydrate= reader.GetInt32(3);
                                nutritionInfo.Sugars= reader.GetInt32(4);
                                nutritionInfo.Protein= reader.GetInt32(5);
                                nutritionInfo.Vitamin_A= reader.GetInt32(6);
                                nutritionInfo.Vitamin_C= reader.GetInt32(7);
                                nutritionInfo.Iron= reader.GetInt32(8);
                                nutritionInfo.Food_Type= reader.GetString(9);

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
    public class NutritionalInfo
    {
        public string Food { get; set; } = "";
        public string Serving { get; set; } = "";
        public string Food_Type { get; set; } = "";
        public int Calories { get; set; }
        public double Total_Fat { get; set; }
        public int Total_Carbo_hydrate { get; set; }
        public int Sugars { get; set; }
        public int Protein { get; set; }
        public int Vitamin_A { get; set; }
        public int Vitamin_C { get; set; }
        public int Iron { get; set; }


    }
}
