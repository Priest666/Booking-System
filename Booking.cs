using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class Booking : IBookable
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Premises BookedPremises { get; set; }


      
        public void DeleteBooking()
        {
            Console.Clear();
            Console.WriteLine("Which booking do you want to remove?");
            string choose = Console.ReadLine().ToLower();
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
            Console.Clear();
            Console.WriteLine("Choose a premises to book:");
            Program.ListAllPremises();

            Console.WriteLine("Enter the name of the premises you want to book:");
            string bookingName = Console.ReadLine().ToLower();

            var selectedRoom = Program.PremisesList.FirstOrDefault(p => p.Name.Equals(bookingName, StringComparison.OrdinalIgnoreCase));
            if (selectedRoom == null)
            {
                Console.WriteLine($"No premises with the name {bookingName} was found.");
                return;
            }

         
            Console.WriteLine("Enter start date and time (YYYY-MM-DD) (00:00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid start date format. Please use YYYY-MM-DD.");
                return;
            }

            Console.WriteLine("Enter end date and time (YYYY-MM-DD) (00:00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid end date format. Please use YYYY-MM-DD.");
                return;
            }

            

            Booking booking = new Booking()
            {
                StartDate = startDate,
                EndDate = endDate,
                BookedPremises = selectedRoom
            };

                Program.BookingList.Add(booking);
                Console.WriteLine("A new booking has been added");
                        
        }

        public void ListAllBookings()
        {
            Console.Clear();
            foreach (var booking in Program.BookingList)
            {
                string roomType = booking.BookedPremises is ClassRoom ? "Classroom" : "Group room";   //added variable to display chosen premise
                Console.WriteLine($"Room type: {roomType} \nName: {booking.BookedPremises.Name} \nCapacity: {booking.BookedPremises.Capacity} \nStart date: {booking.StartDate} \nEnd date: {booking.EndDate} \n");
            }
        }

        public void ListYear()
        {
            Console.Clear();
            Console.WriteLine("Select a year to watch the bookings");
            int correctYear = Convert.ToInt32(Console.ReadLine());
            bool found = false;


            foreach (Booking boc in Program.BookingList)
            {
                TimeSpan span = boc.EndDate - boc.StartDate;

                if (correctYear == boc.StartDate.Year) // Om det år man valde finns med i bokningslistan så skrivs den specifika bokningen ut.
                {
                    found = true;
                    Console.WriteLine($"This is the bookings from {correctYear}");
                    Console.WriteLine($"Bookingname: {boc.BookedPremises.Name}, Booked from: {boc.StartDate} To: {boc.EndDate}, Timespan: Days:{span.Days} Minutes:{span.Minutes} Seconds:{span.Seconds}");
                }
            }

            if (!found)
            {
                Console.WriteLine($"Couldn't find anything from {correctYear}");
            }
        }

        public void UpdateBooking()
        {
            Console.Clear();

            // Be användaren att ange namnet på rummet vars bokning de vill uppdatera.
            Console.Write("Enter the name of the room to update booking: ");
            string roomName = Console.ReadLine();

            // Sök efter bokningen associerad med det angivna rumsnamnet.
            var booking = Program.BookingList.FirstOrDefault(b => b.BookedPremises.Name == roomName);

            // Om en bokning hittas, fortsätt med uppdateringen.
            if (booking != null)
            {
                // Be användaren att ange ett nytt startdatum för bokningen.
                Console.WriteLine("Enter a new date and time (YYYY-MM-DD) (00:00) ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newStartDate))
                {
                    if (newStartDate < DateTime.Now)
                    {
                        Console.WriteLine("Can't make a booking in the past");
                        Console.ReadLine();
                        return;
                    }

                    // Be användaren att ange ett nytt slutdatum för bokningen.
                    Console.WriteLine("Enter a new enddate and time (YYYY-MM-DD) (00:00) ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate))
                    {

                        if (newEndDate < newStartDate)
                        {
                            Console.WriteLine("Cant book a room that ends before it starts");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            booking.StartDate = newStartDate; // Uppdatera startdatumet för bokningen.
                            booking.EndDate = newEndDate; // Uppdatera slutdatumet för bokningen.
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please use YYYY-MM-DD.");
                        return;
                    }

                    // Bekräfta för användaren att bokningen har uppdaterats.
                    Console.WriteLine($"The booking of {roomName} has been updated successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid input, please use: YYYY-MM-DD.");
                    return;
                }

            }
            // Om ingen bokning hittades för det angivna rumsnamnet, informera användaren.
            else
            {
                Console.WriteLine($"No booking of {roomName} was found");
            }
        }
    }
}
