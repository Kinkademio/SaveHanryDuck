using UnityEngine;

public static class ScoreController 
{
    //Список возможных рекордов
    public enum Score
    {
        PlayerTimeAliveScore,
        PlayerSurvivedScore
    }
    //Гетер для рекородов
    public static float getScore(Score type)
    {
        return PlayerPrefs.GetFloat(type.ToString(), 0);
    }
    //Сеттер для рекордов
    public static void setScore(float value, Score type)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }
}
