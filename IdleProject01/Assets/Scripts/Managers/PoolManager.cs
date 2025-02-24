using System;
using System.Collections.Generic;
using UnityEngine;


//pool �� ���� �۾� �� �ʿ��� �������� �����ϰ� �ִ� �������̽�
public interface IPool
{
    Transform parent { get; set;  }

    Queue<GameObject> pool { get; set;}

    //���͸� �������� ���
    GameObject GetGameObject(Action<GameObject> action = null);

    //���͸� �ݳ��ϴ� ���
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
            //Debug.Log("�Լ��� �����ض�");//���޹��� �׼��� ����
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
            action.Invoke(_gameObject); //���޹��� �׼��� ����
        }
    }
}
public class PoolManager
{
    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    public IPool PoolObject(string path)
    {
        //�ش� Ű�� ���ٸ� �߰��� ����
        if(pool_dict.ContainsKey(path) == false)
        {
            Add(path);
        }

        //ť�� ���� ��� ť �߰�
        if (pool_dict[path].pool.Count <= 0)
        {
            AddQ(path);
        }
         return pool_dict[path];
        //��ųʸ�[Ű] = ��;
        
    }

    public GameObject Add(string path)
    {
        //���޹��� �̸����� Ǯ������Ʈ ����
        var obj = new GameObject(path + "Pool");

        //������Ʈ Ǯ ����
        ObjectPool object_pool = new ObjectPool();

        //��ο� ������ƮǮ�� ��ųʸ��� ����
        pool_dict.Add(path, object_pool);

        //Ʈ������ ����
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
