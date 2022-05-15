using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorebar : MonoBehaviour
{
    [SerializeField] private int digitsInScore = 6;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    
    public void OnScoreChanged(int score)
    {
        textMesh.text = ScoreToString(score);
    }

    private string ScoreToString(int score)
    {
        var scoreString = score.ToString();
        var difference = digitsInScore - scoreString.Length;
        if (difference > 0)
        {
            for (var i = 0; i < difference; i++)
            {
                scoreString = "0" + scoreString;
            }    
        }
        else if (difference < 0)
        {
            scoreString = "";
            for (var i = 0; i < digitsInScore; i++)
            {
                scoreString += "9";
            }
        }

        return scoreString;
    }

}
