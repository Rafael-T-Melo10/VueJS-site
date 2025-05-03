using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class YahooController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IWebHostEnvironment _env;

    public YahooController(IHttpClientFactory clientFactory, IWebHostEnvironment env)
    {
        _clientFactory = clientFactory;
        _env = env;
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetStockHistory(
        string symbol,
        [FromQuery] string interval = "1mo",
        [FromQuery] string range = "5y"
    )
    {
        var mockFileName = $"mock-{symbol}-{interval}-{range}.json";
        var mockPath = Path.Combine(_env.WebRootPath, "mocks", mockFileName);

        if (System.IO.File.Exists(mockPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(mockPath);
            return Content(json, "application/json");
        }

        var client = _clientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "700db5ba69msh081457af76a898cp16c086jsnb9c67140e4db");
        client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");

        var url = $"https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v3/get-chart?symbol={symbol}&interval={interval}&range={range}&region=BR";
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "Erro na API");

        var content = await response.Content.ReadAsStringAsync();

        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "mocks"));

        await System.IO.File.WriteAllTextAsync(mockPath, content);

        return Content(content, "application/json");
    }
    
    [HttpGet("movers")]
    public async Task<IActionResult> GetMarketMovers()
    {
        var mockFileName = "mock-movers.json";
        var mockPath = Path.Combine(_env.WebRootPath, "mocks", mockFileName);

        // 1. Verifica se existe mock salvo
        if (System.IO.File.Exists(mockPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(mockPath);
            return Content(json, "application/json");
        }

        // 2. Se não existir, busca da API
        var client = _clientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "700db5ba69msh081457af76a898cp16c086jsnb9c67140e4db");
        client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");

        var url = "https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-movers?region=BR&lang=pt";

        try
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Erro na API de movers");

            var content = await response.Content.ReadAsStringAsync();

            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "mocks"));
            await System.IO.File.WriteAllTextAsync(mockPath, content);

            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
}

}
