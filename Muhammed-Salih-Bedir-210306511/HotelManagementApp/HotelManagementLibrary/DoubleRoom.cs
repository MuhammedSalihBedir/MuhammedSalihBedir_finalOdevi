using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class DoubleRoom : Room
    {
        public override string Type => "Double Room";
        public DoubleRoom(int roomNumber) : base(roomNumber) => Price = 200;
    }
}
