using EntityFramework_Library.db;

SqlCommands commands = new SqlCommands();
var context = new MyDbContext();

//Wpisuje do bazy losowe dane
if (context.Authors != null && !context.Authors.Any())
{
    commands.FillAuthors(context);
    Thread.Sleep(4000);
    commands.FillBooks(context);
    Thread.Sleep(4000);
}

Console.WriteLine("Witamy w bibliotece \n");

short userChoise;
String keyWord;
do {
    Console.WriteLine("Wpisz 1 by wyszukać autora");
    Console.WriteLine("Wpisz 2 by wyszukać książki");
    Console.WriteLine("Wpisz 3 by zamknąć aplikację\n");
    
    userChoise = Convert.ToInt16(Console.ReadLine());
    switch (userChoise) {
        case 1: 
            Console.WriteLine("Podaj nazwisko autora: \n");
            keyWord = Console.ReadLine()!.Trim();
            commands.GetAuthor(context, keyWord); 
            Console.WriteLine("\n"); 
            break; 
        case 2: 
            Console.WriteLine("Podaj tytuł książki: ");
            keyWord = Console.ReadLine()!.Trim();
            commands.GetBook(context, keyWord); 
            Console.WriteLine("\n"); 
            break; 
    }
} while (userChoise != 3);