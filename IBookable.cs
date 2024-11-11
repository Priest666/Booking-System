using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal interface IBookable
    { 
        //Interface för Booking klassen.
        void NewBooking();
        void ListAllBookings();
        void UpdateBooking();
        void DeleteBooking();
        void ListYear();
    }
}
