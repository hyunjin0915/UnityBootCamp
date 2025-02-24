using UnityEngine;

public class MovingGroundCtrl : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float upMax;
    public float downMax;
    private float direction = 1f;
    private Vector2 currentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        currentPosition.y += Time.deltaTime * direction * moveSpeed;
        if (currentPosition.y >=  upMax)
        {
            direction = -1f;
        }
        else if(currentPosition.y <= downMax) 
        {
            direction = 1f;
        }
        transform.position = currentPosition;
    }
}
