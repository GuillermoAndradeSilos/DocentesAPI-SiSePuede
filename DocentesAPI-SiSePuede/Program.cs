using DocentesAPI_SiSePuede.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Sistem21PrimariaContext>(optionsBuilder
    => optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_primaria;user=sistem21_primaria;password=sistemas19_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));
builder.Services.AddControllers();
var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();