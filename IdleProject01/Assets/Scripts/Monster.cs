using System;
using UnityEngine;


public class Monster : Character
{
    public float monster_speed;
    public float rate = 0.5f; //거리 비율

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
        transform.LookAt(Vector3.zero); //영점 기준으로 시선 변경
        float target_distance = Vector3.Distance(transform.position, Vector3.zero);//간격 설정
        if(target_distance <= rate) //간격이 가까워지면 이동 중지
        {
            SetMotionChange("isMOVE", false);
        }
        else //일반적인 경우 움직임을 진행
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * monster_speed); //영점으로 이동
            SetMotionChange("isMOVE", true);

        }
    }

    public void MonsterSample()
    {
        Debug.Log("액션 테스트, 몬스터 생성");
    }
}
