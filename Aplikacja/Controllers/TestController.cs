using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplikacja.Models;

public class TestController : Controller
{
    public IActionResult Index()
    {
        Console.WriteLine("SSSSSSSSSSSS");
        return View();
    }
    public IActionResult WczytajFormularz()
    {
        return View();
    }

    public IActionResult DlaZalogowanych()
    {
        if (HttpContext.Session.Keys.Contains("zalogowano") && HttpContext.Session.GetString("zalogowano")=="True"){
            return View();
        }
        else{
            return RedirectToAction("WczytajFormularz");
        }
    }

    //Obsługa metody POST
    [HttpPost] 
    public IActionResult WczytajFormularz(IFormCollection form)
    {
        string dane = form["dane"].ToString();
        string haslo=form["haslo"].ToString();
        Console.WriteLine("haslo: "+haslo);
        if(haslo=="123"){
            HttpContext.Session.SetString("zalogowano", "True");
            // string url=string.Format("DlaZalogowanych");
            return RedirectToAction("DlaZalogowanych");
        }
        else{
            HttpContext.Session.SetString("zalogowano", "False");
        }
        ViewData["dane"] += dane;
        return View();
    }
    [HttpPost] 
    public IActionResult DlaZalogowanych(IFormCollection form)
    {
        Console.WriteLine("Xxxx");
        HttpContext.Session.SetString("zalogowano", "False");
        return RedirectToAction("WczytajFormularz");
    }
    
}