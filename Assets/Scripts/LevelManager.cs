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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    public UnityEvent levelCleared;
    
    public GameObject GetRandomEnemy()
    {
        var index = Random.Range(0, enemies.Count);

        return enemies[index];    
    }

    public void OnEnemyDeath(Entity entity)
    {
        enemies.Remove(entity.gameObject);

        if (enemies.Count == 0)
        {
            levelCleared.Invoke();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void OnPlayerDeath(Entity entity)
    {
        
    }
}
