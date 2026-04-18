using System.Net.Http.Json;
using System.Text.Json.Serialization;
using NetCord.Services.ApplicationCommands;

namespace Chuckybot;

public class Joke : ApplicationCommandModule<ApplicationCommandContext>
{
    private static readonly HttpClient HttpClient = new ();
    private static readonly string ApiUrl = "https://api.chucknorris.io/jokes";
        
    [SlashCommand("chucky", "Get a random joke")]
    public async Task<string> GetJokeAsync(string? query = null)
    {
        var random = new Random();
        try
        {
            ChuckNorrisResponse response;    
            if (query is null)
            {
                response =
                    await HttpClient.GetFromJsonAsync<ChuckNorrisResponse>($"{ApiUrl}/random");
            }
            else
            {
                var responses = await HttpClient.GetFromJsonAsync<List<ChuckNorrisResponse>>($"{ApiUrl}/search?query={query}");
                response = responses?.Shuffle().FirstOrDefault();
            }
            
            return response?.Value ?? "Chuck Norris stared down the API, and it refused to return a joke.";
        }
        catch
        {
            return "Even Chuck Norris couldn't connect to the API right now.";
        }
    }
}