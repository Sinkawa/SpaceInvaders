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

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    private bool _isShooting;

    private void Update()
    {
        if (_isShooting)
            CurrentWeapon.Shoot(_transform);
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = movementSpeed * _movementDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            _isShooting = true;
        if (context.canceled)
            _isShooting = false;
    }
    
    public void PickUp(GameObject weapon)
    {
        
        CurrentWeaponObject = weapon;
        
        StartCoroutine(RemovePickUp(CurrentWeapon.GetTimeOfAction()));
    }

    private IEnumerator RemovePickUp(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentWeaponObject = defaultWeaponPrefab;
    }
}
