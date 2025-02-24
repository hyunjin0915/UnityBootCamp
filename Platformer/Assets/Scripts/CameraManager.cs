using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //카메라의 스크롤 제한 값
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    //서브스크린
    public GameObject subScreen;

    //강제 스크롤 기능 처리
    public bool isForceScrollX = false;
    public bool isForceScrollY = false;
    public float forceScrollSpeedX = 0.5f; //1초간 움직일 X 방향의 거리
    public float forceScrollSpeedY = 0.5f; 

    GameObject player;

    float x;
    float y;
    float z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        x = player.transform.position.x;
        y = player.transform.position.y;
        z = transform.position.z; //카메라 좌표

        //가로 강제 스크롤
        if(isForceScrollX )
        {
            x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
        }

        if (x < leftLimit)
        {
            x = leftLimit;
        }
        else if (x > rightLimit)
        {
            x = rightLimit;
        }

        //세로 강제 스크롤
        if (isForceScrollY)
        {
            y = transform.position.x + (forceScrollSpeedY * Time.deltaTime);
        }

        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if (y > topLimit)
        {
            y = topLimit;
        }

        //현재 카메라 위치 조정 - 카메라는 z축 까지 사용함
        Vector3 vector3 = new Vector3(x, y, z);
        transform.position = vector3;

        //subScreen
        if (subScreen != null)
        {
            y=subScreen.transform.position.y;
            z=subScreen.transform.position.z;
            Vector3 v = new Vector3(x * 0.5f, y, z);
            subScreen.transform.position = v;
        }
    }


}
