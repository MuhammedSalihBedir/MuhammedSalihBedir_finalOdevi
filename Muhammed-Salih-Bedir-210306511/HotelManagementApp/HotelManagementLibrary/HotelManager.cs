using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class HotelManager
    {
        private readonly List<IRoom> rooms = new List<IRoom>();
        private readonly List<Employee> employees;
        private IBookingService bookingService;

        public HotelManager()
        {
            employees = CsvHelper.LoadEmployees() ?? new List<Employee>();
        }

        public void AddRoom(IRoom room) => rooms.Add(room);
        public List<IRoom> GetRooms() => rooms;
        public void SetBookingService(IBookingService service) => bookingService = service;

        public void AddBooking(int roomNumber, Customer customer) => bookingService.AddBooking(roomNumber, customer);
        public void DeleteBooking(int roomNumber) => bookingService.DeleteBooking(roomNumber);
        public void UpdateBooking(int roomNumber, Customer customer) => bookingService.UpdateBooking(roomNumber, customer);

        public void ListRooms() => DisplayRooms(rooms, "Room List");
        public void ListAvailableRooms() => DisplayRooms(rooms.Where(r => !r.IsBooked && !bookingService.IsRoomBooked(r.RoomNumber)), "Available Rooms");

        private void DisplayRooms(IEnumerable<IRoom> roomsToDisplay, string title)
        {
            var roomsList = roomsToDisplay.OrderBy(r => r.RoomNumber).ToList();
            if (!roomsList.Any())
            {
                ConsoleHelper.ShowWarning($"No {title.ToLower()} found.");
                return;
            }

            Console.WriteLine($"\n📋 {title}:");
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║  Room No. |     Type    |  Price  |   Status   ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            foreach (var room in roomsList)
            {
                Console.WriteLine($"║  {room.RoomNumber,-8} | {room.Type,-11} | ${room.Price,-6} | {(room.IsBooked ? "Booked" : "Available")}  ║");
            }
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        public void ListBookings()
        {
            var bookings = bookingService.GetAllBookings();
            if (!bookings.Any())
            {
                ConsoleHelper.ShowInfo("No bookings found.");
                return;
            }

            Console.WriteLine("\n📋 Booking List:");
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Room No. |    Customer Name  |      Contact      |     Date   ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            foreach (var booking in bookings.OrderBy(b => b.RoomNumber))
            {
                Console.WriteLine($"║  {booking.RoomNumber,-8} | {booking.Customer.Name,-17} | {booking.Customer.Contact,-15} | {booking.BookingDate.ToShortDateString(),-10} ║");
            }
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            CsvHelper.SaveEmployees(employees);
            ConsoleHelper.ShowSuccess($"Employee {employee.Name} has been successfully added.");
        }

        public void DeleteEmployee(string employeeId)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            employees.Remove(employee);
            CsvHelper.SaveEmployees(employees);
            ConsoleHelper.ShowSuccess($"Employee {employee.Name} has been successfully removed.");
        }

        public void ListEmployees()
        {
            if (!employees.Any())
            {
                ConsoleHelper.ShowInfo("No employees found.");
                return;
            }

            Console.WriteLine("\n📋 Employee List:");
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ID |     Name           |      Contact      |         Position        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
            foreach (var employee in employees)
            {
                Console.WriteLine($"║  {employee.EmployeeId,-2} | {employee.Name,-18} | {employee.Contact,-16} | {employee.Position,-23} ║");
            }
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
        }

        public bool IsRoomAvailable(int roomNumber)
        {
            var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            return room != null && !room.IsBooked && !bookingService.IsRoomBooked(roomNumber);
        }

        public bool IsRoomBooked(int roomNumber)
        {
            return bookingService.IsRoomBooked(roomNumber);
        }
    }
}
