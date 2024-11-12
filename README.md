# Booking-


Bokningssystem Grupp 2 - Lista

DOKUMENTATION


○ Eventuella kända begränsningar:

Inget som vi inte har fixat eller hittat. 
○ Hur man startar och använder programmet:

När du startar programmet kommer det upp en meny med 8st val. Väljer du “1. New booking” direkt kommer du få fram 4 st förprogrammerade salar, 2 st grupprum och 2 st klassrum. Fyll i vilken sal och hur länge du vill boka den, det går ej att boka längre än 5 dygn i sträck per sal. Du kommer sedan få ett meddelande på skärmen när din bokning är bekräftad. 

Menyval 2. “List all bookings” visar alla bokningar som finns sparade, om du precis startat programmet och inte gjort en bokning kommer inte detta menyval fungera. 

Menyval 3. “Update booking”, först kommer du att få fylla i vilken bokning det är du vill boka om, om du valt att boka “classroom 101”, så skriver du även det här. Sedan får du välja vilka nya datum du vill boka om det till, även här gäller max 5 dygn. 

Menyval 4. “Delete booking”, skriv även in här vilken bokning du vill ta bort, och du kommer sedan få ett meddelande att bokningen tagits bort. Försöker du ta bort något som inte stämmer överens med vad som är bokat får du felmeddelande. Samma gäller ifall du skulle klicka enter, “null”. Och kommer tillbaka till menyn.

Menyval 5. “List specific year”, här får du fylla i vilket år du vill se om det finns en bokning. Om du skriver “2025” och det finns en eller flera bokningar då, kommer dessa att skrivas ut.

Menyval 6. “Sortpremises”, Här får du välja om du vill sortera salarna med kapaciteten stigande eller fallande. 

Menyval 7. “List all premisis”, skriver ut vilka klassrum och grupprum som är valbara för bokning. 

Menyval 8. “Create new premisis”, om man som användare vill skapa ett nytt klassrum/grupprum används denna. Först får du välja ett namn på den sal du vill skapa, och sedan göra ett val.

1. för att skapa ett klassrum
2. för att skapa ett grupprum. 

Sedan får du göra ett val på hur många platser du vill att klassrummet/grupprummet ska innehålla. Om du har valt ett klassrum får du även göra ett val på om du vill att de ska ingå en projektor, och om du valt grupprum får du välja om du vill att det ska ingå en whiteboardtavla. 

Menyval 9. “Save and exit”, detta menyval kommer spara de salar som du själv skapat och sedan avsluta programmet. 


○ Val och motiveringar för implementation:
Denna del tyckte vi var mest intressant i koden och väljer därför att lyfta den lite gällande filhanteringen. Vi upptäckte att om man startar programmet för första gången så existerar inte json filen och då kom det direkt upp ett felmeddelande att filen inte existerade. Så för att lösa problemet att användaren direkt får upp ett felmeddelande så skapades denna kodbit för att se till att filen skapas direkt när programmet körs för första gången. 

 if (!File.Exists(PremisesFile)) // Kontrollera om filen redan finns.
 {
     // Default lokaler
     PremisesList.Add(new ClassRoom("Classroom 1", 30, true));
     PremisesList.Add(new ClassRoom("Classroom 2", 25, false));
     PremisesList.Add(new GroupRoom("Group Room A", 15, true));
     PremisesList.Add(new GroupRoom("Group Room B", 10, false));

     SavePremisesToFile(); // Skapa filen och spara standardlistan.
     Console.WriteLine("Premises file created with default premises.");
 }
 else
 {
     // Om filen finns, ladda lokaler från filen.
     try // Har try catch ifall något går fel.
     {
         string jsonData = File.ReadAllText(PremisesFile);
         PremisesList = JsonSerializer.Deserialize<List<Premises>>(jsonData);
     }
     catch (Exception e)
     {
         Console.WriteLine(e.Message);
     }



Hela converter.cs är väldigt episk, tack Gustav! Klassen är där för att serializer hade svårt med att spara tex bool-värden från klasser som ärver. Så det behövdes göra en helt ny klass som hanterar det scenariot. 

○ Beskrivning av filformat och struktur:
Vi har valt att lägga alla klasser och interface i separata filer för att få en så enkel och lättöverskådlig struktur som möjligt. Det som vi har under Main i Program är endast i det menyval som användaren vill skapa en ny sal, eller spara ner programmet, eftersom den logiken kräver ingen extra klass.

○ Vilken student har huvudansvaret för vilka delar:

Alla: I början av grupparbetet satt vi alla 4 och skapade strukturen för hur vi ville ha det med alla klasser, vilka som skulle ärva från respektive klass och Interface. Sedan i slutet har även alla 4 varit delaktiga med diverse små justeringar. 

Jonatan Präst: Gjort menyn, metoderna UpdateBooking, LoadPremisisFromFile, SavePremisisToFile för att kunna spara ner det vi skapat. Tillsammans med Jacob jobbat på CreateNewPremisis samt med Thimmy och Rikard på NewBooking. 
	
Jacob Burke: Gjort metoderna ListAllBookings, ListAllPremisis samt tillsammans med Jonatan Präst jobbat på CreateNewPremisis. 

Thimmy Severin: Gjort DeleteBooking, ListYear, SortPremises samt tillsammans med Jonatan och Rikard jobbat NewBooking. 

Rikard Lundberg: Jobbat tillsammans med Thimmy och Jonatan på NewBooking, och tillsammans med Jonatan och Jacob på CreateNewPremisis.

Fredrik Wigren: Ej deltagit.
 









