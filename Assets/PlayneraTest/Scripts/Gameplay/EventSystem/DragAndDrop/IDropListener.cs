using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public interface IDropListener
    {
        void OnItemDropped(IDragDropItem item, PointerEventData eventData);
    }
}
