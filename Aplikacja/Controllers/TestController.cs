using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplikacja.Models;
using Microsoft.Data.Sqlite;
using System.Collections;

public class TestController : Controller
{

    
    public SqliteConnection connectToDB(){
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "F:/code/dotnet/mvc_core/Aplikacja/dane.db";
        var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        return connection;
    }

    public ArrayList users=new ArrayList();


    public string ExecuteSelectQuery(string query){
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "F:/code/dotnet/mvc_core/Aplikacja/dane.db";
        var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        SqliteCommand selectCmd = connectToDB().CreateCommand();
        selectCmd.CommandText = query;
        string ?resp= (string?)selectCmd.ExecuteScalar();
        
        if(resp==null){
            resp="none";
        }
        Console.WriteLine(resp);
        return resp;
    }

    public string ExecuteInsertQuery(string query){

        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "F:/code/dotnet/mvc_core/Aplikacja/dane.db";
        var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        SqliteCommand insertCmd = connection.CreateCommand();
        insertCmd.CommandText=query;
        insertCmd.ExecuteNonQuery();

        return "";
    }


    public string ExecuteSelectQueryOnAll(string query){
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "F:/code/dotnet/mvc_core/Aplikacja/dane.db";
        var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        SqliteCommand selectCmd = connection.CreateCommand();
        selectCmd.CommandText = query;
        using (SqliteDataReader reader = selectCmd.ExecuteReader()){
            bool firstRow=true;
            while(reader.Read()){
                    string row="";
                    if (firstRow){
                        for (int a = 0; a < reader.FieldCount; a++){
                            row+=reader.GetName(a);
                            row+=",";
                            }
                        firstRow = false;  
                    }
                    users.Add(row);
                    row="";
                    for (int a = 0; a < reader.FieldCount; a++){
                        String?val = null;
                        try {
                            val = reader.GetString(a);
                        } catch {}
                        row+=val;
                        row+=",";
                    }
                    users.Add(row);
                }
        }
        ViewBag.data=users;
        return "";
    }

    public IActionResult WczytajFormularz()
    {
        return View();
    }

    public IActionResult DlaZalogowanych()
    {
        if (HttpContext.Session.Keys.Contains("zalogowano") && HttpContext.Session.GetString("zalogowano")=="True"){
            ExecuteSelectQueryOnAll("select * from uzytkownicy");
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
        string login = form["login"].ToString();
        string haslo=form["haslo"].ToString();
        Console.WriteLine("haslo: "+haslo);
        string response=ExecuteSelectQuery($"select login,haslo from uzytkownicy where login=\'{login}\' and haslo=\'{haslo}\'");
        if(response!="none"){
            HttpContext.Session.SetString("zalogowano", "True");
            // string url=string.Format("DlaZalogowanych");
            return RedirectToAction("DlaZalogowanych");
        }
        ViewData["dane"] += "Sproboj ponownie";
        return View();
    }

    [HttpPost] 
    public IActionResult Wyloguj(IFormCollection form)
    {
        HttpContext.Session.SetString("zalogowano", "False");
        return RedirectToAction("WczytajFormularz");
    }

    [HttpPost] 
    public IActionResult DodajUzytkownika(IFormCollection form)
    {
        string login=form["login"].ToString();
        string haslo=form["haslo"].ToString();
        ExecuteInsertQuery($"insert into uzytkownicy values(\'{login}\',\'{haslo}\')");
        return RedirectToAction("DlaZalogowanych");
    }
    
}