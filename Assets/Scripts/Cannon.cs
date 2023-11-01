using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Shoot settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _minBulletSpeed;
    [SerializeField] private float _maxBulletSpeed;
    [SerializeField] private float _shootDelay;
    
    [Space]
    [SerializeField] private Pool _pool;

    private float _timer;

    private Vector2 TargetPosition => _target.position;
    private Vector2 FirePosition => _firePoint.position;

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0f)
            return;

        _timer = _shootDelay;
        Fire();
    }

    private void Fire()
    {
        var bulletSpeed = Random.Range(_minBulletSpeed, _maxBulletSpeed);
        var direction = (TargetPosition - FirePosition).normalized;
        var force = direction * bulletSpeed;

        var projectile = _pool.GetFreeElement(FirePosition) as Projectile;
        projectile.Setup(direction);
    }
}