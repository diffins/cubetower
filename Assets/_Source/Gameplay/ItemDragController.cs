using _Source.Interfaces;
using UnityEngine;

public class ItemDragController : MonoBehaviour
{
    private IDraggable _draggable;

    public void SetDraggable(IDraggable draggable, bool hardSetPosition = false)
    {
        _draggable = draggable;

        if(hardSetPosition)
            _draggable.Position.Value = GetMouseWorldPosition() + _draggable.PivotOffset;
        
        _draggable.OnStartDrag();
    }
    
    private void Update()
    {
        if (_draggable == null) return;

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
            return;
        }
        
        Vector3 newPosition = GetMouseWorldPosition() + _draggable.PivotOffset;
        _draggable.Position.Value = Vector3.Lerp( _draggable.Position.Value, newPosition, 20f * Time.deltaTime);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = 10; 
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void EndDrag()
    {
        _draggable.OnEndDrag();
        _draggable = null;
    }
}