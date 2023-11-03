using UnityEngine;

public class Projectile : PoolObject, IDecelerate
{
    private Rigidbody2D _rigidbody;

    private Vector2 _currentDirection;
    private Vector2 _gravity;
    private float _currentSpeed;

    private Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();
    public float SlowdownFactor { get; set; }

    private void Awake()
    {
        _gravity = Vector2.down * Rigidbody.mass; 
    }

    public void Setup(Vector2 direction, float speed)
    {
        SlowdownFactor = 1f;
        _currentSpeed = speed;
        _currentDirection = direction;
    }
    
    private void FixedUpdate()
    {
        var fixedDelta = Time.fixedDeltaTime * SlowdownFactor;
        _currentDirection = Vector2.Lerp(_currentDirection, _gravity, 0.2f * fixedDelta);
        var newPosition = Rigidbody.position + _currentDirection * (_currentSpeed * fixedDelta);
        Rigidbody.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var dir = Rigidbody.position - (Vector2)other.gameObject.transform.position;
        _currentDirection = dir.normalized;
    }
}