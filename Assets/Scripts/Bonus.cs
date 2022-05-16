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
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bonus : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool randomizeAngle = true;
    
    [SerializeField][Range(0f, 60f)] private float randomAngleLimits = 10f;
    [SerializeField][Range(0f, 45f)] private float angleLimits = 30f;

    [SerializeField] private GameObject weaponPrefab;
    
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private float _maxRotation;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _maxRotation = _rigidbody2D.rotation + angleLimits;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = movementSpeed * _transform.right;
        if (randomizeAngle)
            _rigidbody2D.rotation = RandomizeRotation();
    }

    private float RandomizeRotation()
    {
        var angle = _rigidbody2D.rotation + Random.Range(-randomAngleLimits, randomAngleLimits);
        return Mathf.Clamp(angle, -_maxRotation, _maxRotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.PickUp(weaponPrefab);
            
            Destroy(gameObject);
        }
    }
}