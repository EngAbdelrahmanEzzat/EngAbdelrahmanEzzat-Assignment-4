using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BenchmarkDotNet.Running;

namespace AcademyScheduleAnalyzer;
class Program
{
    static void Main(string[] args)
    {
        string[] sessionNames =
        {
        "C# Basics",
        "Arrays",
        "Functions",
        "Date and Time",
        "Exception Handling"
        };
        DateTime[] sessionDates =
        {
        new DateTime(2026, 9, 10, 18, 0, 0),
        new DateTime(2026, 9, 13, 18, 0, 0),
        new DateTime(2026, 9, 17, 18, 0, 0),
        new DateTime(2026, 9, 20, 18, 0, 0),
        new DateTime(2026, 9, 24, 18, 0, 0)
        };
        int[] sessionDurations =
        {
        180,
        240,
        180,
        240,
        180
        };

        DisplaySchedule(sessionNames, sessionDates, sessionDurations);
        SearchSession(sessionNames, sessionDates, sessionDurations, "ArrAys");
        SortSessionNames(sessionNames);
        ReverseSessionNames(sessionNames);
        FindSessionIndex(sessionNames);

        CheckSessionExists(sessionNames);
        FindSession(sessionNames);
        FindSessionIndex2(sessionNames);
        CopyAndModifySession(sessionNames);

        DisplayDurationAnalysis(sessionDurations);
        SortSessionDurations(sessionDurations);

        Part21_24_Benchmark();
        
    }
    static void DisplaySchedule(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
    {
        Console.WriteLine("Academy Schedule:");
        Console.WriteLine("-----------------");
        for (int i = 0; i < sessionNames.Length; i++)
        {
            Console.WriteLine($"{i+1}. { sessionNames[i]}");
            Console.WriteLine($"Date: {sessionDates[i].ToString("dd MMMM yyyy")}");
            Console.WriteLine($"StartTime: {sessionDates[i].ToString("hh:mm tt")}");
            Console.WriteLine($"Duration: {sessionDurations[i]} minutes");
            Console.WriteLine();
        }
    }
    static void SearchSession(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations, string searchTerm)
    {
        Console.WriteLine($"Searching for sessions containing '{searchTerm}':");
        Console.WriteLine("-----------------");
        for (int i = 0; i < sessionNames.Length; i++)
        {
            if (sessionNames[i].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine($"{i+1}. { sessionNames[i]}");
                Console.WriteLine($"Date: {sessionDates[i].ToString("dd MMMM yyyy")}");
                Console.WriteLine($"StartTime: {sessionDates[i].ToString("hh:mm tt")}");
                Console.WriteLine($"Duration: {sessionDurations[i]} minutes");
                Console.WriteLine();
                return;
            }
        }
        Console.WriteLine("Session not found.");
    }
    static void SortSessionNames(string[] sessionNames)
    {
        string[] sortedNames=new string [sessionNames.Length];
        Array.Copy(sessionNames, sortedNames, sessionNames.Length);
        Array.Sort(sortedNames);
        Console.WriteLine("Sorted Session Names:");
        Console.WriteLine("-----------------");
        foreach (string name in sortedNames)
        {
            Console.WriteLine(name);
        }
        Console.WriteLine();
    }
    static void ReverseSessionNames(string[] sessionNames)
    {
        string[] reversedNames = new string[sessionNames.Length];
        Array.Copy(sessionNames, reversedNames, sessionNames.Length);
        Array.Reverse(reversedNames);
        Console.WriteLine("Reversed Session Names:");
        Console.WriteLine("-----------------");
        foreach (string name in reversedNames)
        {
            Console.WriteLine(name);
        }
        Console.WriteLine();
    }
    static void FindSessionIndex(string[] sessionNames)
    {
        Console.WriteLine("Enter The Session Name to Search:");
        string searchTerm = Console.ReadLine()!;
        for (int i = 0; i < sessionNames.Length; i++)
        {
            if (sessionNames[i].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine($"Session found at index {i}");
                return;
            }
        }
        Console.WriteLine("Session not found.");
    }
    static void CheckSessionExists(string[] sessionNames)
    {
        Console.WriteLine("Enter The Session Name to Check:");
        string searchTerm = Console.ReadLine() ?? "";

        bool exists = Array.Exists(sessionNames, name =>
            name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

        if (exists)
        {
            Console.WriteLine("Session exists.");
        }
        else
        {
            Console.WriteLine("Session does not exist.");
        }
    }
    static void FindSession(string[] sessionNames)
    {
        Console.WriteLine("Enter part of the session name to find:");
        string searchTerm = Console.ReadLine() ?? "";

        string? result = Array.Find(sessionNames, name =>
            name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

        if (result != null)
        {
            Console.WriteLine($"Found session: {result}");
        }
        else
        {
            Console.WriteLine("No matching session found.");
        }
    }
    static void FindSessionIndex2(string[] sessionNames)
    {
        Console.WriteLine("Enter part of the session name to find its index:");
        string searchTerm = Console.ReadLine() ?? "";

        int index = Array.FindIndex(sessionNames, name =>
            name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

        if (index >= 0)
        {
            Console.WriteLine($"Session found at index: {index}");
        }
        else
        {
            Console.WriteLine("Session not found.");
        }
    }
    static void CopyAndModifySession(string[] sessionNames)
    {
        
        string[] copiedNames = new string[sessionNames.Length];

       
        Array.Copy(sessionNames, copiedNames, sessionNames.Length);

        
        copiedNames[0] = "Modified Session Name"; // Modify the first element of the copied array


        Console.WriteLine("Original array:");
        foreach (string name in sessionNames)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("\nCopied array:");
        foreach (string name in copiedNames)
        {
            Console.WriteLine(name);
        }
    }
    static int GetTotalDuration(int[] sessionDurations)
    {
        int total = 0;
        foreach (int duration in sessionDurations)
        {
            total += duration;
        }
        return total;
    }

    static double GetAverageDuration(int[] sessionDurations)
    {
        return (double)GetTotalDuration(sessionDurations) / sessionDurations.Length;
    }

    static int GetShortestDuration(int[] sessionDurations)
    {
        int shortest = sessionDurations[0];
        foreach (int duration in sessionDurations)
        {
            if (duration < shortest)
            {
                shortest = duration;
            }
        }
        return shortest;
    }

    static int GetLongestDuration(int[] sessionDurations)
    {
        int longest = sessionDurations[0];
        foreach (int duration in sessionDurations)
        {
            if (duration > longest)
            {
                longest = duration;
            }
        }
        return longest;
    }
    static void DisplayDurationAnalysis(int[] sessionDurations)
    {
        int total = GetTotalDuration(sessionDurations);
        double average = GetAverageDuration(sessionDurations);
        int shortest = GetShortestDuration(sessionDurations);
        int longest = GetLongestDuration(sessionDurations);

        Console.WriteLine("Duration Analysis:");
        Console.WriteLine("-----------------");
        Console.WriteLine($"Total Duration: {total} minutes");
        Console.WriteLine($"Average Duration: {average} minutes");
        Console.WriteLine($"Shortest Duration: {shortest} minutes");
        Console.WriteLine($"Longest Duration: {longest} minutes");
    }
    static void SortSessionDurations(int[] sessionDurations)
    {
        int[] sortedDurations = new int[sessionDurations.Length];
        Array.Copy(sessionDurations, sortedDurations, sessionDurations.Length);
        Array.Sort(sortedDurations);

        Console.WriteLine("Sorted Durations (ascending):");
        Console.WriteLine("-----------------");
        foreach (int duration in sortedDurations)
        {
            Console.WriteLine(duration);
        }
    }
    static void DisplaySessionDetails(string[] seessionNames, DateTime[] sessionDates, int[] sessionDurations, int index)
    {
        if (index >= 0 && index < seessionNames.Length)
        {
            Console.WriteLine($"Session Details for Index {index}:");
            Console.WriteLine($"Name: {seessionNames[index]}");
            Console.WriteLine($"Date: {sessionDates[index].ToString("dd MMMM yyyy")}");
            Console.WriteLine($"StartTime: {sessionDates[index].ToString("hh:mm tt")}");
            Console.WriteLine($"Duration: {sessionDurations[index]} minutes");
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }
    static DateTime GetSessionEndTime(DateTime[] sessionDates, int[] sessionDurations, int index)
    {
       
            return sessionDates[index].AddMinutes(sessionDurations[index]);
       
    }

    static DateTime? ReadSessionDate()// Method to read a session date from user input
    {
        Console.WriteLine("Enter the session date (dd/MM/yyyy):");
        string input = Console.ReadLine()!;
        if (DateTime.TryParse(input, out DateTime sessionDate))
        {
            return sessionDate;
        }
        else
        {
            Console.WriteLine("Invalid date format.");
            return null;
        }
    }
    static string BuildReportUsingString(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
    {
        string report = "";
        report += " Report (Using String)\n";
        report += "-----------------\n";

        for (int i = 0; i < sessionNames.Length; i++)
        {
            report += $"{i + 1}. {sessionNames[i]}\n";
            report += $"Date: {sessionDates[i].ToString("dd MMMM yyyy")}\n";
            report += $"StartTime: {sessionDates[i].ToString("hh:mm tt")}\n";
            report += $"Duration: {sessionDurations[i]} minutes\n\n";
        }

        return report;
    }
    static string BuildReportUsingStringBuilder(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
    {
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("Report (Using StringBuilder)");
        reportBuilder.AppendLine("-----------------");
        for (int i = 0; i < sessionNames.Length; i++)
        {
            reportBuilder.AppendLine($"{i + 1}. {sessionNames[i]}");
            reportBuilder.AppendLine($"Date: {sessionDates[i].ToString("dd MMMM yyyy")}");
            reportBuilder.AppendLine($"StartTime: {sessionDates[i].ToString("hh:mm tt")}");
            reportBuilder.AppendLine($"Duration: {sessionDurations[i]} minutes");
            reportBuilder.AppendLine();
        }
        return reportBuilder.ToString();
    }

    static void ChangeValueUsingRef(ref int number)
    {
        number = number * 2;// This method changes the value of the number passed by reference
    }
    static bool FindSessionByName(string[] sessionNames, int[] sessionDurations, string sessionName, out int index, out int duration)
    {
        for (int i = 0; i < sessionNames.Length; i++)
        {
            if (sessionNames[i].Equals(sessionName, StringComparison.OrdinalIgnoreCase))
            {
                index = i;
                duration = sessionDurations[i];
                return true;
            }
        }
        //دي لو احنا ملقناش اي حاجه بالاسم ده
        index = -1;
        duration = 0;
        return false;
    }
    static void ModifyArrayElement(string[] sessionNames)
    {
        sessionNames[0] = "Modified Session Name"; // تعديل بدون ref
    }
    static void CalculateTotalDuration(params int[] durations)
    {
        int total = 0;
        foreach (int duration in durations)
        {
            total += duration;
        }
        Console.WriteLine($"Total Duration: {total} minutes");
    }
    static void SessionDateDetails(string sessionName, string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
    {
        for (int i = 0; i < sessionNames.Length; i++)
        {
            if (sessionNames[i].Equals(sessionName, StringComparison.OrdinalIgnoreCase))
            {
               
                Console.WriteLine($"Date: {sessionDates[i].ToString("dd MMMM yyyy")}");
                Console.WriteLine($"The Day is {sessionDates[i].Day}");
                Console.WriteLine($"The Month is {sessionDates[i].Month}");
                Console.WriteLine($"The Year is {sessionDates[i].Year}");
                Console.WriteLine($"The Day of the Week is {sessionDates[i].DayOfWeek}");
                Console.WriteLine($"The Start Time is {sessionDates[i].ToString("hh:mm tt")}");
                Console.WriteLine($"The Duration is {sessionDurations[i]} minutes");
                Console.WriteLine($"The End Time is {sessionDates[i].AddMinutes(sessionDurations[i]).ToString("hh:mm tt")}");
                return;
            }
        }

    }
    static void DateDifference(string session1,string session2, DateTime[] sessionDates)//part 10
    {
        TimeSpan difference = sessionDates[1] - sessionDates[0];
        Console.WriteLine($"Total Days between {session1} and {session2}: {difference.Days} days");
        Console.WriteLine($"Total Hours between {session1} and {session2}: {difference.TotalHours} hours");
    
    }
    static void PastandUpcomingSessions(string[] Sessions, DateTime[] dates)
    {
        for (int i = 0; i < Sessions.Length; i++)
        {
            if (DateTime.Now > dates[i])
            {
                Console.WriteLine($"{Sessions[i]} Past");
            }
            else
            {
                Console.WriteLine($"{Sessions[i]} Upcoming");
            }
        }
    }
    static void FindtheNextSession(string[] SessionsNames, DateTime[] dates)
    {
        DateTime Now = DateTime.Now;
        DateTime D1 = Now;
        string Session = "";

        for (int i = 0; i < SessionsNames.Length; i++)
        {
            if (Now > dates[i])
            {
                continue;
            }
            else if (D1 < dates[i])
            {
                D1 = dates[i];
                Session = SessionsNames[i];
            }
        }

        TimeSpan remaining = D1 - Now;

        Console.WriteLine($"The Next Session: {Session}");
        Console.WriteLine($"{D1.ToString("dd MMMM yyyy")}");
        Console.WriteLine($"{D1.ToString("hh:mm tt")}");
        Console.WriteLine($"Time Remaining: {remaining.Days} days {remaining.Hours} hours");
    }
   
    static void DisplaySelectedSession(DateTime selectedDate)
    {
        

        Console.WriteLine(selectedDate.ToString("yyyy-MM-dd"));
        Console.WriteLine(selectedDate.ToString("dd/MM/yyyy"));
        Console.WriteLine(selectedDate.ToString("dd MMMM yyyy"));
        Console.WriteLine(selectedDate.ToString("dddd, dd MMMM yyyy hh:mm tt"));
    }
    static DateTime ReadValidDate()
    {
        while (true)
        {
            Console.Write("Enter date (yyyy-MM-dd HH:mm): ");
            string input = Console.ReadLine()!;

            if (DateTime.TryParseExact(
                input,
                "yyyy-MM-dd HH:mm",
                null,
                System.Globalization.DateTimeStyles.None,
                out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Invalid date. Try again.");
        }
    }
    static int ReadMenuOption()
    {
        while (true)
        {
            Console.Write("Choose an option: ");

            try
            {
                int option = int.Parse(Console.ReadLine()!);
                return option;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid menu option. Enter a number.");
            }
        }
    }
    static void ReadSessionIndex(string[] Sessions)
    {
        while (true)
        {
            Console.Write("Enter session index: ");

            try
            {
                int index = int.Parse(Console.ReadLine()!);
                Console.WriteLine($"Session: {Sessions[index]}");
                break;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The selected session index is out of range.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
    static void ValidateDuration(int duration)
    {
        if (duration <= 0)
        {
            throw new ArgumentException("Duration must be greater than zero.");
        }

        Console.WriteLine("Duration accepted.");
    }
    static int ReadMenuOptionToTryFinally()
    {
        while (true)
        {
            try
            {
                Console.Write("Choose an option: ");
                int option = int.Parse(Console.ReadLine()!);
                return option;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid menu option. Enter a number.");
            }
            finally
            {
                Console.WriteLine("Input operation finished.");
            }
        }
    }
    static void Part21_24_Benchmark()//من بارت 21 ل 24
    {
        BenchmarkRunner.Run<StringConcatBenchmark>();
    }



}