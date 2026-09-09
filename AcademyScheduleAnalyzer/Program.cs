using Microsoft.VisualBasic;
using static System.Collections.Specialized.BitVector32;

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


}