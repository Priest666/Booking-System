using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class Premises
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Premises(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }


        public virtual string Print()
        {
            return "Base class method";
        }

    }
}
