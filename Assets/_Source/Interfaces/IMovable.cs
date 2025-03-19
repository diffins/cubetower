using R3;
using UnityEngine;

namespace _Source.Interfaces
{
    public interface IMovable
    {
        public abstract Vector3 PivotOffset { get; }
        public abstract Transform Transform { get; }
        
        public abstract ReactiveProperty<Vector3> Position { get; set; }
    }
}