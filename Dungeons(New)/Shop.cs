using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    internal class Shop
    {
        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int weaponP;
            int armorP;

            while (true)
            {
                potionP = 20 + 10 * p.modifier;
                armorP = 100 * (p.armorValue + 1);
                weaponP = 100 * p.weaponValue;

                Console.Clear();
                Console.WriteLine("=========Shop==========");
                Console.WriteLine(" ⚔️(W)eapon : " + weaponP + "G");
                Console.WriteLine(" 🛡️(A)rmor  : " + armorP + "G");
                Console.WriteLine(" 🧪(P)otion : " + potionP + "G");
                Console.WriteLine(" 💀(E)xit (Exit Shop)");
                Console.WriteLine(" 🚪(Q)uit (Quit Game)");
                Console.WriteLine("=======================");

                Console.WriteLine("");
                Console.WriteLine("");

                Console.WriteLine(p.name + "'s Stats");
                Console.WriteLine("========================");
                Console.WriteLine("💚Health          : " + p.health);
                Console.WriteLine("⚡Level           : " + p.level);
                Console.WriteLine("💰Gold            : " + p.gold);
                Console.WriteLine("⚔️Weapon Strength : " + p.weaponValue);
                Console.WriteLine("🛡️Armor Toughness : " + p.armorValue);
                Console.WriteLine("🧪Potions         : " + p.potion);
                Console.WriteLine("🔥Difficulty      : " + p.modifier);
                Console.WriteLine("💀Killed Enemies  : " + p.deadEnemy);

                Console.Write("⚡XP : ");
                Console.Write("[");
                Program.ProgressBar("+"," ", ((decimal)p.xp / (decimal)p.GetLevelUpValue()),14);
                Console.WriteLine("]");

                Console.WriteLine("========================");

                //Wait for input
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potion")
                {
                    TryBuy("potion", potionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponP, p);
                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorP, p);
                }
                else if (input == "q" || input == "quit")
                {
                    Program.Quit();
                }
                else if (input == "e" || input == "exit")
                    break;

            }

        }

        static void TryBuy(string item, int cost, Player p)
        {
            if (p.gold >= cost)
            {
                if (item == "potion")
                    p.potion++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;

                p.gold -= cost;
            }
            else
            {
                Program.Print("Ya don't have enough 💰gold!");
                Console.ReadKey();
            }
        }
    }
}
