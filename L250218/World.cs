using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class World
    {
        public GameObject[] gameObjects =  new GameObject[100];
        int useGameObjCnt = 0;
        public void Instantiate(GameObject gameObject)
        {
            gameObjects[useGameObjCnt] = gameObject;
            useGameObjCnt++;
        }

        internal void Update()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Update();
            }
        }

        internal void Render()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Render();
            }
        }
    }
}
