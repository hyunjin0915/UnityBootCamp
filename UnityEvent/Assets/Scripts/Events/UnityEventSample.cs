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
            Kill(this, EventArgs.Empty); //이벤트 핸들러들을 호출
        }
        else
        {
            Debug.Log("ㅋ킬이벤트 진행 중");
        }
    }

    public void CountPlus() => count++;
}

public class UnityEventSample : MonoBehaviour
{
    // 1. 이벤트 정의
    SpecialPortalEvent specialPortalEvent = new SpecialPortalEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2. 이벤트 핸들러에 이벤트 연결
        specialPortalEvent.Kill += new EventHandler(MonsterKill);

        for (int i = 0; i < 5; i++)
        {
            specialPortalEvent.OnKill(); //3. 이벤트 진행을 위해 기능 진행 
        }
    }

    // 이벤트가 발생했을 때 실행될 코드
    private void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("pp"); 
    }
}
