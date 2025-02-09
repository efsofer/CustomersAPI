using Microsoft.EntityFrameworkCore;
using CustomersAPI.Data;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



var builder = WebApplication.CreateBuilder(args);

// ✅ הוספת חיבור למסד הנתונים
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite()) // Enables support for SQL Server geography data type
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals; // ✅ מונע NaN ו-Infinity
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // ✅ תמיכה בהפניות חוזרות
        options.JsonSerializerOptions.WriteIndented = true;
    });


// ✅ הוספת שירותי Authentication ו-Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// ✅ רישום הבקרות
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// ✅ הפעלת Swagger רק במצב פיתוח
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
