using System;
using UnityEngine;

public class Projectile : PoolObject, ISlowdowner
{
    private Rigidbody2D _rigidbody;

    private Vector2 _currentDirection;
    
    private Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();
    public float SlowdownFactor { get; set; }

    public void Setup(Vector2 direction)
    {
        SlowdownFactor = 1f;
        _currentDirection = direction;
    }
    
    private void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + _currentDirection * SlowdownFactor);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // var a = Mathf.Abs(Rigidbody.velocity - other.relativeVelocity)
        _currentDirection *= -1f;
    }
}