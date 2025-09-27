// Program.cs
using System;
using Pikachu;

class Program
{
    static void Main()
    {
        // create game and set sample board
        PikachuGame g = new PikachuGame();
        int[,] board = PikachuGame.SampleBoard();
        g.SetBoard(board);

        Console.WriteLine("== Pikachu Console by " + g.Author() + " ==");
        Console.WriteLine("Board:");
        Console.WriteLine(g.BoardToString());
        Console.WriteLine("Enter coordinates r1 c1 r2 c2 (0-based). Example: 0 0 0 7");
        Console.Write("> ");
        string line = Console.ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            Console.WriteLine("No input. Exiting.");
            return;
        }
        string[] parts = line.Split(' ');
        if (parts.Length < 4)
        {
            Console.WriteLine("Need 4 numbers.");
            return;
        }
        int r1 = Convert.ToInt32(parts[0]);
        int c1 = Convert.ToInt32(parts[1]);
        int r2 = Convert.ToInt32(parts[2]);
        int c2 = Convert.ToInt32(parts[3]);

        bool can = g.CanConnect(r1, c1, r2, c2);
        Console.WriteLine("Can connect? " + (can ? "YES" : "NO"));
        Console.WriteLine("Message: " + g.LastMessage());
        if (can)
        {
            Console.WriteLine("Removing pair...");
            g.RemovePair(r1, c1, r2, c2);
            Console.WriteLine("Board after:");
            Console.WriteLine(g.BoardToString());
        }
        Console.WriteLine("-- End --");
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}
