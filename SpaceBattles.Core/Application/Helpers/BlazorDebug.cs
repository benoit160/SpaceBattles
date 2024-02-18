using System.Diagnostics;

namespace SpaceBattles.Core.Application.Helpers;

public static class BlazorDebug
{
    [Conditional("DEBUG")]
    public static void WriteLine(string input)
    {
        Console.WriteLine(input);
    }
}