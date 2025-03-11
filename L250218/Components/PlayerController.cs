using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class PlayerController : Component
    {
        public SpriteRenderer spriteRenderer;
        public CharacterController2D characterController;
        public override void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            characterController = GetComponent<CharacterController2D>();
        }
        private float elapseTime = 0.005f;

        public override void Update()
        {

            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_w))
            {
                spriteRenderer.spriteIndexY = 2;
                characterController.Move(0, -1);
               
                
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_a))
            {
                spriteRenderer.spriteIndexY = 0;
                characterController.Move(-1, 0);
               
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_s))
            {
                spriteRenderer.spriteIndexY = 3;
                characterController.Move(0, 1);
               
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_d))
            {
                spriteRenderer.spriteIndexY = 1;
                characterController.Move(1, 0);
               
            }

            
        }

        public void OnTriggerEnter(Collider2D other)
        {
            if(other.gameObject.name == "Goal")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().isFinish = true;
                Console.WriteLine("골 성공");
            }
            if (other.gameObject.name == "Monster")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver = true;
            }

            //Console.WriteLine("겹침 감지" + other.gameObject.name);
        }

    }
}
