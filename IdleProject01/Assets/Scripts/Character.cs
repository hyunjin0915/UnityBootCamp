using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    protected Animator animator;

    public double hp;
    public double atk;
    public float attack_speed;

    public float attak_range;
    public float target_range;

    protected Transform target; //Ÿ�ٿ� ���� ����

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }


    protected void SetMotionChange(string motion_name, bool param)
    {
        animator.SetBool(motion_name, param);
    }

    //animation event�� ���� ȣ��� �Լ�
    protected virtual void Bullet()
    {
        Debug.Log("�߻�");
    }

    //Ÿ���� ã�� ���(���Ϳ� �÷��̾� ����� �����)
    protected void TargetSearch<T>(T[] targets) where T : Component
    {
        T[] units = targets;
        Transform closest = null; //���� ����� ���� ���� null
        float max_distance = target_range; //�ִ�Ÿ� == Ÿ���� �Ÿ�

        //Ÿ�� ��ü�� ������� �Ÿ��� üũ
        foreach (var unit in units)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

           
            if(distance < max_distance)
            {
                closest = unit.transform;
                max_distance = distance;
            }
        }
        target = closest.transform;

        if (target != null)
        {
            transform.LookAt(target.position);
        }
    }
}
