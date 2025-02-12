using UnityEngine;

public class EnemyFactory 
{
    public enum ENEMYTPE
    {
        Goblin,
        Slime,
        Wolf
    }
    /// <summary>
    /// Factory에서 다루는 데이터의 형태를 return  하는 코드
    /// </summary>
    /// <param name="type">생성할 객체의 형태 표현</param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public Enemy Create(ENEMYTPE type)
    {
        switch (type)
        {
            case ENEMYTPE.Goblin:
                return new Goblin();
            case ENEMYTPE.Slime:
                return new Slime();
            case ENEMYTPE.Wolf:
                return new Wolf();
            default:
                throw new System.Exception("생성 실패");
        }
    }
}
