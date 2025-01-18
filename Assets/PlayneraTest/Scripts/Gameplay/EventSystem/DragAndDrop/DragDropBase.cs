using System;
using Assets.PlayneraTest.Scripts.Gameplay.EventSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop
{
    public class DragDropBase : MonoBehaviour, IDragDropItem, IDragDropComplex, IClickComplex
    {
        private IDragDropRegister _dragDropRegister;

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        public event Action<IDragDropItem, PointerEventData> BeginDrag;
        public event Action<IDragDropItem, PointerEventData> Drag;
        public event Action<IDragDropItem, PointerEventData> EndDrag;
        public event Action<IDragDropItem, PointerEventData> PointerDown;
        public event Action<IDragDropItem, PointerEventData> PointerUp;

        public Bounds GetBoundingBox()
        {
            return GetComponent<Renderer>().bounds;
        }

        public void Init(IDragDropRegister dragDropRegister)
        {
            _dragDropRegister = dragDropRegister;
            _dragDropRegister.Add(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log($"{nameof(OnBeginDrag)}");
            BeginDrag?.Invoke(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"{nameof(OnDrag)}");
            Drag?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"{nameof(OnEndDrag)}");
            EndDrag?.Invoke(this, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{nameof(OnPointerDown)}");
            PointerDown?.Invoke(this, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"{nameof(OnPointerUp)}");
            PointerUp?.Invoke(this, eventData);
        }

        private void OnDestroy()
        {
            _dragDropRegister?.Remove(this);
        }
    }
}
