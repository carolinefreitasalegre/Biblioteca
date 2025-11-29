using Repositories.DataContext;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Repositories.Repositories.Contracts;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IItemColecaoRepository, ItemColecaoRepository>();




var app = builder.Build();

// --- PIPELINE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();  // 👈 necessário para API com controllers

app.Run();