using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using Microsoft.Extensions.DependencyInjection;
using ReversiMvcApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Configure database contexts
builder.Services.AddDbContext<ReversiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReversiDbContext")
    ?? throw new InvalidOperationException("Connection string 'ReversiDbContext' not found.")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add this line to register the RoleManager<IdentityRole>
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireBeheerderRole", policy => policy.RequireRole("Beheerder"));
});

builder.Services.AddControllersWithViews();

// Register other services
builder.Services.AddTransient<ReversiApi>();

var app = builder.Build();

// Ensure the database is up to date and roles are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    // Ensure roles are created
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await EnsureRolesAsync(roleManager);

    // Ensure a user is assigned the Beheerder role
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await EnsureDefaultUserAsync(userManager);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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

async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "Beheerder" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

async Task EnsureDefaultUserAsync(UserManager<IdentityUser> userManager)
{
    var defaultUser = await userManager.FindByEmailAsync("Admin@Admin.nl");
    if (defaultUser != null && !await userManager.IsInRoleAsync(defaultUser, "Beheerder"))
    {
        await userManager.AddToRoleAsync(defaultUser, "Beheerder");
    }
}
