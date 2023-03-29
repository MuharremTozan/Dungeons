using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    //For data saving
    [Serializable]
    internal class Player
    {
        public string name;
        public int id;
        public int gold = 0;
        public int level = 1;
        public int xp = 0;
        public int health = 10;
        public int damage = 1;
        public int armorValue = 0;
        public int potion = 5;
        public int weaponValue = 1;
        public int killedEnemyCount = 0;
        public int modifier = 0;
        public int deadEnemy = 0;
        //modifier for diff

        public enum PlayerClass
        {Mage, Archer, Warrior}

        public PlayerClass currentClass = PlayerClass.Warrior;
        //Generic class is Warrior

        public int GetHealth()
        {
           int upper = (2 * modifier + 5);
           int lower = (modifier + 2);
           return Program.rand.Next(lower, upper);
        }
        public int GetPower()
        {
           int upper = (2 * modifier + 2);
           int lower = (modifier + 1);
           return Program.rand.Next(lower, upper);
        }
        public int GetGold()
        {
           int upper = (15 * modifier + 50);
           int lower = (10 * modifier + 10);
           return Program.rand.Next(lower, upper);
        }

        public int GetXp()
        {
            int upper = (20 * modifier + 50);
            int lower = (15 * modifier + 10);
            return Program.rand.Next(lower, upper);
        }

        public int GetLevelUpValue()
        {
            return 100 * level + 400;
        }

        public bool CanLevelUp()
        {
            if (xp >= GetLevelUpValue())
                return true;
            else
                return false;
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                xp -= GetLevelUpValue();
                level++;
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Program.Print("Congrats! You are now level " +level+ "!");
            Console.ResetColor();
        }
    }
}
