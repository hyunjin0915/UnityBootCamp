using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public Text message; //Ÿ������ �ؽ�Ʈ
    [SerializeField] [TextArea] private string content; //����� ����
    [SerializeField] private float delay = 0.2f; //�д� �ӵ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OnMessageButtonClick()
    {
        StartCoroutine(Typing());
    }

    //2��� ���
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
        message.text = ""; //���� ȭ���� �޼����� ����
        int typingCnt = 0; //Ÿ���� ī��Ʈ�� 0���� ����

        //���� ī��Ʈ�� �������� ���̿� �ٸ��ٸ�
        while (typingCnt != content.Length)
        {
            if(typingCnt < content.Length)
            {
                //message.text += content.Substring(typingCnt);
                message.text += content[typingCnt];
                //���� ī��Ʈ�� �ش��ϴ� �ܾ� �ϳ��� �޼��� �ؽ�Ʈ UI�� ����
                typingCnt++; //ī��Ʈ�� 1 ����
                yield return new WaitForSeconds(delay);
            }
        }
        

    }
}
