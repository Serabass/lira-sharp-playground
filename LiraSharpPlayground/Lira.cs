using LiraSharpLib.Providers;

namespace LiraSharpPlayground;

public static class Lira
{
    public static LiraProviderBase Provider = new PlaygroundProvider();

}
