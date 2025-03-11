using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace L250218
{
    public class CharacterController2D : Collider2D
    {
        public void Move(int addX, int addY)
        {
            int futureX = transform.X + addX;
            int futureY = transform.Y + addY;

            foreach(var choicObject in Engine.Instance.world.GetAllGameObjects)
            {
                if (choicObject.GetComponent<Collider2D>() != null)
                {
                    if(choicObject.transform.X == futureX && choicObject.transform.Y == futureY)
                    {
                        if(choicObject.GetComponent<Collider2D>().isTrigger == true)
                        {
                            //OntriggerEnter 함수 나랑 상대랑 둘다 실행되게 
                            //상속이 아니기 때문에 Reflection을 사용해야 함
                            Object[] parameters = { choicObject.GetComponent<Collider2D>() };
                            gameObject.ExecuteMethod("OnTriggerEnter", parameters);
                            Object[] parameters2 = { gameObject.GetComponent<Collider2D>() };
                            choicObject.ExecuteMethod("OnTriggerEnter", parameters2);
                            break;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            transform.Translate(addX, addY);
        }
    }
}
