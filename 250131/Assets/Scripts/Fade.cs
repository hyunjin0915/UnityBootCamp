using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private GameObject image;
    private Renderer ImgRenderer;


    public int cntSpawn = 5;

    [SerializeField] private GameObject rangeObject;
    [SerializeField] private GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ImgRenderer = image.GetComponent<Renderer>();

        StartCoroutine(FadingOut());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadingOut()
    {
        float f = 1;
        while(f>0)
        {
            f -= 0.01f;
            Color c = ImgRenderer.material.color;
            c.a = f;
            ImgRenderer.material.color = c;
            yield return null;
        }
        StartCoroutine(RandomRespawn());
    }
    IEnumerator FadingIn()
    {
        float f = 0;
        while (f < 1)
        {
            f += 0.01f;
            Color c = ImgRenderer.material.color;
            c.a = f;
            ImgRenderer.material.color = c;
            yield return null;
        }
    }
    Vector3 GetRPos()
    {
        Vector3 originPos = rangeObject.transform.position;

        float range_X = rangeObject.transform.localScale.x;
        range_X = Random.Range((range_X/2)*-1, range_X/2);
        Vector3 randomPos = new Vector3(range_X, 0, 0);

        return (originPos + randomPos);
    }
    IEnumerator RandomRespawn()
    {
        while(cntSpawn > 0)
        {
            GameObject instantObj = Instantiate(prefab, GetRPos(), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            cntSpawn--;
        }
        StartCoroutine(FadingIn());
    }
}
