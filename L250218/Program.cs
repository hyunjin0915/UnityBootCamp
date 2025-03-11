using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace L250218
{
    internal class Program
    {
        public static int Compare(GameObject first, GameObject second)
        {
            SpriteRenderer spriteRenderer1 = first.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = second.GetComponent<SpriteRenderer>();

            //null 처리 추가
            if (spriteRenderer1 == null || spriteRenderer2 == null)
            {
                return 0;
            }

            return spriteRenderer1.OrderLayer - spriteRenderer2.OrderLayer;
        }

        static void Main(string[] args)
        {

            Engine.Instance.Init();
            Engine.Instance.setSortCompare(Compare);
            //Engine.Instance.world.sortCompare = Compare;

            Engine.Instance.Load("level01.map");
            Engine.Instance.Run();

            Engine.Instance.Quit();

        }

        
    }
}
