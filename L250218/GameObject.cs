using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class GameObject
    {
        public GameObject() { }
        public int X, Y;
        public char Shape;
        public int OrderLayer;
        public bool isTrigger = false;
        public bool isCollide = false;

        public bool PredictionCollection(int newX, int newY)
        {
            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; i++)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide)
                {
                    if ((Engine.Instance.world.GetAllGameObjects[i].X == newX) &&
                        (Engine.Instance.world.GetAllGameObjects[i].Y == newY))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public virtual void Update()
        {

        }
        public virtual void Render()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Shape);
        }

    }
}
