using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{
    //0) �ν����Ϳ��� ���� �����ϴ� ���
    public AudioSource audioSourceBGM;

    //1) AudioSourceSample ��ü�� ��ü������ ����� �ҽ��� ������ �ִ� ��� 
    //private AudioSource own_audioSource;

    //2) ������ ã�Ƽ� �����ϴ� ���
    public AudioSource audioSourceSFX;

    //3) Resource.Load() ����� �̿��� ���ҽ� ������ �ִ� ����� �ҽ��� Ŭ���� �޾ƿ���
   

    public AudioClip bgm; //����� Ŭ���� ���� ����
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //1)�� ��� GetComponent<T>�� ���� �ش� ��ü�� ������ �ִ� ����� �ҽ� ���� ����
        //own_audioSource = GetComponent<AudioSource>();

        //2) �� ��� GameObject.Find().GetComponent<T> Ȱ��
        //GameObject.Find()�� ������ ã�� gameObject�� return�ϴ� ����� ���� ����
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        audioSourceBGM.clip = bgm;
       
        audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;
        //Resource.Load()�� ���ҽ� �������� ������Ʈ�� ã�� �ε��ϴ� ���
        //�̶� �޾ƿ��� ���� Object�̹Ƿ� "as Ŭ������"�� ���� �ش� �����Ͱ� � �������� ǥ�����ָ� �� ���·� �޾ƿ��� ��
        // +) �̷��Ե� �Ǵ� �� audioSourceSFX.clip = Resources.Load<AudioClip>("Explosion");

        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;
        //���ҽ� �ε� �� ��ΰ� ������ �ִٸ� "������/���ϸ�"���� �ۼ�
        //���ҽ� �ε� �� �ۼ��ϴ� �̸����� Ȯ���ڸ�(.json .avi)�� �ۼ����� ����
        // +) ���� audioSourceSFX.clip = Resources.Load<AudioClip>("Audio/BombCharge");

        //UnityWebRequest�� GetAudioClip ��� Ȱ�� - ������̳� ������ ����� �� ����ϴ� ��� ���� 
        StartCoroutine("GetAudioClip");

        audioSourceBGM.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //�� ����� ����ϸ� �۾� ������ ���� �����
    //�Ͻ����� �÷����� �� ���
    IEnumerator GetAudioClip()
    {
        UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("file:///" + Application.dataPath + "/Audio" + "/Explosion" + ".wav", AudioType.WAV);
        yield return uwr.SendWebRequest(); //����

        AudioClip new_clip = DownloadHandlerAudioClip.GetContent(uwr); //���� ��θ� ������� �ٿ�ε� ����
        audioSourceBGM.clip = new_clip;
        audioSourceBGM.Play();
    }
}
