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
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IWeapon
{
    [SerializeField] private string nameOfWeapon;
    [SerializeField] private int timeOfAction = 0;
    [SerializeField] [Tooltip("Shots per minute")] private int fireRate = 60;
    
    [SerializeField] protected GameObject projectilePrefab;

    protected bool _shootingAllowed = true;
    private float ShootCooldown => 60f / fireRate; //60 seconds in minute  

    public string GetName()
    {
        return nameOfWeapon;
    }

    public int GetTimeOfAction()
    {
        return timeOfAction;
    }

    public virtual void Shoot(Transform shotPoint)
    {
        if (!_shootingAllowed)
            return;
        
        Instantiate(projectilePrefab, shotPoint.position, shotPoint.rotation);
        _shootingAllowed = false;

        StartCoroutine(AllowShooting());
    }

    protected IEnumerator AllowShooting()
    {
        yield return new WaitForSeconds(ShootCooldown);
        _shootingAllowed = true;
    }
}