# How to 

## Run the project
`dotnet run -c Release -f net8.0 --filter "*" --runtimes net8.0 net9.0`

The preceding dotnet run command:

- Builds the benchmarks in a Release build. 
This is important for performance testing, as most optimizations are disabled in Debug builds, in both the C# compiler and the JIT compiler.
- Targets .NET 8 for the host project. In general with BenchmarkDotNet, 
you want to target the lowest-common denominator of all runtimes you’ll be executing against, so as to ensure that all of the APIs 
being used are available everywhere they’re needed.
- Runs all of the benchmarks in the whole program. The --filter argument can be refined to scope down to just a subset of benchmarks desired, but "*" says “run ’em all.”
runs the tests on both .NET 8 and .NET 9.


The `[MemoryDiagnoser]` attribute indicates I want it to track managed allocation, 
the `[DisassemblyDiagnoser]` attribute indicates I want it to report on the actual assembly code generated for the test