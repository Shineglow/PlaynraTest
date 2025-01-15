using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.DragAndDrop
{
    public interface IDragDropItem
    {
        Vector3 Position { get; set; }

        event Action<IDragDropItem, PointerEventData> BeginDrag;
        event Action<IDragDropItem, PointerEventData> Drag;
        event Action<IDragDropItem, PointerEventData> EndDrag;
        event Action<IDragDropItem, PointerEventData> PointerDown;
        event Action<IDragDropItem, PointerEventData> PointerUp;

        Bounds GetBoundingBox();
    }
}
