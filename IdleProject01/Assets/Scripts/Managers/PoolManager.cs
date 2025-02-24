using System;
using System.Collections.Generic;
using UnityEngine;


//pool 에 대한 작업 시 필요한 정보들을 보관하고 있는 인터페이스
public interface IPool
{
    Transform parent { get; set;  }

    Queue<GameObject> pool { get; set;}

    //몬스터를 가져오는 기능
    GameObject GetGameObject(Action<GameObject> action = null);

    //몬스터를 반납하는 기능
    void ObjectReturn(GameObject _gameObject, Action<GameObject> action = null);
}
public class ObjectPool : IPool
{
    public Transform parent { get; set; }
    public Queue<GameObject> pool { get; set; } = new Queue<GameObject>();

    public GameObject GetGameObject(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue();
        obj.SetActive(true);

        if(action != null)
        {
            action.Invoke(obj);
            //Debug.Log("함수를 실행해라");//전달받은 액션을 실행
        }
        return obj;
    }

    public void ObjectReturn(GameObject _gameObject, Action<GameObject> action = null)
    {
        pool.Enqueue(_gameObject);
        _gameObject.transform.parent = parent;
        _gameObject.SetActive(false);

        if (action != null)
        {
            action.Invoke(_gameObject); //전달받은 액션을 실행
        }
    }
}
public class PoolManager
{
    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    public IPool PoolObject(string path)
    {
        //해당 키가 없다면 추가를 진행
        if(pool_dict.ContainsKey(path) == false)
        {
            Add(path);
        }

        //큐에 없는 경우 큐 추가
        if (pool_dict[path].pool.Count <= 0)
        {
            AddQ(path);
        }
         return pool_dict[path];
        //딕셔너리[키] = 값;
        
    }

    public GameObject Add(string path)
    {
        //전달받은 이름으로 풀오브젝트 생성
        var obj = new GameObject(path + "Pool");

        //오브젝트 풀 생성
        ObjectPool object_pool = new ObjectPool();

        //경로와 오브젝트풀을 딕셔너리에 저장
        pool_dict.Add(path, object_pool);

        //트랜스폼 설정
        object_pool.parent = obj.transform;

        return obj;
    }

    public void AddQ(string path)
    {
        var go = Manager.instance.CreateFromPath(path);
        go.transform.parent = pool_dict[path].parent;

        pool_dict[path].ObjectReturn(go);
    }
}
