using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class HotelManager
    {
        private readonly List<IRoom> rooms;
        private readonly IBookingService bookingService;

        public HotelManager(List<IRoom> rooms, IBookingService bookingService)
        {
            this.rooms = rooms;
            this.bookingService = bookingService;
        }

        public void AddRoom(IRoom room) => rooms.Add(room);
        public void ListRooms() => rooms.ForEach(Console.WriteLine);
        public void AddBooking(int roomNumber, Customer customer) =>
            bookingService.AddBooking(roomNumber, customer);
        public void ListBookings() => bookingService.ListBookings();
        public void DeleteBooking(int roomNumber) =>
            bookingService.DeleteBooking(roomNumber);
        public void UpdateBooking(int roomNumber, Customer newCustomer) =>
            bookingService.UpdateBooking(roomNumber, newCustomer);
    }
}
