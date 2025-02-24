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

    protected Transform target; //타겟에 대한 정보

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }


    protected void SetMotionChange(string motion_name, bool param)
    {
        animator.SetBool(motion_name, param);
    }

    //animation event에 의해 호출될 함수
    protected virtual void Bullet()
    {
        Debug.Log("발사");
    }

    //타겟을 찾는 기능(몬스터와 플레이어 공통된 기능임)
    protected void TargetSearch<T>(T[] targets) where T : Component
    {
        T[] units = targets;
        Transform closest = null; //가장 가까운 값은 현재 null
        float max_distance = target_range; //최대거리 == 타겟의 거리

        //타겟 전체를 대상으로 거리를 체크
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
