using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class Monster
    {
        protected int hp;
        /*public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }*/
        public int Hp
        {
            get; set;
        }
        public bool isAlive;
        public int attack;
        public int monGold;

        public Monster() 
        {
            Console.WriteLine("몬스터 생성");
        }
         ~Monster() { }

        public Monster(int _hp, bool _isAlive, int _attack, int _monGold) 
        {
            hp= _hp;
            isAlive= _isAlive;
            attack = _attack;
            monGold= _monGold;
        }

        
        
        public virtual void Move()
        {
            Console.WriteLine("monster move");
        }

        public virtual void Attack(Player _player)
        {
            _player.hp -= attack;
            if(_player.hp < 0)
            {
                _player.isAlive = false;
            }
        }
    }
}
