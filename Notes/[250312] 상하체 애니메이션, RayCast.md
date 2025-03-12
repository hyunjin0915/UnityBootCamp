## 애니메이션을 재생 특정 위치에서 원하는 함수 호출

![image (10)](https://github.com/user-attachments/assets/db38198a-977b-4de1-acf6-ba2a27e9dba3)

- 상속 관계에 있는 함수만 가져올 수 있음
- 조건문으로 **예외 처리** 필수
    - 클립이 없을 수 있고
    - 다른 함수, 변수에 대해 부를 때 없을 수도 있어서

### 걷기 소리 적용

발을 내딛을 때마다 소리 적용

RayCast 해서 바닥 지형에 따른 소리 다르게 적용

## 상하체 애니메이션 나누기

아바타 - 캐릭터 뼈대 관리하기

Asset - Avatar Mask 생성

![image (10)](https://github.com/user-attachments/assets/008cf4ae-66ea-4bc7-9bb6-19ebf14e9dc2)

IK : 캐릭터가 환경에 따라서 자연스럽게 상호작용 하는 것

Base Layer :  레이어 번호에 따라서 애니메이션을 재생함

Ex) 변신하는 경우에 따라서 나눠서 관리

➡️ 3D 에서는 몸을 이걸로 나눠서 관리함!

### Layer - 옵션들

- **Weight** : 애니메이션에 얼마나 영향을 줄지 정도
    - *UpperBody* Layer의 Weight를 1로
        
        → *UpperBody* Layer 가 *Base Layer* Layer를 덮어쓰게 됨
        
        → 상체는 설정해둔 애니메이션으로 플레이 됨
        
- Blending
- Override : 덮어쓴다
- Additive :
- Sync : 동기화 , Base layer 와 재생속도 동일하게
- IK Pass : 운동화에 대한 부분을 활성화 비활성화 여부

✔️ 상하체 상황별 애니메이션 나눠 가져와서 적용하기

---

- 물리 엔진 = 하나하나 다 계산하는 것

### RayCast

발사체 → 다 구현할 수 없으니 광선을 쏴서 총 쏘는 것을 구현

언리얼; Trace

### OncollisionEnter

플레이어의 Character Controller 에 RigidBody 없어서 개발할 때 생각을 잘하고 해야 함

Character Controller 의 Skin Width 때문에 OnCollisonEnter 충돌 처리가 안됨

→ 이 값을 늘려주면 물체 위로 올라가 버리는 문제 등등.. 이 발생

→ 거리 계산해서 처리/ 플레이어에 콜라이더 넣기 등등..

### 충돌체가 뭔지 구분하는 방법

1. 이름
    
    해당 오브젝트가 한 개만 존재했을 때 
    
2. Tag
    
    그룹이 있을 때
    
    EX) 모든 플레이어들에게 ..~
    
3. Layer
    
    충돌에 대한 형태
    

➡️ **설계**를 잘하지 않으면 너무 양이 많아질 수 있음

- 데미지랑 위치 이동 플레이어에게 달기
    
    → 물체들과 충돌하는 경우가 잦기 때문에 플레이어에 
    
- 플레이어 위치 순간이동 시킬 때 CharacterController 껐다 켜야 함

### 애니메이션이 재생 중인지 여부를 확인(상태)

```csharp
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    if(stateInfo.IsName(currentAnimation) && stateInfo.normalizedTime >= 1.0f)
    {
				//다른 애니메이션 재생 
    }

```

### 애니메이션 재생 속도 조절하기

1. 코드에서
    
    animator.speed = 1;
    
2. 인스펙터 창에서 직접 조절

- Rig Builder 항상 최상위에 있어야 함

### 총 쏘기

```csharp
Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward); //카메라에서 정면을 향해 
RaycastHit hit;

if(Physics.Raycast(ray, out hit, weaponMaxDistance))
{
    Debug.Log("Hit : " + hit.collider.name);
    Debug.DrawLine(ray.origin, hit.point, Color.red, 2.0f);
        
}
else
{
    Debug.DrawLine(ray.origin, ray.origin + ray.direction * weaponMaxDistance, Color.green, 2.0f);
}
```
