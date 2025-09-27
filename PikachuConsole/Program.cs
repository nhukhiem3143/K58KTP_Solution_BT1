using System;
using Pikachu; // namespace của class library

namespace PikachuConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PikachuGame g = new PikachuGame();
            g.SetBoard(PikachuGame.SampleBoard());

            Console.WriteLine("== Pikachu Console by " + g.Author() + " ==");
            Console.WriteLine("Ban choi:");
            Console.WriteLine(g.BoardToString());
            Console.WriteLine("Nhap toa do r1 c1 r2 c2 (dem tu 0). Vi du: 0 0 0 7");
            Console.Write("> ");
            string line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("Khong co du lieu nhap. Thoat chuong trinh.");
                return;
            }
            string[] parts = line.Split(' ');
            if (parts.Length < 4)
            {
                Console.WriteLine("Can nhap du 4 so.");
                return;
            }
            int r1 = Convert.ToInt32(parts[0]);
            int c1 = Convert.ToInt32(parts[1]);
            int r2 = Convert.ToInt32(parts[2]);
            int c2 = Convert.ToInt32(parts[3]);

            bool can = g.CanConnect(r1, c1, r2, c2);
            Console.WriteLine("Co noi duoc khong? " + (can ? "Yes" : "No"));
            Console.WriteLine("Thong bao: " + g.LastMessage());
            if (can)
            {
                Console.WriteLine("Xoa cap nay...");
                g.RemovePair(r1, c1, r2, c2);
                Console.WriteLine("Ban choi sau khi xoa:");
                Console.WriteLine(g.BoardToString());
            }
            Console.WriteLine("-- The End --");
            Console.WriteLine("Nhan Enter de thoat.");
            Console.ReadLine();
        }
    }
}