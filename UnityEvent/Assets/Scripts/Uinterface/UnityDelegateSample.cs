using UnityEngine;
using System;

public class UnityDelegateSample : MonoBehaviour
{
    Action action;
    Action<int> action2;
    Func<int> func01;
    Func<int, int, int> func02;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action = Sample;
        action();

        action2 = Sample;

        func01 = () => 10;
        Func<int> test = () => 25;

        func02 = (a, b) => (a + b);

        func02 = (a, b) =>
        {
            if (a > b)
            {
                a = b;
            }
            return a + b;
        };
    }
    public void Sample()
    {

    }
    public void Sample(int a)
    {

    }
    public void Sample(string s, int a)
    {

    }
}
