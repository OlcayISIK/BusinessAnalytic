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
    public class Register : PageModel
    {
        public void OnGet()
        {
        }
        public async void OnPost()
        {
            User currUser = new User();
            currUser.Name = Request.Form["Name"];
            currUser.Password = Request.Form["Password"];
            var ConfirmPass = Request.Form["PasswordConfirm"];
            if (ConfirmPass == currUser.Password)
            {
                LoginController loginController = new LoginController();
                await loginController.Register(currUser);
            }

        }

    }
}
