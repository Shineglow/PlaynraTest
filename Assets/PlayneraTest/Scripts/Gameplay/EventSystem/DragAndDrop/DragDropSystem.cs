using System;
using System.Collections.Generic;
using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public class DragDropSystem : IDragDropRegister
    {
        private readonly List<IDragDropItem> _items;
        private readonly List<IDropListener> _listners;

        private Vector3 _pointerToObjectCenterDelta;
        private readonly ICamera _camera;
        private readonly ICameraMovement _cameraMovement;
        private readonly float _horizontalCameraMovementTriggerZone;

        public event Action<IDragDropItem, PointerEventData> EndDrag;

        public bool IsDragNow { get; private set; }

        public DragDropSystem(
            ICamera camera, 
            ICameraMovement cameraMovement, 
            float horizontalCameraMovementTriggerZone)
        {
            _camera = camera;
            _cameraMovement = cameraMovement;
            _items = new List<IDragDropItem>();
            _listners = new List<IDropListener>();
            _horizontalCameraMovementTriggerZone = horizontalCameraMovementTriggerZone;
        }

        public void Add(IDragDropItem item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.Drag += OnDrag;
            item.EndDrag += OnEndDrag;
        }

        public void Remove(IDragDropItem item)
        {
            _items.Remove(item);
            item.PointerDown += OnPointerDown;
            item.Drag += OnDrag;
        }

        public void RemoveAllItems()
        {
            for (int i = _items.Count-1; i > -1; i--)
            {
                Remove(_items[i]);
            }
        }

        private void OnPointerDown(IDragDropItem item, PointerEventData eventData)
        {
            Vector3 pointerPos = Get3DWorldPositionFor2D(eventData);
            _pointerToObjectCenterDelta = item.Position - pointerPos;
        }

        private void OnStartDrag(IDragDropItem item, PointerEventData eventData)
        {
            IsDragNow = true;
        }

        private void OnDrag(IDragDropItem item, PointerEventData eventData)
        {
            var newPos = Get3DWorldPositionFor2D(eventData);
            item.Position = newPos + _pointerToObjectCenterDelta;

            var horizontalPos = _camera.ScreenToViewportPoint(eventData.position).x; // 0 .. 1
            if (horizontalPos < _horizontalCameraMovementTriggerZone)
            {
                _cameraMovement.SlowlyMoveToTheLeft();
            }
            else if((1f - horizontalPos) < _horizontalCameraMovementTriggerZone)
            {
                _cameraMovement.SlowlyMoveToTheRight();
            }
        }

        private void OnEndDrag(IDragDropItem item, PointerEventData eventData) 
        {
            IsDragNow = false;
            EndDrag?.Invoke(item, eventData);
        }

        private Vector3 Get3DWorldPositionFor2D(PointerEventData eventData)
        {
            var pointerPos = _camera.ScreenToWorldPoint(eventData.position);
            pointerPos.z = 0;
            return pointerPos;
        }
    }
}
