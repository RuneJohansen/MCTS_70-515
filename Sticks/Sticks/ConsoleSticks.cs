using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sticks
{
    class ConsoleSticks : SticksGame
    {
        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine("---------------------------");
                Console.WriteLine(String.Format("1) Indtast spiller 1 ({0})", this.Player1));
                Console.WriteLine(String.Format("2) Indtast spiller 2 ({0})", this.Player2));
                Console.WriteLine("3) Start Spil");
                Console.WriteLine();
                Console.WriteLine("9) Afslut");

                ConsoleKeyInfo choice = Console.ReadKey();
                switch (choice.KeyChar)
                {
                    case '1' :
                        Console.WriteLine();
                        Console.WriteLine("Indtast navn på spiller 1");
                        this.Player1 = Console.ReadLine();
                        break;
                    case '2' :
                        Console.WriteLine();
                        Console.WriteLine("Indtast navn på spiller 2");
                        this.Player2 = Console.ReadLine();
                        break;
                    case '3' :
                        this.StartGame();
                        break;
                    case '9' :
                        Environment.Exit(0);
                        break;
                    case 'S':
                        this.ShowMenu();
                        break;
                }
            }
        }

        public override void ShowSticks()
        {
            Console.Clear();
            StringBuilder sticksLine = new StringBuilder();
            for (int i = 0; i < this.SticksCount; i++)
                sticksLine.Append("| ");
            Console.WriteLine(sticksLine.ToString());
        }
        public override void PlayerPick()
        {
            Console.WriteLine("{0}'s tur");
            Console.WriteLine("Vælg antal pinde (1, 2 eller 3) - S = Stop spil");
            if (this.ComputerOpponent && this.CurrentPlayer.ToLower() == "computer")
            {

            }
            else
            {
                Console.WriteLine(String.Format("{0} - ", this.CurrentPlayer));
                {

                }
            }

        }
        public override void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Tryk en tast...");
            Console.ReadKey();
        }
        public override void ShowWinner()
        {
            throw new NotImplementedException();
        }

    }
}
