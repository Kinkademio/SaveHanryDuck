using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //��������� ��� ������ ����� scrObj
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject SettingsUI;

    [SerializeField] AudioSource menuSoundPlayer;
    [SerializeField] AudioSource gameSoundPlayer;
    

    private GameObject currentActiveUI;
    private bool inGame = false;

    private void Start()
    {
       
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        this.currentActiveUI = MenuUI;
        menuSoundPlayer.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            this.openMenu();
            gameSoundPlayer.Pause();
            menuSoundPlayer.Play();
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

        backToGame();
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
