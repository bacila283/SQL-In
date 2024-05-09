using WebApplication1;
using WebApplication1.Controllers;

ConnecttoDB connecttoDB = new ConnecttoDB();


HomeController homeController = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run(async (context) =>
{
	//context.Response.ContentType = "text/html; charset=utf-8";
	context.Response.ContentType = "text/html; charset=utf-8";

	// если обращение идет по адресу "/postuser", получаем данные формы
	if (context.Request.Path == "/postuser")
	{
		var form = context.Request.Form;
		string login = form["login"];
		string password = form["password"];
		try
		{
			connecttoDB.Main(login, password);
		}
		catch
		{
			Console.WriteLine("SOmes wrong(");
		};

		//homeController.TestM(login, password);
		//context.Response.Redirect(str);
		context.Response.ContentType = "?";
	}
	else
	{
		await context.Response.SendFileAsync("html/index.html");
	}
});
app.Run();
