using UnityEngine;

public interface Enemy 
{
    void Action();
}

public class Goblin : Enemy
{
    public void Action()
    {
        Debug.Log("����� ������ �մϴ�.");
    }
}

public class Slime : Enemy
{
    public void Action()
    {
        Debug.Log("Slime ������ �մϴ�.");
    }
}

public class Wolf : Enemy
{
    public void Action()
    {
        Debug.Log("Wolf ������ �մϴ�.");
    }
}
