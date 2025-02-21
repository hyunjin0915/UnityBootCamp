### 플레이어 이동에 따른 회전

`private float GetAngle(Vector2 from, Vector2 to)`

= 각도 계산하는 함수

```csharp
        if (h != 0 || v != 0)
        {
            //from 과 to의 차이를 계산
            float dx = to.x - from.x;
            float dy = to.y - from.y;

            float radian = Mathf.Atan2(dy, dx);
            angle = radian * Mathf.Rad2Deg;
        }

```

- **아크탄젠트**
    
    https://docs.unity3d.com/kr/Packages/com.unity.visualeffectgraph@10.8/manual/Operator-Atan.html
    
    아크탄젠트(사용 가능한 타입 입력) → **입력의 아크탄젠트를 라디안 단위로** 리턴
    
    - 사용 가능한 타입?
        
        float, Vector, Vector2, Vector3, Vector4, Position, Direction
        
    - y/x 를 변수 하나로 받음.. → x가 0이면 버그
        
        → `Atan2(y, x)` 함수가 생김 
        
- **탄젠트**
    
    https://docs.unity3d.com/kr/Packages/com.unity.visualeffectgraph@10.8/manual/Operator-Tangent.html
    
    탄젠트(라디안 입력) → 입력한 라디안의 탄젠트 값 리턴 (Type은 입력한 타입과 일치하도록 변경)
    

### 특정 게임 오브젝트 추적

추적할 좌표의 벡터를 구하기

AddForce 사용

전달 값으로는 추적할 오브젝트의 위치

```csharp
float dx = position.x - transform.position.x;
float dy = position.y - transform.position.y;

float radian = Mathf.Atan2(dy, dx);

float x = Mathf.Cos(radian);
float y = Mathf.Sin(radian);

Vector2 v = new Vector2 (x, y);
return v; 
```

### 바라보고 있는 각도(Degree)로 화살 발사

```csharp
private void Attack()
{
    //화살을 가지고 있고 공격 상태가 아닌 경우
    if (ItemKeeper.hasArrows > 0 && inAttack == false)
    {
        ItemKeeper.hasArrows--;
        inAttack = true;

        var playerController = GetComponent<PlayerController>();
        float z = playerController.z;
        var rotation = Quaternion.Euler(0, 0, z);
        var arrowObject = Instantiate(arrowPrefab, transform.position, rotation);

        float x = Mathf.Cos(z * Mathf.Deg2Rad);
        float y = Mathf.Sin(z * Mathf.Deg2Rad);
        Vector3 vector = new Vector3(x, y, 0f) * speed;

        Rigidbody2D rbody = arrowObject.GetComponent<Rigidbody2D>();
        rbody.AddForce(vector, ForceMode2D.Impulse);

        //발사 딜레이만큼 지연시켜서 공격모드를 해제
        Invoke("AttackChange", delay);
    }
}

```

## Quaternion.Euler()

https://docs.unity3d.com/kr/2018.4/Manual/QuaternionAndEulerRotationsInUnity.html

### 오일러 각(Euler)

= 일반적인 각도 체계

- 직관적이고 읽기 쉬운 포맷
- 180도 넘는 회전 나타낼 수 있음
- **짐벌락 문제(Gimbal-lock)**
    
    = 축이 겹쳐지는 문제
    
    - 세 개의 축을 동시에 계산하지 않고, **각 축을 독립적으로 판단** → 겹쳐지는 문제
    - 한 축에 대해서는 계산이 불가능,, 정확한 각도 계산 불가,,

### 쿼터니언(Quaternion)

= 3D 공간의 회전을 나타냄 

- 각 축을 **한 번에 계산 진행!**
- x, y, z축과 스칼라(w) 총 4개의 값이 주어짐
- 직관적인 이해 어려움
- 방향과 회전 둘 다 표현 가능
    - 회전은 한 orientation 에서 다른 orientation 으로 측정 - **180보다 큰 값은 표현 X**

### 특징 및 주의사항

- transform 인스펙터 창에서는 회전을 오일러 각으로 표시
    
    → 이 회전 값은 백그라운드에서 새로운 쿼터니언 회전 값으로 변환됨!
    
- 스크립트에서 회전처리를 다루는 경우 **쿼터니언 클래스로 회전값을 만들고 수정**해야 함
    - 오일러 각을 사용할 수도 있는 상황에도
    **오일러 각을 처리하는 쿼너티언 클래스 함수를 사용**해야 함
    
    → 회전의 오일러 값을 수정하고 다시 적용하면 의도하지 않은 부작용 발생 가능 
    

---

## GetAxis

### GetAxis

-1 ~ 1 사이의 가상 축의 값을 반환

→ 슈퍼마리오처럼 가속도가 붙는 느낌으로 스무수수스스 하게 이동

### GetAxisRaw

스무스 필터링이 적용되지 않아서 -1, 0, 1 값을 반환

→ 스타듀벨리처럼 일정한 속도로 이동

## Tilemap 충돌 판정

https://docs.unity3d.com/kr/2022.3/Manual/class-TilemapCollider2D.html

LateUpdate 중에 콜라이더 모양을 업데이트 

여러 타일 변경사항을 일괄처리 → 성능에 미치는 영향을 최소화 

- **None** : 충돌 판정 없음
- **Sprite** : 이미지 모양대로 충돌 판정
    
    투명 부분에는 충돌 판정 없음
    
- **Grid :** 타일의 사각형 모양 기준으로 충돌
