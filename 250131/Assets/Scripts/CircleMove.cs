using UnityEngine;

public class CircleMove : MonoBehaviour
{
    //Circle을 지정된 위치로 Lerp 시키는 스크립트
    public GameObject circle;
    Vector3 pos = new Vector3(5, -3, 0);
   

    // Update is called once per frame
    void Update()
    {
        //circle.transform.position = Vector3.Lerp(circle.transform.position,pos, Time.deltaTime); 
        //0을 입력할 경우 움직이지 않음/ 1이 최대치 

        //일정한 속도로 목표 지점까지 이동하게 만드는 스크립트
        //Vecot3.MoveTowards (시작 지점, 끝 지점, 이동 속도)
        //circle.transform.position = Vector3.MoveTowards(circle.transform.position, pos, Time.deltaTime);

        circle.transform.position = Vector3.Slerp(circle.transform.position, pos, 0.05f);
    }
}
