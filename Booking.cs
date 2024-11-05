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
        
        public void DeleteBooking()
        {
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

            Premises selectedRoom = null;
            
            Console.WriteLine("What do you want to book?\n1.Classroom\n2.Grouproom");
            Console.WriteLine();

            String UserInput = Console.ReadLine();

            switch (UserInput)
            {
                case "1":

                    bool hasProjector = true;

                    Console.WriteLine("You chose to book a Classroom.");
                    Console.WriteLine();
                    Console.WriteLine("What is your name? (The booking will go under this name)");
                    Console.WriteLine();
                    String BookingName1 = Console.ReadLine().ToLower();

                    Console.WriteLine("Choose capacity: ");
                    int ClassRoomCap = int.Parse(Console.ReadLine());
                    
                    selectedRoom = new ClassRoom(BookingName1, ClassRoomCap, hasProjector);

                    break;

                case "2":
                    Console.WriteLine("You chose to book a Grouproom.");
                    Console.WriteLine();
                    Console.WriteLine("What is your name? (The booking will go under this name)");
                    Console.WriteLine();
                    String BookingName2 = Console.ReadLine();

                    Console.WriteLine("Choose capacity: ");
                    int GroupRoomCap = int.Parse(Console.ReadLine());

                    selectedRoom = new GroupRoom(BookingName2, GroupRoomCap, hasProjector = false);
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            Console.WriteLine("Select starting date (YYYY-MM-DD)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Select ending date (YYYY-MM-DD)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            Booking booking = new Booking()
            {
                StartDate = startDate,
                EndDate = endDate,
                BookedPremises = selectedRoom
            };
            
            Program.BookingList.Add(booking);
            Console.WriteLine("A new booking was added");

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

            Console.Clear();

            // Prompt the user to enter the name of the room whose booking they wish to update.
            Console.Write("Enter room name to update booking: ");
            string roomName = Console.ReadLine();

            // Search for the booking associated with the specified room name.
            var booking = Program.BookingList.FirstOrDefault(b => b.BookedPremises.Name == roomName);

            // If a booking is found, proceed with the update.
            if (booking != null)
            {
                // Ask the user to enter a new start date for the booking.
                Console.WriteLine("Enter new start date and time (YYYY-MM-DD) (00:00:00): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newStartDate))
                {
                    booking.StartDate = newStartDate; // Update the start date of the booking.
                }
                else
                {
                    Console.WriteLine("Invalid start date format. Please use YYYY-MM-DD.");
                    return; 
                }

                // Ask the user to enter a new end date for the booking.
                Console.WriteLine("Enter new end date and time (YYYY-MM-DD) (00:00:00): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate))
                {
                    booking.EndDate = newEndDate; // Update the end date of the booking.
                }
                else
                {
                    Console.WriteLine("Invalid end date format. Please use YYYY-MM-DD.");
                    return; 
                }

                // Confirm to the user that the booking update was successful.
                Console.WriteLine($"Updated booking of room {roomName} has been successful!");
            }
            // If no booking was found for the given room name, notify the user.
            else
            {
                Console.WriteLine($"No booking of {roomName} was found.");
            }
        }


    }
}
