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

    [SerializeField] float baseVolume = 0.5f;

    private GameObject currentActiveUI;
    public bool inGame = false;

    public GameObject Player;
    GameObject MainCamera;
    [SerializeField] float lifeTimer = 10f;
    private float Timer;


    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            this.openMenu();
            gameSoundPlayer.Pause();
            menuSoundPlayer.Play();
        }

        if (inGame)
        {
            endGame();
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
        pauseGame();
        this.swapVisibleUi(currentActiveUI, MenuUI);
        this.inGame = false;
    }

    //Переход к игре
    public void backToGame()
    {
        resumeGame();
        this.swapVisibleUi(currentActiveUI, GameUI);
        menuSoundPlayer.Pause();
        gameSoundPlayer.Play();
        this.inGame = true;
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

        string data = PlayerPrefs.GetString("save");
        //Тут нужно эту структуру проинициализировать и выполнить действия для загрузки юзера на уровень
        Player.transform.position = new Vector3(0, 0, -1);
        Player.SetActive(true);
        MainCamera.GetComponent<GenerationManager>().enabled = true;
        MainCamera.GetComponent<GenerationManager>().GenerationManagerFirstRoom();
        MainCamera.transform.position = new Vector3(0, 0, MainCamera.transform.position.z);
        MainCamera.GetComponent<CameraControl>().enabled = true;
        Player.GetComponent<PlayerControl>().SetKeyboardActive(true);

        backToGame();
    }

    public void endGame()
    {
        if (Timer < 0f)
        {
            this.swapVisibleUi(currentActiveUI, LoseUI);

        }
        else
        {
            Timer -= Time.deltaTime;
            updateTimer(Timer);
        }
    }

    public void winGame()
    {
        this.swapVisibleUi(currentActiveUI, WinUI);
    }

    public void resetGame()
    {
        inGame = false;
        Player.GetComponent<PlayerControl>().SetKeyboardActive(false);
        MainCamera.GetComponent<GenerationManager>().DestroyAllWithout();
        MainCamera.GetComponent<GenerationManager>().enabled = false;
        MainCamera.GetComponent<CameraControl>().enabled = false;
        Player.SetActive(false);
        resetTimer();

        this.swapVisibleUi(currentActiveUI, MenuUI);
        gameSoundPlayer.Pause();
        menuSoundPlayer.Play();
    }


    //Пауза
    public void pauseGame()
    {
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

