using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImg;
    public Sprite gameOverSprite;
    public Sprite gameClearSprite;
    public GameObject panel;
    public GameObject restartBtn;
    public GameObject nextBtn;

    Image img;

    //Time Controller
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeController;

    GameObject player;
    PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeController = GetComponent<TimeController>();

        if (timeController != null)
        {
            if(timeController.gameTime == 0)
            {
                timeBar.SetActive(false); //시간 제한이 없으면 숨기기
            }
        }

        Invoke("InactiveImg", 1.0f);
        panel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.state.Equals("GameClear"))
        {
            mainImg.SetActive(true);
            panel.SetActive(true);

            restartBtn.GetComponent<Button>().interactable = false;
            mainImg.GetComponent<Image>().sprite = gameClearSprite;
            PlayerController.state = "GameEnd";

            if (timeController != null)
            {
                timeController.isTimeOver = true;
            }
        }
        else if(PlayerController.state.Equals("GameOver"))
        {
            mainImg.SetActive(true);
            panel.SetActive(true);

            nextBtn.GetComponent<Button>().interactable = false;
            mainImg.GetComponent<Image>().sprite = gameOverSprite;
            PlayerController.state = "GameEnd";

            if (timeController != null)
            {
                timeController.isTimeOver = true;
            }
        }
        else if (PlayerController.state.Equals("Playing"))
        {
            //게임 진행시에 대한 추가적인 처리
            if(timeController != null)
            {
                if(timeController.gameTime>0.0f)
                {
                    //표기상 정수로 보이게 설정(소수점 버리기)
                    int time = (int)timeController.displayTime;

                    //시간 갱신 UI
                    timeText.GetComponent<Text>().text = time.ToString();

                    if(time == 0)
                    {
                        playerController.GameOver();
                    }
                }
            }
        }
    }

    private void InactiveImg()
    {
        mainImg.SetActive(false);
    }
}
