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
        nameInputField.onEndEdit.AddListener(NameValueChanged);//이 방식은 인스펙터에서 안 보임
        descriptionInputField.onEndEdit.AddListener(DescriptionValueChanged);

        loadButton.interactable = interactable;
        //버튼의 interactable은 사용자와의 상호작용 여부를 제어할 때 사용하는 값

        itemData = new ItemData();
    }

    /* public으로 만든 함수는 유니티 인스펙터에서 직접 연결
     * public이 아닌 함수는 스크립트 코드를 통해 기능 연결 
     */
    public void Sample()
    {
        Debug.Log("Input Field's on value changed");
    }

    /// <summary>
    /// 작업이 마무리 되었을 때 해당 문구를 입력했음을 알려주는 함수
    /// </summary>
    /// <param name="text">문구</param>
    public void NameValueChanged(string text)
    {

        itemData.itemName = text;

        Debug.Log(text + " 이름 입력했읍니다..");
    }
    public void DescriptionValueChanged(string text)
    {
        itemData.itemDescription = text;

        Debug.Log(text + " 설명 입력했읍니다..");
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
