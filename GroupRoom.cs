using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class GroupRoom : Premises
    {
        public bool HasProjector { get; set; } // Har denna för att avgöra om det är ett grupprum eller klassrum.

        public GroupRoom(string name, int capacity, bool hasProjector) : base(name, capacity)
        {
            HasProjector = hasProjector;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name} Capacity: {Capacity} Has projector: {HasProjector}");
        }
    }
}
