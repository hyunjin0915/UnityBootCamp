using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public abstract class Component
    {
        public virtual void Awake()
        {

        }
        public abstract void Update();

        public GameObject gameObject;


        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in gameObject.components)
            {
                if(component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }
    }
}
