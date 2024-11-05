using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class Booking : IBookable
    {
        public void DeleteBooking()
        {
            Console.WriteLine("Which booking do you want to remove?");
            string choose = Console.ReadLine();
            bool found = false;

            for (int i = Program.BookingList.Count - 1; i >= 0; i--) // Fick inte till det med en foreach, så denna gör att i börjar på den sista count i listan pga -1 och minskar med ett för varje varv som choose != namnet
            {
                if (choose == Program.BookingList[i].BookedPremises.Name) // Här kollar det om choose == namnet på position i, om de är sant körs koden under
                {
                    found = true;
                    Console.WriteLine($"Removing {Program.BookingList[i].BookedPremises.Name}");
                    Program.BookingList.RemoveAt(i);
                    Console.ReadLine();
                }
            }
            if (!found)
            {
                Console.WriteLine($"Couldn't find {choose}");
                Console.ReadLine();
            }
        }

        public void NewBooking()
        {
        }


        public void ListAllBookings()
        {
            
        }

        public void ListYear()
        {
            Console.WriteLine("Select a year to watch the bookings");
            int correctYear = Convert.ToInt32(Console.ReadLine());
            bool found = false;

            foreach (Booking boc in Program.BookingList)
            {
                
                if (correctYear == boc.StartDate.Year) // Om det år man valde finns med i bokningslistan så skrivs den specifika bokningen ut.
                {
                    found = true;
                    Console.WriteLine($"This is the bookings from {correctYear}");
                    Console.WriteLine($"Bookingname: {boc.BookedPremises.Name}, Booked from: {boc.StartDate} To: {boc.EndDate}");
                }
            }
            Console.ReadLine();

            if (!found)
            {
                Console.WriteLine($"Couldn't find anything from {correctYear}");
                Console.ReadLine();
            }
        }


        public void UpdateBooking()
        {

        }
    }
}
