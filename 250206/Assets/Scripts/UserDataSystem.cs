using UnityEngine;

public class UserDataSystem : MonoBehaviour
{
    public UserData data01;
    public UserData data02;

    private void Start()
    {
        data01 = new UserData();
        data02 = new UserData("seohyun20080428","seohyun","0428","seohyun20080428@naver.com");

        //02 �����͸� �����ͼ�
        string data_value = data02.GetData();
        Debug.Log(data_value);

        //playerprefs�� Data01�̶�� �̸����� 02�� ����Ƽ�ø� ������
        PlayerPrefs.SetString("Data01", data_value);

        data01 = UserData.SetData(data_value);
        Debug.Log(data01.GetData());
    }
}
