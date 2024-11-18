using System.Runtime.InteropServices;

namespace PerformanceEngineering.UnitTests;

[TestClass]
public sealed class SpanTests
{
    [TestMethod]
    public void UseSpanToAccessSubsetArray()
    {
        var arr = new byte[10];
        Span<byte> bytes = arr;

        Span<byte> slicedBytes = bytes.Slice(5, 2);

        slicedBytes[0] = 42;
        slicedBytes[1] = 60;

        Assert.AreEqual(42, slicedBytes[0]);
        Assert.AreEqual(60, slicedBytes[1]);

        Assert.AreEqual(arr[5], slicedBytes[0]);
        Assert.AreEqual(arr[6], slicedBytes[1]);

        bytes[2] = 100;

        Assert.AreEqual(arr[2], bytes[2]);
    }

    [TestMethod]
    public void UseRefReturningIndexer()
    {
        Span<MutableStruct> spanOfStructs = new MutableStruct[1];

        spanOfStructs[0].Value = 42;

        Assert.AreEqual(42, spanOfStructs[0].Value);
    }

    [TestMethod]
    public void UseSpanToAvoidAllocation()
    {
        string str = "Hello, World";
        string worldString = str.Substring(7, 5); // Allocates

        ReadOnlySpan<char> worldSpan = str.AsSpan().Slice(7, 5); // No allocation
        Assert.AreEqual('W', worldSpan[0]);
    }

    [TestMethod]
    public void StringAllocationsVsNotAllocation()
    {
        string input = "123,456";
        int commaPos = input.IndexOf(',');

        // two string allocation
        int first = int.Parse(input.Substring(0, commaPos));
        int second = int.Parse(input.Substring(commaPos + 1));

        ReadOnlySpan<char> inputSpan = input;
        int noAllocatedFirst = int.Parse(inputSpan.Slice(0, commaPos));
        int noAllocationSecond = int.Parse(inputSpan.Slice(commaPos + 1));
    }


    [TestMethod]
    public void CreateStringWithoutAllocation()
    {
        int length = 200;

        Random rand = new Random(30);

        //Now, not only have you avoided the allocation, you’re writing directly into the string’s memory on the heap, which means you’re also
        //avoiding the copy and you’re not constrained by size limitations of the stack.
        string id = string.Create(length, rand, (chars, r) =>
                                                {
                                                    for (int i = 0; i < chars.Length; i++)
                                                    {
                                                        chars[i] = (char)rand.Next(0, 100);
                                                    }
                                                });
    }

    [TestMethod]
    public void TestSpanIteration()
    {
        Span<int> constWriteable = stackalloc int[] { 1, 2, 3 };

        var result = IterateThroughSpan(constWriteable);
        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void TestPatternMatchingWithSpan()
    {
        ReadOnlySpan<char> hv = "Hong Victoria";

        CheckStart(hv);
    }

    private static void CheckStart(ReadOnlySpan<char> chars)
    {
        if (chars is ['H', .. var theRest])
        {
            Console.WriteLine("Chars start with 'H' and the rest length is " + theRest.Length);
        }
    }

    public int IterateThroughSpan(ReadOnlySpan<int> intSpan)
    {
        var sum = 0;
        for (int i = 0; i < intSpan.Length; i++)
        {
            sum += intSpan[i];
        }

        return sum;
    }

    private struct MutableStruct
    {
        public int Value { get; set; }
    }
}