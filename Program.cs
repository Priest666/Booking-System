using System.ComponentModel.Design;
using System.Text.Json;
using System.IO;

namespace Booking_System
{
    internal class Program
    {
        public static List<Booking> BookingList = new List<Booking>(); // Lista för att lagra alla bokningar.

        public static List<Premises> PremisesList = new List<Premises>(); // Lista för att lagra alla lokaler.

        private static readonly string PremisesFile = "premises.json"; // Filnamn för att spara och ladda lokaler.

        // Enkel meny.
        private static void Menu() 
        {
            Console.WriteLine("1. New booking");
            Console.WriteLine("2. List all bookings");
            Console.WriteLine("3. Update booking");
            Console.WriteLine("4. Delete booking");
            Console.WriteLine("5. List specific year booking.");
            Console.WriteLine("6. List all premises");
            Console.WriteLine("7. Create new premises");
            Console.WriteLine("8. Save and Exit");
        } 

        public static void Main(string[] args)
        {
            LoadPremisesFromFile(); // Laddar lokaler vid uppstart.
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
                        SavePremisesToFile(); //Sparar ner salarna.
                        runProgram = false; 
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }

        // Metod för att lista alla lokaler i PremisesList.
        public static void ListAllPremises()
        {
            Console.Clear();
            foreach (var premises in PremisesList)
            {
                Console.WriteLine($"Room name: {premises.Name}, Max capacity: {premises.Capacity}, {premises.Print()}");
            }
        }

        // Metod för att skapa en ny lokal.
        public static void CreateNewPremises()
        {
            Console.Clear();

            Console.WriteLine("Enter name for the premises:");
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name)) // Säkerställer att namnet inte är tomt.
            {
                Console.WriteLine("Name cannot be empty.");
                name = Console.ReadLine();
            }

            // Kontroll för att se om lokalen redan existerar.
            // OrdinalIgnoreCase ser till så att oavsett om användaren skriver in små eller stora bokstäver så kommer den att hitta namnet i listan.
            if (PremisesList.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("A premises of that name already exists.");
                return;
            }

            Console.WriteLine("Press 1 to make a classroom.");
            Console.WriteLine("Press 2 to make a group room.");

            int premiseChoice;
            // Validering för lokaltyp.
            while (!int.TryParse(Console.ReadLine(), out premiseChoice) || (premiseChoice != 1 && premiseChoice != 2))
            {
                Console.WriteLine("Invalid input. Enter 1 or 2:");
            }

            Premises newPremises; // Deklarerar den nya lokalen.

            // Om användaren väljer klassrum.
            if (premiseChoice == 1)
            {
                Console.WriteLine("Enter capacity:");
                int capacity;
                while (!int.TryParse(Console.ReadLine(), out capacity)) // Säkerställer att kapaciteten är ett nummer.
                {
                    Console.WriteLine("Invalid input.");
                }

                Console.WriteLine("Do you want the premises to have a projector? (yes/no):");
                string input = Console.ReadLine().ToLower();

                // Validering för projektorval.
                while (input != "yes" && input != "no")
                {
                    Console.WriteLine("Invalid input. Enter yes or no:");
                    input = Console.ReadLine().ToLower();
                }

                bool hasProjector = input == "yes"; // Sätter projektorvärdet beroende på input.

                // Skapar ett nytt klassrum.
                newPremises = new ClassRoom(name, capacity, hasProjector);
                PremisesList.Add(newPremises);

                Console.WriteLine($"Successfully created new premise by the name of: {newPremises.Name}.");
            }

            // Om användaren väljer grupprum.
            if (premiseChoice == 2)
            {
                Console.WriteLine("Enter capacity:");
                int capacity;
                while (!int.TryParse(Console.ReadLine(), out capacity)) // Säkerställer att kapaciteten är ett nummer.
                {
                    Console.WriteLine("Invalid input.");
                }

                Console.WriteLine("Do you want the premises to have a whiteboard? (yes/no):");
                string input = Console.ReadLine().ToLower();

                // Validering för whiteboardval.
                while (input != "yes" && input != "no")
                {
                    Console.WriteLine("Invalid input. Enter yes or no:");
                    input = Console.ReadLine().ToLower();
                }

                bool hasWhiteboard = input == "yes"; // Sätter whiteboard-värdet beroende på input.

                // Skapar ett nytt grupprum.
                newPremises = new GroupRoom(name, capacity, hasWhiteboard);
                PremisesList.Add(newPremises);

                Console.WriteLine($"Successfully created new premise by the name of: {newPremises.Name}.");
            }
        }

        // Metod för att ladda lokaler från en JSON-fil.
        public static void LoadPremisesFromFile()
        {
            try // Använder try catch ifall något går fel.
            {
                string jsonData = File.ReadAllText(PremisesFile); // Läser JSON-data från filen.

                PremisesList = JsonSerializer.Deserialize<List<Premises>>(jsonData); // Deserialiserar JSON till listan.
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }

            // Default salar.
            if (PremisesList.Count == 0)
            {
                PremisesList.Add(new ClassRoom("Classroom 1", 30, true));
                PremisesList.Add(new ClassRoom("Classroom 2", 25, false));
                PremisesList.Add(new GroupRoom("Group Room A", 15, true));
                PremisesList.Add(new GroupRoom("Group Room B", 10, false));
            }
        }

        // Metod för att spara lokaler till en JSON-fil.
        public static void SavePremisesToFile()
        {
            var json = JsonSerializer.Serialize(PremisesList, new JsonSerializerOptions { WriteIndented = true }); // Serialiserar till JSON och WriteIndented gör filen mer läsbar.
            File.WriteAllText(PremisesFile, json); // Sparar JSON till filen.
        }
    }
}
