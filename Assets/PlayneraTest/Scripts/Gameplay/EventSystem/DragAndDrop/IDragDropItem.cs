using Assets.PlayneraTest.Scripts.General;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public interface IDragDropItem : ITransformAccessorFull
    {
        event Action<IDragDropItem, PointerEventData> BeginDrag;
        event Action<IDragDropItem, PointerEventData> Drag;
        event Action<IDragDropItem, PointerEventData> EndDrag;
        event Action<IDragDropItem, PointerEventData> PointerDown;
        event Action<IDragDropItem, PointerEventData> PointerUp;

        Bounds GetBoundingBox();
    }
}
