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

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScoreSystem))]
public class MainMenu : MonoBehaviour
{
    private ScoreSystem _scoreSystem;

    [SerializeField] private TextMeshProUGUI highScoreText; 
    
    public void Start()
    {
        _scoreSystem = GetComponent<ScoreSystem>();
        var highScore = _scoreSystem.GetHighScore();
        highScoreText.text = (highScore == 0) ? "No highscore!" : highScore.ToString();
    }

    public void StartGame()
    {
        _scoreSystem.ClearCurrentScore();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        _scoreSystem.SaveToDisk();
        Application.Quit();
    }
}