using NetCord;
using NetCord.Gateway;
using NetCord.Services.ApplicationCommands;

namespace Chuckybot;

class Program
{
    static async Task Main(string[] args)
    {
        string botToken = Environment.GetEnvironmentVariable("CHUCKYBOT_TOKEN") ?? "TOKEN NOT IN CHUCKYBOT_TOKEN ENVIRONMENT VARIABLE";

        GatewayClient client = new(new BotToken(botToken), new GatewayClientConfiguration
        {
            Intents = GatewayIntents.Guilds
        });
        
        ApplicationCommandService<ApplicationCommandContext> commandService = new();
        commandService.AddModules(typeof(Program).Assembly);

        client.InteractionCreate += async interaction =>
        {
            if (interaction is not ApplicationCommandInteraction appInteraction) return;

            try
            {
                await commandService.ExecuteAsync(new ApplicationCommandContext(appInteraction, client));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        };

        client.Ready += async readyEventArgs =>
        {
            Console.WriteLine($"Ready! Logged in as {readyEventArgs.User.Username}");

            await commandService.RegisterCommandsAsync(client.Rest, client.Id);
        };
        
        await client.StartAsync();
        
        Console.WriteLine("Bot is running. Press Ctrl+C to exit.");
        
        await Task.Delay(Timeout.Infinite);
    }
}