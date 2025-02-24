using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class World
    {
        //public GameObject[] gameObjects =  new GameObject[100];
        List<GameObject> gameObjects = new List<GameObject>();

        public List<GameObject> GetAllGameObjects
        {
            get { return gameObjects; }
        }
        int useGameObjCnt = 0;
        public void Instantiate(GameObject gameObject)
        {
            /*gameObjects[useGameObjCnt] = gameObject;
            useGameObjCnt++;*/
            gameObjects.Add(gameObject);
        }

        internal void Update()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }

        internal void Render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Render();
            }
        }

        public void Sort()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = i+1; j < gameObjects.Count; j++)
                {
                    if (gameObjects[i].OrderLayer > gameObjects[j].OrderLayer)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }
                }
            }
        }
    }
}
