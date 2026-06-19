using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMvc.Data;
using WebMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Incluir el servicio de DbContext con la cadena de conexion a SQL Server
builder.Services.AddDbContext<LibreriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//Incluir el servicio de Identity con configuracion personalizada
builder.Services.AddIdentityCore<Usuario>(options =>
 {
     options.SignIn.RequireConfirmedAccount = false;
     options.Password.RequireNonAlphanumeric = false;
 })
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<LibreriaDbContext>()
 .AddSignInManager();


/*
Manejo de Cookies
Sin esta configuración, ASP.NET no sabe cómo leer 
la sesión del usuario entre requests. AddAuthentication define el esquema por defecto, 
y AddIdentityCookies registra las cookies que Identity usa para persistir el login. 
*/
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = IdentityConstants.ApplicationScheme;
})
.AddIdentityCookies();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    o.SlidingExpiration = true;
    o.LoginPath = "/Usuario/Login";
    o.AccessDeniedPath = "/Usuario/AccessDenied";
});

var app = builder.Build();

/*Invocar la ejecucion del dbseeder con un using scope.
Un scope es un contenedor temporal que permite resolver servicios con vida 
Scoped fuera del ciclo normal de un request.
*/
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LibreriaDbContext>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbSeeder.SeedAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Log errors or handle them as needed
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*
Configuración para corregir el problema con el atributo Precio de tipo double, 
que en la BBDD se representa en formato US, por ejemplo "22.5", 
pero que en mi sistema local se hace de la forma "22,5".
*/
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
    SupportedCultures = new[] { new System.Globalization.CultureInfo("en-US") },
    SupportedUICultures = new[] { new System.Globalization.CultureInfo("en-US") }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
