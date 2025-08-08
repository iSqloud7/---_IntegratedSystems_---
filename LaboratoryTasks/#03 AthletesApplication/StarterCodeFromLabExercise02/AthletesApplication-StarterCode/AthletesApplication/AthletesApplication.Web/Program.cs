using AthletesApplication.Domain.IdentityModels;
using AthletesApplication.Repository.Data;
using AthletesApplication.Repository.Implementation;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Implementation;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AthletesApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Dependency Injection for the services
builder.Services.AddTransient<IAthleteService, AthleteService>();
builder.Services.AddTransient<ICompetitionService, CompetitionService>();
builder.Services.AddTransient<IParticipationService, ParticipationService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<ITournamentService, TournamentService>();

// Dependency Injection for the Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
