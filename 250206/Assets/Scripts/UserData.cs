using UnityEngine;

//클래스에 대한 직렬화
[System.Serializable]
public class UserData
{ 
    public string UserID;
    public string UserName;
    public string UserPassword;
    public string UserEmail;


    //아이디, 이름, 비밀번호, 이메일 순서대로 작성하면 생성할 수 있는 UserData
    public UserData(string userID, string userName, string userPassword, string userEmail)
    {
        UserID = userID;
        UserName = userName;
        UserPassword = userPassword;
        UserEmail = userEmail;
    }
    public UserData() { }

    /// <summary>
    /// 데이터를 하나의 문자열로 리턴하는 코드
    /// </summary>
    /// <returns>아이디,이름,비밀번호,이메일 순으로 처리</returns>
    public string GetData() => $"{UserID},{UserName},{UserPassword},{UserEmail}";
    //1줄짜리 return 함수를 작성하는 경우 {} 대신 =>로 작성 가능

    /// <summary>
    /// 데이터에 대한 정보를 전달받고 UserData로 리턴하는 코드
    /// </summary>
    /// <param name="data">아이디,이름,비밀번호,이메일 순으로 작성된 데이터</param>
    /// <returns></returns>
    public static UserData SetData(string data)
    {
        string[] data_values = data.Split(',');
        //문자열.Split(',')해당 문자열을 ()안에 넣어준 ,를 기준으로 잘라서 배열로 만들어줌

        return new UserData(data_values[0], data_values[1], data_values[2], data_values[3]);
    }
}
