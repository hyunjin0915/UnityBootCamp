using UnityEngine;
using UnityEngine.UI;
//플레이어의 이동(RigidBody를 이용)

/*해당 기능을 통해 이 스크립트를 컴포넌트로써 사용할 경우 적어놓은 컴포넌트를 요구하게 됨
    1. 해당 컴포넌트가 없는 오브젝트에 연결할 경우 자동으로 연결 진행
    2. 이 스크립트가 연결된 상태라면 그 오브젝트는 대사의 컴포넌트를 제거할 수 없음 */
[RequireComponent (typeof(Rigidbody2D))]
public class DuckMoving : MonoBehaviour
{
    public float speed = 2.0f; //소수점을 적을 때는 마지막에 f 기호를 사용, 소수점 약 6자리까지

    public double jump_Force = 3.5; //double도 실수를 표현하는 자료형이며 이 경우에는 f를 붙이지 않음, 소수점 약 15자리까지

    public bool isJump = false;

    private Rigidbody2D rigid;


    public GameObject[] items;
    int itemCnt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        /* GetComponent<T>
            해당 컴포넌트의 값을 얻어오는 기능
            T부분에는 컴포넌트의 형태를 작성
            컴포넌트의 형태가 다르다면 오류 발생 
            해당 데이터가 존재하지 않을 경우라면 null(값 없음)을 반환 
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
        /* GetAxisRaw("키 이름");은 InputManager의 키를 얻어오면서
         *  클릭에 따라 -1, 0, 1의 값을 가져옴
         *  Horizontal : 수평 이동 a, d 키나 키보드의 왼쪽 오른쪽 키
         *  Vertical : 수직 이동 w, s 키나 키보드의 위 아래 키 
         *  => 위의 코드를 통해 움직일 방향을 계산
         * */

        Vector3 velocity = new Vector3 (x, y, 0) * speed * Time.deltaTime; // 속력 = 방향 * 속도
        transform.position += velocity;
    }
    private void Jump()
    {
        //사용자가 키보드 space 키를 입력했다면 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * (float)jump_Force, ForceMode2D.Impulse);
                /*type casting
                 * (타입 이름) 변수를 통해 해당 변수를 설정한 타입으로 변경할 수 있음
                 * 단 캐스팅이 가능한 범위에서만 가능 
                 *  주로 int -> float 같은 경우
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
            Debug.Log("골인 지점 도착");
        }

        else if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            items[itemCnt].SetActive(true);
            itemCnt++;
        }
    }
}
