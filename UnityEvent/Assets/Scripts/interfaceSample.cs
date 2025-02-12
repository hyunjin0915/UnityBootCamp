using UnityEngine;
public interface ICountAble
{
    public int Count { get; set; }

    void CountPlus();
}
public interface IuseAble
{
    void Use();
}

class Potion : ICountAble, IuseAble
{
    int count;
    string name;
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            if (count > 99)
            {
                Debug.Log("99���ִ��Դϴ�.");
                count = 99;
            }

        }
    }

    public string Name { get => name; set => name = value; }

    public void CountPlus() => Count++;

    public void Use()
    {
        Debug.Log(Name + "�� ���");
        Count--;
    }
}

public class interfaceSample : MonoBehaviour
{
    Potion potion = new Potion();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potion.Count = 99;
        potion.Name = "���� ����";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
