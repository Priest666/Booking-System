using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booking_System
{
    public class Booking : IBookable
    {
        
        public void DeleteBooking()
        {
            
        }

        public void ListAllBookings()
        {
            foreach (var madeBookings in Program.BookingList)
            {
                Console.WriteLine("testing, testing.");
            }
        }

        public void ListYear()
        {
            
        }

        public void NewBooking()
        {
            
        }

        public void UpdateBooking()
        {
            
        }
    }
}
