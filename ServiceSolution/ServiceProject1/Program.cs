using Org.Example.Service;
using ServiceProject1;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET Business Service";
});

builder.Services.AddSingleton<IBusinessService, BusinessService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
