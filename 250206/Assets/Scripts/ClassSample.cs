using UnityEngine;

class Unit
{
    //Ŭ�������� �ش� Ŭ������ ���� ��ü�� ������ �ۼ�
    public string unit_name;


    public static void UnitAction()
    {
        Debug.Log("������ �����մϴ�.");
    }

    public void Cry()
    {
        Debug.Log("����");
    }
}

public class ClassSample : MonoBehaviour
{
    Unit unit; //Unit Ŭ���� �������� ���� unit ��ü(Object)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit.unit_name = "jgkj";
        //Ŭ����������.�ʤ��� ���� Ŭ������ ������ �ִ� �ʵ�(����)�� ����� �� ����

        unit.Cry();
        
        Unit.UnitAction();
        //static�� ���� ������ �Լ��� ��ü�� �������� �ʰ� Ŭ�������� �ٷ� �� ����� ������ ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
