Raycast 는 적에도 적용

충돌 - gameObject tag를 바꾸기 (좀비한테 닿는다던가)

→ 대신 tag에 대한 설계가 확실해야 함(런타임 중에 발생하기 때문에)

layer 도 가능 / name 은 안됨

Raycast 함수 매개변수에 충돌을 감지할 Layer를 따로 설정해줄 수 있음

→ 불필요한 연산을 줄일 수 있음/3

### 여러 개 RayCast

```csharp
RaycastHit[] raycastHits = Physics.RaycastAll(ray, weaponMaxDistance, TargetLayerMask);
if(raycastHits.Length > 0)
{
    foreach (RaycastHit hitObj in raycastHits)
    {
        Debug.Log("충돌 : " + hitObj.collider.name);
        Debug.DrawLine(ray.origin, hitObj.point, Color.red);
    }

```

### Trail Renderer Component

https://docs.unity3d.com/kr/2021.3/Manual/class-TrailRenderer.html

= 움직이는 게임 오브젝트 뒤에 폴리곤 트레일을 렌더링

실제로 게임을 만들어서 packaging 할 때는 사용하지 않고 editor 상에서 테스트 할 때 사용함

(렌더링하는 데에 리소스가 많이 들기 때문)

## Item 구현

E키를 눌러서 앞에 Box 모양의 RayCast를 만들 수 있음

```csharp
Vector3 origin = itemGetPos.position; //player pivot이 발끝이라 따로 지정
Vector3 direction = itemGetPos.forward;
//RaycastHit[] hits;
hits = Physics.BoxCastAll(origin, boxSize/2, direction, Quaternion.identity, castDistance, itemLayer);

```

### 여러 플래그 값들 설정

총 아이템을 주워야 장전 및 공격 가능

### 파티클 효과 넣기

총에 파티클 달아서 애니메이션 총 쏘는 순간에 이벤트 추가

### 총 딜레이 넣기

→ 코루틴 사용

### 발소리 적용하기

발 내딛을 때마다 이벤트 넣기

스크립트

바닥으로 raycast 쏴서 소리 다르게 재생되게 하기 

바닥에 물건도 많으니까 그 물건들도 다 tag로 구분(wood일때..~)

```csharp
public void FootStepSoundOn()
{
    if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, itemLayer))
    {
        if(hit.ColliderHit.tag == "Wood")
        {
            audioSource.PlayOneShot(audioClipFire); //발소리재생
        }
        else if (hit.ColliderHit.tag == "Wood")
        {
            audioSource.PlayOneShot(audioClipFire); //발소리재생
        }
    }

}
```

# AI

**상태** : 대기 상태/ 죽은 상태/ 살아있는/  

**행동** : 이동/ 순찰/ 추적/ 공격/ 

---

---

### 각자 수정해보기 🏃🏻‍♀️ 🏃🏻‍♀️🏃🏻‍♀️🏃🏻‍♀️🏃🏻‍♀️

E키가 아무데나 되는 게 아니라 아이템 가까이에서만 작동하게 수정

E키 하는 동안 걷기 모션 안되게 수정

반동 - 카메라 진동 

기울기

총에 대한 부분 다 data로 빼기 

따로 save 해줘야 함

무기별로 무게

미니맵은 카메라 상단에서 아래 보도록 해서 구현

(아트가 따로 더 필요한 영역)

## 맵 레퍼런스

![image.png](attachment:bfb3dcec-9ccd-48c3-95d2-8f96fe5a1515:image.png)
