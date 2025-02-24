using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //애니메이션 클립 이름으로 설정
    public enum ANIME_STATE
    {
        PlayerIDLE,
        PlayerRun,
        PlayerJump,
        PlayerClear,
        PlayerGameOver
    }

    Animator animator;
    string currentAni; //현재 진행중인 애니메이션
    string previousAni; //기존에 진행 중이던 애니메이션


    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    public static string state = "playing"; //현재의 상태(플레이 중)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        state = "Playing";
    }

    private void FixedUpdate()
    {
        if (state != "Playing")
        {
            return;
        }
        //지정한 두 점을 연결하는 가상의 선에 게임오브젝트가 접촉하는 지를 조사해 bool값으로 return해주는 함수
        //up은 (0, 1, 0)
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //지면 위에 있거나 속도가 0이 아닌 경우
        if(onGround || axisH != 0)
        {
            rbody.linearVelocity = new Vector2(speed*axisH, rbody.linearVelocityY);
        }
        //지면 위에 있는 상태에서 점프 키가 눌린 상황
        if (onGround && goJump)
        {
            Vector2 jumpPower = new Vector2(0, jump); //플레이어가 가진 점프 수치만큼 벡터 설계
            rbody.AddForce(jumpPower, ForceMode2D.Impulse); // 해당 위치로 힘을 가함
            goJump = false;
        }

        //애니메이션 작업
        if(onGround)
        {
            if (axisH == 0)
            {
                //current = ANIME_STATE.PlayerIDLE.ToString();
                currentAni = Enum.GetName(typeof(ANIME_STATE), 0); //해당 enum에 있는 그 값의 이름을 가져오기
            }
            else
            {
                currentAni = Enum.GetName(typeof(ANIME_STATE), 1);

            }
        }
        else
        {
            currentAni = Enum.GetName(typeof(ANIME_STATE), 2);
        }

        if(currentAni != previousAni)
        {
            previousAni = currentAni;
            animator.Play(currentAni);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(state != "Playing")
        {
            return;
        }
        //이동 좌우반전
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        goJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            Goal();
        }
        else if(collision.CompareTag("Dead"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 4));
        state = "GameOver";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false; //더이상 충돌이 발생하지 않게 콜라이더 비활성화
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    private void Goal()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 3));
        state = "GameClear";
        GameStop();
    }
    private void GameStop()
    {
        rbody.linearVelocity = new Vector2(0, 0);
    }
}
