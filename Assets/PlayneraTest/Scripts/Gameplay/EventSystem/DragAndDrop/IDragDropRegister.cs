using Assets.PlayneraTest.Scripts.Gameplay.DragAndDrop;

namespace Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public interface IDragDropRegister
    {
        void Add(IDragDropItem item);
        void Remove(IDragDropItem item);
    }
}
