using System;
using System.Collections.Generic;
using UnityEngine;

/* 1. �迭 �Ǵ� ����Ʈ ���·� ����Ǿ� �ִ� json  ������ ����ϴ� ����
 * 2. Resources ������ �̿���(Ư�� ����) �����ͷε� ����� ���
 */

//���� �ȿ� �ִ� ������ �ϳ��� ���� ǥ��
[Serializable]
public class Item
{
    public string item_name;
    public int item_count;
}

//������ ���� ǥ��
[Serializable]
public class Inventory
{
    public List<Item> inventory; //json ���Ͽ��� ����ϰ� �ִ� �̸� �״�� ���
    // = public Item[] inventory; ����Ƽ������ �迭�� ����Ʈ ���� ���
}
public class JsonArraySample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("item_inventory");
        //json ������ ã�ƿ� �� �ƴ϶� textAsset ������ ã�ƿ��� �Ŷ� �ڿ� Ȯ���ڸ�.json�� ������ ����
        //���ҽ� ���� �ȿ� �ٸ� ������ ��������� /�� ������ �ۼ��ϱ�

        Inventory inventory = JsonUtility.FromJson<Inventory>(textAsset.text);

        int total = 0; 

        foreach(Item item in inventory.inventory)
        {
            total += item.item_count;
        }
        Debug.Log(total);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
