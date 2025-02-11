using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Goblin : Monster
    {
        public Goblin()
        {
            Console.WriteLine("고블린 생성자");
        }
        ~Goblin(){ }


        public Goblin(int _hp, bool _isAlive, int _attack, int _monGold) : base(_hp, _isAlive, _attack, _monGold)
        {
        }

        public override void Move()
        {
            Console.WriteLine("Goblin Walk");
        }

        
    }
}
