using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelManagementLibrary
{
    public interface IBookingService
    {
        void AddBooking(int roomNumber, Customer customer);
        void DeleteBooking(int roomNumber);
        void UpdateBooking(int roomNumber, Customer newCustomer);
        List<Booking> GetAllBookings();
        bool IsRoomBooked(int roomNumber);
    }

    public class BookingService : IBookingService
    {
        private readonly List<Booking> bookings;
        private readonly List<IRoom> rooms;

        public BookingService(List<IRoom> rooms)
        {
            this.rooms = rooms;
            bookings = CsvHelper.LoadBookings() ?? new List<Booking>();
        }

        public void AddBooking(int roomNumber, Customer customer)
        {
            var room = GetRoom(roomNumber) ?? throw new Exception("Room not found.");
            if (IsRoomBooked(roomNumber)) throw new Exception($"Room {roomNumber} is already booked.");

            var booking = new Booking(roomNumber, customer);
            bookings.Add(booking);
            room.IsBooked = true;
            SaveBookings();
            ConsoleHelper.ShowSuccess($"Room {roomNumber} has been successfully booked for {customer.Name}.");
        }

        public void DeleteBooking(int roomNumber)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomNumber == roomNumber)
                ?? throw new Exception("Booking not found.");

            var room = GetRoom(roomNumber);
            if (room != null) room.IsBooked = false;

            bookings.Remove(booking);
            SaveBookings();
            ConsoleHelper.ShowSuccess($"Booking for room {roomNumber} has been successfully cancelled.");
        }

        public void UpdateBooking(int roomNumber, Customer newCustomer)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomNumber == roomNumber)
                ?? throw new Exception("Booking not found.");

            booking.Customer = newCustomer;
            SaveBookings();
            ConsoleHelper.ShowSuccess($"Booking for room {roomNumber} has been successfully updated.");
        }

        public List<Booking> GetAllBookings() => bookings;

        public bool IsRoomBooked(int roomNumber) => bookings.Any(b => b.RoomNumber == roomNumber);

        private IRoom GetRoom(int roomNumber) => rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

        private void SaveBookings() => CsvHelper.SaveBookings(bookings);
    }
}
