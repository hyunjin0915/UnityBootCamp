### 오전 동안 진행할 작업

1. 맵 배치
2. 플레이어 배치
3. 몬스터 소환
4. 투사체 날리기
5. 피격 시 진행할 이벤트

---

몬스터 소환시 너무 많은 클론이 만들어짐

→ 오브젝트 풀링을 통해 개선

# 🪣 오브젝트 풀링(Object Pooling)

https://unity.com/kr/how-to/use-object-pooling-boost-performance-c-scripts-unity

= 오브젝트를 pool에 만들어두고. **필요할 때마다 안에 있는 객체를 꺼내서 사용**하는 방식

- 매번 실시간으로 파괴, 생성하는 것보다 CPU의 부담을 줄일 수 있음
    - 객체가 필요할 때 애플리케이션에서 먼저 인스턴화 할 필요X
- 미리 할당해두기 때문에 메모리를 희생해서 성능을 높이는 방식

## 메모리 관련,,

➡️ GC를 실행하는 데에 필요한 메모리 관리 비용을 줄일 수 있음

(관리되는 메모리 할당하는 데에 CPU 시간 ↑, 이로 인해 CPU가 작업 완료할 때까지 다른 작업 중단,, → 런타임 성능 영향 O)

## 🛠️ 실습

### Manager.cs

앞으로 만들 모든 매니저에 대한 연결점

해당 매니저에 대한 접근(Property로 구현)

- CreateFromPath() 함수
    
    

### PoolManager.cs

1. `IPool 인터페이스` 설계
    
    = Pool에 대한 틀을 제공하는 용도
    
    - 트랜스폼(풀 연결 위치)
    - 큐(풀 구현)
    - 몬스터 하나 얻어오는 기능
    - 반납하는 기능
2. `ObjectPool 클래스` 설계 : IPool
    
    인터페이스 기반으로 기능 설계 완료
    
3. Pool Manager 멤버(변수, 함수) 설계
    
    string, IPool을 key, value로 가지는 dictionary 등록
    
    경로를 전달받아 키를 추가하는 함수 구현 
    
    경로를 전달받아 오브젝트를 생성하고 지정된 트랜스폼 쪽에 연결해주는 함수 구현 
    

❗경로에 대한 정보로 설계를 진행해서 매니저 쪽에 추가 코드 작성

- `GetGameObject()` 함수
    
    큐(pool)에서 꺼내오고 SetActive(true)
    전달 받은 액션이 있다면 실행
    꺼내온 GameObject 를 리턴 
    

### Spawner.cs

- 몬스터, 플레이어 개체들 리스트로 관리
- `IEnumerator SpawnMonsterPooling()` 코루틴 함수
    
    ```csharp
    var go = Manager.POOL.PoolObject("Almond").GetGameObject((value) =>
    {
        value.transform.position = pos;
        value.transform.LookAt(Vector3.zero);
    });
    ```
    
    - `PoolObject`
        
        PoolManager 클래스에서 Pool Dictionary에 해당 키가 없는 경우 추가,
        큐에 없는 경우 추가 후 해당 풀(인터페이스)를 리턴
        
    
- `IEnumerator ReturnMonsterPooling()` 코루틴 함수
    
    ```csharp
    IEnumerator ReturnMonsterPooling(GameObject ob)
    {
        yield return new WaitForSeconds(1.0f);
        Manager.POOL.pool_dict["Almond"].ObjectReturn(ob);
    }
    ```
    
    - Pooling한 값에 대한 리턴을 해주는 코드
    (실행하면 계속 없어짐)

### 애니메이션 적용

![Image](https://github.com/user-attachments/assets/6221bbe3-307c-4486-83ee-deb8514ec384)

- Animator에 적당한 Parameter 설정해서 적용하기
- 인스펙터 창에서 함수 이름으로 이벤트 등록할 수 있음!
    
    (함수는 해당 Animator 적용 중인 게임오브젝트의 스크립트에 작성)
    
    → 이거 제대로 안 돌아감.. 수정 필요..ㅠㅠ
