
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<WebApiServiceAgentSettings>(builder.Configuration.GetSection("WebApiServiceAgents"));

builder.Services.AddScoped<IMatchService, MatchService>();

// IHttpClientFactory
builder.Services.AddHttpClient();

// Configure the HTTP request pipeline.
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Cors", policy =>
        {
            policy
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition")
                .WithOrigins(new string[] { "https://brightgroveserviceswebapi20241220221247.azurewebsites.net" })
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Cors", policy =>
        {
            policy
                .SetIsOriginAllowed((origin) =>
                {
                    var uri = new Uri(origin);

                    // Check standard azure domain names
                    var isAllowed = uri.Scheme == Uri.UriSchemeHttps &&
                                    (
                                        uri.Host.EndsWith("azurewebsites.net", StringComparison.OrdinalIgnoreCase) ||
                                        uri.Host.EndsWith("azureedge.net", StringComparison.OrdinalIgnoreCase) ||
                                        uri.Host.EndsWith("blob.core.windows.net", StringComparison.OrdinalIgnoreCase) ||
                                        uri.Host.EndsWith("search.windows.net", StringComparison.OrdinalIgnoreCase) ||
                                        uri.Host.EndsWith("servicebus.windows.net", StringComparison.OrdinalIgnoreCase)
                                    );

                    // Check localhost
                    if (!isAllowed && builder.Environment.IsDevelopment())
                        isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

                    return isAllowed;
                })
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition");
        });
    });
}

var app = builder.Build();

app.UseCors("Cors");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();