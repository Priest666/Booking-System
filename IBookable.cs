using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal interface IBookable
    {
        void NewBooking();
        void ListAllBookings();
        void UpdateBooking();
        void DeleteBooking();
        void ListYear();
    }
}
