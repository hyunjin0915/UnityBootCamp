using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCount : MonoBehaviour
{
    //�ؽ�Ʈ�� ī��Ʈ�� ����ϴ� ����� ����
    //ī��Ʈ�� �ʸ��� ��� 1�� �����ϴ� ���·� ó��
    [SerializeField] private Text countText;
    private int cnt = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         �ڷ�ƾ ��� ���
        StartCoroutine("�Լ� �̸�(IEnumerator ������ �Լ�)");
        1. ���ڿ��� ���� �Լ��� ã�Ƽ� �����ϴ� ���
            >> ��Ÿ�� �߻��ص� ������ �߻����� ����. ������ ����� �������X
            >> StopCoroutine()�� ���� �ܺο��� �����ϴ� ���� ����

        StartCoroutine(�Լ� �̸�());
        2. �ش� �Լ��� ȣ���� ���� ����� ��ȯ�޴� ����
            >> ��Ÿ �߻� �� ���� üũ ����
            >> �� ������δ� StopCoroutine()�� ���� �ܺο����� ���� ����� ����� �� ����
         */
        StartCoroutine("CountPlus");

    }

   IEnumerator CountPlus()
    {
        while(true)
        {
            cnt++;
            countText.text = cnt.ToString("N0");
            // C#�� ToString()�� ���� ���� ���·� ������ ����
            //N0�� ���� 3�ڸ� �������� ,�� ǥ���ϴ� format 1000 -> 1,000
            yield return null;
            //���� ���������� �Ѿ 
        }


        /*Debug.Log("�ƾƾ�");
        yield return new WaitForSeconds(1);
        //yield �� �Ͻ������� CPU�� ������ �ٸ� �Լ��� �����ϴ� Ű����
        Debug.Log("������");
        yield return new WaitForSeconds(5);
        Debug.Log("����");*/

    }
}
