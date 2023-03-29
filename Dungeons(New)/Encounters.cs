using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    internal class Encounters
    {
        static Random rand = new Random();
        //Encounter Generic

        //Encounter
        public static void FirstEncounter()
        {
            Program.Print("You throw open the door and grab a rusty metal sword while charging toward your captor.");
            Program.Print("He turns...");
            Console.ReadKey();
            Combat(false, "👨‍Human Rogue", 1, 4);
        }

        public static void BasicFightEncounter()
        {
            Console.WriteLine("You turn the corner and there you see a hulking beast...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Console.WriteLine("The door slowly creaks open as you peer into the dark room.");
            Console.WriteLine("You see a tall man with a long beard looking at a large tome.");
            Console.ReadKey();
            Combat(false, "🧙‍Dark Wizard", 4, 2);
        }

        //Encounter Tools
        public static void RandomEncounter()
        {
            switch (rand.Next(0, 6))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    BasicFightEncounter();
                    break;
                case 0:
                    WizardEncounter();
                    break;
            }
        }
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }

            Console.Clear();
            Program.Print(n);
            Program.Print("Health: " + h + " Power: " + p);
            Console.WriteLine("=================================");
            Console.WriteLine("|  ⚔️(A)ttack       🛡️(D)efend  |");
            Console.WriteLine("|  🛒(R)un(&Shop)   💚(H)eal    |");
            Console.WriteLine("=================================");
            Program.Print("Health : " + Program.currentPlayer.health + " Weapon Strength : " + Program.currentPlayer.weaponValue + " Potions : " + Program.currentPlayer.potion);
            Console.Clear();

            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine("Health: " + h + " Power: " + p);
                Console.WriteLine("=================================");
                Console.WriteLine("|  ⚔️(A)ttack       🛡️(D)efend  |");
                Console.WriteLine("|  🛒(R)un(&Shop)   💚(H)eal    |");
                Console.WriteLine("=================================");
                Console.WriteLine("Health : " + Program.currentPlayer.health + " Weapon Strength : " + Program.currentPlayer.weaponValue + " Potions : " + Program.currentPlayer.potion);
                string input = Console.ReadLine();

                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Program.Print("You are battling to ⚔️ " + n);
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = Program.currentPlayer.weaponValue + rand.Next(1, 4) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Warrior)? 3:0);
                    Program.Print("You lose 💔" + damage + " health and deal ⚔️" + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Program.Print("You are defending to 🛡️ " + n);
                    int damage = (p / 4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = Program.currentPlayer.weaponValue / 4 + ((Program.currentPlayer.currentClass == Player.PlayerClass.Warrior) ? 3 : 0);
                    Program.Print("You lose 💔" + damage + " health and deal ⚔️" + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Archer && rand.Next(0, 2) == 0)
                    {
                        Program.Print("👟You are running from " + n);
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Program.Print("You lose 💔" + damage + " health and are unable to escape.");
                        Program.currentPlayer.health -= damage;
                    }
                    else
                    {
                        Program.Print("👟You use your crazy ninja moves to evade the " + n + " and you successfully escape!");
                        Console.ReadKey();
                        // go to store
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potion < 1)
                    {
                        Program.Print("As you desperatly grasp for a potion in your bag, all that you feel are empty glass flasks");
                        Program.Print(n + " laughing to you.");

                    }
                    else
                    {
                        Program.Print("💚You drink a speacial life potion!");
                        int potionV = rand.Next(3, 7) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Mage)? 3:0);
                        Program.currentPlayer.health += potionV;
                        Program.currentPlayer.potion -= 1;

                    }
                }
                if (Program.currentPlayer.health < 1)
                {
                    //Death Code
                    Console.ReadKey();
                    Console.Clear();
                    Program.Print("💀YOU DIED💀");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
                Console.Clear();
            }

            //After battle
            int g = Program.currentPlayer.GetGold();
            int x = Program.currentPlayer.GetXp();
            Console.WriteLine("As you stand victorious over the " + n + ", its body dissolves into 💰" + g + " gold coins!");
            Console.WriteLine("You have gained ⚡" + x + " XP!");
            Program.currentPlayer.gold += g;
            Program.currentPlayer.xp += x;
            
            Program.currentPlayer.killedEnemyCount += 1;
            Program.currentPlayer.deadEnemy += 1;
            if (Program.currentPlayer.killedEnemyCount == 20)
            {
                Program.currentPlayer.killedEnemyCount = 0;
                Program.currentPlayer.modifier++;
            }

            if (Program.currentPlayer.CanLevelUp())
                Program.currentPlayer.LevelUp();
            
            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 9))
            {
                case 0:
                    return "🦴Skeleton";
                case 1:
                    return "🧟Zombie";
                case 2:
                    return "🧙Human Cultist";
                case 3:
                    return "💰Grave Robber";
                case 4:
                    return "👹Ogre";
                case 5:
                    return "🧌Troll";
                case 6:
                    return "🧝Elven Archer";
                case 7:
                    return "🧛🏻Vampire‍";
                case 8:
                    return "🗡Assasin";
            }
            return "👨Human Rogue";
        }
    }
}
