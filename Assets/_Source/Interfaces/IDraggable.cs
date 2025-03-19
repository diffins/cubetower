namespace _Source.Interfaces
{
    public interface IDraggable : IMovable
    {
        public virtual void OnStartDrag() {}

        public virtual void OnEndDrag() {}
        
        
    }
}