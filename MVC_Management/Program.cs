using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Profiles;
using MVC_Management.loansProfile;
using MVC_Management.PaymentLoanProfit;
using MVC_Management.cllientprofile;
using MVC_Management.StatementView;
using MVC_Management.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();





builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddLogging();
builder.Services.AddHostedService<OverduePenaltiesService>();

var configaMapper = new AutoMapper.MapperConfiguration(
    options =>
    {


        options.AllowNullDestinationValues = true;
        options.AllowNullCollections = true;
        options.AddProfile(new AutoMapperProfiles());
        options.AddProfile(new AutoMapperLoans());
        options.AddProfile(new AutoMapperPL());
        options.AddProfile(new AutoMapperPerLinePayment());
        options.AddProfile(new RecordsAutomapper());
        options.AddProfile(new statementAutomapper()
);
    });


var Mapping = configaMapper.CreateMapper();
builder.Services.AddSingleton(Mapping);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
