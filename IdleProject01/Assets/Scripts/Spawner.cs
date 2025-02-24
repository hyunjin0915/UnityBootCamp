using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //몬스터는 맵에 특정 마리수를 몇초마다 반복해서 소환
    public GameObject monster_prefab;
    
    public int monster_count;
    public int spawn_time;
    Vector3 pos;
    public float summon_rate; //소환 반경 비율 조절
    public float re_rate = 2.0f; 

    public static List<Monster> monster_list = new List<Monster>();
    public static List<Player> player_list = new List<Player>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("SpawnMonsterPooling");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //일반적인 방법으로 만들기
    IEnumerator SpawnMonster()
    {
        for (int i = 0; i < monster_count; i++)
        {
            do
            {
                pos = Vector3.zero + Random.insideUnitSphere * summon_rate;
                pos.y = 0;//땅바닥에 붙어있도록
            }
            while (Vector3.Distance(pos, Vector3.zero) <= re_rate);
            GameObject go = Instantiate(monster_prefab, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawn_time);
        StartCoroutine("SpawnMonster");
    }


    //오브젝트 풀링 기법으로 만들기
    IEnumerator SpawnMonsterPooling()
    {
        for (int i = 0; i < monster_count; i++)
        {
            do
            {
                pos = Vector3.zero + Random.insideUnitSphere * summon_rate;
                pos.y = 0;//땅바닥에 붙어있도록
            }
            while (Vector3.Distance(pos, Vector3.zero) <= re_rate);

            //전달할 함수가 없는 경우(일반 생성)
            //var go = Manager.POOL.PoolObject("Almond").GetGameObject(); 
            //전달할 함수가 있는 경우(Action<GameObject>)
            var go = Manager.POOL.PoolObject("Almond").GetGameObject((value) =>
            {
               // Debug.Log("함수를 실행함");
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);
                monster_list.Add(value.GetComponent<Monster>());
            });
            StartCoroutine(ReturnMonsterPooling(go)); //풀링한 값에 대한 반납
        }
        

        yield return new WaitForSeconds(spawn_time);
        StartCoroutine("SpawnMonsterPooling");
    }

    //몬스터 풀링한 값에 대한 리턴 코드
    IEnumerator ReturnMonsterPooling(GameObject ob)
    {
        yield return new WaitForSeconds(1.0f);
        Manager.POOL.pool_dict["Almond"].ObjectReturn(ob);
    }
}
