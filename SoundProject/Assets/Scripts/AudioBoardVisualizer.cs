using UnityEngine;
using UnityEngine.Audio;


// 보드를 이용해서 오디오를 화면에 표현하는 도구
//AudioUI 오브젝트에 연결
public class AudioBoardVisualizer : MonoBehaviour
{
    //보드 증가량
    public float minBoard = 100f;
    public float maxBoard = 1000f;

    //사용할 오디오 클립
    public AudioClip audioClip;
    //사용할 오디오 소스
    public AudioSource audioSource;

    //오디오 믹서 연결
    public AudioMixer audioMixer;

    public Board[] boards;

    //스펙트럼용 samples(고정 수치임)
    public int samples = 64;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        boards = GetComponentsInChildren<Board>();

        
        if (audioSource == null)
        {
            //게임 오브젝트를 만들어서 컴포넌트를 등록해주는 코드
            audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        //오디오믹서 그룹 중에서 "MAster" 그룹을 찾아 적용
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] data = audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        //GetSpectrumData(samples, AdditionalCanvasShaderChannels, FFTWindow);

        //samples = FFT(신호에 대한 주파수 영역)를 저장할 배열, 이 배열 값은 2의 제곱수로 표현
        //채널은 최대 8개의 채널을 지원해주고 있음 - 일반적으로는 0을 사용
        //FFTWindow는 샘플링을 진행할 때 쓰는 값 

        for (int i = 0; i < boards.Length; i++)
        {
            var size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard) * 3.0f);
            //3.0f는 막대에 대한 수치 보정 값

          
            boards[i].GetComponent<RectTransform>().sizeDelta = size;
            //sizeDelta는 부모를 기준으로 크기가 얼마나 큰지 작은지를 나타낸 수치를 의미
        }
    }
}
