using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Customer : Person
    {
        public Customer(string name, string contact) : base(name, contact) { }

        public override string ToString() =>
            $"Customer: {Name}, Contact: {Contact}";
    }
}
