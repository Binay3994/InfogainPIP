using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.Context;
using Sales_EcommerceUtility.DI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Sales_EcommerceDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<Sales_EcommerceDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// configuring Swagger/OpenAPI 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    option.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole("Employee"));
    option.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
}
    
    );
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


//builder.Services.AddSwaggerGen();

/*-----------------------------Swagger PArt-----------------------------*/
#region Swagger REgion
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET Core Web API E-Commerce",
        Description = " ITI Project"
    });
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
        {
        Type = ReferenceType.SecurityScheme,
        Id = "Bearer"
        }
        },
        new string[] {}
        }
        });
});
#endregion
//--------------------------------
builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
         policy => policy.WithOrigins("http://localhost:3000")
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();    //add before authorezation 
app.UseAuthorization();

app.MapControllers();

app.Run();