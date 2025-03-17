using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace L250317
{
    class GameObject
    {
        public GameObject(int inGold = 10, int inMp = 5) 
        {
            Gold = inGold;
            MP = inMp;
        }

        public int Gold;
        public int MP;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            GameObject g = new GameObject(10, 20);
            string jsonData =  JsonConvert.SerializeObject(g);
            Console.WriteLine(jsonData);

            GameObject g2 = JsonConvert.DeserializeObject<GameObject>(jsonData);
            Console.WriteLine(g2.Gold);

            string Data = "{Gold : 10, MP : 30}";

            JObject Json = JObject.Parse(Data);
            Console.WriteLine(Json.Value<int>("Gold")); 
        }
    }
}
