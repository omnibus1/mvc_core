﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplikacja.Models;

namespace Aplikacja.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public String LiczbaOdwiedzin(int liczbaPowtorzen,
      String napis){
      String napisPomoc = "";
      for (int a = 0; a < liczbaPowtorzen; a++)
      {
      napisPomoc += napis + "\n";
      }
      return napisPomoc;
}
}
