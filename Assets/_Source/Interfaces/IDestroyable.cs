using UnityEngine;

namespace _Source.Interfaces
{
    public interface IDestroyable : IMovable
    {
        public void DestroyImmediately();
        public void DestroyThrowTrash(Vector3 position);
    }
}