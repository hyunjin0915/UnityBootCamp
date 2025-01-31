using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCount : MonoBehaviour
{
    //텍스트에 카운트를 출력하는 기능을 구현
    //카운트는 초마다 계속 1씩 증가하는 형태로 처리
    [SerializeField] private Text countText;
    private int cnt = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         코루틴 사용 방법
        StartCoroutine("함수 이름(IEnumerator 형태의 함수)");
        1. 문자열을 통해 함수를 찾아서 실행하는 방식
            >> 오타가 발생해도 오류가 발생하진 않음. 하지만 제대로 실행되지X
            >> StopCoroutine()을 통해 외부에서 중지하는 것이 가능

        StartCoroutine(함수 이름());
        2. 해당 함수를 호출해 실행 결과를 반환받는 형태
            >> 오타 발생 시 오류 체크 가능
            >> 이 방식으로는 StopCoroutine()을 통한 외부에서의 중지 기능을 사용할 수 없음
         */
        StartCoroutine("CountPlus");

    }

   IEnumerator CountPlus()
    {
        while(true)
        {
            cnt++;
            countText.text = cnt.ToString("N0");
            // C#의 ToString()을 통해 문자 형태로 변형이 가능
            //N0은 숫자 3자리 간격으로 ,를 표시하는 format 1000 -> 1,000
            yield return null;
            //다음 프레임으로 넘어감 
        }


        /*Debug.Log("아아아");
        yield return new WaitForSeconds(1);
        //yield 는 일시적으로 CPU의 권한을 다른 함수에 위임하는 키워드
        Debug.Log("이이이");
        yield return new WaitForSeconds(5);
        Debug.Log("우우우");*/

    }
}
