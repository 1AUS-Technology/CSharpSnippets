using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CsharpRecap.UsingLinq;

public static class ChunkExtensions
{
    public static IEnumerable<IGrouping<TKey, TSource>> ChunkBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
    {
        return source.ChunkBy(keySelector, EqualityComparer<TKey>.Default);
    }

    public static IEnumerable<IGrouping<TKey, TSource>> ChunkBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey> comparer)
    {
        // Flag to signal end of source sequence.
        const bool noMoreSourceElements = true;
        IEnumerator<TSource>? enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        while (true)
        {
            var key = keySelector(enumerator.Current);
            Chunk<TKey, TSource> current = new (key, enumerator, value => comparer.Equals(key, keySelector(value)));
            yield return current;
            if (current.CopyAllChunkElements() == noMoreSourceElements)
            {
                yield break;
            }
        }
    }
}

internal class Chunk<TKey, TSource> : IGrouping<TKey, TSource>
{
    private readonly object _lock = new();

    // Stores the contents of the first source element that
    // belongs with this chunk.
    private readonly ChunkItem head;

    private IEnumerator<TSource> enumerator;

    // Flag to indicate the source iterator has reached the end of the source sequence.
    internal bool isLastSourceElement;

    // A reference to the predicate that is used to compare keys.
    private Func<TSource, bool> predicate;

    // End of the list. It is repositioned each time a new
    // ChunkItem is added.
    private ChunkItem? tail;

    public Chunk(TKey key, [DisallowNull] IEnumerator<TSource> enumerator, [DisallowNull] Func<TSource, bool> predicate)
    {
        Key = key;
        this.enumerator = enumerator;
        this.predicate = predicate;

        // A chunk always contains at least one element
        head = new ChunkItem(enumerator.Current);
        // The end and beginning are the same until the list contains > 1 elements.
        tail = head;
    }

    // Indicates that all chunk elements have been copied to the list of ChunkItems.
    private bool DoneCopyingChunk => tail == null;

    public IEnumerator<TSource> GetEnumerator()
    {
        ChunkItem? current = head;

        while (current != null)
        {
            yield return current.Value;

            lock (_lock)
            {
                if (current == tail)
                {
                    CopyNextChunkElement();
                }
            }

            // Move to the next ChunkItem in the list.
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public TKey Key { get; }

    private void CopyNextChunkElement()
    {
        // Try to advance the iterator on the source sequence.
        isLastSourceElement = !enumerator.MoveNext();

        // If we are (a) at the end of the source, or (b) at the end of the current chunk
        // then null out the enumerator and predicate for reuse with the next chunk.
        if (isLastSourceElement || !predicate(enumerator.Current))
        {
            enumerator = default!;
            predicate = default!;
        }
        else
        {
            tail!.Next = new ChunkItem(enumerator.Current);
        }

        // tail will be null if we are at the end of the chunk elements
        // This check is made in DoneCopyingChunk.
        tail = tail!.Next;
    }

    // Called after the end of the last chunk was reached.
    internal bool CopyAllChunkElements()
    {
        while (true)
        {
            lock (_lock)
            {
                if (DoneCopyingChunk)
                {
                    return isLastSourceElement;
                }

                CopyNextChunkElement();
            }
        }
    }


    private class ChunkItem(TSource val)
    {
        public readonly TSource Value = val;
        public ChunkItem? Next;
    }
}