using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace RazorPagesIntro.Pages
{
    public class KodIndex : PageModel
    {
        public string Message { get; private set; } = "Model strony stworzony w C#";

        public void OnGet()
        {
            Message += " Czas na serwerze " + DateTime.Now;
        }
    }
}