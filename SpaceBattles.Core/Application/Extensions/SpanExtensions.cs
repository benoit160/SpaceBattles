namespace SpaceBattles.Core.Application.Extensions;

public static class SpanExtensions
{
    public static bool All<T>(this ReadOnlySpan<T> span, Predicate<T> predicate)
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (!predicate(span[i])) return false;
        }

        return true;
    }

    public static bool Any<T>(this ReadOnlySpan<T> span, Predicate<T> predicate)
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (predicate(span[i])) return true;
        }

        return false;
    }

    public static T? Find<T>(this ReadOnlySpan<T> span, Predicate<T> predicate)
        where T : class
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (predicate(span[i])) return span[i];
        }

        return null;
    }
}