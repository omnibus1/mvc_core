using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace RazorPagesIntro.Pages
{
    public class Formularz : PageModel
    {
        public string loginMessage { get; private set; } = "Login: ";
        public string hasloMessage { get; private set; } = "Haslo: ";

        public string poprawnoscLogowania { get; private set;}="";

        public void OnGet()
        {
            if (Request.Query != null){
                string login=Request.Query["user-login"].ToString();
                string haslo=Request.Query["user-haslo"].ToString();
                loginMessage += login;
                hasloMessage+=haslo;
                if(haslo=="123"){
                    HttpContext.Session.SetString("zalogowano","Prawda");
                    poprawnoscLogowania="Zalogowano";
                }
                else{
                    poprawnoscLogowania="Sproboj ponownie";
                }
            }
        }

    }
}