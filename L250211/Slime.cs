using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Slime : Monster
    {
        public Slime()
        {
        }
        public Slime(int _hp, bool _isAlive, int _attack, int _monGold) : base(_hp, _isAlive, _attack, _monGold)
        {
        }
        ~Slime() { }

        public override void Move()
        {
            Console.WriteLine("Slide");
        }
    }
}
