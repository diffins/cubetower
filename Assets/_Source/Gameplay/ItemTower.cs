using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Source.Enums;
using _Source.UiControllers;
using UnityEngine;
using Zenject;

public class ItemTower : MonoBehaviour
{
    [Inject] private Reporter _reporter;
    
    [SerializeField] private GameObject _pivot;
    private List<Item> _towerItems = new List<Item>();
    private float HighPoint => _pivot.transform.position.y + _towerItems.Sum(item => item.Collider.size.y);
    
    private Coroutine _notifyCoroutine;

    public void Receive(Item item)
    {
        if (IsItemPositionValid(item))
        {
            item.Fall(HighPoint + item.Collider.size.y / 2);
            ApplyItem(item);
        }
        else
        {
            item.DestroyImmediately();
        }
    }

    public void RemoveItem(Item item)
    {
        if (_towerItems.Contains(item))
        {
            int index = _towerItems.IndexOf(item);
            _towerItems.RemoveAt(index);
            if(_notifyCoroutine != null) 
                StopCoroutine(_notifyCoroutine);
            _notifyCoroutine = StartCoroutine(NotifyItems());
        }
    }

    private void ApplyItem(Item item)
    {
        _towerItems.Add(item);
        _reporter.Report(ReportType.PlaceCube);
        if (ScreenZone.CheckPosition(new Vector3(0f, HighPoint, 0f)) == ScreenZoneType.OutOfScreen)
        {
            _reporter.Report(ReportType.TopHighTower);
        }
    }

    private IEnumerator NotifyItems()
    {
        for (int i = 0; i < _towerItems.Count; i++)
        {
            if (!IsItemPositionValid(_towerItems[i], i))
            {
                var item = _towerItems[i];
                item.DestroyImmediately();
                RemoveItem(item);
                yield break;
            }
            if (CheckHighPoint(i, out var highPoint))
            {
                _towerItems[i].Fall(highPoint +  _towerItems[i].Collider.size.y / 2);
                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
    }

    private bool CheckHighPoint(int index, out float highPoint)
    {
        highPoint = GetHighPoint(index);
        var itemBottomY = _towerItems[index].Transform.position.y - _towerItems[index].Collider.size.y / 2;
        return itemBottomY > highPoint;
    }

    private float GetHighPoint(int index)
    {
        var highPoint = _pivot.transform.position.y;

        if (index != 0)
        {
            for (int i = 0; i < index; i++)
            {
                var item = _towerItems[i];
                highPoint += item.Collider.size.y;
            }
        }
        return highPoint;
    }

    private bool IsItemPositionValid(Item newItem, int index = -1)
    {
        var validWidth = true;
        var validAboveTopItem = true;
        
        var itemBottomY = newItem.Transform.position.y - newItem.Collider.size.y / 2;
        if (_towerItems.Count > 0 && index != 0)
        {
            var topItem = index == -1 ? _towerItems[^1] : _towerItems[index - 1];
            var topItemX = topItem.Transform.position.x;
            var topItemWidth = topItem.Collider.size.x;
            validWidth = (Mathf.Abs(newItem.Transform.position.x - topItemX) <= topItemWidth / 2);
            
            var topItemTopY = topItem.Transform.position.y + topItem.Collider.size.y / 2;
            validAboveTopItem = (itemBottomY >= topItemTopY);
        }

        var validHeight = index switch
        {
            -1 => itemBottomY > HighPoint,
            0 => true,
            _ => itemBottomY >= GetHighPoint(index)
        };

        return validWidth && validHeight && validAboveTopItem;
    }
    
}