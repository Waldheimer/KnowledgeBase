using KnowledgeBaseAPI.Data;
using KnowledgeBaseAPI.Data.DataContext;
using KnowledgeBaseAPI.Data.DTOs;
using KnowledgeBaseAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<KB_DataContext>(options =>
{
    string mode = builder.Configuration.GetValue<string>("mode")!;

    if(!mode.Equals("local"))
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    }else
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    }
});

builder.Services.AddScoped<CommandService>();
builder.Services.AddScoped<SnippetService>();
builder.Services.AddScoped<DocumentationService>();
builder.Services.AddScoped<InfoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
