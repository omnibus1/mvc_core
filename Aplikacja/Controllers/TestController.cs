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
}