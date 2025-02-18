using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218_2
{
    class DynamicArray
    {
        public DynamicArray()
        {

        }
        ~DynamicArray() { }


        protected int count = 0;
        public void Add(Object inObject)
        {
            if (count >= objects.Length)
            {
                Object[] newObject = new Object[objects.Length*2];
                for (int i = 0; i < objects.Length; i++)
                {
                    newObject[i] = objects[i];
                }
                objects = newObject;
            }
            objects[count] = inObject;
            count++;

        }
        public bool Remove(Object removeObject)
        {
            for (int i = 0; i < Count; i++)
            {
                if(objects[i] == removeObject)
                {
                    return RemoveAt(i);
                }
            }
            return false;
        }
        public bool RemoveAt(int index)
        {
            if(index >= 0 && index < Count)
            {
                for (int i = index; i < Count-1; i++)
                {
                    objects[i] = objects[i + 1];
                }
                this.Count--;
                return true;
            }
            return false;
        }
        public void Insert(int insertIndex, Object value)
        {
            this.Add(0);
           
            for (int i = Count; i > insertIndex; i--)
            {
                objects[i] = objects[i-1];
            }
            
            objects[insertIndex + 1] = value;

        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        protected Object[] objects = new Object[3];

        public Object this[int index]
        {
            get
            {
                return objects[index];
            }
            set
            {
                if (index < objects.Length)
                {
                    objects[index] = value;
                    //중간에 값 변경할 때 사용
                }
            }
        }


    }
    internal class Program
    {
        static public void Print<T>(T[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
        }
        static void Main(string[] args)
        {

            DynamicArray a = new DynamicArray();
            for (int i = 0; i < 10; i++)
            {
                a.Add(i);
            }

            a.RemoveAt(3);
            //a.Insert(2, 5);
            for (int i = 0; i < a.Count; i++)
            {
                Console.WriteLine(a[i]);
            }
        }
    }

}
