using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Player
    {
        public int hp;
        public int attack;
        public int gold;
        public bool isAlive;

        public Player(int hp, int attack, int gold, bool isAlive)
        {
            this.hp = hp;
            this.attack = attack;
            this.gold = gold;
            this.isAlive = isAlive;
        }
        ~Player()
        {
            Console.WriteLine("플레이어 소멸자");
        }

        public void Attack(Monster monster)
        {
            monster.Hp -= attack;
            gold += monster.monGold;

            if(monster.Hp < 0)
            {
                monster.isAlive = false;
            }
        }
        public void Move()
        {

        }
        public void GetGold(int _gold)
        {
            gold += _gold;
        }
    }
}
