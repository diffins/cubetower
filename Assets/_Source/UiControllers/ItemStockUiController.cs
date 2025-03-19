using _Source.Configs;
using _Source.Enums;
using _Source.UiControllers;
using UnityEngine;
using Zenject;

public class ItemStockUiController : MonoBehaviour
{
    [Inject] private StockConfig _stockConfig;
    [Inject] private ColorsConfig _colorsConfig;
    [Inject] private ItemPool _itemPool;
    [Inject] private ItemDragController _dragController;
    [Inject] private Reporter _reporter;

    [SerializeField] private ItemUiController _prefab;
    [SerializeField] private Transform _itemsUiRoot;

    private void Start()
    {
        foreach (var itemColor in _stockConfig.Items)
        {
            ItemUiController itemUiController = Instantiate(_prefab, _itemsUiRoot);
            var config = ScriptableObject.CreateInstance<ItemConfig>();
            config.ColorsConfig = _colorsConfig;
            config.Color = itemColor;
            itemUiController.Initialize(this, config);
        }
    }

    public void SpawnItem(ItemConfig itemConfigPointer)
    {
        var item = _itemPool.GetItem(itemConfigPointer);
        _dragController.SetDraggable(item, true);
        _reporter.Report(ReportType.GotCube);
    }
}