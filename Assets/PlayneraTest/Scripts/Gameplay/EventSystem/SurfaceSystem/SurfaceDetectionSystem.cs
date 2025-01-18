using System.Collections.Generic;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.SurfaceSystem
{
    public class SurfaceDetectionSystem : ISurfaceDetectionSystem
    {
        private readonly List<ISurfacePointerListener> _surfacePointerListeners;
        private ISurfacePointerListener _currentSurface;

        public SurfaceDetectionSystem()
        {
            _surfacePointerListeners = new();
        }
        
        public void Add(ISurfacePointerListener surfacePointerListener)
        {
            _surfacePointerListeners.Add(surfacePointerListener);
            surfacePointerListener.SurfaceEnter += OnSurfaceEnter;
            surfacePointerListener.SurfaceExit += OnSurfaceExit;
        }

        public void Remove(ISurfacePointerListener surfacePointerListener)
        {
            _surfacePointerListeners.Remove(surfacePointerListener);
            surfacePointerListener.SurfaceEnter -= OnSurfaceEnter;
            surfacePointerListener.SurfaceExit -= OnSurfaceExit;

        }

        public void RemoveAll()
        {
            for (int i = _surfacePointerListeners.Count; i > -1; i--)
            {
                Remove(_surfacePointerListeners[i]);
            }
        }
        
        private void OnSurfaceEnter(ISurfacePointerListener obj)
        {
            _currentSurface = obj;
        }
        
        private void OnSurfaceExit(ISurfacePointerListener obj)
        {
            if (_currentSurface == obj)
            {
                _currentSurface = null;
            }
        }
    }
}