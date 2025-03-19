using System.Collections.Generic;
using _Source.Enums;
using UnityEngine;

namespace _Source.Configs
{
    [CreateAssetMenu(fileName = "StockConfig", menuName = "Configs/StockConfig")]
    public class StockConfig : ScriptableObject
    {
        public List<ItemColor> Items;
    }
}