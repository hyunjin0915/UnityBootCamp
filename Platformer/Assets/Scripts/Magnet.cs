using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float coinSpeed = 1f;
    float x;
    float y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            /*while(collision.gameObject.transform.position != gameObject.transform.position)
            {
                Vector2 vec = collision.transform.position;
                vec.x = vec.x + coinSpeed * Time.deltaTime;
                vec.y = vec.y + coinSpeed * Time.deltaTime;
                collision.transform.position = vec;
            }*/
        }
    }

    
}
