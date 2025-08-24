// See https://aka.ms/new-console-template for more information

using BestCrush.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<BestCrushDbContext>(options => options.UseSqlite("Data Source=file.db"));

IHost host = builder.Build();
host.Run();
