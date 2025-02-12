using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class MeetEvent
{
    public delegate void MeetEventHandler(string message);

    public event MeetEventHandler meetHandler;

    public void Meet()
    {
        meetHandler("Hamburger");
    }
}

public class UnityDelegateEventSample : MonoBehaviour
{
    public TMP_Text messageUI;
    MeetEvent meetEvent = new MeetEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        meetEvent.meetHandler += EventMessage;
    }

    private void EventMessage(string message)
    {
        messageUI.text = message;
        Debug.Log(message);
    }

    public void OnMeetButtonClicked()
    {
        meetEvent.Meet();
    }
}
