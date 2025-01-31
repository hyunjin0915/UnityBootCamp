using UnityEngine;

public class CreateObject3 : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    /*
    ����ȭ
    �����ͳ� ������Ʈ�� ��ǻ�� ȯ�濡 �����ϰ� �籸���� �� �ִ� ����(����)�� ��ȯ�ϴ� ����
    ����Ƽ������ �����ϰ� private ������ �����͸� �ν����Ϳ��� ���� �� �ְ� �������شٰ� ����
     */
    private GameObject sample;


    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/TableBody");
        /*
        Resources.Load<T>("������ġ/���¸�");
        T : �������� ���¸� �����ִ� ��ġ
        */
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            sample = Instantiate(prefab);

            sample.AddComponent<VectorSample>();
            /*
             * gameObject.AddComponent<T>
             * ������Ʈ�� ������Ʈ ����� �߰��ϴ� ���
             * GetComponent<T>
             * ������Ʈ�� ������ �ִ� ������Ʈ�� ����� ������ ���
             * ��ũ��Ʈ���� �ش� ������Ʈ�� ����� �����ͼ� ����ϰ� ���� ��� ��� 
             */
        }
    }
}
