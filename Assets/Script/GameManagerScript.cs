using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //Костыльно так нельзя нужен scrObj
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

    //Текущее состояние игры
    private GameState gameState;


    public GameObject Player;
    public GameObject PlayerDrone;
    GameObject MainCamera;
    
    private float Timer;


    //Смена состояния игры
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
    //Интерфейс для получения текущего состояния игры из-вне
    public GameState getCurretGameState()
    {
        return gameState;
    }






    private void Start()
    {
        //Устанавливаем по умолчанию состояние игры в Меню
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


    //Закрытие приложения игры
    public void exitGame()
    {
        Application.Quit();
    }
    

    //Открытие настроек игры
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

    //Переход к игре
    public void backToGame()
    {
        this.swapVisibleUi(currentActiveUI, GameUI);
        menuSoundPlayer.Pause();
        gameSoundPlayer.Play();
        changeGameState(GameState.GameProcess);
    }

    //Сохранение игрововго прогресса
    public void saveGame()
    {
        //Тут нужно найти менеджер игровых уровней и получить от него массив собранных кветов и их тип и  сохранить в строку
        string saveString = "";
        PlayerPrefs.SetString("save", saveString);
    }

    public void loadGame()
    {
        if(gameState == GameState.onMenu) {
            //Тут нужно эту структуру проинициализировать и выполнить действия для загрузки юзера на уровень
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


    //Пауза
    public void pauseGame()
    {
       gameState = GameState.onPause;
       Time.timeScale = 0f;
    }

    //Возобновление игрового процесса
    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    //Меняет активынй UI
    private void swapVisibleUi(GameObject hide, GameObject show)
    {
        hide.SetActive(false);
        show.SetActive(true);
        currentActiveUI = show;
    }

    //Изменеие громкости звука
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

