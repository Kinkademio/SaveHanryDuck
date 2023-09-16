using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //Костыльно так нельзя нужен scrObj
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
    }

    //Переход к игре
    public void backToGame()
    {
        resumeGame();
        this.swapVisibleUi(currentActiveUI, GameUI);
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

        backToGame();
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

}
