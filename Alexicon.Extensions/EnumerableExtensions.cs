namespace Alexicon.Extensions;

public static class EnumerableExtensions
{
    public static List<T> PopRandomElements<T>(this IList<T> list, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        }

        if (count > list.Count)
        {
            throw new ArgumentException("Count cannot be greater than the number of elements in the list.");
        }
        
        var poppedElements = new List<T>(count);
        var listSize = list.Count;

        for (var i = 0; i < count; i++)
        {
            var randomIndex = Random.Shared.Next(listSize--);
            poppedElements.Add(list[randomIndex]);
            list.RemoveAt(randomIndex);
        }

        return poppedElements;
    }

    public static void Shuffle<T>(this IList<T> list, int attempts)
    {
        var n = list.Count;

        for (var a = 0; a < attempts; a++)
        {
            while (n > 1)
            {
                var k = Random.Shared.Next((--n) + 1);

                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }

    public static void ShuffleOnce<T>(this IList<T> list) => list.Shuffle(1);

    public static void ShuffleTwice<T>(this IList<T> list) => list.Shuffle(2);

    public static void ShuffleFiveTimes<T>(this IList<T> list) => list.Shuffle(5);
}