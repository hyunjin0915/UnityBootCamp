using System.IO;
using UnityEngine;

public class JsonLoadSample : MonoBehaviour
{
    [System.Serializable]
    public class Item01
    {
        public string name;
        public string description;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string load_json = File.ReadAllText(Application.dataPath + "/item01.json");

        var data = JsonUtility.FromJson<Item01>(load_json);
        Debug.Log(data.name);

        //������ �����ؼ�
        data.name = "dfdfd";
        data.description = "dhfdkjhifnd";

        //Json ���Ϸ� ��������(Save)
        File.WriteAllText(Application.dataPath + "/item02.json", JsonUtility.ToJson(data));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
