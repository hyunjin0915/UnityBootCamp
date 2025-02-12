using UnityEngine;

public class UsingFactory : MonoBehaviour
{
    EnemyFactory enemyFactory = new EnemyFactory();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy enemy = enemyFactory.Create(EnemyFactory.ENEMYTPE.Goblin);
        enemy.Action();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
