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

namespace DefaultNamespace
{
    public class ScoreSystem : MonoBehaviour
    {
        private const string ScoreKey = "score";
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
            CurrentScore = PlayerPrefs.GetInt(ScoreKey);
        }

        private void ClearPoints()
        {
            PlayerPrefs.SetInt(ScoreKey, 0);    
        }
        
        private void SavePoints()
        {
            PlayerPrefs.SetInt(ScoreKey, CurrentScore);    
        }
        
        public void OnPlayerDeath(Entity entity)
        {
            _finalScore = CurrentScore;
            CurrentScore = 0;
            ClearPoints();
        }

        public void OnEnemyDeath(Entity entity)
        {
            if (entity.Points < 0)
                throw new ArgumentOutOfRangeException();
            
            CurrentScore += entity.Points;
        }

        public void OnLevelCleared()
        {
            SavePoints();
        }

        public void OnNewLevelLoad()
        {
            ClearPoints();
        }
    }
}