using System;
using System.IO;
using Discord;
using Discord.Addons.Hosting;
using Discord.Interactions;
using Discord.WebSocket;
using Giveaways;
using Giveaways.Data;
using Giveaways.Services;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddNamedOptions<StartupOptions>();

builder.Services.AddHangfire(options => options.UseSQLiteStorage());
builder.Services.AddHangfireServer();
builder.Services.AddSqlite<AppDbContext>(builder.Configuration.GetConnectionString("Default"));

builder.Services.AddDiscordHost((config, _) =>
{
    config.SocketConfig = new DiscordSocketConfig
    {
        LogLevel = LogSeverity.Info,
        GatewayIntents = GatewayIntents.AllUnprivileged,
        LogGatewayIntentWarnings = false,
        UseInteractionSnowflakeDate = false,
        AlwaysDownloadUsers = false,
    };

    config.Token = builder.Configuration.GetSection(StartupOptions.GetSectionName()).Get<StartupOptions>()!.Token;
});

builder.Services.AddInteractionService((config, _) =>
{
    config.LogLevel = LogSeverity.Debug;
    config.DefaultRunMode = RunMode.Async;
    config.UseCompiledLambda = true;
});
builder.Services.AddInteractiveService(config =>
{
    config.LogLevel = LogSeverity.Warning;
    config.DefaultTimeout = TimeSpan.FromMinutes(5);
    config.ProcessSinglePagePaginators = true;
});

builder.Services.AddSingleton<InteractionRouter>();
builder.Services.AddHostedService<InteractionHandler>();

builder.Services.AddScoped<GiveawayFormatter>();
builder.Services.AddScoped<GiveawayService>();
builder.Services.AddSingleton<GiveawayScheduler>();

var host = builder.Build();

await host.MigrateAsync<AppDbContext>();
await host.RunAsync();
