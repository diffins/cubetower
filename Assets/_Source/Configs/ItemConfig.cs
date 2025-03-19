using _Source.Enums;
using UnityEngine;

namespace _Source.Configs
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public ColorsConfig ColorsConfig;
        public ItemColor Color;

        public Color GetColor()
        {
            return ColorsConfig.GetColor(Color);
        }
    }
}