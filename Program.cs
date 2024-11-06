using System.ComponentModel.Design;
using System.Text.Json;
using System.IO;


namespace Booking_System
{
    internal class Program
    {
        public static List<Booking> BookingList = new List<Booking>();
        public static List<Premises> PremisesList = new List<Premises>();
        private static string PremisesFile = "premises.json";

        private static void Menu()
        {
            Console.WriteLine("1. New booking");
            Console.WriteLine("2. List all bookings");
            Console.WriteLine("3. Update booking");
            Console.WriteLine("4. Delete booking");
            Console.WriteLine("5. List specific year booking.");
            Console.WriteLine("6. List all premises");
            Console.WriteLine("7. Create new premises");
            Console.WriteLine("8. Exit");
        }

        public static void Main(string[] args)
        {

            LoadPremisesFromFile(); // Ladda lokaler vid uppstart.
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
                        ListAllPremises();
                        break;
                    case "7":
                        CreateNewPremises();
                        break;
                    case "8":
                        SavePremisesToFile();
                        runProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public static void ListAllPremises()
        {

        }

        public static void CreateNewPremises()
        {

        }

        public static void LoadPremisesFromFile()
        {
            PremisesList.Add(new ClassRoom("Room 101", 30, true));
            PremisesList.Add(new ClassRoom("Room 102", 25, true));
            PremisesList.Add(new GroupRoom("Group Room A", 15, false));
            PremisesList.Add(new GroupRoom("Group Room B", 10, false));

            SavePremisesToFile();
        }

        public static void SavePremisesToFile()
        {
            var json = JsonSerializer.Serialize(PremisesList);
            File.WriteAllText(PremisesFile, json);
        }

    }
}
