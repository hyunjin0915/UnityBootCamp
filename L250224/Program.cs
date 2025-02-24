namespace L250224
{
    internal class Program
    {
        /*class BitArray32
        {
            public uint Data;

            public void Set(int position)
            {
                if (position > 0 && position <= 32)
                {
                    Data = Data | (uint)(1 << (position - 1));
                }
            }

            public void UnSet(int position)
            {
                if (position > 0 && position <= 32)
                {
                    Data = Data & ~(uint)(1 << (position - 1));
                }
            }
            public bool Check(uint other)
            {
                return (Data & other > 0) ? true : false;
            }
        }*/
        static void Main(string[] args)
        {
            /*BitArray32 bitArray = new BitArray32();
            //00000000 0101
            bitArray.Set(3);
            bitArray.Set(1);
            Console.WriteLine(bitArray.Data);

            bitArray.UnSet(1);
            Console.WriteLine(bitArray.Data);*/

            byte a = 0;
            a = 1 << 7;
            Console.WriteLine(Convert.ToString(a,2));
        }
    }
}
