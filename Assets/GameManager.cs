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

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Player events")]
    public UnityEvent<Entity> onPlayerDeath;
    public UnityEvent<int> onPlayerHealthChanged;
    public UnityEvent<IWeapon> onWeaponChanged;
    
    [Header("Entity events")]
    public UnityEvent<Entity> onEnemyDeath;

    public void OnPlayerDeath(Entity entity)
    {
        onPlayerDeath.Invoke(entity);
    }
    
    public void OnPlayerHealthChanged(int health)
    {
        onPlayerHealthChanged.Invoke(health);
    }
    
    public void OnEnemyDeath(Entity entity)
    {
        onEnemyDeath.Invoke(entity);
    }

    public void OnWeaponChanged(IWeapon weapon)
    {
        onWeaponChanged.Invoke(weapon);
    }
    
}