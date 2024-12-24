using System;
using HotelManagementLibrary;
using System.Collections.Generic;

namespace HotelManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize rooms
            var rooms = new List<IRoom>
    {
        new SingleRoom(101),
        new DoubleRoom(102),
        new Suite(201, "Jacuzzi, Kitchen, Balcony")
    };

            // Initialize services
            var bookingService = new BookingService(rooms);
            var hotelManager = new HotelManager(rooms, bookingService);

            Console.WriteLine("Welcome to the Hotel Management System! 🏨");
            Console.WriteLine("Type 'help' to see the list of commands.");

            while (true)
            {
                Console.Write("\n> ");
                var input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(input)) continue;

                try
                {
                    switch (input)
                    {
                        case "help":
                            ShowHelp();
                            break;

                        case "list rooms":
                            hotelManager.ListRooms();
                            break;

                        case "add booking":
                            AddBooking(hotelManager);
                            break;

                        case "list bookings":
                            hotelManager.ListBookings();
                            break;

                        case "delete booking":
                            DeleteBooking(hotelManager);
                            break;

                        case "update booking":
                            UpdateBooking(hotelManager);
                            break;

                        case "exit":
                            Console.WriteLine("Goodbye! 👋");
                            return;

                        default:
                            Console.WriteLine("Invalid command. Type 'help' for the list of commands.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("  help            - Show this help message.");
            Console.WriteLine("  list rooms      - List all rooms and their availability.");
            Console.WriteLine("  add booking     - Book a room for a customer.");
            Console.WriteLine("  delete booking  - Cancel a booking for a specific room.");
            Console.WriteLine("  update booking  - Update booking information for a specific room.");
            Console.WriteLine("  list bookings   - List all current bookings.");
            Console.WriteLine("  exit            - Exit the system.");
        }

        static void AddBooking(HotelManager hotelManager)
        {
            Console.Write("\nEnter Room Number to Book: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter Customer Contact: ");
            var contact = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact))
            {
                Console.WriteLine("Customer name and contact cannot be empty.");
                return;
            }

            var customer = new Customer(name, contact);
            hotelManager.AddBooking(roomNumber, customer);
            Console.WriteLine("Booking successful!");
        }

        static void DeleteBooking(HotelManager hotelManager)
        {
            Console.Write("\nEnter Room Number to Delete Booking: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            hotelManager.DeleteBooking(roomNumber);
            Console.WriteLine("Booking deleted successfully.");
        }

        static void UpdateBooking(HotelManager hotelManager)
        {
            Console.Write("\nEnter Room Number to Update Booking: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            Console.Write("Enter New Customer Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter New Customer Contact: ");
            var contact = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact))
            {
                Console.WriteLine("Customer name and contact cannot be empty.");
                return;
            }

            var newCustomer = new Customer(name, contact);
            hotelManager.UpdateBooking(roomNumber, newCustomer);
            Console.WriteLine("Booking updated successfully.");
        }
    }
}

