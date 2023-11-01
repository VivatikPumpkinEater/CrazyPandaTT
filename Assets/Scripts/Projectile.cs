using System;
using UnityEngine;

public class Projectile : PoolObject
{
    private Rigidbody2D _rigidbody;
    
    private Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();

    public void AddForce(Vector2 force)
    {
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}