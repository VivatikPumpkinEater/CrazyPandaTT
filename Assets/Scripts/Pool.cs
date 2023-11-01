using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _startCapacity;

    private List<PoolObject> _pool = new();

    private void Awake()
    {
        _pool.Clear();
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<PoolObject>();

        for (var i = 0; i < _startCapacity; i++)
            CreateElement();
    }

    private PoolObject CreateElement()
    {
        var createObject = Instantiate(_prefab, _container);
        createObject.gameObject.SetActive(false);

        _pool.Add(createObject);

        return createObject;
    }

    private void TryGetElement(out PoolObject element)
    {
        foreach (var i in _pool)
        {
            if (i.gameObject.activeInHierarchy)
                continue;
            
            element = i;
            i.gameObject.SetActive(true);
            return;
        }

        element = CreateElement();
        element.gameObject.SetActive(true);
    }

    public PoolObject GetFreeElement()
    {
        TryGetElement(out var element);
        return element;
    }

    public PoolObject GetFreeElement(Vector2 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }
}