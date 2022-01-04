using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAnalytic.Controllers;
using BusinessAnalytic.Models.Entities;
    
namespace BusinessAnalytic.Views.Login
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        public async void OnPost()
        {
            User currUser = new User();
            currUser.Name = Request.Form["Name"];
            currUser.Password = Request.Form["Password"];

            LoginController loginController = new LoginController();
            var a= await loginController.Login(currUser);
        }

    }
}
