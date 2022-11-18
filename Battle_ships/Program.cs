using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_ships
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field(8,8); // set field size
            int shipCount = 2; // set how many ships
            List<Block> ships = new List<Block>();
            generateShips(ref ships, shipCount,field); // generate ships
            //foreach(Block b in ships) Console.WriteLine(b);

            while (ships.Count > 0)
            {
                Console.WriteLine("Enter your position!");
                string[] position = Console.ReadLine().Split(',');
                while (!checkEnter(position, field)) // check if player entered correctly
                {
                    
                    Console.WriteLine("Enter your position agian!");
                    position = Console.ReadLine().Split(',');
                }

                Block player = new Block(int.Parse(position[0]), int.Parse(position[1]));

                int result = getResult(ref ships, player);
                if(result == 0)
                {
                    Console.Write("You broke 1 ship, "+ships.Count);
                    if (ships.Count > 1) Console.WriteLine(" ships are left!");
                    else Console.WriteLine(" ship is left!");

                }else if(result >= 1&& result <= 2)
                {
                    Console.WriteLine("Hot!!!");
                }
                else if (result >= 3 && result <= 4)
                {
                    Console.WriteLine("Warm!!!");
                }
                else
                {
                    Console.WriteLine("Cold...");
                }
            }

            

            Console.WriteLine("Winner winner chicken dinner!");
            Console.ReadLine();
        }
        /// <summary>
        ///make sure no enter error will be happened.
        /// </summary>
        private static bool checkEnter(string[] position,Field field)
        {

            if(position.Length != 2) { //check if player entered 2 parameters
                Console.WriteLine("The command should be [number],[number]");
                return false; 
            }
            if(!int.TryParse(position[0],out int x)|| !int.TryParse(position[1],out int y)) //check if that 2 parameters are number
            {
                Console.WriteLine("The command should be [number],[number]");
                return false;
            }
            if(x < 1 || x > field.XAxis || y < 1 || y > field.YAxis) { //check if that 2 parameters are in the field
                Console.WriteLine("The size of the field is " + field.XAxis + " x " + field.YAxis + ".");
                return false; 
            }
            return true;
        }


        /// <summary>
        /// get distance between player and ships.
        /// </summary>
        private static int getResult(ref List<Block> ships,Block player)
        {
            int distance = int.MaxValue;
            for(int i = 0; i < ships.Count; i++)
            {
                Block ship = ships[i];
                if(ship == player)
                {
                    distance = 0;
                    ships.Remove(ship);
                }
                else
                {
                    int tmpDistance = ship + player;
                    distance = tmpDistance < distance ? tmpDistance : distance;
                }
            }
            return distance;
        }

        /// <summary>
        ///generate ships depends on the number of ships and the size of the field
        /// </summary>
        private static void generateShips(ref List<Block> ships, int shipsCount,Field field) {
            Random random = new Random();
            while (shipsCount > 0) {
                bool isExist = addBlockIfNotExist(ref ships,new Block(random.Next(1, field.XAxis+1), random.Next(1, field.YAxis+1)));
                if(!isExist)shipsCount--;
            }
        }

        /// <summary>
        ///make sure no ship in same position.
        /// </summary>
        private static bool addBlockIfNotExist(ref List<Block> ships, Block ship)
        {
            if(ships.FindIndex(s => s == ship) == -1)
            {
                ships.Add(ship);
                return false;
            }
            else
            {
                return true;
            }
        }

        struct Field
        {
            public Field(int xAxis,int yAxis)
            {
                XAxis = xAxis;
                YAxis = yAxis;
            }
            public int XAxis { get; set; }
            public int YAxis { get; set; }
        }

        private class Block
        {
            public Block(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public static int operator+(Block a,Block b)
            {
                return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            }
            public static bool operator ==(Block a, Block b)
            {
                return (a.X==b.X)&&(a.Y==b.Y);
            }
            public static bool operator !=(Block a, Block b)
            {
                return !(a == b);
            }
            public override string ToString()
            {
                return String.Format("postion {0},{1}", X, Y);
            }
        }
    }
}
