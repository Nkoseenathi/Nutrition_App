using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Nutrition_App.Pages.Food
{
    public class Edit : PageModel
    {
        //Create properties for creating a new food item 
        //Associate the properties to the form by binding them
        //Add an error message that will be displayed if the field is not provided
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public string Food { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public string Serving { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public string Food_Type { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public string Calories { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public double Total_Fat { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Total_Carbo_hydrate { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Sugars { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Protein { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Vitamin_A { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Vitamin_C { get; set; }
        [BindProperty, Required(ErrorMessage = "This field is required")]
        public byte Iron { get; set; }
        public string ErrorMessage { get; set; } = "";

        //Read the name parameter from the url
        public void OnGet(string name)
        {
            //Connect to the database and retrieve the details of the food provided in the url
            try
            {

                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "Select * from nutrition where Food_and_Serving like '%" + name + "%'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NutritionalInfo nutritionInfo = new NutritionalInfo();
                                String[] temp = reader.GetString(0).Split(",");
                                Food = temp[0];
                                if (temp.Length > 1)
                                    Serving = temp[1];
                                else
                                    Serving = "N/A";

                                Calories = reader.GetString(1);
                                Total_Fat = reader.GetDouble(2);
                                Total_Carbo_hydrate = reader.GetByte(3);
                                Sugars = reader.GetByte(4);
                                Protein = reader.GetByte(5);
                                Vitamin_A = reader.GetByte(6);
                                Vitamin_C = reader.GetByte(7);
                                Iron = reader.GetByte(8);
                                temp = reader.GetString(9).Split(new char[] { ',', ' ' });
                                Food_Type = temp[0];



                            }
                            else
                            {
                                //if the food does not exist redirect the user to the index page
                                Response.Redirect("/Food/Index");
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {

                ErrorMessage = e.ToString();
            }
        }
        public void OnPost()
        {

            //Check if the submitted data is valid
            if (!ModelState.IsValid)
            {
                return;
            }


            // Connect to the database and update the food item using the update statement 
            try
            {
                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "UPDATE NUTRITION SET "
                    + "Food_and_Serving=@Food_and_Serving, Calories=@Calories, Total_Fat=@Total_Fat, Total_Carbo_hydrate=@Total_Carbo_hydrate, Sugars=@Sugars, Protein=@Protein, Vitamin_A=@Vitamin_A,Vitamin_C=@Vitamin_C, Iron=@Iron, Food_Type=@Food_Type"
                    + " where Food_and_Serving like '%" + Food + "%'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Food_and_Serving", Food
                        + "," + Serving);
                        command.Parameters.AddWithValue("@Calories", Calories);
                        command.Parameters.AddWithValue("@Total_Fat", Total_Fat);
                        command.Parameters.AddWithValue("@Total_Carbo_hydrate", Total_Carbo_hydrate);
                        command.Parameters.AddWithValue("@Sugars", Sugars);
                        command.Parameters.AddWithValue("@Protein", Protein);
                        command.Parameters.AddWithValue("@Vitamin_A", Vitamin_A);
                        command.Parameters.AddWithValue("@Vitamin_C", Vitamin_C);
                        command.Parameters.AddWithValue("@Iron", Iron);
                        command.Parameters.AddWithValue("@Food_Type", Food_Type);

                        command.ExecuteNonQuery();
                        Response.Redirect("/Food/Index");

                    }
                }
            }
            catch (Exception e)
            {
                //if there is an error message store it and it will be displayed in the page

                ErrorMessage = e.ToString();
            }
        }
    }
}