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
                timeBar.SetActive(false); //�ð� ������ ������ �����
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
            //���� ����ÿ� ���� �߰����� ó��
            if(timeController != null)
            {
                if(timeController.gameTime>0.0f)
                {
                    //ǥ��� ������ ���̰� ����(�Ҽ��� ������)
                    int time = (int)timeController.displayTime;

                    //�ð� ���� UI
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
