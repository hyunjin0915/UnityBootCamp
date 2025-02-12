using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//�������̽� Isubject�� ������� �����ϴ� ������ ��� ����
public class UsingObserver2 : MonoBehaviour, Isubject
{
    List<NewObserver> observers = new List<NewObserver>();
    public void Add(NewObserver observer)
    {
         observers.Add(observer);
    }

    public void Notify()
    {
        //������ ������ �ִ� ��� �������� ������� OnNotify() ����
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify();
        }
    }

    public void Remove(NewObserver observer)
    {
        if(observers.Count>0)
        {
            observers.Remove(observer); //����Ʈ�� add �� remove ����� ����ϴ� ��
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
