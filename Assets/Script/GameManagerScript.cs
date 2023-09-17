using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //��������� ��� ������ ����� scrObj
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject SettingsUI;

    [SerializeField] AudioSource menuSoundPlayer;
    [SerializeField] AudioSource gameSoundPlayer;

    [SerializeField] float baseVolume = 0.5f;

    private GameObject currentActiveUI;
    public bool inGame = false;

    public GameObject Player;
    GameObject MainCamera;
    public float Timer = 0f;

    private void Start()
    {
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
        pauseGame();
        this.swapVisibleUi(currentActiveUI, MenuUI);
        this.inGame = false;
    }

    //������� � ����
    public void backToGame()
    {
        resumeGame();
        this.swapVisibleUi(currentActiveUI, GameUI);
        menuSoundPlayer.Pause();
        gameSoundPlayer.Play();
        this.inGame = true;
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

        string data = PlayerPrefs.GetString("save");
        //��� ����� ��� ��������� ������������������� � ��������� �������� ��� �������� ����� �� �������
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
        if (Timer > 10f)
        {
            inGame = false;
            Player.GetComponent<PlayerControl>().SetKeyboardActive(false);
            MainCamera.GetComponent<GenerationManager>().DestroyAllWithout();
            MainCamera.GetComponent<GenerationManager>().enabled = false;
            MainCamera.GetComponent<CameraControl>().enabled = false;
            Player.SetActive(false);
            Timer = 0f;

            this.swapVisibleUi(currentActiveUI, MenuUI);
            gameSoundPlayer.Pause();
            menuSoundPlayer.Play();
        }
        else
        {
            Timer += Time.deltaTime;
        }
    }

    //�����
    public void pauseGame()
    {
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
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }

}
