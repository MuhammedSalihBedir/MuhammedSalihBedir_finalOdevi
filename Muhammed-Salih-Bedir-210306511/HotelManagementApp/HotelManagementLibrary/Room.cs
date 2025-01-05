using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public interface IRoom
    {
        int RoomNumber { get; set; }
        string Type { get; }
        decimal Price { get; set; }
        bool IsBooked { get; set; }
    }

    public abstract class Room : IRoom
    {
        public int RoomNumber { get; set; }
        public abstract string Type { get; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }

        protected Room(int roomNumber)
        {
            RoomNumber = roomNumber;
            IsBooked = false;
        }
    }
}
