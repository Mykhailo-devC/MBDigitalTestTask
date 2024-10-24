using MBDigitalTestTask.Models;
using MBDigitalTestTask.Services.DataManager.Book;
using MBDigitalTestTask.Services.DataManager.History;
using MBDigitalTestTask.Services.DataManager.Library;
using MBDigitalTestTask.Services.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllers();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<DbClientContext>(opt =>
        opt.UseInMemoryDatabase("LibNet")
    );
}
else
{
    builder.Services.AddDbContext<DbClientContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
}



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILibraryDataManager, LibraryDataManager>();
builder.Services.AddScoped<IBookDataManager, BookDataManager>();
builder.Services.AddScoped<IHistoryDataManager, HistoryDataManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbClientContext>();
    await context.Database.EnsureCreatedAsync();

    if (app.Environment.IsDevelopment())
    {
        await DbSeedInitializer.CreateSafeSeedData(context);
    }
    else
    {
        await DbSeedInitializer.CreateSeedData(context);
    }
    
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
