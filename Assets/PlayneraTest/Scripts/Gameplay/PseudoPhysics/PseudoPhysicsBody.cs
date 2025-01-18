using UnityEngine;

namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public class PseudoPhysicsBody : MonoBehaviour, IPseudoPhysicsBody
    {
        public float GroundPositionY { get; set; }
        public float VerticalVelocity { get; set; }
        public float VerticalAcceleration { get; set; }

        public bool IsTouchedGround { get; set; } = false;
        public bool IsGroundPositionDirty { get; set; } = true;
        public bool IsActive { get; set; }

        [field: SerializeField] public EItemType Type {  get; private set; }
    }
}