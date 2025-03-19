using _Source.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUiController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _image;

    private ItemStockUiController _stockUiController;
    private ItemConfig _itemConfig;

    private Vector2 _firstDragPosition;
    private bool _checkDrag;
    
    private ItemStockUiInput _stockUiInput;
    
    public void Initialize(ItemStockUiController stockUiController, ItemConfig itemConfig)
    {
        _stockUiController = stockUiController;
        _itemConfig = itemConfig;
        _image.color = itemConfig.GetColor();

        _stockUiInput = GetComponentInParent<ItemStockUiInput>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _stockUiInput.HandlePointerDownItem(_itemConfig, eventData.position);
    }
}
