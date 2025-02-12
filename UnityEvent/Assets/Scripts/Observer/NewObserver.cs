using UnityEngine;

/// <summary>
/// 옵저버에 대한 관리, 활용을 진행하기 위한 인터페이스
/// </summary>
public interface Isubject
{
    void Add(NewObserver observer);
    void Remove(NewObserver observer);
    void Notify();
}

public abstract class NewObserver
{
    public abstract void OnNotify();
}

public class Observer1 : NewObserver
{
    public override void OnNotify()
    {
        Debug.Log("NewObserver action #1");
    }
}

public class Observer2 : NewObserver
{
    public override void OnNotify()
    {
        Debug.Log("NewObserver action #2");
    }
}
