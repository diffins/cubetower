using _Source.Configs;
using UnityEngine;
using Zenject;

public class ConfigsInstaller : MonoInstaller
{
    [SerializeField] private StockConfig _stockConfig;
    [SerializeField] private ColorsConfig _colorsConfig;
    [SerializeField] private ReportsConfig _reportsConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(_colorsConfig).AsSingle().Lazy();
        Container.BindInstance(_stockConfig).AsSingle().Lazy();
        Container.BindInstance(_reportsConfig).AsSingle().Lazy();
    }
}