using UnityEngine;

//Ŭ������ ���� ����ȭ
[System.Serializable]
public class UserData
{ 
    public string UserID;
    public string UserName;
    public string UserPassword;
    public string UserEmail;


    //���̵�, �̸�, ��й�ȣ, �̸��� ������� �ۼ��ϸ� ������ �� �ִ� UserData
    public UserData(string userID, string userName, string userPassword, string userEmail)
    {
        UserID = userID;
        UserName = userName;
        UserPassword = userPassword;
        UserEmail = userEmail;
    }
    public UserData() { }

    /// <summary>
    /// �����͸� �ϳ��� ���ڿ��� �����ϴ� �ڵ�
    /// </summary>
    /// <returns>���̵�,�̸�,��й�ȣ,�̸��� ������ ó��</returns>
    public string GetData() => $"{UserID},{UserName},{UserPassword},{UserEmail}";
    //1��¥�� return �Լ��� �ۼ��ϴ� ��� {} ��� =>�� �ۼ� ����

    /// <summary>
    /// �����Ϳ� ���� ������ ���޹ް� UserData�� �����ϴ� �ڵ�
    /// </summary>
    /// <param name="data">���̵�,�̸�,��й�ȣ,�̸��� ������ �ۼ��� ������</param>
    /// <returns></returns>
    public static UserData SetData(string data)
    {
        string[] data_values = data.Split(',');
        //���ڿ�.Split(',')�ش� ���ڿ��� ()�ȿ� �־��� ,�� �������� �߶� �迭�� �������

        return new UserData(data_values[0], data_values[1], data_values[2], data_values[3]);
    }
}
