using System.Collections.Generic;
using UnityEngine;

public class Item2
{
    string name;
    int count;
}
public class Inventory2
{
    List<Item> inventory;
}

public class JsonPractice : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset textasset = Resources.Load<TextAsset>("item_inventory");
        Inventory2 inventory = JsonUtility.FromJson<Inventory2>(textasset.text);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
