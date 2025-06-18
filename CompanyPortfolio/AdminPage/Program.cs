using AdminPage.Service;
using AdminPage.Service.GenericRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IFirebaseGenericRepository<>), typeof(FirebaseGenericRepository<>));
builder.Services.AddScoped<AboutServices>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<SssService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ServicesService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<BannerService>();



//Add sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseSession(); // BU ÖNEMLÝ

// Configure the HTTP request pipeline.
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
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
