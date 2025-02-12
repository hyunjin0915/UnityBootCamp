using UnityEngine;

public class DelegateSample : MonoBehaviour
{
    
    delegate void DelegateTest();
    delegate string DelegateText(float x);
    delegate int DelegateInt(int x, int y);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Delegate 사용
        //Delegate명 변수명 = new Delegate명(함수명) <- 함수 이름이 메모리에 저장되어 있기 때문에 가능함
        DelegateTest dt = new DelegateTest(Attack);

        //함수처럼 호출
        dt();

        //내용 변경
        dt = Guard;

        dt();
        //하나의 이름으로 여러 기능 사용 가능


    }

    void Attack() => Debug.Log("공격");
    void Guard() => Debug.Log("방어");
    void MoveLeft() => Debug.Log("왼쪽 이동");

}
