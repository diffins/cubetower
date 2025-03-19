using System;
using _Source.Interfaces;
using UnityEngine;

namespace _Source
{
    public class DestroyZone : MonoBehaviour
    {
        [SerializeField] private TrashholeInput _input;

        private bool _trashholeActive = false;
        private void Start()
        {
            _input.OnEnter += () => _trashholeActive = true;
            _input.OnExit += () => _trashholeActive = false;
        }

        public void Receive(IDestroyable destroyable)
        {
            if(_trashholeActive)
                destroyable.DestroyThrowTrash(_input.Position);
            else
                destroyable.DestroyImmediately();
        }
    }
}