using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime = 0.0f; //실제 진행할 게임 시간
    public bool isTimeOver = false;
    public float displayTime = 0.0f; //화면에 표시하기 위한 시간

    float times = 0.0f; //현재 시간

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isCountDown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimeOver)
        {
            times += Time.deltaTime;

            if (isCountDown)
            {
                displayTime = gameTime - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
        }
    }
}
