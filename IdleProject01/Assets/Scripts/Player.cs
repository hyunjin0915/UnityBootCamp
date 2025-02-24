using UnityEngine;

public class Player : Character
{
    Vector3 start_pos;
    Quaternion rotation;

    protected override void Start()
    {
        base.Start();
        start_pos = transform.position;
        rotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            //가까운 타겟 조사
            //list -> array
            TargetSearch(Spawner.monster_list.ToArray());


            float pos = Vector3.Distance(transform.position, start_pos);
            if (pos > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, start_pos, Time.deltaTime * 2.0f);
                transform.LookAt(start_pos);
                SetMotionChange("isMOVE", true);
            }
            else
            {
                transform.rotation = rotation;
                SetMotionChange("isMOVE", false);
            }
            return;
        }

        float target_distance = Vector3.Distance(transform.position, target.position);
        if (target_distance <= target_range && target_distance > attak_range)
        {
            SetMotionChange("isMOVE", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2.0f);
        }
        else if(target_distance <= attak_range)
        {
            SetMotionChange("isATTACK", true);
        }
    }
}
