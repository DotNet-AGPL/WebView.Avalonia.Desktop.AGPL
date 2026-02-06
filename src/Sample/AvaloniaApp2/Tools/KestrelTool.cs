namespace AvaloniaApp2.Tools;

using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

public static class KestrelTool
{
    public static IHost BuildSlimWebApp_Test(string[] urls = default!)
    {
        urls = urls?.Length > 0 ? urls : ["http://*:5000"];

        var serviceAction = (IServiceCollection services) =>
        {
            if (1 == 1)
            {
                return;
            }
        };

        var endpointRouteAction = (IEndpointRouteBuilder endpointRoute) =>
        {
            // 处理/api/hello路径
            endpointRoute.MapGet("/api/hello", context =>
            {
                var name = context.Request.Query["name"];
                var message = string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";

                return context.Response.WriteAsync($"api/hello：{message}");
            });

#pragma warning disable IL2026, IL3050
            endpointRoute.MapGet("api/users", () => $"User ").AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                // Per-endpoint tweaks
                operation.Summary = "Gets all users";
                operation.Description = "Return all users";

                return Task.CompletedTask;
            });

            endpointRoute.MapGet("api/users/{id}", (int id) =>
            {
                return new KestrelTool.User { Id = id, Name = "Alice" };

                //return Results.Ok();
            }).AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.OperationId = Guid.NewGuid().ToString("N");
                operation.Parameters = new[] { new OpenApiParameter { Name = "id", In = ParameterLocation.Path } };
                operation.Summary = "获取用户信息";
                operation.Description = "根据用户ID获取用户详细信息";

                return Task.CompletedTask;
            })
            //.Produces<User>(StatusCodes.Status200OK, "application/json")
            ;

            // app.Services.GetRequiredService<IHostApplicationLifetime>();
#pragma warning restore IL2026, IL3050
        };

        return KestrelTool.BuildSlimWebApp(urls, serviceAction: serviceAction, endpointRouteAction: endpointRouteAction);
    }

    public record IdNameRecord(int Id, string Name);

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public static IHost BuildEmptyWebApp(
        string[] urls,
        string webRoot = "wwwroot",
        Action<IServiceCollection> serviceAction = default!,
        Action<IEndpointRouteBuilder> endpointRouteAction = default!,
        IJsonTypeInfoResolver jsonTypeInfoResolver = default!,
        Func<RequestDelegate, RequestDelegate> middleware = default!)
    {
        urls = urls?.Length > 0 ? urls : ["http://*:5000"];

        // 获取程序运行目录作为内容根目录
        var contentRoot = AppContext.BaseDirectory;

        // 创建WebApi服务
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());

        // 指定内容根目录
        builder.WebHost.UseContentRoot(Path.Combine(contentRoot));

        // JSON配置
        if (jsonTypeInfoResolver != default!)
        {
            builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(jsonTypeInfoResolver));

            //services.AddScoped<IStartupFilter, LogDashboardStartupFilter>();
        }

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        // 启用目录浏览服务
        builder.Services.AddDirectoryBrowser();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        // 服务及组件配置（IStartupFilter）
        serviceAction?.Invoke(builder.Services);

        var app = builder.Build();

        app.UseCors();

        // Configure the HTTP request pipeline.
        /*
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        */

        app.UseDeveloperExceptionPage();

        // openapi/v1.json
        app.MapOpenApi().CacheOutput(options =>
        {
            // 缓存1小时
            options.Expire(TimeSpan.FromHours(1));
        });

        if (string.IsNullOrEmpty(webRoot) == false && Directory.Exists(Path.Combine(contentRoot, webRoot)))
        {
            // 使用默认文件中间件，设置默认文件列表，优先级从高到低
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = [.. new[] { "index.html", "default!.html", "default!.htm" }]
            });

            // 添加静态文件中间件，这会启用对wwwroot目录下静态文件的访问
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, webRoot)),
                RequestPath = string.Empty
            });

            // 启用目录浏览中间件（配合静态文件中间件）
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, webRoot)),
                RequestPath = string.Empty // 目录浏览的根路径（与静态文件保持一致）
            });
        }

        // 中间件
        if (middleware != null)
        {
            app.Use(middleware);
        }

        // 终结点
        endpointRouteAction?.Invoke(app);

        //var userGroup = app.MapGroup("api/users");

        /*
        // 处理/api/hello路径
        app.MapGet("/api/hello", context =>
        {
            var name = context.Request.Query["name"];
            var message = string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";

            return context.Response.WriteAsync($"api/hello：{message}");
        });
        */

        // app.Services.GetRequiredService<IHostApplicationLifetime>();

        return app;
    }

    public static IHost BuildSlimWebApp(
        string[] urls,
        string webRoot = "wwwroot",
        Action<IServiceCollection> serviceAction = default!,
        Action<IEndpointRouteBuilder> endpointRouteAction = default!,
        IJsonTypeInfoResolver jsonTypeInfoResolver = default!,
        Func<RequestDelegate, RequestDelegate> middleware = default!)
    {
        urls = urls?.Length > 0 ? urls : ["http://*:5000"];

        // 获取程序运行目录作为内容根目录
        var contentRoot = AppContext.BaseDirectory;

        // 创建WebApi服务
        var builder = WebApplication.CreateSlimBuilder();

        // 指定内容根目录
        builder.WebHost.UseContentRoot(Path.Combine(contentRoot));

        // JSON配置
        if (jsonTypeInfoResolver != default!) 
        {
            builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(jsonTypeInfoResolver));

            //services.AddScoped<IStartupFilter, LogDashboardStartupFilter>();
        }

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        // 启用目录浏览服务
        builder.Services.AddDirectoryBrowser();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        // 服务及组件配置（IStartupFilter）
        serviceAction?.Invoke(builder.Services);

        var app = builder.Build();

        app.UseCors();

        // Configure the HTTP request pipeline.
        /*
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        */

        app.UseDeveloperExceptionPage();

        // openapi/v1.json
        app.MapOpenApi().CacheOutput(options =>
        {
            // 缓存1小时
            options.Expire(TimeSpan.FromHours(1));
        });

        if (string.IsNullOrEmpty(webRoot) == false && Directory.Exists(Path.Combine(contentRoot, webRoot)))
        {
            // 使用默认文件中间件，设置默认文件列表，优先级从高到低
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = [.. new[] { "index.html", "default!.html", "default!.htm" }]
            });

            // 添加静态文件中间件，这会启用对wwwroot目录下静态文件的访问
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, webRoot)),
                RequestPath = string.Empty
            });

            // 启用目录浏览中间件（配合静态文件中间件）
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, webRoot)),
                RequestPath = string.Empty // 目录浏览的根路径（与静态文件保持一致）
            });
        }

        // 中间件
        if (middleware != null)
        {
            app.Use(middleware);
        }

        // 终结点
        endpointRouteAction?.Invoke(app);

        //var userGroup = app.MapGroup("api/users");

        /*
        // 处理/api/hello路径
        app.MapGet("/api/hello", context =>
        {
            var name = context.Request.Query["name"];
            var message = string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";

            return context.Response.WriteAsync($"api/hello：{message}");
        });
        */

        // app.Services.GetRequiredService<IHostApplicationLifetime>();

        return app;
    }

    public static IHost BuildSlimWebApp_Demo(
        string[] urls, 
        string webRoot = "wwwroot", 
        Action<IServiceCollection, IApplicationBuilder> configAction = default!,
        Func<RequestDelegate, RequestDelegate> middleware = default!)
    {
        urls = urls?.Length > 0 ? urls : ["http://*:5000"];

        // 获取程序运行目录作为内容根目录
        var contentRoot = AppContext.BaseDirectory;

        var builder = WebApplication.CreateSlimBuilder();

        // 指定内容根目录
        builder.WebHost.UseContentRoot(Path.Combine(contentRoot));

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            //options.SerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
        });

        // 启用缓存
        builder.Services.AddOutputCache(options => options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromSeconds(1))));

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0);

        // 启用目录浏览服务
        builder.Services.AddDirectoryBrowser();

        // 启用跨域
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        var app = builder.Build();

        app.UseOutputCache();

        // 服务及组件配置
        configAction?.Invoke(builder.Services, app);

        app.UseCors();

        // Configure the HTTP request pipeline.
        app.UseDeveloperExceptionPage();

        // openapi/v1.json
        app.MapOpenApi().CacheOutput(options =>
        {
            // 缓存1小时
            options.Expire(TimeSpan.FromSeconds(1));
        });

        if (string.IsNullOrEmpty(webRoot)==false && Directory.Exists(Path.Combine(contentRoot, webRoot))) 
        {
            // 使用默认文件中间件，设置默认文件列表，优先级从高到低
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = [.. new[] { "index.html", "default!.html", "default!.htm" }]
            });

            // 添加静态文件中间件，这会启用对wwwroot目录下静态文件的访问
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, webRoot)),
                RequestPath = string.Empty
            });

            // 启用目录浏览中间件（配合静态文件中间件）
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, webRoot)),
                RequestPath = string.Empty // 目录浏览的根路径（与静态文件保持一致）
            });
        }

        // 中间件
        if (middleware != null) 
        {
            app.Use(middleware);
        }

        var userGroup = app.MapGroup("api/users");

        //userGroup.MapGet("/", () => { });

        // 处理/api/hello路径
        app.MapGet("/api/hello", context =>
        {
            var name = context.Request.Query["name"];
            var message = string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";

            return context.Response.WriteAsync($"api/hello：{message}");
        });

#pragma warning disable IL2026, IL3050
        app.MapGet("api/users", () => $"User ").AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            // Per-endpoint tweaks
            operation.Summary = "Gets all users";
            operation.Description = "Return all users";

            return Task.CompletedTask;
        });

        app.MapGet("api/users/{id}", (int id) =>
        {
            return new User { Id = id, Name = "Alice" };

            //return Results.Ok();
        }).AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            operation.OperationId = Guid.NewGuid().ToString("N");
            operation.Parameters = new[] { new OpenApiParameter { Name = "id",In = ParameterLocation.Path } }; 
            operation.Summary = "获取用户信息";
            operation.Description = "根据用户ID获取用户详细信息";

            return Task.CompletedTask;
        })
        //.Produces<User>(StatusCodes.Status200OK, "application/json")
        ;

        // app.Services.GetRequiredService<IHostApplicationLifetime>();
#pragma warning restore IL2026, IL3050
        
        //var temp = typeof(AppJsonSerializerContext);
        //var temp1 = typeof(BatchJsonContext);
        return app;
    }
}
