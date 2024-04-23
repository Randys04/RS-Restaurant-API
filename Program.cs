using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("ConexionPorDefecto");
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantProjectAPI.ContextoDB>(x => x.UseSqlServer(connectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var reglasCors = "ReglasCors";
builder.Services.AddCors(opt => {
    opt.AddPolicy(name: reglasCors, builder => {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(reglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
