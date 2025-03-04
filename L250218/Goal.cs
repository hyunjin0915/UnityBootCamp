using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class Goal : GameObject
    {
        public Goal(int _X, int _Y, char _Shape)
        {
            X = _X;
            Y = _Y;
            Shape = _Shape;
            OrderLayer = 3;
            isTrigger = true;

            color.r = 0;
            color.g = 255;
            color.b = 0;
        }

        public override void Update()
        {
            if (Engine.Instance.player.X == X && Engine.Instance.player.Y == Y)
            {
                Engine.Instance.isRunning = false;
            }
        }
}
}
