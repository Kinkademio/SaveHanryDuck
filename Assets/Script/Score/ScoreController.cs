using UnityEngine;

public static class ScoreController 
{
    //������ ��������� ��������
    public enum Score
    {
        PlayerTimeAliveScore,
        PlayerSurvivedScore
    }
    //����� ��� ���������
    public static float getScore(Score type)
    {
        return PlayerPrefs.GetFloat(type.ToString(), 0);
    }
    //������ ��� ��������
    public static void setScore(float value, Score type)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }
}
