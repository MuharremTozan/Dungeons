using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dungeons
{
    internal class Program
    {
        public static Random rand = new Random();
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            //Emoji
            Console.OutputEncoding = Encoding.Unicode;

            //Start Message
            Time();
            Console.ReadKey();

            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }

            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();


            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }

        }

        static Player NewStart(int i)
        {
            
            Console.Clear();
            Player p = new Player();
            Print("Welcome to the Game!");
            Print("(if you remember)Enter Name:");
            p.name = Console.ReadLine();
            Print("Class : \U0001F49AMage --  👟Archer -- ⚔️Warrior");

            bool flag = false;
            while (flag == false)
            {
                flag = true;
                string input = Console.ReadLine().ToLower();
                if (input == "mage")
                    p.currentClass = Player.PlayerClass.Mage;
                else if (input == "archer")
                    p.currentClass = Player.PlayerClass.Archer;
                else if (input == "warrior")
                    p.currentClass = Player.PlayerClass.Warrior;
                else
                {
                    flag = false;
                    Print("Please choose a existing class!");
                }
            }

            p.id = i;
            Console.Clear();

            Print("You awake in a cold, stone, dark room.");
            Print("You feel dazed and are having trouble remembering anything about your past.");

            if (p.name == "")
            {
                Print("You can't even remember your own name...");
                p.name = "Hero";
            }
            else
                Print("You know your name is " + p.name);

            Console.ReadKey();
            Console.Clear();

            Print("You grope around in the darkness until you find a door handle.");
            Print("You feel some resistance as you turn the handle, but the rusty lock breaks with little effort.");
            Print("You see your captor standing with his back to you outside the door.");

            return p;
        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }

        public static void Save()
        {
            BinaryFormatter binform = new BinaryFormatter();
            string path = "saves/" + currentPlayer.id.ToString() + ".level";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binform.Serialize(file, currentPlayer);
            file.Close();
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Print("Choose your player:");

                foreach (Player p in players)
                {
                    Print(p.id + " : " + p.name);
                }

                Print("Please input player name or id  (id:# or playername)");
                Print("-Additionally, 'create' will start a new save!");

                string[] data = Console.ReadLine().Split(':');

                try
                {
                    //Checking if there is an id to which the entered id can be matched
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Print("There is no player with that id!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Print("Your id needs to be a number! Press any key to continue!");
                            Console.ReadKey();
                        }
                    }

                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;

                    }

                    //Checking for name
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Print("There is no player with that name!");
                        Console.ReadKey();

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Print("Your id needs to be a number! Press any key to continue!");
                    Console.ReadKey();
                }
            }
        }
        
        public static void Print(string text, int speed = 1)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
        }

        public static void ProgressBar(string fillerChar, string backGroundChar, decimal value, int size)
        {
            int dif = (int)(value * size);
            for (int i = 0; i < size; i++)
            {
                if(i < dif)
                    Console.Write(fillerChar);
                else
                    Console.Write(backGroundChar);
            }
        }

        public static void Time()
        {
            TimeSpan nowTime = DateTime.Now.TimeOfDay;
            int time = Convert.ToInt32(nowTime.TotalHours);

            if (time > 22 && time < 6)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Print("Good Night!", 100);
                Console.ResetColor();
            }
            else if (time > 5 && time < 13)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Print("Good Morning!", 100);
                Console.ResetColor();
            }
            else if (time > 12 && time < 18)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Print("Good Noons!", 100);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Print("Good Evening!", 100);
                Console.ResetColor();
            }
        }

        

    }
}