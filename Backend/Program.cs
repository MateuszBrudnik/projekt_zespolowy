using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projekt.Data;
using Projekt.Entities;
using Projekt.Services;
using ProjektST2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ExpenseContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IWarningService, WarningService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<PdfService>();


// Dodajemy Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ExpenseContext>()
    .AddDefaultTokenProviders();

var vapidDetails = builder.Configuration.GetSection("VapidDetails");
builder.Services.AddScoped<PushNotificationService>(provider =>
    new PushNotificationService(
        vapidDetails["Subject"],
        vapidDetails["PublicKey"],
        vapidDetails["PrivateKey"],
        provider.GetRequiredService<ExpenseContext>()
    )
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
   options.TokenValidationParameters = new TokenValidationParameters
   {
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true,
       ValidIssuer = builder.Configuration["Jwt:Issuer"],
       ValidAudience = builder.Configuration["Jwt:Audience"],
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
   };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

