// Copyright © 2022 Alexander Suvorov. All rights reserved.
// 
// This file is part of SpaceInvaders.
// 
// SpaceInvaders is free software: you can redistribute it and/or modify it under the terms of the GNU General
// Public License as published by the Free Software Foundation, either version 3 of the License, or (at your
// option) any later version.
// 
// SpaceInvaders is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the
// implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
// Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with SpaceInvaders. If not, see
// <https://www.gnu.org/licenses/>.

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScoreSystem))]
public class GameOver : MonoBehaviour
{
    [SerializeField] private int digitsInScore = 6;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        var scoreSystem = GetComponent<ScoreSystem>(); 
        scoreSystem.SaveFinalScore();
        scoreSystem.ClearCurrentScore();
        
        var finalScore = scoreSystem.GetFinalScore();
        var highScore = scoreSystem.GetHighScore();

        scoreText.text = $"Your score: {ScoreToString(finalScore)}";
        highScoreText.text = scoreSystem.SaveHighScore() ? "Highscore!" : $"Highscore: {ScoreToString(highScore)}";
        
        scoreSystem.SaveToDisk();
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
