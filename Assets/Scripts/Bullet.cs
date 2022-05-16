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

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : Projectile
{
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected int damage = 1;

    [SerializeField] protected ParticleSystem destroyEffect;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = movementSpeed * transform.right;
    }

    protected override void ShowDestroyEffect()
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);
    }

    protected override void InteractWithEntity(Entity entity)
    {
        entity.ApplyDamage(damage);
    }
}
