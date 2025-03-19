using _Source;
using _Source.Configs;
using _Source.UiControllers;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [SerializeField] private ItemPool _itemPool;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private ItemDragController _itemDragController;
    [SerializeField] private DestroyZone _destroyZone;
    [SerializeField] private ItemTower _itemTower;
    [SerializeField] private Reporter _reporter;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_itemDragController).AsSingle().Lazy();
        Container.BindInstance(_reporter).AsSingle().Lazy();
        Container.BindInstance(_itemPool).AsSingle().Lazy();
        Container.BindInstance(_destroyZone).AsSingle().Lazy();
        Container.BindInstance(_itemTower).AsSingle().Lazy();
        Container.BindFactory<ItemConfig, Item, Item.Factory>().FromComponentInNewPrefab(_itemPrefab);
    }
}