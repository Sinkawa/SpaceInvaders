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

public class HomingBullet : Bullet
{
    [SerializeField] private float activationDelay = 1f;
    [SerializeField] private float maxRotationAngle = 2f;
    
    protected override void Move()
    {
        _rigidbody2D.velocity = movementSpeed * _transform.right;
        StartCoroutine(nameof(HomingMove));
    }
    
    private Vector3 GetRandomEnemyPosition()
    {
        var enemies = FindObjectsOfType<Enemy>();
        return enemies[Random.Range(0, enemies.Count())].transform.position;
    }
    
    private IEnumerator HomingMove()
    {
        yield return new WaitForSeconds(activationDelay);
        var targetPosition = GetRandomEnemyPosition();
        Debug.Log($"target position: {targetPosition}");
        while (true)
        {
            yield return new WaitForFixedUpdate();
            var heading = _transform.right;
            Debug.Log($"heading: {heading}");
            var directionToTarget = targetPosition - _transform.position;
            Debug.Log($"direction to target: {directionToTarget}");

            var angle = Vector2.SignedAngle(heading, directionToTarget.normalized);
            Debug.Log($"angle: {angle}");
            angle = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle);
            Debug.Log($"angle (clamped): {angle}");

            _rigidbody2D.SetRotation(_rigidbody2D.rotation + angle);
            Debug.Log($"_rigidbody2D rotation: {_rigidbody2D.rotation}");
            Debug.Log($"_transform rotation: {_transform.rotation}");
            _rigidbody2D.velocity = movementSpeed * _transform.right;
        }
    }
    
    
}