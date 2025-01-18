using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop
{
    public abstract class DragDropAbstractAction
    {
        public abstract void DoAction(IDragDropItem dragDropItem, PointerEventData eventData, AdditionalDragDropInfo additionalDragDropInfo);
    }
}
