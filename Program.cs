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
            foreach (var premises in PremisesList)
            {
                Console.WriteLine($"Room name: {premises.Name}, Max capacity: {premises.Capacity}");
            }
        }

        public static void CreateNewPremises()
        {
            Console.WriteLine("Enter name for the premises:");
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                name = Console.ReadLine();
            }

            //OrdinalIgnoreCase gör så att oavsett om användaren använder små eller stora bokstäver så kommer den att hitta namnet i listan.                                                                             
            if (PremisesList.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("A premises of that name already exists.");
                return;
            }

            Console.WriteLine("Enter capacity:");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity))
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Do you want the premises to have a projector? (yes/no):");
            string input = Console.ReadLine().ToLower();

            while (input != "yes" && input != "no")
            {
                Console.WriteLine("Invalid input. Enter yes or no:");
                input = Console.ReadLine().ToLower();
            }

            bool hasProjector = input == "yes";

            Premises newPremises;

            if (hasProjector == true)
            {
                newPremises = new ClassRoom(name, capacity, hasProjector);
            }
            else
            {
                newPremises = new GroupRoom(name, capacity, hasProjector);
            }

            PremisesList.Add(newPremises);
            Console.WriteLine($"{(hasProjector ? "Classroom" : "Group Room")} {newPremises.Name} was created!");
        }

        public static void LoadPremisesFromFile()
        {        
            try
            {
                string jsonData = File.ReadAllText(PremisesFile);

                PremisesList = JsonSerializer.Deserialize<List<Premises>>(jsonData);
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }     

            if (PremisesList.Count == 0)
            {
                PremisesList.Add(new ClassRoom("Classroom 101", 30, true));
                PremisesList.Add(new ClassRoom("Classroom 102", 25, true));
                PremisesList.Add(new GroupRoom("Group Room A", 15, false));
                PremisesList.Add(new GroupRoom("Group Room B", 10, false));
            }
        }

        public static void SavePremisesToFile()
        {
            var json = JsonSerializer.Serialize(PremisesList, new JsonSerializerOptions { WriteIndented = true }); //Gör så innehållet i filen blir mer läsbart.
            File.WriteAllText(PremisesFile, json);
        }
    }
}
