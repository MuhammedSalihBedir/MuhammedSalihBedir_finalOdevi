using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public interface IRoom
    {
        int RoomNumber { get; }
        bool IsAvailable { get; }
        decimal BasePrice { get; }
        void Book();
        void CancelBooking();
        string GetRoomType();
    }

    public abstract class Room : IRoom
    {
        public int RoomNumber { get; }
        public bool IsAvailable { get; private set; }
        public decimal BasePrice { get; protected set; }

        public Room(int roomNumber, decimal basePrice)
        {
            RoomNumber = roomNumber;
            BasePrice = basePrice;
            IsAvailable = true;
        }

        public void Book()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Room is already booked.");
            IsAvailable = false;
        }

        public void CancelBooking()
        {
            if (IsAvailable)
                throw new InvalidOperationException("Room is not booked.");
            IsAvailable = true;
        }

        public abstract string GetRoomType();

        public override string ToString() =>
            $"Room {RoomNumber} - {GetRoomType()} - {(IsAvailable ? "Available" : "Booked")} - Price: {BasePrice:C}";
    }

    public class SingleRoom : Room
    {
        public SingleRoom(int roomNumber) : base(roomNumber, 50) { }
        public override string GetRoomType() => "Single Room";
    }

    public class DoubleRoom : Room
    {
        public bool HasExtraBed { get; }

        public DoubleRoom(int roomNumber, bool hasExtraBed = false)
            : base(roomNumber, hasExtraBed ? 100 : 80)
        {
            HasExtraBed = hasExtraBed;
        }

        public override string GetRoomType() =>
            HasExtraBed ? "Double Room with Extra Bed" : "Double Room";
    }

    public class Suite : Room
    {
        public string Amenities { get; }

        public Suite(int roomNumber, string amenities) : base(roomNumber, 200)
        {
            Amenities = amenities;
        }

        public override string GetRoomType() => "Suite";

        public override string ToString() =>
            base.ToString() + $" - Amenities: {Amenities}";
    }
}
