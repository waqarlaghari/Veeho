using Microsoft.EntityFrameworkCore;
using Veeho.API.VideoGrpcService;
using Veeho.Application.Interfaces;
using Veeho.Application.Services;
using Veeho.Infrastructure.Seed;
using Veeho.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddGrpc();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeeder.SeedRolesAndAdminAsync(services);
}
// Configure the HTTP request pipeline.
app.MapGrpcService<VideoGrpcService>();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
