using System.Net.Http.Json;
using System.Text.Json.Serialization;
using NetCord.Services.ApplicationCommands;

namespace Chuckybot;

public class Joke : ApplicationCommandModule<ApplicationCommandContext>
{
    private static readonly HttpClient _httpClient = new ();

    [SlashCommand("chuckie", "Get a random joke")]
    public async Task<string> GetJokeAsync()
    {
        try
        {
            var response =
                await _httpClient.GetFromJsonAsync<ChuckNorrisResponse>("https://api.chucknorris.io/jokes/random");

            return response?.Value ?? "Chuck Norris stared down the API, and it refused to return a joke.";
        }
        catch
        {
            return "Even Chuck Norris couldn't connect to the API right now.";
        }
    }
}