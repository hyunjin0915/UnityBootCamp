using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //���ʹ� �ʿ� Ư�� �������� ���ʸ��� �ݺ��ؼ� ��ȯ
    public GameObject monster_prefab;
    
    public int monster_count;
    public int spawn_time;
    Vector3 pos;
    public float summon_rate; //��ȯ �ݰ� ���� ����
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

    //�Ϲ����� ������� �����
    IEnumerator SpawnMonster()
    {
        for (int i = 0; i < monster_count; i++)
        {
            do
            {
                pos = Vector3.zero + Random.insideUnitSphere * summon_rate;
                pos.y = 0;//���ٴڿ� �پ��ֵ���
            }
            while (Vector3.Distance(pos, Vector3.zero) <= re_rate);
            GameObject go = Instantiate(monster_prefab, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawn_time);
        StartCoroutine("SpawnMonster");
    }


    //������Ʈ Ǯ�� ������� �����
    IEnumerator SpawnMonsterPooling()
    {
        for (int i = 0; i < monster_count; i++)
        {
            do
            {
                pos = Vector3.zero + Random.insideUnitSphere * summon_rate;
                pos.y = 0;//���ٴڿ� �پ��ֵ���
            }
            while (Vector3.Distance(pos, Vector3.zero) <= re_rate);

            //������ �Լ��� ���� ���(�Ϲ� ����)
            //var go = Manager.POOL.PoolObject("Almond").GetGameObject(); 
            //������ �Լ��� �ִ� ���(Action<GameObject>)
            var go = Manager.POOL.PoolObject("Almond").GetGameObject((value) =>
            {
               // Debug.Log("�Լ��� ������");
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);
                monster_list.Add(value.GetComponent<Monster>());
            });
            StartCoroutine(ReturnMonsterPooling(go)); //Ǯ���� ���� ���� �ݳ�
        }
        

        yield return new WaitForSeconds(spawn_time);
        StartCoroutine("SpawnMonsterPooling");
    }

    //���� Ǯ���� ���� ���� ���� �ڵ�
    IEnumerator ReturnMonsterPooling(GameObject ob)
    {
        yield return new WaitForSeconds(1.0f);
        Manager.POOL.pool_dict["Almond"].ObjectReturn(ob);
    }
}
