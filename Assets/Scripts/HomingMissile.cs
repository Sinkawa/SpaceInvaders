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
using System.Linq;
using UnityEngine;

public class HomingMissile : Projectile
{
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected int damage = 1;
    
    [SerializeField] protected ParticleSystem destroyEffect;

    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    
    [SerializeField] private float activationDelay = 1f;
    [SerializeField] private float maxRotationAngle = 2f;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        StartCoroutine(nameof(HomingMove));
    }
    
    private static Vector3 GetRandomEnemyPosition(out bool error)
    {
        error = false;
        var enemies = FindObjectsOfType<Enemy>();
        var enemiesCount = enemies.Count();

        if (enemiesCount != 0) 
            return enemies[Random.Range(0, enemiesCount)].transform.position;
        
        error = true;
        return new Vector3();
    }
    
    private IEnumerator HomingMove()
    {
        _rigidbody2D.velocity = movementSpeed * _transform.right;
        yield return new WaitForSeconds(activationDelay);
        
        var targetPosition = GetRandomEnemyPosition(out bool error);
        if (error)
            yield break;
        
        while (true)
        {
            yield return new WaitForFixedUpdate();
            var heading = _transform.right;
            var directionToTarget = targetPosition - _transform.position;

            var angle = Vector2.SignedAngle(heading, directionToTarget.normalized);
            angle = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle);

            _rigidbody2D.SetRotation(_rigidbody2D.rotation + angle);
            _rigidbody2D.velocity = movementSpeed * _transform.right;

            movementSpeed++;
        }
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