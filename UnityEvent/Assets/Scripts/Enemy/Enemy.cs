using UnityEngine;

public interface Enemy 
{
    void Action();
}

public class Goblin : Enemy
{
    public void Action()
    {
        Debug.Log("고블린이 공격을 합니다.");
    }
}

public class Slime : Enemy
{
    public void Action()
    {
        Debug.Log("Slime 공격을 합니다.");
    }
}

public class Wolf : Enemy
{
    public void Action()
    {
        Debug.Log("Wolf 공격을 합니다.");
    }
}
