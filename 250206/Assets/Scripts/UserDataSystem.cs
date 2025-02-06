using UnityEngine;

public class UserDataSystem : MonoBehaviour
{
    public UserData data01;
    public UserData data02;

    private void Start()
    {
        data01 = new UserData();
        data02 = new UserData("seohyun20080428","seohyun","0428","seohyun20080428@naver.com");

        //02 데이터를 가져와서
        string data_value = data02.GetData();
        Debug.Log(data_value);

        //playerprefs에 Data01이라는 이름으로 02의 데이티ㅓ를 저장함
        PlayerPrefs.SetString("Data01", data_value);

        data01 = UserData.SetData(data_value);
        Debug.Log(data01.GetData());
    }
}
