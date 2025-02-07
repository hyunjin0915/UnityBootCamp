using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{
    //0) 인스펙터에서 직접 연결하는 경우
    public AudioSource audioSourceBGM;

    //1) AudioSourceSample 객체가 자체적으로 오디오 소스를 가지고 있는 경우 
    //private AudioSource own_audioSource;

    //2) 씬에서 찾아서 연결하는 경우
    public AudioSource audioSourceSFX;

    //3) Resource.Load() 기능을 이용해 리소스 폴더에 있는 오디오 소스의 클립을 받아오기
   

    public AudioClip bgm; //오디오 클립에 대한 연결
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //1)의 경우 GetComponent<T>를 통해 해당 객체가 가지고 있는 오디오 소스 연결 가능
        //own_audioSource = GetComponent<AudioSource>();

        //2) 의 경우 GameObject.Find().GetComponent<T> 활용
        //GameObject.Find()는 씬에서 찾은 gameObject를 return하는 기능을 갖고 있음
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        audioSourceBGM.clip = bgm;
       
        audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;
        //Resource.Load()는 리소스 폴더에서 오브젝트를 찾아 로드하는 기능
        //이때 받아오는 값은 Object이므로 "as 클래스명"을 통해 해당 데이터가 어떤 형태인지 표현해주면 그 형태로 받아오게 됨
        // +) 이렇게도 되는 듯 audioSourceSFX.clip = Resources.Load<AudioClip>("Explosion");

        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;
        //리소스 로드 시 경로가 정해져 있다면 "폴더명/파일명"으로 작성
        //리소스 로드 시 작성하는 이름에는 확장자명(.json .avi)를 작성하지 않음
        // +) 가능 audioSourceSFX.clip = Resources.Load<AudioClip>("Audio/BombCharge");

        //UnityWebRequest의 GetAudioClip 기능 활용 - 모바일이나 서버를 사용할 때 사용하는 통신 도구 
        StartCoroutine("GetAudioClip");

        audioSourceBGM.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //이 방법을 사용하면 작업 끝나면 값이 사라짐
    //일시적인 플레이할 때 사용
    IEnumerator GetAudioClip()
    {
        UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("file:///" + Application.dataPath + "/Audio" + "/Explosion" + ".wav", AudioType.WAV);
        yield return uwr.SendWebRequest(); //전달

        AudioClip new_clip = DownloadHandlerAudioClip.GetContent(uwr); //받은 경로를 기반으로 다운로드 진행
        audioSourceBGM.clip = new_clip;
        audioSourceBGM.Play();
    }
}
