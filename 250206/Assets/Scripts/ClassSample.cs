using UnityEngine;

class Unit
{
    //클래스에는 해당 클래스로 만들 객체의 정보를 작성
    public string unit_name;


    public static void UnitAction()
    {
        Debug.Log("유닛이 동작합니다.");
    }

    public void Cry()
    {
        Debug.Log("응애");
    }
}

public class ClassSample : MonoBehaviour
{
    Unit unit; //Unit 클래스 선언으로 만든 unit 객체(Object)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit.unit_name = "jgkj";
        //클래스변수명.필ㄷ를 통해 클래스가 가지고 있는 필드(변수)를 사용할 수 있음

        unit.Cry();
        
        Unit.UnitAction();
        //static이 붙은 변수나 함수는 객체를 생성하지 않고 클래스에서 바로 그 기능을 가져와 사용
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
