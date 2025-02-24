namespace L250224_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int[] arr1 = { 9, 20, 28, 18, 11 };
            int[] arr2 = { 30, 1, 21, 17, 28 };

            int[] result = new int[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = arr1[i] | arr2[i];
                Console.WriteLine(result[i]);
            }

            int bitMask = 0b00000001;

            for (int i = 0; i < n; i++)
            {
                bitMask = 1 << (n - 1);
                //Console.WriteLine(Convert.ToString(bitMask,2));
                for (int j = 0; j < 8; j++)
                {
                    Console.Write((bitMask & result[i]));
                    //Console.Write((bitMask & result[i]) > 0 ? "#" : " ");
                    bitMask = bitMask >> 1;
                }
                Console.WriteLine();
            }
        }
    }
}
