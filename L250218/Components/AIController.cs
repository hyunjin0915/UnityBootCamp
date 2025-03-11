using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class AIController : Component
    {
        public CharacterController2D characterController2D;
        public SpriteRenderer spriteRenderer;

        private Random random = new Random();

        private float elapseTime = 0.005f;

        public override void Awake()
        {
            characterController2D = GetComponent<CharacterController2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Update()
        {
            if (elapseTime >= 500.0f)
            {
                elapseTime = 0.0f;
                int direction = random.Next() % 4;
                //int direction = 0;

                int temp = 0;
                if (direction == 0)
                {
                    characterController2D.Move(0, -1);

                }
                else if (direction == 1)
                {
                    characterController2D.Move(0, 1);

                }
                else if (direction == 2)
                {
                    characterController2D.Move(1, 0);


                }
                else if (direction == 3)
                {
                    characterController2D.Move(-1, 0);
                    
                }
            }

            else
            {
                elapseTime += Time.deltaTime;
            }

        }

        private void OnTriggerEnter(Collider2D other)
        {
            //Console.WriteLine("부딪힘!" + other.gameObject.name);
        }
    }
}
