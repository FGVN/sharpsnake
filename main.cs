using System;
using System.Threading;

namespace SnakeTry2
{

    class PrintBoard
    {
        public int Height = 20;
        public int Width = 30;

        private int x;
        private int y;

        public PrintBoard(int x, int y)
        {
            this.x = x;
            this.y = y;
            Print(x, y);
        }

        public void Print(int x, int y)
        {
            Console.Clear();
            HorizontalLine(x, y, Width);
            HorizontalLine(x, y + Height, Width);
            VerticalLine(x, y, Height);
            VerticalLine(x + Width, y, Height);
            FoodMaker();
        }

        public int[] FoodMaker()
        {
            Random rand = new Random();
            int randx = rand.Next(1, Width - 1);
            int randy = rand.Next(1, Height - 1);
            Console.SetCursorPosition(randx, randy);
            Console.Write("O");
            int[] AppleCords = new int [2];
            AppleCords[0] = randx;
            AppleCords[1] = randy;
            return AppleCords;
        }

        public void HorizontalLine(int x, int y, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write("#");
            }
        }

        public void VerticalLine(int x, int y, int length)
        {
            for (int i = 0; i <= length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("#");
            }
        }
    }
    class Program
    {

        public static int direction;
        public static int Height = 30;
        public static int Width = 20;


        static PrintBoard Board = new PrintBoard(0, 0);

        static void Main()
        {

            Console.SetWindowSize(Height+1, Width+1);
            int sx = Height / 2;
            int sy = Width / 2;

            int size = 4;
            Console.CursorVisible = false;

            int[,] pos = new int[size, 2];
            for (int i = 0; i < size; i++)
            {
                pos[i, 0] = sx;
                pos[i, 1] = sy - i;
            }

            int[] Cords = new int[2];
            Cords = Board.FoodMaker();

            Snakeauto(pos, size, Cords);
            //SnakeHead(sx, sy, 0);

        }

        static void Snakeauto(int[,] pos, int size, int[] Cords)
        {


            Thread.Sleep(100);



            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(pos[i, 0], pos[i, 1]);
                Console.Write("■");
            }

            if (HasAte(pos, Cords) != true)
            {
                Console.SetCursorPosition(pos[size - 1, 0], pos[size - 1, 1]);
                Console.Write(" ");
            }
            else
            {
                size++;
                int[,] bufarr = new int[size, 2];
                for (int i = 0; i < size - 1; i++)
                {
                    bufarr[i, 0] = pos[i, 0];
                    bufarr[i, 1] = pos[i, 1];
                }

                pos = new int[size, 2];

                for (int i = 0; i < size - 1; i++)
                {
                    pos[i, 0] = bufarr[i, 0];
                    pos[i, 1] = bufarr[i, 1];
                    if(Cords[0] == pos[i, 0] && Cords[1] == pos[i, 1])
                    {
                        Cords = Board.FoodMaker();
                    }
                }
            }

            int z = size - 1;
            while (z != 0)
            {
                pos[z, 0] = pos[z - 1, 0];
                pos[z, 1] = pos[z - 1, 1];
                z--;
            }

            SnakeHead(pos, size);
            if(LoseCheck(pos, size) != true)
            {
                Snakeauto(pos, size, Cords);
            }
            else
            {
                Console.Clear();
                Console.SetCursorPosition(Height / 2, Width / 2);
                Console.WriteLine("Game Over");
                Console.WriteLine("\tPush <Enter> To Play Again");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    PrintBoard Board = new PrintBoard(0, 0);
                    Main();
                }
            }
        }





        static int[,] SnakeHead(int[,] pos, int size)
        {

            if (Console.KeyAvailable == false)
            {
                switch(direction)
                {
                    case 1:
                        pos[0, 1]--;
                        break;
                    case 2:
                        pos[0, 1]++;
                        break;
                    case 3:
                        pos[0, 0]--;
                        break;
                    case 4:
                        pos[0, 0]++;
                        break;
                }
            }
            else
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = 1;
                        pos[0, 1]--;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = 2;
                        pos[0, 1]++;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = 3;
                        pos[0, 0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = 4;
                        pos[0, 0]++;
                        break;
                }

            }
            return pos;
        }

        static bool HasAte(int [,]pos, int []Cords)
        {
            if(pos[0, 0] == Cords[0] && pos[0, 1] == Cords[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool LoseCheck(int [,]pos, int size)
        {

            if(size > 4)
            {
                for (int i = 1; i < size; i++)
                {
                    if (pos[0, 0] == pos[i, 0] && pos[0, 1] == pos[i, 1])
                    {
                        return true;
                    }

                }
            }
            if (pos[0, 0] == Height || pos[0, 0] <= 0 || pos[0, 1] == Width+1 || pos[0, 1] < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}