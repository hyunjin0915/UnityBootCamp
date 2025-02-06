using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataSystem : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField descriptionInputField;

    public Button loadButton;
    public Text newText;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    public bool interactable;

    public ItemData itemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameInputField.onEndEdit.AddListener(NameValueChanged);//�� ����� �ν����Ϳ��� �� ����
        descriptionInputField.onEndEdit.AddListener(DescriptionValueChanged);

        loadButton.interactable = interactable;
        //��ư�� interactable�� ����ڿ��� ��ȣ�ۿ� ���θ� ������ �� ����ϴ� ��

        itemData = new ItemData();
    }

    /* public���� ���� �Լ��� ����Ƽ �ν����Ϳ��� ���� ����
     * public�� �ƴ� �Լ��� ��ũ��Ʈ �ڵ带 ���� ��� ���� 
     */
    public void Sample()
    {
        Debug.Log("Input Field's on value changed");
    }

    /// <summary>
    /// �۾��� ������ �Ǿ��� �� �ش� ������ �Է������� �˷��ִ� �Լ�
    /// </summary>
    /// <param name="text">����</param>
    public void NameValueChanged(string text)
    {

        itemData.itemName = text;

        Debug.Log(text + " �̸� �Է������ϴ�..");
    }
    public void DescriptionValueChanged(string text)
    {
        itemData.itemDescription = text;

        Debug.Log(text + " ���� �Է������ϴ�..");
    }

    public void OnSaveButtonClicked()
    {
        string dataValue = itemData.GetData();
        Debug.Log(dataValue);
        PlayerPrefs.SetString("ItemInfo", dataValue);
        loadButton.interactable = true;
    }

    public void OnLoadButtonClicked()
    {
        if(PlayerPrefs.HasKey("ItemInfo"))
        {
            //loadButton.interactable = true;

            string getDataStr = PlayerPrefs.GetString("ItemInfo");
            ItemData _data = ItemData.SetData(getDataStr);
            itemNameText.text = _data.itemName;
            itemDescriptionText.text = _data.itemDescription;

           //Debug.Log(_data.itemName);
        }
        
    }
}
