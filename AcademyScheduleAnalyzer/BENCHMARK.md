# Benchmark Analysis — String Concatenation vs StringBuilder

## Environment

- BenchmarkDotNet v0.15.8, Windows 11
- Intel Core i7-8850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
- .NET SDK 10.0.401, .NET 10.0.12, X64 RyuJIT

## Results

| Method                     | Iterations | Mean               | Error            | StdDev           | Allocated     |
|--------------------------- |----------- |-------------------:|-----------------:|-----------------:|--------------:|
| StringConcatenation        | 100        |         2,542.1 ns |        103.48 ns |        301.86 ns |         32 KB |
| StringBuilderConcatenation | 100        |           524.2 ns |         20.12 ns |         58.69 ns |       2.03 KB |
| StringConcatenation        | 1000       |       213,035.3 ns |     12,693.43 ns |     37,227.64 ns |       2957 KB |
| StringBuilderConcatenation | 1000       |         2,629.8 ns |        100.24 ns |        290.82 ns |      14.52 KB |
| StringConcatenation        | 10000      |    18,340,469.7 ns |    178,094.72 ns |    148,717.16 ns |  293242.16 KB |
| StringBuilderConcatenation | 10000      |        31,650.2 ns |        388.82 ns |        477.50 ns |     122.41 KB |
| StringConcatenation        | 100000     | 4,053,300,276.9 ns | 75,068,996.74 ns | 62,686,012.19 ns | 29302490.8 KB |
| StringBuilderConcatenation | 100000     |       645,762.0 ns |      2,873.40 ns |      2,547.19 ns |    1183.45 KB |

## Analysis

### Which approach was faster with 100 iterations?

`StringBuilderConcatenation` was faster (524.2 ns vs. 2,542.1 ns for `StringConcatenation`), roughly **4.8x faster** at this small size. The gap already exists even at low iteration counts, though it is not yet dramatic.

### Which approach was faster with 100,000 iterations?

`StringBuilderConcatenation` was dramatically faster: 645,762 ns (≈0.65 ms) versus 4,053,300,276.9 ns (≈4.05 seconds) for `StringConcatenation`. That is roughly **6,300x faster**.

### Which approach allocated more memory?

`StringConcatenation` allocated far more memory at every iteration count, and the gap widens enormously as iterations increase. At 100,000 iterations, `StringConcatenation` allocated about **29.3 GB** of managed memory in total versus only **~1.18 MB** for `StringBuilderConcatenation` — roughly **24,700x more**.

### What happened to string concatenation performance as the loop size increased?

Performance degraded far worse than linearly. Going from 100 → 1,000 → 10,000 → 100,000 iterations (each step is a 10x increase in work), the mean time went:
- 100 → 1,000: ~84x slower (not ~10x)
- 1,000 → 10,000: ~86x slower
- 10,000 → 100,000: ~221x slower

This is quadratic-like (O(n²)) growth, not linear (O(n)). Each concatenation creates a new, longer string, so the cost of each operation grows with the string's current length — the more you've already appended, the more expensive the next append becomes.

### Why does repeated string concatenation create additional allocations?

In .NET, `string` is **immutable** — once created, its contents can never change. Every time `result += "abc"` executes, the runtime cannot extend the existing string in place. Instead, it must:
1. Allocate a brand-new block of memory large enough for the old string + the new text
2. Copy all the characters from the old string into the new memory
3. Copy the new text onto the end
4. Discard the old string (it becomes garbage, waiting for the Garbage Collector)

Because the string keeps getting longer, each new allocation is also progressively larger, and each copy operation touches more data — hence the accelerating (near-quadratic) cost.

### Why does StringBuilder usually perform better when text is repeatedly appended?

`StringBuilder` maintains an internal, mutable character buffer that has spare capacity. `Append()` writes new characters directly into that existing buffer without creating a new object, as long as there's room. Only when the buffer fills up does `StringBuilder` allocate a new, larger internal array (typically doubling in size) and copy the existing contents over — and this happens far less often than on every single append. This is why the Gen0/Gen1/Gen2 GC counts and total allocated memory stay so much lower for `StringBuilderConcatenation` in the results above.

### Is StringBuilder always better than normal string operations? Explain.

No. `StringBuilder` is better specifically when there are **many repeated modifications** to the same piece of text, especially in a loop, because it avoids repeated allocations and copies.

However, plain string operations (`+`, `+=`, string interpolation, `string.Concat`) are perfectly fine — and often clearer or even faster — when:
- Concatenating a **small, fixed number of strings once** (e.g., `var name = first + " " + last;`) — the compiler can optimize a single, known concatenation efficiently, and the overhead of creating and managing a `StringBuilder` object isn't justified.
- The code is **not in a loop** and mutability isn't needed.
- Readability matters more than the tiny performance difference for a one-off operation.

`StringBuilder` introduces its own object overhead (allocation of the builder itself, and a final `ToString()` call), so for very few operations that cost isn't worth paying. The crossover point where `StringBuilder` clearly wins is exactly what these benchmarks demonstrate: as the number of repeated appends grows, `StringBuilder`'s advantage grows from noticeable (100 iterations) to overwhelming (100,000 iterations).