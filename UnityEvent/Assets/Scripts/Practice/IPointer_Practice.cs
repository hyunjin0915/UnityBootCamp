using UnityEngine;
using UnityEngine.EventSystems;

public class IPointer_Practice : MonoBehaviour, IPointerClickHandler
{
    delegate void DelegateColor();
    DelegateColor dt;

    public Material m1;
    public Material m2;
    bool colorB = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        dt();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dt = new DelegateColor(ChangeColor);
    }

    void ChangeColor()
    {
        MeshRenderer mesh_ = transform.gameObject.GetComponent<MeshRenderer>();

        if (colorB)
        {
            mesh_.material = m1;
            colorB = false;
        }
        else
        {
            mesh_.material = m2;
            colorB = true;
        }
    }
}
