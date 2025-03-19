using System;
using System.Collections.Generic;
using System.Linq;
using _Source.Enums;
using UnityEngine;

namespace _Source.Configs
{
    [CreateAssetMenu(fileName = "ColorsConfig", menuName = "Configs/ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {
        public List<ColorConfig> Elements = new List<ColorConfig>();
        
        public Color GetColor(ItemColor type)
        {
            return Elements.FirstOrDefault(x => x.Type == type)!.Color;
        }
        
        [Serializable]
        public struct ColorConfig
        {
            public ItemColor Type;
            public Color Color;
        }

    }
}