using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Jsons;

namespace RequestsManager;

public static class Services
{
    private static readonly Dictionary<Type, string> _urls = [];

    public static void RegisterServer<T>(string url)
    {
        _urls[typeof(T)] = url;
    }

    public static async Task<Json> Send<T>(string method, Json data) => await Send(_urls[typeof(T)], method, data);

    public static async Task<Json> Send(string url, string method, Json data)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        client.DefaultRequestHeaders.Add("Accept", "*/*");

        var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{url}{method}", content);

        response.EnsureSuccessStatusCode();

        var responseString = response.Content.ReadAsStringAsync().Result;
        // TODO: wtf, how to do this right?
        var unescaped = Regex.Unescape(responseString)[1..^1];

        return new Json(unescaped);
    }
}