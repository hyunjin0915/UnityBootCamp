using UnityEngine;
using UnityEngine.UI;
//�÷��̾��� �̵�(RigidBody�� �̿�)

/*�ش� ����� ���� �� ��ũ��Ʈ�� ������Ʈ�ν� ����� ��� ������� ������Ʈ�� �䱸�ϰ� ��
    1. �ش� ������Ʈ�� ���� ������Ʈ�� ������ ��� �ڵ����� ���� ����
    2. �� ��ũ��Ʈ�� ����� ���¶�� �� ������Ʈ�� ����� ������Ʈ�� ������ �� ���� */
[RequireComponent (typeof(Rigidbody2D))]
public class DuckMoving : MonoBehaviour
{
    public float speed = 2.0f; //�Ҽ����� ���� ���� �������� f ��ȣ�� ���, �Ҽ��� �� 6�ڸ�����

    public double jump_Force = 3.5; //double�� �Ǽ��� ǥ���ϴ� �ڷ����̸� �� ��쿡�� f�� ������ ����, �Ҽ��� �� 15�ڸ�����

    public bool isJump = false;

    private Rigidbody2D rigid;


    public GameObject[] items;
    int itemCnt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        /* GetComponent<T>
            �ش� ������Ʈ�� ���� ������ ���
            T�κп��� ������Ʈ�� ���¸� �ۼ�
            ������Ʈ�� ���°� �ٸ��ٸ� ���� �߻� 
            �ش� �����Ͱ� �������� ���� ����� null(�� ����)�� ��ȯ 
         */
        itemCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        
    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        /* GetAxisRaw("Ű �̸�");�� InputManager�� Ű�� �����鼭
         *  Ŭ���� ���� -1, 0, 1�� ���� ������
         *  Horizontal : ���� �̵� a, d Ű�� Ű������ ���� ������ Ű
         *  Vertical : ���� �̵� w, s Ű�� Ű������ �� �Ʒ� Ű 
         *  => ���� �ڵ带 ���� ������ ������ ���
         * */

        Vector3 velocity = new Vector3 (x, y, 0) * speed * Time.deltaTime; // �ӷ� = ���� * �ӵ�
        transform.position += velocity;
    }
    private void Jump()
    {
        //����ڰ� Ű���� space Ű�� �Է��ߴٸ� 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * (float)jump_Force, ForceMode2D.Impulse);
                /*type casting
                 * (Ÿ�� �̸�) ������ ���� �ش� ������ ������ Ÿ������ ������ �� ����
                 * �� ĳ������ ������ ���������� ���� 
                 *  �ַ� int -> float ���� ���
                 */
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            isJump = false ;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("���� ���� ����");
        }

        else if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            items[itemCnt].SetActive(true);
            itemCnt++;
        }
    }
}
