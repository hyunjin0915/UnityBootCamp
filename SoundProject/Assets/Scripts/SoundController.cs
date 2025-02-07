using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Slider�� UI
//�ڵ��ϼ����� ��������� SliderJoint2D��
///RigidBody ������� ���� ���ӿ�����Ʈ�� �������� ���� ���� �̲������� �ϴ� ������ �� �� ���(Ex. �̴��̹�)



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
        //����� �ͼ��� ������ �ִ� �Ķ����(Expose)��ġ�� �����ϴ� ���
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
