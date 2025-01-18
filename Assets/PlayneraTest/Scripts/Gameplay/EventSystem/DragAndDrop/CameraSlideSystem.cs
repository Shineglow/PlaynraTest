using System;
using System.Collections.Generic;
using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public class CameraSlideSystem : IDragDropRegister
    {
        private readonly List<IDragDropItem> _items;

        private Vector3 _pointerStartPos;
        private readonly ICamera _camera;
        private readonly ICameraMovement _cameraMovement;

        public CameraSlideSystem(ICamera camera, ICameraMovement cameraMovement)
        {
            _camera = camera;
            _cameraMovement = cameraMovement;
            _items = new List<IDragDropItem>();
        }

        public event Action<IDragDropItem, PointerEventData> EndDrag;

        public void Add(IDragDropItem item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.Drag += OnDrag;
            item.EndDrag += OnEndDrag;
        }

        private void OnEndDrag(IDragDropItem item, PointerEventData eventData)
        {
            EndDrag?.Invoke(item, eventData);
        }

        public void Remove(IDragDropItem item)
        {
            if (_items.Remove(item))
            {
                item.PointerDown -= OnPointerDown;
                item.Drag -= OnDrag;
            }
        }

        private void OnPointerDown(IDragDropItem item, PointerEventData eventData)
        {
            _pointerStartPos = _camera.ScreenToWorldPoint(eventData.position);
        }

        private void OnDrag(IDragDropItem item, PointerEventData eventData)
        {
            var deltaX = GetDeltaX(eventData);
            _cameraMovement.SetPosition(_camera.Position + deltaX);
        }

        private Vector3 GetDeltaX(PointerEventData eventData)
        {
            var pos = _camera.ScreenToWorldPoint(eventData.position);
            var pointerPos = _pointerStartPos - pos;
            pointerPos.z = 0;
            pointerPos.y = 0;
            return pointerPos;
        }
    }
}