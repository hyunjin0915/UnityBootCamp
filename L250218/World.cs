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
                foreach (Component component in gameObjects[i].components)
                {
                    component.Update();
                }
            }
        }

        internal void Render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                SpriteRenderer spriteRenderer =  gameObjects[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.Render(); //spriterenderer 컴포넌트가 달린 오브젝트만 그리라는 뜻
                }
            }
        }

        public void Sort()
        {
            /*for (int i = 0; i < gameObjects.Count; i++)
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
            }*/
        }
    }
}
