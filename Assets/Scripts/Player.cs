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
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    private bool _shootingAllowed = true;
    
    private void Update()
    {
        _transform.Translate(Time.deltaTime * movementSpeed * _movementDirection );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed || !_shootingAllowed)
            return;

        Shoot();
        
        Invoke(nameof(AllowShooting), shootCooldown);
    }

    public override void ApplyDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException();
        
        health--;
    }
    
    private void AllowShooting()
    {
        _shootingAllowed = true;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, _transform.position, _transform.rotation);
        _shootingAllowed = false;
    }
}
