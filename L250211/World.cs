using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250211
{
    public class World
    {
        public GameObject[] gameObjects = new GameObject[100];
        int useGameObjectCnt = 0;

        public void Instantiate(GameObject gameObject)
        {
            gameObjects[useGameObjectCnt] = gameObject;
            useGameObjectCnt++;

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
