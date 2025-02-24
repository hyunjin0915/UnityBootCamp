using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime = 0.0f; //���� ������ ���� �ð�
    public bool isTimeOver = false;
    public float displayTime = 0.0f; //ȭ�鿡 ǥ���ϱ� ���� �ð�

    float times = 0.0f; //���� �ð�

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
