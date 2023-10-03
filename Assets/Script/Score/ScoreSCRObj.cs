using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Scores", order = 1)]
public class ScoreSCRObj : ScriptableObject
{
    [SerializeField] Score[] scores;

    public Score getByName(string name)
    {
        return scores.FirstOrDefault(obj => obj.scoreName == name);
    }

    public Score[] getScores()
    {
        return scores;
    }

    public void setNewScoreValue(string name, float newCode)
    {
        getByName(name).scoreValue = newCode;
    }

}
