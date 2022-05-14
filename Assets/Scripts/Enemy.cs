﻿// Copyright © 2022 Alexander Suvorov. All rights reserved.
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

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Entity
{
 
    public override void ApplyDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException();

        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
