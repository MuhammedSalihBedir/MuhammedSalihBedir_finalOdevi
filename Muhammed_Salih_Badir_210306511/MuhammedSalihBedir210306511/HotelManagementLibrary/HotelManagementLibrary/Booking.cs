using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementLibrary
{
    public class Booking
    {
        public IRoom Room { get; }
        public Customer Customer { get; }
        public DateTime BookingDate { get; }

        public Booking(IRoom room, Customer customer)
        {
            Room = room;
            Customer = customer;
            BookingDate = DateTime.Now;
        }

        public override string ToString() =>
            $"Booking: {Customer.Name} -> {Room.GetRoomType()} (Room {Room.RoomNumber}) on {BookingDate}";
    }
}
