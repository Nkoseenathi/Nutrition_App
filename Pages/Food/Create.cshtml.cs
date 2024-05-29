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
    public class Create : PageModel
    {
        // properties for creating a new food item
        //Associate the properties to the form by binding them
        //Add an error message that will be displayed if the field is not provided
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public string Food { get; set; } = "";
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public string Serving { get; set; } = "";
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public string Food_Type { get; set; } = "";
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public string Calories { get; set; } = "";
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public double Total_Fat { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Total_Carbo_hydrate { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Sugars { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Protein { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Vitamin_A { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Vitamin_C { get; set; }
        [BindProperty, Required(ErrorMessage ="This field is required")]
        public byte Iron { get; set; }
        public string ErrorMessage { get; set; } = "";

         public void OnGet(){}
        public void OnPost()
        { 

            //Check if the submitted data is valid
            if(!ModelState.IsValid){
                return;
            }
           
            //Create new food item
            try
            {
                //Create a connection string
                string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
               
               //Connect to the database and Insert the new Item using the insert satement
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "INSERT INTO NUTRITION "
                    +"(Food_and_Serving, Calories, Total_Fat, Total_Carbo_hydrate, Sugars, Protein, Vitamin_A, Vitamin_C, Iron, Food_Type)"
                    +"VALUES"
                    +"(@Food_and_Serving, @Calories, @Total_Fat, @Total_Carbo_hydrate, @Sugars, @Protein, @Vitamin_A, @Vitamin_C, @Iron, @Food_Type)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Food_and_Serving",Food
                        +","+Serving);
                        command.Parameters.AddWithValue("@Calories",Calories);
                        command.Parameters.AddWithValue("@Total_Fat",Total_Fat);
                        command.Parameters.AddWithValue("@Total_Carbo_hydrate",Total_Carbo_hydrate);
                        command.Parameters.AddWithValue("@Sugars",Sugars);
                        command.Parameters.AddWithValue("@Protein",Protein);
                        command.Parameters.AddWithValue("@Vitamin_A",Vitamin_A);
                        command.Parameters.AddWithValue("@Vitamin_C",Vitamin_C);
                        command.Parameters.AddWithValue("@Iron",Iron);
                        command.Parameters.AddWithValue("@Food_Type",Food_Type);
                        
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                //if there is an error message store it and it will be displayed in user interface
                ErrorMessage = e.ToString();
                return;
            }
            //After creating the item redirect to the index page
            Response.Redirect("/Food/Index");
        }
    }
}