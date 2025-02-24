using System;
using UnityEngine;


public class Monster : Character
{
    public float monster_speed;
    public float rate = 0.5f; //�Ÿ� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        transform.LookAt(Vector3.zero); //���� �������� �ü� ����
        float target_distance = Vector3.Distance(transform.position, Vector3.zero);//���� ����
        if(target_distance <= rate) //������ ��������� �̵� ����
        {
            SetMotionChange("isMOVE", false);
        }
        else //�Ϲ����� ��� �������� ����
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * monster_speed); //�������� �̵�
            SetMotionChange("isMOVE", true);

        }
    }

    public void MonsterSample()
    {
        Debug.Log("�׼� �׽�Ʈ, ���� ����");
    }
}
