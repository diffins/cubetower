using System.Collections;
using System.Collections.Generic;
using _Source;
using _Source.Configs;
using _Source.Enums;
using _Source.Interfaces;
using _Source.UiControllers;
using R3;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour, IDraggable, IDestroyable
{
    public ItemColor Color => _config.Color;
    public Vector3 PivotOffset => Vector3.down * .5f;
    public Transform Transform => transform;
    public BoxCollider2D Collider { get; private set; }
    public ReactiveProperty<Vector3> Position { get; set; } = new ReactiveProperty<Vector3>();
    
    [SerializeField] private ItemView _view;
    [SerializeField] private float _fallSpeed = 5f;
    
    private ItemConfig _config;
    private ItemPool _itemPool;
    private ItemDragController _dragController;
    private DestroyZone _destroyZone;
    private ItemTower _tower;
    private Reporter _reporter;
    
    private Coroutine _fallingCoroutine;
    private bool _active;

    [Inject]
    public void Construct(
        ItemPool pool,
        ItemDragController itemDragController,
        DestroyZone destroyZone,
        ItemTower tower,
        Reporter reporter,
        ItemConfig config = null)
    {
        _config = config;
        _itemPool = pool;
        _dragController = itemDragController;
        _destroyZone = destroyZone;
        _tower = tower;
        _reporter = reporter;
        
        Position.Subscribe(_view.OnChangePosition);
        Position.Subscribe(OnChangePosition);
        
        Collider = GetComponent<BoxCollider2D>();
        
        if(_config != null)
            Initialize(config);
    }

    public class Factory : PlaceholderFactory<ItemConfig, Item> { }
    
    public void Initialize(ItemConfig config)
    {
        _config = config;
        _view.Initialize(config);
        _active = true;
    }

    public void OnStartDrag()
    {
        Collider.enabled = false;
        _view.OnStartDrag();
    }

    public void OnEndDrag()
    {
        Collider.enabled = true;
        _view.OnEndDrag();
        PlaceItemInZone();
    }

    public void Fall(float targetY)
    {
        if (_fallingCoroutine != null) 
            StopCoroutine(_fallingCoroutine);
        
        _fallingCoroutine = StartCoroutine(FallCoroutine(targetY));
    }
    
    private IEnumerator FallCoroutine(float targetY)
    {
        var targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Position.Value = Vector3.MoveTowards(transform.position, targetPosition, _fallSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void DestroyImmediately()
    {
        _reporter.Report(ReportType.RemoveCube);
        _active = false;
        _tower.RemoveItem(this);
        _view.PlayDestroyImmediatelyAnimation(Destroy);
    }

    public void DestroyThrowTrash(Vector3 position)
    {
        _reporter.Report(ReportType.TrashCube);
        _active = false;
        _view.PlayDestroyThrowTrashAnimation(Destroy, position);
    }
    
    private void PlaceItemInZone()
    {
        var screenZone = ScreenZone.CheckPosition(transform.position);

        switch (screenZone)
        {
            case ScreenZoneType.TowerZone:
                _tower.Receive(this);
                break;
            case ScreenZoneType.DestroyZone:
                _destroyZone.Receive(this);
                break;
            case ScreenZoneType.OutOfScreen:
            default:
                DestroyImmediately();
                break;
        }
    }
    
    private void OnChangePosition(Vector3 position)
    {
        transform.position = position;
    }
    
    private void OnMouseDown()
    {
        if (!_active) return; 
        
        if(_fallingCoroutine != null)
            StopCoroutine(_fallingCoroutine);
        _tower.RemoveItem(this);
        _dragController.SetDraggable(this);
    }

    private void Destroy()
    {
        if(_fallingCoroutine != null) 
            StopCoroutine(_fallingCoroutine);
        _itemPool.ReturnItem(this);
    }
}