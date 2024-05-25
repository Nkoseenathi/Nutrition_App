using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Nutrition_App.Pages.Food
{
    public class Create : PageModel
    {
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

        public void OnGet()
        {
        }
    }
}