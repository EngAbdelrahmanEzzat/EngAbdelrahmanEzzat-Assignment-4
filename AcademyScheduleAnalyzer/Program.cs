using Microsoft.VisualBasic;

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
}