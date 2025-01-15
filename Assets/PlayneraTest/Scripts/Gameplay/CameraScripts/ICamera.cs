using UnityEngine;

namespace Assets.PlayneraTest.Scripts.Gameplay.CameraScripts
{
    public interface ICamera
    {
        
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        Vector3 Forward { get; }
        Ray ViewportPointToRay(Vector3 pos);
        Vector3 ScreenToWorldPoint(Vector3 pos);
        Vector3 ScreenToViewportPoint(Vector3 pos);
    }
}
