using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Slider는 UI
//자동완성으로 만들어지는 SliderJoint2D는
///RigidBody 물리제어를 받은 게임오브젝트가 공간에서 선을 따라 미끄러지게 하는 설정을 할 때 사용(Ex. 미닫이문)



public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider MasterVolumeSlider;
    [SerializeField]
    private Slider BGMVolumeSlider;
    [SerializeField]
    private Slider SFXVolumeSlider;
    private void Awake()
    {
        MasterVolumeSlider.onValueChanged.AddListener(SetMaster);
        BGMVolumeSlider.onValueChanged.AddListener(SetBGM);
        SFXVolumeSlider.onValueChanged.AddListener(SetSFX);
    }
    private void SetMaster(float volume)
    {
        //오디오 믹서가 가지고 있는 파라미터(Expose)수치를 설정하는 기능
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    private void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    private void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    
}
