using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.UI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Admin", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Member");
});


// Add services to the container.
var UserconnectionString = builder.Configuration.GetConnectionString("UserDbConnectionstring") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(UserconnectionString));

var EventconnectionString = builder.Configuration.GetConnectionString("EventDbConnectionstring");
builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(EventconnectionString));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IAppUserModelRepo, AppUserModelRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IEventModelRepo, EventModelRepo>();


using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.Migrate();

    if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
    {

        IdentityRole adminRole = new();
        adminRole.Name = "Admin";
        var createRoleResult = roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

        if (createRoleResult.Succeeded)
        {
            IdentityUser? user = userManager.FindByNameAsync("Admin").GetAwaiter().GetResult();
            if (user != null)
            {
                var addToRoleResult = userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
        }
    }
}

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Index";
});


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
