using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Domain.DTO;
using Domain.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Models;
using Repositories.DataContext;
using Repositories.Repositories;
using Repositories.Repositories.Contracts;
using Services.Contracts;
using Services.Mappings;
using Services.Services;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

//jwt
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
    IConfiguration configuration;
    configuration = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,    
        ValidateIssuerSigningKey = true,

        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
        
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
        
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoggedinUser, LoggedinUser>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IItemColecaoRepository, ItemColecaoRepository>();

builder.Services.AddScoped<IUploadPhotoService, UploadPhotoService>();
builder.Services.AddScoped<IUSuarioservice, UsuarioService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAuthService, AuthService>();



builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped<IValidator<UsuarioRequest>, UsuariorequestValidator>();
builder.Services.AddScoped<IValidator<UsuarioRequest>, UsuarioUpdateValidator>();


builder.Services.AddValidatorsFromAssemblyContaining<LivroRequestValidator>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", cors =>
    {
        cors.WithOrigins(
                "https://minhabiblioteca-app.onrender.com",
                "http://localhost:5164", // Blazor WebAssembly
                "https://localhost:5164" // (se abrir em HTTPS),
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        // .AllowCredentials();
    });
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment() || builder.Configuration["EnableSwagger"] == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles(); 
app.UseStaticFiles();          

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();  

app.MapFallbackToFile("index.html"); 

app.Run();