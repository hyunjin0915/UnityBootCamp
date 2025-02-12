using UnityEngine;
using System;

public class SpecialPortalEvent
{
    public event EventHandler Kill;
    int count = 0;

    public void OnKill()
    {
        CountPlus();
        if (count == 5)
        {
            count = 0;
            Kill(this, EventArgs.Empty); //�̺�Ʈ �ڵ鷯���� ȣ��
        }
        else
        {
            Debug.Log("��ų�̺�Ʈ ���� ��");
        }
    }

    public void CountPlus() => count++;
}

public class UnityEventSample : MonoBehaviour
{
    // 1. �̺�Ʈ ����
    SpecialPortalEvent specialPortalEvent = new SpecialPortalEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2. �̺�Ʈ �ڵ鷯�� �̺�Ʈ ����
        specialPortalEvent.Kill += new EventHandler(MonsterKill);

        for (int i = 0; i < 5; i++)
        {
            specialPortalEvent.OnKill(); //3. �̺�Ʈ ������ ���� ��� ���� 
        }
    }

    // �̺�Ʈ�� �߻����� �� ����� �ڵ�
    private void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("pp"); 
    }
}
