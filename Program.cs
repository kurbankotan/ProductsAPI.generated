using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//CORS i�in service ekleme
var MyAllowSpecificOrigions = "_myAllowSpecificOrigions";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigions, policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")     //Api �zerinden veri �ekecek olan. De�i�tir
                                                   .AllowAnyHeader()        //T�m header'a izin ver
                                                   .AllowAnyMethod();       //T�m metotlara izin ver
    });
});



builder.Services.AddDbContext<ProductsContext>(x => x.UseSqlite("Data Source = products.db"));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ProductsContext>();


builder.Services.Configure<IdentityOptions>(options =>{

    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthentication(options => {

                                          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidIssuer = "kurbankotan.com",    //Hangi firma taraf�ndan
                                ValidateAudience = false,
                                ValidAudience = "",                        //Kimin i�in
                                ValidAudiences = new string[] {"a","b"},    //veya kimler i�in geli�tirilmi�
                                ValidateIssuerSigningKey = true,            //En �nemli k�s�m bu. Validate edilen k�s�m
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value ?? "")),
                                ValidateLifetime = true
                            };
                        });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//Swagger'da login i�in 
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Token i�in Authentication middleware'in eklenmesi laz�m
app.UseAuthentication();

app.UseRouting();

//CORS i�in
app.UseCors(MyAllowSpecificOrigions);  //Statik dosyalar varsa. UseStatik'ten �nce tan�mlanmasl�


app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
