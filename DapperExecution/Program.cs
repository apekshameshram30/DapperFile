using DapperExecution.Contex;
using DapperExecution.Repository;
using DapperExecution.Sevices;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddCors(options =>
//{

//    options.AddPolicy("AllowOrigin", builder =>
//    {
//        builder
//             .WithOrigins("http://localhost:4200")//update according to your  angular app
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//    });

//});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DapperExapmple", Version = "v1" });
});

    // Add framework services.
    //services.AddDbContext<DapperContext>(options =>
    //    options.UseSqlServer(Configuration.GetConnectionString("connStr")));

    // Other service configurations...

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
