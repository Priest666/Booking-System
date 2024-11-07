using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Booking_System
{

    
     internal class ClassRoom : Premises
    {
       
        public bool HasProjector {  get; set; } // Har denna för att avgöra om det är ett grupprum eller klassrum.

        public ClassRoom(string name, int capacity, bool hasProjector) : base(name, capacity)
        {
            HasProjector = hasProjector;
            
        }

        public override string Print()
        {
            
            return "Available Projector: " + HasProjector + ".";
        }
       


    }
}
