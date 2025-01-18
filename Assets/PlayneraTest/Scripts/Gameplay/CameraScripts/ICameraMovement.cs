using Assets.PlayneraTest.Scripts.General;
using UnityEngine;

namespace PlayneraTest.Scripts.Gameplay.CameraScripts
{
    public interface ICameraMovement : ITransformAccessorFull
    {
        void SlowlyMoveToTheRight();
        void SlowlyMoveToTheLeft();
        void SetPosition(Vector3 position);
    }
}
