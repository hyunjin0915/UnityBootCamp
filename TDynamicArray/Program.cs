namespace TDynamicArray
{
    class TDynamicArray<T> : IComparable
    {
        public TDynamicArray()
        {

        }
        ~TDynamicArray() { }


        protected int count = 0;
        public void Add(T inObject)
        {
            if (count >= objects.Length)
            {
                T[] newObject = new T[objects.Length * 2];
                for (int i = 0; i < objects.Length; i++)
                {
                    newObject[i] = objects[i];
                }
                objects = newObject;
            }
            objects[count] = inObject;
            count++;

        }
        public bool Remove(T removeObject)
        {
            for (int i = 0; i < Count; i++)
            {
                if (removeObject.Equals(objects[i]))
                {
                    return RemoveAt(i);
                }
            }
            return false;
        }
        public bool RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count - 1; i++)
                {
                    objects[i] = objects[i + 1];
                }
                this.Count--;
                return true;
            }
            return false;
        }
        public void Insert(int insertIndex, T value)
        {
           // Add(0);

            for (int i = Count; i > insertIndex; i--)
            {
                objects[i] = objects[i - 1];
            }

            objects[insertIndex + 1] = value;

        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        protected T[] objects = new T[3];

        public T this[int index]
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
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
        }
        static void Main(string[] args)
        {

            TDynamicArray<int> a = new TDynamicArray<int>();
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
