using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //�ִϸ��̼� Ŭ�� �̸����� ����
    public enum ANIME_STATE
    {
        PlayerIDLE,
        PlayerRun,
        PlayerJump,
        PlayerClear,
        PlayerGameOver
    }

    Animator animator;
    string currentAni; //���� �������� �ִϸ��̼�
    string previousAni; //������ ���� ���̴� �ִϸ��̼�


    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    public static string state = "playing"; //������ ����(�÷��� ��)

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
        //������ �� ���� �����ϴ� ������ ���� ���ӿ�����Ʈ�� �����ϴ� ���� ������ bool������ return���ִ� �Լ�
        //up�� (0, 1, 0)
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //���� ���� �ְų� �ӵ��� 0�� �ƴ� ���
        if(onGround || axisH != 0)
        {
            rbody.linearVelocity = new Vector2(speed*axisH, rbody.linearVelocityY);
        }
        //���� ���� �ִ� ���¿��� ���� Ű�� ���� ��Ȳ
        if (onGround && goJump)
        {
            Vector2 jumpPower = new Vector2(0, jump); //�÷��̾ ���� ���� ��ġ��ŭ ���� ����
            rbody.AddForce(jumpPower, ForceMode2D.Impulse); // �ش� ��ġ�� ���� ����
            goJump = false;
        }

        //�ִϸ��̼� �۾�
        if(onGround)
        {
            if (axisH == 0)
            {
                //current = ANIME_STATE.PlayerIDLE.ToString();
                currentAni = Enum.GetName(typeof(ANIME_STATE), 0); //�ش� enum�� �ִ� �� ���� �̸��� ��������
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
        //�̵� �¿����
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
        GetComponent<CapsuleCollider2D>().enabled = false; //���̻� �浹�� �߻����� �ʰ� �ݶ��̴� ��Ȱ��ȭ
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
