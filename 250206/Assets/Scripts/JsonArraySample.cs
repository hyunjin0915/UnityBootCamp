using System;
using System.Collections.Generic;
using UnityEngine;

/* 1. 배열 또는 리스트 형태로 저장되어 있는 json  파일을 사용하는 예제
 * 2. Resources 폴더를 이용한(특정 폴더) 데이터로드 방법을 사용
 */

//묶음 안에 있는 데이터 하나에 대한 표현
[Serializable]
public class Item
{
    public string item_name;
    public int item_count;
}

//묶음에 대한 표현
[Serializable]
public class Inventory
{
    public List<Item> inventory; //json 파일에서 사용하고 있는 이름 그대로 사용
    // = public Item[] inventory; 유니티에서는 배열과 리스트 같은 취급
}
public class JsonArraySample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("item_inventory");
        //json 파일을 찾아온 게 아니라 textAsset 파일을 찾아오는 거라서 뒤에 확장자명.json을 붙이지 않음
        //리소스 폴더 안에 다른 폴더를 만들었으면 /로 폴더명 작성하기

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
