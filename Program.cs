using NewWebApi.Interface;
using NewWebApi.Services;
using System.Text.Json.Serialization;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json.Serialization;
using Quartz;
using NLog;
using LogServices;
using FreeKassa.NET;
using NewWebApi.Extensions;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Repositories;
using NewWebApi.DataAccess;
using NewWebApi.Services.Contracts;


var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddFreeKassa(int.Parse(Environment.GetEnvironmentVariable("FreeKassa")), Environment.GetEnvironmentVariable("SecretKey1"), Environment.GetEnvironmentVariable("SecretKey2"));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
	.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IPaymentService, PaymentServices>();
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IOpenAiService, OpenAiService>((HttpClient client) =>
{
	client.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
	client.DefaultRequestHeaders.Add("Authorization",$"Bearer {Environment.GetEnvironmentVariable("OpenAiKey")}");
});
var app = builder.Build();

app.UseExceptionHandler(appError =>{});

app.UseCors("AllowOrigin");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHsts();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{
	if(context.Request.Path.StartsWithSegments("/tset"))
	{
		await context.Response.WriteAsync("Ok");
	}
	else
	{
		await next();
	}
});


// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program{}