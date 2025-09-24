#nullable enable
using System;

namespace DemoConsoleApp;

public record Person(string FirstName, string LastName);

class Program
{
    static void Main(string[] args)
    {
        // Exemple de record
        var person = new Person("Arka", "Wittich");
        Console.WriteLine($"Hello {person.FirstName} {person.LastName}!");

        // Nullable reference types
        string? maybeNull = args.Length > 0 ? args[0] : null;
        Console.WriteLine(maybeNull ?? "No argument provided");

        // Switch expression
        int day = DateTime.Now.DayOfWeek switch
        {
            DayOfWeek.Monday => 1,
            DayOfWeek.Tuesday => 2,
            DayOfWeek.Wednesday => 3,
            DayOfWeek.Thursday => 4,
            DayOfWeek.Friday => 5,
            DayOfWeek.Saturday => 6,
            DayOfWeek.Sunday => 7,
            _ => 0
        };
        Console.WriteLine($"Day mapped to number: {day}");

        string? testNullable = null;
        Console.WriteLine(testNullable.Length);
    }
}
