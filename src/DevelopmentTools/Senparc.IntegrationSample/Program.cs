
using Senparc.Ncf.Database.SqlServer;

using Senparc.IntegrationSample;
using Senparc.Xncf.DatabaseToolkit.Domain.Services;
using Senparc.Xncf.SystemCore.Domain.Database;

var builder = WebApplication.CreateBuilder(args);

//添加（注册） Ncf 服务（必须）
builder.AddNcf<SQLServerDatabaseConfiguration>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Use NCF（必须）
app.UseNcf();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages();
//    endpoints.MapControllers();
//});
app.Run();
