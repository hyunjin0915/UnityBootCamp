using UnityEngine;

public class ItemData
{
    public string itemName;
    [TextArea]public string itemDescription;

    public ItemData()
    {

    }
    public ItemData(string itemName, string itemDescription)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
    }

    public string GetData() => $"{itemName},{itemDescription}";

    public static ItemData SetData(string data)
    {
        string[] data_values = data.Split(',');
        return new ItemData(data_values[0], data_values[1]);
    }
}
