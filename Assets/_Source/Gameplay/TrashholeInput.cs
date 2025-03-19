using System;
using UnityEngine;

public class TrashholeInput : MonoBehaviour
{
    public event Action OnExit;
    public event Action OnEnter;
    
    public Vector3 Position => transform.position;

    private void OnMouseEnter()
    {
        OnEnter?.Invoke();
    }
    
    private void OnMouseExit()
    {
        OnExit?.Invoke();
    }
}
