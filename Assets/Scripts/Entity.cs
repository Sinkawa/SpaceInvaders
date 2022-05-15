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

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Entity : MonoBehaviour, IDamageable
{
    [Header("General")] 
    [SerializeField] protected float movementSpeed = 10f;
    [SerializeField] protected float shootCooldown = 2f;
    [SerializeField] private int gamePoints = 100;
    public int Points => gamePoints;

    [Header("Health and damage")] 
    [SerializeField] [Range(1, 10)] private int health = 3;
    [SerializeField] [Range(1, 10)] private int collisionDamage = 10;

    [Header("Damage indication")] 
    [SerializeField] private Color damageColor = Color.red;

    [SerializeField] private float flashingDuration = 0.5f;
    [SerializeField] private float flashingRate = 0.05f;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected ParticleSystem destroyEffect;

    protected GameObject _currentWeaponPrefab;

    public UnityEvent<int> healthChanged;
    public UnityEvent<Entity> destroyed;

    protected Rigidbody2D _rigidbody2D;
    protected Vector2 _movementDirection;
    protected Transform _transform;
    protected bool _shootingAllowed = true;

    private int _currentHealth;

    protected int Health
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            healthChanged.Invoke(value);
        }
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentWeaponPrefab = weaponPrefab;
        Health = health;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Entity entity))
        {
            entity.ApplyDamage(collisionDamage); 
        }
    }
    
    public void ApplyDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException();

        Health -= damage;
        
        if (Health <= 0)
            Destroy();
        else
            StartCoroutine(nameof(Flash));
    }

    private IEnumerator Flash()
    {
        var defaultColor = _spriteRenderer.color;

        var endTime = Time.time + flashingDuration;
        
        while (Time.time <= endTime)
        {
            
            _spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashingRate);
            
            _spriteRenderer.color = defaultColor;
            yield return new WaitForSeconds(flashingRate);
        }
    }
    
    private void Destroy()
    {
        destroyed.Invoke(this);
        
        Instantiate(destroyEffect, _transform.position, _transform.rotation);

        Destroy(gameObject);
    }
    
    protected virtual void Shoot() {}
}