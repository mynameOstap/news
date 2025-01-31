using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Repository;
using Service;
using Configurate;
using Data;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Додаємо OpenApi (Swagger)
builder.Services.AddSwaggerGen(); // Це необхідно для Swagger

// Інші сервіси
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddHttpClient<NewsService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddApiAuthentication(builder.Configuration, jwtOptions);
builder.Services.AddHttpContextAccessor();

var configuration = builder.Configuration;
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Додати Swagger в сервіси
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Увімкнути Swagger
    app.UseSwagger();  
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });

    var swaggerUrl = $"http://localhost:{app.Urls.FirstOrDefault()?.Split(':').Last() ?? "5273"}/swagger";
    Console.WriteLine($"Swagger доступний за адресою: {swaggerUrl}");

    try
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = swaggerUrl,
            UseShellExecute = true
        });
    }
    catch
    {
        Console.WriteLine("Не вдалося автоматично відкрити Swagger.");
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 

app.Run();
