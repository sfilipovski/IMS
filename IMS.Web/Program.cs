using IMS.Domain.Identity;
using IMS.Repository;
using IMS.Repository.Implementation;
using IMS.Repository.Interface;
using IMS.Service.Interface;
using IMS.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using IMS.Domain.Common;
using IMS.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<Customer>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentityCore<Admin>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));


builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Testing Api",
        Description = "Testing Endpoints",

        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "S.Filipovski",
            Url = new Uri("https://www.github.com/sfilipovski")
        }
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("swagger/v1/swagger.json", "Testing API");
        opt.RoutePrefix = string.Empty;
    });
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
