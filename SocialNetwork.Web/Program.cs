using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Service;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IPostService, PostService>();
builder.Services.AddHttpClient<IAccountService, AccountService>();
builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddHttpClient<ICommentService, CommentService>();
builder.Services.AddHttpClient<ILikePostService, LikePostService>();

SD.SocialNetworkAPIBase = builder.Configuration["ServiceUrls:SocialNetworkApi"]!;

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikePostService, LikePostService>();

builder.Services.AddScoped<SetLayoutViewBagFilter>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
