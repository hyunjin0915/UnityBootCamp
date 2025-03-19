### 게임 인트로 어떻게 할지 생각해보기

- 직관적이게
- 무기, 아이템 배치 플레이어의 동선을 따라서
- 고도를 이용해서 - 저기로 가고 싶게 느끼게
- 아이템과 적을 배치
- 빗변에 신경 쓰이는 지형지물 배치
    - 끊임없이 다른 곳을 들리는 재미 → 모험의 호기심- 플레이의 주도권을 얻는 느낌을 주기
- 소리
- 하늘에서 아이템 떨어지기
    - 하늘에서 떨어지면서 시작?

---

### 부위별 데미지 주기

- 몬스터 TAkeDamage 에 매개변수
- 부위 별로 콜라이더 넣어서 구현도 가능

### 파티클 넣기

1. 좀비 자체에 넣어서 재생
2. **플레이어에 넣기**
    
    레이캐스트 부딪힌 곳으로 이동시켜서 파티클 재생
    

### 손전등 추가

### 총알 장전 추가

# AI Navigation

https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/index.html

- 움직일 Ground 에 Nav Mesh Surface Component 추가
- 좀비에 Nav Mesh Agent Component 추가

- 목표 지점까지 계속 움직이면 뒤집어짐
    
    → `Stopping Disatnce 옵션` 설정
    
- Obstacle Avoidance - 캡슐 콜라이더에 맞게 설정
    
    (너무 크면 못 지나감)
    

- 몬스터 스크립트 AI Agent 사용 코드로 수정하기

- 몬스터 손 크기보다 Collider 크기를 더 크게 하기
    
    → 조금 더 크게 잡아야 실감남
    
    → 애니메이션 녹화 기능으로 휘두를 때만 Collider 켜기 
    

### 메뉴 UI 레퍼런스

![image (3)](https://github.com/user-attachments/assets/52ff8b97-1e19-47d8-9381-d90161873310)

![image (5)](https://github.com/user-attachments/assets/6bfd735b-e54c-4647-88ee-f3e081d79124)


---

### ~~~과제~~~

- 레벨디자인
- AI 수정 및 추가
- Player 수정 및 추가
- Menu 디자인
