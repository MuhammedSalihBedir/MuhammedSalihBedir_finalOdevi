using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelManagementLibrary
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string Contact { get; set; }

        protected Person(string name, string contact)
        {
            Name = name;
            Contact = contact;
        }
    }
}
