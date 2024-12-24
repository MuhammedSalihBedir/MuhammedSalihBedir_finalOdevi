using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public interface IBookingService
    {
        void AddBooking(int roomNumber, Customer customer);
        void ListBookings();
        void DeleteBooking(int roomNumber);
        void UpdateBooking(int roomNumber, Customer newCustomer);
    }

    public class BookingService : IBookingService
    {
        private readonly List<IRoom> rooms;
        private readonly List<Booking> bookings = new List<Booking>();

        public BookingService(List<IRoom> rooms)
        {
            this.rooms = rooms;
        }

        public void AddBooking(int roomNumber, Customer customer)
        {
            var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null || !room.IsAvailable)
                throw new InvalidOperationException("Room is not available.");

            room.Book();
            bookings.Add(new Booking(room, customer));
        }

        public void ListBookings()
        {
            foreach (var booking in bookings)
                Console.WriteLine(booking);
        }

        public void DeleteBooking(int roomNumber)
        {
            var booking = bookings.FirstOrDefault(b => b.Room.RoomNumber == roomNumber);
            if (booking == null)
                throw new InvalidOperationException("No booking found for the specified room.");

            booking.Room.CancelBooking();
            bookings.Remove(booking);
        }

        public void UpdateBooking(int roomNumber, Customer newCustomer)
        {
            var booking = bookings.FirstOrDefault(b => b.Room.RoomNumber == roomNumber);
            if (booking == null)
                throw new InvalidOperationException("No booking found for the specified room.");

            booking.Room.CancelBooking();
            booking.Room.Book();
            bookings.Remove(booking);
            bookings.Add(new Booking(booking.Room, newCustomer));
        }
    }
}
