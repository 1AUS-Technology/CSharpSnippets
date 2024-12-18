﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Security.Cryptography;

namespace PerformanceWithNet6.BenchmarkExamples
{
    [SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 10, iterationCount: 10)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class Md5vsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5vsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);
        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);

    }
}
