using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    internal class Booking : IBookable
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Premises BookedPremises { get; set; } // Den specifika lokalen som är bokad.

        // Metod för att radera en bokning.
        public void DeleteBooking()
        {
            Console.Clear();
            ListAllBookings(); // Visar alla bokningar.
            Console.WriteLine("Which booking do you want to remove?");
            string choose = Console.ReadLine();
            bool found = false;

            // Loopar bakåt genom listan för att undvika fel när man tar bort element.
            for (int i = Program.BookingList.Count - 1; i >= 0; i--)
            {
                // Kollar om det angivna namnet matchar bokningens rum.
                if (choose == Program.BookingList[i].BookedPremises.Name)
                {
                    found = true;
                    Console.WriteLine($"Removing {Program.BookingList[i].BookedPremises.Name}");
                    Program.BookingList.RemoveAt(i); // Tar bort bokningen.
                    Console.ReadLine();
                }
            }
            if (string.IsNullOrEmpty(choose)) // Ifall användarinput är null fångas det upp.
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                return;
            }
            if (!found) // Om ingen matchande bokning hittades.
            {
                Console.WriteLine();
                Console.WriteLine($"Couldn't find {choose}");
                Console.WriteLine();
                return;
                
            }
          
        }

        // Metod för att skapa en ny bokning.
        public void NewBooking()
        {
            Console.Clear();
            Console.WriteLine("Choose a premises to book\n");
            Program.ListAllPremises(); // Visar alla tillgängliga lokaler.

            Console.WriteLine("Enter the name of the premises you want to book:");
            string bookingName = Console.ReadLine();

            // Letar efter lokalen med angivet namn.
            var selectedRoom = Program.PremisesList.FirstOrDefault(p => p.Name.Equals(bookingName, StringComparison.OrdinalIgnoreCase));
            if (selectedRoom == null)
            {
                Console.WriteLine($"No premises with the name {bookingName} was found.");
                return;
            }

            // Tar emot startdatum från användaren.
            Console.WriteLine("Enter start date and time (YYYY-MM-DD) (00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid start date format. Please use YYYY-MM-DD.");
                return;
            }
            if (startDate < DateTime.Now) // Säkerställer att bokningen inte är i det förflutna.
            {
                Console.WriteLine("Can't make a booking back in time.");
                Console.ReadLine();
                return;
            }

            // Tar emot slutdatum från användaren.
            Console.WriteLine("Enter end date and time (YYYY-MM-DD) (00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid end date format. Please use YYYY-MM-DD.");
                return;
            }
            if (endDate < startDate) // Kontrollerar att slutdatumet inte är före startdatumet.
            {
                Console.WriteLine("Can't book a room that ends before it starts.");
                Console.ReadLine();
                return;
            }

            // Begränsar bokningens längd till högst 5 dagar.
            TimeSpan bookingspan = endDate - startDate;
            if (bookingspan > TimeSpan.FromDays(5))
            {
                Console.WriteLine("Can't do a booking for longer than 5 days, please adjust your booking or make several ones.");
                Console.ReadLine();
                return;
            }

            // Kollar om det valda rummet redan är bokat under den valda tidsperioden,
            // genom att se om det finns någon bokning där rumsnamnet matchar och startdatumet ligger inom en befintlig bokning.
            bool isBooked = Program.BookingList.Any(b => b.BookedPremises.Name == selectedRoom.Name && startDate >= b.StartDate && startDate < b.EndDate);
            if (isBooked)
            {
                Console.WriteLine("This room is already booked during this period.");
                return;
            }

            // Skapar och lägger till bokningen i listan.
            Booking booking = new Booking()
            {
                StartDate = startDate,
                EndDate = endDate,
                BookedPremises = selectedRoom
            };

            Program.BookingList.Add(booking);
            Console.WriteLine("A new booking has been added.");
        }

        // Metod för att lista alla bokningar.
        public void ListAllBookings()
        {
            Console.Clear();

            bool BookingExist = false;

            foreach (var booking in Program.BookingList)
            {
                BookingExist = true;
                string roomType = booking.BookedPremises is ClassRoom ? "Classroom" : "Group room";
                Console.WriteLine($"Room type: {roomType} \nName: {booking.BookedPremises.Name} \nCapacity: {booking.BookedPremises.Capacity} \nStart date: {booking.StartDate} \nEnd date: {booking.EndDate} \n");
            }
            if (!BookingExist) //if-sats för att se över om det över huvudtaget finns någon bokning att "lista" upp.
            {
                Console.WriteLine("There is nothing booked at this moment.\nYou have to book a room first in order to manage bookings.");
                Console.WriteLine();
                
            }

        }

        // Metod för att lista bokningar för ett specifikt år.
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
                    Console.WriteLine($"Bookingname: {boc.BookedPremises.Name}, Booked from: {boc.StartDate} To: {boc.EndDate}, Timespan: Days:{span.Days} Hours:{span.Hours} Minutes: {span.Minutes}");
                }
            }

            if (!found)
            {
                Console.WriteLine($"Couldn't find anything from {correctYear}");
            }
        }

        // Metod för att uppdatera en aktiv bokning.
        public void UpdateBooking()
        {
            Console.Clear();

            Console.WriteLine("Active bookings\n");
            ListAllBookings();

            // Be användaren att ange namnet på rummet vars bokning de vill uppdatera.
            Console.Write("Enter the name of the room to update booking: ");
            string roomName = Console.ReadLine();

            // Sök efter bokningen associerad med det angivna rumsnamnet, detta gör även så att det inte spelar någon roll om man skriver med stor bokstav eller ej.
            var booking = Program.BookingList.FirstOrDefault(b => string.Equals (b.BookedPremises.Name, roomName, StringComparison.OrdinalIgnoreCase));
            
            // Ser över först och främst att användarinput inte är null, felmeddelande skrivs ut.
            if (string.IsNullOrEmpty(roomName))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                return;
            }

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
                    Console.WriteLine("Enter a new end date and time (YYYY-MM-DD) (00:00) ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate))
                    {

                        if (newEndDate < newStartDate)
                        {
                            Console.WriteLine("Cant book a room that ends before it starts");
                            Console.ReadLine();
                            return;
                        }
                        TimeSpan bookingspan = newEndDate - newStartDate; // Samma som i newbooking, gör att man inte ej boka längre än 5 dagar. 
                        if (bookingspan > TimeSpan.FromDays(5))
                        {
                            Console.WriteLine("Cant do a booking for longer than 5 days, please adjust your booking or make several ones.");
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
                Console.WriteLine();
                Console.WriteLine($"No booking of {roomName} was found.");
                Console.WriteLine();
            }
            
        }
    }
}
