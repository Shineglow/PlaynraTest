namespace PlayneraTest.Scripts.Gameplay.EventSystem.SurfaceSystem
{
    public interface ISurfaceDetectionSystem
    {
        void Add(ISurfacePointerListener surfacePointerListener);
        void Remove(ISurfacePointerListener surfacePointerListener);
        void RemoveAll();
    }
}