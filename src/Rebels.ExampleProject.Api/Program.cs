using Rebels.ExampleProject.CQRS;
using Rebels.ExampleProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connection = new SqliteConnection(config.GetConnectionString("Db"));
    connection.Open();
    return connection;
});
builder.Services.AddDbContext<IUnitOfWork, UnitOfWork>((sp, options) => options.UseSqlite(sp.GetRequiredService<SqliteConnection>()));
builder.Services.AddCQRS();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

    if (!db.Database.EnsureCreated()) throw new InvalidProgramException("Creating and seeding of database failed.");
}

app.Run();
