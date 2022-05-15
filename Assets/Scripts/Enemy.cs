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
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class Enemy : Entity
{
    [SerializeField] private bool isActive = false;
    [SerializeField] private float activationDelay = 2f;

    private void Awake()
    {
        StartCoroutine(nameof(Activate));
    }
    
    private void Update()
    {
        if (isActive && _shootingAllowed)
            Shoot();
    }
    
    protected override void Shoot()
    {
        Instantiate(weaponPrefab, _transform.position, _transform.rotation);
        _shootingAllowed = false;
        StartCoroutine(nameof(AllowShooting));
    }
    
    private IEnumerator AllowShooting()
    {
        yield return new WaitForSeconds(shootCooldown);
        _shootingAllowed = true;
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(activationDelay);
        isActive = true;
    }
    
}
