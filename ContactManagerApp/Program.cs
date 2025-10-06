using ContactManager.Core.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCoreLayer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ContactManagerApp.MappingProfiles.ViewModelProfile>();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Index}/{id?}");

app.Run();
