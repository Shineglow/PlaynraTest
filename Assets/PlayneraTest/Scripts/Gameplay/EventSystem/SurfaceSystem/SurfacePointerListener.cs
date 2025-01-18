using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.SurfaceSystem
{
    public class SurfacePointerListener : MonoBehaviour, ISurfacePointerListener, IPointerEnterHandler, IPointerExitHandler
    {
        private ISurfaceDetectionSystem _surfaceDetectionSystem;
        public event Action<ISurfacePointerListener> SurfaceEnter;
        public event Action<ISurfacePointerListener> SurfaceExit;

        public void Init(ISurfaceDetectionSystem surfaceDetectionSystem)
        {
            _surfaceDetectionSystem = surfaceDetectionSystem;
            surfaceDetectionSystem.Add(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            SurfaceEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SurfaceExit?.Invoke(this);
        }

        private void OnDestroy()
        {
            _surfaceDetectionSystem?.Remove(this);
        }
    }
}