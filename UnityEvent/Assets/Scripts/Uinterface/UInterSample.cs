using UnityEngine;
using UnityEngine.EventSystems;

public class UInterSample : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭진행함");
    }
    
}
