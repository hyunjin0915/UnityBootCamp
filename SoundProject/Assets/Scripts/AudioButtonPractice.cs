using UnityEngine;

public class AudioButtonPractice : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClicked()
    {
        audioSource.Play();
    }

    public void OnPauseButtonClicked()
    {
        audioSource.Pause();
    }

    public void OnUnPauseButtonClicked()
    {
        audioSource.UnPause();
    }
}
