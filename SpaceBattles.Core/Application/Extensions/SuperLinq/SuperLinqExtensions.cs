namespace SpaceBattles.Core.Application.Extensions.SuperLinq;

public static class SuperLinqExtensions
{
    private const string NotFound = "No elements matching the given predicate was found.";

    public static ref readonly T First<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        where T : struct
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                return ref source[i];
            }
        }

        throw new Exception(NotFound);
    }

    public static ref T First<T>(this Span<T> source, Predicate<T> predicate)
        where T : struct
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                return ref source[i];
            }
        }

        throw new Exception(NotFound);
    }

    public static int Count<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
    {
        int count = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                count++;
            }
        }

        return count;
    }

    public static int Count<T>(this ReadOnlyMemory<T> source, Predicate<T> predicate)
    {
        int count = 0;
        for (int i = 0; i < source.Span.Length; i++)
        {
            if (predicate(source.Span[i]))
            {
                count++;
            }
        }

        return count;
    }
}