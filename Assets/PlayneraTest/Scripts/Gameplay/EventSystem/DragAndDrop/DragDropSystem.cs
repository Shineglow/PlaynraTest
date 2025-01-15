using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.DragAndDrop
{
    public class DragDropSystem : IDragDropRegister
    {
        private const float MaxDistance = 100f;
        private List<IDragDropItem> _items;
        private List<IDropListner> _listners;

        private Vector3 pointerToObjectCenterDelta;
        private ICamera _camera;
        private ICameraMovement _cameraMovement;
        private float _horizontalCameraMovementTriggerZone;

        public bool IsDragNow { get; private set; }

        public DragDropSystem(ICamera camera, ICameraMovement cameraMovement, float horizontalCameraMovementTriggerZone)
        {
            _camera = camera;
            _cameraMovement = cameraMovement;
            _items = new List<IDragDropItem>();
            _listners = new List<IDropListner>();
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
            pointerToObjectCenterDelta = item.Position - pointerPos;
        }

        private void OnStartDrag(IDragDropItem item, PointerEventData eventData)
        {
            IsDragNow = true;
        }

        private void OnDrag(IDragDropItem item, PointerEventData eventData)
        {
            var newPos = Get3DWorldPositionFor2D(eventData);
            item.Position = newPos + pointerToObjectCenterDelta;

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
            Ray ray = _camera.ViewportPointToRay(eventData.position);
            if(Physics.BoxCast(
                item.Position, 
                item.GetBoundingBox().extents, 
                _camera.Forward, 
                out var hitInfo,
                _camera.Rotation,
                MaxDistance,
                LayerMask.GetMask("DropListner")))
            {
                if(!hitInfo.transform.TryGetComponent<IDropListner>(out var dragDropItem))
                {
                    Debug.LogError($"Item with \"DropListner\" layer hasn't component of {nameof(IDropListner)}");
                }

                dragDropItem.OnItemDroped(item);
            }
        }

        private Vector3 Get3DWorldPositionFor2D(PointerEventData eventData)
        {
            var pointerPos = _camera.ScreenToWorldPoint(eventData.position);
            pointerPos.z = 0;
            return pointerPos;
        }
    }
}
