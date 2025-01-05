using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class Suite : Room
    {
        public override string Type => "Suite";
        public Suite(int roomNumber) : base(roomNumber) => Price = 500;
    }
}
