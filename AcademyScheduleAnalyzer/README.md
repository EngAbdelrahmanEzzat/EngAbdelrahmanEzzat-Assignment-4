<p align="center">
  <strong>SIMULATION</strong> · Software House &amp; Academy · <a href="https://simulationeg.com">simulationeg.com</a>
</p>

# Academy Schedule Analyzer

**Student Name:** Abdelrahman Ezzat Fetouh
**Cohort:** Groub 2
**Assignment:** Assignment 4 (Academy Schedule Analyzer)

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-console_app-239120?logo=csharp&logoColor=white)

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [LeetCode](#leetcode)
- [LinkedIn](#linkedin)
- [Benchmark](#benchmark)

---

## Overview

**Academy Schedule Analyzer** is a C# console application that manages and analyzes a schedule of academy sessions — searching, sorting, filtering by date, validating input, and generating reports.

## Features

- 📋 Display all sessions with full details
- 🔍 Search sessions by name (partial match)
- 🔤 Sort and reverse session names
- 📊 Duration statistics (total, average, shortest, longest)
- 📅 Full date breakdown for a session (day, month, year, day of week)
- ⏳ Split sessions into past vs. upcoming
- ⏭️ Find the next upcoming session with time remaining
- 🆚 Compare two session dates
- ✅ Custom date input validation
- 📝 Generate reports two ways — `string` concatenation vs. `StringBuilder` — for a hands-on performance comparison
- 🔁 Persistent menu loop with input validation (`try/catch`) until the user exits

## Tech Stack

- **Language:** C#
- **Runtime:** .NET SDK
- **Type:** Console Application

## Project Structure

```
.
├── AcademyScheduleAnalyzer/   # Main console application
├── LeetCode/                  # LeetCode solutions & notes
├── LinkedIn/                  # LinkedIn posts & write-ups
├── BENCHMARK.md                # Benchmark results (string vs StringBuilder)
└── README.md                  # You are here
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later recommended)
- A terminal / IDE of your choice (Visual Studio, VS Code, Rider, etc.)

### Run the Application

```bash
cd AcademyScheduleAnalyzer
dotnet run
```

The console menu will appear:

```
===================================
      Academy Schedule Analyzer
===================================
1.  Display all sessions
...
0.  Exit
===================================
```

- Enter the number of the operation you want to run, then press **Enter**.
- Choose **0** at any time to exit the application.

---

## LeetCode

Solutions and notes: [`LeetCode/README.md`](./LeetCode/README.md)

## LinkedIn

Posts and write-ups: [`LinkedIn/README.md`](./LinkedIn/README.md)

## Benchmark

Performance results (`string` vs `StringBuilder`, etc.): [`BENCHMARK.md`](./BENCHMARK.md)