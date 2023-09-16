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
    

    private GameObject currentActiveUI;


    private void Start()
    {
        this.currentActiveUI = MenuUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.openMenu();
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
    }

    //������� � ����
    public void backToGame()
    {
        resumeGame();
        this.swapVisibleUi(currentActiveUI, GameUI);
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
