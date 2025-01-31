using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public Text message; //타이핑할 텍스트
    [SerializeField] [TextArea] private string content; //출력할 내용
    [SerializeField] private float delay = 0.2f; //읽는 속도

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OnMessageButtonClick()
    {
        StartCoroutine(Typing());
    }

    //2배속 기능
    public void ByTwo()
    {
        if(delay == 0.2f)
        {
            delay = 0.1f;
        }
        else
        {
            delay = 0.2f;
        }
    }
    IEnumerator Typing()
    {
        message.text = ""; //현재 화면의 메세지를 지움
        int typingCnt = 0; //타이핑 카운트를 0으로 설정

        //현재 카운트가 컨텐츠의 길이와 다르다면
        while (typingCnt != content.Length)
        {
            if(typingCnt < content.Length)
            {
                //message.text += content.Substring(typingCnt);
                message.text += content[typingCnt];
                //현재 카운트에 해당하는 단어 하나를 메세지 텍스트 UI에 전달
                typingCnt++; //카운트를 1 증가
                yield return new WaitForSeconds(delay);
            }
        }
        

    }
}
