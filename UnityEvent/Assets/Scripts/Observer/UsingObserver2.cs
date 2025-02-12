using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//인터페이스 Isubject를 기반으로 설계하는 옵저버 사용 예제
public class UsingObserver2 : MonoBehaviour, Isubject
{
    List<NewObserver> observers = new List<NewObserver>();
    public void Add(NewObserver observer)
    {
         observers.Add(observer);
    }

    public void Notify()
    {
        //옵저버 묶음에 있는 모든 옵저버를 대산으로 OnNotify() 실행
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify();
        }
    }

    public void Remove(NewObserver observer)
    {
        if(observers.Count>0)
        {
            observers.Remove(observer); //리스트의 add 와 remove 기능을 사용하는 것
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var observer1 = new Observer1();
        var observer2 = new Observer2();

        Add(observer1);
        Add(observer2);

        Notify();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
