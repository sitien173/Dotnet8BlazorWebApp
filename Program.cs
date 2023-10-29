global using BlazorWebApp.Shared;
global using BlazorWebApp.Extensions;
using System.Globalization;
using BlazorWebApp.Components;
using BlazorWebApp.Data;
using BlazorWebApp.Pipeline;
using BlazorWebApp.Shared.Enums;
using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration[ConstantStrings.SyncfusionLicenseKey]);

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(ConstantStrings.DefaultCulture);
    
    var supportedCultures = SupportedCulture.List.Select(x => x.Value).ToArray();
    options.AddSupportedUICultures(supportedCultures);
    
    options.FallBackToParentUICultures = true;
    options.RequestCultureProviders.Clear();
});

builder.Services.AddAutoMapper(assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorPipeline<,>));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddDbContext<BlazorBlogXDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConstantStrings.DefaultConnection)!, opt =>
    {
        opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    }));

builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = ConstantStrings.ApplicationName, Version = "v1" });
        c.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
    }).AddEndpointsApiExplorer();

builder.Services.AddSerilog(opt => { opt.ReadFrom.Configuration(builder.Configuration); });

builder.Services.AddCarter(configurator: c =>
{
    c.WithValidatorLifetime(ServiceLifetime.Scoped);
});

builder.Services.AddHttpClient(ConstantStrings.ApplicationName, client =>
{
    client.BaseAddress = new Uri(builder.Configuration[ConstantStrings.AppSetting_SiteUrl]!);
});

// Global configuration for FluentValidation
ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(ConstantStrings.DefaultCulture);
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(app.Configuration)
    .WriteTo.Console()
    .CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
    
    // migration database
    await using var context = app.Services.CreateAsyncScope();
    await context.ServiceProvider.GetRequiredService<BlazorBlogXDbContext>().Database.MigrateAsync();
}
else
{
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRequestLocalization();
app.UseFluentValidationExceptionHandler();
app.MapCarter();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
