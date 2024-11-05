using System.ComponentModel.Design;

namespace Booking_System
{
    internal class Program
    {
        public static List<Booking> BookingList = new List<Booking>();

        private static void Menu()
        {
            Console.WriteLine("1. New booking");
            Console.WriteLine("2. List all bookings");
            Console.WriteLine("3. Update booking");
            Console.WriteLine("4. Delete booking");
            Console.WriteLine("5. List specific year booking.");
            Console.WriteLine("6. Exit");
        }

        static void Main(string[] args)
        {
            Booking booking = new Booking();
            bool runProgram = true;

            while (runProgram)
            {
                Menu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        booking.NewBooking();
                        break;
                    case "2":
                        booking.ListAllBookings();
                        break;
                    case "3":
                        booking.UpdateBooking();
                        break;
                    case "4":
                        booking.DeleteBooking();
                        break;
                    case "5":
                        booking.ListYear(); 
                        break;
                    case "6":
                        runProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
       
    }
}
