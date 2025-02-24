using System.Collections;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject movingGround;
    public float timer = 10f;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ControlMovingGround());
        }
    }

    IEnumerator ControlMovingGround()
    {
        movingGround.SetActive(true);
        yield return new WaitForSeconds(timer);
        movingGround.SetActive(false);
    }
}
