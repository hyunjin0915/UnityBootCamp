﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L250218
{
    public class Monster : GameObject
    {
        /*private Random random = new Random();

        private float elapseTime = 0.005f;
        public Monster(int _X, int _Y, char _Shape)
        {
            X = _X;
            Y = _Y;
            Shape = _Shape;
            OrderLayer = 5;
            isTrigger = true;

            color.r = 100;
            color.g = 100;
            color.b = 100;

            LoadBMP("Data/monster.bmp");
        }

        public override void Update()
        {
            if(elapseTime >=500.0f)
            {
                elapseTime = 0.0f;
                int direction = random.Next() % 4;
                //int direction = 0;

                int temp = 0;
                if (direction == 0)
                {
                    temp = Y - 1;
                    if (!PredictionCollection(X, Y - 1))
                    {
                        Y--;
                    }

                }
                else if (direction == 1)
                {
                    temp = Y + 1;
                    if (!PredictionCollection(X, Y + 1))
                    {
                        Y++;
                    }

                }
                else if (direction == 2)
                {
                    temp = X + 1;
                    if (!PredictionCollection(X + 1, Y))
                    {
                        X++;
                    }

                }
                else if (direction == 3)
                {
                    temp = X - 1;
                    if (!PredictionCollection(X - 1, Y))
                    {
                        X--;
                    }

                }
            }

            else
            {
                elapseTime += Time.deltaTime;
            }
            
        }
        */
    }
}
