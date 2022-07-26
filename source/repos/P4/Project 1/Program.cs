using Project_1.DB;


var context = new MyDbXontext();

if (!context.PracownicyDbSet.Any())
{
    PostQueries.FillEmployees(context);
    //Thread.Sleep(4000);
    PostQueries.FillCars(context);
    //Thread.Sleep(4000);
}

Console.WriteLine("Ewidencja samochodów w firmie");

short userChoise;
String keyWord;
do
{
    Console.WriteLine("Wpisz 1 by zobaczyć niezakończone wyjazdy");
    Console.WriteLine("Wpisz 2 by zobaczyć dostępne samochody");
    Console.WriteLine("Wpisz 3 by wyszukać pracownika po nazwisku");
    Console.WriteLine("Wpisz 4 by wypożyczyć samochód");
    Console.WriteLine("Wpisz 5 by zwrócić samochód");
    Console.WriteLine("Wpisz 6 by wyszukać niezakończone podróże pracownika");
    Console.WriteLine("Wpisz 9 by zamknąć aplikację\n");

    userChoise = Convert.ToInt16(Console.ReadLine());
    switch (userChoise)
    {
        case 1:
            Console.WriteLine("Niezakończone wyjazdy: \n");
            GetQueries.GetActiveTrips(context);
            Console.WriteLine("\n\n");
            break;
        case 2:
            Console.WriteLine("Dostępne samochody: \n");
            GetQueries.GetAvailableCars(context);
            Console.WriteLine("*****************\n\n");
            break;
        case 3:
            Console.WriteLine("Podaj nazwisko pracownika: ");
            keyWord = Console.ReadLine()!.Trim();
            GetQueries.GetEmployeeByLastname(context, keyWord);
            Console.WriteLine("\n\n");
            break;
        case 4:
            Console.WriteLine("Zaczynamy rejestrację nowej podróży \n Proszę wpisać ID pracownika");
            int podaneID = int.Parse(Console.ReadLine().Trim());
            PostQueries.takeCar(context, ValidateInputData.checkIfEmployeeExistById(context, podaneID));
            Console.WriteLine("Samochód został wypożyczony\n\n");
            break;
        case 5:
            Console.WriteLine("Podaj ID pracownika który chce zwrócić samochód");
            int employeeID = ValidateInputData.checkIfEmployeeExistById(context, int.Parse(s: Console.ReadLine().Trim()));
            Console.WriteLine("Podaj stan licznika");
            var currentCounterState = int.Parse(Console.ReadLine().Trim());
            int tripID = GetQueries.returnTripID(context, employeeID);

            PostQueries.returnCar(context, employeeID, tripID, currentCounterState);
            Console.WriteLine("\n\n");
            break;
        case 6:
            Console.WriteLine("Podaj id pracownika");
            int id = ValidateInputData.checkIfEmployeeExistById(context, int.Parse(Console.ReadLine().Trim()));
            Console.WriteLine("opuszczamy funkcję ValidateInputData, id = " + id);
            Console.WriteLine(GetQueries.GetEmployeesActiveTrip(context, id).ToString());
            break;
        default:
            Console.WriteLine("Błędny wybór\n\n");
            break;
    }
} while (userChoise != 9);