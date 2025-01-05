using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class SingleRoom : Room
    {
        public override string Type => "Single Room";
        public SingleRoom(int roomNumber) : base(roomNumber) => Price = 100;
    }
}
