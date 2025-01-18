using System;

namespace PlayneraTest.Scripts.Gameplay.EventSystem.SurfaceSystem
{
    public interface ISurfacePointerListener
    {
        event Action<ISurfacePointerListener> SurfaceEnter;
        event Action<ISurfacePointerListener> SurfaceExit;
    }
}