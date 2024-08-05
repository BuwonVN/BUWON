using Blazored.LocalStorage;
using Blazored.Toast;
using BWERP;
using BWERP.Repositories.Interfaces;
using BWERP.Repositories.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<ICommentApiClient, CommentApiClient>();
builder.Services.AddTransient<IDailyReportApiClient, DailyReportApiClient>();
builder.Services.AddTransient<IProductionApiClient, ProductionApiClient>();
builder.Services.AddTransient<IWeeklyReportApiClient, WeeklyReportApiClient>();
builder.Services.AddTransient<IDepartmentApiClient, DepartmentApiClient>();

builder.Services.AddTransient<IEmailApiClient, EmailApiClient>();
builder.Services.AddHttpClient<ISendMailService, SendMailService>();
builder.Services.AddHostedService<ScheduledTaskService>();

builder.Services.AddTransient<ITaskApiClient, TaskApiClient>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IRoleApiClient, RoleApiClient>();
builder.Services.AddTransient<IMenuApiClient, MenuApiClient>();
builder.Services.AddTransient<IRoleMenuApiClient, RoleMenuApiClient>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenService, AuthenService>();

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("https://localhost:7036")
	//BaseAddress = new Uri("http://10.11.10.42:8080/")
});

builder.Services.AddHttpClient();
//TOAST
builder.Services.AddBlazoredToast();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
