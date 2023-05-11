using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var connectionStringBuilder = new SqliteConnectionStringBuilder();
connectionStringBuilder.DataSource = "./dane.db";
var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
connection.Open();
SqliteCommand delTableCmd = connection.CreateCommand();
delTableCmd.CommandText = "DROP TABLE IF EXISTS uzytkownicy";
delTableCmd.ExecuteNonQuery();
SqliteCommand createTableCmd = connection.CreateCommand();
createTableCmd.CommandText ="CREATE TABLE uzytkownicy (\"login\" text not null PRIMARY KEY, \"haslo\" text not null);";
createTableCmd.ExecuteNonQuery();
SqliteCommand insertCmd = connection.CreateCommand();
insertCmd.CommandText="INSERT INTO uzytkownicy values"+"(\"admin\",\"admin\"),"+"(\"kasia\",\"123\");";
insertCmd.ExecuteNonQuery();


builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;//plik cookie jest niedostępny przez skrypt po stronie klienta
    options.Cookie.IsEssential = true;//pliki cookie sesji będą zapisywane dzięki czemu sesje będzie mogła być śledzona podczas nawigacji lub przeładowania strony
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
