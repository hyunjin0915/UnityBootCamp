using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //ī�޶��� ��ũ�� ���� ��
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    //���꽺ũ��
    public GameObject subScreen;

    //���� ��ũ�� ��� ó��
    public bool isForceScrollX = false;
    public bool isForceScrollY = false;
    public float forceScrollSpeedX = 0.5f; //1�ʰ� ������ X ������ �Ÿ�
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
        z = transform.position.z; //ī�޶� ��ǥ

        //���� ���� ��ũ��
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

        //���� ���� ��ũ��
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

        //���� ī�޶� ��ġ ���� - ī�޶�� z�� ���� �����
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
