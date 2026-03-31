using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PaintStore.API.Mapping;
using PaintStore.DataAccess;
using PaintStore.Models.DTOs;
using PaintStore.Models.Interfaces.Repositories;
using PaintStore.Models.Interfaces.Services;
using PaintStore.Repositories.Users;
using PaintStore.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//Migrations ------ 数据迁移 ------- 合理的记录变化 -----自动apply 到 不同环境中去
//指的是当数据库结构发生变化的时候 ------User table ------ Add Column ----- IsVIP
builder.Services.AddControllers().AddJsonOptions(x=> 
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<PaintStoreDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("PaintStoreDb")));

//AddScoped 负责把类 作为 依赖 注册在ASP.NET Core 依赖注入容器里

//Abstraction <--------> Implementation

builder.Services.AddScoped<IUserService, UserService>();    

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(cfg => {}, typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDto>();

var appVersion = builder.Configuration.GetSection("AppVersion");

var openAIKey = builder.Configuration.GetSection("ApiKeys:OpenAI");

Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    //app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
