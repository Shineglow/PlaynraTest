using UnityEngine;

namespace PlayneraTest.Scripts.Gameplay.CameraScripts
{
    public interface ICameraMovement
    {
        void SlowlyMoveToTheRight();
        void SlowlyMoveToTheLeft();
        void SetPosition(Vector3 position);
    }
}
