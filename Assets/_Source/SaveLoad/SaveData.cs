using System.Collections.Generic;
using System.Numerics;
using _Source.Enums;

namespace _Source.SaveLoad
{
    [System.Serializable]
    public class SaveData
    {
        public List<ItemSaveData> Items = new List<ItemSaveData>();

        public void FillItems(ItemTower tower)
        {
            foreach (var item in tower.Items)
            {
                Items.Add(new ItemSaveData()
                {
                    Color = item.Color,
                    Position = new Vector3
                    {
                        X = item.Transform.position.x,
                        Y = item.Transform.position.y,
                        Z = item.Transform.position.z
                    }
                });
            }
        }
    }

    [System.Serializable]
    public class ItemSaveData
    {
        public ItemColor Color;
        public Vector3 Position;
    }
    
    
}