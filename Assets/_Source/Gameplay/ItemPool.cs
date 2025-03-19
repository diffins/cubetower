using System.Collections.Generic;
using _Source.Configs;
using UnityEngine;
using Zenject;

public class ItemPool : MonoBehaviour
{
    [Inject] private readonly Item.Factory _itemFactory;
    [SerializeField] private int _initialPoolSize = 10;

    private Queue<Item> _pool = new Queue<Item>();

    private void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            var item = _itemFactory.Create(null);
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }

    public Item GetItem(ItemConfig config)
    {
        if (_pool.Count > 0)
        {
            var item = _pool.Dequeue();
            item.gameObject.SetActive(true);
            item.Initialize(config);
            return item;
        }

        var newItem = _itemFactory.Create(config);
        newItem.Initialize(config);
        return newItem;
    }

    public void ReturnItem(Item item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
    }
}