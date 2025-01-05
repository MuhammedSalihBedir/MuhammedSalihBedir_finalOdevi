using System;
using HotelManagementLibrary;

namespace HotelManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var hotelManager = new HotelManager();
            InitializeDefaultRooms(hotelManager);

            var ui = new HotelUI(hotelManager);
            ui.Start();
        }

        private static void InitializeDefaultRooms(HotelManager hotelManager)
        {
            // Initialize some default rooms
            hotelManager.AddRoom(new SingleRoom(101));
            hotelManager.AddRoom(new SingleRoom(102));
            hotelManager.AddRoom(new SingleRoom(103));
            hotelManager.AddRoom(new SingleRoom(104));
            hotelManager.AddRoom(new SingleRoom(105));


            hotelManager.AddRoom(new DoubleRoom(201));
            hotelManager.AddRoom(new DoubleRoom(202));
            hotelManager.AddRoom(new DoubleRoom(203));
            hotelManager.AddRoom(new DoubleRoom(204));
            hotelManager.AddRoom(new DoubleRoom(205));

            hotelManager.AddRoom(new Suite(301));
            hotelManager.AddRoom(new Suite(302));
            hotelManager.AddRoom(new Suite(303));
            hotelManager.AddRoom(new Suite(304));
            hotelManager.AddRoom(new Suite(305));
        }
    }
}