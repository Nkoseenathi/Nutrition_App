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
    public class Delete : PageModel
    {
        private readonly ILogger<Delete> _logger;

        public Delete(ILogger<Delete> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public void OnPost(String name)
        {
            deleteFood(name);
            Response.Redirect("/Food/Index");
        }

        private void deleteFood(String name){
try
{
                    string connString = "Server=.;Database=nutrition;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    connection.Open();
                    String sql = "Delete from nutrition where Food_and_Serving = " + name;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    { 
                    command.ExecuteNonQuery();


                    }
                }

}
catch (System.Exception)
{
    
    throw;
}

        }
    }
}