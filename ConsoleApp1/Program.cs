using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
Random rnd = new Random();
List<List<int>> hiddenNumber = new List<List<int>>();
// här är "undersidan av kortet" så att säga alltså svaret
List<List<int>> guessedNumber = new List<List<int>>();
// här äe vad spelaren ser.
List<int> guessesX = new();
// gissade tal på x axeln
List<int> guessesY = new();
// gissade tal på y axeln.

int grid = 6;
// hur stor spelplanen ska vara

main();
Console.ReadLine();

void main()
{
    Start();
    // hämtar in alla startväderna till listorna
    while (true)
    {
        Map();
        Guessing();
        Guessing();
        Checker();
    }
}

void Start()
{
    System.Console.WriteLine("Vill du spela memory?");
    System.Console.WriteLine("Ja, Nej");
    string svar = Console.ReadLine();
    while (svar != "ja")
    {
        System.Console.WriteLine("Fel svar");
        svar = Console.ReadLine();
        // Checkar för att se till att du vill spela
    }
    System.Console.WriteLine("Perfekt, då har du kommit rätt!");
    Thread.Sleep(1000);
    Console.Clear();
    MapUser();
    Logic();
}

void MapUser()
{
    for (int x = 0; x < grid; x++)
    {
        guessedNumber.Add(new List<int> { });
        for (int y = 0; y < grid; y++)
        {
            guessedNumber[x].Add(0);
        }
    }
    // ritar ut vad användaren ser i början alltså en grid*grid karta 
}

void Logic()
{
    // skapar vad som är på undersidan av korten
    int reapeater = 0;
    // används för att se till att det inte finns fler än 2 av samma nummer
    int testNumber;
    // testar nummer för att se om dom finns.
    for (int x = 0; x < grid; x++)
    // skapar x axel listorna
    {
        for (int y = 0; y < grid; y++)
        {
            // skapar y axel värderna
            while (true)
            {
                reapeater = 0;
                testNumber = rnd.Next(1, (grid * grid / 2) + 1);
                // ser till så att det finns rätt antal med tal på undersidan av kortet sedan genererar den ett värde 
                for (int xTest = 0; xTest < hiddenNumber.Count; xTest++)
                {
                    // checkar för att se att värdet inte finns ngn annan stans i listan eller i ngnm annan lista
                    for (int yTest = 0; yTest < hiddenNumber[xTest].Count; yTest++)
                    {
                        if (hiddenNumber[xTest][yTest] == testNumber)
                        {
                            reapeater++;
                            // om värdet finns så lägger den till för att se till att det inte finns över 2 stycken senare
                        }
                    }
                }
                if (reapeater < 2)
                { break; }
                // ser till att det bara finns 2 av varje tal och inte mer
            }
            if (hiddenNumber.Count < x + 1)
            {
                hiddenNumber.Add(new List<int> { testNumber });
                // skapar ny lista för x axeln ifall det inte inte finns någon för det ledet sen innan och lägger in talet
            }
            else
            {
                hiddenNumber[hiddenNumber.Count - 1].Add(testNumber);
                // lägger in talet i rätt lista
            }
        }
    }
}

void Map()
{
    // ritar ut kartan efter den synliga listan med listor
    System.Console.Write("   ");
    for (int z = 0; z < grid; z++)
    {
        // ritar ut x kordinater
        System.Console.Write(z + 1 + " ");
    }
    System.Console.WriteLine();
    System.Console.WriteLine();
    for (int x = 0; x < grid; x++)
    {
        System.Console.Write(x + 1 + "  ");
        // ritar ut y kordinaterna
        for (int y = 0; y < grid; y++)
        {
            System.Console.Write(guessedNumber[x][y] + " ");
            // ritar ut kartan efter listan
        }
        System.Console.WriteLine();
    }
}

void Guessing()
{
    int guessXNr;
    int guessYNr;
    // gissningarna du kommer att göra
    while (true)
    {
        int reapeater = 0;
        System.Console.WriteLine("Tja, vilket tal vill du gissa? Skriv x kordinaten först");
        bool loop1 = false;
        // ser till att du skriver ett tal in
        while (true)
        {
            string guessX = Console.ReadLine();
            loop1 = int.TryParse(guessX, out guessXNr);
            if (loop1 == true)
            {
                if (guessXNr < 1 || guessXNr > grid)
                    System.Console.WriteLine("Talet ska finnas med på kordinatsystemet");
                else
                    break;
            }
        }
        // samma sak som föregående kod igen
        System.Console.WriteLine("Tja, vilket tal vill du gissa? Skriv y kordinaten");
        bool loop2 = false;
        while (true)
        {
            string guessY = Console.ReadLine();
            loop2 = int.TryParse(guessY, out guessYNr);
            if (loop2 == true)
            {
                if (guessYNr < 1 || guessYNr > grid)
                    System.Console.WriteLine("Talet ska finnas med på kordinatsystemet");
                else
                    break;
            }
        }
        guessesX.Add(guessXNr);
        guessesY.Add(guessYNr);
        // lägger till gissningarna i en lista så att du inte kan köra samma gissning igen om den är öppen
        for (int k = 0; k < guessesX.Count; k++)
        {
            if (guessesX[k] == guessesX[guessesX.Count - 1] && guessesY[k] == guessesY[guessesY.Count - 1])
            {
                reapeater++;
            }
            // checkar ifall du gjort samma gissning under samma runda eller om du har haft rätt på den rutan
        }
        if (reapeater == 1)
            break;
        else
            System.Console.WriteLine("skriv en kordinat du inte redan har gissat på");
    }
    guessedNumber[guessXNr - 1][guessYNr - 1] = hiddenNumber[guessXNr - 1][guessYNr - 1];
    // sätter ut rutan du gissat på in till kart listan
    Console.Clear();
    Map();
    // ritar ut kartan
    System.Console.WriteLine(guessesX.Count);
    Thread.Sleep(1000);
}
void Checker()
{
    if (hiddenNumber[guessesX[guessesX.Count - 1] - 1][guessesY[guessesY.Count - 1] - 1] != hiddenNumber[guessesX[guessesX.Count - 2] - 1][guessesY[guessesY.Count - 2] - 1])
    {
        // checkar ifall du har gissat rätt ifall det är fel tar den bort gissningen  från både guesses så du kan gissa det igen och från det synliga kordinatsystemet.
        guessedNumber[guessesX[guessesX.Count - 1] - 1][guessesY[guessesY.Count - 1] - 1] = 0;
        guessedNumber[guessesX[guessesX.Count - 2] - 1][guessesY[guessesY.Count - 2] - 1] = 0;

        if (guessesY.Count >= 2)
        {
            guessesY.RemoveAt(guessesY.Count - 1);
            guessesY.RemoveAt(guessesY.Count - 1);
        }
        else if (guessesY.Count == 1)
        {
            guessesY.RemoveAt(0);
        }
        if (guessesX.Count >= 2)
        {
            guessesX.RemoveAt(guessesX.Count - 1);
            guessesX.RemoveAt(guessesX.Count - 1);
        }
        else if (guessesX.Count == 1)
        {
            guessesX.RemoveAt(0);
        }
        Console.Clear();
    }
}