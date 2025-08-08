using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Repository.Implementation;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Implementation;
using CoursesApplication.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var environment = builder.Environment.EnvironmentName;

var connectionString = environment == "Test"
    ? "InMemoryDatabase"
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (environment == "Test")
    {
        options.UseInMemoryDatabase("InMemoryDatabase");
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Student>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PasswordHasher<Student>>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddTransient(typeof(ICourseService), typeof(CourseService));
builder.Services.AddTransient(typeof(ISemesterService), typeof(SemesterService));
builder.Services.AddTransient(typeof(IStudentOnCourseService), typeof(StudentOnCourseService));
builder.Services.AddTransient(typeof(IDataFetchService), typeof(DataFetchService));


builder.Services.AddHttpClient();

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

if (app.Environment.IsEnvironment("Test"))
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var manager = scope.ServiceProvider.GetRequiredService<UserManager<Student>>();
    var passwordHasher = services.GetRequiredService<PasswordHasher<Student>>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    await ApplicationDbContextSeeder.SeedDatabase(context, manager, passwordHasher);
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

public partial class Program { }
