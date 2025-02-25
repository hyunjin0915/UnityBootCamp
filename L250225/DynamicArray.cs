using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250225
{
    public class DynamicArray<T>  :  IEnumerable<T>
    {
        protected T[] dataArray;

        //public Object[] arrayList = new Object[5];
        protected int count = 0; //배열에 들어가 있는 요소 개수
        int arraySize = 5; //현재 배열 공간 개수

        public DynamicArray()
        {
            dataArray = new T[5]; 
        }
        ~DynamicArray() { }

        public int Count
        {
            get { return count; }
        }
        public T[] DataArray
        {
            get { return dataArray; }
        }


        /*public void Add(object _object)
{
int cnt = count;
cnt++;
if (cnt > arraySize)
{
arraySize *= 2;
Object[] newArray = new object[arraySize];

for (int i = 0; i < count; i++)
{
newArray[i] = arrayList[i];
}
newArray[count] = _object;
arrayList = newArray;
}
else
{
arrayList[count] = _object;
}
count++;
}*/

        public void Add(T _object)
        {
            if(count >= arraySize)
            {
                ResizeArray();
            }
            dataArray[count] = _object;
            count++;
        }
        private void ResizeArray()
        {
            arraySize *= 2;
            T[] newArray = new T[arraySize];
            for (int i = 0; i < count; i++)
            {
                newArray[i] = dataArray[i];
            }
            dataArray = newArray;
        }
        /*public void RemoveAt(T _object)
        {
            for (int i = 0; i < count; i++)
            {
                if (dataArray[i].Equals(_object))
                {
                    RemoveAt(i);
                }
            }
        }*/
        public void RemoveAt(int index)
        {
            if(index >= 0 && index < count)
            {
                for (int i = index; i < count -1; i++)
                {
                    dataArray[i] = dataArray[i + 1];
                }
                count--;
            }
            

        }
        public T this[int index]
        {
            get
            {
                return dataArray[index];
            }
            set
            {
                if (index < dataArray.Length)
                {
                    dataArray[index] = value;
                }
            }
        }

         public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return dataArray[i];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
