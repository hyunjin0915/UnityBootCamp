using UnityEngine;

public class CircleMove : MonoBehaviour
{
    //Circle�� ������ ��ġ�� Lerp ��Ű�� ��ũ��Ʈ
    public GameObject circle;
    Vector3 pos = new Vector3(5, -3, 0);
   

    // Update is called once per frame
    void Update()
    {
        //circle.transform.position = Vector3.Lerp(circle.transform.position,pos, Time.deltaTime); 
        //0�� �Է��� ��� �������� ����/ 1�� �ִ�ġ 

        //������ �ӵ��� ��ǥ �������� �̵��ϰ� ����� ��ũ��Ʈ
        //Vecot3.MoveTowards (���� ����, �� ����, �̵� �ӵ�)
        //circle.transform.position = Vector3.MoveTowards(circle.transform.position, pos, Time.deltaTime);

        circle.transform.position = Vector3.Slerp(circle.transform.position, pos, 0.05f);
    }
}
