using UnityEngine;
using UnityEngine.EventSystems;

public class UInterSample : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ŭ��������");
    }
    
}
