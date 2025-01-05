using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelManagementLibrary
{
    public static class ConsoleHelper
    {
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error: {message}");
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ {message}");
            Console.ResetColor();
        }

        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️ Warning: {message}");
            Console.ResetColor();
        }

        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ️ {message}");
            Console.ResetColor();
        }
    }

    public static class CsvHelper
    {
        private static readonly string employeesFile = "employees.csv";
        private static readonly string bookingsFile = "bookings.csv";

        public static void SaveEmployees(List<Employee> employees)
        {
            var lines = employees.Select(e => $"{e.Name},{e.Contact},{e.EmployeeId},{e.Position}");
            File.WriteAllLines(employeesFile, lines);
        }

        public static List<Employee> LoadEmployees()
        {
            if (!File.Exists(employeesFile)) return new List<Employee>();

            return File.ReadAllLines(employeesFile)
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Employee(parts[0], parts[1], parts[2], parts[3]);
                })
                .ToList();
        }

        public static void SaveBookings(List<Booking> bookings)
        {
            var lines = bookings.Select(b => $"{b.RoomNumber},{b.Customer.Name},{b.Customer.Contact},{b.BookingDate}");
            File.WriteAllLines(bookingsFile, lines);
        }

        public static List<Booking> LoadBookings()
        {
            if (!File.Exists(bookingsFile)) return new List<Booking>();

            return File.ReadAllLines(bookingsFile)
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Booking(int.Parse(parts[0]), new Customer(parts[1], parts[2]))
                    {
                        BookingDate = DateTime.Parse(parts[3])
                    };
                })
                .ToList();
        }
    }
}
