using System;
using _Source.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStockUiInput : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;

    private bool _isPointerDownItem = false;
    private ItemConfig _itemConfigPointer;
    private Vector2 _positionOnPointerDown;

    private ItemStockUiController _stockUiController;
    
    private void Start()
    {
        _stockUiController = GetComponent<ItemStockUiController>();
    }

    public void HandlePointerDownItem(ItemConfig itemConfig, Vector2 position)
    {
        _isPointerDownItem = true;
        _itemConfigPointer = itemConfig;
        _positionOnPointerDown = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_isPointerDownItem)
        {
            var xDelta = eventData.position.x - _positionOnPointerDown.x;
            var yDelta = eventData.position.y - _positionOnPointerDown.y;
            if ((Math.Abs(yDelta) > Math.Abs(xDelta) || Math.Abs(Math.Abs(yDelta) - Math.Abs(xDelta)) < 5) && yDelta > 0)
            {
                _stockUiController.SpawnItem(_itemConfigPointer);
                _scrollRect.enabled = false;
            }
            _isPointerDownItem = false;
            _itemConfigPointer = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _scrollRect.enabled = true;
    }

}