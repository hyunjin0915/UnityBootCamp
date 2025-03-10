using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class GameObject
    {
        public List<Component> components = new List<Component>();

        public bool isTrigger = false;
        public bool isCollide = false;

        public string name;
        protected static int gameObjectCount = 0;

        public Transform transform;
        public GameObject() 
        {
            Init();
            gameObjectCount++;
            name = "GameObject(" + gameObjectCount+")";
        }
        ~GameObject()
        {
            gameObjectCount--;
        }
        public void Init()
        {
            transform = AddComponent<Transform>(new Transform()); //게임오브젝트 생성되면 Transform 하나 생기도록 - 하나만 생기게 수정해야 함
        }

        public T AddComponent<T>(T inComponent) where T : Component
        {
            components.Add(inComponent);
            inComponent.gameObject = this; //해당 컴포넌트에 게임 오브젝트 정보를 넣어줘야 함
            return inComponent;
        }
        public bool PredictionCollection(int newX, int newY)
        {
            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; i++)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide)
                {
                    /*if ((Engine.Instance.world.GetAllGameObjects[i].X == newX) &&
                        (Engine.Instance.world.GetAllGameObjects[i].Y == newY))
                    {
                        return true;
                    }*/
                }
            }
            return false;
        }
        public virtual void Update()
        {
            
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }

    }
}
