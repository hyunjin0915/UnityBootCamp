using UnityEngine;

public class DelegateSample : MonoBehaviour
{
    
    delegate void DelegateTest();
    delegate string DelegateText(float x);
    delegate int DelegateInt(int x, int y);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Delegate ���
        //Delegate�� ������ = new Delegate��(�Լ���) <- �Լ� �̸��� �޸𸮿� ����Ǿ� �ֱ� ������ ������
        DelegateTest dt = new DelegateTest(Attack);

        //�Լ�ó�� ȣ��
        dt();

        //���� ����
        dt = Guard;

        dt();
        //�ϳ��� �̸����� ���� ��� ��� ����


    }

    void Attack() => Debug.Log("����");
    void Guard() => Debug.Log("���");
    void MoveLeft() => Debug.Log("���� �̵�");

}
