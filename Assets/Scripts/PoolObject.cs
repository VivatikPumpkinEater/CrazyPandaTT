using UnityEngine;

public class PoolObject : MonoBehaviour, IPoolObject
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}