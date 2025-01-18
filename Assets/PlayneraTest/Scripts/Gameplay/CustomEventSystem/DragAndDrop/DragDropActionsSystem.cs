using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop
{
    internal class DragDropActionsSystem : IDragDropRegister
    {
        private readonly List<IDragDropItem> _items;
        private readonly List<DragDropAbstractAction> _onPointerDownActions;
        private readonly List<DragDropAbstractAction> _onDragActions;
        private readonly List<DragDropAbstractAction> _onPointerUpActions;
        private readonly ICamera _camera;
        private readonly ICameraMovement _cameraMovement;
        private readonly IEventSystemAccessor _eventSytemAccesor;
        private readonly float _horizontalCameraMovementTriggerZone;

        public event Action<IDragDropItem, PointerEventData> EndDrag;

        public bool IsDragNow { get; private set; }

        public bool IsHoldNow { get; private set; }

        private AdditionalDragDropInfo _additionalDragDropInfo;

        public DragDropActionsSystem(
            ICamera camera,
            ICameraMovement cameraMovement,
            IEventSystemAccessor eventSytemAccesor,
            List<DragDropAbstractAction> onPointerDownActions,
            List<DragDropAbstractAction> onDragActions,
            List<DragDropAbstractAction> onPointerUpActions,
            float horizontalCameraMovementTriggerZone)
        {
            _camera = camera;
            _cameraMovement = cameraMovement;
            _eventSytemAccesor = eventSytemAccesor;
            _items = new List<IDragDropItem>();
            _onPointerDownActions = onPointerDownActions ?? new List<DragDropAbstractAction>();
            _onDragActions = onDragActions ?? new List<DragDropAbstractAction>();
            _onPointerUpActions = onPointerUpActions ?? new List<DragDropAbstractAction>();
            _horizontalCameraMovementTriggerZone = horizontalCameraMovementTriggerZone;

            _additionalDragDropInfo.Camera = _camera;
            _additionalDragDropInfo.CameraMovement = _cameraMovement;
            _additionalDragDropInfo.DragDropRegister = this;
        }

        public void Add(IDragDropItem item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.Drag += OnDrag;
            item.EndDrag += OnEndDrag;
            item.BeginDrag += OnStartDrag;
        }

        public void Remove(IDragDropItem item)
        {
            _items.Remove(item);
            item.PointerDown -= OnPointerDown;
            item.Drag -= OnDrag;
            item.PointerUp -= OnPointerUp;
            item.EndDrag -= OnEndDrag;
            item.BeginDrag -= OnStartDrag;
        }

        public void RemoveAllItems()
        {
            for (int i = _items.Count - 1; i > -1; i--)
            {
                Remove(_items[i]);
            }
        }

        private void OnPointerDown(IDragDropItem item, PointerEventData eventData)
        {
            IsHoldNow = true;

            Vector3 pointerPos = Get3DWorldPositionFor2D(eventData);
            _additionalDragDropInfo.PointerStartWorldPosition = pointerPos;
            _additionalDragDropInfo.ItemStartWorldPosition = item.Position;

            _onPointerDownActions.ForEach(a => a.DoAction(item, eventData, _additionalDragDropInfo));
        }

        private void OnPointerUp(IDragDropItem item, PointerEventData eventData)
        {
            IsHoldNow = false;
            _onPointerUpActions.ForEach(a => a.DoAction(item, eventData, _additionalDragDropInfo));
        }

        private void OnStartDrag(IDragDropItem item, PointerEventData eventData)
        {
            IsDragNow = true;
        }

        private void OnDrag(IDragDropItem item, PointerEventData eventData)
        {
            _onDragActions.ForEach(a => a.DoAction(item, eventData, _additionalDragDropInfo));

            // need to fix, camera don't slide when drag
            var horizontalPos = _camera.ScreenToViewportPoint(eventData.position).x; // 0 .. 1
            if (horizontalPos < _horizontalCameraMovementTriggerZone)
            {
                _cameraMovement.SlowlyMoveToTheLeft();
            }
            else if ((1f - horizontalPos) < _horizontalCameraMovementTriggerZone)
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
