using Microsoft.EntityFrameworkCore;
using FTNchat.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("origin",
                      builder =>
                      {
                          builder.WithOrigins("*")
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                      });
});

builder.Services.AddDbContext<FTNchatContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FTNchatContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("origin");

app.UseAuthorization();

app.MapControllers();

app.Run();