# NavMesh Link

https://docs.unity3d.com/kr/2020.3/Manual/class-NavMeshLink.html

## 정해진 링크를 통해서 장애물 건너다니게

- 장애물 게임오브젝트에 해당 컴포넌트 추가
    - 그 지점이 surface 표면에 연결되어 있어야 함
    
    → 아니면 버그 발생할 수 있음!! (공중에 있거나 하면,,)
    
- 하나의 오브젝트에 이 컴포넌트 여러 개 넣어도 O

### 주의

- AI에게 순찰할 때 모든 곳을 검사해서 Link 를 찾아야 함
    
    ➡️ 이걸 코드로 써줘야 함 
    
    - 좀비 입장에서 주기적으로 앞에 무엇이 있는지 검사해야 함
        
        뭐가 있는데 - 링크가 있으면 - 어떤 행동을 하도록 
        
- AI의 경우 오차가 계속 생길 수밖에 없음
    
    ➡️ 항상 움직임을 할 때는 잠시 멈추고 다음 행동으로 넘어가도록 해야 함
    

https://docs.unity3d.com/kr/530/ScriptReference/NavMeshAgent.html

# Manager class들 싱글톤으로 바꾸기

## PlayerManager

## SoundManager

### Dictionary 로 AudioClip 관리

![image (6)](https://github.com/user-attachments/assets/659a6216-2a58-4e71-823b-f561c6711187)

- Dictionary 는 인스펙터 창에 Serializable 해도 보이지 않음
    
    ➡️ AudioClip 정보를 담는 **Struct 배열을 Serializable로 만들어서 이를 Dictionary 에 저장**하기 
    
- 인스펙터 창에서 AudioClip들 추가
- SoundManager.cs 에서 이를 Dictionary 에 저장 & 사용하는 함수 작성

### SFX 소리 크기가 모든 곳에서 동일하게 남

→ 3차원 공간이니까 수정 필요 (AudioSource 막 남발해서 만들면 안됨..!)

- **PlayClipAtPoint() 사용하기**
    
    = 소리가 나는 위치를 지정해주는 함수 
    
    ```csharp
    public void PlaySFX(string name, Vector3 position)
    {
        if (DicsfxClips.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(DicsfxClips[name], position);
        }
    }
    ```
    
    - 새로운 GameObject를 동적으로 생성
        
        (임시 오브젝트가 생성됨)
        
    - 클립이 끝나면 자동으로 삭제됨
        
        GC가 자동으로 Destroy - 따로 관리할 필요 없음 
        
- 해당 방법이 정답은 X
    - 요즘 3D는 공간에 구애 받지 않고 여러 곳에서 소리가 나기 때문에 이렇게 구현
    - 지속적으로 재생되는 사운드는 AudioSource를 직접 생성하고 관리하는 게 더 좋을 수 있음!

⚠️ 이렇게 하면 메뉴 효과음이랑, 게임 내의 효과음이랑 다르게 구분해주는 것이 필요

➡️ 함수 매개변수로 구분해주는 인자 받기

(난 오버라이딩으로 구현)

- AudioSOurce 의 Spatial Blend 옵션 수정하기
    - PlayClipAtPoint 사용하면 디폴트로 옵션이 1이 됨
    - BGM은 옵션 0으로 수정하기!!
        
        `bgmSource.spatialBlend = 0f;`
        

### BGM 소리 씬 전환될 때 커졌다가 작아졌다가 되게 수정

```csharp
public void PlayBGM(string name, float fadeDuration = 1.0f)
{
    if(DicbgmClips.ContainsKey (name))
    {
        if(currentBGMCoroutiune != null)
        {
            StopCoroutine(currentBGMCoroutiune);
        }

        currentBGMCoroutiune = StartCoroutine(FadeOutBGM(fadeDuration, () =>
        {
            bgmSource.clip = DicbgmClips[name];
            bgmSource.Play();
            currentBGMCoroutiune = StartCoroutine(FadeInBGM(fadeDuration));
        }));

        bgmSource.clip = DicbgmClips[name];
        bgmSource.Play();
    }
}
```

```csharp
private IEnumerator FadeOutBGM(float duration, Action OnFadeComplete)
{
    float startVolume = bgmSource.volume;

    for (float t = 0; t < duration; t+= Time.deltaTime)
    {
        bgmSource.volume = Mathf.Lerp(startVolume, 0f, t/duration);
        yield return null;
    }

    bgmSource.volume = 0;
    OnFadeComplete?.Invoke(); //페이드아웃이 완료되면 다음 작업 실행
}
```

## 아이템 주웠을 때 플레이어 손에 위치하기

- `gameObject.transform.SetParent(transform.position);`
    
    부모를 지정하기
    
- `gameObject.transform.SetParent(null);`
    
    부모에서 빠지고 World 공간으로 가게 됨 
    

---

# +@)

팀프로젝트 할 때 꼭 기획을 꼼꼼하게 잘 해야 함

## 과제 ~~~~~~~~

- Level Design
- Player, AI 기능 추가 및 버그들 수정하기
- SoundManager, SceneManager 코드 추가 및 수정
- 아이템 구현
    
    총알, 체력 회복
    
- SoundManager.cs 에 씬 전환될 때 씬마다 다른 BGM 재생되게 수정

발표 5분이상 25분 미만

동영상 녹화, 피피티???????????????????????????????????????? 만들기

28일 발표

---

체력/ 회복 

수류탄?

헬리콥터
