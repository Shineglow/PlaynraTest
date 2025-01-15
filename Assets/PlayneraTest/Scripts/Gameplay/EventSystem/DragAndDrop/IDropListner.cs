using Assets.PlayneraTest.Scripts.Gameplay.DragAndDrop;

namespace Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public interface IDropListner
    {
        void OnItemDroped(IDragDropItem item);
    }
}
