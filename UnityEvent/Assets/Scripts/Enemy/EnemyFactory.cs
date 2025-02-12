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
    /// Factory���� �ٷ�� �������� ���¸� return  �ϴ� �ڵ�
    /// </summary>
    /// <param name="type">������ ��ü�� ���� ǥ��</param>
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
                throw new System.Exception("���� ����");
        }
    }
}
