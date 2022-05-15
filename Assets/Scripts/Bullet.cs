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

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected LayerMask targetLayer;
    
    [SerializeField] protected ParticleSystem destroyEffect;

    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Move();
    }
    
    protected virtual void Move()
    {
        _rigidbody2D.velocity = movementSpeed * _transform.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Entity entity))
        {
            
            if ((targetLayer & 1 << entity.gameObject.layer) == 1 << entity.gameObject.layer)
            {
                entity.ApplyDamage(damage);
                
                Instantiate(destroyEffect, _transform.position, _transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    
}
