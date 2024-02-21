using LiraSharpLib.Lira.Entities.Parser;
using LiraSharpLib.Providers;

namespace LiraSharpPlayground;

public class PlaygroundProvider : LiraProviderBase
{
  public Playground playground;
  public override string Name => "playground";

  public override string Description => "Playground provider";

  public override async Task Init()
  {
    playground = new() {
      Provider = this
    };
  }
}
