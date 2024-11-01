using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class GroupRoom : Premises
    {
        public bool HasWhiteboard { get; set; } // Har denna för att avgöra om det är ett grupprum eller klassrum.

        public GroupRoom(string name, int capacity, bool hasWhiteboard) : base(name, capacity)
        {
            HasWhiteboard = hasWhiteboard;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name} Capacity: {Capacity} Has whiteboard: {HasWhiteboard}");
        }
    }
}
