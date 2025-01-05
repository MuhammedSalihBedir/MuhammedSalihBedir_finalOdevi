using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class HotelUI
    {
        private readonly HotelManager hotelManager;
        private readonly Dictionary<string[], Action> commandMap;

        public HotelUI(HotelManager hotelManager)
        {
            this.hotelManager = hotelManager;
            commandMap = new Dictionary<string[], Action>
            {
                { new[] { "list rooms", "lr" }, () => {
                    ClearAndShowNavigation("Room List");
                    hotelManager.ListRooms();
                    ShowContinuePrompt();
                } },
                { new[] { "add booking", "ad" }, () => {
                    ClearAndShowNavigation("New Booking");
                    AddBooking();
                    ShowContinuePrompt();
                } },
                { new[] { "list bookings", "lb" }, () => {
                    ClearAndShowNavigation("Booking List");
                    hotelManager.ListBookings();
                    ShowContinuePrompt();
                } },
                { new[] { "delete booking", "db" }, () => {
                    ClearAndShowNavigation("Delete Booking");
                    DeleteBooking();
                    ShowContinuePrompt();
                } },
                { new[] { "update booking", "ub" }, () => {
                    ClearAndShowNavigation("Update Booking");
                    UpdateBooking();
                    ShowContinuePrompt();
                } },
                { new[] { "employee", "emp" }, HandleEmployeeCommands },
                { new[] { "exit", "e" }, () => {
                    Console.Clear();
                    ConsoleHelper.ShowInfo("Thank you for using our system. Goodbye!");
                    Environment.Exit(0);
                } }
            };
        }

        public void Start()
        {
            var bookingService = new BookingService(hotelManager.GetRooms());
            hotelManager.SetBookingService(bookingService);
            ShowWelcomeScreen();
            RunCommandLoop();
        }

        private void RunCommandLoop()
        {
            while (true)
            {
                Console.Write("\n📝 Enter command: ");
                var input = Console.ReadLine()?.ToLower().Trim() ?? "";

                try
                {
                    var matchedCommand = commandMap.FirstOrDefault(cmd => cmd.Key.Contains(input));
                    if (matchedCommand.Key != null)
                    {
                        matchedCommand.Value();
                    }
                    else
                    {
                        ConsoleHelper.ShowError("Invalid command. Please choose from the commands shown above.");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(ex.Message);
                    ShowContinuePrompt();
                }
            }
        }

        private void ShowWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   Welcome to Hotel Management System   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine("\n📋 Available Commands:");
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║  list rooms (lr)    - List all rooms          ║");
            Console.WriteLine("║  add booking (ad)   - Book a room             ║");
            Console.WriteLine("║  delete booking (db) - Cancel a booking       ║");
            Console.WriteLine("║  update booking (ub) - Update a booking       ║");
            Console.WriteLine("║  list bookings (lb) - List all bookings       ║");
            Console.WriteLine("║  employee (emp)     - Employee management     ║");
            Console.WriteLine("║  exit (e)           - Exit the system         ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n⚠️ Note: Press ESC at any time to return to the main menu");
            Console.ResetColor();
        }

        private void ClearAndShowNavigation(string destination)
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine($"🔄 Navigating to: {destination}");
            Console.WriteLine(new string('═', 50) + "\n");
        }

        private string ReadValidatedInput(string prompt, Func<string, bool> validator)
        {
            string input;
            do
            {
                Console.Write(prompt);

                input = "";
                while (true)
                {
                    var keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        ShowWelcomeScreen();
                        RunCommandLoop();
                        return null;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input = input.Substring(0, input.Length - 1);
                            Console.Write("\b \b");
                        }
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        input += keyInfo.KeyChar;
                        Console.Write(keyInfo.KeyChar);
                    }
                }
            } while (!validator(input) || input == "");
            return input;
        }

        private bool TryGetRoomNumber(string prompt, out int roomNumber)
        {
            Console.Write(prompt);
            roomNumber = 0;

            string input = "";

            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ShowWelcomeScreen();
                    RunCommandLoop();
                    return false;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
            }

            if (!int.TryParse(input, out roomNumber))
            {
                ConsoleHelper.ShowError("Invalid room number.");
                return false;
            }
            return true;
        }

        private void HandleEmployeeCommands()
        {
            ClearAndShowNavigation("Employee Management Section");


            if (ReadValidatedInput("🔐 Enter password //password is 0000: ", p => p == "0000") != "0000")
            {
                ConsoleHelper.ShowError("Invalid password. Access denied.");
                return;
            }

            bool stayInEmployeeMenu = true;
            while (stayInEmployeeMenu)
            {
                Console.WriteLine("\n👥 Employee Management Menu:");
                Console.WriteLine("╔═══════════════════════╗");
                Console.WriteLine("║  1. Add Employee      ║");
                Console.WriteLine("║  2. List Employees    ║");
                Console.WriteLine("║  3. Delete Employee   ║");
                Console.WriteLine("║ESC. Back to Main Menu ║");
                Console.WriteLine("╚═══════════════════════╝");

                var choice = ReadValidatedInput("\n Select option: ", s => new[] { "1", "2", "3" }.Contains(s));
                if (choice == null)
                {
                    return;
                }

                switch (choice)
                {
                    case "1":
                        ClearAndShowNavigation("Add New Employee");
                        AddEmployee();
                        break;
                    case "2":
                        ClearAndShowNavigation("Employee List");
                        hotelManager.ListEmployees();
                        break;
                    case "3":
                        ClearAndShowNavigation("Delete Employee");
                        DeleteEmployee();
                        break;
                }

                if (stayInEmployeeMenu)
                {
                    Console.WriteLine("\nPress any key to return to Employee Menu...");
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    ClearAndShowNavigation("Employee Management Section");
                }
            }
        }

        private void DeleteEmployee()
        {
            hotelManager.ListEmployees();
            var employeeId = ReadValidatedInput("\nEnter Employee ID to delete: ", s => !string.IsNullOrWhiteSpace(s));

            try
            {
                hotelManager.DeleteEmployee(employeeId);
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(ex.Message);
            }
        }

        private void AddEmployee()
        {
            var name = ReadValidatedInput("Enter Employee Name: ", nameChecked);
            var contact = ReadValidatedInput("Enter Employee Phone number (+90 xxx xxx xx xx): ", phoneChecked);
            var employeeId = ReadValidatedInput("Enter Employee ID: ", s => !string.IsNullOrWhiteSpace(s));
            var position = ReadValidatedInput("Enter Employee Position: ", s => !string.IsNullOrWhiteSpace(s));

            hotelManager.AddEmployee(new Employee(name, contact, employeeId, position));
        }

        private void AddBooking()
        {
            hotelManager.ListAvailableRooms();
            var name = ReadValidatedInput("\nEnter Customer Name: ", nameChecked);
            var contact = ReadValidatedInput("Enter Customer Phone number (+90 xxx xxx xx xx): ", phoneChecked);

            int roomNumber;
            bool isValidRoom;
            do
            {
                isValidRoom = TryGetRoomNumber("\nEnter Room Number to Book: ", out roomNumber);
                if (isValidRoom && !hotelManager.IsRoomAvailable(roomNumber))
                {
                    ConsoleHelper.ShowError($"Room {roomNumber} is not available. Please choose an available room.");
                    hotelManager.ListAvailableRooms();
                    isValidRoom = false;
                }
            } while (!isValidRoom);

            try
            {
                hotelManager.AddBooking(roomNumber, new Customer(name, contact));
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(ex.Message);
                AddBooking();
            }
        }

        private void DeleteBooking()
        {
            hotelManager.ListBookings();
            if (TryGetRoomNumber("\nEnter Room Number to Delete Booking: ", out int roomNumber))
            {
                try
                {
                    hotelManager.DeleteBooking(roomNumber);
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(ex.Message);
                }
            }
        }

        private void UpdateBooking()
        {
            hotelManager.ListBookings();
            int roomNumber;
            bool isValidRoom;

            do
            {
                isValidRoom = TryGetRoomNumber("\nEnter Room Number to Update Booking: ", out roomNumber);
                if (!isValidRoom) return;  // Return if ESC was pressed

                if (!hotelManager.IsRoomBooked(roomNumber))
                {
                    ConsoleHelper.ShowError($"Room {roomNumber} is not in the reservation list. Please enter a room number from the list above.");
                    hotelManager.ListBookings();
                    isValidRoom = false;
                }
            } while (!isValidRoom);

            try
            {
                var name = ReadValidatedInput("Enter New Customer Name: ", nameChecked);
                if (name == null) return;  // Return if ESC was pressed

                var contact = ReadValidatedInput("Enter Customer Phone number (+90 xxx xxx xx xx): ", phoneChecked);
                if (contact == null) return;  // Return if ESC was pressed

                hotelManager.UpdateBooking(roomNumber, new Customer(name, contact));
                ConsoleHelper.ShowSuccess($"Booking for room {roomNumber} has been updated successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(ex.Message);
            }
        }

        private bool nameChecked(string name)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z ]*$")) ||
                (System.Text.RegularExpressions.Regex.IsMatch(name, @"^[ ]*$")))
            {
                ConsoleHelper.ShowError("The name must consist of letters and spaces and must not be empty.");
                return false;
            }
            return true;
        }

        private bool phoneChecked(string contact)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(contact, @"^\+90\s\d{3}\s\d{3}\s\d{2}\s\d{2}$")) ||
                string.IsNullOrEmpty(contact))
            {
                ConsoleHelper.ShowError("Invalid phone number.");
                return false;
            }
            return true;
        }

        private void ShowContinuePrompt()
        {
            Console.WriteLine("\nPress any key to return to Main Menu...");
            Console.ReadKey();
            ShowWelcomeScreen();
        }
    }
}
