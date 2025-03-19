using System.Collections;
using System.Collections.Generic;
using _Source.Configs;
using _Source.SaveLoad;
using UnityEngine;
using Zenject;

public class SaveLoader : MonoBehaviour
{
    [Inject] private ItemPool _itemPool;
    [Inject] private ItemTower _tower;
    [Inject] private ColorsConfig _colorsConfig;
    
    private void Start()
    {
        SaveData loadedData = SaveLoadManager.LoadPlayerData();
        
        if (loadedData != null)
        {
            var list = new List<Item>();
            foreach (var item in loadedData.Items)
            {
                var config = ScriptableObject.CreateInstance<ItemConfig>();
                config.ColorsConfig = _colorsConfig;
                config.Color = item.Color;
                var initItem = _itemPool.GetItem(config);
                initItem.Transform.position = new Vector3(item.Position.X, item.Position.Y, item.Position.Z);
                list.Add(initItem);
            }
            _tower.UploadSavedData(list);
        }
    }
}
