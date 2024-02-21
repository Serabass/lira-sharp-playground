using System.Diagnostics;
using LiraSharpLib.Lira.Entities.Parser;
using LiraSharpLib.Lira.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LiraSharpPlayground.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    [BindProperty(SupportsGet = true)]
    public string LiraCode { get; set; } = @"#provider ""playground""

~i\""(hello+|bonjour)$\"" => [
  - hello
  - hola
  - bonjour
  - salut
  - привет :w {
    :w = [
      - мир
      - жир
    ]
  }
]
";

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = "hello";

    [BindProperty(SupportsGet = true)]
    public string Output { get; set; } = "";

    public void OnGet()
    {

    }

    public async void OnPost()
    {
        var doc = new LiraParser
        {
            Path = ""
        }.ParseDocument(LiraCode);
        Lira.Provider = new PlaygroundProvider
        {
            Document = doc
        };
        var result = await Lira.Provider.ProcessInput(Input);

        if (result is ProcessStringResult stringResult)
        {
            Output = stringResult.Value;
            return;
        }

        Debugger.Break();
    }
}
