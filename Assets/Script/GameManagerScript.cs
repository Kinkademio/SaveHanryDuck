using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //��������� ��� ������ ����� scrObj
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject SettingsUI;
    [SerializeField] GameObject HelpUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject LoseUI;

    [SerializeField] AudioSource menuSoundPlayer;
    [SerializeField] AudioSource gameSoundPlayer;
    [SerializeField] Text timerField;
    [SerializeField] float lifeTimer = 10f;
    [SerializeField] float baseVolume = 0.5f;

    private GameObject currentActiveUI;

    public enum GameState
    {
        onMenu, 
        onPause,
        GameProcess,
    }

    //������� ��������� ����
    private GameState gameState;


    public GameObject Player;
    public GameObject PlayerDrone;
    GameObject MainCamera;
    
    private float Timer;


    //����� ��������� ����
    public void changeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.GameProcess:
                break;
            case GameState.onPause:
                break;
            case GameState.onMenu:
                break;
        }
        gameState = newState;
    }
    //��������� ��� ��������� �������� ��������� ���� ��-���
    public GameState getCurretGameState()
    {
        return gameState;
    }






    private void Start()
    {
        //������������� �� ��������� ��������� ���� � ����
        changeGameState(GameState.onMenu);

        Timer = lifeTimer;
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", baseVolume);                                                                                                                 
        }
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        this.currentActiveUI = MenuUI;
        menuSoundPlayer.Play();

        MainCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        if (gameState == GameState.GameProcess)
        {
            endGame();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.openMenu();
                gameSoundPlayer.Pause();
                menuSoundPlayer.Play();
            }
        }
    }


    //�������� ���������� ����
    public void exitGame()
    {
        Application.Quit();
    }
    

    //�������� �������� ����
    public void openSettings()
    {
        this.swapVisibleUi(currentActiveUI, SettingsUI);
        GameObject.FindGameObjectWithTag("SoundSettingsSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
    }

    public void openMenu()
    {
        this.swapVisibleUi(currentActiveUI, MenuUI);
        if (gameState == GameState.GameProcess)
        {
            pauseGame();
        }
    }

    //������� � ����
    public void backToGame()
    {
        this.swapVisibleUi(currentActiveUI, GameUI);
        menuSoundPlayer.Pause();
        gameSoundPlayer.Play();
        changeGameState(GameState.GameProcess);
    }

    //���������� ��������� ���������
    public void saveGame()
    {
        //��� ����� ����� �������� ������� ������� � �������� �� ���� ������ ��������� ������ � �� ��� �  ��������� � ������
        string saveString = "";
        PlayerPrefs.SetString("save", saveString);
    }

    public void loadGame()
    {
        if(gameState == GameState.onMenu) {
            //��� ����� ��� ��������� ������������������� � ��������� �������� ��� �������� ����� �� �������
            Player.transform.position = new Vector3(0, 0, Player.transform.position.z);
            Player.SetActive(true);
            PlayerDrone.transform.position = new Vector3(0, 0, -3);
            PlayerDrone.SetActive(true);
            MainCamera.GetComponent<GenerationManager>().enabled = true;
            MainCamera.GetComponent<GenerationManager>().GenerationManagerFirstRoom();
            MainCamera.transform.position = new Vector3(0, 0, MainCamera.transform.position.z);
            MainCamera.GetComponent<CameraControl>().enabled = true;
            Player.GetComponent<PlayerControl>().SetKeyboardActive(true);
        }
        if (gameState == GameState.onPause)
        {
            resumeGame();
        }   
        backToGame();
    }

    public void endGame()
    {
        if (Timer < 0f)
        {
            deactivateGame();
            this.swapVisibleUi(currentActiveUI, LoseUI);
            changeGameState(GameState.onMenu);
        }
        else
        {
            Timer -= Time.deltaTime;
            updateTimer(Timer);
        }
    }

    public void winGame()
    {
        deactivateGame();
        this.swapVisibleUi(currentActiveUI, WinUI);
        changeGameState(GameState.onMenu);
    }

    public void deactivateGame()
    {
        Player.GetComponent<PlayerControl>().SetKeyboardActive(false);

        MainCamera.GetComponent<GenerationManager>().DestroyAllWithout();
        MainCamera.GetComponent<GenerationManager>().enabled = false;
        MainCamera.GetComponent<CameraControl>().enabled = false;
        PlayerDrone.SetActive(false);
        Player.SetActive(false);
        resetTimer();
    }

    public void resetGame()
    {

        this.swapVisibleUi(currentActiveUI, MenuUI);
        gameSoundPlayer.Pause();
        menuSoundPlayer.Play();
    }


    //�����
    public void pauseGame()
    {
       gameState = GameState.onPause;
       Time.timeScale = 0f;
    }

    //������������� �������� ��������
    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    //������ �������� UI
    private void swapVisibleUi(GameObject hide, GameObject show)
    {
        hide.SetActive(false);
        show.SetActive(true);
        currentActiveUI = show;
    }

    //�������� ��������� �����
    public void changeGameVolume(float volume)
    {
        Debug.Log(volume);
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }

    private void updateTimer(float newTime)
    {
        string time = System.Convert.ToString(newTime);
        string[] parts = time.Split(',');
        timerField.text = parts[0];

    }

    public void resetTimer()
    {
        this.Timer = lifeTimer;
    }


    public void openHelp()
    {
        this.swapVisibleUi(currentActiveUI, HelpUI);
    }
}

