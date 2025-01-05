using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelManagementLibrary
{

    public class Booking
    {
        public int RoomNumber { get; set; }
        public Customer Customer { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(int roomNumber, Customer customer)
        {
            RoomNumber = roomNumber;
            Customer = customer;
            BookingDate = DateTime.Now;
        }
    }
}
