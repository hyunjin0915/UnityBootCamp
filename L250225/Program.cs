using System.Collections;

namespace L250225
{
    internal class Program
    {
        static int[] arr = { 1, 2, 3, 4, 5 };
        static IEnumerable GetNumbers()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return arr[i];
            }
        }

        class CustomException : Exception
        {
            public CustomException(string message) : base(message)
            {
                
            }
        }
        static void Main2(string[] args)
        {
            try
            {
                int num = 5;
                if(num == 5)
                    throw new CustomException ("내가 만든 거");
              
            }
            catch (Exception e) 
            {

            }

            DynamicArray<int> myArray = new DynamicArray<int>();
            myArray.Add(1);
            myArray.Add(2);
            myArray.Add(3);
            myArray.Add(4);
            myArray.Add(5);
            myArray.Add(6);

            myArray.RemoveAt(0);

            for (int i = 0; i < myArray.Count; i++)
            {
                Console.WriteLine(myArray[i]);
            }
        }

        public abstract class Animal
        {
            public abstract void Eat();
            public int legs;
            
        }

        class Tiger : Animal
        {
            public override void Eat()
            {
                throw new NotImplementedException();
            }
        }

        class Lion : Animal
        {
            public override void Eat()
            {
                throw new NotImplementedException();
            }
        }

        static void Main(string[] args)
        {
            /*Lion lion = new Lion();
            Type type = lion.GetType();
            if(Type.GetType(lion)==type.GetInterface("Animal"))
            {
                
            }*/

            DynamicArray<int> myArray = new DynamicArray<int>();
            myArray.Add(1);
            myArray.Add(2);
            myArray.Add(3);
            myArray.Add(4);
            myArray.Add(5);
            myArray.Add(6);

            myArray.RemoveAt(0);


            foreach(var i in myArray)
            {
                Console.WriteLine(i);
            }
        }
    }
}

