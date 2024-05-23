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
                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
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
                                if (temp.Length > 1)
                                    nutritionInfo.Serving = temp[1];
                                else
                                    nutritionInfo.Serving = "N/A";

                                nutritionInfo.Calories = reader.GetString(1);
                                nutritionInfo.Total_Fat = reader.GetDouble(2);
                                nutritionInfo.Total_Carbo_hydrate= reader.GetByte(3);
                                nutritionInfo.Sugars= reader.GetByte(4);
                                nutritionInfo.Protein= reader.GetByte(5);
                                nutritionInfo.Vitamin_A= reader.GetByte(6);
                                nutritionInfo.Vitamin_C= reader.GetByte(7);
                                nutritionInfo.Iron= reader.GetByte(8);
                                temp = reader.GetString(9).Split(new char[]{',', ' '});
                                nutritionInfo.Food_Type= temp[0];

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
