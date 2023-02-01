using EFWithSQLite.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();
app.MapControllers();
app.Run();
