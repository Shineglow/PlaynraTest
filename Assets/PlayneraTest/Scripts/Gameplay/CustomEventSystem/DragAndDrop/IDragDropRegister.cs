using System;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public interface IDragDropRegister
    {
        bool IsDragNow { get; }
        bool IsHoldNow { get; }

        event Action<IDragDropItem, PointerEventData> EndDrag;
        
        void Add(IDragDropItem item);
        void Remove(IDragDropItem item);
    }
}
