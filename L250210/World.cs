using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250210
{
    public class World
    {
        public Wall[] walls;
        public Floor[] floors;
        public Player player;
        public Monster[] monsters;
        public Goal goal;

        public World() 
        { 
            walls = new Wall[10];
            floors = new Floor[10];
            player = new Player();
            monsters = new Monster[10];
            goal = new Goal();
        }
    }

    
}
