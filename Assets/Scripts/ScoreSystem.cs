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
using UnityEngine;
using UnityEngine.Events;
public class ScoreSystem : MonoBehaviour
{
    private const string CurrentScoreKey = "CurrentScore";
    private const string HighScoreKey = "HighScore";
    private const string FinalScoreKey = "FinalScore";
    
    private int _currentScore;
    private int _finalScore;


    protected int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            scoreChanged.Invoke(_currentScore);
        }
    }
    
    public UnityEvent<int> scoreChanged;
    
    private void Awake()
    {
        CurrentScore = PlayerPrefs.GetInt(CurrentScoreKey);
    }

    public void ClearCurrentScore()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, 0);    
    }
    
    private void SaveCurrentScore()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, CurrentScore);    
    }

    public void SaveFinalScore()
    {
        PlayerPrefs.SetInt(FinalScoreKey, CurrentScore);    
    }
    
    public int GetFinalScore()
    {
        return PlayerPrefs.GetInt(FinalScoreKey);        
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey);  
    }

    public bool SaveHighScore()
    {
        var finalScore = GetFinalScore();
        var lastHighScore = GetHighScore();

        var isHighScored = finalScore > lastHighScore;
        
        if (isHighScored)
            PlayerPrefs.SetInt(HighScoreKey, finalScore);

        return isHighScored;
    }

    public void OnEnemyDeath(Entity entity)
    {
        if (entity.Points < 0)
            throw new ArgumentOutOfRangeException();
        
        CurrentScore += entity.Points;
    }

    public void OnLevelCleared()
    {
        SaveCurrentScore();
    }

    public void SaveToDisk()
    {
        PlayerPrefs.Save();
    }
}
